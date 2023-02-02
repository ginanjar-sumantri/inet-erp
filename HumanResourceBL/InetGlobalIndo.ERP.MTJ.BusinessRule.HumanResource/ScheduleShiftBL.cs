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
    public sealed class ScheduleShiftBL : Base
    {
        public ScheduleShiftBL()
        {

        }

        #region HRMTrScheduleShiftHd
        public double RowsCountScheduleShift(string _prmCategory, string _prmKeyword)
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
                               from _scheduleShift in this.db.HRMTrScheduleShiftHds
                               where (SqlMethods.Like(_scheduleShift.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like((_scheduleShift.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && _scheduleShift.Status != ScheduleShiftDataMapper.GetStatus(TransStatus.Deleted)
                               select _scheduleShift.TransNmbr
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrScheduleShiftHd> GetListScheduleShift(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMTrScheduleShiftHd> _result = new List<HRMTrScheduleShiftHd>();

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
                                from _scheduleShift in this.db.HRMTrScheduleShiftHds
                                where (SqlMethods.Like(_scheduleShift.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_scheduleShift.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _scheduleShift.Status != ScheduleShiftDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _scheduleShift.EditDate descending
                                select new
                                {
                                    TransNmbr = _scheduleShift.TransNmbr,
                                    FileNmbr = _scheduleShift.FileNmbr,
                                    Status = _scheduleShift.Status,
                                    TransDate = _scheduleShift.TransDate,
                                    PeriodCode = _scheduleShift.PeriodCode,
                                    StartDate = _scheduleShift.StartDate,
                                    EndDate = _scheduleShift.EndDate,
                                    Remark = _scheduleShift.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrScheduleShiftHd(_row.TransNmbr, _row.FileNmbr, _row.Status, _row.TransDate, _row.PeriodCode, _row.StartDate, _row.EndDate, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrScheduleShiftHd GetSingleScheduleShift(string _prmCode)
        {
            HRMTrScheduleShiftHd _result = null;

            try
            {
                _result = this.db.HRMTrScheduleShiftHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleScheduleShiftApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrScheduleShiftHd _hRMTrScheduleShiftHd = this.db.HRMTrScheduleShiftHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrScheduleShiftHd != null)
                    {
                        if (_hRMTrScheduleShiftHd.Status != ScheduleShiftDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiScheduleShift(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrScheduleShiftHd _ScheduleShift = this.db.HRMTrScheduleShiftHds.Single(_temp => _temp.TransNmbr == _prmCode[i]);

                    if ((_ScheduleShift.FileNmbr ?? "").Trim() == "")
                    {
                        var _query = (from _detail in this.db.HRMTrScheduleShiftDts
                                      where _detail.TransNmbr == _prmCode[i]
                                      select _detail);

                        this.db.HRMTrScheduleShiftDts.DeleteAllOnSubmit(_query);

                        this.db.HRMTrScheduleShiftHds.DeleteOnSubmit(_ScheduleShift);

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

        public bool DeleteMultiApproveScheduleShift(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrScheduleShiftHd _hRMTrScheduleShiftHd = this.db.HRMTrScheduleShiftHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrScheduleShiftHd.Status == ScheduleShiftDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _hRMTrScheduleShiftHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _hRMTrScheduleShiftHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }


                    if (_hRMTrScheduleShiftHd != null)
                    {
                        if ((_hRMTrScheduleShiftHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.HRMTrScheduleShiftDts
                                          where _detail.TransNmbr.ToLower().Trim() == _prmCode[i].ToLower().Trim()
                                          select _detail);

                            this.db.HRMTrScheduleShiftDts.DeleteAllOnSubmit(_query);

                            this.db.HRMTrScheduleShiftHds.DeleteOnSubmit(_hRMTrScheduleShiftHd);

                            _result = true;
                        }
                        else if (_hRMTrScheduleShiftHd.FileNmbr != "" && _hRMTrScheduleShiftHd.Status == ScheduleShiftDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _hRMTrScheduleShiftHd.Status = ScheduleShiftDataMapper.GetStatus(TransStatus.Deleted);
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

        public bool EditScheduleShift(HRMTrScheduleShiftHd _prmScheduleShift)
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

        public string AddScheduleShift(HRMTrScheduleShiftHd _prmScheduleShift)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();

                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmScheduleShift.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRMTrScheduleShiftHds.InsertOnSubmit(_prmScheduleShift);

                var _query = (
                               from _temp in this.db.Temporary_TransactionNumbers
                               where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                               select _temp
                             );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmScheduleShift.TransNmbr.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrScheduleShiftHd> GetListDDLScheduleShift()
        {
            List<HRMTrScheduleShiftHd> _result = new List<HRMTrScheduleShiftHd>();

            try
            {
                var _query = (
                                from _scheduleShiftHd in this.db.HRMTrScheduleShiftHds
                                where _scheduleShiftHd.Status == ScheduleShiftDataMapper.GetStatus(TransStatus.Posted)
                                orderby _scheduleShiftHd.FileNmbr ascending
                                select new
                                {
                                    TransNmbr = _scheduleShiftHd.TransNmbr,
                                    FileNmbr = _scheduleShiftHd.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrScheduleShiftHd(_row.TransNmbr, _row.FileNmbr));
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
                this.db.spHRM_ScheduleShiftGetAppr(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.ScheduleShift);
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
                    this.db.spHRM_ScheduleShiftApprove(_prmCode, _prmuser, ref _result);

                    if (_result == "")
                    {
                        HRMTrScheduleShiftHd _scheduleShift = this.GetSingleScheduleShift(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_scheduleShift.TransDate.Year, _scheduleShift.TransDate.Month, AppModule.GetValue(TransactionType.ScheduleShift), this._companyTag, ""))
                        {
                            _scheduleShift.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.ScheduleShift);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = _scheduleShift.FileNmbr;
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
                this.db.spHRM_ScheduleShiftPosting(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.ScheduleShift);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = this.GetSingleScheduleShift(_prmCode).FileNmbr;
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
                this.db.spHRM_ScheduleShiftUnPost(_prmCode, _prmuser, ref _result);

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

        #region HRMTrScheduleShiftDt
        public double RowsCountScheduleShiftDt(string _prmCode)
        {
            double _result = 0;

            var _query = (
                            from _scheduleShiftDt in this.db.HRMTrScheduleShiftDts
                            where _scheduleShiftDt.TransNmbr == _prmCode
                            select _scheduleShiftDt.EmpGroupCode
                         ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMTrScheduleShiftDt> GetListScheduleShiftDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRMTrScheduleShiftDt> _result = new List<HRMTrScheduleShiftDt>();

            try
            {
                var _query = (
                                from _scheduleShiftDt in this.db.HRMTrScheduleShiftDts
                                where _scheduleShiftDt.TransNmbr == _prmCode
                                orderby _scheduleShiftDt.EmpGroupCode ascending
                                select new
                                {
                                    TransNmbr = _scheduleShiftDt.TransNmbr,
                                    EmpGroupCode = _scheduleShiftDt.EmpGroupCode,
                                    EmpGroupName = (
                                                        from _msEmpGroup in this.db.HRMMsEmpGroups
                                                        where _msEmpGroup.EmpGroupCode == _scheduleShiftDt.EmpGroupCode
                                                        select _msEmpGroup.EmpGroupName
                                                      ).FirstOrDefault(),
                                    EffectiveDate = _scheduleShiftDt.EffectiveDate,
                                    ShiftCode = _scheduleShiftDt.ShiftCode,
                                    ShiftName = (
                                                    from _msShift in this.db.HRMMsShifts
                                                    where _msShift.ShiftCode == _scheduleShiftDt.ShiftCode
                                                    select _msShift.ShiftName
                                                  ).FirstOrDefault(),
                                    NoDay = _scheduleShiftDt.NoDay
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrScheduleShiftDt(_row.TransNmbr, _row.EmpGroupCode, _row.EmpGroupName, _row.EffectiveDate, _row.ShiftCode, _row.ShiftName, _row.NoDay));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrScheduleShiftDt> GetListScheduleShiftDt(string _prmCode)
        {
            List<HRMTrScheduleShiftDt> _result = new List<HRMTrScheduleShiftDt>();

            try
            {
                var _query = (
                                from _scheduleShiftDt in this.db.HRMTrScheduleShiftDts
                                where _scheduleShiftDt.TransNmbr == _prmCode
                                orderby _scheduleShiftDt.EmpGroupCode ascending
                                select new
                                {
                                    TransNmbr = _scheduleShiftDt.TransNmbr,
                                    EmpGroupCode = _scheduleShiftDt.EmpGroupCode,
                                    EmpGroupName = (
                                                        from _msEmpGroup in this.db.HRMMsEmpGroups
                                                        where _msEmpGroup.EmpGroupCode == _scheduleShiftDt.EmpGroupCode
                                                        select _msEmpGroup.EmpGroupName
                                                      ).FirstOrDefault(),
                                    EffectiveDate = _scheduleShiftDt.EffectiveDate,
                                    ShiftCode = _scheduleShiftDt.ShiftCode,
                                    ShiftName = (
                                                    from _msShift in this.db.HRMMsShifts
                                                    where _msShift.ShiftCode == _scheduleShiftDt.ShiftCode
                                                    select _msShift.ShiftName
                                                  ).FirstOrDefault(),
                                    NoDay = _scheduleShiftDt.NoDay
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrScheduleShiftDt(_row.TransNmbr, _row.EmpGroupCode, _row.EmpGroupName, _row.EffectiveDate, _row.ShiftCode, _row.ShiftName, _row.NoDay));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrScheduleShiftDt GetSingleScheduleShiftDt(String _prmEmpGroupCode, DateTime _prmEffectiveDate, String _prmTransNmbr, String _prmShiftCode)
        {
            HRMTrScheduleShiftDt _result = null;

            try
            {
                _result = this.db.HRMTrScheduleShiftDts.Single(_temp => _temp.TransNmbr == _prmTransNmbr && _temp.EmpGroupCode == _prmEmpGroupCode && _temp.EffectiveDate == _prmEffectiveDate && _temp.ShiftCode == _prmShiftCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int RowsCountScheduleShiftDtWithEmpGroup(string _prmEmpGroupCode, string _prmTransNmbr)
        {
            int _result = 0;

            try
            {
                _result = this.db.HRMTrScheduleShiftDts.Where(_temp => _temp.EmpGroupCode == _prmEmpGroupCode && _temp.TransNmbr == _prmTransNmbr).Count();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrScheduleShiftDt GetLastDateScheduleShiftDt(string _prmEmpGroupCode, string _prmTransNmbr)
        {
            HRMTrScheduleShiftDt _result = null;

            try
            {
                DateTime _query = (
                                from _hrmTrScheduleShift in this.db.HRMTrScheduleShiftDts
                                where _hrmTrScheduleShift.TransNmbr == _prmTransNmbr && _hrmTrScheduleShift.EmpGroupCode == _prmEmpGroupCode
                                select _hrmTrScheduleShift.EffectiveDate
                             ).Max();

                _result = this.db.HRMTrScheduleShiftDts.Single(_temp => _temp.EffectiveDate == _query && _temp.TransNmbr == _prmTransNmbr && _temp.EmpGroupCode == _prmEmpGroupCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiScheduleShiftDt(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('=');

                    HRMTrScheduleShiftDt _ScheduleShiftDt = this.db.HRMTrScheduleShiftDts.Single(_temp => _temp.TransNmbr == _tempSplit[0] && _temp.EmpGroupCode == _tempSplit[1] && _temp.EffectiveDate == DateFormMapper.GetValue(_tempSplit[2]));

                    this.db.HRMTrScheduleShiftDts.DeleteOnSubmit(_ScheduleShiftDt);
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

        public bool AddScheduleShiftDt(String _prmTransNmbr, String _prmEmpGroup, String _prmDetilTrans)
        {
            bool _result = false;

            try
            {
                String[] _detailTransaksi = _prmDetilTrans.Split('^');
                foreach (String _dataDetil in _detailTransaksi)
                {
                    String[] _rowData = _dataDetil.Split('|');
                    HRMTrScheduleShiftDt _hrmTrScheduleShiftDt = new HRMTrScheduleShiftDt();

                    _hrmTrScheduleShiftDt.TransNmbr = _prmTransNmbr;
                    _hrmTrScheduleShiftDt.EmpGroupCode = _prmEmpGroup;
                    _hrmTrScheduleShiftDt.EffectiveDate = DateFormMapper.GetValue(_rowData[2]);
                    _hrmTrScheduleShiftDt.NoDay = Convert.ToInt32(_rowData[3]);

                    var _query2 = (
                                       from _msShift in this.db.HRMMsShifts
                                       where _msShift.ShiftName == _rowData[1]
                                       select new
                                       {
                                           ShiftCode = _msShift.ShiftCode
                                       }
                            ).FirstOrDefault();

                    _hrmTrScheduleShiftDt.ShiftCode = _query2.ShiftCode;

                    this.db.HRMTrScheduleShiftDts.InsertOnSubmit(_hrmTrScheduleShiftDt);
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

        public bool EditScheduleShiftDt(HRMTrScheduleShiftDt _prmScheduleShiftDt)
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

        public HRMTrScheduleShiftPosting GetSingleScheduleShiftPosting(string _prmEmpNumb, DateTime _prmScheduleDate)
        {
            HRMTrScheduleShiftPosting _result = null;

            try
            {
                _result = this.db.HRMTrScheduleShiftPostings.Single(_temp => _temp.EmpNumb == _prmEmpNumb && _temp.ScheduleDate == _prmScheduleDate);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        ~ScheduleShiftBL()
        {

        }
    }
}
