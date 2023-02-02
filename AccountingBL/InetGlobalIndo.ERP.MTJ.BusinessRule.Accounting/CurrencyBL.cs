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
    public sealed class CurrencyBL : Base
    {
        public CurrencyBL()
        {

        }

        #region Currency

        public String GetCurrencyName(String _prmCurrCode)
        {
            String _result = "";

            _result = this.db.MsCurrencies.Single(_temp => _temp.CurrCode == _prmCurrCode).CurrName;

            return _result;
        }

        public double RowsCount(string _prmCategory, string _prmKeyword)
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
                            from _msCurrency in this.db.MsCurrencies
                            where (SqlMethods.Like(_msCurrency.CurrCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msCurrency.CurrName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msCurrency
                        ).Count();

            _result = _query;

            return _result;
        }

        public bool Edit(MsCurrency _prmMsCurrency)
        {
            bool _result = false;

            try
            {
                //if ((_prmMsCurrency.FgHome == CurrencyDataMapper.GetYesNo(YesNo.Yes) && this.GetCurrDefault() == "") || _prmMsCurrency.FgHome == CurrencyDataMapper.GetYesNo(YesNo.No))
                //{
                this.db.SubmitChanges();

                _result = true;
                //}
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(MsCurrency _prmMsCurrency)
        {
            bool _result = false;

            try
            {
                if ((_prmMsCurrency.FgHome == YesNoDataMapper.GetYesNo(YesNo.Yes) && this.GetCurrDefault() == "") || _prmMsCurrency.FgHome == YesNoDataMapper.GetYesNo(YesNo.No))
                {
                    this.db.MsCurrencies.InsertOnSubmit(_prmMsCurrency);

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

        public bool DeleteMulti(string[] _prmCurrCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCurrCode.Length; i++)
                {
                    MsCurrency _msCurrency = this.db.MsCurrencies.Single(_curr => _curr.CurrCode.Trim().ToLower() == _prmCurrCode[i].Trim().ToLower());

                    this.db.MsCurrencies.DeleteOnSubmit(_msCurrency);
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

        public MsCurrency GetSingle(string _prmCurrCode)
        {
            MsCurrency _result = null;

            _result = this.db.MsCurrencies.Single(_curr => _curr.CurrCode == _prmCurrCode);

            return _result;
        }

        public List<MsCurrency> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsCurrency> _result = new List<MsCurrency>();

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
                                from _msCurrency in this.db.MsCurrencies
                                where (SqlMethods.Like(_msCurrency.CurrCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msCurrency.CurrName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msCurrency.UserDate descending
                                select new
                                {
                                    CurrCode = _msCurrency.CurrCode,
                                    CurrName = _msCurrency.CurrName,
                                    FgHome = _msCurrency.FgHome,
                                    DecimalPlace = _msCurrency.DecimalPlace,
                                    DecimalPlaceReport = _msCurrency.DecimalPlaceReport,
                                    ValueTolerance = _msCurrency.ValueTolerance
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsCurrency(_row.CurrCode, _row.CurrName, _row.FgHome, _row.DecimalPlace, _row.DecimalPlaceReport, _row.ValueTolerance));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsCurrency> GetList()
        {
            List<MsCurrency> _result = new List<MsCurrency>();

            try
            {
                var _query = (
                                from curr in this.db.MsCurrencies
                                where curr.FgHome == 'N'
                                orderby curr.UserDate descending
                                select new
                                {
                                    CurrCode = curr.CurrCode,
                                    CurrName = curr.CurrName,
                                    FgHome = curr.FgHome,
                                    DecimalPlace = curr.DecimalPlace,
                                    DecimalPlaceReport = curr.DecimalPlaceReport,
                                    ValueTolerance = curr.ValueTolerance
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsCurrency(_row.CurrCode, _row.CurrName, _row.FgHome, _row.DecimalPlace, _row.DecimalPlaceReport, _row.ValueTolerance));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //NB: untuk mendapatkan currCode yang FgHomenya adalah true
        public string GetCurrDefault()
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msCurrency in this.db.MsCurrencies
                                where _msCurrency.FgHome == HomeCurrency.IsHome(true)
                                select new
                                {
                                    CurrCode = _msCurrency.CurrCode
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.CurrCode;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsCurrency> GetListAll()
        {
            List<MsCurrency> _result = new List<MsCurrency>();

            try
            {
                var _query = (
                                from curr in this.db.MsCurrencies
                                orderby curr.CurrCode ascending
                                select new
                                {
                                    CurrCode = curr.CurrCode,
                                    CurrName = curr.CurrName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { CurrCode = this._string, CurrName = this._string });
                    _result.Add(new MsCurrency(_row.CurrCode, _row.CurrName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsCurrency> GetListCurrForSupplierBeginning(string _prmSuppCode)
        {
            List<MsCurrency> _result = new List<MsCurrency>();

            try
            {
                var _query = (
                                from _msSupp in this.db.MsSuppliers
                                join _msSuppGroup in this.db.MsSuppGroupAccs
                                    on _msSupp.SuppGroup equals _msSuppGroup.SuppGroup
                                where _msSupp.SuppCode == _prmSuppCode
                                orderby _msSuppGroup.CurrCode ascending
                                select new
                                {
                                    CurrCode = _msSuppGroup.CurrCode,
                                    CurrName = (
                                                    from _msCurrency in this.db.MsCurrencies
                                                    where _msCurrency.CurrCode == _msSuppGroup.CurrCode
                                                    select _msCurrency.CurrName
                                                ).FirstOrDefault()
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsCurrency(_row.CurrCode, _row.CurrName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsCurrency> GetListCurrForCustomerBeginning(string _prmCustCode)
        {
            List<MsCurrency> _result = new List<MsCurrency>();

            try
            {
                var _query = (
                                from _msCust in this.db.MsCustomers
                                join _msCustGroup in this.db.MsCustGroupAccs
                                    on _msCust.CustGroup equals _msCustGroup.CustGroup
                                where _msCust.CustCode == _prmCustCode
                                orderby _msCustGroup.CurrCode ascending
                                select new
                                {
                                    CurrCode = _msCustGroup.CurrCode,
                                    CurrName = (
                                                    from _msCurrency in this.db.MsCurrencies
                                                    where _msCurrency.CurrCode == _msCustGroup.CurrCode
                                                    select _msCurrency.CurrName
                                                ).FirstOrDefault()
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsCurrency(_row.CurrCode, _row.CurrName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsCurrency> GetListForDDLExceptHome()
        {
            List<MsCurrency> _result = new List<MsCurrency>();

            try
            {
                var _query = (
                                from curr in this.db.MsCurrencies
                                where curr.FgHome == 'N'
                                orderby curr.CurrCode ascending
                                select new
                                {
                                    CurrCode = curr.CurrCode,
                                    CurrName = curr.CurrName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { CurrCode = this._string, CurrName = this._string });
                    _result.Add(new MsCurrency(_row.CurrCode, _row.CurrName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsCurrency> GetListForDDLForMasterDefault(string _prmSetCode)
        {
            List<MsCurrency> _result = new List<MsCurrency>();

            try
            {
                var _query = (
                                from curr in this.db.MsCurrencies
                                where !(
                                        from _msDefaultAcc in this.db.Master_DefaultAccs
                                        join _msAccount in this.db.MsAccounts
                                        on _msDefaultAcc.Account equals _msAccount.Account
                                        where _msDefaultAcc.SetCode == _prmSetCode
                                        select _msAccount.CurrCode
                                        ).Contains(curr.CurrCode)
                                orderby curr.CurrCode ascending
                                select new
                                {
                                    CurrCode = curr.CurrCode,
                                    CurrName = curr.CurrName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsCurrency(_row.CurrCode, _row.CurrName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public byte GetDecimalPlace(string _prmCurrCode)
        {
            byte _result = 0;

            try
            {
                var _query = (
                                from _msCurrency in this.db.MsCurrencies
                                where _msCurrency.CurrCode.Trim().ToLower() == _prmCurrCode.Trim().ToLower()
                                select new
                                {
                                    DecimalPlace = _msCurrency.DecimalPlace
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.DecimalPlace;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool IsCurrExists(String _prmCurr)
        {
            bool _result = false;

            try
            {
                var _query = from _msCurr in this.db.MsCurrencies
                             where _msCurr.CurrCode == _prmCurr
                             select new
                             {
                                 _msCurr.CurrCode
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

        public List<MsCurrency> GetListCurrForDDL()
        {
            List<MsCurrency> _result = new List<MsCurrency>();

            try
            {
                var _query = (
                                from curr in this.db.MsCurrencies
                                orderby curr.CurrCode ascending
                                select new
                                {
                                    CurrCode = curr.CurrCode,
                                    CurrName = curr.CurrName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsCurrency(_row.CurrCode, _row.CurrName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsCurrency> GetListCurrForDDLNotInAdjustDiffRateDt(String _prmTransNmbr)
        {
            List<MsCurrency> _result = new List<MsCurrency>();

            try
            {
                var _query = (
                                from _curr in this.db.MsCurrencies
                                where !(
                                        from _adjustDiffRateDt in this.db.GLAdjustDiffRateDts
                                        where _curr.CurrCode == _adjustDiffRateDt.CurrCode
                                        && _adjustDiffRateDt.TransNmbr == _prmTransNmbr
                                        select _adjustDiffRateDt.CurrCode
                                       ).Contains(_curr.CurrCode)
                                orderby _curr.CurrCode ascending
                                select new
                                {
                                    CurrCode = _curr.CurrCode,
                                    CurrName = _curr.CurrName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsCurrency(_row.CurrCode, _row.CurrName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~CurrencyBL()
        {
        }
    }
}