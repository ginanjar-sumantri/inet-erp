using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Transactions;
using System.IO;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.Web.UI.WebControls;
using System.Web;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class ScreeningProcessBL : Base
    {
        public ScreeningProcessBL()
        {

        }

        #region HRM_ScreeningProcess
        public double RowsCountHRMScreeningProcess(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                           from _screeningProcess in this.db.HRM_ScreeningProcesses
                           where (SqlMethods.Like(_screeningProcess.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                              && (SqlMethods.Like((_screeningProcess.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                           select _screeningProcess.ScreeningScheduleCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<HRM_ScreeningProcess> GetListHRMScreeningProcess(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRM_ScreeningProcess> _result = new List<HRM_ScreeningProcess>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _screeningProcess in this.db.HRM_ScreeningProcesses
                                where (SqlMethods.Like(_screeningProcess.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like((_screeningProcess.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _screeningProcess.EditDate descending
                                select new
                                {
                                    ScreeningScheduleCode = _screeningProcess.ScreeningScheduleCode,
                                    ScreeningSchedule = (
                                                            from _screenSchedule in this.db.HRM_ScreeningSchedules
                                                            where _screenSchedule.ScreeningScheduleCode == _screeningProcess.ScreeningScheduleCode
                                                            select _screenSchedule.FileNmbr
                                                        ).FirstOrDefault(),
                                    TransNmbr = _screeningProcess.TransNmbr,
                                    FileNmbr = _screeningProcess.FileNmbr,
                                    TransDateBegin = _screeningProcess.TransDateBegin,
                                    TransDateEnd = _screeningProcess.TransDateEnd,
                                    Status = _screeningProcess.Status,
                                    Institution = _screeningProcess.Institution,
                                    Remark = _screeningProcess.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_ScreeningProcess(_row.ScreeningScheduleCode, _row.ScreeningSchedule, _row.TransNmbr, _row.FileNmbr, _row.TransDateBegin, _row.TransDateEnd, _row.Status, _row.Institution, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_ScreeningProcess GetSingleHRMScreeningProcess(Guid _prmCode)
        {
            HRM_ScreeningProcess _result = null;

            try
            {
                _result = this.db.HRM_ScreeningProcesses.Single(_temp => _temp.ScreeningScheduleCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiHRMScreeningProcess(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                List<string> _fileName = new List<string>();

                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_ScreeningProcess _screeningProcess = this.db.HRM_ScreeningProcesses.Single(_temp => _temp.ScreeningScheduleCode == new Guid(_prmCode[i]));

                    if (_screeningProcess != null)
                    {
                        if ((_screeningProcess.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (
                                              from _detail in this.db.HRM_ScreeningProcessAttachments
                                              where _detail.ScreeningScheduleCode == new Guid(_prmCode[i])
                                              select _detail);

                            foreach (HRM_ScreeningProcessAttachment _row in _query)
                            {
                                _fileName.Add(_row.File);
                            }

                            var _query2 = (
                                              from _detail2 in this.db.HRM_ScreeningProcessComments
                                              where _detail2.ScreeningScheduleCode == new Guid(_prmCode[i])
                                              select _detail2);

                            this.db.HRM_ScreeningProcessAttachments.DeleteAllOnSubmit(_query);
                            this.db.HRM_ScreeningProcessComments.DeleteAllOnSubmit(_query2);

                            this.db.HRM_ScreeningProcesses.DeleteOnSubmit(_screeningProcess);

                            _result = true;
                        }
                        else
                        {
                            _result = false;
                            break;
                        }
                    }
                }

                if (_result == true)
                {
                    this.db.SubmitChanges();

                    foreach (String _item in _fileName)
                    {
                        if (File.Exists(ApplicationConfig.ScreeningProcessAttachmentPath + _item.Trim()) == true)
                        {
                            File.Delete(ApplicationConfig.ScreeningProcessAttachmentPath + _item.Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _result = false;
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string AddHRMScreeningProcess(HRM_ScreeningProcess _prmHRM_ScreeningProcess)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmHRM_ScreeningProcess.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRM_ScreeningProcesses.InsertOnSubmit(_prmHRM_ScreeningProcess);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmHRM_ScreeningProcess.ScreeningScheduleCode.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditHRMScreeningProcess(HRM_ScreeningProcess _prmHRM_ScreeningProcess)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetApproval(Guid _prmScreeningProcessCode, string _prmUserName)
        {
            string _result = "";

            try
            {
                int _success = this.db.spHRM_ScreeningProcessGetAppr(_prmScreeningProcessCode, _prmUserName, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMScreeningProcess);
                    _transActivity.TransNmbr = _prmScreeningProcessCode.ToString();
                    _transActivity.FileNmbr = "";
                    _transActivity.Username = HttpContext.Current.User.Identity.Name;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();

                    _result = "Waiting For Approval Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Waiting For Approval Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Approve(Guid _prmScreeningProcessCode, string _prmUserName)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    int _success = this.db.spHRM_ScreeningProcessApprove(_prmScreeningProcessCode, _prmUserName, ref _result);

                    if (_result == "")
                    {
                        HRM_ScreeningProcess _screeningProcess = this.GetSingleHRMScreeningProcess(_prmScreeningProcessCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_screeningProcess.TransDateBegin.Year, _screeningProcess.TransDateEnd.Month, AppModule.GetValue(TransactionType.HRMScreeningProcess), this._companyTag, ""))
                        {
                            _screeningProcess.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.HRMScreeningProcess);
                        _transActivity.TransNmbr = _prmScreeningProcessCode.ToString();
                        _transActivity.FileNmbr = _screeningProcess.FileNmbr;
                        _transActivity.Username = HttpContext.Current.User.Identity.Name;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        
                        this.db.SubmitChanges();

                        _scope.Complete();

                        _result = "Approve Success";
                    }
                }
            }
            catch (Exception ex)
            {
                _result = "Approve Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }
        #endregion

        #region HRM_ScreeningProcessComment
        public double RowsCountHRM_ScreeningProcessComment(string _prmCode)
        {
            double _result = 0;

            var _query =
             (
                from _applicantScreening in this.db.HRM_ScreeningProcessComments
                where _applicantScreening.ScreeningScheduleCode == new Guid(_prmCode)
                select _applicantScreening.ScreeningProcessCommentCode
             ).Count();

            _result = _query;

            return _result;
        }

        public List<HRM_ScreeningProcessComment> GetListHRM_ScreeningProcessComment(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRM_ScreeningProcessComment> _result = new List<HRM_ScreeningProcessComment>();

            try
            {
                var _query = (
                                from _screeningProcess in this.db.HRM_ScreeningProcessComments
                                where _screeningProcess.ScreeningScheduleCode == new Guid(_prmCode)
                                orderby _screeningProcess.EditDate descending
                                select new
                                {
                                    ScreeningProcessCommentCode = _screeningProcess.ScreeningProcessCommentCode,
                                    ScreeningScheduleCode = _screeningProcess.ScreeningScheduleCode,
                                    EmpNumb = _screeningProcess.EmpNumb,
                                    EmpName = (
                                                from _emp in this.db.MsEmployees
                                                where _screeningProcess.EmpNumb == _emp.EmpNumb
                                                select _emp.EmpName
                                              ).FirstOrDefault(),
                                    CommentStatusCode = _screeningProcess.CommentStatusCode,
                                    CommentStatus = (
                                                        from _commentStatus in this.db.HRM_Master_CommentStatus
                                                        where _commentStatus.CommentStatusCode == _screeningProcess.CommentStatusCode
                                                        select _commentStatus.CommentStatusName
                                                    ).FirstOrDefault(),
                                    Comment = _screeningProcess.Comment
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_ScreeningProcessComment(_row.ScreeningProcessCommentCode, _row.ScreeningScheduleCode, _row.EmpNumb, _row.EmpName, _row.CommentStatusCode, _row.CommentStatus, _row.Comment));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_ScreeningProcessComment GetSingleHRM_ScreeningProcessComment(string _prmCode)
        {
            HRM_ScreeningProcessComment _result = null;

            try
            {
                _result = this.db.HRM_ScreeningProcessComments.Single(_temp => _temp.ScreeningProcessCommentCode == new Guid(_prmCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiHRM_ScreeningProcessComment(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_ScreeningProcessComment _screeningProcessAttachment = this.db.HRM_ScreeningProcessComments.Single(_temp => _temp.ScreeningProcessCommentCode == new Guid(_prmCode[i]));

                    this.db.HRM_ScreeningProcessComments.DeleteOnSubmit(_screeningProcessAttachment);
                }

                this.db.SubmitChanges();

                _result = true;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddHRM_ScreeningProcessComment(HRM_ScreeningProcessComment _prmHRM_ScreeningProcessComment)
        {
            bool _result = false;

            try
            {
                this.db.HRM_ScreeningProcessComments.InsertOnSubmit(_prmHRM_ScreeningProcessComment);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditHRM_ScreeningProcessComment(HRM_ScreeningProcessComment _prmHRM_ScreeningProcessComment)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        #region HRM_ScreeningProcessAttachment
        public double RowsCountHRM_ScreeningProcessAttachment(string _prmCode)
        {
            double _result = 0;

            var _query =
             (
                from _applicantScreeningAttachment in this.db.HRM_ScreeningProcessAttachments
                where _applicantScreeningAttachment.ScreeningScheduleCode == new Guid(_prmCode)
                select _applicantScreeningAttachment.ProcessAttachmentCode
             ).Count();

            _result = _query;

            return _result;
        }

        public List<HRM_ScreeningProcessAttachment> GetListHRM_ScreeningProcessAttachment(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRM_ScreeningProcessAttachment> _result = new List<HRM_ScreeningProcessAttachment>();

            try
            {
                var _query = (
                                from _screeningProcessAttachment in this.db.HRM_ScreeningProcessAttachments
                                where _screeningProcessAttachment.ScreeningScheduleCode == new Guid(_prmCode)
                                orderby _screeningProcessAttachment.EditDate descending
                                select new
                                {
                                    ProcessAttachmentCode = _screeningProcessAttachment.ProcessAttachmentCode,
                                    ScreeningScheduleCode = _screeningProcessAttachment.ScreeningScheduleCode,
                                    ScreeningSchedule = (
                                                            from _screeningSchedule in this.db.HRM_ScreeningSchedules
                                                            where _screeningSchedule.ScreeningScheduleCode == _screeningProcessAttachment.ScreeningScheduleCode
                                                            select _screeningSchedule.FileNmbr
                                                        ).FirstOrDefault(),
                                    File = _screeningProcessAttachment.File,
                                    Remark = _screeningProcessAttachment.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_ScreeningProcessAttachment(_row.ProcessAttachmentCode, _row.ScreeningScheduleCode, _row.ScreeningSchedule, _row.File, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_ScreeningProcessAttachment GetSingleHRM_ScreeningProcessAttachment(string _prmCode)
        {
            HRM_ScreeningProcessAttachment _result = null;

            try
            {
                _result = this.db.HRM_ScreeningProcessAttachments.Single(_temp => _temp.ProcessAttachmentCode == new Guid(_prmCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiHRM_ScreeningProcessAttachment(string[] _prmCode)
        {
            bool _result = false;
            string[] _fileName = new string[_prmCode.Length];

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_ScreeningProcessAttachment _screeningProcessAttachment = this.db.HRM_ScreeningProcessAttachments.Single(_temp => _temp.ProcessAttachmentCode == new Guid(_prmCode[i]));

                    _fileName[i] = _screeningProcessAttachment.File;

                    this.db.HRM_ScreeningProcessAttachments.DeleteOnSubmit(_screeningProcessAttachment);
                }

                this.db.SubmitChanges();

                for (int i = 0; i < _prmCode.Length; i++)
                {
                    if (File.Exists(ApplicationConfig.ScreeningProcessAttachmentPath + _fileName[i]) == true)
                    {
                        File.Delete(ApplicationConfig.ScreeningProcessAttachmentPath + _fileName[i]);
                    }
                }

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String AddHRM_ScreeningProcessAttachment(HRM_ScreeningProcessAttachment _prmHRM_ScreeningProcessAttachment, FileUpload _prmFileUpload)
        {
            String _result = "";

            try
            {
                _result = this.UploadApplicantAttachmentFile(_prmHRM_ScreeningProcessAttachment.ProcessAttachmentCode.ToString(), _prmFileUpload);

                if (_result == "")
                {
                    String _path = _prmFileUpload.PostedFile.FileName;
                    FileInfo _file = new FileInfo(_path);
                    _prmHRM_ScreeningProcessAttachment.File = _prmHRM_ScreeningProcessAttachment.ProcessAttachmentCode.ToString() + _file.Extension;

                    this.db.HRM_ScreeningProcessAttachments.InsertOnSubmit(_prmHRM_ScreeningProcessAttachment);
                    this.db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditHRM_ScreeningProcessAttachment(HRM_ScreeningProcessAttachment _prmHRM_ScreeningProcessAttachment)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String UploadApplicantAttachmentFile(string _prmAttachCode, FileUpload _prmFileUpload)
        {
            String _result = "";

            String _path = _prmFileUpload.PostedFile.FileName;
            FileInfo _file = new FileInfo(_path);
            String _imagepath = ApplicationConfig.ScreeningProcessAttachmentPath + _prmAttachCode + _file.Extension;

            if (_path == "")
            {
                _result = "Invalid filename supplied";
            }
            if (_prmFileUpload.PostedFile.ContentLength == 0)
            {
                _result = "Invalid file content";
            }
            if (_result == "")
            {
                if (DocumentHandler.IsDocumentFile(_path, ApplicationConfig.AttachmentExtension) == true)
                {
                    if (_prmFileUpload.PostedFile.ContentLength <= Convert.ToDecimal(ApplicationConfig.AttachmentMaxSize))
                    {
                        if (File.Exists(ApplicationConfig.ScreeningProcessAttachmentPath + _prmAttachCode + _file.Extension) == true)
                        {
                            File.Delete(ApplicationConfig.ScreeningProcessAttachmentPath + _prmAttachCode + _file.Extension);
                        }

                        _file.CopyTo(_imagepath, true);
                        _prmFileUpload.PostedFile.SaveAs(_imagepath);

                        _file.Refresh();
                    }
                    else
                    {
                        _result = "Unable to upload, file exceeds maximum limit";
                    }
                }
            }

            return _result;
        }

        public String GetFileNameByApplicantAttachmentCode(Guid _prmCode)
        {
            String _result = "";

            try
            {
                var _query =
                        (
                            from _screenProcAttachment in this.db.HRM_ScreeningProcessAttachments
                            where _screenProcAttachment.ProcessAttachmentCode == _prmCode
                            select new
                            {
                                Remark = _screenProcAttachment.Remark
                            }
                        ).FirstOrDefault();

                _result = _query.Remark;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteAttachmentScreeningProcess(HRM_ScreeningProcessAttachment _prmHRM_ScreeningProcessAttachment)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion HRM_ScreeningProcessAttachment
        ~ScreeningProcessBL()
        {
        }
    }
}
