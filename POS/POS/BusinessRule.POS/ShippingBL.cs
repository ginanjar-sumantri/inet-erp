using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;

namespace BusinessRule.POS
{
    public sealed class ShippingBL : Base
    {
        public ShippingBL()
        {
        }

        #region POSMsShippingType

        public double RowsCountPOSMsShippingType(string _prmCategory, string _prmKeyword)
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
                             from _posMsShippingType in this.db.POSMsShippingTypes
                             where (SqlMethods.Like(_posMsShippingType.ShippingTypeCode.ToString(), _pattern1))
                                && (SqlMethods.Like(_posMsShippingType.ShippingTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             select _posMsShippingType
                        ).Count();

            _result = _query;

            return _result;
        }

        public POSMsShippingType GetSinglePOSMsShippingType(string _prmCode)
        {
            POSMsShippingType _result = null;

            try
            {
                _result = this.db.POSMsShippingTypes.Single(_temp => _temp.ShippingTypeCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<POSMsShippingType> GetListPOSMsShippingType(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsShippingType> _result = new List<POSMsShippingType>();

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
                                from _posMsShippingTypes in this.db.POSMsShippingTypes
                                where (SqlMethods.Like(_posMsShippingTypes.ShippingTypeCode.ToString(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_posMsShippingTypes.ShippingTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _posMsShippingTypes.ShippingTypeName descending
                                select _posMsShippingTypes
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

        public bool AddPOSMsShippingType(POSMsShippingType _prmPOSMsShippingType)
        {
            bool _result = false;

            try
            {
                this.db.POSMsShippingTypes.InsertOnSubmit(_prmPOSMsShippingType);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSubmit()
        {
            bool _result = false;
            try
            {
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public bool DeleteMultiPOSMsShippingType(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');
                    var _query = (from _posMsShippingType in this.db.POSMsShippingTypes
                                  where _posMsShippingType.ShippingTypeCode == _tempSplit[0]
                                  select _posMsShippingType);

                    this.db.POSMsShippingTypes.DeleteAllOnSubmit(_query);
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

        #region POSMsShippingTypeDt

        public double RowsCountPOSMsShippingTypeDt(string _prmCategory, string _prmKeyword)
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
                             from _posMsShippingTypeDt in this.db.POSMsShippingTypeDts
                             join _posMsShippingType in this.db.POSMsShippingTypes
                             on _posMsShippingTypeDt.ShippingTypeCode equals _posMsShippingType.ShippingTypeCode
                             where (SqlMethods.Like(_posMsShippingTypeDt.ShippingTypeCode.ToString(), _pattern1))
                             && (SqlMethods.Like(_posMsShippingType.ShippingTypeName.ToString(), _pattern2))
                             select _posMsShippingTypeDt
                        ).Count();

            _result = _query;

            return _result;
        }

        public POSMsShippingTypeDt GetSinglePOSMsShippingTypeDt(string _prmCode, string _prmCityCode, string _prmProductShape)
        {
            POSMsShippingTypeDt _result = null;

            try
            {
                _result = this.db.POSMsShippingTypeDts.Single(_temp => _temp.ShippingTypeCode == _prmCode && _temp.CityCode == _prmCityCode && _temp.ProductShape == _prmProductShape);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<POSMsShippingTypeDt> GetListPOSMsShippingTypeDt(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsShippingTypeDt> _result = new List<POSMsShippingTypeDt>();

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
                                from _posMsShippingTypeDt in this.db.POSMsShippingTypeDts
                                join _posMsShippingType in this.db.POSMsShippingTypes
                                on _posMsShippingTypeDt.ShippingTypeCode equals _posMsShippingType.ShippingTypeCode
                                where (SqlMethods.Like(_posMsShippingTypeDt.ShippingTypeCode.ToString(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_posMsShippingType.ShippingTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _posMsShippingType.ShippingTypeName descending
                                select _posMsShippingTypeDt
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

        public bool AddPOSMsShippingTypeDt(POSMsShippingTypeDt _prmPOSMsShippingTypeDt)
        {
            bool _result = false;

            try
            {
                this.db.POSMsShippingTypeDts.InsertOnSubmit(_prmPOSMsShippingTypeDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiPOSMsShippingTypeDt(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('|');
                    var _query = (from _posMsShippingTypeDt in this.db.POSMsShippingTypeDts
                                  where _posMsShippingTypeDt.ShippingTypeCode == _tempSplit[0]
                                    && _posMsShippingTypeDt.CityCode == _tempSplit[1]
                                    && _posMsShippingTypeDt.ProductShape == _tempSplit[2]
                                  select _posMsShippingTypeDt);

                    this.db.POSMsShippingTypeDts.DeleteAllOnSubmit(_query);
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

        public double RowsCountShippingTypeDtForVendor(string _prmVendorCode, char _prmFgZone)
        {
            double _result = 0;
            var _query =
                        (
                             from _v_posMsShipping in this.db.V_POSMsShippings
                             where _v_posMsShipping.VendorCode == _prmVendorCode
                             && (_v_posMsShipping.FgZone == _prmFgZone
                             || _v_posMsShipping.FgZone == null)
                             select _v_posMsShipping
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<V_POSMsShipping> GetListShippingTypeDtForVendor(int _prmReqPage, int _prmPageSize, string _prmVendorCode, char _prmFgZone)
        {
            List<V_POSMsShipping> _result = new List<V_POSMsShipping>();



            try
            {
                var _query = (
                                from _v_posMsShipping in this.db.V_POSMsShippings
                                where _v_posMsShipping.VendorCode == _prmVendorCode
                                && (_v_posMsShipping.FgZone == _prmFgZone
                                || _v_posMsShipping.FgZone == null)
                                orderby _v_posMsShipping.ShippingZonaTypeName descending, _v_posMsShipping.ProductShape, _v_posMsShipping.CityName
                                select _v_posMsShipping
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

        #endregion

        #region POSMsZone

        public double RowsCountPOSMsZone(string _prmCategory, string _prmKeyword)
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
                             from _posMsZone in this.db.POSMsZones
                             where (SqlMethods.Like(_posMsZone.ZoneCode.ToString(), _pattern1))
                                && (SqlMethods.Like(_posMsZone.ZoneName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             select _posMsZone
                        ).Count();

            _result = _query;

            return _result;
        }

        public POSMsZone GetSinglePOSMsZone(string _prmCode)
        {
            POSMsZone _result = null;

            try
            {
                _result = this.db.POSMsZones.Single(_temp => _temp.ZoneCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<POSMsZone> GetListPOSMsZone(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsZone> _result = new List<POSMsZone>();

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
                                from _posMsZones in this.db.POSMsZones
                                where (SqlMethods.Like(_posMsZones.ZoneCode.ToString(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_posMsZones.ZoneName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _posMsZones.ModifiedDate descending, _posMsZones.CreatedDate descending
                                select _posMsZones
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

        public bool AddPOSMsZone(POSMsZone _prmPOSMsZone)
        {
            bool _result = false;

            try
            {
                this.db.POSMsZones.InsertOnSubmit(_prmPOSMsZone);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiPOSMsZone(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');
                    var _query = (from _posMsZone in this.db.POSMsZones
                                  where _posMsZone.ZoneCode == _tempSplit[0]
                                  select _posMsZone);

                    this.db.POSMsZones.DeleteAllOnSubmit(_query);
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

        #region POSMsZoneCountry

        public double RowsCountPOSMsZoneCountry(string _prmCategory, string _prmKeyword)
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
                             from _posMsZoneCountry in this.db.POSMsZoneCountries
                             join _posMsZone in this.db.POSMsZones
                             on _posMsZoneCountry.ZoneCode equals _posMsZone.ZoneCode
                             where (SqlMethods.Like(_posMsZoneCountry.ZoneCode.ToString(), _pattern1))
                             && (SqlMethods.Like(_posMsZone.ZoneName.ToString(), _pattern2))
                             select _posMsZoneCountry
                        ).Count();

            _result = _query;

            return _result;
        }

        public POSMsZoneCountry GetSinglePOSMsZoneCountry(string _prmCode, string _prmCountryCode, string _prmCityCode)
        {
            POSMsZoneCountry _result = null;

            try
            {
                _result = this.db.POSMsZoneCountries.FirstOrDefault(_temp => _temp.ZoneCode == _prmCode && _temp.CountryCode == _prmCountryCode && _temp.CityCode == _prmCityCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<POSMsZoneCountry> GetListPOSMsZoneCountry(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsZoneCountry> _result = new List<POSMsZoneCountry>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = _prmKeyword;
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = _prmKeyword;
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _posMsZoneCountry in this.db.POSMsZoneCountries
                                join _posMsZone in this.db.POSMsZones
                                on _posMsZoneCountry.ZoneCode equals _posMsZone.ZoneCode
                                where (SqlMethods.Like(_posMsZoneCountry.ZoneCode.ToString(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_posMsZone.ZoneName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _posMsZoneCountry.CountryCode, _posMsZoneCountry.CityCode
                                select _posMsZoneCountry
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

        public bool AddPOSMsZoneCountry(POSMsZoneCountry _prmPOSMsZoneCountry)
        {
            bool _result = false;

            try
            {
                this.db.POSMsZoneCountries.InsertOnSubmit(_prmPOSMsZoneCountry);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiPOSMsZoneCountry(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('|');
                    var _query = (from _posMsZoneCountry in this.db.POSMsZoneCountries
                                  where _posMsZoneCountry.ZoneCode == _tempSplit[0]
                                    && _posMsZoneCountry.CountryCode == _tempSplit[1]
                                  && _posMsZoneCountry.CityCode == _tempSplit[2]
                                  select _posMsZoneCountry);

                    this.db.POSMsZoneCountries.DeleteAllOnSubmit(_query);
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

        #region POSMsZonePrice

        //public double RowsCountPOSMsZonePrice(string _prmCategory, string _prmKeyword)
        //{
        //    double _result = 0;

        //    string _pattern1 = "%%";
        //    string _pattern2 = "%%";

        //    if (_prmCategory == "Code")
        //    {
        //        _pattern1 = "%" + _prmKeyword + "%";
        //        _pattern2 = "%%";

        //    }
        //    else if (_prmCategory == "Name")
        //    {
        //        _pattern2 = "%" + _prmKeyword + "%";
        //        _pattern1 = "%%";
        //    }

        //    var _query =
        //                (
        //                     from _posMsZonePrice in this.db.POSMsZonePrices
        //                     join _posMsZone in this.db.POSMsZones
        //                     on _posMsZonePrice.ZoneCode equals _posMsZone.ZoneCode
        //                     where (SqlMethods.Like(_posMsZonePrice.ZoneCode.ToString(), _pattern1))
        //                     && (SqlMethods.Like(_posMsZone.ZoneName.ToString(), _pattern2))
        //                     select _posMsZonePrice
        //                ).Count();

        //    _result = _query;

        //    return _result;
        //}

        public double RowsCountPOSMsZonePrice(string _prmZoneCode)
        {
            double _result = 0;

            var _query =
                        (
                             from _posMsZonePrice in this.db.POSMsZonePrices
                             join _posMsZone in this.db.POSMsZones
                             on _posMsZonePrice.ZoneCode equals _posMsZone.ZoneCode
                             where _posMsZone.ZoneCode == _prmZoneCode
                             select _posMsZonePrice
                        ).Count();

            _result = _query;

            return _result;
        }

        public double RowsCountPOSMsZonePriceForVendor(string _prmVendorCode)
        {
            double _result = 0;
            var _query =
                        (
                             from _general_Temporary in this.db.General_TemporaryTables
                             where _general_Temporary.PrimaryKey1 == _prmVendorCode
                             && _general_Temporary.TableName == "spPOS_GetZoneByVendorResult"
                             select _general_Temporary
                        ).Count();

            _result = _query;

            return _result;
        }

        public POSMsZonePrice GetSinglePOSMsZonePrice(string _prmCode, string _prmProductShape, decimal _prmWeight)
        {
            POSMsZonePrice _result = null;

            try
            {
                _result = this.db.POSMsZonePrices.FirstOrDefault(_temp => _temp.ZoneCode == _prmCode && _temp.ProductShape == _prmProductShape && _temp.Weight == _prmWeight);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public POSMsZonePrice GetSinglePOSMsZonePriceForEdit(string _prmCode, string _prmProductShape, decimal _prmWeight)
        {
            POSMsZonePrice _result = null;

            try
            {
                _result = this.db.POSMsZonePrices.FirstOrDefault(_temp => "[" + _temp.ZoneCode + "]" == _prmCode && _temp.ProductShape == _prmProductShape && _temp.Weight == _prmWeight);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        
        public List<POSMsZonePrice> GetListPOSMsZonePrice(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsZonePrice> _result = new List<POSMsZonePrice>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = _prmKeyword;
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = _prmKeyword;
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _posMsZonePrice in this.db.POSMsZonePrices
                                join _posMsZone in this.db.POSMsZones
                                on _posMsZonePrice.ZoneCode equals _posMsZone.ZoneCode
                                where (SqlMethods.Like(_posMsZonePrice.ZoneCode.ToString(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_posMsZone.ZoneName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _posMsZonePrice.ProductShape, _posMsZonePrice.Weight
                                select _posMsZonePrice
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

        public bool AddPOSMsZonePrice(POSMsZonePrice _prmPOSMsZonePrice)
        {
            bool _result = false;

            try
            {
                this.db.POSMsZonePrices.InsertOnSubmit(_prmPOSMsZonePrice);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiPOSMsZonePrice(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('|');
                    var _query = (from _posMsZonePrice in this.db.POSMsZonePrices
                                  where _posMsZonePrice.ZoneCode == _tempSplit[0]
                                    && _posMsZonePrice.ProductShape == _tempSplit[1]
                                  && _posMsZonePrice.Weight == Convert.ToDecimal(_tempSplit[2])
                                  select _posMsZonePrice);

                    this.db.POSMsZonePrices.DeleteAllOnSubmit(_query);
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

        #region POSMsZoneMultiplePrice

        public double RowsCountPOSMsZoneMultiplePrice(string _prmCategory, string _prmKeyword)
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
                             from _posMsZoneMultiplePrice in this.db.POSMsZoneMultiplePrices
                             join _posMsZone in this.db.POSMsZones
                             on _posMsZoneMultiplePrice.ZoneCode equals _posMsZone.ZoneCode
                             where (SqlMethods.Like(_posMsZoneMultiplePrice.ZoneCode.ToString(), _pattern1))
                             && (SqlMethods.Like(_posMsZone.ZoneName.ToString(), _pattern2))
                             select _posMsZoneMultiplePrice
                        ).Count();

            _result = _query;

            return _result;
        }

        public double RowsCountPOSMsZoneMultiplePriceForVendor(string _prmVendorCode)
        {
            double _result = 0;
            var _query =
                        (
                             from _general_Temporary in this.db.General_TemporaryTables
                             where _general_Temporary.PrimaryKey1 == _prmVendorCode
                             && _general_Temporary.TableName == "spPOS_GetZoneMultipleByVendorResult"
                             select _general_Temporary
                        ).Count();

            _result = _query;

            return _result;
        }

        public POSMsZoneMultiplePrice GetSinglePOSMsZoneMultiplePriceForEdit(string _prmCode, string _prmProductShape, decimal _prmWeight1, decimal _prmWeight2)
        {
            POSMsZoneMultiplePrice _result = null;

            try
            {
                _result = this.db.POSMsZoneMultiplePrices.FirstOrDefault(_temp => "[" + _temp.ZoneCode + "]" == _prmCode && _temp.ProductShape == _prmProductShape && _temp.Weight1 == _prmWeight1 && _temp.Weight2 == _prmWeight2);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        
        public POSMsZoneMultiplePrice GetSinglePOSMsZoneMultiplePrice(string _prmCode, string _prmProductShape, decimal _prmWeight1, decimal _prmWeight2)
        {
            POSMsZoneMultiplePrice _result = null;

            try
            {
                _result = this.db.POSMsZoneMultiplePrices.FirstOrDefault(_temp => _temp.ZoneCode == _prmCode && _temp.ProductShape == _prmProductShape && _temp.Weight1 == _prmWeight1 && _temp.Weight2 == _prmWeight2);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<POSMsZoneMultiplePrice> GetListPOSMsZoneMultiplePrice(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsZoneMultiplePrice> _result = new List<POSMsZoneMultiplePrice>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = _prmKeyword;
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = _prmKeyword;
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _POSMsZoneMultiplePrice in this.db.POSMsZoneMultiplePrices
                                join _posMsZone in this.db.POSMsZones
                                on _POSMsZoneMultiplePrice.ZoneCode equals _posMsZone.ZoneCode
                                where (SqlMethods.Like(_POSMsZoneMultiplePrice.ZoneCode.ToString(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_posMsZone.ZoneName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _POSMsZoneMultiplePrice.ProductShape, _POSMsZoneMultiplePrice.Weight1, _POSMsZoneMultiplePrice.Weight2
                                select _POSMsZoneMultiplePrice
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

        public bool AddPOSMsZoneMultiplePrice(POSMsZoneMultiplePrice _prmPOSMsZoneMultiplePrice)
        {
            bool _result = false;

            try
            {
                this.db.POSMsZoneMultiplePrices.InsertOnSubmit(_prmPOSMsZoneMultiplePrice);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiPOSMsZoneMultiplePrice(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('|');
                    var _query = (from _POSMsZoneMultiplePrice in this.db.POSMsZoneMultiplePrices
                                  where _POSMsZoneMultiplePrice.ZoneCode == _tempSplit[0]
                                    && _POSMsZoneMultiplePrice.ProductShape == _tempSplit[1]
                                  && _POSMsZoneMultiplePrice.Weight1 == Convert.ToDecimal(_tempSplit[2])
                                  && _POSMsZoneMultiplePrice.Weight2 == Convert.ToDecimal(_tempSplit[3])
                                  select _POSMsZoneMultiplePrice);

                    this.db.POSMsZoneMultiplePrices.DeleteAllOnSubmit(_query);
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

        public List<General_TemporaryTable> CheckExistWeight(String _prmZoneCode, String _prmProductShape, Decimal _prmWeight1, Decimal _prmWeight2)
        {
            List<General_TemporaryTable> _result = null;

            this.db.spPOS_CheckWeight(_prmZoneCode, _prmProductShape, _prmWeight1, _prmWeight2);

            var _query = (from _temp in this.db.General_TemporaryTables
                          where _temp.TableName == "POSMsZoneMultiplePrice"
                          && _temp.StoreProcedure == "spPOS_CheckWeight"
                          && _temp.PrimaryKey1 == _prmZoneCode
                          && _temp.PrimaryKey2 == _prmProductShape
                          select _temp
                          );

            if (_query.Count() > 0)
                _result = new List<General_TemporaryTable>();

            foreach (var _item in _query)
            {
                _result.Add(_item);
                //_result.Add(new General_TemporaryTable(_item.ID, _item.TableName, _item.StoreProcedure,
                //    _item.PrimaryKeyDescription, _item.PrimaryKey1, _item.PrimaryKey2, _item.PrimaryKey3, _item.PrimaryKey4, _item.PrimaryKey5,
                //    _item.FieldDescription, _item.Field1, _item.Field2, _item.Field3, _item.Field4, _item.Field5,
                //    _item.Field6, _item.Field7, _item.Field8, _item.Field9, _item.Field10,
                //    _item.Field11, _item.Field12, _item.Field13, _item.Field14, _item.Field15,
                //    _item.Field16, _item.Field17, _item.Field18, _item.Field19, _item.Field20,
                //    _item.Remark, _item.CreatedBy, _item.CreatedDate));
            }
            return _result;
        }

        public List<General_TemporaryTable> GetZoneByVendor(int _prmReqPage, int _prmPageSize, String _prmVendorCode, bool _prmMultiple)
        {
            List<General_TemporaryTable> _result = null;

            this.db.spPOS_GetZoneByVendor(_prmVendorCode);
            String _tableName = "";
            if (_prmMultiple == true)
                _tableName = "spPOS_GetZoneMultipleByVendorResult";
            else
                _tableName = "spPOS_GetZoneByVendorResult";

            var _query = (from _temp in this.db.General_TemporaryTables
                          where _temp.TableName == _tableName
                          && _temp.StoreProcedure == "spPOS_GetZoneByVendor"
                          && _temp.PrimaryKey1 == _prmVendorCode
                          select _temp
                          ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

            if (_query.Count() > 0)
                _result = new List<General_TemporaryTable>();

            foreach (var _item in _query)
            {
                _result.Add(_item);
            }
            return _result;
        }

        public General_TemporaryTable GetSingleZoneByVendor(String _prmVendorCode, bool _prmMultiple)
        {
            General_TemporaryTable _result = null;

            String _tableName = "";
            if (_prmMultiple == true)
                _tableName = "spPOS_GetZoneMultipleByVendorResult";
            else
                _tableName = "spPOS_GetZoneByVendorResult";

            _result = this.db.General_TemporaryTables.FirstOrDefault(_temp => _temp.TableName == _tableName && _temp.StoreProcedure == "spPOS_GetZoneByVendor" && _temp.PrimaryKey1 == _prmVendorCode);

            return _result;
        }

        #endregion

        #region POSShipping

        public double RowsCountPOSTrShippingHd(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "ReferenceNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                             from _posTrShippingHd in this.db.POSTrShippingHds
                             where (SqlMethods.Like(_posTrShippingHd.TransNmbr.ToString(), _pattern1))
                                && (SqlMethods.Like(_posTrShippingHd.ReferenceNo.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             select _posTrShippingHd
                        ).Count();

            _result = _query;

            return _result;
        }

        public POSTrShippingHd GetSinglePOSTrShippingHd(String _prmCode)
        {
            POSTrShippingHd _result = new POSTrShippingHd();
            try
            {
                _result = this.db.POSTrShippingHds.FirstOrDefault(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public POSTrShippingDt GetSinglePOSTrShippingDt(String _prmCode, Int32 _prmItemNo)
        {
            POSTrShippingDt _result = new POSTrShippingDt();
            try
            {
                _result = this.db.POSTrShippingDts.FirstOrDefault(_temp => _temp.TransNmbr == _prmCode & _temp.ItemNo == _prmItemNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        //public List<POSTrShippingHd> GetListShippingSendToCashier(String _prmCustID, String _prmSearchBy, String _prmSearchText)
        //{
        //    String _pattern1 = "%";
        //    String _pattern2 = "%";

        //    if (_prmCustID.Trim() != "")
        //    {
        //        _pattern1 = "%" + _prmCustID + "%";
        //    }

        //    if (_prmSearchBy.Trim() == "JobOrder")
        //    {
        //        _pattern2 = "%" + _prmSearchText + "%";
        //    }

        //    List<POSTrShippingHd> _result = new List<POSTrShippingHd>();
        //    try
        //    {
        //        var _query = (
        //                        from _shippingHd in this.db.POSTrShippingHds
        //                        where SqlMethods.Like((_shippingHd.ReferenceNo ?? "").Trim().ToLower(), _pattern1.Trim().ToLower())
        //                            && SqlMethods.Like((_shippingHd.TransNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower())
        //                            && _shippingHd.Status == POSTrShippingDataMapper.GetStatus(POSTrShippingStatus.SendToCashier)
        //                            && _shippingHd.DoneSettlement == POSTrSettlementDataMapper.GetDoneSettlement(POSDoneSettlementStatus.NotYet)
        //                            && _shippingHd.IsVOID == false
        //                        //&& (_shippingHd.DeliveryOrderReff == "" || _shippingHd.DeliveryOrderReff == null)
        //                        select _shippingHd
        //                    );
        //        foreach (var _row in _query)
        //            _result.Add(_row);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return _result;
        //}

        //public List<POSTrShippingHd> GetListShippingPayNotDelivered(String _prmCustID, String _prmSearchBy, String _prmSearchText)
        //{
        //    String _pattern1 = "%";
        //    String _pattern2 = "%";
        //    String _pattern3 = "%";

        //    if (_prmCustID.Trim() != "")
        //    {
        //        _pattern1 = "%" + _prmCustID + "%";
        //    }

        //    if (_prmSearchBy.Trim() == "JobOrder")
        //    {
        //        _pattern2 = "%" + _prmSearchText + "%";
        //    }
        //    else if (_prmSearchBy.Trim() == "CustName")
        //    {
        //        _pattern3 = "%" + _prmSearchText + "%";
        //    }

        //    List<POSTrShippingHd> _result = new List<POSTrShippingHd>();
        //    try
        //    {
        //        var _query = (
        //                        from _shippingHd in this.db.POSTrShippingHds
        //                        join _settlementRef in this.db.POSTrSettlementDtRefTransactions
        //                            on _shippingHd.TransNmbr equals _settlementRef.ReferenceNmbr
        //                        join _settlement in this.db.POSTrSettlementHds
        //                            on _settlementRef.TransNmbr equals _settlement.TransNmbr
        //                        join _settlementDtProducy in this.db.POSTrSettlementDtProducts
        //                            on _shippingHd.TransNmbr equals _settlementDtProducy.ReffNmbr
        //                        into joined
        //                        from _settlementDtProducy in joined.DefaultIfEmpty()
        //                        where SqlMethods.Like((_shippingHd.ReferenceNo ?? "").Trim().ToLower(), _pattern1.Trim().ToLower())
        //                            && SqlMethods.Like((_settlement.TransNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower())
        //                            && SqlMethods.Like((_shippingHd.CustName ?? "").Trim().ToLower(), _pattern3.Trim().ToLower())
        //                            && ((_settlement.Status == POSTrSettlementDataMapper.GetStatus(POSTrSettlementStatus.Posted)) || _shippingHd.DPPaid > 0) //&& _settlementDtProducy.FgStock == 'Y'
        //                            && _settlementRef.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Shipping)
        //                            && _shippingHd.IsVOID == false
        //                        //&& ((_shippingHd.DeliveryStatus == null || _shippingHd.DeliveryStatus == false) ? false : true) == POSTrSettlementDataMapper.GetDeliveryStatus(POSDeliveryStatus.NotYetDelivered)
        //                        select _shippingHd
        //                    ).Distinct();
        //        foreach (var _row in _query)
        //            _result.Add(_row);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return _result;
        //}

        public List<POSTrShippingHd> GetListPOSTrShippingHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSTrShippingHd> _result = new List<POSTrShippingHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "ReferenceNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _posTrShippingHd in this.db.POSTrShippingHds
                                join _posTrShippingDt in this.db.POSTrShippingDts
                                on _posTrShippingHd.TransNmbr equals _posTrShippingDt.TransNmbr
                                where (SqlMethods.Like(_posTrShippingHd.TransNmbr.ToString(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_posTrShippingHd.ReferenceNo.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && _posTrShippingHd.IsVOID == false
                                   && _posTrShippingDt.PaymentType == '1'
                                orderby _posTrShippingHd.TransDate descending
                                select _posTrShippingHd
                            ).Distinct().Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

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

        public List<POSTrShippingDt> GetListShippingDtByTransNmbr(String _prmTransNmbr)
        {
            List<POSTrShippingDt> _result = null;
            try
            {
                var _query = (
                                from _trShippingDt in this.db.POSTrShippingDts
                                where _trShippingDt.TransNmbr == _prmTransNmbr
                                && _trShippingDt.IsVoid == false
                                select _trShippingDt
                            );
                if (_query.Count() > 0)
                    _result = new List<POSTrShippingDt>();
                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Boolean SetDelivery(String _prmTransNmbr, Boolean _prmDeliverValue)
        {
            Boolean _result = false;

            try
            {
                POSTrShippingHd _shippingHd = this.db.POSTrShippingHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                _shippingHd.DeliveryStatus = _prmDeliverValue;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        public Boolean SetVOID(String _prmTransNmbr, String _prmReasonCode, Boolean _prmVOIDValue)
        {
            Boolean _result = false;
            POSReasonBL _reasonBL = new POSReasonBL();
            try
            {
                POSTrShippingHd _shippingHd = this.db.POSTrShippingHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                if (IsNumeric(_prmReasonCode))
                {
                    _shippingHd.Remark = _reasonBL.GetReasonByCode(Convert.ToInt32(_prmReasonCode));
                }
                else
                {
                    _shippingHd.Remark = _prmReasonCode;
                }

                _shippingHd.IsVOID = _prmVOIDValue;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        public Boolean SetVOIDDt(String _prmTransNmbr, Boolean _prmVOIDValue)
        {
            Boolean _result = false;
            try
            {
                List<POSTrShippingDt> _listPOSShippingDt = this.GetListShippingDtByTransNmbr(_prmTransNmbr);
                foreach (var _item in _listPOSShippingDt)
                {
                    POSTrShippingDt _shippingDt = this.db.POSTrShippingDts.Single(_temp => _temp.TransNmbr == _item.TransNmbr & _temp.ItemNo == _item.ItemNo);
                    _shippingDt.IsVoid = _prmVOIDValue;
                }
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        public Boolean DeletePOSTrShippingDt(String _prmTransNmbr, Int32 _prmItemNo)
        {
            Boolean _result = false;

            try
            {
                POSTrShippingDt _posTrShippingDt = this.db.POSTrShippingDts.FirstOrDefault(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower() & _temp.ItemNo == _prmItemNo);
                this.db.POSTrShippingDts.DeleteOnSubmit(_posTrShippingDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        public string GetRefNmbrInterByTransType(string _prmCode, string _prmTranstype)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _posTrSettlementRefTransac in this.db.POSTrSettlementDtRefTransactions
                                where _posTrSettlementRefTransac.TransNmbr == _prmCode
                                && _posTrSettlementRefTransac.TransType == _prmTranstype
                                select _posTrSettlementRefTransac.ReferenceNmbr
                              ).FirstOrDefault();


                String RefNmbr = _query;

                var _query2 = (
                                from _posTrSettlemenRefTrans in this.db.POSTrSettlementDtRefTransactions
                                join _posShippingHD in this.db.POSTrShippingHds
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posShippingHD.TransNmbr
                                where _posShippingHD.TransNmbr == RefNmbr
                                select _posShippingHD.ReferenceNo
                                ).FirstOrDefault();

                _result = _query2;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GetTransnumbSettlement(String _transNmbr)
        {
            String _result = "";
            try
            {
                //var _query = (
                //                from _posTrSettlementRefTransac in this.db.POSTrSettlementDtRefTransactions
                //                where _posTrSettlementRefTransac.ReferenceNmbr == _transNmbr
                //                && _posTrSettlementRefTransac.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Shipping)
                //                select _posTrSettlementRefTransac.TransNmbr
                //              ).FirstOrDefault();

                //String _transNmbrSettlementRef = _query;
                //var _query2 = (
                //                from _posTrSettlementHds in this.db.POSTrSettlementHds
                //                where _posTrSettlementHds.TransNmbr == _transNmbrSettlementRef
                //                select _posTrSettlementHds.TransNmbr
                //              ).FirstOrDefault();

                //_result = _query2;
                var _query = (
                                from _posTrSettlementRefTransac in this.db.POSTrSettlementDtRefTransactions
                                join _posTrSettlementHd in this.db.POSTrSettlementHds
                                on _posTrSettlementRefTransac.TransNmbr equals _posTrSettlementHd.TransNmbr
                                where _posTrSettlementRefTransac.ReferenceNmbr == _transNmbr
                                && _posTrSettlementRefTransac.TransType.ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Shipping).ToLower()
                                select _posTrSettlementHd.FileNmbr
                              ).FirstOrDefault();

                _result = _query;
            }

            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetMemberName(string _prmMemberCode)
        {
            if (this.db.MsMembers.Where(a => a.MemberCode == _prmMemberCode).Count() > 0)
                return this.db.MsMembers.Single(a => a.MemberCode == _prmMemberCode).MemberName;
            else return "";
        }

        //public Boolean SendToCashier(String _prmTransNmbr)
        //{
        //    Boolean _result = false;
        //    try
        //    {
        //        using (TransactionScope _scope = new TransactionScope())
        //        {
        //            POSTrShippingHd _posTrShippingHd = this.GetSinglePOSTrShippingHd(_prmTransNmbr);
        //            foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(Convert.ToDateTime(_posTrShippingHd.TransDate).Year, Convert.ToDateTime(_posTrShippingHd.TransDate).Month, POSTransTypeDataMapper.GetTransType(POSTransType.Shipping), this._companyTag, ""))
        //            {
        //                _posTrShippingHd.FileNmbr = item.Number;
        //            }

        //            _result = true;

        //            this.db.SubmitChanges();

        //            _scope.Complete();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return _result;
        //}

        public string GetMemberNameByTransType(string _prmCode, string _prmTranstype)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _posTrSettlementRefTransac in this.db.POSTrSettlementDtRefTransactions
                                where _posTrSettlementRefTransac.TransNmbr == _prmCode
                                && _posTrSettlementRefTransac.TransType == _prmTranstype
                                select _posTrSettlementRefTransac.ReferenceNmbr
                              ).FirstOrDefault();


                String RefNmbr = _query;

                var _query2 = (
                                from _posTrSettlemenRefTrans in this.db.POSTrSettlementDtRefTransactions
                                join _posShippingHD in this.db.V_POSReferencePayedLists
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posShippingHD.TransNmbr
                                where _posShippingHD.TransNmbr == RefNmbr
                                && _posShippingHD.TransType == _prmTranstype
                                select _posShippingHD.CustName
                                ).FirstOrDefault();

                _result = _query2;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Char GetDonePayByTransType(string _prmCode, string _prmTranstype)
        {
            Char _result = 'N';

            try
            {
                var _query = (
                                from _posTrSettlementRefTransac in this.db.POSTrSettlementDtRefTransactions
                                where _posTrSettlementRefTransac.TransNmbr == _prmCode
                                && _posTrSettlementRefTransac.TransType == _prmTranstype
                                select _posTrSettlementRefTransac.ReferenceNmbr
                              ).FirstOrDefault();


                String RefNmbr = _query;

                var _query2 = (
                                from _posTrSettlemenRefTrans in this.db.POSTrSettlementDtRefTransactions
                                join _posShippingHD in this.db.V_POSReferencePayedLists
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posShippingHD.TransNmbr
                                where _posShippingHD.TransNmbr == RefNmbr
                                && _posShippingHD.TransType == _prmTranstype
                                select _posShippingHD.DoneSettlement
                                ).FirstOrDefault();

                _result = ((_query2 == null) ? 'N' : 'Y');

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public POSMsShipping GetPOSMsShipping(string _prmVendorCode, string _prmShippingTypeCode, string _prmProductShape, string _prmCityCode)
        {
            POSMsShipping _result = null;
            //var _query = (
            //            from _posMsShipping in db.spPOS_MsShipping(_prmVendorCode, _prmShippingTypeCode, _prmProductShape, _prmCityCode)
            //            select _posMsShipping
            //        );

            //foreach (var _row in _query)
            //{
            //    //_result.Add(new POSMsShipping(_row.VendorCode,_row.ShippingTypeCode,_row.ProductShape,_row.CityCode,_row.VendorName,_row.ShippingTypeName,_row.CityName,_row.Percentage,_row.Price1,_row.Price2,_row.EstimationTime));
            //    _result = new POSMsShipping(_row.VendorCode, _row.ShippingTypeCode, _row.ProductShape, _row.CityCode, _row.VendorName, _row.ShippingTypeName, _row.CityName, _row.Percentage, _row.Price1, _row.Price2, _row.EstimationTime, _row.UnitCode);
            //}
            this.db.spPOS_MsShipping(_prmVendorCode, _prmShippingTypeCode, _prmProductShape, _prmCityCode);
            var _query = (
                            from _temp in db.General_TemporaryTables
                            where _temp.TableName == "spPOS_MsShippingResult"
                            && _temp.StoreProcedure == "spPOS_MsShipping"
                            && _temp.PrimaryKey1 == _prmVendorCode
                            && _temp.PrimaryKey2 == _prmShippingTypeCode
                            && _temp.PrimaryKey3 == _prmProductShape
                            && _temp.PrimaryKey4 == _prmCityCode
                            select _temp);

            foreach (var _row in _query)
            {
                _result = new POSMsShipping(_row.Field1, _row.Field2, _row.Field3, _row.Field4, _row.Field5, _row.Field6, _row.Field7, Convert.ToDecimal(_row.Field8), Convert.ToDecimal(_row.Field9), Convert.ToDecimal(_row.Field10), _row.Field11, _row.Field12);
            }
            return _result;
        }

        public General_TemporaryTable GetPOSMsZone(string _prmZoneCode, string _prmProductShape, double? _prmWeight, string _prmCountryCode)
        {
            General_TemporaryTable _result = null;
            this.db.spPOS_MsShippingZone(_prmZoneCode, _prmProductShape, _prmWeight, _prmCountryCode);

            var _query = (
                            from _temp in db.General_TemporaryTables
                            where _temp.TableName == "spPOS_MsShippingZoneResult"
                            && _temp.StoreProcedure == "spPOS_MsShippingZone"
                            && _temp.PrimaryKey1 == _prmZoneCode
                            && _temp.PrimaryKey2 == _prmProductShape
                            select _temp
                         );
            foreach (var _row in _query)
            {
                _result = _row;
            }
            return _result;
        }

        public List<POSMsShippingType> GetShippingType(string _prmVendorCode)
        {
            List<POSMsShippingType> _result = null;

            var _query = (
                        from _posMsShippingType in db.POSMsShippingTypes
                        join _posMsShippingVendorDt in db.POSMsShippingVendorDts
                        on _posMsShippingType.ShippingTypeCode equals _posMsShippingVendorDt.ShippingZonaTypeCode
                        where _posMsShippingType.FgActive == 'Y'
                        & _posMsShippingVendorDt.FgActive == 'Y'
                        & _posMsShippingVendorDt.VendorCode == _prmVendorCode
                        select _posMsShippingType
                    ).Distinct();

            if (_query.Count() > 0)
                _result = new List<POSMsShippingType>();

            foreach (var _row in _query)
            {
                _result.Add(_row);
            }
            return _result;
        }

        public List<POSMsZone> GetShippingZone(string _prmVendorCode)
        {
            List<POSMsZone> _result = null;

            var _query = (
                        from _posMsZone in db.POSMsZones
                        join _posMsShippingVendorDt in db.POSMsShippingVendorDts
                        on _posMsZone.ZoneCode equals _posMsShippingVendorDt.ShippingZonaTypeCode
                        where _posMsZone.FgActive == 'Y'
                        & _posMsShippingVendorDt.FgActive == 'Y'
                        & _posMsShippingVendorDt.VendorCode == _prmVendorCode
                        select _posMsZone
                    );

            if (_query.Count() > 0)
                _result = new List<POSMsZone>();

            foreach (var _row in _query)
            {
                _result.Add(_row);
            }
            return _result;
        }

        public List<V_POSMsShipping> GetShippingZoneType(string _prmVendorCode, string _prmCountryCode, string _prmCityCode)
        {
            List<V_POSMsShipping> _result = null;
            string _shippingZoneType = "";
            var _query = (
                                from _vPOSMsShipping in this.db.V_POSMsShippings
                                where _vPOSMsShipping.VendorCode == _prmVendorCode
                                && _vPOSMsShipping.CountryCode == _prmCountryCode
                                && _vPOSMsShipping.CityCode == _prmCityCode
                                orderby _vPOSMsShipping.VendorName
                                select _vPOSMsShipping
                            ).Distinct();

            if (_query.Count() > 0)
                _result = new List<V_POSMsShipping>();

            foreach (var _row in _query)
            {
                if (_shippingZoneType != _row.ShippingZonaTypeName)
                {
                    _shippingZoneType = _row.ShippingZonaTypeName;
                    _result.Add(_row);
                }
            }

            return _result;
        }

        public bool AddPOSTrShippingHd(POSTrShippingHd _prmPOSTrShippingHd)
        {
            bool _result = false;

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmPOSTrShippingHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }
                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                this.db.POSTrShippingHds.InsertOnSubmit(_prmPOSTrShippingHd);

                var _query = (
                            from _temp in this.db.Temporary_TransactionNumbers
                            where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                            select _temp
                          );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditPOSTrShippingHd(POSTrShippingHd _prmPOSTrShippingHd)
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

        public bool AddPOSTrShippingDt(POSTrShippingDt _prmPOSTrShippingDt)
        {
            bool _result = false;

            try
            {
                this.db.POSTrShippingDts.InsertOnSubmit(_prmPOSTrShippingDt);
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditPOSTrShippingDt(POSTrShippingDt _prmPOSTrShippingDt)
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

        public int RowsCountPOSTrShippingDt(string _prmCode)
        {
            int _result = 0;

            _result = this.db.POSTrShippingDts.Where(_row => _row.TransNmbr == _prmCode).Count();

            return _result;
        }

        public Decimal GetMaxWeightDocument(string _prmZoneCode, string _prmProductShape)
        {
            Decimal _result = 0;
            POSMsZonePrice _posMsZonePrice = this.db.POSMsZonePrices.OrderByDescending(_row => _row.Weight).FirstOrDefault(_row => _row.ZoneCode == _prmZoneCode && _row.ProductShape == _prmProductShape && _row.FgActive == 'Y');
            _result = (_posMsZonePrice == null) ? 0 : _posMsZonePrice.Weight;
            return _result;
        }

        public List<V_POSMsShipping> GetListByCountryCity(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<V_POSMsShipping> _result = new List<V_POSMsShipping>();

            try
            {
                string[] _split = _prmKeyword.Split(',');
                string _pattern1 = _split[0];
                string _pattern2 = _split[1];
                string _vendorName = "";

                var _query = (
                                from _vPOSMsShipping in this.db.V_POSMsShippings
                                where _vPOSMsShipping.CountryCode == _pattern1
                                && _vPOSMsShipping.CityCode == _pattern2
                                orderby _vPOSMsShipping.VendorName
                                select _vPOSMsShipping
                            ).Distinct().Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);
                foreach (var _row in _query)
                {
                    if (_vendorName != _row.VendorName)
                    {
                        _vendorName = _row.VendorName;
                        _result.Add(_row);
                    }
                }

                //if (_prmCategory == "FgHome")
                //{
                //    var _query = (
                //                    from _posMsShippingVendor in this.db.POSMsShippingVendors
                //                    join _posMsShippingVendorDt in db.POSMsShippingVendorDts
                //                        on _posMsShippingVendor.VendorCode equals _posMsShippingVendorDt.VendorCode
                //                    join _posMsShippingTypeDt in db.POSMsShippingTypeDts
                //                        on _posMsShippingVendorDt.ShippingZonaTypeCode equals _posMsShippingTypeDt.ShippingTypeCode
                //                    where (_posMsShippingVendor.FgZone == null || _posMsShippingVendor.FgZone == 'N')
                //                    && _posMsShippingVendor.FgActive == 'Y'
                //                    && _posMsShippingVendorDt.FgActive == 'Y'
                //                    && _posMsShippingTypeDt.FgActive == 'Y'
                //                    orderby _posMsShippingVendor.VendorName
                //                    select _posMsShippingVendor
                //                ).Distinct().Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);
                //    foreach (var _row in _query)
                //    {
                //        _result.Add(_row);
                //    }
                //}
                //else
                //{
                //string _pattern1 = "%%";

                //if (_prmCategory == "CountryCode")
                //{
                //    _pattern1 = _prmKeyword.ToUpper();
                //}
                //var _query = (
                //                from _posMsShippingVendor in this.db.POSMsShippingVendors
                //                join _posMsShippingVendorDt in db.POSMsShippingVendorDts
                //                    on _posMsShippingVendor.VendorCode equals _posMsShippingVendorDt.VendorCode
                //                join _posMsZoneCountry in db.POSMsZoneCountries
                //                    on _posMsShippingVendorDt.ShippingZonaTypeCode equals _posMsZoneCountry.ZoneCode
                //                where _posMsShippingVendor.FgZone == 'Y'
                //                && _posMsShippingVendor.FgActive == 'Y'
                //                && _posMsShippingVendorDt.FgActive == 'Y'
                //                && _posMsZoneCountry.FgActive == 'Y'
                //                && _posMsZoneCountry.CountryCode.ToUpper() == _pattern1
                //                orderby _posMsShippingVendor.VendorName
                //                select _posMsShippingVendor
                //            ).Distinct().Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);
                //foreach (var _row in _query)
                //{
                //    _result.Add(_row);
                //}
                //}

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public POSMsShippingVendor GetSinglePOSMsShippingVendor(string _prmCode)
        {
            POSMsShippingVendor _result = null;

            try
            {
                _result = this.db.POSMsShippingVendors.Single(_temp => _temp.VendorCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string PostingPOSTrShippingHd(string _prmTransNmbr, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                int _success = this.db.spPOS_ShippingPost(_prmTransNmbr, _prmuser, ref _errorMsg);

                if (_errorMsg == "")
                {
                    _result = "Posting Success";
                    this.db.SubmitChanges();
                }
                else
                {
                    _result = _errorMsg;
                }
            }
            catch (Exception ex)
            {
                _result = "Posting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }
            return _result;
        }

        public string UnpostingPOSTrShippingHd(string _prmTransNmbr, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                //int _success = this.db.spPOS_ShippingUnPost(_prmTransNmbr, _prmuser, ref _errorMsg);
                int _success = this.db.spPOS_ShippingUnPost(_prmTransNmbr, _prmuser, ref _errorMsg);

                if (_errorMsg == "")
                {
                    _result = "Unposting Success";
                }
                else
                {
                    _result = _errorMsg;
                }
            }
            catch (Exception ex)
            {
                _result = "Unposting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }

        #endregion

        ~ShippingBL()
        {
        }
    }
}
