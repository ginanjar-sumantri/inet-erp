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
    public sealed class AttendanceBL : Base
    {
        public AttendanceBL()
        {

        }

        #region HRMTrAttendanceClock
        public List<HRMTrAttendanceClock> GetListHRMTrAttendanceClock()
        {
            List<HRMTrAttendanceClock> _result = new List<HRMTrAttendanceClock>();

            try
            {
                var _query = (
                                from _attendance in this.db.HRMTrAttendanceClocks
                                orderby _attendance.EditDate descending
                                select new
                                {
                                    EmpNumb = _attendance.EmpNumb,
                                    TransDate = _attendance.TransDate
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrAttendanceClock(_row.EmpNumb, _row.TransDate));
                }
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "DeleteMultiHRMTrAttendanceDaily", "HRM");
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public double RowsCountHRMTrAttendanceClock(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "EmpCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "EmpName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query =
                            (
                               from _attendance in this.db.HRMTrAttendanceClocks
                               join _msEmployee in this.db.MsEmployees
                                   on _attendance.EmpNumb equals _msEmployee.EmpNumb
                               where (SqlMethods.Like(_attendance.EmpNumb.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msEmployee.EmpName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               orderby _attendance.EmpNumb descending
                               select _attendance
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "DeleteMultiHRMTrAttendanceDaily", "HRM");
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrAttendanceClock> GetListHRMTrAttendanceClock(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMTrAttendanceClock> _result = new List<HRMTrAttendanceClock>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";


            if (_prmCategory == "EmpCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "EmpName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _attendance in this.db.HRMTrAttendanceClocks
                                join _msEmployee in this.db.MsEmployees
                                    on _attendance.EmpNumb equals _msEmployee.EmpNumb
                                where (SqlMethods.Like(_attendance.EmpNumb.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msEmployee.EmpName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _attendance.EmpNumb ascending
                                select new
                                {
                                    EmpNumb = _attendance.EmpNumb,
                                    EmpName = _msEmployee.EmpName,
                                    TransDate = _attendance.TransDate,
                                    Operation = _attendance.Operation,
                                    TransStatus = _attendance.TransStatus
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrAttendanceClock(_row.EmpNumb, _row.EmpName, _row.TransDate, _row.Operation, _row.TransStatus));
                }
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "DeleteMultiHRMTrAttendanceDaily", "HRM");
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrAttendanceClock GetSingleHRMTrAttendanceClock(String _prmEmpNumb, DateTime _prmTransDate, char _prmOperation)
        {
            HRMTrAttendanceClock _result = null;


            try
            {

                //List<string> strings = db.HRMTrAttendanceClocks.Select(t => t.TransDate).ToList().Select(d => d.ToString("MM/dd/yyyy")).ToList();

                //foreach (string _row in strings)
                //{
                //    if (_row == _prmTransDate)
                //    {
                //        String _pattern = "%" + _prmTransDate + "%";

                        DateTime _date = (
                                            from _clock in this.db.HRMTrAttendanceClocks
                                            where _clock.TransDate >= _prmTransDate
                                                && _clock.TransDate <= _prmTransDate.AddDays(1)
                                                && _clock.Operation == _prmOperation
                                            select _clock.TransDate
                                          ).FirstOrDefault();

                        _result = (
                                        from _attendance in this.db.HRMTrAttendanceClocks
                                        where _attendance.EmpNumb == _prmEmpNumb
                                              && _attendance.TransDate == _date
                                              && _attendance.Operation == _prmOperation
                                        select _attendance
                                  ).Single();
                //    }
                //}
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "DeleteMultiHRMTrAttendanceDaily", "HRM");
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String IsTransDateExists(String _prmTransDate)
        {
            String _result = "";

            try
            {
                _result = this.db.HRMTrAttendanceClocks.Single(_temp => _temp.TransDate.ToShortDateString() == _prmTransDate).TransDate.ToShortDateString();
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "DeleteMultiHRMTrAttendanceDaily", "HRM");
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Boolean AddHRMTrAttendance(List<HRMTrAttendanceClock> _prmListHRMTrAttendanceClock)
        {
            Boolean _result = false;

            try
            {
                List<HRMTrAttendanceClock> _detail = new List<HRMTrAttendanceClock>();

                foreach (HRMTrAttendanceClock _row in _prmListHRMTrAttendanceClock)
                {
                    HRMTrAttendanceClock _attendanceClock = new HRMTrAttendanceClock();

                    _attendanceClock.EmpNumb = _row.EmpNumb;
                    _attendanceClock.TransDate = _row.TransDate;
                    _attendanceClock.Operation = _row.Operation;
                    _attendanceClock.TransStatus = _row.TransStatus;
                    _attendanceClock.CreatedBy = HttpContext.Current.User.Identity.Name;
                    _attendanceClock.CreatedDate = DateTime.Now;
                    _attendanceClock.EditBy = HttpContext.Current.User.Identity.Name;
                    _attendanceClock.EditDate = DateTime.Now;

                    _detail.Add(_attendanceClock);
                }

                this.db.HRMTrAttendanceClocks.InsertAllOnSubmit(_detail);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "DeleteMultiHRMTrAttendanceDaily", "HRM");
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string ImportToAttendanceDaily(string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.spHRM_ImportToAttendanceDaily(_prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Import Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Import Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public List<HRMTrAttendanceClock> GetListAdvancedSearch(DateTime _prmDateFrom, DateTime _prmDateTo, String _prmFgFilter, String _prmEmpNumb, String _prmEmpFrom, String _prmEmpTo)
        {
            List<HRMTrAttendanceClock> _result = new List<HRMTrAttendanceClock>();

            try
            {
                if (_prmFgFilter == "0")
                {
                    var _query = (
                                   from _attendance in this.db.HRMTrAttendanceClocks
                                   join _msEmployee in this.db.MsEmployees
                                       on _attendance.EmpNumb equals _msEmployee.EmpNumb
                                   where (_attendance.EmpNumb.CompareTo(_prmEmpFrom) >= 0 && _attendance.EmpNumb.CompareTo(_prmEmpTo) <= 0)
                                        && _attendance.TransDate >= _prmDateFrom && _attendance.TransDate <= _prmDateTo
                                   orderby _attendance.EmpNumb ascending
                                   select new
                                   {
                                       EmpNumb = _attendance.EmpNumb,
                                       EmpName = _msEmployee.EmpName,
                                       TransDate = _attendance.TransDate,
                                       Operation = _attendance.Operation,
                                       TransStatus = _attendance.TransStatus
                                   }
                                );

                    foreach (var _row in _query)
                    {
                        _result.Add(new HRMTrAttendanceClock(_row.EmpNumb, _row.EmpName, _row.TransDate, _row.Operation, _row.TransStatus));
                    }
                }
                else if (_prmFgFilter == "1")
                {
                    var _query = (
                                   from _attendance in this.db.HRMTrAttendanceClocks
                                   join _msEmployee in this.db.MsEmployees
                                       on _attendance.EmpNumb equals _msEmployee.EmpNumb
                                   where _attendance.EmpNumb.Contains(_prmEmpNumb)
                                        && _attendance.TransDate >= _prmDateFrom
                                        && _attendance.TransDate <= _prmDateTo
                                   orderby _attendance.EmpNumb ascending
                                   select new
                                   {
                                       EmpNumb = _attendance.EmpNumb,
                                       EmpName = _msEmployee.EmpName,
                                       TransDate = _attendance.TransDate,
                                       Operation = _attendance.Operation,
                                       TransStatus = _attendance.TransStatus
                                   }
                                );

                    foreach (var _row in _query)
                    {
                        _result.Add(new HRMTrAttendanceClock(_row.EmpNumb, _row.EmpName, _row.TransDate, _row.Operation, _row.TransStatus));
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "DeleteMultiHRMTrAttendanceDaily", "HRM");
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        #region HRMTrAttendanceDaily
        public double RowsCountHRMTrAttendanceDaily(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            if (_prmCategory == "EmpCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "EmpName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "AbsenceTypeName")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "ShiftName")
            {
                _pattern4 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }

            try
            {
                var _query =
                            (
                               from _attendance in this.db.HRMTrAttendanceDailies
                               join _msEmployee in this.db.MsEmployees
                                    on _attendance.EmpNumb equals _msEmployee.EmpNumb
                               join _msShift in this.db.HRMMsShifts
                                    on _attendance.ShiftCode equals _msShift.ShiftCode
                               where (SqlMethods.Like(_attendance.EmpNumb.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                  && (SqlMethods.Like(_msEmployee.EmpName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                  && (SqlMethods.Like(_attendance.AbsenceTypeCode.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                  && (SqlMethods.Like(_msShift.ShiftName.Trim().ToLower(), _pattern4.Trim().ToLower()))
                               select _attendance
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "DeleteMultiHRMTrAttendanceDaily", "HRM");
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrAttendanceDaily> GetListHRMTrAttendanceDaily(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMTrAttendanceDaily> _result = new List<HRMTrAttendanceDaily>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            if (_prmCategory == "EmpCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "EmpName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "AbsenceTypeName")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "ShiftName")
            {
                _pattern4 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }

            try
            {
                var _query = (
                                from _attendance in this.db.HRMTrAttendanceDailies
                                join _msEmployee in this.db.MsEmployees
                                    on _attendance.EmpNumb equals _msEmployee.EmpNumb
                                join _msShift in this.db.HRMMsShifts
                                     on _attendance.ShiftCode equals _msShift.ShiftCode
                                where (SqlMethods.Like(_attendance.EmpNumb.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msEmployee.EmpName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (SqlMethods.Like(_attendance.AbsenceTypeCode.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                    && (SqlMethods.Like(_msShift.ShiftName.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                orderby _attendance.TransDate descending
                                select new
                                {
                                    EmpNumb = _attendance.EmpNumb,
                                    EmpName = _msEmployee.EmpName,
                                    TransDate = _attendance.TransDate,
                                    Status = _attendance.Status,
                                    ShiftCode = _attendance.ShiftCode,
                                    ShiftName = _msShift.ShiftName,
                                    AbsenceTypeCode = _attendance.AbsenceTypeCode,
                                    AbsenceTypeName = ( from _master_AbsenceType in this.db.HRMMsAbsenceTypes
                                                        where _attendance.AbsenceTypeCode == _master_AbsenceType.AbsenceTypeCode
                                                        select _master_AbsenceType.AbsenceTypeName
                                                       ).FirstOrDefault(),
                                    ClockIn = _attendance.ClockIn,
                                    ClockOut = _attendance.ClockOut
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrAttendanceDaily(_row.EmpNumb, _row.EmpName, _row.TransDate, _row.Status, _row.ShiftCode, _row.ShiftName, _row.AbsenceTypeCode, _row.AbsenceTypeName, _row.ClockIn, _row.ClockOut));
                }
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "DeleteMultiHRMTrAttendanceDaily", "HRM");
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrAttendanceDaily GetSingleHRMTrAttendanceDaily(String _prmEmpNumb, DateTime _prmTransDate)
        {
            HRMTrAttendanceDaily _result = null;

            try
            {
                _result = this.db.HRMTrAttendanceDailies.Single(_temp => _temp.EmpNumb == _prmEmpNumb && _temp.TransDate == _prmTransDate);
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "DeleteMultiHRMTrAttendanceDaily", "HRM");
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Boolean IsEmpNumbAndTransDateExists(String _prmEmpNumb, DateTime _prmTransDate)
        {
            Boolean _result = false;

            try
            {
                var _query = (
                                from _attendanceDaily in this.db.HRMTrAttendanceDailies
                                where _attendanceDaily.EmpNumb == _prmEmpNumb && _attendanceDaily.TransDate == _prmTransDate
                                select _attendanceDaily
                            ).Count();

                if (_query > 0)
                {
                    _result = false;
                }
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "DeleteMultiHRMTrAttendanceDaily", "HRM");
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiHRMTrAttendanceDaily(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    String[] _tempSplit = _prmCode[i].Split('=');

                    HRMTrAttendanceDaily _hrmAttendanceDaily = this.db.HRMTrAttendanceDailies.Single(_temp => _temp.EmpNumb == _tempSplit[0] && _temp.TransDate == DateFormMapper.GetValue(_tempSplit[1]));

                    this.db.HRMTrAttendanceDailies.DeleteOnSubmit(_hrmAttendanceDaily);

                    _result = true;
                }

                if (_result == true)
                    this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "DeleteMultiHRMTrAttendanceDaily", "HRM");
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Boolean AddHRMTrAttendanceDaily(HRMTrAttendanceDaily _prmHRMTrAttendanceDaily)
        {
            Boolean _result = false;

            try
            {
                this.db.HRMTrAttendanceDailies.InsertOnSubmit(_prmHRMTrAttendanceDaily);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "DeleteMultiHRMTrAttendanceDaily", "HRM");
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditHRMTrAttendanceDaily(HRMTrAttendanceDaily _prmHRMTrAttendanceDaily)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "DeleteMultiHRMTrAttendanceDaily", "HRM"); 
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~AttendanceBL()
        {
        }
    }
}
