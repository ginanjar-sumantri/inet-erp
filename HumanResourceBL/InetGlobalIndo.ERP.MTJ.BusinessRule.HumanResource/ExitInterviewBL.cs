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
    public sealed class ExitInterviewBL : Base
    {
        public ExitInterviewBL()
        {

        }

        #region HRM_ExitInterview
        public double RowsCountHRMExitInterview(string _prmCategory, string _prmKeyword)
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
                           from _exitInterview in this.db.HRM_ExitInterviews
                           where (SqlMethods.Like(_exitInterview.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                              && (SqlMethods.Like((_exitInterview.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                           select _exitInterview.TerminationRequestCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<HRM_ExitInterview> GetListHRMExitInterview(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRM_ExitInterview> _result = new List<HRM_ExitInterview>();

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
                                from _exitInterview in this.db.HRM_ExitInterviews
                                where (SqlMethods.Like(_exitInterview.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like((_exitInterview.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _exitInterview.EditDate descending
                                select new
                                {
                                    TerminationRequestCode = _exitInterview.TerminationRequestCode,
                                    TransNmbr = _exitInterview.TransNmbr,
                                    FileNmbr = _exitInterview.FileNmbr,
                                    TransDate = _exitInterview.TransDate,
                                    Status = _exitInterview.Status,
                                    EmpNumb = _exitInterview.EmpNumb,
                                    EmpName = (
                                                    from _msEmployee in this.db.MsEmployees
                                                    where _msEmployee.EmpNumb == _exitInterview.EmpNumb
                                                    select _msEmployee.EmpName
                                                ).FirstOrDefault(),
                                    Remark = _exitInterview.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_ExitInterview(_row.TerminationRequestCode, _row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.EmpNumb, _row.EmpName, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_ExitInterview GetSingleHRMExitInterview(Guid _prmCode)
        {
            HRM_ExitInterview _result = null;

            try
            {
                _result = this.db.HRM_ExitInterviews.Single(_temp => _temp.TerminationRequestCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiHRMExitInterview(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                List<string> _fileName = new List<string>();

                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_ExitInterview _exitInterview = this.db.HRM_ExitInterviews.Single(_temp => _temp.TerminationRequestCode == new Guid(_prmCode[i]));

                    if (_exitInterview != null)
                    {
                        if ((_exitInterview.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (
                                              from _detail in this.db.HRM_ExitInterviewAttachments
                                              where _detail.TerminationRequestCode == new Guid(_prmCode[i])
                                              select _detail);

                            foreach (HRM_ExitInterviewAttachment _row in _query)
                            {
                                _fileName.Add(_row.File);
                            }

                            //var _query2 = (
                            //                  from _detail2 in this.db.HRM_TerminationReqComments
                            //                  where _detail2.ExitInterviewCode == new Guid(_prmCode[i])
                            //                  select _detail2);

                            this.db.HRM_ExitInterviewAttachments.DeleteAllOnSubmit(_query);
                            //this.db.HRM_TerminationReqComments.DeleteAllOnSubmit(_query2);

                            this.db.HRM_ExitInterviews.DeleteOnSubmit(_exitInterview);

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
                        if (File.Exists(ApplicationConfig.ExitInterviewAttachmentPath + _item.Trim()) == true)
                        {
                            File.Delete(ApplicationConfig.ExitInterviewAttachmentPath + _item.Trim());
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

        public string AddHRMExitInterview(HRM_ExitInterview _prmHRM_ExitInterview)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmHRM_ExitInterview.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRM_ExitInterviews.InsertOnSubmit(_prmHRM_ExitInterview);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmHRM_ExitInterview.TerminationRequestCode.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditHRMExitInterview(HRM_ExitInterview _prmHRM_ExitInterview)
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

        public string GetApproval(Guid _prmExitInterviewCode)
        {
            string _result = "";

            try
            {
                int _success = this.db.spHRM_ExitInterviewGetAppr(_prmExitInterviewCode, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMExitInterview);
                    _transActivity.TransNmbr = _prmExitInterviewCode.ToString();
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

        public string Approve(Guid _prmExitInterviewCode)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    int _success = this.db.spHRM_ExitInterviewApprove(_prmExitInterviewCode, ref _result);

                    if (_result == "")
                    {
                        HRM_ExitInterview _exitInterview = this.GetSingleHRMExitInterview(_prmExitInterviewCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_exitInterview.TransDate.Year, _exitInterview.TransDate.Month, AppModule.GetValue(TransactionType.HRMExitInterview), this._companyTag, ""))
                        {
                            _exitInterview.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.HRMExitInterview);
                        _transActivity.TransNmbr = _prmExitInterviewCode.ToString();
                        _transActivity.FileNmbr = _exitInterview.FileNmbr;
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

        #region HRM_ExitInterviewAttachment
        public double RowsCountHRM_ExitInterviewAttachment(string _prmCode)
        {
            double _result = 0;

            var _query =
             (
                from _exitInterviewAttachment in this.db.HRM_ExitInterviewAttachments
                where _exitInterviewAttachment.TerminationRequestCode == new Guid(_prmCode)
                select _exitInterviewAttachment.ExitInterviewAttachmentCode
             ).Count();

            _result = _query;

            return _result;
        }

        public List<HRM_ExitInterviewAttachment> GetListHRM_ExitInterviewAttachment(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRM_ExitInterviewAttachment> _result = new List<HRM_ExitInterviewAttachment>();

            try
            {
                var _query = (
                                from _exitInterviewAttachment in this.db.HRM_ExitInterviewAttachments
                                where _exitInterviewAttachment.TerminationRequestCode == new Guid(_prmCode)
                                orderby _exitInterviewAttachment.EditDate descending
                                select new
                                {
                                    ExitInterviewAttachCode = _exitInterviewAttachment.ExitInterviewAttachmentCode,
                                    TerminationRequestCode = _exitInterviewAttachment.TerminationRequestCode,
                                    File = _exitInterviewAttachment.File,
                                    Remark = _exitInterviewAttachment.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_ExitInterviewAttachment(_row.ExitInterviewAttachCode, _row.TerminationRequestCode, _row.File, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_ExitInterviewAttachment GetSingleHRM_ExitInterviewAttachment(string _prmCode)
        {
            HRM_ExitInterviewAttachment _result = null;

            try
            {
                _result = this.db.HRM_ExitInterviewAttachments.Single(_temp => _temp.ExitInterviewAttachmentCode == new Guid(_prmCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiHRM_ExitInterviewAttachment(string[] _prmCode)
        {
            bool _result = false;
            string[] _fileName = new string[_prmCode.Length];

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_ExitInterviewAttachment _exitInterviewAttachment = this.db.HRM_ExitInterviewAttachments.Single(_temp => _temp.ExitInterviewAttachmentCode == new Guid(_prmCode[i]));

                    _fileName[i] = _exitInterviewAttachment.File;

                    this.db.HRM_ExitInterviewAttachments.DeleteOnSubmit(_exitInterviewAttachment);
                }

                this.db.SubmitChanges();

                for (int i = 0; i < _prmCode.Length; i++)
                {
                    if (File.Exists(ApplicationConfig.ExitInterviewAttachmentPath + _fileName[i]) == true)
                    {
                        File.Delete(ApplicationConfig.ExitInterviewAttachmentPath + _fileName[i]);
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

        public String AddHRM_ExitInterviewAttachment(HRM_ExitInterviewAttachment _prmHRM_ExitInterviewAttachment, FileUpload _prmFileUpload)
        {
            String _result = "";

            try
            {
                _result = this.UploadTerminationAttachmentFile(_prmHRM_ExitInterviewAttachment.ExitInterviewAttachmentCode.ToString(), _prmFileUpload);

                if (_result == "")
                {
                    String _path = _prmFileUpload.PostedFile.FileName;
                    FileInfo _file = new FileInfo(_path);
                    _prmHRM_ExitInterviewAttachment.File = _prmHRM_ExitInterviewAttachment.ExitInterviewAttachmentCode.ToString() + _file.Extension;

                    this.db.HRM_ExitInterviewAttachments.InsertOnSubmit(_prmHRM_ExitInterviewAttachment);
                    this.db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditHRM_ExitInterviewAttachment(HRM_ExitInterviewAttachment _prmHRM_ExitInterviewAttachment)
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

        public String UploadTerminationAttachmentFile(string _prmAttachCode, FileUpload _prmFileUpload)
        {
            String _result = "";

            String _path = _prmFileUpload.PostedFile.FileName;
            FileInfo _file = new FileInfo(_path);
            String _imagepath = ApplicationConfig.ExitInterviewAttachmentPath + _prmAttachCode + _file.Extension;

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
                        if (File.Exists(ApplicationConfig.ExitInterviewAttachmentPath + _prmAttachCode + _file.Extension) == true)
                        {
                            File.Delete(ApplicationConfig.ExitInterviewAttachmentPath + _prmAttachCode + _file.Extension);
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

        #endregion HRM_ExitInterviewAttachment

        ~ExitInterviewBL()
        {
        }
    }
}
