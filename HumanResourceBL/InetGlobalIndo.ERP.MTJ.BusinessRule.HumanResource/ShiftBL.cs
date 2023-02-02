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
    public sealed class ShiftBL : Base
    {
        public ShiftBL()
        {

        }

        #region Shift
        public double RowsCountShift(string _prmCategory, string _prmKeyword)
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
                    from _msShift in this.db.HRMMsShifts
                    where (SqlMethods.Like(_msShift.ShiftCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msShift.ShiftName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                    select _msShift.ShiftCode

                ).Count();

            _result = _query;
            return _result;
        }

        public HRMMsShift GetSingleShift(string _prmShiftCode)
        {
            HRMMsShift _result = null;

            try
            {
                _result = this.db.HRMMsShifts.Single(_temp => _temp.ShiftCode == _prmShiftCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetShiftNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msShift in this.db.HRMMsShifts
                                where _msShift.ShiftCode == _prmCode
                                select new
                                {
                                    ShiftName = _msShift.ShiftName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.ShiftName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsShift> GetListShift(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMMsShift> _result = new List<HRMMsShift>();
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
                                from _msShift in this.db.HRMMsShifts
                                where (SqlMethods.Like(_msShift.ShiftCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && (SqlMethods.Like(_msShift.ShiftName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msShift.EditDate descending
                                select new
                                {
                                    ShiftCode = _msShift.ShiftCode,
                                    ShiftName = _msShift.ShiftName,
                                    ShiftIn = _msShift.ShiftIn,
                                    ShiftOut = _msShift.ShiftOut,
                                    BreakIn = _msShift.BreakIn,
                                    BreakOut = _msShift.BreakOut,
                                    BreakMinute = _msShift.BreakMinute,
                                    FgActive = _msShift.FgActive
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsShift(_row.ShiftCode, _row.ShiftName, _row.ShiftIn, _row.ShiftOut, _row.BreakIn, _row.BreakOut, _row.BreakMinute, _row.FgActive));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsShift> GetListShiftForDDL()
        {
            List<HRMMsShift> _result = new List<HRMMsShift>();

            try
            {
                var _query = (
                                from _msShift in this.db.HRMMsShifts
                                where _msShift.FgActive == true
                                orderby _msShift.ShiftName ascending
                                select new
                                {
                                    ShiftCode = _msShift.ShiftCode,
                                    ShiftName = _msShift.ShiftName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsShift(_row.ShiftCode, _row.ShiftName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiShift(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMMsShift _msShift = this.db.HRMMsShifts.Single(_temp => _temp.ShiftCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.HRMMsShifts.DeleteOnSubmit(_msShift);
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

        public bool AddShift(HRMMsShift _prmMsShift)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsShiftName(_prmMsShift.ShiftName, _prmMsShift.ShiftCode) == false)
                {
                    this.db.HRMMsShifts.InsertOnSubmit(_prmMsShift);
                    this.db.SubmitChanges();

                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditShift(HRMMsShift _prmMsShift)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsShiftName(_prmMsShift.ShiftName, _prmMsShift.ShiftCode) == false)
                {
                    this.db.SubmitChanges();

                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        private bool IsExistsShiftName(String _prmShiftName, String _prmShiftCode)
        {
            bool _result = false;

            try
            {
                var _query = from _hrmMsShift in this.db.HRMMsShifts
                             where _hrmMsShift.ShiftName == _prmShiftName && _hrmMsShift.ShiftCode != _prmShiftCode
                             select new
                             {
                                 _hrmMsShift.ShiftCode
                             };

                if (_query.Count() > 0)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        #region ShiftDt
        public double RowsCountShiftDt(string _prmCode)
        {
            double _result = 0;

            var _query =
                         (
                            from _msShiftDt in this.db.HRMMsShiftDts
                            where _msShiftDt.ShiftCode == _prmCode
                            select _msShiftDt
                         ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMMsShiftDt> GetListShiftDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRMMsShiftDt> _result = new List<HRMMsShiftDt>();

            try
            {
                var _query = (
                                from _msShiftDt in this.db.HRMMsShiftDts
                                where _msShiftDt.ShiftCode == _prmCode
                                select new
                                {
                                    ShiftCode = _msShiftDt.ShiftCode,
                                    DayCode = _msShiftDt.DayCode,
                                    DayName = (
                                                from _day in this.db.HRMMsDays
                                                where _day.DayCode == _msShiftDt.DayCode
                                                select _day.DayName
                                              ).FirstOrDefault(),
                                    ShiftIn = _msShiftDt.ShiftIn,
                                    ShiftOut = _msShiftDt.ShiftOut,
                                    BreakIn = _msShiftDt.BreakIn,
                                    BreakOut = _msShiftDt.BreakOut,
                                    BreakMinute = _msShiftDt.BreakMinute
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsShiftDt(_row.ShiftCode, _row.DayCode, _row.DayName, _row.ShiftIn, _row.ShiftOut, _row.BreakIn, _row.BreakOut, _row.BreakMinute));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsShiftDt> GetListShiftDt(string _prmCode)
        {
            List<HRMMsShiftDt> _result = new List<HRMMsShiftDt>();

            try
            {
                var _query = (
                                from _msShiftDt in this.db.HRMMsShiftDts
                                where _msShiftDt.ShiftCode == _prmCode
                                select new
                                {
                                    ShiftCode = _msShiftDt.ShiftCode,
                                    DayCode = _msShiftDt.DayCode,
                                    DayName = (
                                                from _day in this.db.HRMMsDays
                                                where _day.DayCode == _msShiftDt.DayCode
                                                select _day.DayName
                                              ).FirstOrDefault(),
                                    ShiftIn = _msShiftDt.ShiftIn,
                                    ShiftOut = _msShiftDt.ShiftOut,
                                    BreakIn = _msShiftDt.BreakIn,
                                    BreakOut = _msShiftDt.BreakOut,
                                    BreakMinute = _msShiftDt.BreakMinute
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsShiftDt(_row.ShiftCode, _row.DayCode, _row.DayName, _row.ShiftIn, _row.ShiftOut, _row.BreakIn, _row.BreakOut, _row.BreakMinute));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMMsShiftDt GetSingleShiftDt(string _prmShiftCode, int _prmDayCode)
        {
            HRMMsShiftDt _result = null;

            try
            {
                _result = this.db.HRMMsShiftDts.Single(_temp => _temp.ShiftCode == _prmShiftCode && _temp.DayCode == _prmDayCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiShiftDt(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _code = _prmCode[i].Split('=');

                    HRMMsShiftDt _msShiftDt = this.db.HRMMsShiftDts.Single(_temp => _temp.ShiftCode == _code[0] && _temp.DayCode == Convert.ToInt32(_code[1]));
                    this.db.HRMMsShiftDts.DeleteOnSubmit(_msShiftDt);
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

        public bool AddShiftDt(HRMMsShiftDt _prmShiftDt)
        {
            bool _result = false;

            try
            {
                this.db.HRMMsShiftDts.InsertOnSubmit(_prmShiftDt);
                this.db.SubmitChanges();

                _result = true;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditShiftDt(HRMMsShiftDt _prmShiftDt)
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

        ~ShiftBL()
        {

        }
    }
}
