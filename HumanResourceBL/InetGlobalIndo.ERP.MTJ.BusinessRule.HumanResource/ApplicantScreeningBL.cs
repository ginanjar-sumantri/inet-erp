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
    public sealed class ApplicantScreeningBL : Base
    {
        public ApplicantScreeningBL()
        {

        }

        #region HRM.ApplicantScreening


        public double RowsCountApplicationScreening(string _prmCategory, string _prmKeyword)
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
                           from _applicantScreening in this.db.HRM_ApplicantScreenings
                           where (SqlMethods.Like(_applicantScreening.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like((_applicantScreening.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                           select _applicantScreening.ApplicantScreeningCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<HRM_ApplicantScreening> GetListApplicantScreening(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRM_ApplicantScreening> _result = new List<HRM_ApplicantScreening>();

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
                                from _applicantScreening in this.db.HRM_ApplicantScreenings
                                where (SqlMethods.Like(_applicantScreening.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_applicantScreening.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _applicantScreening.EditDate descending
                                select new
                                {
                                    ApplicantScreeningCode = _applicantScreening.ApplicantScreeningCode,
                                    TransNmbr = _applicantScreening.TransNmbr,
                                    FileNmbr = _applicantScreening.FileNmbr,
                                    TransDate = _applicantScreening.TransDate,
                                    RecruitmentRequestFileNmbr = (
                                                                    from _recruitmentRequest in this.db.HRM_RecruitmentRequests
                                                                    where _recruitmentRequest.RecruitmentRequestCode == _applicantScreening.RecruitmentRequestCode
                                                                    select _recruitmentRequest.FileNmbr
                                                                ).FirstOrDefault(),
                                    Status = _applicantScreening.Status

                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_ApplicantScreening(_row.ApplicantScreeningCode, _row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.RecruitmentRequestFileNmbr, _row.Status));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_ApplicantScreening GetSingleApplicantScreening(string _prmCode)
        {
            HRM_ApplicantScreening _result = null;

            try
            {
                _result = this.db.HRM_ApplicantScreenings.Single(_temp => _temp.ApplicantScreeningCode == new Guid(_prmCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiApplicantScreening(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_ApplicantScreening _applicantScreening = this.db.HRM_ApplicantScreenings.Single(_temp => _temp.ApplicantScreeningCode == new Guid(_prmCode[i]));

                    if ((_applicantScreening.FileNmbr ?? "").Trim() == "")
                    {
                        var _query = (from _detail in this.db.HRM_ApplicantScreening_Resumes
                                      where _detail.ApplicantScreeningCode == new Guid(_prmCode[i])
                                      select _detail);

                        this.db.HRM_ApplicantScreening_Resumes.DeleteAllOnSubmit(_query);

                        this.db.HRM_ApplicantScreenings.DeleteOnSubmit(_applicantScreening);

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

        public bool EditApplicantScreening(HRM_ApplicantScreening _prmApplicantScreening)
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

        public string AddApplicantScreening(HRM_ApplicantScreening _prmApplicantScreening)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();

                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmApplicantScreening.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRM_ApplicantScreenings.InsertOnSubmit(_prmApplicantScreening);

                var _query = (
                               from _temp in this.db.Temporary_TransactionNumbers
                               where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                               select _temp
                             );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmApplicantScreening.ApplicantScreeningCode.ToString();
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
                this.db.spHRM_ApplicantScreeningGetAppr(new Guid(_prmCode), 0, 0, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.ApplicantScreening);
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
                    this.db.spHRM_ApplicantScreeningApprove(new Guid(_prmCode), 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        HRM_ApplicantScreening _applicantScreening = this.GetSingleApplicantScreening(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_applicantScreening.TransDate.Year, _applicantScreening.TransDate.Month, AppModule.GetValue(TransactionType.ApplicantScreening), this._companyTag, ""))
                        {
                            _applicantScreening.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.ApplicantScreening);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = _applicantScreening.FileNmbr;
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

        public string Closing(string _prmCode, string _prmRemark, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.spHRM_ApplicantScreeningClosing(new Guid(_prmCode), _prmRemark, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Closing Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Closing Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        #endregion

        #region HRM.ApplicantScreening_Resume

        public double RowsCountApplicationScreening_Resume(string _prmCode)
        {
            double _result = 0;

            var _query =
             (
                from _applicantScreening in this.db.HRM_ApplicantScreening_Resumes
                where _applicantScreening.ApplicantScreeningCode == new Guid(_prmCode)
                select _applicantScreening.ApplicantResumeScreeningCode
             ).Count();

            _result = _query;

            return _result;
        }

        public List<HRM_ApplicantScreening_Resume> GetListApplicantScreening_Resume(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRM_ApplicantScreening_Resume> _result = new List<HRM_ApplicantScreening_Resume>();

            try
            {
                var _query = (
                                from _applicantScreening_Resume in this.db.HRM_ApplicantScreening_Resumes
                                where _applicantScreening_Resume.ApplicantScreeningCode == new Guid(_prmCode)
                                orderby _applicantScreening_Resume.EditDate descending
                                select new
                                {
                                    ApplicantResumeScreeningCode = _applicantScreening_Resume.ApplicantResumeScreeningCode,
                                    ApplicantResumeCode = _applicantScreening_Resume.ApplicantResumeCode,
                                    ExpectedReturn = _applicantScreening_Resume.ExpectedReturn,
                                    Remark = _applicantScreening_Resume.Remark,
                                    IsClose = _applicantScreening_Resume.IsClose
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_ApplicantScreening_Resume(_row.ApplicantResumeScreeningCode, _row.ApplicantResumeCode, _row.ExpectedReturn, _row.Remark, _row.IsClose));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_ApplicantScreening_Resume GetSingleApplicantScreening_Resume(string _prmCode)
        {
            HRM_ApplicantScreening_Resume _result = null;

            try
            {
                _result = this.db.HRM_ApplicantScreening_Resumes.Single(_temp => _temp.ApplicantResumeScreeningCode == new Guid(_prmCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiApplicantScreening_Resume(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_ApplicantScreening_Resume _applicantScreening_Resume = this.db.HRM_ApplicantScreening_Resumes.Single(_temp => _temp.ApplicantResumeScreeningCode == new Guid(_prmCode[i]));

                    this.db.HRM_ApplicantScreening_Resumes.DeleteOnSubmit(_applicantScreening_Resume);
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

        public bool AddApplicantScreening_Resume(HRM_ApplicantScreening_Resume _prmApplicantScreening_Resume)
        {
            bool _result = false;

            try
            {
                this.db.HRM_ApplicantScreening_Resumes.InsertOnSubmit(_prmApplicantScreening_Resume);
                this.db.SubmitChanges();

                _result = true;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditApplicantScreening_Resume(HRM_ApplicantScreening_Resume _prmApplicantScreening_Resume)
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

        public List<HRM_ApplicantResume> GetListDDLApplicantScreeningResume()
        {
            List<HRM_ApplicantResume> _result = new List<HRM_ApplicantResume>();

            try
            {
                var _query = (
                                from _applicantScreeningResume in this.db.HRM_ApplicantScreening_Resumes
                                join _applicantScreening in this.db.HRM_ApplicantScreenings
                                    on _applicantScreeningResume.ApplicantScreeningCode equals _applicantScreening.ApplicantScreeningCode
                                join _applicantResume in this.db.HRM_ApplicantResumes
                                    on _applicantScreeningResume.ApplicantResumeCode equals _applicantResume.ApplicantResumeCode
                                where _applicantScreening.Status == ApplicantResumeDataMapper.GetStatus(AppResumeStatus.Approved)
                                    && _applicantScreeningResume.IsClose == false
                                orderby _applicantResume.FileNmbr ascending
                                select new
                                {
                                    ApplicantResumeCode = _applicantScreeningResume.ApplicantResumeScreeningCode,
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

        public string GetFileNmbrAppResumeByAppScreeningResume(Guid _prmAppScreeningResumeCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _hrm_ApplicantResume in this.db.HRM_ApplicantResumes
                                join _hrmAppScreeningResume in this.db.HRM_ApplicantScreening_Resumes
                                    on _hrm_ApplicantResume.ApplicantResumeCode equals _hrmAppScreeningResume.ApplicantResumeCode
                                where _hrmAppScreeningResume.ApplicantResumeScreeningCode == _prmAppScreeningResumeCode
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

        ~ApplicantScreeningBL()
        {

        }
    }
}
