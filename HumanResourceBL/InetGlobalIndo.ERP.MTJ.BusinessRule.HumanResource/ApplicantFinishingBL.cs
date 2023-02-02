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
    public sealed class ApplicantFinishingBL : Base
    {
        public ApplicantFinishingBL()
        {

        }

        #region HRM.ApplicantFinishing


        public double RowsCountApplicationFinishing(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNmbr")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword + "%";
            }

            var _query =
                        (
                           from _applicantFinishing in this.db.HRM_ApplicantFinishings
                           where (SqlMethods.Like(_applicantFinishing.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like((_applicantFinishing.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                           select _applicantFinishing.ApplicantFinishingCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<HRM_ApplicantFinishing> GetListApplicantFinishing(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRM_ApplicantFinishing> _result = new List<HRM_ApplicantFinishing>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNmbr")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _applicantFinishing in this.db.HRM_ApplicantFinishings
                                where (SqlMethods.Like(_applicantFinishing.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_applicantFinishing.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _applicantFinishing.EditDate descending
                                select new
                                {
                                    ApplicantFinishingCode = _applicantFinishing.ApplicantFinishingCode,
                                    TransNmbr = _applicantFinishing.TransNmbr,
                                    FileNmbr = _applicantFinishing.FileNmbr,
                                    TransDate = _applicantFinishing.TransDate,
                                    RecruitmentRequestFileNmbr = (
                                                                    from _recruitmentRequest in this.db.HRM_RecruitmentRequests
                                                                    where _recruitmentRequest.RecruitmentRequestCode == _applicantFinishing.RecruitmentRequestCode
                                                                    select _recruitmentRequest.FileNmbr
                                                                ).FirstOrDefault(),
                                    Status = _applicantFinishing.Status

                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_ApplicantFinishing(_row.ApplicantFinishingCode, _row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.RecruitmentRequestFileNmbr, _row.Status));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_ApplicantFinishing GetSingleApplicantFinishing(string _prmCode)
        {
            HRM_ApplicantFinishing _result = null;

            try
            {
                _result = this.db.HRM_ApplicantFinishings.Single(_temp => _temp.ApplicantFinishingCode == new Guid(_prmCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiApplicantFinishing(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_ApplicantFinishing _applicantFinishing = this.db.HRM_ApplicantFinishings.Single(_temp => _temp.ApplicantFinishingCode == new Guid(_prmCode[i]));

                    if ((_applicantFinishing.FileNmbr ?? "").Trim() == "")
                    {
                        var _query = (from _detail in this.db.HRM_ApplicantFinishing_Resumes
                                      where _detail.ApplicantFinishingCode == new Guid(_prmCode[i])
                                      select _detail);

                        this.db.HRM_ApplicantFinishing_Resumes.DeleteAllOnSubmit(_query);

                        this.db.HRM_ApplicantFinishings.DeleteOnSubmit(_applicantFinishing);

                        _result = true;
                    }
                    else
                    {
                        _result = false;
                        break;
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

        public bool EditApplicantFinishing(HRM_ApplicantFinishing _prmApplicantFinishing)
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

        public string AddApplicantFinishing(HRM_ApplicantFinishing _prmApplicantFinishing)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();

                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmApplicantFinishing.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRM_ApplicantFinishings.InsertOnSubmit(_prmApplicantFinishing);

                var _query = (
                               from _temp in this.db.Temporary_TransactionNumbers
                               where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                               select _temp
                             );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmApplicantFinishing.ApplicantFinishingCode.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetApproval(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.spHRM_ApplicantFinishingGetAppr(new Guid(_prmCode), ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMApplicantFinishing);
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
                    this.db.spHRM_ApplicantFinishingApprove(new Guid(_prmCode), ref _result);

                    if (_result == "")
                    {
                        HRM_ApplicantFinishing _applicantFinishing = this.GetSingleApplicantFinishing(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_applicantFinishing.TransDate.Year, _applicantFinishing.TransDate.Month, AppModule.GetValue(TransactionType.HRMApplicantFinishing), this._companyTag, ""))
                        {
                            _applicantFinishing.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.HRMApplicantFinishing);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = _applicantFinishing.FileNmbr;
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

        public string Posting(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.spHRM_ApplicantFinishingPosting(new Guid(_prmCode), ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMApplicantFinishing);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = this.GetSingleApplicantFinishing(_prmCode).FileNmbr;
                    _transActivity.Username = _prmuser;
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

        #endregion

        #region HRM.ApplicantFinishing_Resume

        public double RowsCountApplicationFinishing_Resume(string _prmCode)
        {
            double _result = 0;

            var _query =
             (
                from _applicantFinishingResume in this.db.HRM_ApplicantFinishing_Resumes
                where _applicantFinishingResume.ApplicantFinishingCode == new Guid(_prmCode)
                select _applicantFinishingResume.ApplicantFinishingResumeCode
             ).Count();

            _result = _query;

            return _result;
        }

        public List<HRM_ApplicantFinishing_Resume> GetListApplicantFinishing_Resume(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRM_ApplicantFinishing_Resume> _result = new List<HRM_ApplicantFinishing_Resume>();

            try
            {
                var _query = (
                                from _applicantFinishing_Resume in this.db.HRM_ApplicantFinishing_Resumes
                                where _applicantFinishing_Resume.ApplicantFinishingCode == new Guid(_prmCode)
                                orderby _applicantFinishing_Resume.EditDate descending
                                select new
                                {
                                    ApplicantFinishingResumeCode = _applicantFinishing_Resume.ApplicantFinishingResumeCode,
                                    ApplicantFinishingCode = _applicantFinishing_Resume.ApplicantFinishingCode,
                                    ApplicantResumeScreeningCode = (
                                                                        from _appScreeningResume in this.db.HRM_ApplicantScreening_Resumes
                                                                        join _appResume in this.db.HRM_ApplicantResumes
                                                                            on _appScreeningResume.ApplicantResumeCode equals _appResume.ApplicantResumeCode
                                                                        where _appScreeningResume.ApplicantResumeScreeningCode == _applicantFinishing_Resume.ApplicantResumeScreeningCode
                                                                        select _appResume.FileNmbr
                                                                    ).FirstOrDefault(),
                                    EmpNumb = _applicantFinishing_Resume.EmpNumb,
                                    Remark = _applicantFinishing_Resume.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_ApplicantFinishing_Resume(_row.ApplicantFinishingResumeCode, _row.ApplicantFinishingCode, _row.ApplicantResumeScreeningCode, _row.EmpNumb, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_ApplicantFinishing_Resume GetSingleApplicantFinishing_Resume(string _prmCode)
        {
            HRM_ApplicantFinishing_Resume _result = null;

            try
            {
                _result = this.db.HRM_ApplicantFinishing_Resumes.Single(_temp => _temp.ApplicantFinishingResumeCode == new Guid(_prmCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiApplicantFinishing_Resume(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_ApplicantFinishing_Resume _applicantFinishing_Resume = this.db.HRM_ApplicantFinishing_Resumes.Single(_temp => _temp.ApplicantFinishingResumeCode == new Guid(_prmCode[i]));

                    this.db.HRM_ApplicantFinishing_Resumes.DeleteOnSubmit(_applicantFinishing_Resume);
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

        public bool AddApplicantFinishing_Resume(HRM_ApplicantFinishing_Resume _prmApplicantFinishing_Resume)
        {
            bool _result = false;

            try
            {
                this.db.HRM_ApplicantFinishing_Resumes.InsertOnSubmit(_prmApplicantFinishing_Resume);

                this.db.SubmitChanges();

                _result = true;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditApplicantFinishing_Resume(HRM_ApplicantFinishing_Resume _prmApplicantFinishing_Resume)
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

        //public List<HRM_ApplicantResume> GetListDDLApplicantFinishingResume()
        //{
        //    List<HRM_ApplicantResume> _result = new List<HRM_ApplicantResume>();

        //    try
        //    {
        //        var _query = (
        //                        from _applicantFinishingResume in this.db.HRM_ApplicantFinishing_Resumes
        //                        join _applicantFinishing in this.db.HRM_ApplicantFinishings
        //                            on _applicantFinishingResume.ApplicantFinishingCode equals _applicantFinishing.ApplicantFinishingCode
        //                        join _applicantResume in this.db.HRM_ApplicantResumes
        //                            on _applicantFinishingResume.ApplicantResumeCode equals _applicantResume.ApplicantResumeCode
        //                        where _applicantFinishing.Status == ApplicantResumeDataMapper.GetStatus(AppResumeStatus.Approved)
        //                        orderby _applicantResume.FileNmbr ascending
        //                        select new
        //                        {
        //                            ApplicantResumeCode = _applicantFinishingResume.ApplicantResumeFinishingCode,
        //                            FileNmbr = _applicantResume.FileNmbr
        //                        }
        //                    );

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new HRM_ApplicantResume(_row.ApplicantResumeCode, _row.FileNmbr));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public string GetFileNmbrAppResumeByAppFinishingResume(Guid _prmAppFinishingResumeCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _hrm_ApplicantResume in this.db.HRM_ApplicantResumes
                                join _hrmAppScreeningResume in this.db.HRM_ApplicantScreening_Resumes
                                    on _hrm_ApplicantResume.ApplicantResumeCode equals _hrmAppScreeningResume.ApplicantResumeCode
                                join _hrmAppFinishingResume in this.db.HRM_ApplicantFinishing_Resumes
                                    on _hrmAppScreeningResume.ApplicantResumeScreeningCode equals _hrmAppFinishingResume.ApplicantResumeScreeningCode
                                where _hrmAppFinishingResume.ApplicantFinishingResumeCode == _prmAppFinishingResumeCode
                                select new
                                {
                                    FileNmbr = _hrm_ApplicantResume.FileNmbr
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

        ~ApplicantFinishingBL()
        {

        }
    }
}
