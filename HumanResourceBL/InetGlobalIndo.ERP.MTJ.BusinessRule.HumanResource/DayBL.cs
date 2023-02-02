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
    public sealed class DayBL : Base
    {
        public DayBL()
        {

        }

        #region Day
        public double RowsCountDay(string _prmCategory, string _prmKeyword)
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

            try
            {
                var _query =
                            (
                                from _msDay in this.db.HRMMsDays
                                where (SqlMethods.Like(_msDay.DayCode.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msDay.DayName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select _msDay.DayCode
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        public HRMMsDay GetSingleDay(int _prmDayCode)
        {
            HRMMsDay _result = null;

            try
            {
                _result = this.db.HRMMsDays.Single(_temp => _temp.DayCode == _prmDayCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetDayNameByCode(int _prmDayCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _hrmMsDay in this.db.HRMMsDays
                                where _hrmMsDay.DayCode == _prmDayCode
                                select new
                                {
                                    DayName = _hrmMsDay.DayName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.DayName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsDay> GetListDay(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMMsDay> _result = new List<HRMMsDay>();

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
                                from _hrmMsDay in this.db.HRMMsDays
                                where (SqlMethods.Like(_hrmMsDay.DayCode.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_hrmMsDay.DayName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _hrmMsDay.DayCode ascending
                                select new
                                {
                                    DayCode = _hrmMsDay.DayCode,
                                    DayName = _hrmMsDay.DayName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsDay(_row.DayCode, _row.DayName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsDay> GetListDayForDDL()
        {
            List<HRMMsDay> _result = new List<HRMMsDay>();

            try
            {
                var _query = (
                                from _hrmMsDay in this.db.HRMMsDays
                                orderby _hrmMsDay.DayCode ascending
                                select new
                                {
                                    DayCode = _hrmMsDay.DayCode,
                                    DayName = _hrmMsDay.DayName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsDay(_row.DayCode, _row.DayName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditDay(HRMMsDay _prmHRMMsDay)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsDayName(_prmHRMMsDay.DayName, _prmHRMMsDay.DayCode) == false)
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

        public bool AddDay(HRMMsDay _prmHRMMsDay)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsDayName(_prmHRMMsDay.DayName, _prmHRMMsDay.DayCode) == false)
                {
                    this.db.HRMMsDays.InsertOnSubmit(_prmHRMMsDay);
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

        private bool IsExistsDayName(String _prmDayName, int _prmDayCode)
        {
            bool _result = false;

            try
            {
                var _query = from _hrmMsDay in this.db.HRMMsDays
                             where _hrmMsDay.DayName == _prmDayName && _hrmMsDay.DayCode != _prmDayCode
                             select new
                             {
                                 _hrmMsDay.DayCode
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

        public bool DeleteMultiDay(string[] _prmDayCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmDayCode.Length; i++)
                {
                    HRMMsDay _HRMMsDay = this.db.HRMMsDays.Single(_temp => _temp.DayCode == Convert.ToInt32(_prmDayCode[i]));

                    this.db.HRMMsDays.DeleteOnSubmit(_HRMMsDay);
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

        ~DayBL()
        {

        }
    }
}
