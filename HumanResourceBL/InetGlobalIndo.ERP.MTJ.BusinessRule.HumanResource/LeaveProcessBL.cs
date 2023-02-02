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
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class LeaveProcessBL : Base
    {
        public LeaveProcessBL()
        {
        }

        #region HRMTrLeaveProcessHd
        public double RowsCountHRMTrLeaveProcessHd(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Year")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "Period")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                           from _leaveProcessHd in this.db.HRMTrLeaveProcessHds
                           where (SqlMethods.Like(_leaveProcessHd.ProcessYear.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                              && (SqlMethods.Like(_leaveProcessHd.ProcessPeriod.ToString().Trim().ToLower(), _pattern2.Trim().ToLower()))
                           select _leaveProcessHd
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMTrLeaveProcessHd> GetListHRMTrLeaveProcessHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMTrLeaveProcessHd> _result = new List<HRMTrLeaveProcessHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Year")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "Period")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _leaveProcessHd in this.db.HRMTrLeaveProcessHds
                                where (SqlMethods.Like(_leaveProcessHd.ProcessYear.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                  && (SqlMethods.Like(_leaveProcessHd.ProcessPeriod.ToString().Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _leaveProcessHd.EditDate descending
                                select new
                                {
                                    ProcessYear = _leaveProcessHd.ProcessYear,
                                    ProcessPeriod = _leaveProcessHd.ProcessPeriod,
                                    Status = _leaveProcessHd.Status,
                                    Remark = _leaveProcessHd.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrLeaveProcessHd(_row.ProcessYear, _row.ProcessPeriod, _row.Status, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrLeaveProcessHd GetSingleHRMTrLeaveProcessHd(int _prmProcessYear, int _prmProcessPeriod)
        {
            HRMTrLeaveProcessHd _result = null;

            try
            {
                _result = this.db.HRMTrLeaveProcessHds.Single(_temp => _temp.ProcessYear == _prmProcessYear && _temp.ProcessPeriod == _prmProcessPeriod);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrLeaveProcessHd GetSingleHRMTrLeaveProcessHdView(int _prmProcessYear, int _prmProcessPeriod)
        {
            HRMTrLeaveProcessHd _result = new HRMTrLeaveProcessHd();

            try
            {
                var _query = (
                                from _leaveProcessHd in this.db.HRMTrLeaveProcessHds
                                orderby _leaveProcessHd.EditDate descending
                                where _leaveProcessHd.ProcessYear == _prmProcessYear
                                    && _leaveProcessHd.ProcessPeriod == _prmProcessPeriod
                                select new
                                {
                                    ProcessYear = _leaveProcessHd.ProcessYear,
                                    ProcessPeriod = _leaveProcessHd.ProcessPeriod,
                                    Status = _leaveProcessHd.Status,
                                    Remark = _leaveProcessHd.Remark
                                }
                            ).Single();

                _result.ProcessYear = _query.ProcessYear;
                _result.ProcessPeriod = _query.ProcessPeriod;
                _result.Status = _query.Status;
                _result.Remark = _query.Remark;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiHRMTrLeaveProcessHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    String[] _code = _prmCode[i].Split('-');

                    HRMTrLeaveProcessHd _leaveProcessHd = this.db.HRMTrLeaveProcessHds.Single(_temp => _temp.ProcessYear == Convert.ToInt32(_code[0]) && _temp.ProcessPeriod == Convert.ToInt32(_code[1]));

                    if (_leaveProcessHd != null)
                    {
                        if (_leaveProcessHd.Status != LeaveProcessDataMapper.GetStatus(TransStatus.Approved) &&
                            _leaveProcessHd.Status != LeaveProcessDataMapper.GetStatus(TransStatus.Posted))
                        {
                            var _query = (from _detail in this.db.HRMTrLeaveProcessDts
                                          where _detail.ProcessYear == Convert.ToInt32(_code[0])
                                            && _detail.ProcessPeriod == Convert.ToInt32(_code[1])
                                          select _detail);

                            this.db.HRMTrLeaveProcessDts.DeleteAllOnSubmit(_query);

                            this.db.HRMTrLeaveProcessHds.DeleteOnSubmit(_leaveProcessHd);

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

        public string AddHRMTrLeaveProcessHd(HRMTrLeaveProcessHd _prmHRMTrLeaveProcessHd)
        {
            string _result = "";

            try
            {
                this.db.HRMTrLeaveProcessHds.InsertOnSubmit(_prmHRMTrLeaveProcessHd);

                //List<HRMTrLeaveProcessDt> _detail = new List<HRMTrLeaveProcessDt>();

                //foreach (spHRM_TrLeaveProcessGetDtResult _item in this.db.spHRM_TrLeaveProcessGetDt(_prmHRMTrLeaveProcessHd.ProcessYear))
                //{
                //    HRMTrLeaveProcessDt _hrmTrLeaveProcessDt = new HRMTrLeaveProcessDt();

                //    _hrmTrLeaveProcessDt.ProcessYear = _prmHRMTrLeaveProcessHd.ProcessYear;
                //    _hrmTrLeaveProcessDt.ProcessPeriod = _prmHRMTrLeaveProcessHd.ProcessPeriod;
                //    _hrmTrLeaveProcessDt.EmployeeId = _item.EmpNumb;
                //    _hrmTrLeaveProcessDt.NewLeaveDay = _item.Total;
                //    _hrmTrLeaveProcessDt.StartEffective = _item.StartEffective;
                //    _hrmTrLeaveProcessDt.EndEffective = _item.EndEffective;

                //    _detail.Add(_hrmTrLeaveProcessDt);
                //}
                //this.db.HRMTrLeaveProcessDts.InsertAllOnSubmit(_detail);

                this.db.SubmitChanges();

                _result = _prmHRMTrLeaveProcessHd.ProcessYear.ToString() + "-" + _prmHRMTrLeaveProcessHd.ProcessPeriod.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string Generate(int _prmYear, int _prmPeriod)
        {
            string _result = "";

            try
            {
                List<HRMTrLeaveProcessDt> _detail = new List<HRMTrLeaveProcessDt>();

                foreach (spHRM_TrLeaveProcessGetDtResult _item in this.db.spHRM_TrLeaveProcessGetDt(_prmYear))
                {
                    HRMTrLeaveProcessDt _hrmTrLeaveProcessDt = new HRMTrLeaveProcessDt();

                    _hrmTrLeaveProcessDt.ProcessYear = _prmYear;
                    _hrmTrLeaveProcessDt.ProcessPeriod = _prmPeriod;
                    _hrmTrLeaveProcessDt.EmployeeId = _item.EmpNumb;
                    _hrmTrLeaveProcessDt.DefaultLeave = _item.DefaultTotal;
                    _hrmTrLeaveProcessDt.DeductionLeave = _item.Deduction;
                    _hrmTrLeaveProcessDt.NewLeaveDay = _item.Total;
                    _hrmTrLeaveProcessDt.StartEffective = _item.StartEffective;
                    _hrmTrLeaveProcessDt.EndEffective = _item.EndEffective;

                    _detail.Add(_hrmTrLeaveProcessDt);
                }
                this.db.HRMTrLeaveProcessDts.InsertAllOnSubmit(_detail);

                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                _result = "Generate Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditHRMTrLeaveProcessHd(HRMTrLeaveProcessHd _prmHRMTrLeaveProcessHd)
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

        public string GetAppr(int _prmPeriodYear, int _prmPeriodPeriod)
        {
            string _result = "";

            try
            {
                int _success = this.db.spHRM_TrLeaveProcessGetAppr(_prmPeriodYear, _prmPeriodPeriod, HttpContext.Current.User.Identity.Name, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMLeaveAdd);
                    _transActivity.TransNmbr = _prmPeriodYear.ToString() + "-" + _prmPeriodPeriod.ToString();
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

        public string Approve(int _prmPeriodYear, int _prmPeriodPeriod)
        {
            string _result = "";

            try
            {
                int _success = this.db.spHRM_TrLeaveProcessApprove(_prmPeriodYear, _prmPeriodPeriod, HttpContext.Current.User.Identity.Name, ref _result);

                Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                _transActivity.ActivitiesCode = Guid.NewGuid();
                _transActivity.TransType = AppModule.GetValue(TransactionType.HRMLeaveAdd);
                _transActivity.TransNmbr = _prmPeriodYear.ToString() + "-" + _prmPeriodPeriod.ToString();
                _transActivity.FileNmbr = "";
                _transActivity.Username = HttpContext.Current.User.Identity.Name;
                _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                _transActivity.ActivitiesDate = DateTime.Now;
                _transActivity.Reason = "";

                this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                this.db.SubmitChanges();

                _result = "Approve Success";
            }
            catch (Exception ex)
            {
                _result = "Approve Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Posting(int _prmPeriodYear, int _prmPeriodPeriod)
        {
            string _result = "";

            try
            {
                int _success = this.db.spHRM_TrLeaveProcessPosting(_prmPeriodYear, _prmPeriodPeriod, HttpContext.Current.User.Identity.Name, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMLeaveAdd);
                    _transActivity.TransNmbr = _prmPeriodYear.ToString() + "-" + _prmPeriodPeriod.ToString();
                    _transActivity.FileNmbr = "";
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

        public string Unposting(int _prmPeriodYear, int _prmPeriodPeriod)
        {
            string _result = "";

            try
            {
                int _success = this.db.spHRM_TrLeaveProcessUnPost(_prmPeriodYear, _prmPeriodPeriod, HttpContext.Current.User.Identity.Name, ref _result);

                if (_result == "")
                {
                    _result = "Unposting Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Unposting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        #endregion

        #region HRMTrLeaveProcessDt
        public double RowsCountHRMTrLeaveProcessDt(int _prmProcessYear, int _prmProcessPeriod)
        {
            double _result = 0;

            var _query =
                        (
                           from _leaveProcessDt in this.db.HRMTrLeaveProcessDts
                           where _leaveProcessDt.ProcessYear == _prmProcessYear
                                && _leaveProcessDt.ProcessPeriod == _prmProcessPeriod
                           select _leaveProcessDt
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMTrLeaveProcessDt> GetListHRMTrLeaveProcessDt(int _prmReqPage, int _prmPageSize, int _prmProcessYear, int _prmProcessPeriod)
        {
            List<HRMTrLeaveProcessDt> _result = new List<HRMTrLeaveProcessDt>();

            try
            {
                var _query = (
                                from _leaveProcessDt in this.db.HRMTrLeaveProcessDts
                                join _msEmp in this.db.MsEmployees
                                    on _leaveProcessDt.EmployeeId equals _msEmp.EmpNumb
                                where _leaveProcessDt.ProcessYear == _prmProcessYear
                                    && _leaveProcessDt.ProcessPeriod == _prmProcessPeriod
                                orderby _msEmp.EmpName
                                select new
                                {
                                    ProcessYear = _leaveProcessDt.ProcessYear,
                                    ProcessPeriod = _leaveProcessDt.ProcessPeriod,
                                    EmployeeId = _leaveProcessDt.EmployeeId,
                                    EmpName = _msEmp.EmpName,
                                    NewLeaveDay = _leaveProcessDt.NewLeaveDay,
                                    StartEffective = _leaveProcessDt.StartEffective,
                                    EndEffective = _leaveProcessDt.EndEffective,
                                    DefaultLeave = _leaveProcessDt.DefaultLeave,
                                    DeductionLeave = _leaveProcessDt.DeductionLeave
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrLeaveProcessDt(_row.ProcessYear, _row.ProcessPeriod, _row.EmployeeId, _row.EmpName, _row.NewLeaveDay, _row.StartEffective, _row.EndEffective, _row.DefaultLeave, _row.DeductionLeave));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrLeaveProcessDt GetSingleHRMTrLeaveProcessDt(int _prmProcessYear, int _prmProcessPeriod, String _prmEmpNumb)
        {
            HRMTrLeaveProcessDt _result = null;

            try
            {
                _result = this.db.HRMTrLeaveProcessDts.Single(_temp => _temp.ProcessYear == _prmProcessYear && _temp.ProcessPeriod == _prmProcessPeriod && _temp.EmployeeId == _prmEmpNumb);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiHRMTrLeaveProcessDt(int _prmProcessYear, int _prmProcessPeriod, String[] _prmEmpNumb)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmEmpNumb.Length; i++)
                {
                    String[] _empNumb = _prmEmpNumb[i].Split('=');

                    HRMTrLeaveProcessDt _leaveProcessDt = this.db.HRMTrLeaveProcessDts.Single(_temp => _temp.ProcessYear == _prmProcessYear && _temp.ProcessPeriod == _prmProcessPeriod && _temp.EmployeeId.Trim().ToLower() == _empNumb[1].Trim().ToLower());

                    if (_leaveProcessDt != null)
                    {
                        this.db.HRMTrLeaveProcessDts.DeleteOnSubmit(_leaveProcessDt);
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

        public bool AddHRMTrLeaveProcessDt(HRMTrLeaveProcessDt _prmHRMTrLeaveProcessDt)
        {
            bool _result = false;

            try
            {
                this.db.HRMTrLeaveProcessDts.InsertOnSubmit(_prmHRMTrLeaveProcessDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditHRMTrLeaveProcessDt(HRMTrLeaveProcessDt _prmHRMTrLeaveProcessDt)
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

        ~LeaveProcessBL()
        {
        }
    }
}
