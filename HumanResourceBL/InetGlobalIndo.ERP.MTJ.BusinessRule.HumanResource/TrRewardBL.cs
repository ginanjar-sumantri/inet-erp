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
    public sealed class TrRewardBL : Base
    {
        public TrRewardBL()
        {

        }

        #region HRMTrEmpRewardHd
        public double RowsCountReward(string _prmCategory, string _prmKeyword)
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

            try
            {
                var _query =
                            (
                               from _reward in this.db.HRMTrEmpRewardHds
                               where (SqlMethods.Like(_reward.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like((_reward.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && _reward.Status != EmpRewardDataMapper.GetStatus(TransStatus.Deleted)
                               select _reward.TransNmbr
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrEmpRewardHd> GetListReward(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMTrEmpRewardHd> _result = new List<HRMTrEmpRewardHd>();

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
                                from _reward in this.db.HRMTrEmpRewardHds
                                where (SqlMethods.Like(_reward.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_reward.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _reward.Status != EmpRewardDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _reward.EditDate descending
                                select new
                                {
                                    TransNmbr = _reward.TransNmbr,
                                    FileNmbr = _reward.FileNmbr,
                                    Status = _reward.Status,
                                    TransDate = _reward.TransDate,
                                    OrgUnit = _reward.OrgUnit,
                                    OrgUnitName = (
                                                        from _msOrgUnit in this.db.Master_OrganizationUnits
                                                        where _msOrgUnit.OrgUnit == _reward.OrgUnit
                                                        select _msOrgUnit.Description
                                                    ).FirstOrDefault(),
                                    Remark = _reward.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrEmpRewardHd(_row.TransNmbr, _row.FileNmbr, _row.Status, _row.TransDate, _row.OrgUnit, _row.OrgUnitName, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrEmpRewardHd GetSingleReward(string _prmCode)
        {
            HRMTrEmpRewardHd _result = null;

            try
            {
                _result = this.db.HRMTrEmpRewardHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleRewardApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrEmpRewardHd _hRMTrEmpRewardHd = this.db.HRMTrEmpRewardHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrEmpRewardHd != null)
                    {
                        if (_hRMTrEmpRewardHd.Status != EmpRewardDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiReward(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrEmpRewardHd _reward = this.db.HRMTrEmpRewardHds.Single(_temp => _temp.TransNmbr == _prmCode[i]);

                    if ((_reward.FileNmbr ?? "").Trim() == "")
                    {
                        var _query = (from _detail in this.db.HRMTrEmpRewardDts
                                      where _detail.TransNmbr == _prmCode[i]
                                      select _detail);

                        this.db.HRMTrEmpRewardDts.DeleteAllOnSubmit(_query);

                        this.db.HRMTrEmpRewardHds.DeleteOnSubmit(_reward);

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

        public bool DeleteMultiApproveReward(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrEmpRewardHd _hRMTrEmpRewardHd = this.db.HRMTrEmpRewardHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrEmpRewardHd.Status == EmpRewardDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _hRMTrEmpRewardHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _hRMTrEmpRewardHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }


                    if (_hRMTrEmpRewardHd != null)
                    {
                        if ((_hRMTrEmpRewardHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.HRMTrEmpRewardDts
                                          where _detail.TransNmbr.ToLower().Trim() == _prmCode[i].ToLower().Trim()
                                          select _detail);

                            this.db.HRMTrEmpRewardDts.DeleteAllOnSubmit(_query);

                            this.db.HRMTrEmpRewardHds.DeleteOnSubmit(_hRMTrEmpRewardHd);

                            _result = true;
                        }
                        else if (_hRMTrEmpRewardHd.FileNmbr != "" && _hRMTrEmpRewardHd.Status == EmpRewardDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _hRMTrEmpRewardHd.Status = EmpRewardDataMapper.GetStatus(TransStatus.Deleted);
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

        public bool EditReward(HRMTrEmpRewardHd _prmReward)
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

        public string AddReward(HRMTrEmpRewardHd _prmReward)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();

                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmReward.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRMTrEmpRewardHds.InsertOnSubmit(_prmReward);

                var _query = (
                               from _temp in this.db.Temporary_TransactionNumbers
                               where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                               select _temp
                             );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmReward.TransNmbr.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrEmpRewardHd> GetListDDLReward()
        {
            List<HRMTrEmpRewardHd> _result = new List<HRMTrEmpRewardHd>();

            try
            {
                var _query = (
                                from _rewardHd in this.db.HRMTrEmpRewardHds
                                where _rewardHd.Status == EmpRewardDataMapper.GetStatus(TransStatus.Posted)
                                orderby _rewardHd.FileNmbr ascending
                                select new
                                {
                                    TransNmbr = _rewardHd.TransNmbr,
                                    FileNmbr = _rewardHd.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrEmpRewardHd(_row.TransNmbr, _row.FileNmbr));
                }
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
                this.db.spHRM_RewardGetAppr(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.EmpReward);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = "";
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();
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
                    this.db.spHRM_RewardApprove(_prmCode, _prmuser, ref _result);

                    if (_result == "")
                    {
                        HRMTrEmpRewardHd _reward = this.GetSingleReward(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(Convert.ToDateTime(_reward.TransDate).Year, Convert.ToDateTime(_reward.TransDate).Month, AppModule.GetValue(TransactionType.EmpReward), this._companyTag, ""))
                        {
                            _reward.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.EmpReward);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = _reward.FileNmbr;
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
                this.db.spHRM_RewardPosting(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.EmpReward);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = this.GetSingleReward(_prmCode).FileNmbr;
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

        public string UnPosting(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.spHRM_RewardUnPost(_prmCode, _prmuser, ref _result);

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

        #region HRMTrEmpRewardDt

        public double RowsCountRewardDt(string _prmCode)
        {
            double _result = 0;

            var _query =
                         (
                            from _rewardDt in this.db.HRMTrEmpRewardDts
                            where _rewardDt.TransNmbr == _prmCode
                            select _rewardDt.EmpNumb
                         ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMTrEmpRewardDt> GetListRewardDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRMTrEmpRewardDt> _result = new List<HRMTrEmpRewardDt>();

            try
            {
                var _query = (
                                from _rewardDt in this.db.HRMTrEmpRewardDts
                                where _rewardDt.TransNmbr == _prmCode
                                orderby _rewardDt.EmpNumb ascending
                                select new
                                {
                                    TransNmbr = _rewardDt.TransNmbr,
                                    EmpNumb = _rewardDt.EmpNumb,
                                    EmpName = (
                                                from _employee in this.db.MsEmployees
                                                where _employee.EmpNumb == _rewardDt.EmpNumb
                                                select _employee.EmpName
                                               ).FirstOrDefault(),
                                    JobTitle = _rewardDt.JobTitle,
                                    JobTitleName = (
                                                       from _jobTitle in this.db.MsJobTitles
                                                       where _jobTitle.JobTtlCode == _rewardDt.JobTitle
                                                       select _jobTitle.JobTtlName
                                                   ).FirstOrDefault(),
                                    Reward = _rewardDt.Reward,
                                    RewardName = (
                                                       from _reward in this.db.HRMMsRewards
                                                       where _reward.RewardCode == _rewardDt.Reward
                                                       select _reward.RewardName
                                                   ).FirstOrDefault(),
                                    RewardNote = _rewardDt.RewardNote,
                                    Remark = _rewardDt.Remark

                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrEmpRewardDt(_row.TransNmbr, _row.EmpNumb, _row.EmpName, _row.JobTitle, _row.JobTitleName, _row.Reward, _row.RewardName, _row.RewardNote, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrEmpRewardDt GetSingleRewardDt(string _prmEmpNumb, string _prmTransNmbr)
        {
            HRMTrEmpRewardDt _result = null;

            try
            {
                _result = this.db.HRMTrEmpRewardDts.Single(_temp => _temp.EmpNumb == _prmEmpNumb && _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiRewardDt(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('=');

                    HRMTrEmpRewardDt _RewardDt = this.db.HRMTrEmpRewardDts.Single(_temp => _temp.TransNmbr == _tempSplit[0] && _temp.EmpNumb == _tempSplit[1]);

                    this.db.HRMTrEmpRewardDts.DeleteOnSubmit(_RewardDt);
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

        public bool AddRewardDt(HRMTrEmpRewardDt _prmRewardDt)
        {
            bool _result = false;

            try
            {
                this.db.HRMTrEmpRewardDts.InsertOnSubmit(_prmRewardDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditRewardDt(HRMTrEmpRewardDt _prmRewardDt)
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

        ~TrRewardBL()
        {

        }
    }
}
