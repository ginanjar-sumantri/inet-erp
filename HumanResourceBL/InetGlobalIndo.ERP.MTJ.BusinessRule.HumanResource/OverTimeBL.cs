using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Web;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class OverTimeBL : Base
    {
        public OverTimeBL()
        {

        }

        #region OverTime
        public double RowsCountOverTime(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";

            if (_prmCategory == "Hour")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query =
                            (
                                from _msOverTime in this.db.HRMMsOverTimes
                                where (SqlMethods.Like(_msOverTime.OvertimeHour.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                select _msOverTime.OvertimeHour
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        public HRMMsOverTime GetSingleOverTime(Decimal _prmOverTimeHour)
        {
            HRMMsOverTime _result = null;

            try
            {
                _result = this.db.HRMMsOverTimes.Single(_temp => _temp.OvertimeHour == _prmOverTimeHour);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsOverTime> GetListOverTime(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMMsOverTime> _result = new List<HRMMsOverTime>();

            string _pattern1 = "%%";

            if (_prmCategory == "Hour")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _hrmMsOverTime in this.db.HRMMsOverTimes
                                where (SqlMethods.Like(_hrmMsOverTime.OvertimeHour.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                orderby _hrmMsOverTime.OvertimeHour ascending
                                select new
                                {
                                    OvertimeHour = _hrmMsOverTime.OvertimeHour,
                                    WorkDay = _hrmMsOverTime.WorkDay,
                                    Holiday = _hrmMsOverTime.Holiday,
                                    PublicHoliday = _hrmMsOverTime.PublicHoliday
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsOverTime(_row.OvertimeHour, _row.WorkDay, _row.Holiday, _row.PublicHoliday));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditOverTime(HRMMsOverTime _prmHRMMsOverTime)
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

        public bool AddOverTime(HRMMsOverTime _prmHRMMsOverTime)
        {
            bool _result = false;

            try
            {
                this.db.HRMMsOverTimes.InsertOnSubmit(_prmHRMMsOverTime);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiOverTime(string[] _prmOverTimeCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmOverTimeCode.Length; i++)
                {
                    HRMMsOverTime _HRMMsOverTime = this.db.HRMMsOverTimes.Single(_temp => _temp.OvertimeHour == Convert.ToDecimal(_prmOverTimeCode[i]));

                    this.db.HRMMsOverTimes.DeleteOnSubmit(_HRMMsOverTime);
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
        #endregion

        #region OverTimeRoundSetup
        public double RowsCountOverTimeRoundSetup(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";

            if (_prmCategory == "Minute")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query =
                            (
                                from _msOverTimeRoundSetup in this.db.HRMMsOverTimeRoundSetups
                                where (SqlMethods.Like(_msOverTimeRoundSetup.StartMinute.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                select _msOverTimeRoundSetup.StartMinute
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        public HRMMsOverTimeRoundSetup GetSingleOverTimeRoundSetup(int _prmStartMinute)
        {
            HRMMsOverTimeRoundSetup _result = null;

            try
            {
                _result = this.db.HRMMsOverTimeRoundSetups.Single(_temp => _temp.StartMinute == _prmStartMinute);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Decimal GetOverTimeRoundingHour(int _prmMinute)
        {
            Decimal _result = 0;

            try
            {
                var _query = (
                                from _msOverTimeRoundSetup in this.db.HRMMsOverTimeRoundSetups
                                where _msOverTimeRoundSetup.StartMinute >= _prmMinute && _msOverTimeRoundSetup.EndMinute <= _prmMinute
                                select _msOverTimeRoundSetup.OTHour
                             ).FirstOrDefault();

                _result = Convert.ToDecimal(_query);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsOverTimeRoundSetup> GetListOverTimeRoundSetup(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMMsOverTimeRoundSetup> _result = new List<HRMMsOverTimeRoundSetup>();

            string _pattern1 = "%%";

            if (_prmCategory == "Minute")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _hrmMsOverTimeRoundSetup in this.db.HRMMsOverTimeRoundSetups
                                where (SqlMethods.Like(_hrmMsOverTimeRoundSetup.StartMinute.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                orderby _hrmMsOverTimeRoundSetup.StartMinute ascending
                                select new
                                {
                                    StartMinute = _hrmMsOverTimeRoundSetup.StartMinute,
                                    EndMinute = _hrmMsOverTimeRoundSetup.EndMinute,
                                    OTHour = _hrmMsOverTimeRoundSetup.OTHour
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsOverTimeRoundSetup(_row.StartMinute, _row.EndMinute, _row.OTHour));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditOverTimeRoundSetup(HRMMsOverTimeRoundSetup _prmHRMMsOverTimeRoundSetup)
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

        public bool AddOverTimeRoundSetup(HRMMsOverTimeRoundSetup _prmHRMMsOverTimeRoundSetup)
        {
            bool _result = false;

            try
            {
                this.db.HRMMsOverTimeRoundSetups.InsertOnSubmit(_prmHRMMsOverTimeRoundSetup);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiOverTimeRoundSetup(string[] _prmStartMinute)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmStartMinute.Length; i++)
                {
                    HRMMsOverTimeRoundSetup _hrmMsOverTimeRoundSetup = this.db.HRMMsOverTimeRoundSetups.Single(_temp => _temp.StartMinute == Convert.ToInt32(_prmStartMinute[i]));

                    this.db.HRMMsOverTimeRoundSetups.DeleteOnSubmit(_hrmMsOverTimeRoundSetup);
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
        #endregion

        ~OverTimeBL()
        {

        }
    }
}
