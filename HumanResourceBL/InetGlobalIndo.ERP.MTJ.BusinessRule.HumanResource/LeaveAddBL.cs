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
    public sealed class LeaveAddBL : Base
    {
        public LeaveAddBL()
        {

        }

        #region HRMTrLeaveAddHd
        public double RowsCountLeaveAdd(string _prmCategory, string _prmKeyword)
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
                String _type = LeaveAddDataMapper.GetTypeText(LeaveType.Add);

                var _query =
                            (
                               from _leaveAdd in this.db.HRMTrLeaveAddHds
                               where _leaveAdd.Type == _type
                                    && (SqlMethods.Like(_leaveAdd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_leaveAdd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _leaveAdd.Status != LeaveAddDataMapper.GetStatusByte(TransStatus.Deleted)
                               select _leaveAdd.TransNmbr
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrLeaveAddHd> GetListLeaveAdd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMTrLeaveAddHd> _result = new List<HRMTrLeaveAddHd>();

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
                String _type = LeaveAddDataMapper.GetTypeText(LeaveType.Add);

                var _query = (
                                from _leaveAdd in this.db.HRMTrLeaveAddHds
                                where _leaveAdd.Type == _type
                                    && (SqlMethods.Like(_leaveAdd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_leaveAdd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _leaveAdd.Status != LeaveAddDataMapper.GetStatusByte(TransStatus.Deleted)
                                orderby _leaveAdd.EditDate descending
                                select new
                                {
                                    TransNmbr = _leaveAdd.TransNmbr,
                                    FileNmbr = _leaveAdd.FileNmbr,
                                    Status = _leaveAdd.Status,
                                    TransDate = _leaveAdd.TransDate,
                                    StartEffectiveDate = _leaveAdd.StartEffectiveDate,
                                    EndEffectiveDate = _leaveAdd.EndEffectiveDate
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrLeaveAddHd(_row.TransNmbr, _row.FileNmbr, _row.Status, _row.TransDate, _row.StartEffectiveDate, _row.EndEffectiveDate));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrLeaveAddHd GetSingleLeaveAdd(string _prmCode)
        {
            HRMTrLeaveAddHd _result = null;

            try
            {
                _result = this.db.HRMTrLeaveAddHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleLeaveAddApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrLeaveAddHd _hRMTrLeaveAddHd = this.db.HRMTrLeaveAddHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrLeaveAddHd != null)
                    {
                        if (_hRMTrLeaveAddHd.Status != LeaveAddDataMapper.GetStatusByte(TransStatus.Posted))
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

        public bool DeleteMultiLeaveAdd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrLeaveAddHd _leaveAdd = this.db.HRMTrLeaveAddHds.Single(_temp => _temp.TransNmbr == _prmCode[i]);

                    if ((_leaveAdd.FileNmbr ?? "").Trim() == "")
                    {
                        var _query = (from _detail in this.db.HRMTrLeaveAddDts
                                      where _detail.TransNmbr == _prmCode[i]
                                      select _detail);

                        this.db.HRMTrLeaveAddDts.DeleteAllOnSubmit(_query);

                        this.db.HRMTrLeaveAddHds.DeleteOnSubmit(_leaveAdd);

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

        public bool DeleteMultiApproveLeaveAdd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrLeaveAddHd _hRMTrLeaveAddHd = this.db.HRMTrLeaveAddHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrLeaveAddHd.Status == LeaveAddDataMapper.GetStatusByte(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _hRMTrLeaveAddHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _hRMTrLeaveAddHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }


                    if (_hRMTrLeaveAddHd != null)
                    {
                        if ((_hRMTrLeaveAddHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.HRMTrLeaveAddDts
                                          where _detail.TransNmbr.ToLower().Trim() == _prmCode[i].ToLower().Trim()
                                          select _detail);

                            this.db.HRMTrLeaveAddDts.DeleteAllOnSubmit(_query);

                            this.db.HRMTrLeaveAddHds.DeleteOnSubmit(_hRMTrLeaveAddHd);

                            _result = true;
                        }
                        else if (_hRMTrLeaveAddHd.FileNmbr != "" && _hRMTrLeaveAddHd.Status == LeaveAddDataMapper.GetStatusByte(TransStatus.Approved))
                        {
                            _hRMTrLeaveAddHd.Status = LeaveAddDataMapper.GetStatusByte(TransStatus.Deleted);
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

        public bool EditLeaveAdd(HRMTrLeaveAddHd _prmLeaveAdd)
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

        public string AddLeaveAdd(HRMTrLeaveAddHd _prmLeaveAdd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();

                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmLeaveAdd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRMTrLeaveAddHds.InsertOnSubmit(_prmLeaveAdd);

                var _query = (
                               from _temp in this.db.Temporary_TransactionNumbers
                               where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                               select _temp
                             );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmLeaveAdd.TransNmbr.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrLeaveAddHd> GetListDDLLeaveAdd()
        {
            List<HRMTrLeaveAddHd> _result = new List<HRMTrLeaveAddHd>();

            try
            {
                String _type = LeaveAddDataMapper.GetTypeText(LeaveType.Add);

                var _query = (
                                from _leaveAddHd in this.db.HRMTrLeaveAddHds
                                where _leaveAddHd.Type == _type
                                    && _leaveAddHd.Status == LeaveAddDataMapper.GetStatusByte(TransStatus.Posted)
                                orderby _leaveAddHd.FileNmbr ascending
                                select new
                                {
                                    TransNmbr = _leaveAddHd.TransNmbr,
                                    FileNmbr = _leaveAddHd.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrLeaveAddHd(_row.TransNmbr, _row.FileNmbr));
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
                this.db.spHRM_LeaveAddGetAppr(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMLeaveAdd);
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
                    this.db.spHRM_LeaveAddApprove(_prmCode, _prmuser, ref _result);

                    if (_result == "")
                    {
                        HRMTrLeaveAddHd _leaveAdd = this.GetSingleLeaveAdd(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(Convert.ToDateTime(_leaveAdd.TransDate).Year, Convert.ToDateTime(_leaveAdd.TransDate).Month, AppModule.GetValue(TransactionType.HRMLeaveAdd), this._companyTag, ""))
                        {
                            _leaveAdd.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.HRMLeaveAdd);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = "";
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        this.db.SubmitChanges();

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
                this.db.spHRM_LeaveAddPosting(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMLeaveAdd);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = "";
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
                this.db.spHRM_LeaveAddUnPost(_prmCode, _prmuser, ref _result);

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

        #region HRMTrLeaveAddDt

        public double RowsCountLeaveAddDt(string _prmCode)
        {
            double _result = 0;

            var _query =
                         (
                            from _leaveAddDt in this.db.HRMTrLeaveAddDts
                            where _leaveAddDt.TransNmbr == _prmCode
                            select _leaveAddDt.EmployeeId
                         ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMTrLeaveAddDt> GetListLeaveAddDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRMTrLeaveAddDt> _result = new List<HRMTrLeaveAddDt>();

            try
            {
                var _query = (
                                from _leaveAddDt in this.db.HRMTrLeaveAddDts
                                where _leaveAddDt.TransNmbr == _prmCode
                                orderby _leaveAddDt.EmployeeId ascending
                                select new
                                {
                                    TransNmbr = _leaveAddDt.TransNmbr,
                                    EmployeeId = _leaveAddDt.EmployeeId,
                                    EmpName = (
                                                from _employee in this.db.MsEmployees
                                                where _employee.EmpNumb == _leaveAddDt.EmployeeId
                                                select _employee.EmpName
                                               ).FirstOrDefault(),
                                    AddLeaveDay = _leaveAddDt.AddLeaveDay,
                                    Remark = _leaveAddDt.Remark

                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrLeaveAddDt(_row.TransNmbr, _row.EmployeeId, _row.EmpName, _row.AddLeaveDay, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrLeaveAddDt GetSingleLeaveAddDt(string _prmEmpNumb, string _prmTransNmbr)
        {
            HRMTrLeaveAddDt _result = null;

            try
            {
                _result = this.db.HRMTrLeaveAddDts.Single(_temp => _temp.EmployeeId == _prmEmpNumb && _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiLeaveAddDt(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('=');

                    HRMTrLeaveAddDt _leaveAddDt = this.db.HRMTrLeaveAddDts.Single(_temp => _temp.TransNmbr == _tempSplit[0] && _temp.EmployeeId == _tempSplit[1]);

                    this.db.HRMTrLeaveAddDts.DeleteOnSubmit(_leaveAddDt);
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

        public bool AddLeaveAddDt(HRMTrLeaveAddDt _prmLeaveAddDt)
        {
            bool _result = false;

            try
            {
                this.db.HRMTrLeaveAddDts.InsertOnSubmit(_prmLeaveAddDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditLeaveAddDt(HRMTrLeaveAddDt _prmLeaveAddDt)
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

        ~LeaveAddBL()
        {

        }
    }
}
