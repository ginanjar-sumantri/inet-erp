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
    public sealed class WarehouseBL : Base
    {
        public WarehouseBL()
        {

        }

        #region Warehouse
        public double RowsCountWarehouse(string _prmCategory, string _prmKeyword)
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
                    from _msWarehouse in this.db.MsWarehouses
                    where (SqlMethods.Like(_msWarehouse.WrhsCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msWarehouse.WrhsName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                    select _msWarehouse

                ).Count();

            _result = _query;
            return _result;
        }

        public List<MsWarehouse> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsWarehouse> _result = new List<MsWarehouse>();
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
                                from _msWarehouse in this.db.MsWarehouses
                                where (SqlMethods.Like(_msWarehouse.WrhsCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && (SqlMethods.Like(_msWarehouse.WrhsName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msWarehouse.UserDate descending
                                select new
                                {
                                    WrhsCode = _msWarehouse.WrhsCode,
                                    WrhsName = _msWarehouse.WrhsName,
                                    WrhsGroup = _msWarehouse.WrhsGroup,
                                    WrhsArea = _msWarehouse.WrhsArea,
                                    FgActive = _msWarehouse.FgActive
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { WrhsCode = this._string, WrhsName = this._string, WrhsGroup = this._string, WrhsArea = this._string, FgActive = this._char });

                    _result.Add(new MsWarehouse(_row.WrhsCode, _row.WrhsName, _row.WrhsGroup, _row.WrhsArea, _row.FgActive));
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
                    MsWarehouse _msWarehouse = this.db.MsWarehouses.Single(_temp => _temp.WrhsCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_msWarehouse != null)
                    {
                        var _query = (from _detail in this.db.MsWarehouse_MsWrhsLocations
                                      where _detail.WrhsCode.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                      select _detail);

                        this.db.MsWarehouse_MsWrhsLocations.DeleteAllOnSubmit(_query);

                        this.db.MsWarehouses.DeleteOnSubmit(_msWarehouse);
                    }
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

        public MsWarehouse GetSingle(string _prmCode)
        {
            MsWarehouse _result = null;

            try
            {
                _result = this.db.MsWarehouses.Single(_temp => _temp.WrhsCode.Trim().ToLower() == _prmCode.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetWarehouseNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msWarehouse in this.db.MsWarehouses
                                where _msWarehouse.WrhsCode.Trim().ToLower() == _prmCode.Trim().ToLower()
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

        public bool Add(MsWarehouse _prmMsWarehouse)
        {
            bool _result = false;

            try
            {
                this.db.MsWarehouses.InsertOnSubmit(_prmMsWarehouse);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(MsWarehouse _prmMsWarehouse)
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

        public List<MsWarehouse> GetListForDDL()
        {
            List<MsWarehouse> _result = new List<MsWarehouse>();

            try
            {
                var _query = (
                                from _msWarehouse in this.db.MsWarehouses
                                orderby _msWarehouse.WrhsName
                                select new
                                {
                                    WrhsCode = _msWarehouse.WrhsCode,
                                    WrhsName = _msWarehouse.WrhsName
                                }
                            ).Distinct();

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { WrhsCode = this._string, WrhsName = this._string });

                    _result.Add(new MsWarehouse(_row.WrhsCode, _row.WrhsName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsWarehouse> GetListForDDLActive()
        {
            List<MsWarehouse> _result = new List<MsWarehouse>();

            try
            {
                var _query = (
                                from _msWarehouse in this.db.MsWarehouses
                                where _msWarehouse.FgActive == WarehouseDataMapper.GetYesNo(YesNo.Yes)
                                orderby _msWarehouse.WrhsName
                                select new
                                {
                                    WrhsCode = _msWarehouse.WrhsCode,
                                    WrhsName = _msWarehouse.WrhsName
                                }
                            ).Distinct();

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

        public List<MsWarehouse> GetListForDDLNoSubled()
        {
            List<MsWarehouse> _result = new List<MsWarehouse>();

            try
            {
                var _query = (
                                from _msWarehouse in this.db.MsWarehouses
                                where (_msWarehouse.FgActive == WarehouseDataMapper.GetYesNo(YesNo.Yes)) && (_msWarehouse.FgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
                                orderby _msWarehouse.WrhsName
                                select new
                                {
                                    WrhsCode = _msWarehouse.WrhsCode,
                                    WrhsName = _msWarehouse.WrhsName
                                }
                            ).Distinct();

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

        public List<MsWarehouse> GetListForDDLCustomerSubled()
        {
            List<MsWarehouse> _result = new List<MsWarehouse>();

            try
            {
                var _query = (
                                from _msWarehouse in this.db.MsWarehouses
                                where (_msWarehouse.FgActive == WarehouseDataMapper.GetYesNo(YesNo.Yes)) && (_msWarehouse.FgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled) || _msWarehouse.FgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                                orderby _msWarehouse.WrhsName
                                select new
                                {
                                    WrhsCode = _msWarehouse.WrhsCode,
                                    WrhsName = _msWarehouse.WrhsName
                                }
                            ).Distinct();

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

        public List<MsWarehouse> GetListForDDLSupplierSubled()
        {
            List<MsWarehouse> _result = new List<MsWarehouse>();

            try
            {
                var _query = (
                                from _msWarehouse in this.db.MsWarehouses
                                where (_msWarehouse.FgActive == WarehouseDataMapper.GetYesNo(YesNo.Yes)) && (_msWarehouse.FgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled) || _msWarehouse.FgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                                orderby _msWarehouse.WrhsName
                                select new
                                {
                                    WrhsCode = _msWarehouse.WrhsCode,
                                    WrhsName = _msWarehouse.WrhsName
                                }
                            ).Distinct();

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

        public char GetWarehouseFgSubledByCode(string _prmCode)
        {
            char _result = ' ';

            try
            {
                var _query = (
                                from _msWarehouse in this.db.MsWarehouses
                                where _msWarehouse.WrhsCode.Trim().ToLower() == _prmCode.Trim().ToLower()
                                select new
                                {
                                    FgSubLed = (_msWarehouse.FgSubLed == null) ? ' ' : Convert.ToChar(_msWarehouse.FgSubLed)
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.FgSubLed;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public char GetWarehouseFgSubledByName(string _prmName)
        {
            char _result = ' ';

            try
            {
                var _query = (
                                from _msWarehouse in this.db.MsWarehouses
                                where _msWarehouse.WrhsName.Trim().ToLower() == _prmName.Trim().ToLower()
                                select new
                                {
                                    FgSubLed = (_msWarehouse.FgSubLed == null) ? ' ' : Convert.ToChar(_msWarehouse.FgSubLed)
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.FgSubLed;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        public List<MsWarehouse> GetWarehouseActiveAndWrhsArea(string _prmWrhsArea)
        {
            List<MsWarehouse> _result = new List<MsWarehouse>();

            try
            {
                var _query = (
                                from _msWarehouse in this.db.MsWarehouses
                                where _msWarehouse.FgActive == WarehouseDataMapper.GetYesNo(YesNo.Yes)
                                && _msWarehouse.WrhsArea == _prmWrhsArea
                                select new
                                {
                                    WrhsCode = _msWarehouse.WrhsCode,
                                    WrhsName = _msWarehouse.WrhsName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result.Add(new MsWarehouse(_obj.WrhsCode, _obj.WrhsName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public double RowsCountWarehouseReport(string _prmWrhsGroup, string _prmWrhsArea)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmWrhsGroup != "")
            {
                _pattern1 = "%" + _prmWrhsGroup + "%";
            }

            if (_prmWrhsArea != "")
            {
                _pattern2 = "%" + _prmWrhsArea + "%";
            }

            var _query =
                        (
                            from _msWrhs in this.db.MsWarehouses
                            where _msWrhs.FgActive == WarehouseDataMapper.GetYesNo(YesNo.Yes)
                               && (SqlMethods.Like(_msWrhs.WrhsGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msWrhs.WrhsArea.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msWrhs.WrhsName
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<MsWarehouse> GetListDDLWrhsForReport(string _prmWrhsGroup, string _prmWrhsArea, int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsWarehouse> _result = new List<MsWarehouse>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            string _pattern4 = "%%";
            string _pattern5 = "%%";

            if (_prmWrhsGroup != "")
            {
                _pattern1 = "%" + _prmWrhsGroup + "%";
            }

            if (_prmWrhsArea != "")
            {
                _pattern2 = "%" + _prmWrhsArea + "%";
            }

            if (_prmCategory == "Code")
            {
                _pattern4 = "%" + _prmKeyword + "%";
                _pattern5 = "%%";
            }
            else if (_prmCategory == "Name")
            {
                _pattern5 = "%" + _prmKeyword + "%";
                _pattern4 = "%%";
            }

            try
            {
                var _query = (
                                from _msWrhs in this.db.MsWarehouses
                                where _msWrhs.FgActive == WarehouseDataMapper.GetYesNo(YesNo.Yes)
                                    && (SqlMethods.Like(_msWrhs.WrhsGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msWrhs.WrhsArea.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (SqlMethods.Like(_msWrhs.WrhsCode.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                    && (SqlMethods.Like(_msWrhs.WrhsName.Trim().ToLower(), _pattern5.Trim().ToLower()))
                                orderby _msWrhs.WrhsName ascending
                                select new
                                {
                                    WrhsCode = _msWrhs.WrhsCode,
                                    WrhsName = _msWrhs.WrhsName + " - " + _msWrhs.WrhsCode
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

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

        public List<MsWarehouse> GetListDDLWrhsForReport(string _prmWrhsGroup, string _prmWrhsArea, int _prmReqPage, int _prmPageSize)
        {
            List<MsWarehouse> _result = new List<MsWarehouse>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmWrhsGroup != "")
            {
                _pattern1 = "%" + _prmWrhsGroup + "%";
            }

            if (_prmWrhsArea != "")
            {
                _pattern2 = "%" + _prmWrhsArea + "%";
            }

            try
            {
                var _query = (
                                from _msWrhs in this.db.MsWarehouses
                                where _msWrhs.FgActive == WarehouseDataMapper.GetYesNo(YesNo.Yes)
                                    && (SqlMethods.Like(_msWrhs.WrhsGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msWrhs.WrhsArea.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msWrhs.WrhsName ascending
                                select new
                                {
                                    WrhsCode = _msWrhs.WrhsCode,
                                    WrhsName = _msWrhs.WrhsName + " - " + _msWrhs.WrhsCode
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

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

        public List<MsWarehouse> GetListDDLWrhsForReport(string _prmWrhsGroup, string _prmWrhsArea)
        {
            List<MsWarehouse> _result = new List<MsWarehouse>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmWrhsGroup != "")
            {
                _pattern1 = "%" + _prmWrhsGroup + "%";
            }

            if (_prmWrhsArea != "")
            {
                _pattern2 = "%" + _prmWrhsArea + "%";
            }

            try
            {
                var _query = (
                                from _msWrhs in this.db.MsWarehouses
                                where _msWrhs.FgActive == WarehouseDataMapper.GetYesNo(YesNo.Yes)
                                    && (SqlMethods.Like(_msWrhs.WrhsGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msWrhs.WrhsArea.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msWrhs.WrhsName ascending
                                select new
                                {
                                    WrhsCode = _msWrhs.WrhsCode,
                                    WrhsName = _msWrhs.WrhsName + " - " + _msWrhs.WrhsCode
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

        #endregion

        #region WrhsGroup
        public double RowsCountWrhsGroup(string _prmCategory, string _prmKeyword)
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
                    from _msWrhsGroup in this.db.MsWrhsGroups
                    where (SqlMethods.Like(_msWrhsGroup.WrhsGroupCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msWrhsGroup.WrhsGroupName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                    select _msWrhsGroup

                ).Count();

            _result = _query;
            return _result;
        }

        public List<MsWrhsGroup> GetListWrhsGroup(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsWrhsGroup> _result = new List<MsWrhsGroup>();
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
                                from _msWrhsGroup in this.db.MsWrhsGroups
                                where (SqlMethods.Like(_msWrhsGroup.WrhsGroupCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                        && (SqlMethods.Like(_msWrhsGroup.WrhsGroupName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msWrhsGroup.UserDate descending
                                select new
                                {
                                    WrhsGroupCode = _msWrhsGroup.WrhsGroupCode,
                                    WrhsGroupName = _msWrhsGroup.WrhsGroupName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { WrhsGroupCode = this._string, WrhsGroupName = this._string });

                    _result.Add(new MsWrhsGroup(_row.WrhsGroupCode, _row.WrhsGroupName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsWrhsGroup> GetListWrhsGroupForDDL()
        {
            List<MsWrhsGroup> _result = new List<MsWrhsGroup>();

            try
            {
                var _query = (
                                from _msWrhsGroup in this.db.MsWrhsGroups
                                orderby _msWrhsGroup.WrhsGroupName
                                select new
                                {
                                    WrhsGroupCode = _msWrhsGroup.WrhsGroupCode,
                                    WrhsGroupName = _msWrhsGroup.WrhsGroupName
                                }
                            ).Distinct();

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { WrhsGroupCode = this._string, WrhsGroupName = this._string });

                    _result.Add(new MsWrhsGroup(_row.WrhsGroupCode, _row.WrhsGroupName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiWrhsGroup(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MsWrhsGroup _msWrhsGroup = this.db.MsWrhsGroups.Single(_temp => _temp.WrhsGroupCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsWrhsGroups.DeleteOnSubmit(_msWrhsGroup);
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

        public MsWrhsGroup GetSingleWrhsGroup(string _prmCode)
        {
            MsWrhsGroup _result = null;

            try
            {
                _result = this.db.MsWrhsGroups.Single(_temp => _temp.WrhsGroupCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetWrhsGroupNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msWrhsGroup in this.db.MsWrhsGroups
                                where _msWrhsGroup.WrhsGroupCode == _prmCode
                                select new
                                {
                                    WrhsGroupName = _msWrhsGroup.WrhsGroupName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.WrhsGroupName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddWrhsGroup(MsWrhsGroup _prmMsWrhsGroup)
        {
            bool _result = false;

            try
            {
                this.db.MsWrhsGroups.InsertOnSubmit(_prmMsWrhsGroup);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditWrhsGroup(MsWrhsGroup _prmMsWrhsGroup)
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

        #region WrhsArea
        public double RowsCountWrhsArea(string _prmCategory, string _prmKeyword)
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
                    from _msWrhsArea in this.db.MsWrhsAreas
                    where (SqlMethods.Like(_msWrhsArea.WrhsAreaCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msWrhsArea.WrhsAreaName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                    select _msWrhsArea

                ).Count();

            _result = _query;
            return _result;
        }

        public List<MsWrhsArea> GetListWrhsArea(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsWrhsArea> _result = new List<MsWrhsArea>();
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
                                from _msWrhsArea in this.db.MsWrhsAreas
                                where (SqlMethods.Like(_msWrhsArea.WrhsAreaCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && (SqlMethods.Like(_msWrhsArea.WrhsAreaName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msWrhsArea.UserDate descending
                                select new
                                {
                                    WrhsAreaCode = _msWrhsArea.WrhsAreaCode,
                                    WrhsAreaName = _msWrhsArea.WrhsAreaName,
                                    Address1 = _msWrhsArea.Address1,
                                    Address2 = _msWrhsArea.Address2
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { WrhsAreaCode = this._string, WrhsAreaName = this._string, Address1 = this._string, Address2 = this._string });

                    _result.Add(new MsWrhsArea(_row.WrhsAreaCode, _row.WrhsAreaName, _row.Address1, _row.Address2));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsWrhsArea> GetListWrhsAreaForDDL()
        {
            List<MsWrhsArea> _result = new List<MsWrhsArea>();

            try
            {
                var _query = (
                                from _msWrhsArea in this.db.MsWrhsAreas
                                orderby _msWrhsArea.WrhsAreaName
                                select new
                                {
                                    WrhsAreaCode = _msWrhsArea.WrhsAreaCode,
                                    WrhsAreaName = _msWrhsArea.WrhsAreaName
                                }
                            ).Distinct();

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { WrhsAreaCode = this._string, WrhsAreaName = this._string });

                    _result.Add(new MsWrhsArea(_row.WrhsAreaCode, _row.WrhsAreaName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsWrhsArea> GetListWrhsAreaDestForDDL(string _prmWarehouseCode)
        {
            List<MsWrhsArea> _result = new List<MsWrhsArea>();

            try
            {
                var _query = (
                                from _msWrhsArea in this.db.MsWrhsAreas
                                where _msWrhsArea.WrhsAreaCode != _prmWarehouseCode
                                orderby _msWrhsArea.WrhsAreaName
                                select new
                                {
                                    WrhsAreaCode = _msWrhsArea.WrhsAreaCode,
                                    WrhsAreaName = _msWrhsArea.WrhsAreaName
                                }
                            ).Distinct();

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { WrhsAreaCode = this._string, WrhsAreaName = this._string });

                    _result.Add(new MsWrhsArea(_row.WrhsAreaCode, _row.WrhsAreaName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiWrhsArea(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MsWrhsArea _msWrhsArea = this.db.MsWrhsAreas.Single(_temp => _temp.WrhsAreaCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsWrhsAreas.DeleteOnSubmit(_msWrhsArea);
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

        public MsWrhsArea GetSingleWrhsArea(string _prmCode)
        {
            MsWrhsArea _result = null;

            try
            {
                _result = this.db.MsWrhsAreas.Single(_temp => _temp.WrhsAreaCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetWrhsAreaNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msWrhsArea in this.db.MsWrhsAreas
                                where _msWrhsArea.WrhsAreaCode == _prmCode
                                select new
                                {
                                    WrhsAreaName = _msWrhsArea.WrhsAreaName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.WrhsAreaName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddWrhsArea(MsWrhsArea _prmMsWrhsArea)
        {
            bool _result = false;

            try
            {
                this.db.MsWrhsAreas.InsertOnSubmit(_prmMsWrhsArea);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditWrhsArea(MsWrhsArea _prmMsWrhsArea)
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

        #region WrhsLocation
        public double RowsCountWrhsLocation(string _prmCategory, string _prmKeyword)
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
                    from _msWrhsLocation in this.db.MsWrhsLocations
                    where (SqlMethods.Like(_msWrhsLocation.WLocationCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msWrhsLocation.WLocationName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                    select _msWrhsLocation

                ).Count();

            _result = _query;
            return _result;
        }

        public List<MsWrhsLocation> GetListWrhsLocation(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsWrhsLocation> _result = new List<MsWrhsLocation>();
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
                                from _msWrhsLocation in this.db.MsWrhsLocations
                                where (SqlMethods.Like(_msWrhsLocation.WLocationCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                        && (SqlMethods.Like(_msWrhsLocation.WLocationName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select new
                                {
                                    WLocationCode = _msWrhsLocation.WLocationCode,
                                    WLocationName = _msWrhsLocation.WLocationName


                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { WLocationCode = this._string, WLocationName = this._string });

                    _result.Add(new MsWrhsLocation(_row.WLocationCode, _row.WLocationName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsWrhsLocation> GetListWrhsLocationForDDL()
        {
            List<MsWrhsLocation> _result = new List<MsWrhsLocation>();

            try
            {
                var _query = (
                                from _msWrhsLocation in this.db.MsWrhsLocations
                                select new
                                {
                                    WLocationCode = _msWrhsLocation.WLocationCode,
                                    WLocationName = _msWrhsLocation.WLocationName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { WLocationCode = this._string, WLocationName = this._string });

                    _result.Add(new MsWrhsLocation(_row.WLocationCode, _row.WLocationName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiWrhsLocation(string[] _prmWLocationCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmWLocationCode.Length; i++)
                {
                    MsWrhsLocation _msWrhsLocation = this.db.MsWrhsLocations.Single(_temp => _temp.WLocationCode.Trim().ToLower() == _prmWLocationCode[i].Trim().ToLower());

                    this.db.MsWrhsLocations.DeleteOnSubmit(_msWrhsLocation);
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

        public MsWrhsLocation GetSingleWrhsLocation(string _prmWlocationCode)
        {
            MsWrhsLocation _result = null;

            try
            {
                _result = this.db.MsWrhsLocations.Single(_temp => _temp.WLocationCode == _prmWlocationCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddWrhsLocation(MsWrhsLocation _prmMswrhsLocation)
        {
            bool _result = false;

            try
            {
                this.db.MsWrhsLocations.InsertOnSubmit(_prmMswrhsLocation);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditWrhsLocation(MsWrhsLocation _prmMsStockType)
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

        public string GetWarehouseLocationNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msWrhsLocation in this.db.MsWrhsLocations
                                where _msWrhsLocation.WLocationCode.Trim().ToLower() == _prmCode.Trim().ToLower()
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

        public List<MsWrhsLocation> GetListWrhsLocationByCodeForDDL(string _prmCode)
        {
            List<MsWrhsLocation> _result = new List<MsWrhsLocation>();

            try
            {
                var _query = (
                                from _msWarehouse_MsWarehouseLoc in this.db.MsWarehouse_MsWrhsLocations
                                join _msWrhsLocation in this.db.MsWrhsLocations
                                    on _msWarehouse_MsWarehouseLoc.WLocation equals _msWrhsLocation.WLocationCode
                                where _msWarehouse_MsWarehouseLoc.WrhsCode.Trim().ToLower() == _prmCode.Trim().ToLower()
                                orderby _msWrhsLocation.WLocationName ascending
                                select new
                                {
                                    WLocationCode = _msWrhsLocation.WLocationCode,
                                    WLocationName = _msWrhsLocation.WLocationName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { WLocationCode = this._string, WLocationName = this._string });

                    _result.Add(new MsWrhsLocation(_row.WLocationCode, _row.WLocationName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsWrhsLocation> GetListWrhsLocationByNameForDDL(string _prmName)
        {
            List<MsWrhsLocation> _result = new List<MsWrhsLocation>();

            try
            {
                var _query = (
                                from _msWarehouse_MsWarehouseLoc in this.db.MsWarehouse_MsWrhsLocations
                                join _msWrhsLocation in this.db.MsWrhsLocations
                                    on _msWarehouse_MsWarehouseLoc.WLocation equals _msWrhsLocation.WLocationCode
                                join _msWareHouse in this.db.MsWarehouses 
                                    on _msWarehouse_MsWarehouseLoc.WrhsCode equals _msWareHouse.WrhsCode 
                                where _msWareHouse.WrhsName.Trim().ToLower() == _prmName.Trim().ToLower()
                                orderby _msWrhsLocation.WLocationName ascending
                                select new
                                {
                                    WLocationCode = _msWrhsLocation.WLocationCode,
                                    WLocationName = _msWrhsLocation.WLocationName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { WLocationCode = this._string, WLocationName = this._string });

                    _result.Add(new MsWrhsLocation(_row.WLocationCode, _row.WLocationName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool IsLocationCodeExists(String _prmLocationCode)
        {
            bool _result = false;

            try
            {
                var _query = from _wrhsLocation in this.db.MsWrhsLocations
                             where _wrhsLocation.WLocationCode == _prmLocationCode
                             select new
                             {
                                 _wrhsLocation.WLocationCode
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

        #region MsWarehouse_MsWrhsLocation
        public int RowsCountMsWarehouseWrhsLocation(string _prmCode)
        {
            int _result = 0;

            _result = this.db.MsWarehouse_MsWrhsLocations.Where(_row => _row.WrhsCode == _prmCode).Count();

            return _result;
        }

        public List<MsWarehouse_MsWrhsLocation> GetListMsWarehouseWrhsLocation(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<MsWarehouse_MsWrhsLocation> _result = new List<MsWarehouse_MsWrhsLocation>();

            try
            {
                var _query = (
                                from _msWrhsLocation in this.db.MsWarehouse_MsWrhsLocations
                                where _msWrhsLocation.WrhsCode == _prmCode
                                orderby _msWrhsLocation.UserDate descending
                                select new
                                {
                                    WrhsCode = _msWrhsLocation.WrhsCode,
                                    WLocation = _msWrhsLocation.WLocation,
                                    WLocationName = (
                                                    from _msWrhsLoc in this.db.MsWrhsLocations
                                                    where _msWrhsLoc.WLocationCode == _msWrhsLocation.WLocation
                                                    select _msWrhsLoc.WLocationName
                                    ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { WrhsCode = this._string, WLocation = this._string, WLocationName = this._string });

                    _result.Add(new MsWarehouse_MsWrhsLocation(_row.WrhsCode, _row.WLocation, _row.WLocationName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiMsWarehouseWrhsLocation(string[] _prmCode, string _prmWrhsCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MsWarehouse_MsWrhsLocation _msWrhsLocation = this.db.MsWarehouse_MsWrhsLocations.Single(_temp => _temp.WLocation.Trim().ToLower() == _prmCode[i].Trim().ToLower() && _temp.WrhsCode == _prmWrhsCode);

                    this.db.MsWarehouse_MsWrhsLocations.DeleteOnSubmit(_msWrhsLocation);
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

        public MsWarehouse_MsWrhsLocation GetSingleMsWarehouseWrhsLocation(string _prmWrhsLocCode, string _prmWrhsCode)
        {
            MsWarehouse_MsWrhsLocation _result = null;

            try
            {
                _result = this.db.MsWarehouse_MsWrhsLocations.Single(_temp => _temp.WrhsCode.Trim().ToLower() == _prmWrhsCode.Trim().ToLower() && _temp.WLocation == _prmWrhsLocCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddMsWarehouseWrhsLocation(MsWarehouse_MsWrhsLocation _prmMsWarehouse_MsWrhsLocation)
        {
            bool _result = false;

            try
            {
                this.db.MsWarehouse_MsWrhsLocations.InsertOnSubmit(_prmMsWarehouse_MsWrhsLocation);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditMsWarehouseWrhsLocation(MsWarehouse_MsWrhsLocation _prmMsWarehouse_MsWrhsLocation)
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

        public bool IsLocationExistInWarehouse(String _prmWarehouseCode, String _prmLocationCode)
        {
            bool _result = false;

            try
            {
                var _query = from _Wrhs_WrhsLocation in this.db.MsWarehouse_MsWrhsLocations
                             where _Wrhs_WrhsLocation.WrhsCode == _prmWarehouseCode
                                && _Wrhs_WrhsLocation.WLocation == _prmLocationCode
                             select new
                             {
                                 _Wrhs_WrhsLocation.WLocation
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

        ~WarehouseBL()
        {

        }
    }
}
