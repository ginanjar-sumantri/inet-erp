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
    public sealed class AbsenceRequestBL : Base
    {
        public AbsenceRequestBL()
        {
        }

        #region HRM.AbsenceRequest
        public double RowsCountAbsenceRequest(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "FileNmbr")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "EmpNumb")
            {
                _pattern1 = "%%";
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "EmpName")
            {
                _pattern1 = "%%";
                _pattern4 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
                _pattern2 = "%%";
            }

            var _query =
                        (
                           from _absenceRequest in this.db.HRM_AbsenceRequests
                           join _emp in this.db.MsEmployees
                                    on _absenceRequest.EmpNumb equals _emp.EmpNumb
                           where (SqlMethods.Like(_absenceRequest.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like((_absenceRequest.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like(_absenceRequest.EmpNumb.Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && (SqlMethods.Like(_emp.EmpName.Trim().ToLower(), _pattern4.Trim().ToLower()))
                               && _absenceRequest.Status != AbsenceRequestDataMapper.GetStatusByte(TransStatus.Deleted)
                           select _absenceRequest
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<HRM_AbsenceRequest> GetListAbsenceRequest(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRM_AbsenceRequest> _result = new List<HRM_AbsenceRequest>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "FileNmbr")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "EmpNumb")
            {
                _pattern1 = "%%";
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "EmpName")
            {
                _pattern1 = "%%";
                _pattern4 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
                _pattern2 = "%%";
            }

            try
            {
                var _query = (
                                from _absenceRequest in this.db.HRM_AbsenceRequests
                                join _emp in this.db.MsEmployees
                                    on _absenceRequest.EmpNumb equals _emp.EmpNumb
                                where (SqlMethods.Like(_absenceRequest.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_absenceRequest.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (SqlMethods.Like(_absenceRequest.EmpNumb.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                    && (SqlMethods.Like(_emp.EmpName.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                    && _absenceRequest.Status != AbsenceRequestDataMapper.GetStatusByte(TransStatus.Deleted)
                                orderby _absenceRequest.EditDate descending
                                select new
                                {
                                    AbsenceRequestCode = _absenceRequest.AbsenceRequestCode,
                                    TransNmbr = _absenceRequest.TransNmbr,
                                    FileNmbr = _absenceRequest.FileNmbr,
                                    TransDate = _absenceRequest.TransDate,
                                    Status = _absenceRequest.Status,
                                    EmpNumb = _absenceRequest.EmpNumb,
                                    EmpName = _emp.EmpName,
                                    StartDate = _absenceRequest.StartDate,
                                    EndDate = _absenceRequest.EndDate,
                                    AbsenceTypeCode = _absenceRequest.AbsenceTypeCode,
                                    AbsenceTypeName = (
                                                        from _absType in this.db.HRMMsAbsenceTypes
                                                        where _absType.AbsenceTypeCode == _absenceRequest.AbsenceTypeCode
                                                        select _absType.AbsenceTypeName
                                                      ).FirstOrDefault(),
                                    Days = _absenceRequest.Days,
                                    Remark = _absenceRequest.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_AbsenceRequest(_row.AbsenceRequestCode, _row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.EmpNumb, _row.EmpName, _row.StartDate, _row.EndDate, _row.AbsenceTypeCode, _row.AbsenceTypeName, _row.Days, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_AbsenceRequest GetSingleAbsenceRequest(string _prmCode)
        {
            HRM_AbsenceRequest _result = null;

            try
            {
                _result = this.db.HRM_AbsenceRequests.Single(_temp => _temp.AbsenceRequestCode == new Guid(_prmCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleHRM_AbsenceRequestApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_AbsenceRequest _hrm_AbsenceRequest = this.db.HRM_AbsenceRequests.Single(_temp => _temp.AbsenceRequestCode == new Guid (_prmCode[i]));

                    if (_hrm_AbsenceRequest != null)
                    {
                        if (_hrm_AbsenceRequest.Status != AbsenceRequestDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiAbsenceRequest(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_AbsenceRequest _absenceRequest = this.db.HRM_AbsenceRequests.Single(_temp => _temp.AbsenceRequestCode == new Guid(_prmCode[i]));

                    if ((_absenceRequest.FileNmbr ?? "").Trim() == "")
                    {
                        var _query = (from _detail in this.db.HRM_AbsenceRequestActings
                                      where _detail.AbsenceRequestCode == new Guid(_prmCode[i])
                                      select _detail);

                        this.db.HRM_AbsenceRequestActings.DeleteAllOnSubmit(_query);
                        this.db.HRM_AbsenceRequests.DeleteOnSubmit(_absenceRequest);

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

        public bool DeleteMultiApproveHRM_AbsenceRequest(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_AbsenceRequest _hrm_AbsenceRequest = this.db.HRM_AbsenceRequests.Single(_temp => _temp.AbsenceRequestCode == new Guid(_prmCode[i]));

                    if (_hrm_AbsenceRequest.Status == AbsenceRequestDataMapper.GetStatusByte(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _hrm_AbsenceRequest.TransNmbr;
                        _unpostingActivity.FileNmbr = _hrm_AbsenceRequest.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }


                    if (_hrm_AbsenceRequest != null)
                    {
                        if ((_hrm_AbsenceRequest.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.HRM_AbsenceRequestActings
                                          where _detail.AbsenceRequestCode == new Guid(_prmCode[i])
                                          select _detail);

                            this.db.HRM_AbsenceRequestActings.DeleteAllOnSubmit(_query);

                            this.db.HRM_AbsenceRequests.DeleteOnSubmit(_hrm_AbsenceRequest);

                            _result = true;
                        }
                        else if (_hrm_AbsenceRequest.FileNmbr != "" && _hrm_AbsenceRequest.Status == AbsenceRequestDataMapper.GetStatusByte(TransStatus.Approved))
                        {
                            _hrm_AbsenceRequest.Status = AbsenceRequestDataMapper.GetStatusByte(TransStatus.Deleted);
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

        public bool EditAbsenceRequest(HRM_AbsenceRequest _prmAbsenceRequest)
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

        public string AddAbsenceRequest(HRM_AbsenceRequest _prmAbsenceRequest)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();

                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmAbsenceRequest.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRM_AbsenceRequests.InsertOnSubmit(_prmAbsenceRequest);

                var _query = (
                               from _temp in this.db.Temporary_TransactionNumbers
                               where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                               select _temp
                             );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);
                this.db.SubmitChanges();

                _result = _prmAbsenceRequest.AbsenceRequestCode.ToString();
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
            //String _validasi = "", _nmbr = "", _absTypeCode = "", _empNumb = "";
            //DateTime _startDate, _endDate;
            //int? _days = 0, _maxDaysAllowed = 0, _beginMonth = 0, _endingMonth = 0, _leaveDayRemain = 0,
            //    _leaveCurrent = 0, _totalAlreadyCuttedDays = 0, _alreadyCuttedDays1 = 0, _alreadyCuttedDays2 = 0;

            try
            {
                //var _queryAbsReq = (
                //                        from _absReq in this.db.HRM_AbsenceRequests
                //                        where _absReq.AbsenceRequestCode == new Guid(_prmCode)
                //                        select new
                //                        {
                //                            _absReq.TransNmbr,
                //                            _absReq.AbsenceTypeCode,
                //                            _absReq.EmpNumb,
                //                            _absReq.StartDate,
                //                            _absReq.EndDate,
                //                            _absReq.Days
                //                        }
                //                   ).FirstOrDefault();
                //_nmbr = _queryAbsReq.TransNmbr;
                //_absTypeCode = _queryAbsReq.AbsenceTypeCode.ToString();
                //_empNumb = _queryAbsReq.EmpNumb;
                //_startDate = _queryAbsReq.StartDate;
                //_endDate = _queryAbsReq.EndDate;
                //_days = _queryAbsReq.Days;

                //var _queryAbsType = (
                //                        from _absType in this.db.HRMMsAbsenceTypes
                //                        where _absType.AbsenceTypeCode == _absTypeCode
                //                        select new
                //                        {
                //                            _absType.IsActingRequired
                //                            //,_absType.IsCutLeave
                //                        }
                //                   ).FirstOrDefault();

                //var _queryEmpLeaveDay = (
                //                        from _empLeaveDay in this.db.Master_EmpLeaveDays
                //                        where _empLeaveDay.EmpNumb == _empNumb
                //                        select new
                //                        {
                //                            _empLeaveDay.LeaveDayRemain,
                //                            _empLeaveDay.ExpiredDateLeaveRemain,
                //                            _empLeaveDay.LeaveCurrent,
                //                            _empLeaveDay.ExpiredDateLeaveCurrent
                //                        }
                //                   ).FirstOrDefault();
                //_leaveDayRemain = _queryEmpLeaveDay.LeaveDayRemain;
                //_leaveCurrent = _queryEmpLeaveDay.LeaveCurrent;

                //var _queryAllowed = (
                //                        (
                //                            from _leaveAllowed1 in this.db.HRM_AbsenceTypeLeaveAlloweds
                //                            where _leaveAllowed1.AbsenceTypeCode == new Guid(_absTypeCode)
                //                                 && _startDate.Month >= _leaveAllowed1.BeginMonth
                //                                 && _startDate.Month <= _leaveAllowed1.EndingMonth
                //                            select new
                //                            {
                //                                _leaveAllowed1.MaxDaysAllowed,
                //                                _leaveAllowed1.BeginMonth,
                //                                _leaveAllowed1.EndingMonth
                //                            }
                //                        ).Union(
                //                            from _leaveAllowed2 in this.db.HRM_AbsenceTypeLeaveAlloweds
                //                            where _leaveAllowed2.AbsenceTypeCode == new Guid(_absTypeCode)
                //                                && (
                //                                        (_startDate.Month <= _leaveAllowed2.EndingMonth && _leaveAllowed2.BeginMonth > _leaveAllowed2.EndingMonth)
                //                                        ||
                //                                        (_startDate.Month >= _leaveAllowed2.BeginMonth && _leaveAllowed2.BeginMonth > _leaveAllowed2.EndingMonth)
                //                                   )
                //                            select new
                //                            {
                //                                _leaveAllowed2.MaxDaysAllowed,
                //                                _leaveAllowed2.BeginMonth,
                //                                _leaveAllowed2.EndingMonth
                //                            }
                //                        )
                //                   ).FirstOrDefault();
                //if (_queryAllowed != null)
                //{
                //    _maxDaysAllowed = _queryAllowed.MaxDaysAllowed;
                //    _beginMonth = _queryAllowed.BeginMonth;
                //    _endingMonth = _queryAllowed.EndingMonth;
                //}

                //var _q1 = (
                //                from _absReq in this.db.HRM_AbsenceRequests
                //                where _absReq.AbsenceRequestCode == new Guid(_prmCode)
                //                    && _endDate < _startDate
                //                select _absReq.AbsenceTypeCode
                //          ).Count();
                //if (_q1 > 0) _validasi = "Get Approval Failed... Absence Request End Date must be larger than Start Date";

                //if (_queryAbsType.IsActingRequired == true)
                //{
                //    var _q2 = (
                //                from _absReqAct in this.db.HRM_AbsenceRequestActings
                //                where _absReqAct.AbsenceRequestCode == new Guid(_prmCode)
                //                select _absReqAct.AbsenceRequestCode
                //          ).Count();
                //    if (_q2 <= 0) _validasi = "Get Approval Failed... Detail Absence Request " + _nmbr + " must be input";
                //}

                ////if (_queryAbsType.IsCutLeave == true)
                ////{
                //if (_queryAllowed != null)
                //{
                //    if (_beginMonth < _endingMonth)
                //    {
                //        var _qSum1 = (
                //                        from _absReq in this.db.HRM_AbsenceRequests
                //                        where _absReq.AbsenceTypeCode == _absTypeCode
                //                            && _absReq.EmpNumb == _empNumb
                //                            && _absReq.Status == AbsenceRequestDataMapper.GetStatus(AbsenceRequestStatus.Approved)
                //                            && _absReq.StartDate.Month >= _beginMonth && _absReq.StartDate.Month <= _endingMonth
                //                        select Convert.ToInt32(_absReq.Days)
                //                     );
                //        if (_qSum1.Count() > 0)
                //        {
                //            _alreadyCuttedDays1 = _qSum1.Sum();
                //        }
                //    }
                //    else
                //    {
                //        var _qSum2 = (
                //                        from _absReq in this.db.HRM_AbsenceRequests
                //                        where _absReq.AbsenceTypeCode == _absTypeCode
                //                            && _absReq.EmpNumb == _empNumb
                //                            && _absReq.Status == AbsenceRequestDataMapper.GetStatus(AbsenceRequestStatus.Approved)
                //                            && _absReq.StartDate.Month >= _endingMonth
                //                        select Convert.ToInt32(_absReq.Days)
                //                     );
                //        int _tempCuttedDays1 = 0;
                //        if (_qSum2.Count() > 0)
                //        {
                //            _tempCuttedDays1 = _qSum2.Sum();
                //        }

                //        var _qSum3 = (
                //                        from _absReq in this.db.HRM_AbsenceRequests
                //                        where _absReq.AbsenceTypeCode == _absTypeCode
                //                            && _absReq.EmpNumb == _empNumb
                //                            && _absReq.Status == AbsenceRequestDataMapper.GetStatus(AbsenceRequestStatus.Approved)
                //                            && _absReq.StartDate.Month <= _beginMonth
                //                        select (_absReq.Days == null) ? 0 : Convert.ToInt32(_absReq.Days)
                //                     );
                //        int _tempCuttedDays2 = 0;
                //        if (_qSum3.Count() > 0)
                //        {
                //            _tempCuttedDays2 = _qSum3.Sum();
                //        }

                //        _alreadyCuttedDays2 = _tempCuttedDays1 + _tempCuttedDays2;
                //    }

                //    _totalAlreadyCuttedDays = _alreadyCuttedDays1 + _alreadyCuttedDays2;
                //}

                //if (_startDate <= _queryEmpLeaveDay.ExpiredDateLeaveRemain || _queryEmpLeaveDay.ExpiredDateLeaveRemain == null)
                //{
                //    if (_queryAllowed != null)
                //    {
                //        if (_days > _maxDaysAllowed - _totalAlreadyCuttedDays || _days > _leaveDayRemain + _leaveCurrent)
                //        {
                //            if (_maxDaysAllowed - _totalAlreadyCuttedDays < _leaveDayRemain + _leaveCurrent)
                //            {
                //                _validasi = "Get Approval Failed... Employee only have " + (_maxDaysAllowed - _totalAlreadyCuttedDays).ToString() + " Leave day(s) left for this period";
                //            }
                //            else
                //            {
                //                _validasi = "Get Approval Failed... Employee only have " + (_leaveDayRemain + _leaveCurrent).ToString() + " Leave day(s) left for this period";
                //            }
                //        }
                //    }
                //    else
                //    {
                //        if (_days > _leaveDayRemain + _leaveCurrent)
                //        {
                //            _validasi = "Get Approval Failed... Employee only have " + (_leaveDayRemain + _leaveCurrent).ToString() + " Leave day(s) left for this period";
                //        }
                //    }
                //}
                //else if (_startDate <= _queryEmpLeaveDay.ExpiredDateLeaveCurrent || _queryEmpLeaveDay.ExpiredDateLeaveCurrent == null)
                //{
                //    if (_queryAllowed != null)
                //    {
                //        if (_days > _maxDaysAllowed - _totalAlreadyCuttedDays || _days > _leaveCurrent)
                //        {
                //            if (_maxDaysAllowed - _totalAlreadyCuttedDays < _leaveCurrent)
                //            {
                //                _validasi = "Get Approval Failed... Employee only have " + (_maxDaysAllowed - _totalAlreadyCuttedDays).ToString() + " Leave day(s) left for this period";
                //            }
                //            else
                //            {
                //                _validasi = "Get Approval Failed... Employee only have " + _leaveCurrent.ToString() + " Leave day(s) left for this period";
                //            }
                //        }
                //    }
                //    else
                //    {
                //        if (_days > _leaveCurrent)
                //        {
                //            _validasi = "Get Approval Failed... Employee only have " + _leaveCurrent.ToString() + " Leave day(s) left for this period";
                //        }
                //    }
                //}
                //else
                //{
                //    _validasi = "Get Approval Failed... Employee does not have enough Leave day(s) left in this period";
                //}
                ////}

                //if (_validasi == "")
                //{
                this.db.spHRM_AbsenceRequestGetAppr(new Guid(_prmCode), _prmuser, ref _result);
                //}
                //else
                //{
                //    _result = _validasi;
                //}

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMAbsenceRequest);
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
            //String _validasi = "", _nmbr = "", _absTypeCode = "", _empNumb = "";
            //DateTime _startDate, _endDate;
            //int? _days = 0;
            //int _maxDaysAllowed = 0, _beginMonth = 0, _endingMonth = 0, _leaveDayRemain = 0,
            //_leaveCurrent = 0, _totalAlreadyCuttedDays = 0, _alreadyCuttedDays1 = 0, _alreadyCuttedDays2 = 0,
            //_cutRemain = 0, _cutCurrent = 0;

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    //var _queryAbsReq = (
                    //                    from _absReq in this.db.HRM_AbsenceRequests
                    //                    where _absReq.AbsenceRequestCode == new Guid(_prmCode)
                    //                    select new
                    //                    {
                    //                        _absReq.TransNmbr,
                    //                        _absReq.AbsenceTypeCode,
                    //                        _absReq.EmpNumb,
                    //                        _absReq.StartDate,
                    //                        _absReq.EndDate,
                    //                        _absReq.Days
                    //                    }
                    //               ).FirstOrDefault();
                    //_nmbr = _queryAbsReq.TransNmbr;
                    //_absTypeCode = _queryAbsReq.AbsenceTypeCode.ToString();
                    //_empNumb = _queryAbsReq.EmpNumb;
                    //_startDate = _queryAbsReq.StartDate;
                    //_endDate = _queryAbsReq.EndDate;
                    //_days = _queryAbsReq.Days;

                    //var _queryAbsType = (
                    //                        from _absType in this.db.HRMMsAbsenceTypes
                    //                        where _absType.AbsenceTypeCode == _absTypeCode
                    //                        select new
                    //                        {
                    //                            _absType.IsActingRequired
                    //                            //,_absType.IsCutLeave
                    //                        }
                    //                   ).FirstOrDefault();

                    //var _queryEmpLeaveDay = (
                    //                        from _msEmpLeaveDay in this.db.Master_EmpLeaveDays
                    //                        where _msEmpLeaveDay.EmpNumb == _empNumb
                    //                        select new
                    //                        {
                    //                            _msEmpLeaveDay.LeaveDayRemain,
                    //                            _msEmpLeaveDay.ExpiredDateLeaveRemain,
                    //                            _msEmpLeaveDay.LeaveCurrent,
                    //                            _msEmpLeaveDay.ExpiredDateLeaveCurrent
                    //                        }
                    //                   ).FirstOrDefault();
                    //_leaveDayRemain = _queryEmpLeaveDay.LeaveDayRemain;
                    //_leaveCurrent = _queryEmpLeaveDay.LeaveCurrent;

                    //var _queryAllowed = (
                    //                        (
                    //                            from _leaveAllowed1 in this.db.HRM_AbsenceTypeLeaveAlloweds
                    //                            where _leaveAllowed1.AbsenceTypeCode == new Guid(_absTypeCode)
                    //                                 && _startDate.Month >= _leaveAllowed1.BeginMonth
                    //                                 && _startDate.Month <= _leaveAllowed1.EndingMonth
                    //                            select new
                    //                            {
                    //                                _leaveAllowed1.MaxDaysAllowed,
                    //                                _leaveAllowed1.BeginMonth,
                    //                                _leaveAllowed1.EndingMonth
                    //                            }
                    //                        ).Union(
                    //                            from _leaveAllowed2 in this.db.HRM_AbsenceTypeLeaveAlloweds
                    //                            where _leaveAllowed2.AbsenceTypeCode == new Guid(_absTypeCode)
                    //                                && (
                    //                                        (_startDate.Month <= _leaveAllowed2.EndingMonth && _leaveAllowed2.BeginMonth > _leaveAllowed2.EndingMonth)
                    //                                        ||
                    //                                        (_startDate.Month >= _leaveAllowed2.BeginMonth && _leaveAllowed2.BeginMonth > _leaveAllowed2.EndingMonth)
                    //                                   )
                    //                            select new
                    //                            {
                    //                                _leaveAllowed2.MaxDaysAllowed,
                    //                                _leaveAllowed2.BeginMonth,
                    //                                _leaveAllowed2.EndingMonth
                    //                            }
                    //                        )
                    //                   ).FirstOrDefault();
                    //if (_queryAllowed != null)
                    //{
                    //    _maxDaysAllowed = _queryAllowed.MaxDaysAllowed;
                    //    _beginMonth = _queryAllowed.BeginMonth;
                    //    _endingMonth = _queryAllowed.EndingMonth;
                    //}

                    //var _q1 = (
                    //                from _absReq in this.db.HRM_AbsenceRequests
                    //                where _absReq.AbsenceRequestCode == new Guid(_prmCode)
                    //                    && _endDate < _startDate
                    //                select _absReq.AbsenceTypeCode
                    //          ).Count();
                    //if (_q1 > 0) _validasi = "Approve Failed... Absence Request End Date must be larger than Start Date";

                    //if (_queryAbsType.IsActingRequired == true)
                    //{
                    //    var _q2 = (
                    //                from _absReqAct in this.db.HRM_AbsenceRequestActings
                    //                where _absReqAct.AbsenceRequestCode == new Guid(_prmCode)
                    //                select _absReqAct.AbsenceRequestCode
                    //          ).Count();
                    //    if (_q2 <= 0) _validasi = "Approve Failed... Detail Absence Request " + _nmbr + " must be input";
                    //}

                    ////if (_queryAbsType.IsCutLeave == true)
                    ////{
                    //if (_queryAllowed != null)
                    //{
                    //    if (_beginMonth < _endingMonth)
                    //    {
                    //        var _qSum1 = (
                    //                        from _absReq in this.db.HRM_AbsenceRequests
                    //                        where _absReq.AbsenceTypeCode == _absTypeCode
                    //                            && _absReq.EmpNumb == _empNumb
                    //                            && _absReq.Status == AbsenceRequestDataMapper.GetStatus(AbsenceRequestStatus.Approved)
                    //                            && _absReq.StartDate.Month >= _beginMonth && _absReq.StartDate.Month <= _endingMonth
                    //                        select Convert.ToInt32(_absReq.Days)
                    //                     );
                    //        if (_qSum1.Count() > 0)
                    //        {
                    //            _alreadyCuttedDays1 = _qSum1.Sum();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        var _qSum2 = (
                    //                        from _absReq in this.db.HRM_AbsenceRequests
                    //                        where _absReq.AbsenceTypeCode == _absTypeCode
                    //                            && _absReq.EmpNumb == _empNumb
                    //                            && _absReq.Status == AbsenceRequestDataMapper.GetStatus(AbsenceRequestStatus.Approved)
                    //                            && _absReq.StartDate.Month >= _endingMonth
                    //                        select Convert.ToInt32(_absReq.Days)
                    //                     );
                    //        int _tempCuttedDays1 = 0;
                    //        if (_qSum2.Count() > 0)
                    //        {
                    //            _tempCuttedDays1 = _qSum2.Sum();
                    //        }

                    //        var _qSum3 = (
                    //                        from _absReq in this.db.HRM_AbsenceRequests
                    //                        where _absReq.AbsenceTypeCode == _absTypeCode
                    //                            && _absReq.EmpNumb == _empNumb
                    //                            && _absReq.Status == AbsenceRequestDataMapper.GetStatus(AbsenceRequestStatus.Approved)
                    //                            && _absReq.StartDate.Month <= _beginMonth
                    //                        select (_absReq.Days == null) ? 0 : Convert.ToInt32(_absReq.Days)
                    //                     );
                    //        int _tempCuttedDays2 = 0;
                    //        if (_qSum3.Count() > 0)
                    //        {
                    //            _tempCuttedDays2 = _qSum3.Sum();
                    //        }

                    //        _alreadyCuttedDays2 = _tempCuttedDays1 + _tempCuttedDays2;
                    //    }

                    //    _totalAlreadyCuttedDays = _alreadyCuttedDays1 + _alreadyCuttedDays2;
                    //}

                    ////HRM_AbsenceRequest _absRequest = this.GetSingleAbsenceRequest(_prmCode);
                    //Master_EmpLeaveDay _empLeaveDay = this.db.Master_EmpLeaveDays.Single(_temp => _temp.EmpNumb == _empNumb);

                    //if (_startDate <= _queryEmpLeaveDay.ExpiredDateLeaveRemain || _queryEmpLeaveDay.ExpiredDateLeaveRemain == null)
                    //{
                    //    if (_queryAllowed != null)
                    //    {
                    //        if (_days > _maxDaysAllowed - _totalAlreadyCuttedDays || _days > _leaveDayRemain + _leaveCurrent)
                    //        {
                    //            if (_maxDaysAllowed - _totalAlreadyCuttedDays < _leaveDayRemain + _leaveCurrent)
                    //            {
                    //                _validasi = "Approve Failed... Employee only have " + (_maxDaysAllowed - _totalAlreadyCuttedDays).ToString() + " Leave day(s) left for this period";
                    //            }
                    //            else
                    //            {
                    //                _validasi = "Approve Failed... Employee only have " + (_leaveDayRemain + _leaveCurrent).ToString() + " Leave day(s) left for this period";
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (_days > _leaveDayRemain + _leaveCurrent)
                    //        {
                    //            _validasi = "Approve Failed... Employee only have " + (_leaveDayRemain + _leaveCurrent).ToString() + " Leave day(s) left for this period";
                    //        }
                    //    }

                    //    if (_validasi == "")
                    //    {
                    //        if (_days <= _leaveDayRemain)
                    //        {
                    //            _leaveDayRemain = _leaveDayRemain - Convert.ToInt32(_days);

                    //            _cutRemain = Convert.ToInt32(_days);
                    //            _cutCurrent = 0;
                    //            _empLeaveDay.LeaveDayRemain = Convert.ToByte(_leaveDayRemain);
                    //        }
                    //        else if (_days <= (_leaveDayRemain + _leaveCurrent))
                    //        {
                    //            _leaveCurrent = _leaveCurrent - (Convert.ToInt32(_days) - _leaveDayRemain);

                    //            _cutRemain = _leaveDayRemain;
                    //            _cutCurrent = Convert.ToInt32(_days) - _leaveDayRemain;

                    //            _leaveDayRemain = 0;
                    //            _empLeaveDay.LeaveDayRemain = Convert.ToByte(_leaveDayRemain);
                    //            _empLeaveDay.LeaveCurrent = Convert.ToByte(_leaveCurrent);
                    //        }
                    //    }
                    //}
                    //else if (_startDate <= _queryEmpLeaveDay.ExpiredDateLeaveCurrent || _queryEmpLeaveDay.ExpiredDateLeaveCurrent == null)
                    //{
                    //    if (_queryAllowed != null)
                    //    {
                    //        if (_days > _maxDaysAllowed - _totalAlreadyCuttedDays || _days > _leaveCurrent)
                    //        {
                    //            if (_maxDaysAllowed - _totalAlreadyCuttedDays < _leaveCurrent)
                    //            {
                    //                _validasi = "Approve Failed... Employee only have " + (_maxDaysAllowed - _totalAlreadyCuttedDays).ToString() + " Leave day(s) left for this period";
                    //            }
                    //            else
                    //            {
                    //                _validasi = "Approve Failed... Employee only have " + _leaveCurrent.ToString() + " Leave day(s) left for this period";
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (_days > _leaveCurrent)
                    //        {
                    //            _validasi = "Approve Failed... Employee only have " + _leaveCurrent.ToString() + " Leave day(s) left for this period";
                    //        }
                    //    }

                    //    if (_validasi == "")
                    //    {
                    //        if (_days <= _leaveCurrent)
                    //        {
                    //            _leaveCurrent = _leaveCurrent - Convert.ToInt32(_days);

                    //            _cutRemain = 0;
                    //            _cutCurrent = Convert.ToInt32(_days);
                    //            _empLeaveDay.LeaveCurrent = Convert.ToByte(_leaveCurrent);
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    _validasi = "Approve Failed... Employee does not have enough Leave day(s) left in this period";
                    //}
                    ////}

                    //if (_validasi == "")
                    //{
                    this.db.spHRM_AbsenceRequestApprove(new Guid(_prmCode), _prmuser, ref _result);
                    //}
                    //else
                    //{
                    //    _result = _validasi;
                    //}

                    if (_result == "")
                    {
                        HRM_AbsenceRequest _absenceRequest = this.GetSingleAbsenceRequest(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_absenceRequest.TransDate.Year, _absenceRequest.TransDate.Month, AppModule.GetValue(TransactionType.HRMAbsenceRequest), this._companyTag, ""))
                        {
                            _absenceRequest.FileNmbr = item.Number;
                        }
                        //_absenceRequest.CutRemain = _cutRemain;
                        //_absenceRequest.CutCurrent = _cutCurrent;

                        //int _count = ((TimeSpan)(_absenceRequest.EndDate - _absenceRequest.StartDate)).Days + 1;
                        //DateTime _dateAtt = _absenceRequest.StartDate;

                        //for (int i = 0; i < _count; i++)
                        //{
                        //    HRM_Attendance _attendance = new HRM_Attendance();
                        //    _attendance.AttendanceCode = Guid.NewGuid();
                        //    _attendance.EmpNumb = _absenceRequest.EmpNumb;
                        //    _attendance.AbsenceTypeCode = _absenceRequest.AbsenceTypeCode;
                        //    _attendance.AttendanceDate = _dateAtt;
                        //    _attendance.Remark = _absenceRequest.Remark;
                        //    _attendance.InsertBy = _prmuser;
                        //    _attendance.InsertDate = DateTime.Now;
                        //    _attendance.EditBy = _prmuser;
                        //    _attendance.EditDate = DateTime.Now;
                        //    this.db.HRM_Attendances.InsertOnSubmit(_attendance);

                        //    HRM_AbsenceRequest_Attendance _absReq_Att = new HRM_AbsenceRequest_Attendance();
                        //    _absReq_Att.AbsenceRequestCode = _absenceRequest.AbsenceRequestCode;
                        //    _absReq_Att.AttendanceCode = _attendance.AttendanceCode;
                        //    this.db.HRM_AbsenceRequest_Attendances.InsertOnSubmit(_absReq_Att);

                        //    _dateAtt = _dateAtt.AddDays(1);
                        //}
                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.HRMAbsenceRequest);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = _absenceRequest.FileNmbr;
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
            List<string> _attCode = new List<string>();

            try
            {
                //using (TransactionScope _scope = new TransactionScope())
                //{
                this.db.spHRM_AbsenceRequestPosting(new Guid(_prmCode), _prmuser, ref _result);

                //var _query = (
                //               from _mapper in this.db.HRM_AbsenceRequest_Attendances
                //               where _mapper.AbsenceRequestCode == new Guid(_prmCode)
                //               select _mapper
                //             );

                //foreach (var _item in _query)
                //{
                //    _attCode.Add(_item.AttendanceCode.ToString());
                //}

                //this.db.HRM_AbsenceRequest_Attendances.DeleteAllOnSubmit(_query);

                //for (int i = 0; i < _attCode.Count; i++)
                //{
                //    var _query2 = (
                //                       from _att in this.db.HRM_Attendances
                //                       where _att.AttendanceCode == new Guid(_attCode[i])
                //                       select _att
                //                     ).FirstOrDefault();

                //    this.db.HRM_Attendances.DeleteOnSubmit(_query2);
                //}

                //this.db.SubmitChanges();

                //_scope.Complete();

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMAbsenceRequest);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = this.GetSingleAbsenceRequest(_prmCode).FileNmbr;
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Posting);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();

                    _result = "Posting Success";
                }
                //}
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
            List<string> _attCode = new List<string>();

            try
            {
                this.db.spHRM_AbsenceRequestUnPost(new Guid(_prmCode), _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "UnPosting Success";
                }
                //}
            }
            catch (Exception ex)
            {
                _result = "UnPosting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }
        #endregion

        #region HRM.AbsenceRequestActing
        public double RowsCountAbsenceRequestActing(string _prmCode)
        {
            double _result = 0;

            var _query =
             (
                from _absenceRequestActing in this.db.HRM_AbsenceRequestActings
                where _absenceRequestActing.AbsenceRequestCode == new Guid(_prmCode)
                select _absenceRequestActing.AbsenceRequestCode
             ).Count();

            _result = _query;

            return _result;
        }

        public List<HRM_AbsenceRequestActing> GetListAbsenceRequestActing(string _prmCode)
        {
            List<HRM_AbsenceRequestActing> _result = new List<HRM_AbsenceRequestActing>();

            try
            {
                var _query = (
                                from _absenceRequestActing in this.db.HRM_AbsenceRequestActings
                                where _absenceRequestActing.AbsenceRequestCode == new Guid(_prmCode)
                                orderby _absenceRequestActing.EditDate descending
                                select new
                                {
                                    AbsenceRequestCode = _absenceRequestActing.AbsenceRequestCode,
                                    EmpNumb = _absenceRequestActing.EmpNumb,
                                    EmpName = (
                                                from _emp in this.db.MsEmployees
                                                where _emp.EmpNumb == _absenceRequestActing.EmpNumb
                                                select _emp.EmpName
                                              ).FirstOrDefault(),
                                    StartDate = _absenceRequestActing.StartDate,
                                    EndDate = _absenceRequestActing.EndDate,
                                    Remark = _absenceRequestActing.Remark
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_AbsenceRequestActing(_row.AbsenceRequestCode, _row.EmpNumb, _row.EmpName, _row.StartDate, _row.EndDate, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_AbsenceRequestActing GetSingleAbsenceRequestActing(string _prmCode, string _prmEmp)
        {
            HRM_AbsenceRequestActing _result = null;

            try
            {
                _result = this.db.HRM_AbsenceRequestActings.Single(_temp => _temp.AbsenceRequestCode == new Guid(_prmCode) && _temp.EmpNumb == _prmEmp);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiAbsenceRequestActing(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _code = _prmCode[i].Split('=');

                    HRM_AbsenceRequestActing _absenceRequestActing = this.db.HRM_AbsenceRequestActings.Single(_temp => _temp.AbsenceRequestCode == new Guid(_code[0]) && _temp.EmpNumb == _code[1]);
                    this.db.HRM_AbsenceRequestActings.DeleteOnSubmit(_absenceRequestActing);
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

        public bool AddAbsenceRequestActing(HRM_AbsenceRequestActing _prmAbsenceRequestActing)
        {
            bool _result = false;

            try
            {
                this.db.HRM_AbsenceRequestActings.InsertOnSubmit(_prmAbsenceRequestActing);
                this.db.SubmitChanges();

                _result = true;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditAbsenceRequestActing(HRM_AbsenceRequestActing _prmAbsenceRequestActing)
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

        public string GetFileNmbrByCode(Guid _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _absenceRequest in this.db.HRM_AbsenceRequests
                                where _absenceRequest.AbsenceRequestCode == _prmCode
                                select new
                                {
                                    FileNmbr = _absenceRequest.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result = _row.FileNmbr;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        ~AbsenceRequestBL()
        {
        }
    }
}
