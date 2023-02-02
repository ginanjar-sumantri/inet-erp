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

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl
{
    public sealed class PriceGroupBL : Base
    {
        public PriceGroupBL()
        {

        }

        #region PriceGroup

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
                            from _priceGroup in this.db.Master_PriceGroups
                            where (SqlMethods.Like(_priceGroup.PriceGroupCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_priceGroup.CurrCode.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _priceGroup
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<Master_PriceGroup> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<Master_PriceGroup> _result = new List<Master_PriceGroup>();

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
                                from _priceGroup in this.db.Master_PriceGroups
                                where (SqlMethods.Like(_priceGroup.PriceGroupCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_priceGroup.CurrCode.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _priceGroup.PriceGroupCode descending
                                select new
                                {
                                    PriceGroupCode = _priceGroup.PriceGroupCode,
                                    Year = _priceGroup.Year,
                                    CurrCode = _priceGroup.CurrCode,
                                    AmountForex = _priceGroup.AmountForex,
                                    FgActive = _priceGroup.FgActive,
                                    StartDate = _priceGroup.StartDate,
                                    EndDate = _priceGroup.EndDate
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_PriceGroup(_row.PriceGroupCode, _row.Year, _row.CurrCode, _row.AmountForex, _row.FgActive, _row.StartDate, _row.EndDate));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_PriceGroup> GetList()
        {
            List<Master_PriceGroup> _result = new List<Master_PriceGroup>();

            try
            {
                var _query = (
                                from _priceGroup in this.db.Master_PriceGroups
                                orderby _priceGroup.PriceGroupCode descending
                                select new
                                {
                                    PriceGroupCode = _priceGroup.PriceGroupCode,
                                    Year = _priceGroup.Year,
                                    CurrCode = _priceGroup.CurrCode,
                                    AmountForex = _priceGroup.AmountForex,
                                    FgActive = _priceGroup.FgActive,
                                    StartDate = _priceGroup.StartDate,
                                    EndDate = _priceGroup.EndDate
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new Master_PriceGroup(_row.PriceGroupCode, _row.Year, _row.CurrCode, _row.AmountForex, _row.FgActive, _row.StartDate, _row.EndDate));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        private bool IsExistsPGCodeAndYear(string _prmCode, int _prmYear)
        {
            bool _result = false;

            try
            {
                var _query = from _master_priceGroup in this.db.Master_PriceGroups
                             where _master_priceGroup.PriceGroupCode == _prmCode && _master_priceGroup.Year == _prmYear
                             select new
                             {
                                 _master_priceGroup.PriceGroupCode
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

        public bool DeleteMulti(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    Master_PriceGroup _priceGroup = this.db.Master_PriceGroups.Single(_temp => _temp.PriceGroupCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.Master_PriceGroups.DeleteOnSubmit(_priceGroup);
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

        public Master_PriceGroup GetSingle(string _prmCode, int _prmYear)
        {
            Master_PriceGroup _result = null;

            try
            {
                _result = this.db.Master_PriceGroups.Single(_temp => _temp.PriceGroupCode.Trim().ToLower() == _prmCode.Trim().ToLower() && _temp.Year == _prmYear);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(Master_PriceGroup _prmMsPriceGroup)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsPGCodeAndYear(_prmMsPriceGroup.PriceGroupCode, _prmMsPriceGroup.Year) == false)
                {
                    this.db.Master_PriceGroups.InsertOnSubmit(_prmMsPriceGroup);
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

        public bool AddPriceGroupList(List<Master_PriceGroup> _prmMsPriceGroupList)
        {
            bool _result = false;

            try
            {
                foreach (Master_PriceGroup _row in _prmMsPriceGroupList)
                {
                    Master_PriceGroup _msPriceGroup = new Master_PriceGroup();

                    _msPriceGroup.PriceGroupCode = _row.PriceGroupCode;
                    _msPriceGroup.Year = _row.Year;
                    _msPriceGroup.CurrCode = _row.CurrCode;
                    _msPriceGroup.AmountForex = _row.AmountForex;
                    _msPriceGroup.FgActive = _row.FgActive;
                    _msPriceGroup.PGDesc = _row.PGDesc;
                    _msPriceGroup.InsertBy = _row.InsertBy;
                    _msPriceGroup.InsertDate = _row.InsertDate;
                    _msPriceGroup.EditBy = _row.EditBy;
                    _msPriceGroup.EditDate = _row.EditDate;
                    _msPriceGroup.StartDate = _row.StartDate;
                    _msPriceGroup.EndDate = _row.EndDate;

                    this.db.Master_PriceGroups.InsertOnSubmit(_msPriceGroup);
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

        public bool Edit(Master_PriceGroup _prmMsPriceGroup)
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

        public List<Master_PriceGroup> GetListForDDLForProduct(int _prmYear)
        {
            List<Master_PriceGroup> _result = new List<Master_PriceGroup>();

            try
            {
                var _query = (
                                from _masterPriceGroup in this.db.Master_PriceGroups
                                where _masterPriceGroup.Year == _prmYear
                                orderby _masterPriceGroup.PriceGroupCode ascending
                                select new
                                {
                                    PriceGroupCode = _masterPriceGroup.PriceGroupCode
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new Master_PriceGroup(_row.PriceGroupCode));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool IsPriceGroupExists(String _prmPriceGroup)
        {
            bool _result = false;

            try
            {
                var _query = from _msPriceGroup in this.db.Master_PriceGroups
                             where _msPriceGroup.PriceGroupCode == _prmPriceGroup
                             select new
                             {
                                 _msPriceGroup.PriceGroupCode
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

        public Decimal GetAmountForexByPGCode(String _prmPGCode)
        {
            Decimal _result = 0;
            try
            {
                var _qry = (from _masterPriceGroup in this.db.Master_PriceGroups where _masterPriceGroup.PriceGroupCode == _prmPGCode select _masterPriceGroup.AmountForex);
                if (_qry.Count() > 0)
                    _result = _qry.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        #endregion

        ~PriceGroupBL()
        {
        }

    }
}
