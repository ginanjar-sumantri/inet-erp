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
    public sealed class PeriodScheduleBL : Base
    {
        public PeriodScheduleBL()
        {

        }

        #region PeriodSchedule
        public double RowsCountPeriodSchedule(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            if (_prmCategory == "Year")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "Month")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            try
            {
                var _query =
                            (
                                from _msPeriodSchedule in this.db.HRMMsPeriodSchedules
                                where (SqlMethods.Like(_msPeriodSchedule.PeriodeCode.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msPeriodSchedule.PeriodYear.ToString().Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like(_msPeriodSchedule.PeriodMonth.ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                select _msPeriodSchedule.PeriodeCode
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        public HRMMsPeriodSchedule GetSinglePeriodSchedule(String _prmPeriodCode)
        {
            HRMMsPeriodSchedule _result = null;

            try
            {
                _result = this.db.HRMMsPeriodSchedules.Single(_temp => _temp.PeriodeCode == _prmPeriodCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsPeriodSchedule> GetListPeriodSchedule(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMMsPeriodSchedule> _result = new List<HRMMsPeriodSchedule>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            if (_prmCategory == "Year")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "Month")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            try
            {
                var _query = (
                                from _hrmMsPeriodSchedule in this.db.HRMMsPeriodSchedules
                                where (SqlMethods.Like(_hrmMsPeriodSchedule.PeriodeCode.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_hrmMsPeriodSchedule.PeriodYear.ToString().Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like(_hrmMsPeriodSchedule.PeriodMonth.ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                orderby _hrmMsPeriodSchedule.PeriodeCode ascending
                                select new
                                {
                                    PeriodeCode = _hrmMsPeriodSchedule.PeriodeCode,
                                    PeriodYear = _hrmMsPeriodSchedule.PeriodYear,
                                    PeriodMonth = _hrmMsPeriodSchedule.PeriodMonth,
                                    StartDate = _hrmMsPeriodSchedule.StartDate,
                                    EndDate = _hrmMsPeriodSchedule.EndDate
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsPeriodSchedule(_row.PeriodeCode, _row.PeriodYear, _row.PeriodMonth, _row.StartDate, _row.EndDate));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsPeriodSchedule> GetListPeriodScheduleForDDL()
        {
            List<HRMMsPeriodSchedule> _result = new List<HRMMsPeriodSchedule>();

            try
            {
                var _query = (
                                from _hrmMsPeriodSchedule in this.db.HRMMsPeriodSchedules
                                orderby _hrmMsPeriodSchedule.PeriodeCode ascending
                                select new
                                {
                                    PeriodeCode = _hrmMsPeriodSchedule.PeriodeCode
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsPeriodSchedule(_row.PeriodeCode));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditPeriodSchedule(HRMMsPeriodSchedule _prmHRMMsPeriodSchedule)
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

        public bool AddPeriodSchedule(HRMMsPeriodSchedule _prmHRMMsPeriodSchedule)
        {
            bool _result = false;

            try
            {
                    this.db.HRMMsPeriodSchedules.InsertOnSubmit(_prmHRMMsPeriodSchedule);
                    this.db.SubmitChanges();

                    _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiPeriodSchedule(string[] _prmPeriodeCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmPeriodeCode.Length; i++)
                {
                    HRMMsPeriodSchedule _HRMMsPeriodSchedule = this.db.HRMMsPeriodSchedules.Single(_temp => _temp.PeriodeCode == _prmPeriodeCode[i]);

                    this.db.HRMMsPeriodSchedules.DeleteOnSubmit(_HRMMsPeriodSchedule);
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

        ~PeriodScheduleBL()
        {

        }
    }
}
