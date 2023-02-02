using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting
{
    public sealed class CurrencyRateBL : Base
    {
        public CurrencyRateBL()
        {

        }

        #region CurrencyRate

        public double RowsCount(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            //string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                //_pattern2 = "%%";
            }

            var _query =
                        (
                            from _msCurrRate in this.db.MsCurrRates
                            join _msCurr in db.MsCurrencies
                                    on _msCurrRate.CurrCode equals _msCurr.CurrCode
                            where (_msCurrRate.CurrDate == db.MsCurrRates.Where(c => c.CurrCode == _msCurr.CurrCode).Max(currRate => currRate.CurrDate))
                               && (_msCurr.FgHome == 'N')
                               && (SqlMethods.Like(_msCurrRate.CurrCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                            select _msCurrRate
                        ).Count();

            _result = _query;

            return _result;
        }

        public int RowsCountCurrRate(string _prmCurrCode)
        {
            var _query = (
                            from _msCurrRate in this.db.MsCurrRates
                            where _msCurrRate.CurrCode == _prmCurrCode
                            orderby _msCurrRate.CurrDate descending
                            select new
                            {
                                CurrDate = _msCurrRate.CurrDate,
                                CurrCode = _msCurrRate.CurrCode,
                                CurrRate = _msCurrRate.CurrRate
                            }
                         ).Count();

            return _query;
        }

        public List<MsCurrRate> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsCurrRate> _result = new List<MsCurrRate>();

            string _pattern1 = "%%";
            //string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                //_pattern2 = "%%";

            }

            try
            {
                var _query = (
                                from _msCurrRate in db.MsCurrRates
                                join _msCurr in db.MsCurrencies
                                    on _msCurrRate.CurrCode equals _msCurr.CurrCode
                                where (_msCurrRate.CurrDate == db.MsCurrRates.Where(c => c.CurrCode == _msCurr.CurrCode).Max(currRate => currRate.CurrDate))
                                   && (_msCurr.FgHome == 'N')
                                   && (SqlMethods.Like(_msCurrRate.CurrCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                orderby _msCurrRate.CurrCode ascending
                                select new
                                {
                                    CurrDate = _msCurrRate.CurrDate,
                                    CurrCode = _msCurrRate.CurrCode,
                                    CurrRate = _msCurrRate.CurrRate
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { CurrDate = this._datetime, CurrCode = this._string, CurrRate = this._decimal });

                    _result.Add(new MsCurrRate(_row.CurrDate, _row.CurrCode, _row.CurrRate));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsCurrRate> GetListCurrRate(int _prmReqPage, int _prmPageSize, string _prmCurrCode)
        {
            List<MsCurrRate> _result = new List<MsCurrRate>();

            try
            {
                var _query = (
                                from _msCurrRate in this.db.MsCurrRates
                                where _msCurrRate.CurrCode == _prmCurrCode
                                orderby _msCurrRate.CurrDate descending
                                select new
                                {
                                    CurrDate = _msCurrRate.CurrDate,
                                    CurrCode = _msCurrRate.CurrCode,
                                    CurrRate = _msCurrRate.CurrRate
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { CurrDate = this._datetime, CurrCode = this._string, CurrRate = this._decimal });

                    _result.Add(new MsCurrRate(_row.CurrDate, _row.CurrCode, _row.CurrRate));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public decimal GetSingleLatestCurrRate(string _prmCurrCode)
        {
            decimal _result = 0;

            try
            {
                var _query = from _msCurrRate in db.MsCurrRates
                             join _msCurr in db.MsCurrencies
                                 on _msCurrRate.CurrCode equals _msCurr.CurrCode
                             where (_msCurrRate.CurrDate == db.MsCurrRates.Where(c => c.CurrCode == _msCurr.CurrCode).Max(currRate => currRate.CurrDate)) && (_msCurr.FgHome == 'N') && (_msCurr.CurrCode == _prmCurrCode)
                             orderby _msCurrRate.CurrCode ascending
                             select new
                             {
                                 CurrRate = _msCurrRate.CurrRate
                             };

                foreach (var _obj in _query)
                {
                    _result = _obj.CurrRate;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public MsCurrRate GetSingle(string _prmCurrDate, string _prmCurrCode)
        {
            MsCurrRate _result = null;

            try
            {
                _result = this.db.MsCurrRates.Single(_temp => _temp.CurrDate == Convert.ToDateTime(_prmCurrDate) && _temp.CurrCode == _prmCurrCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(MsCurrRate _prmMsCurrRate)
        {
            bool _result = false;

            try
            {
                this.db.MsCurrRates.InsertOnSubmit(_prmMsCurrRate);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(MsCurrRate _prmMsCurrRate)
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


        ~CurrencyRateBL()
        {

        }
    }
}
