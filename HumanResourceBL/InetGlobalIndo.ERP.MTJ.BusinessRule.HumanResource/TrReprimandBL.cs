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
    public sealed class TrReprimandBL : Base
    {
        public TrReprimandBL()
        {

        }

        #region HRMTrEmpReprimandHd
        public double RowsCountReprimand(string _prmCategory, string _prmKeyword)
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
                               from _reprimand in this.db.HRMTrEmpReprimandHds
                               where (SqlMethods.Like(_reprimand.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like((_reprimand.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && _reprimand.Status != EmpReprimandDataMapper.GetStatus(TransStatus.Deleted)
                               select _reprimand.TransNmbr
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrEmpReprimandHd> GetListReprimand(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMTrEmpReprimandHd> _result = new List<HRMTrEmpReprimandHd>();

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
                                from _reprimand in this.db.HRMTrEmpReprimandHds
                                where (SqlMethods.Like(_reprimand.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_reprimand.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _reprimand.Status != EmpReprimandDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _reprimand.EditDate descending
                                select new
                                {
                                    TransNmbr = _reprimand.TransNmbr,
                                    FileNmbr = _reprimand.FileNmbr,
                                    Status = _reprimand.Status,
                                    TransDate = _reprimand.TransDate,
                                    OrgUnit = _reprimand.OrgUnit,
                                    OrgUnitName = (
                                                        from _msOrgUnit in this.db.Master_OrganizationUnits
                                                        where _msOrgUnit.OrgUnit == _reprimand.OrgUnit
                                                        select _msOrgUnit.Description
                                                    ).FirstOrDefault(),
                                    Remark = _reprimand.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrEmpReprimandHd(_row.TransNmbr, _row.FileNmbr, _row.Status, _row.TransDate, _row.OrgUnit, _row.OrgUnitName, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrEmpReprimandHd GetSingleReprimand(string _prmCode)
        {
            HRMTrEmpReprimandHd _result = null;

            try
            {
                _result = this.db.HRMTrEmpReprimandHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleReprimandApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrEmpReprimandHd _hRMTrAppraisalHd = this.db.HRMTrEmpReprimandHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrAppraisalHd != null)
                    {
                        if (_hRMTrAppraisalHd.Status != EmpReprimandDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiReprimand(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrEmpReprimandHd _reprimand = this.db.HRMTrEmpReprimandHds.Single(_temp => _temp.TransNmbr == _prmCode[i]);

                    if ((_reprimand.FileNmbr ?? "").Trim() == "")
                    {
                        var _query = (from _detail in this.db.HRMTrEmpReprimandDts
                                      where _detail.TransNmbr == _prmCode[i]
                                      select _detail);

                        this.db.HRMTrEmpReprimandDts.DeleteAllOnSubmit(_query);

                        this.db.HRMTrEmpReprimandHds.DeleteOnSubmit(_reprimand);

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

        public bool DeleteMultiApproveReprimand(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrEmpReprimandHd _hRMTrEmpReprimandHd = this.db.HRMTrEmpReprimandHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrEmpReprimandHd.Status == EmpReprimandDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _hRMTrEmpReprimandHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _hRMTrEmpReprimandHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }


                    if (_hRMTrEmpReprimandHd != null)
                    {
                        if ((_hRMTrEmpReprimandHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.HRMTrEmpReprimandDts
                                          where _detail.TransNmbr.ToLower().Trim() == _prmCode[i].ToLower().Trim()
                                          select _detail);

                            this.db.HRMTrEmpReprimandDts.DeleteAllOnSubmit(_query);

                            this.db.HRMTrEmpReprimandHds.DeleteOnSubmit(_hRMTrEmpReprimandHd);

                            _result = true;
                        }
                        else if (_hRMTrEmpReprimandHd.FileNmbr != "" && _hRMTrEmpReprimandHd.Status == EmpReprimandDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _hRMTrEmpReprimandHd.Status = EmpReprimandDataMapper.GetStatus(TransStatus.Deleted);
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

        public bool EditReprimand(HRMTrEmpReprimandHd _prmReprimand)
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

        public string AddReprimand(HRMTrEmpReprimandHd _prmReprimand)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();

                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmReprimand.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRMTrEmpReprimandHds.InsertOnSubmit(_prmReprimand);

                var _query = (
                               from _temp in this.db.Temporary_TransactionNumbers
                               where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                               select _temp
                             );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmReprimand.TransNmbr.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrEmpReprimandHd> GetListDDLReprimand()
        {
            List<HRMTrEmpReprimandHd> _result = new List<HRMTrEmpReprimandHd>();

            try
            {
                var _query = (
                                from _reprimandHd in this.db.HRMTrEmpReprimandHds
                                where _reprimandHd.Status == EmpReprimandDataMapper.GetStatus(TransStatus.Posted)
                                orderby _reprimandHd.FileNmbr ascending
                                select new
                                {
                                    TransNmbr = _reprimandHd.TransNmbr,
                                    FileNmbr = _reprimandHd.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrEmpReprimandHd(_row.TransNmbr, _row.FileNmbr));
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
                this.db.spHRM_ReprimandGetAppr(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.EmpReprimand);
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
                    this.db.spHRM_ReprimandApprove(_prmCode, _prmuser, ref _result);

                    if (_result == "")
                    {
                        HRMTrEmpReprimandHd _reprimand = this.GetSingleReprimand(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(Convert.ToDateTime(_reprimand.TransDate).Year, Convert.ToDateTime(_reprimand.TransDate).Month, AppModule.GetValue(TransactionType.EmpReprimand), this._companyTag, ""))
                        {
                            _reprimand.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.EmpReprimand);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = _reprimand.FileNmbr;
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
                this.db.spHRM_ReprimandPosting(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.EmpReprimand);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = this.GetSingleReprimand(_prmCode).FileNmbr;
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
                this.db.spHRM_ReprimandUnPost(_prmCode, _prmuser, ref _result);

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

        #region HRMTrEmpReprimandDt

        public double RowsCountReprimandDt(string _prmCode)
        {
            double _result = 0;

            var _query =
                         (
                            from _reprimandDt in this.db.HRMTrEmpReprimandDts
                            where _reprimandDt.TransNmbr == _prmCode
                            select _reprimandDt.EmpNumb
                         ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMTrEmpReprimandDt> GetListReprimandDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRMTrEmpReprimandDt> _result = new List<HRMTrEmpReprimandDt>();

            try
            {
                var _query = (
                                from _reprimandDt in this.db.HRMTrEmpReprimandDts
                                where _reprimandDt.TransNmbr == _prmCode
                                orderby _reprimandDt.EmpNumb ascending
                                select new
                                {
                                    TransNmbr = _reprimandDt.TransNmbr,
                                    EmpNumb = _reprimandDt.EmpNumb,
                                    EmpName = (
                                                from _employee in this.db.MsEmployees
                                                where _employee.EmpNumb == _reprimandDt.EmpNumb
                                                select _employee.EmpName
                                               ).FirstOrDefault(),
                                    JobTitle = _reprimandDt.JobTitle,
                                    JobTitleName = (
                                                       from _jobTitle in this.db.MsJobTitles
                                                       where _jobTitle.JobTtlCode == _reprimandDt.JobTitle
                                                       select _jobTitle.JobTtlName
                                                   ).FirstOrDefault(),
                                    Reprimand = _reprimandDt.Reprimand,
                                    ReprimandName = (
                                                       from _Reprimand in this.db.HRMMsReprimands
                                                       where _Reprimand.ReprimandCode == _reprimandDt.Reprimand
                                                       select _Reprimand.ReprimandName
                                                   ).FirstOrDefault(),
                                    ReprimandNote = _reprimandDt.ReprimandNote,
                                    StartEffective = _reprimandDt.StartEffective,
                                    EndEffective = _reprimandDt.EndEffective,
                                    RefferedTo = _reprimandDt.RefferedTo,
                                    Remark = _reprimandDt.Remark

                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrEmpReprimandDt(_row.TransNmbr, _row.EmpNumb, _row.EmpName, _row.JobTitle, _row.JobTitleName, _row.Reprimand, _row.ReprimandName, _row.ReprimandNote, _row.StartEffective, _row.EndEffective, _row.RefferedTo, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrEmpReprimandDt GetSingleReprimandDt(string _prmEmpNumb, string _prmTransNmbr)
        {
            HRMTrEmpReprimandDt _result = null;

            try
            {
                _result = this.db.HRMTrEmpReprimandDts.Single(_temp => _temp.EmpNumb == _prmEmpNumb && _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiReprimandDt(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('=');

                    HRMTrEmpReprimandDt _ReprimandDt = this.db.HRMTrEmpReprimandDts.Single(_temp => _temp.TransNmbr == _tempSplit[0] && _temp.EmpNumb == _tempSplit[1]);

                    this.db.HRMTrEmpReprimandDts.DeleteOnSubmit(_ReprimandDt);
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

        public bool AddReprimandDt(HRMTrEmpReprimandDt _prmReprimandDt)
        {
            bool _result = false;

            try
            {
                this.db.HRMTrEmpReprimandDts.InsertOnSubmit(_prmReprimandDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditReprimandDt(HRMTrEmpReprimandDt _prmReprimandDt)
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

        ~TrReprimandBL()
        {

        }
    }
}
