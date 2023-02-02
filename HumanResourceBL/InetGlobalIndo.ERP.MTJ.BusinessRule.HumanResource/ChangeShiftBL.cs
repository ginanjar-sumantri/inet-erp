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
    public sealed class ChangeShiftBL : Base
    {
        public ChangeShiftBL()
        {

        }

        #region HRMTrChangeShiftHd
        public double RowsCountChangeShift(string _prmCategory, string _prmKeyword)
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
                               from _changeShift in this.db.HRMTrChangeShiftHds
                               where (SqlMethods.Like(_changeShift.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like((_changeShift.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && _changeShift.Status != ChangeShiftDataMapper.GetStatus(TransStatus.Deleted)
                               select _changeShift.TransNmbr
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrChangeShiftHd> GetListChangeShift(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMTrChangeShiftHd> _result = new List<HRMTrChangeShiftHd>();

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
                                from _changeShift in this.db.HRMTrChangeShiftHds
                                where (SqlMethods.Like(_changeShift.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_changeShift.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _changeShift.Status != ChangeShiftDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _changeShift.EditDate descending
                                select new
                                {
                                    TransNmbr = _changeShift.TransNmbr,
                                    FileNmbr = _changeShift.FileNmbr,
                                    Status = _changeShift.Status,
                                    TransDate = _changeShift.TransDate,
                                    ChangeType = _changeShift.ChangeType,
                                    Remark = _changeShift.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrChangeShiftHd(_row.TransNmbr, _row.FileNmbr, _row.Status, _row.TransDate, _row.ChangeType, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrChangeShiftHd GetSingleChangeShift(string _prmCode)
        {
            HRMTrChangeShiftHd _result = null;

            try
            {
                _result = this.db.HRMTrChangeShiftHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleHRMTrChangeShiftHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrChangeShiftHd _hRMTrChangeShiftHd = this.db.HRMTrChangeShiftHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrChangeShiftHd != null)
                    {
                        if (_hRMTrChangeShiftHd.Status != ChangeShiftDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiChangeShift(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrChangeShiftHd _changeShift = this.db.HRMTrChangeShiftHds.Single(_temp => _temp.TransNmbr == _prmCode[i]);

                    if ((_changeShift.FileNmbr ?? "").Trim() == "")
                    {
                        var _query = (from _detail in this.db.HRMTrChangeShiftDts
                                      where _detail.TransNmbr == _prmCode[i]
                                      select _detail);

                        this.db.HRMTrChangeShiftDts.DeleteAllOnSubmit(_query);

                        this.db.HRMTrChangeShiftHds.DeleteOnSubmit(_changeShift);

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

        public bool DeleteMultiApproveHRMTrChangeShiftHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrChangeShiftHd _hRMTrChangeShiftHd = this.db.HRMTrChangeShiftHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrChangeShiftHd.Status == ChangeShiftDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _hRMTrChangeShiftHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _hRMTrChangeShiftHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }


                    if (_hRMTrChangeShiftHd != null)
                    {
                        if ((_hRMTrChangeShiftHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.HRMTrChangeShiftDts
                                          where _detail.TransNmbr.ToLower().Trim() == _prmCode[i].ToLower().Trim()
                                          select _detail);

                            this.db.HRMTrChangeShiftDts.DeleteAllOnSubmit(_query);

                            this.db.HRMTrChangeShiftHds.DeleteOnSubmit(_hRMTrChangeShiftHd);

                            _result = true;
                        }
                        else if (_hRMTrChangeShiftHd.FileNmbr != "" && _hRMTrChangeShiftHd.Status == ChangeShiftDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _hRMTrChangeShiftHd.Status = ChangeShiftDataMapper.GetStatus(TransStatus.Deleted);
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

        public bool EditChangeShift(HRMTrChangeShiftHd _prmChangeShift)
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

        public string AddChangeShift(HRMTrChangeShiftHd _prmChangeShift)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();

                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmChangeShift.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRMTrChangeShiftHds.InsertOnSubmit(_prmChangeShift);

                var _query = (
                               from _temp in this.db.Temporary_TransactionNumbers
                               where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                               select _temp
                             );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmChangeShift.TransNmbr.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrChangeShiftHd> GetListDDLChangeShift()
        {
            List<HRMTrChangeShiftHd> _result = new List<HRMTrChangeShiftHd>();

            try
            {
                var _query = (
                                from _changeShiftHd in this.db.HRMTrChangeShiftHds
                                where _changeShiftHd.Status == ChangeShiftDataMapper.GetStatus(TransStatus.Posted)
                                orderby _changeShiftHd.FileNmbr ascending
                                select new
                                {
                                    TransNmbr = _changeShiftHd.TransNmbr,
                                    FileNmbr = _changeShiftHd.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrChangeShiftHd(_row.TransNmbr, _row.FileNmbr));
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
                this.db.spHRM_ChangeShiftGetAppr(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMChangeShift);
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
                    this.db.spHRM_ChangeShiftApprove(_prmCode, _prmuser, ref _result);

                    if (_result == "")
                    {
                        HRMTrChangeShiftHd _ChangeShift = this.GetSingleChangeShift(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_ChangeShift.TransDate.Year, _ChangeShift.TransDate.Month, AppModule.GetValue(TransactionType.HRMChangeShift), this._companyTag, ""))
                        {
                            _ChangeShift.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.HRMChangeShift);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = _ChangeShift.FileNmbr;
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
                this.db.spHRM_ChangeShiftPosting(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMChangeShift);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = this.GetSingleChangeShift(_prmCode).FileNmbr;
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
                this.db.spHRM_ChangeShiftUnPost(_prmCode, _prmuser, ref _result);

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

        #region HRMTrChangeShiftDt
        public double RowsCountChangeShiftDt(string _prmCode)
        {
            double _result = 0;

            var _query = (
                            from _changeShiftDt in this.db.HRMTrChangeShiftDts
                            where _changeShiftDt.TransNmbr == _prmCode
                            select _changeShiftDt.FromEmpNumb
                         ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMTrChangeShiftDt> GetListChangeShiftDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRMTrChangeShiftDt> _result = new List<HRMTrChangeShiftDt>();

            try
            {
                var _query = (
                                from _changeShiftDt in this.db.HRMTrChangeShiftDts
                                where _changeShiftDt.TransNmbr == _prmCode
                                orderby _changeShiftDt.FromEmpNumb ascending
                                select new
                                {
                                    TransNmbr = _changeShiftDt.TransNmbr,
                                    FromEmpNumb = _changeShiftDt.FromEmpNumb,
                                    FromEmpName = (
                                                    from _msEmployeeFrom in this.db.MsEmployees
                                                    where _msEmployeeFrom.EmpNumb == _changeShiftDt.FromEmpNumb
                                                    select _msEmployeeFrom.EmpName
                                                  ).FirstOrDefault(),
                                    FromDate = _changeShiftDt.FromDate,
                                    FromShift = _changeShiftDt.FromShift,
                                    FromShiftName = (
                                                from _msShiftFrom in this.db.HRMMsShifts
                                                where _msShiftFrom.ShiftCode == _changeShiftDt.FromShift
                                                select _msShiftFrom.ShiftName
                                              ).FirstOrDefault(),
                                    ToEmpNumb = _changeShiftDt.ToEmpNumb,
                                    ToEmpName = (
                                                    from _msEmployeeTo in this.db.MsEmployees
                                                    where _msEmployeeTo.EmpNumb == _changeShiftDt.ToEmpNumb
                                                    select _msEmployeeTo.EmpName
                                                  ).FirstOrDefault(),
                                    ToDate = _changeShiftDt.ToDate,
                                    ToShift = _changeShiftDt.ToShift,
                                    ToShiftName = (
                                                    from _msShiftTo in this.db.HRMMsShifts
                                                    where _msShiftTo.ShiftCode == _changeShiftDt.ToShift
                                                    select _msShiftTo.ShiftName
                                                  ).FirstOrDefault(),
                                    Remark = _changeShiftDt.Remark,
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrChangeShiftDt(_row.TransNmbr, _row.FromEmpNumb, _row.FromEmpName, _row.FromDate, _row.FromShift, _row.FromShiftName, _row.ToEmpNumb, _row.ToEmpName, _row.ToDate, _row.ToShift, _row.ToShiftName, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrChangeShiftDt GetSingleChangeShiftDt(string _prmEmpNumb, string _prmTransNmbr)
        {
            HRMTrChangeShiftDt _result = null;

            try
            {
                _result = this.db.HRMTrChangeShiftDts.Single(_temp => _temp.FromEmpNumb == _prmEmpNumb && _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiChangeShiftDt(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('=');

                    HRMTrChangeShiftDt _ChangeShiftDt = this.db.HRMTrChangeShiftDts.Single(_temp => _temp.TransNmbr == _tempSplit[0] && _temp.FromEmpNumb == _tempSplit[1]);

                    this.db.HRMTrChangeShiftDts.DeleteOnSubmit(_ChangeShiftDt);
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

        public bool AddChangeShiftDt(HRMTrChangeShiftDt _prmChangeShiftDt)
        {
            bool _result = false;

            try
            {
                this.db.HRMTrChangeShiftDts.InsertOnSubmit(_prmChangeShiftDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditChangeShiftDt(HRMTrChangeShiftDt _prmChangeShiftDt)
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

        ~ChangeShiftBL()
        {

        }
    }
}
