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
    public sealed class TaxTypeBL : Base
    {
        public TaxTypeBL()
        {

        }

        #region TaxType

        public String GetCurrencyName(String _prmTaxCode)
        {
            String _result = "";

            _result = this.db.MsTaxTypes.Single(_temp => _temp.TaxTypeCode == _prmTaxCode).TaxTypeName;

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
                            from _msTaxType in this.db.MsTaxTypes
                            where (SqlMethods.Like(_msTaxType.TaxTypeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msTaxType.TaxTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msTaxType
                        ).Count();

            _result = _query;

            return _result;
        }

        public bool Edit(MsTaxType _prmMsTaxType)
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

        public bool Add(MsTaxType _prmMsTaxType)
        {
            bool _result = false;

            try
            {
                this.db.MsTaxTypes.InsertOnSubmit(_prmMsTaxType);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMulti(string[] _prmTaxType)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmTaxType.Length; i++)
                {
                    MsTaxType _msTaxType = this.db.MsTaxTypes.Single(_temp => _temp.TaxTypeCode.Trim().ToLower() == _prmTaxType[i].Trim().ToLower());

                    this.db.MsTaxTypes.DeleteOnSubmit(_msTaxType);
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

        public MsTaxType GetSingle(string _prmTaxCode)
        {
            MsTaxType _result = null;

            _result = this.db.MsTaxTypes.Single(_temp => _temp.TaxTypeCode == _prmTaxCode);

            return _result;
        }

        public List<MsTaxType> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsTaxType> _result = new List<MsTaxType>();

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
                                from _msTaxType in this.db.MsTaxTypes
                                where (SqlMethods.Like(_msTaxType.TaxTypeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msTaxType.TaxTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msTaxType.CreatedDate descending
                                select _msTaxType
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsTaxType> GetList()
        {
            List<MsTaxType> _result = new List<MsTaxType>();

            try
            {
                var _query = (
                                from _msTaxType in this.db.MsTaxTypes
                                where _msTaxType.fgActive == true
                                orderby _msTaxType.CreatedDate descending
                                select _msTaxType
                            );

                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Int32 CountTaxType(String _prmTaxTypeCode)
        {
            Int32 _result = 0;
            
            try
            {
                _result = (from _taxType in this.db.MsTaxTypes
                           where _taxType.TaxTypeCode.Trim().ToLower() == _prmTaxTypeCode.Trim().ToLower()
                           select _taxType
                            ).Count();    
            }
            catch (Exception ex)
            {   
                throw ex;
            }

            return _result;

        }

        //NB: untuk mendapatkan currCode yang FgHomenya adalah true
        //public string GetCurrDefault()
        //{
        //    string _result = "";

        //    try
        //    {
        //        var _query = (
        //                        from _msCurrency in this.db.MsCurrencies
        //                        where _msCurrency.FgHome == HomeCurrency.IsHome(true)
        //                        select new
        //                        {
        //                            CurrCode = _msCurrency.CurrCode
        //                        }
        //                      );

        //        foreach (var _obj in _query)
        //        {
        //            _result = _obj.CurrCode;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public List<MsCurrency> GetListAll()
        //{
        //    List<MsCurrency> _result = new List<MsCurrency>();

        //    try
        //    {
        //        var _query = (
        //                        from curr in this.db.MsCurrencies
        //                        orderby curr.CurrCode ascending
        //                        select new
        //                        {
        //                            CurrCode = curr.CurrCode,
        //                            CurrName = curr.CurrName
        //                        }
        //                    );

        //        foreach (object _obj in _query)
        //        {
        //            var _row = _obj.Template(new { CurrCode = this._string, CurrName = this._string });
        //            _result.Add(new MsCurrency(_row.CurrCode, _row.CurrName));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public List<MsCurrency> GetListCurrForSupplierBeginning(string _prmSuppCode)
        //{
        //    List<MsCurrency> _result = new List<MsCurrency>();

        //    try
        //    {
        //        var _query = (
        //                        from _msSupp in this.db.MsSuppliers
        //                        join _msSuppGroup in this.db.MsSuppGroupAccs
        //                            on _msSupp.SuppGroup equals _msSuppGroup.SuppGroup
        //                        where _msSupp.SuppCode == _prmSuppCode
        //                        orderby _msSuppGroup.CurrCode ascending
        //                        select new
        //                        {
        //                            CurrCode = _msSuppGroup.CurrCode,
        //                            CurrName = (
        //                                            from _msCurrency in this.db.MsCurrencies
        //                                            where _msCurrency.CurrCode == _msSuppGroup.CurrCode
        //                                            select _msCurrency.CurrName
        //                                        ).FirstOrDefault()
        //                        }
        //                    );

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new MsCurrency(_row.CurrCode, _row.CurrName));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public List<MsCurrency> GetListCurrForCustomerBeginning(string _prmCustCode)
        //{
        //    List<MsCurrency> _result = new List<MsCurrency>();

        //    try
        //    {
        //        var _query = (
        //                        from _msCust in this.db.MsCustomers
        //                        join _msCustGroup in this.db.MsCustGroupAccs
        //                            on _msCust.CustGroup equals _msCustGroup.CustGroup
        //                        where _msCust.CustCode == _prmCustCode
        //                        orderby _msCustGroup.CurrCode ascending
        //                        select new
        //                        {
        //                            CurrCode = _msCustGroup.CurrCode,
        //                            CurrName = (
        //                                            from _msCurrency in this.db.MsCurrencies
        //                                            where _msCurrency.CurrCode == _msCustGroup.CurrCode
        //                                            select _msCurrency.CurrName
        //                                        ).FirstOrDefault()
        //                        }
        //                    );

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new MsCurrency(_row.CurrCode, _row.CurrName));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public List<MsCurrency> GetListForDDLExceptHome()
        //{
        //    List<MsCurrency> _result = new List<MsCurrency>();

        //    try
        //    {
        //        var _query = (
        //                        from curr in this.db.MsCurrencies
        //                        where curr.FgHome == 'N'
        //                        orderby curr.CurrCode ascending
        //                        select new
        //                        {
        //                            CurrCode = curr.CurrCode,
        //                            CurrName = curr.CurrName
        //                        }
        //                    );

        //        foreach (object _obj in _query)
        //        {
        //            var _row = _obj.Template(new { CurrCode = this._string, CurrName = this._string });
        //            _result.Add(new MsCurrency(_row.CurrCode, _row.CurrName));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public List<MsCurrency> GetListForDDLForMasterDefault(string _prmSetCode)
        //{
        //    List<MsCurrency> _result = new List<MsCurrency>();

        //    try
        //    {
        //        var _query = (
        //                        from curr in this.db.MsCurrencies
        //                        where !(
        //                                from _msDefaultAcc in this.db.Master_DefaultAccs
        //                                join _msAccount in this.db.MsAccounts
        //                                on _msDefaultAcc.Account equals _msAccount.Account
        //                                where _msDefaultAcc.SetCode == _prmSetCode
        //                                select _msAccount.CurrCode
        //                                ).Contains(curr.CurrCode)
        //                        orderby curr.CurrCode ascending
        //                        select new
        //                        {
        //                            CurrCode = curr.CurrCode,
        //                            CurrName = curr.CurrName
        //                        }
        //                    );

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new MsCurrency(_row.CurrCode, _row.CurrName));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public byte GetDecimalPlace(string _prmCurrCode)
        //{
        //    byte _result = 0;

        //    try
        //    {
        //        var _query = (
        //                        from _msCurrency in this.db.MsCurrencies
        //                        where _msCurrency.CurrCode.Trim().ToLower() == _prmCurrCode.Trim().ToLower()
        //                        select new
        //                        {
        //                            DecimalPlace = _msCurrency.DecimalPlace
        //                        }
        //                      );

        //        foreach (var _obj in _query)
        //        {
        //            _result = _obj.DecimalPlace;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public bool IsCurrExists(String _prmCurr)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        var _query = from _msCurr in this.db.MsCurrencies
        //                     where _msCurr.CurrCode == _prmCurr
        //                     select new
        //                     {
        //                         _msCurr.CurrCode
        //                     };

        //        if (_query.Count() > 0)
        //        {
        //            _result = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public List<MsCurrency> GetListCurrForDDL()
        //{
        //    List<MsCurrency> _result = new List<MsCurrency>();

        //    try
        //    {
        //        var _query = (
        //                        from curr in this.db.MsCurrencies
        //                        orderby curr.CurrCode ascending
        //                        select new
        //                        {
        //                            CurrCode = curr.CurrCode,
        //                            CurrName = curr.CurrName
        //                        }
        //                    );

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new MsCurrency(_row.CurrCode, _row.CurrName));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public List<MsCurrency> GetListCurrForDDLNotInAdjustDiffRateDt(String _prmTransNmbr)
        //{
        //    List<MsCurrency> _result = new List<MsCurrency>();

        //    try
        //    {
        //        var _query = (
        //                        from _curr in this.db.MsCurrencies
        //                        where !(
        //                                from _adjustDiffRateDt in this.db.GLAdjustDiffRateDts
        //                                where _curr.CurrCode == _adjustDiffRateDt.CurrCode
        //                                select _adjustDiffRateDt.CurrCode
        //                               ).Contains(_curr.CurrCode)
        //                        orderby _curr.CurrCode ascending
        //                        select new
        //                        {
        //                            CurrCode = _curr.CurrCode,
        //                            CurrName = _curr.CurrName
        //                        }
        //                    );

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new MsCurrency(_row.CurrCode, _row.CurrName));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        #endregion

        ~TaxTypeBL()
        {
        }
    }
}