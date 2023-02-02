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
    public sealed class LemburBL : Base
    {
        public LemburBL()
        {

        }

        #region HRMTrLemburHd


        public double RowsCountLembur(string _prmCategory, string _prmKeyword)
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
                               from _lembur in this.db.HRMTrLemburHds
                               where (SqlMethods.Like(_lembur.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like((_lembur.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && _lembur.Status != LemburDataMapper.GetStatus(TransStatus.Deleted)
                               select _lembur.TransNmbr
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrLemburHd> GetListLembur(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMTrLemburHd> _result = new List<HRMTrLemburHd>();

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
                                from _lembur in this.db.HRMTrLemburHds
                                where (SqlMethods.Like(_lembur.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_lembur.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _lembur.Status != LemburDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _lembur.EditDate descending
                                select new
                                {
                                    TransNmbr = _lembur.TransNmbr,
                                    FileNmbr = _lembur.FileNmbr,
                                    Status = _lembur.Status,
                                    TransDate = _lembur.TransDate,
                                    DayType = _lembur.DayType,
                                    Remark = _lembur.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrLemburHd(_row.TransNmbr, _row.FileNmbr, _row.Status, _row.TransDate, _row.DayType, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrLemburHd GetSingleLembur(string _prmCode)
        {
            HRMTrLemburHd _result = null;

            try
            {
                _result = this.db.HRMTrLemburHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleLemburApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrLemburHd _hRMTrLeaveAddHd = this.db.HRMTrLemburHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrLeaveAddHd != null)
                    {
                        if (_hRMTrLeaveAddHd.Status != LeaveAddDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiLembur(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrLemburHd _Lembur = this.db.HRMTrLemburHds.Single(_temp => _temp.TransNmbr == _prmCode[i]);

                    if ((_Lembur.FileNmbr ?? "").Trim() == "")
                    {
                        var _query = (from _detail in this.db.HRMTrLemburDts
                                      where _detail.TransNmbr == _prmCode[i]
                                      select _detail);

                        this.db.HRMTrLemburDts.DeleteAllOnSubmit(_query);

                        this.db.HRMTrLemburHds.DeleteOnSubmit(_Lembur);

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

        public bool DeleteMultiApproveLembur(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrLemburHd _hRMTrLemburHd = this.db.HRMTrLemburHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrLemburHd.Status == LemburDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _hRMTrLemburHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _hRMTrLemburHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }


                    if (_hRMTrLemburHd != null)
                    {
                        if ((_hRMTrLemburHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.HRMTrLemburDts
                                          where _detail.TransNmbr.ToLower().Trim() == _prmCode[i].ToLower().Trim()
                                          select _detail);

                            this.db.HRMTrLemburDts.DeleteAllOnSubmit(_query);

                            this.db.HRMTrLemburHds.DeleteOnSubmit(_hRMTrLemburHd);

                            _result = true;
                        }
                        else if (_hRMTrLemburHd.FileNmbr != "" && _hRMTrLemburHd.Status == LemburDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _hRMTrLemburHd.Status = LemburDataMapper.GetStatus(TransStatus.Deleted);
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

        public bool EditLembur(HRMTrLemburHd _prmLembur)
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

        public string AddLembur(HRMTrLemburHd _prmLembur)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();

                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmLembur.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRMTrLemburHds.InsertOnSubmit(_prmLembur);

                var _query = (
                               from _temp in this.db.Temporary_TransactionNumbers
                               where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                               select _temp
                             );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmLembur.TransNmbr.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrLemburHd> GetListDDLLembur()
        {
            List<HRMTrLemburHd> _result = new List<HRMTrLemburHd>();

            try
            {
                var _query = (
                                from _lemburHd in this.db.HRMTrLemburHds
                                where _lemburHd.Status == LemburDataMapper.GetStatus(TransStatus.Posted)
                                orderby _lemburHd.FileNmbr ascending
                                select new
                                {
                                    TransNmbr = _lemburHd.TransNmbr,
                                    FileNmbr = _lemburHd.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrLemburHd(_row.TransNmbr, _row.FileNmbr));
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
                this.db.spHRM_LemburGetAppr(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMLembur);
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
                    this.db.spHRM_LemburApprove(_prmCode, _prmuser, ref _result);

                    if (_result == "")
                    {
                        HRMTrLemburHd _lembur = this.GetSingleLembur(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_lembur.TransDate.Year, _lembur.TransDate.Month, AppModule.GetValue(TransactionType.HRMLembur), this._companyTag, ""))
                        {
                            _lembur.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.HRMLembur);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = _lembur.FileNmbr;
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
                this.db.spHRM_LemburPosting(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMLembur);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = this.GetSingleLembur(_prmCode).FileNmbr;
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
                this.db.spHRM_LemburUnPost(_prmCode, _prmuser, ref _result);

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

        public Boolean Cancel(string _prmTransNmbr, string _prmEmpNumb, string _prmRemark, string _prmuser)
        {
            Boolean _result = false;

            try
            {
                HRMTrLemburDt _lemburDt = this.GetSingleLemburDt(_prmEmpNumb, _prmTransNmbr);

                _lemburDt.CancelRemark = _prmRemark;
                _lemburDt.StatusProcess = 'C';

                this.db.SubmitChanges();
                
                _result = true;

                //this.db.spHRM_LemburCancel(_prmTransNmbr, _prmEmpNumb, _prmRemark, _prmuser, ref _result);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        #region HRMTrLemburDt

        public double RowsCountLemburDt(string _prmCode)
        {
            double _result = 0;

            var _query =
             (
                from _lemburDt in this.db.HRMTrLemburDts
                where _lemburDt.TransNmbr == _prmCode
                select _lemburDt.EmpNumb
             ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMTrLemburDt> GetListLemburDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRMTrLemburDt> _result = new List<HRMTrLemburDt>();

            try
            {
                var _query = (
                                from _lemburDt in this.db.HRMTrLemburDts
                                where _lemburDt.TransNmbr == _prmCode
                                orderby _lemburDt.EmpNumb ascending
                                select new
                                {
                                    TransNmbr = _lemburDt.TransNmbr,
                                    EmpNumb = _lemburDt.EmpNumb,
                                    EmpName = (
                                                from _msEmployee in this.db.MsEmployees
                                                where _msEmployee.EmpNumb == _lemburDt.EmpNumb
                                                select _msEmployee.EmpName
                                              ).FirstOrDefault(),
                                    StartHours = _lemburDt.StartHours,
                                    EndHours = _lemburDt.EndHours,
                                    Hours = _lemburDt.Hours,
                                    FgFullDays = _lemburDt.FgFullDays,
                                    FgMakan = _lemburDt.FgMakan,
                                    FgShift = _lemburDt.FgShift,
                                    StatusProcess = _lemburDt.StatusProcess
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrLemburDt(_row.TransNmbr, _row.EmpNumb, _row.EmpName, _row.StartHours, _row.EndHours, _row.Hours, _row.FgFullDays, _row.FgMakan, _row.FgShift, _row.StatusProcess));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrLemburDt GetSingleLemburDt(string _prmEmpNumb, string _prmTransNmbr)
        {
            HRMTrLemburDt _result = null;

            try
            {
                _result = this.db.HRMTrLemburDts.Single(_temp => _temp.EmpNumb == _prmEmpNumb && _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiLemburDt(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('=');

                    HRMTrLemburDt _LemburDt = this.db.HRMTrLemburDts.Single(_temp => _temp.TransNmbr == _tempSplit[0] && _temp.EmpNumb == _tempSplit[1]);

                    this.db.HRMTrLemburDts.DeleteOnSubmit(_LemburDt);
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

        public bool AddLemburDt(HRMTrLemburDt _prmLemburDt)
        {
            bool _result = false;

            try
            {
                this.db.HRMTrLemburDts.InsertOnSubmit(_prmLemburDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditLemburDt(HRMTrLemburDt _prmLemburDt)
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

        ~LemburBL()
        {

        }
    }
}
