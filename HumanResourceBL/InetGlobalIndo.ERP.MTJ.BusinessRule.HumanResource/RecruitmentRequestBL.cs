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
using System.Web;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class RecruitmentRequestBL : Base
    {
        public RecruitmentRequestBL()
        {

        }

        #region HRM_RecruitmentRequest
        public double RowsCountHRMRecruitmentRequest(string _prmCategory, string _prmKeyword)
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
            else if (_prmCategory == "OrgUnit")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            var _query =
                        (
                           from _recruitmentRequest in this.db.HRM_RecruitmentRequests
                           join _master_OrgUnit in this.db.Master_OrganizationUnits
                                on _recruitmentRequest.OrgUnit equals _master_OrgUnit.OrgUnit
                           where (SqlMethods.Like(_recruitmentRequest.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                              && (SqlMethods.Like((_recruitmentRequest.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                              && (SqlMethods.Like(_master_OrgUnit.Description.Trim().ToLower(), _pattern3.Trim().ToLower()))
                           select _recruitmentRequest.RecruitmentRequestCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<HRM_RecruitmentRequest> GetListHRMRecruitmentRequest(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRM_RecruitmentRequest> _result = new List<HRM_RecruitmentRequest>();

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
            else if (_prmCategory == "OrgUnit")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            try
            {
                var _query = (
                                from _recruitmentRequest in this.db.HRM_RecruitmentRequests
                                join _master_OrgUnit in this.db.Master_OrganizationUnits
                                    on _recruitmentRequest.OrgUnit equals _master_OrgUnit.OrgUnit
                                where (SqlMethods.Like(_recruitmentRequest.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                  && (SqlMethods.Like((_recruitmentRequest.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                  && (SqlMethods.Like(_master_OrgUnit.Description.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                orderby _recruitmentRequest.EditDate descending
                                select new
                                {
                                    RecruitmentRequestCode = _recruitmentRequest.RecruitmentRequestCode,
                                    TransNmbr = _recruitmentRequest.TransNmbr,
                                    FileNmbr = _recruitmentRequest.FileNmbr,
                                    StartDate = _recruitmentRequest.StartDate,
                                    Status = _recruitmentRequest.Status,
                                    CloseDate = _recruitmentRequest.CloseDate,
                                    ExpectedDate = _recruitmentRequest.ExpectedDate,
                                    Qty = _recruitmentRequest.Qty,
                                    QtyDone = _recruitmentRequest.QtyDone,
                                    OrgUnit = _recruitmentRequest.OrgUnit,
                                    OrgUnitName = _master_OrgUnit.Description,
                                    InsertBy = _recruitmentRequest.InsertBy
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_RecruitmentRequest(_row.RecruitmentRequestCode, _row.TransNmbr, _row.FileNmbr, _row.StartDate, _row.Status, _row.CloseDate, _row.ExpectedDate, _row.Qty, _row.QtyDone, _row.OrgUnit, _row.OrgUnitName, _row.InsertBy));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_RecruitmentRequest GetSingleHRMRecruitmentRequest(Guid _prmCode)
        {
            HRM_RecruitmentRequest _result = null;

            try
            {
                _result = this.db.HRM_RecruitmentRequests.Single(_temp => _temp.RecruitmentRequestCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_RecruitmentRequest GetSingleHRMRecruitmentRequestView(Guid _prmCode)
        {
            HRM_RecruitmentRequest _result = new HRM_RecruitmentRequest();

            try
            {
                var _query = (
                               from _recruitmentRequest in this.db.HRM_RecruitmentRequests
                               join _master_OrgUnit in this.db.Master_OrganizationUnits
                                    on _recruitmentRequest.OrgUnit equals _master_OrgUnit.OrgUnit
                               orderby _recruitmentRequest.EditDate descending
                               where _recruitmentRequest.RecruitmentRequestCode == _prmCode
                               select new
                               {
                                   RecruitmentRequestCode = _recruitmentRequest.RecruitmentRequestCode,
                                   TransNmbr = _recruitmentRequest.TransNmbr,
                                   FileNmbr = _recruitmentRequest.FileNmbr,
                                   StartDate = _recruitmentRequest.StartDate,
                                   Status = _recruitmentRequest.Status,
                                   CloseDate = _recruitmentRequest.CloseDate,
                                   ExpectedDate = _recruitmentRequest.ExpectedDate,
                                   Qty = _recruitmentRequest.Qty,
                                   QtyDone = _recruitmentRequest.QtyDone,
                                   OrgUnit = _recruitmentRequest.OrgUnit,
                                   OrgUnitName = _master_OrgUnit.Description
                               }
                           ).Single();

                _result.RecruitmentRequestCode = _query.RecruitmentRequestCode;
                _result.TransNmbr = _query.TransNmbr;
                _result.FileNmbr = _query.FileNmbr;
                _result.StartDate = _query.StartDate;
                _result.Status = _query.Status;
                _result.CloseDate = _query.CloseDate;
                _result.ExpectedDate = _query.ExpectedDate;
                _result.Qty = _query.Qty;
                _result.QtyDone = _query.QtyDone;
                _result.OrgUnit = _query.OrgUnit;
                _result.OrgUnitName = _query.OrgUnitName;
                //_result.TermName = _query.TermName;
                //_result.DiscForex = _query.DiscForex;
                //_result.BaseForex = _query.BaseForex;
                //_result.Attn = _query.Attn;
                //_result.Remark = _query.Remark;
                //_result.CNCustNo = _query.CNCustNo;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiHRMRecruitmentRequest(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_RecruitmentRequest _recruitmentRequest = this.db.HRM_RecruitmentRequests.Single(_temp => _temp.RecruitmentRequestCode == new Guid(_prmCode[i]));

                    if (_recruitmentRequest != null)
                    {
                        if ((_recruitmentRequest.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.HRM_RecruitmentRequest_Master_Skills
                                          where _detail.RecruitmentRequestCode == new Guid(_prmCode[i])
                                          select _detail);

                            this.db.HRM_RecruitmentRequest_Master_Skills.DeleteAllOnSubmit(_query);

                            this.db.HRM_RecruitmentRequests.DeleteOnSubmit(_recruitmentRequest);

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
                    this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string AddHRMRecruitmentRequest(HRM_RecruitmentRequest _prmHRM_RecruitmentRequest)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmHRM_RecruitmentRequest.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRM_RecruitmentRequests.InsertOnSubmit(_prmHRM_RecruitmentRequest);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmHRM_RecruitmentRequest.RecruitmentRequestCode.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditHRMRecruitmentRequest(HRM_RecruitmentRequest _prmHRM_RecruitmentRequest)
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

        public string GetAppr(Guid _prmRecruitmentRequestCode)
        {
            string _result = "";

            try
            {
                int _success = this.db.S_HRMRecruitmentRequestGetAppr(_prmRecruitmentRequestCode, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMRecruitmentRequest);
                    _transActivity.TransNmbr = _prmRecruitmentRequestCode.ToString();
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

        public string Approve(Guid _prmRecruitmentRequestCode)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    int _success = this.db.S_HRMRecruitmentRequestApprove(_prmRecruitmentRequestCode, ref _result);

                    if (_result == "")
                    {
                        HRM_RecruitmentRequest _recruitmentRequest = this.GetSingleHRMRecruitmentRequest(_prmRecruitmentRequestCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_recruitmentRequest.StartDate.Year, _recruitmentRequest.StartDate.Month, AppModule.GetValue(TransactionType.HRMRecruitmentRequest), this._companyTag, ""))
                        {
                            _recruitmentRequest.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.HRMRecruitmentRequest);
                        _transActivity.TransNmbr = _prmRecruitmentRequestCode.ToString();
                        _transActivity.FileNmbr = _recruitmentRequest.FileNmbr;
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

        public string Posting(Guid _prmRecruitmentRequestCode)
        {
            string _result = "";

            try
            {
                int _success = this.db.S_HRMRecruitmentRequestPosting(_prmRecruitmentRequestCode, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMRecruitmentRequest);
                    _transActivity.TransNmbr = _prmRecruitmentRequestCode.ToString();
                    _transActivity.FileNmbr = this.GetSingleHRMRecruitmentRequest(_prmRecruitmentRequestCode).FileNmbr;
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

        //public string Unposting(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        //{
        //    string _result = "";

        //    try
        //    {
        //        int _success = this.db.S_FNCICNUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

        //        if (_result == "")
        //        {
        //            _result = "Unposting Success";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _result = "Unposting Failed";
        //        ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
        //    }

        //    return _result;
        //}

        public List<HRM_RecruitmentRequest> GetListDDLRecruitmentRequest()
        {
            List<HRM_RecruitmentRequest> _result = new List<HRM_RecruitmentRequest>();

            try
            {
                var _query = (
                                from _recruitmentRequest in this.db.HRM_RecruitmentRequests
                                where _recruitmentRequest.Status == RecruitmentRequestDataMapper.GetStatus(RecruitmentRequestStatus.Approved)
                                orderby _recruitmentRequest.FileNmbr ascending
                                select new
                                {
                                    RecruitmentRequestCode = _recruitmentRequest.RecruitmentRequestCode,
                                    FileNmbr = _recruitmentRequest.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_RecruitmentRequest(_row.RecruitmentRequestCode, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        #region HRM_RecruitmentRequest_Master_Skill
        public double RowsCountHRMRecruitmentRequest_MasterSkill(Guid _prmCode)
        {
            double _result = 0;

            var _query =
                        (
                           from _recruitmentRequest in this.db.HRM_RecruitmentRequest_Master_Skills
                           where _recruitmentRequest.RecruitmentRequestCode == _prmCode
                           select _recruitmentRequest.RecruitmentRequestCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<HRM_RecruitmentRequest_Master_Skill> GetListHRMRecruitmentRequest_MasterSkill(int _prmReqPage, int _prmPageSize, Guid _prmCode)
        {
            List<HRM_RecruitmentRequest_Master_Skill> _result = new List<HRM_RecruitmentRequest_Master_Skill>();

            try
            {
                var _query = (
                                from _recruitmentRequest in this.db.HRM_RecruitmentRequest_Master_Skills
                                where _recruitmentRequest.RecruitmentRequestCode == _prmCode
                                orderby _recruitmentRequest.EditDate descending
                                select new
                                {
                                    RecruitmentRequestCode = _recruitmentRequest.RecruitmentRequestCode,
                                    SkillCode = _recruitmentRequest.SkillCode,
                                    SkillName = (
                                                    from _skill in this.db.Master_Skills
                                                    where _recruitmentRequest.SkillCode == _skill.SkillCode
                                                    select _skill.SkillName
                                                ).FirstOrDefault(),
                                    MinExperience = _recruitmentRequest.MinExperience
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_RecruitmentRequest_Master_Skill(_row.RecruitmentRequestCode, _row.SkillCode, _row.SkillName, _row.MinExperience));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_RecruitmentRequest_Master_Skill GetSingleHRMRecruitmentRequest_MasterSkill(Guid _prmCode, Guid _prmSkillCode)
        {
            HRM_RecruitmentRequest_Master_Skill _result = null;

            try
            {
                _result = this.db.HRM_RecruitmentRequest_Master_Skills.Single(_temp => _temp.RecruitmentRequestCode == _prmCode && _temp.SkillCode == _prmSkillCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiHRMRecruitmentRequest_MasterSkill(Guid _prmCode, string[] _prmSkillCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmSkillCode.Length; i++)
                {
                    HRM_RecruitmentRequest_Master_Skill _recruitmentRequest = this.db.HRM_RecruitmentRequest_Master_Skills.Single(_temp => _temp.SkillCode == new Guid(_prmSkillCode[i]) && _temp.RecruitmentRequestCode == _prmCode);

                    if (_recruitmentRequest != null)
                    {
                        this.db.HRM_RecruitmentRequest_Master_Skills.DeleteOnSubmit(_recruitmentRequest);
                    }
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

        public bool AddHRMRecruitmentRequest_MasterSkill(HRM_RecruitmentRequest_Master_Skill _prmHRM_RecruitmentRequest_MasterSkill)
        {
            bool _result = false;

            try
            {
                this.db.HRM_RecruitmentRequest_Master_Skills.InsertOnSubmit(_prmHRM_RecruitmentRequest_MasterSkill);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditHRMRecruitmentRequest_MasterSkill(HRM_RecruitmentRequest_Master_Skill _prmHRM_RecruitmentRequest_MasterSkill)
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

        ~RecruitmentRequestBL()
        {
        }
    }
}
