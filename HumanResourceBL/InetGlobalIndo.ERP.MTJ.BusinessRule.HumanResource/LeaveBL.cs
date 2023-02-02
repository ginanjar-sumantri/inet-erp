using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Web;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.IO;
using System.Drawing;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class LeaveBL : Base
    {
        public LeaveBL()
        {

        }

        #region Leave
        public double RowsCountLeave(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                (
                    from _msLeave in this.db.HRMMsLeaves
                    where (SqlMethods.Like(_msLeave.LeavesCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msLeave.LeavesName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                    select _msLeave.LeavesCode
                ).Count();

            _result = _query;
            return _result;
        }

        public HRMMsLeave GetSingleLeave(string _prmLeaveCode)
        {
            HRMMsLeave _result = null;

            try
            {
                _result = this.db.HRMMsLeaves.Single(_temp => _temp.LeavesCode == _prmLeaveCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetLeaveNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msLeave in this.db.HRMMsLeaves
                                where _msLeave.LeavesCode == _prmCode
                                select new
                                {
                                    LeavesName = _msLeave.LeavesName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.LeavesName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsLeave> GetListLeave(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMMsLeave> _result = new List<HRMMsLeave>();
            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }
            try
            {
                var _query = (
                                from _msLeave in this.db.HRMMsLeaves
                                where (SqlMethods.Like(_msLeave.LeavesCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && (SqlMethods.Like(_msLeave.LeavesName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msLeave.EditDate descending
                                select new
                                {
                                    LeavesCode = _msLeave.LeavesCode,
                                    LeavesName = _msLeave.LeavesName,
                                    MaxTakenDays = _msLeave.MaxTakenDays,
                                    LeaveDays = _msLeave.LeaveDays,
                                    fgIncludeHoliday = _msLeave.fgIncludeHoliday
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsLeave(_row.LeavesCode, _row.LeavesName, _row.MaxTakenDays, _row.LeaveDays, _row.fgIncludeHoliday));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsLeave> GetListLeaveForDDL()
        {
            List<HRMMsLeave> _result = new List<HRMMsLeave>();

            try
            {
                var _query = (
                                from _msLeave in this.db.HRMMsLeaves
                                orderby _msLeave.LeavesName ascending
                                select new
                                {
                                    LeavesCode = _msLeave.LeavesCode,
                                    LeavesName = _msLeave.LeavesName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsLeave(_row.LeavesCode, _row.LeavesName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiLeave(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMMsLeave _msLeave = this.db.HRMMsLeaves.Single(_temp => _temp.LeavesCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.HRMMsLeaves.DeleteOnSubmit(_msLeave);
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

        public bool AddLeave(HRMMsLeave _prmMsLeave)
        {
            bool _result = false;

            try
            {
                this.db.HRMMsLeaves.InsertOnSubmit(_prmMsLeave);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditLeave(HRMMsLeave _prmMsLeave)
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

        public string GetLeaveRequest(DateTime _prmStartDate, DateTime _prmEndDate, String _prmLeaveCode, String _prmEmpNumb)
        {
            string _result = "";

            try
            {
                int? _days = 0;
                int? _holidays = 0;
                int? _dispensasi = 0;
                int? _balance = 0;
                int? _taken = 0;
                DateTime? _lastTaken = null;

                this.db.spHRM_GetInfoLeaveRequest(_prmStartDate, _prmEndDate, _prmLeaveCode, _prmEmpNumb, ref _days, ref _holidays, ref _dispensasi, ref _balance, ref _taken, ref _lastTaken);

                _result = _days.ToString() + "=" + _holidays.ToString() + "=" + _dispensasi.ToString() + "=" + _balance.ToString() + "=" + _taken.ToString() + "=" + DateFormMapper.GetValue(_lastTaken);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrLeavePosting GetListLeavePosting(String _prmEmpNumb, DateTime _prmStartDate, DateTime _prmEndDate)
        {
            HRMTrLeavePosting _result = new HRMTrLeavePosting();

            try
            {
                var _query = (
                                from _trLeavePosting in this.db.HRMTrLeavePostings
                                where _trLeavePosting.EmployeeId == _prmEmpNumb && _trLeavePosting.StartDate <= _prmStartDate && _trLeavePosting.EndDate >= _prmEndDate
                                orderby _trLeavePosting.StartDate descending
                                select new
                                {
                                    StartDate = _trLeavePosting.StartDate,
                                    EndDate = _trLeavePosting.EndDate,
                                    LeaveIn = _trLeavePosting.LeaveIn,
                                    LeaveOut = _trLeavePosting.LeaveOut,
                                    MassLeaveIn = _trLeavePosting.MassLeaveIn,
                                    MassLeaveOut = _trLeavePosting.MassLeaveOut
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.EndDate = _row.EndDate;
                    _result.StartDate = _row.StartDate;
                    _result.LeaveIn = _row.LeaveIn;
                    _result.LeaveOut = _row.LeaveOut;
                    _result.MassLeaveIn = _row.MassLeaveIn;
                    _result.MassLeaveOut = _row.MassLeaveOut;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        ~LeaveBL()
        {

        }
    }
}
