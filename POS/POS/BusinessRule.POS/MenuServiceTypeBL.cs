using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;

namespace BusinessRule.POS
{
    public sealed class MenuServiceTypeBL : Base
    {
        public MenuServiceTypeBL()
        {
        }

        #region Member

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
                             from _posMsMenuServiceType in this.db.POSMsMenuServiceTypes
                             where (SqlMethods.Like(_posMsMenuServiceType.MenuServiceTypeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_posMsMenuServiceType.MenuServiceTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             select _posMsMenuServiceType
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<POSMsMenuServiceType> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsMenuServiceType> _result = new List<POSMsMenuServiceType>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
                _pattern1 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "Name")
                _pattern2 = "%" + _prmKeyword + "%";

            try
            {
                var _query = (
                                from _posMsMenuServiceType in this.db.POSMsMenuServiceTypes
                                where (SqlMethods.Like(_posMsMenuServiceType.MenuServiceTypeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_posMsMenuServiceType.MenuServiceTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _posMsMenuServiceType.MenuServiceTypeCode descending
                                select _posMsMenuServiceType
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

        public List<POSMsMenuServiceType> GetList()
        {
            List<POSMsMenuServiceType> _result = new List<POSMsMenuServiceType>();

            try
            {
                var _query = (
                                from _posMsMenuServiceType in this.db.POSMsMenuServiceTypes
                                orderby _posMsMenuServiceType.MenuServiceTypeCode ascending
                                select _posMsMenuServiceType
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

        public bool DeleteMulti(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    POSMsMenuServiceType _posMsMenuServiceType = this.db.POSMsMenuServiceTypes.Single(_temp => _temp.MenuServiceTypeCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.POSMsMenuServiceTypes.DeleteOnSubmit(_posMsMenuServiceType);
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

        public POSMsMenuServiceType GetSingle(string _prmCode)
        {
            POSMsMenuServiceType _result = null;

            try
            {
                _result = this.db.POSMsMenuServiceTypes.FirstOrDefault(_temp => _temp.MenuServiceTypeCode.ToLower() == _prmCode.ToLower() || _temp.MenuServiceTypeName.ToLower() == _prmCode.ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public string GetPOSDiscountConfigByCode(string _prmCode)
        //{
        //    string _result = "";

        //    try
        //    {
        //        var _query = (
        //                        from _posMsMenuServiceType in this.db.POSMsMenuServiceTypes
        //                        where _posMsMenuServiceType.MenuServiceTypeCode == _prmCode
        //                        select new
        //                        {
        //                            MenuServiceTypeName = _posMsMenuServiceType.MenuServiceTypeName
        //                        }
        //                      );

        //        foreach (var _obj in _query)
        //        {
        //            _result = _obj.MenuServiceTypeName;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public bool CekToPOSMSDiscountConfigMember(String _prmCode)
        //{
        //    bool _result = false;
        //    try
        //    {
        //        var _query = (from _posMSDiscountConfigMember in this.db.POSMsDiscountConfigMembers
        //                      where _posMSDiscountConfigMember.DiscountConfigCode == _prmCode
        //                      select _posMSDiscountConfigMember
        //            ).Count();

        //        if (_query == 0)
        //            _result = true;
        //    }
        //    catch (Exception ex)
        //    {   
        //        throw ex;
        //    }
        //    return _result;
        //}

        public bool EditPOSMsMenuServiceType(POSMsMenuServiceType _prmPOSMsMenuServiceTypeDt)
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

        #region Detail

        public List<POSMsMenuServiceTypeDt> GetListPOSMsMenuServiceTypeDt(string _prmCode)
        {
            List<POSMsMenuServiceTypeDt> _result = new List<POSMsMenuServiceTypeDt>();

            try
            {
                var _query = (
                                from _posMsMenuServiceTypeDt in this.db.POSMsMenuServiceTypeDts
                                where _posMsMenuServiceTypeDt.MenuServiceTypeCode == _prmCode
                                orderby _posMsMenuServiceTypeDt.ProductSubGroup ascending
                                select _posMsMenuServiceTypeDt
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

        public POSMsMenuServiceTypeDt GetSinglePOSMsMenuServiceTypeDt(string _prmCode, String _prmMemberType)
        {
            POSMsMenuServiceTypeDt _result = null;

            try
            {
                _result = this.db.POSMsMenuServiceTypeDts.Single(_temp => _temp.MenuServiceTypeCode == _prmMemberType && _temp.ProductSubGroup == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditPOSMsMenuServiceTypeDt(POSMsMenuServiceTypeDt _prmPOSMsMenuServiceTypeDt)
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

        public bool DeleteMultiPOSMsMenuServiceTypeDt(string[] _prmMemberType, string _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmMemberType.Length; i++)
                {
                    POSMsMenuServiceTypeDt _posMsMenuServiceTypeDt = this.db.POSMsMenuServiceTypeDts.Single(_temp => _temp.ProductSubGroup == _prmMemberType[i] && _temp.MenuServiceTypeCode.Trim().ToLower() == _prmCode.Trim().ToLower());

                    this.db.POSMsMenuServiceTypeDts.DeleteOnSubmit(_posMsMenuServiceTypeDt);
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

        public bool AddPOSMsMenuServiceTypeDt(POSMsMenuServiceTypeDt _prmPOSMsMenuServiceTypeDt)
        {
            bool _result = false;

            try
            {
                this.db.POSMsMenuServiceTypeDts.InsertOnSubmit(_prmPOSMsMenuServiceTypeDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProductSubGroup> GetListDDL()
        {
            List<MsProductSubGroup> _result = new List<MsProductSubGroup>();

            try
            {
                var _query = (
                                from _msProductSubGroup in this.db.MsProductSubGroups
                                orderby _msProductSubGroup.ProductSubGrpCode ascending
                                select new
                                {
                                    ProductSubGrpCode = _msProductSubGroup.ProductSubGrpCode,
                                    ProductSubGrpName = _msProductSubGroup.ProductSubGrpName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsProductSubGroup(_row.ProductSubGrpCode, _row.ProductSubGrpName));

                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsWarehouse> GetListDDLWareHouse()
        {
            List<MsWarehouse> _result = new List<MsWarehouse>();

            try
            {
                var _query = (
                                from _msWarehouse in this.db.MsWarehouses
                                orderby _msWarehouse.WrhsCode ascending
                                select new
                                {
                                    WrhsCode = _msWarehouse.WrhsCode,
                                    WrhsName = _msWarehouse.WrhsName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsWarehouse(_row.WrhsCode, _row.WrhsName));

                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsWrhsLocation> GetListDDLWareHouseLocation(String _prmWarehouse)
        {
            List<MsWrhsLocation> _result = new List<MsWrhsLocation>();

            try
            {
                var _query = (
                                from _msWarehouseLocation in this.db.MsWarehouse_MsWrhsLocations
                                join _msWrhsLocation in this.db.MsWrhsLocations
                                on _msWarehouseLocation.WLocation equals _msWrhsLocation.WLocationCode
                                where _msWarehouseLocation.WrhsCode == _prmWarehouse
                                orderby _msWarehouseLocation.WrhsCode ascending
                                select _msWrhsLocation
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

        public List<MsWarehouse> GetListDDLWareHouseDepositIn()
        {
            List<MsWarehouse> _result = new List<MsWarehouse>();

            try
            {
                var _query = (
                                from _msWrhsLocation in this.db.MsWrhsLocations
                                join _msWarehouseLocation in this.db.MsWarehouse_MsWrhsLocations
                                on _msWrhsLocation.WLocationCode equals _msWarehouseLocation.WLocation
                                join _msWarehouse in this.db.MsWarehouses
                                on _msWarehouseLocation.WrhsCode equals _msWarehouse.WrhsCode
                                where _msWarehouse.WrhsType == 1
                                orderby _msWarehouse.WrhsCode ascending
                                select _msWarehouse
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

        public List<MsWrhsLocation> GetListDDLDepositLocation(String _prmWarehouse)
        {
            List<MsWrhsLocation> _result = new List<MsWrhsLocation>();

            try
            {
                var _query = (
                                from _msWrhsLocation in this.db.MsWrhsLocations
                                join _msWarehouseLocation in this.db.MsWarehouse_MsWrhsLocations
                                on _msWrhsLocation.WLocationCode equals _msWarehouseLocation.WLocation
                                where _msWarehouseLocation.WrhsCode == _prmWarehouse
                                //&& _msWarehouse.WrhsType == 1
                                orderby _msWarehouseLocation.WrhsCode ascending
                                select _msWrhsLocation
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

        public string GetWarehouseNamebyCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msWarehouse in this.db.MsWarehouses
                                //join _msUnit in this.db.MsUnits
                                //    on _msWarehouse.Unit equals _msUnit.UnitCode
                                where _msWarehouse.WrhsCode == _prmCode
                                select new
                                {
                                    WrhsName = _msWarehouse.WrhsName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.WrhsName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetNameSubProductbyCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msProductSubGroup in this.db.MsProductSubGroups
                                join _msMenuServiceTypeDt in this.db.POSMsMenuServiceTypeDts
                                on _msProductSubGroup.ProductSubGrpCode equals _msMenuServiceTypeDt.ProductSubGroup
                                where _msProductSubGroup.ProductSubGrpCode == _prmCode
                                select new
                                {
                                    ProductSubGrpName = _msProductSubGroup.ProductSubGrpName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.ProductSubGrpName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GetWLocationNameByWLocationCode(string _prmWLocationCode)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _msWrhsLocation in this.db.MsWrhsLocations
                                where _msWrhsLocation.WLocationCode == _prmWLocationCode
                                select new
                                {
                                    WLocationName = _msWrhsLocation.WLocationName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.WLocationName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~MenuServiceTypeBL()
        {
        }
    }
}
