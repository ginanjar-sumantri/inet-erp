using System;
using System.Collections.Generic;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.IO;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class ApplicantResumeBL : Base
    {
        public ApplicantResumeBL()
        {

        }

        #region ApplicantResume

        public double RowsCountApplicationResume(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "Name")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            var _query =
                        (
                           from _applicationResume in this.db.HRM_ApplicantResumes
                           where (SqlMethods.Like(_applicationResume.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                 && (SqlMethods.Like((_applicationResume.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                 && (SqlMethods.Like(_applicationResume.Name.Trim().ToLower(), _pattern3.Trim().ToLower()))
                           select _applicationResume.ApplicantResumeCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<HRM_ApplicantResume> GetListApplicantResume(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRM_ApplicantResume> _result = new List<HRM_ApplicantResume>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "Name")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            try
            {
                var _query = (
                                from _applicantResume in this.db.HRM_ApplicantResumes
                                where (SqlMethods.Like(_applicantResume.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_applicantResume.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (SqlMethods.Like(_applicantResume.Name.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                orderby _applicantResume.EditDate descending
                                select new
                                {
                                    ApplicationResumeCode = _applicantResume.ApplicantResumeCode,
                                    TransNmbr = _applicantResume.TransNmbr,
                                    FileNmbr = _applicantResume.FileNmbr,
                                    TransDate = _applicantResume.TransDate,
                                    Name = _applicantResume.Name,
                                    Status = _applicantResume.Status

                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_ApplicantResume(_row.ApplicationResumeCode, _row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Name, _row.Status));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_ApplicantResume GetSingleApplicantResume(string _prmCode)
        {
            HRM_ApplicantResume _result = null;

            try
            {
                _result = this.db.HRM_ApplicantResumes.Single(_temp => _temp.ApplicantResumeCode == new Guid(_prmCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiApplicantResume(string[] _prmCode)
        {
            bool _result = false;


            try
            {
                List<string> _fileName = new List<string>();

                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_ApplicantResume _applicantResume = this.db.HRM_ApplicantResumes.Single(_temp => _temp.ApplicantResumeCode == new Guid(_prmCode[i]));

                    if (_applicantResume != null)
                    {
                        if ((_applicantResume.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (
                                              from _detail in this.db.HRM_ApplicantAttachments
                                              where _detail.ApplicantResumeCode == new Guid(_prmCode[i])
                                              select _detail);

                            foreach (HRM_ApplicantAttachment _row in _query)
                            {
                                _fileName.Add(_row.File);
                            }

                            this.db.HRM_ApplicantAttachments.DeleteAllOnSubmit(_query);

                            this.db.HRM_ApplicantResumes.DeleteOnSubmit(_applicantResume);

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
                        if (File.Exists(ApplicationConfig.ApplicantResumeAttachmentPath + _item.Trim()) == true)
                        {
                            File.Delete(ApplicationConfig.ApplicantResumeAttachmentPath + _item.Trim());
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

        public bool EditApplicantResume(HRM_ApplicantResume _prmAppResume)
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

        public string AddApplicantResume(HRM_ApplicantResume _prmAppResume)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmGLFADevaluationHd.TransDate.Year, _prmGLFADevaluationHd.TransDate.Month, AppModule.GetValue(TransactionType.FADevaluation), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmAppResume.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRM_ApplicantResumes.InsertOnSubmit(_prmAppResume);

                var _query = (
                               from _temp in this.db.Temporary_TransactionNumbers
                               where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                               select _temp
                             );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmAppResume.ApplicantResumeCode.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String ChangeApplicantPicture(string _prmEmpID, FileUpload _prmFileUpload)
        {
            String _result = "";

            String _path = _prmFileUpload.PostedFile.FileName;
            FileInfo _file = new FileInfo(_path);
            String _imagepath = ApplicationConfig.ApplicantResumePhotoPath + _prmEmpID + _file.Extension;

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
                if (PictureHandler.IsPictureFile(_path, ApplicationConfig.ImageExtension) == true)
                {
                    System.Drawing.Image _uploadedImage = System.Drawing.Image.FromStream(_prmFileUpload.PostedFile.InputStream);

                    Decimal _width = Convert.ToDecimal(_uploadedImage.PhysicalDimension.Width);
                    Decimal _height = Convert.ToDecimal(_uploadedImage.PhysicalDimension.Height);

                    if (_width > Convert.ToDecimal(ApplicationConfig.ImageWidth) || _height > Convert.ToDecimal(ApplicationConfig.ImageHeight))
                    {
                        _result = "This image is too big - please resize it!";
                    }
                    else
                    {
                        if (_prmFileUpload.PostedFile.ContentLength <= Convert.ToDecimal(ApplicationConfig.ImageMaxSize))
                        {
                            HRM_ApplicantResume _emp = this.GetSingleApplicantResume(_prmEmpID);

                            if (File.Exists(ApplicationConfig.EmployeePhotoPath + _emp.Photo) == true)
                            {
                                File.Delete(ApplicationConfig.EmployeePhotoPath + _emp.Photo);
                            }

                            //_file.CopyTo(_imagepath, true);
                            _prmFileUpload.PostedFile.SaveAs(_imagepath);

                            _emp.Photo = _prmEmpID + _file.Extension;
                            this.db.SubmitChanges();

                            _file.Refresh();

                            _result = "File uploaded successfully";
                        }
                        else
                        {
                            _result = "Unable to upload, file exceeds maximum limit";
                        }
                    }
                }
                else
                {
                    _result = "File type not supported";
                }
            }

            return _result;
        }

        public string GetApproval(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.spHRM_ApplicantResumeGetAppr(new Guid(_prmCode), 0, 0, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.ApplicantResume);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = "";
                    _transActivity.Username = _prmuser;
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

        public string Approve(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    this.db.spHRM_ApplicantResumeApprove(new Guid(_prmCode), 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        HRM_ApplicantResume _appResume = this.GetSingleApplicantResume(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_appResume.TransDate.Year, _appResume.TransDate.Month, AppModule.GetValue(TransactionType.ApplicantResume), this._companyTag, ""))
                        {
                            _appResume.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.ApplicantResume);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = _appResume.FileNmbr;
                        _transActivity.Username = _prmuser;
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

        public List<HRM_ApplicantResume> GetListDDLApplicantResume()
        {
            List<HRM_ApplicantResume> _result = new List<HRM_ApplicantResume>();

            try
            {
                var _query = (
                                from _applicantResume in this.db.HRM_ApplicantResumes
                                where !(
                                            from _applicantScreeningResume2 in this.db.HRM_ApplicantScreening_Resumes
                                            where _applicantScreeningResume2.ApplicantResumeCode == _applicantResume.ApplicantResumeCode
                                            select _applicantScreeningResume2.ApplicantResumeCode
                                        ).Contains(_applicantResume.ApplicantResumeCode)
                                    && _applicantResume.Status == ApplicantResumeDataMapper.GetStatus(AppResumeStatus.Approved)
                                orderby _applicantResume.FileNmbr ascending
                                select new
                                {
                                    ApplicantResumeCode = _applicantResume.ApplicantResumeCode,
                                    FileNmbr = _applicantResume.FileNmbr
                                }
                            ).Union(
                                from _applicantResume in this.db.HRM_ApplicantResumes
                                join _applicantScreeningResume in this.db.HRM_ApplicantScreening_Resumes
                                    on _applicantResume.ApplicantResumeCode equals _applicantScreeningResume.ApplicantResumeCode
                                where _applicantScreeningResume.IsClose == true
                                orderby _applicantResume.FileNmbr ascending
                                select new
                                {
                                    ApplicantResumeCode = _applicantResume.ApplicantResumeCode,
                                    FileNmbr = _applicantResume.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_ApplicantResume(_row.ApplicantResumeCode, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetFileNmbrApplicantResume(Guid _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _applicantResume in this.db.HRM_ApplicantResumes
                                where _applicantResume.ApplicantResumeCode == _prmCode
                                select new
                                {
                                    FileNmbr = _applicantResume.FileNmbr
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.FileNmbr;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        #region ApplicantAttachment

        public double RowsCountApplicantAttachment(string _prmAppResumeCode)
        {
            double _result = 0;

            var _query =
             (
                from _applicantAttach in this.db.HRM_ApplicantAttachments
                where _applicantAttach.ApplicantResumeCode == new Guid(_prmAppResumeCode)
                select _applicantAttach.ApplicantAttachmentCode
             ).Count();

            _result = _query;

            return _result;
        }

        public List<HRM_ApplicantAttachment> GetListApplicantAttachment(int _prmReqPage, int _prmPageSize, string _prmAppResumeCode)
        {
            List<HRM_ApplicantAttachment> _result = new List<HRM_ApplicantAttachment>();



            try
            {
                var _query = (
                                from _applicantAttachment in this.db.HRM_ApplicantAttachments
                                where _applicantAttachment.ApplicantResumeCode == new Guid(_prmAppResumeCode)
                                orderby _applicantAttachment.EditDate descending
                                select new
                                {
                                    Remark = _applicantAttachment.Remark,
                                    ApplicantAttachmentCode = _applicantAttachment.ApplicantAttachmentCode,
                                    File = _applicantAttachment.File
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_ApplicantAttachment(_row.Remark, _row.ApplicantAttachmentCode, _row.File));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_ApplicantAttachment GetSingleApplicantAttachment(string _prmCode)
        {
            HRM_ApplicantAttachment _result = null;

            try
            {
                _result = this.db.HRM_ApplicantAttachments.Single(_temp => _temp.ApplicantAttachmentCode == new Guid(_prmCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiApplicantAttachment(string[] _prmCode)
        {
            bool _result = false;
            string[] _fileName = new string[_prmCode.Length];

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_ApplicantAttachment _applicantAttach = this.db.HRM_ApplicantAttachments.Single(_temp => _temp.ApplicantAttachmentCode == new Guid(_prmCode[i]));

                    _fileName[i] = _applicantAttach.File;

                    this.db.HRM_ApplicantAttachments.DeleteOnSubmit(_applicantAttach);
                }

                this.db.SubmitChanges();

                for (int i = 0; i < _prmCode.Length; i++)
                {
                    if (File.Exists(ApplicationConfig.ApplicantResumeAttachmentPath + _fileName[i]) == true)
                    {
                        File.Delete(ApplicationConfig.ApplicantResumeAttachmentPath + _fileName[i]);
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

        //public bool EditApplicantAttachment(HRM_ApplicantAttachment _prmAppAttachment, FileUpload _prmFileUpload)
        //{
        //    bool _result = false;

        //    try
        //    {

        //        this.ChangeApplicantAttachmentFile(_prmAppAttachment.ApplicantAttachmentCode.ToString(), _prmFileUpload);

        //        String _path = _prmFileUpload.PostedFile.FileName;
        //        FileInfo _file = new FileInfo(_path);
        //        _prmAppAttachment.File = _prmAppAttachment.ApplicantAttachmentCode.ToString() + _file.Extension;

        //        this.db.SubmitChanges();

        //        _result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public bool EditApplicantAttachment(HRM_ApplicantAttachment _prmAppAttachment)
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


        public String AddApplicantAttachment(HRM_ApplicantAttachment _prmAppAttachment, FileUpload _prmFileUpload)
        {
            String _result = "";

            try
            {
                _result = this.UploadApplicantAttachmentFile(_prmAppAttachment.ApplicantAttachmentCode.ToString(), _prmFileUpload);

                if (_result == "")
                {
                    String _path = _prmFileUpload.PostedFile.FileName;
                    FileInfo _file = new FileInfo(_path);
                    _prmAppAttachment.File = _prmAppAttachment.ApplicantAttachmentCode.ToString() + _file.Extension;

                    this.db.HRM_ApplicantAttachments.InsertOnSubmit(_prmAppAttachment);

                    this.db.SubmitChanges();
                }
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
            String _imagepath = ApplicationConfig.ApplicantResumeAttachmentPath + _prmAttachCode + _file.Extension;

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
                        if (File.Exists(ApplicationConfig.ApplicantResumeAttachmentPath + _prmAttachCode + _file.Extension) == true)
                        {
                            File.Delete(ApplicationConfig.ApplicantResumeAttachmentPath + _prmAttachCode + _file.Extension);
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

        public String GetFileNameByApplicantAttachmentCode(Guid _prmAppAttachmentCode)
        {
            String _result = "";

            try
            {
                var _query =
                        (
                            from _applicantAttachment in this.db.HRM_ApplicantAttachments
                            where _applicantAttachment.ApplicantAttachmentCode == _prmAppAttachmentCode
                            select new
                            {
                                FileName = _applicantAttachment.File
                            }
                        ).FirstOrDefault();

                _result = _query.FileName;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~ApplicantResumeBL()
        {

        }
    }
}
