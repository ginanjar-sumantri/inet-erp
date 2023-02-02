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
    public sealed class TerminationFinishingBL : Base
    {
        public TerminationFinishingBL()
        {

        }

        #region HRM_TerminationFinishing
        public double RowsCountHRMTerminationFinishing(string _prmCategory, string _prmKeyword)
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
                           from _terminationFinishing in this.db.HRM_TerminationFinishings
                           where (SqlMethods.Like(_terminationFinishing.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                              && (SqlMethods.Like((_terminationFinishing.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                              && _terminationFinishing.Status != TerminationFinishingDataMapper.GetStatus(TransStatus.Deleted)
                           select _terminationFinishing.TerminationRequestCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<HRM_TerminationFinishing> GetListHRMTerminationFinishing(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRM_TerminationFinishing> _result = new List<HRM_TerminationFinishing>();

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
                                from _terminationFinishing in this.db.HRM_TerminationFinishings
                                where (SqlMethods.Like(_terminationFinishing.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like((_terminationFinishing.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && _terminationFinishing.Status != TerminationFinishingDataMapper.GetStatus(TransStatus.Deleted) 
                                orderby _terminationFinishing.EditDate descending
                                select new
                                {
                                    TerminationRequestCode = _terminationFinishing.TerminationRequestCode,
                                    TransNmbr = _terminationFinishing.TransNmbr,
                                    FileNmbr = _terminationFinishing.FileNmbr,
                                    TransDate = _terminationFinishing.TransDate,
                                    Status = _terminationFinishing.Status,
                                    IsAllowCertification = _terminationFinishing.IsAllowCertification,
                                    Remark = _terminationFinishing.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_TerminationFinishing(_row.TerminationRequestCode, _row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.IsAllowCertification, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_TerminationFinishing GetSingleHRMTerminationFinishing(string _prmCode)
        {
            HRM_TerminationFinishing _result = null;

            try
            {
                _result = this.db.HRM_TerminationFinishings.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleTerminationFinishingApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_TerminationFinishing _hRMTerminationFinishingHd = this.db.HRM_TerminationFinishings.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTerminationFinishingHd != null)
                    {
                        if (_hRMTerminationFinishingHd.Status != TerminationFinishingDataMapper.GetStatus(TransStatus.Posted))
                        {
                            _result = true;
                        }
                        else
                        {
                            _result = false;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiHRMTerminationFinishing(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                List<string> _fileName = new List<string>();

                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_TerminationFinishing _terminationFinishing = this.db.HRM_TerminationFinishings.Single(_temp => _temp.TerminationRequestCode == new Guid(_prmCode[i]));

                    if (_terminationFinishing != null)
                    {
                        if ((_terminationFinishing.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (
                                              from _detail in this.db.HRM_TerminationFinishingAttachments
                                              where _detail.TransNmbr == _prmCode[i]
                                              select _detail);

                            foreach (HRM_TerminationFinishingAttachment _row in _query)
                            {
                                _fileName.Add(_row.File);
                            }

                            //var _query2 = (
                            //                  from _detail2 in this.db.HRM_TerminationReqComments
                            //                  where _detail2.TerminationFinishingCode == new Guid(_prmCode[i])
                            //                  select _detail2);

                            this.db.HRM_TerminationFinishingAttachments.DeleteAllOnSubmit(_query);
                            //this.db.HRM_TerminationReqComments.DeleteAllOnSubmit(_query2);

                            this.db.HRM_TerminationFinishings.DeleteOnSubmit(_terminationFinishing);

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
                        if (File.Exists(ApplicationConfig.TerminationFinishingAttachmentPath + _item.Trim()) == true)
                        {
                            File.Delete(ApplicationConfig.TerminationFinishingAttachmentPath + _item.Trim());
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

        public bool DeleteMultiApproveTerminationFinishing(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_TerminationFinishing _hRMTerminationFinishingHd = this.db.HRM_TerminationFinishings.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTerminationFinishingHd.Status == TerminationFinishingDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _hRMTerminationFinishingHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _hRMTerminationFinishingHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }


                    if (_hRMTerminationFinishingHd != null)
                    {
                        if ((_hRMTerminationFinishingHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.HRM_TerminationFinishingAttachments
                                          where _detail.TransNmbr.ToLower().Trim() == _prmCode[i].ToLower().Trim()
                                          select _detail);

                            this.db.HRM_TerminationFinishingAttachments.DeleteAllOnSubmit(_query);

                            this.db.HRM_TerminationFinishings.DeleteOnSubmit(_hRMTerminationFinishingHd);

                            _result = true;
                        }
                        else if (_hRMTerminationFinishingHd.FileNmbr != "" && _hRMTerminationFinishingHd.Status == TerminationFinishingDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _hRMTerminationFinishingHd.Status = TerminationFinishingDataMapper.GetStatusByte(TransStatus.Deleted);
                            _result = true;
                        }
                    }
                }

                if (_result == true)
                    this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string AddHRMTerminationFinishing(HRM_TerminationFinishing _prmHRM_TerminationFinishing)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmHRM_TerminationFinishing.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRM_TerminationFinishings.InsertOnSubmit(_prmHRM_TerminationFinishing);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmHRM_TerminationFinishing.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditHRMTerminationFinishing(HRM_TerminationFinishing _prmHRM_TerminationFinishing)
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

        public string GetApproval(String _prmTransNmbr)
        {
            string _result = "";

            try
            {
                int _success = this.db.spHRM_TerminationFinishingGetAppr(_prmTransNmbr, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMTerminationFinishing);
                    _transActivity.TransNmbr = _prmTransNmbr.ToString();
                    _transActivity.FileNmbr = "";
                    _transActivity.Username = HttpContext.Current.User.Identity.Name;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();

                    _result = "Get Approval Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Get Approval Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Approve(String _prmTransNmbr)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    int _success = this.db.spHRM_TerminationFinishingApprove(_prmTransNmbr, ref _result);

                    if (_result == "")
                    {
                        HRM_TerminationFinishing _TerminationFinishing = this.GetSingleHRMTerminationFinishing(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_TerminationFinishing.TransDate.Year, _TerminationFinishing.TransDate.Month, AppModule.GetValue(TransactionType.HRMTerminationFinishing), this._companyTag, ""))
                        {
                            _TerminationFinishing.FileNmbr = item.Number;
                        }

                        //TerminationRequestBL _terminationRequestBL = new TerminationRequestBL();

                        //if (_terminationRequestBL.Close(_TerminationFinishing.TerminationRequestCode) == "")
                        //{

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.HRMTerminationFinishing);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = _TerminationFinishing.FileNmbr;
                        _transActivity.Username = HttpContext.Current.User.Identity.Name;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        
                        this.db.SubmitChanges();

                        _scope.Complete();

                        _result = "Approve Success";
                        //}
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

        public string Posting(String _prmTransNmbr)
        {
            string _result = "";

            try
            {
                int _success = this.db.spHRM_TerminationFinishingPosting(_prmTransNmbr, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMTerminationFinishing);
                    _transActivity.TransNmbr = _prmTransNmbr.ToString();
                    _transActivity.FileNmbr = this.GetSingleHRMTerminationFinishing(_prmTransNmbr).FileNmbr;
                    _transActivity.Username = HttpContext.Current.User.Identity.Name;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Posting);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();

                    _result = "Posting Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Posting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string UnPosting(String _prmTransNmbr, String _prmUser)
        {
            string _result = "";

            try
            {
                int _success = this.db.spHRM_TerminationFinishingUnPost(_prmTransNmbr, _prmUser, ref _result);

                if (_result == "")
                {
                    _result = "UnPosting Success";
                }
            }
            catch (Exception ex)
            {
                _result = "UnPosting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        #endregion

        #region HRM_TerminationFinishingAttachment
        public double RowsCountHRM_TerminationFinishingAttachment(string _prmCode)
        {
            double _result = 0;

            var _query =
             (
                from _terminationFinishingAttachment in this.db.HRM_TerminationFinishingAttachments
                where _terminationFinishingAttachment.TransNmbr == _prmCode
                select _terminationFinishingAttachment.TerminationFinishingAttachmentCode
             ).Count();

            _result = _query;

            return _result;
        }

        public List<HRM_TerminationFinishingAttachment> GetListHRM_TerminationFinishingAttachment(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRM_TerminationFinishingAttachment> _result = new List<HRM_TerminationFinishingAttachment>();

            try
            {
                var _query = (
                                from _terminationFinishingAttachment in this.db.HRM_TerminationFinishingAttachments
                                where _terminationFinishingAttachment.TransNmbr == _prmCode
                                orderby _terminationFinishingAttachment.EditDate descending
                                select new
                                {
                                    TerminationFinishingAttachCode = _terminationFinishingAttachment.TerminationFinishingAttachmentCode,
                                    TransNmbr = _terminationFinishingAttachment.TransNmbr,
                                    File = _terminationFinishingAttachment.File,
                                    Remark = _terminationFinishingAttachment.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_TerminationFinishingAttachment(_row.TerminationFinishingAttachCode, _row.TransNmbr, _row.File, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_TerminationFinishingAttachment GetSingleHRM_TerminationFinishingAttachment(string _prmCode)
        {
            HRM_TerminationFinishingAttachment _result = null;

            try
            {
                _result = this.db.HRM_TerminationFinishingAttachments.Single(_temp => _temp.TerminationFinishingAttachmentCode == new Guid(_prmCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiHRM_TerminationFinishingAttachment(string[] _prmCode)
        {
            bool _result = false;
            string[] _fileName = new string[_prmCode.Length];

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_TerminationFinishingAttachment _terminationFinishingAttachment = this.db.HRM_TerminationFinishingAttachments.Single(_temp => _temp.TerminationFinishingAttachmentCode == new Guid(_prmCode[i]));

                    _fileName[i] = _terminationFinishingAttachment.File;

                    this.db.HRM_TerminationFinishingAttachments.DeleteOnSubmit(_terminationFinishingAttachment);
                }

                this.db.SubmitChanges();

                for (int i = 0; i < _prmCode.Length; i++)
                {
                    if (File.Exists(ApplicationConfig.TerminationFinishingAttachmentPath + _fileName[i]) == true)
                    {
                        File.Delete(ApplicationConfig.TerminationFinishingAttachmentPath + _fileName[i]);
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

        public String AddHRM_TerminationFinishingAttachment(HRM_TerminationFinishingAttachment _prmHRM_TerminationFinishingAttachment, FileUpload _prmFileUpload)
        {
            String _result = "";

            try
            {
                _result = this.UploadTerminationAttachmentFile(_prmHRM_TerminationFinishingAttachment.TerminationFinishingAttachmentCode.ToString(), _prmFileUpload);

                if (_result == "")
                {
                    String _path = _prmFileUpload.PostedFile.FileName;
                    FileInfo _file = new FileInfo(_path);
                    _prmHRM_TerminationFinishingAttachment.File = _prmHRM_TerminationFinishingAttachment.TerminationFinishingAttachmentCode.ToString() + _file.Extension;

                    this.db.HRM_TerminationFinishingAttachments.InsertOnSubmit(_prmHRM_TerminationFinishingAttachment);
                    this.db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditHRM_TerminationFinishingAttachment(HRM_TerminationFinishingAttachment _prmHRM_TerminationFinishingAttachment)
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
            String _imagepath = ApplicationConfig.TerminationFinishingAttachmentPath + _prmAttachCode + _file.Extension;

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
                        if (File.Exists(ApplicationConfig.TerminationFinishingAttachmentPath + _prmAttachCode + _file.Extension) == true)
                        {
                            File.Delete(ApplicationConfig.TerminationFinishingAttachmentPath + _prmAttachCode + _file.Extension);
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

        #endregion HRM_TerminationFinishingAttachment

        ~TerminationFinishingBL()
        {
        }
    }
}
