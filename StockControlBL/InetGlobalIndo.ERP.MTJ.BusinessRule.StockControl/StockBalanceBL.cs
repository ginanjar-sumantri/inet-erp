using System;
using System.Collections.Generic;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Transactions;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl
{
    public sealed class StockBalanceBL : Base
    {
        public StockBalanceBL()
        {
        }

        #region STCStockValue

        public double RowsCountStokBalance(string _prmProductCode)
        {

            double _result = 0;

            _result = (
                        from _stcStockValues in this.db.STCStockValues
                        join _msWarehouse in this.db.MsWarehouses
                            on _stcStockValues.WrhsCode equals _msWarehouse.WrhsCode
                        join _msProduct in this.db.MsProducts
                            on _stcStockValues.ProductCode equals _msProduct.ProductCode
                        join _msWrhsLocation in this.db.MsWrhsLocations
                            on _stcStockValues.LocationCode equals _msWrhsLocation.WLocationCode
                        join _msUnit in this.db.MsUnits
                            on _stcStockValues.UnitCode equals _msUnit.UnitCode
                        where _stcStockValues.ProductCode == _prmProductCode
                              && _stcStockValues.Qty > 0
                        orderby _stcStockValues.ProductCode descending
                        select new
                        {
                            ProductCode = _stcStockValues.ProductCode
                        }
                    ).Count();

            return _result;
        }

        public List<STCStockValue> GetListStokBalance(int _prmReqPage, int _prmPageSize, string _prmProductCode)
        {
            List<STCStockValue> _result = new List<STCStockValue>();

            try
            {
                var _query = (
                                from _stcStockValues in this.db.STCStockValues
                                join _msWarehouse in this.db.MsWarehouses
                                    on _stcStockValues.WrhsCode equals _msWarehouse.WrhsCode
                                join _msProduct in this.db.MsProducts
                                    on _stcStockValues.ProductCode equals _msProduct.ProductCode
                                join _msWrhsLocation in this.db.MsWrhsLocations
                                    on _stcStockValues.LocationCode equals _msWrhsLocation.WLocationCode
                                join _msUnit in this.db.MsUnits
                                    on _stcStockValues.UnitCode equals _msUnit.UnitCode
                                where _stcStockValues.ProductCode == _prmProductCode
                                      && _stcStockValues.Qty > 0
                                orderby _stcStockValues.ProductCode descending
                                //groupby  _stcStockValues.ProductCode, _stcStockValues.WrhsCode,_stcStockValues.LocationCode
                                select new
                                {
                                    WrhsSubLed = _stcStockValues.WrhsSubLed,
                                    ProductCode = _stcStockValues.ProductCode,
                                    ProductName = _msProduct.ProductName,
                                    Wrhs = _stcStockValues.WrhsCode,
                                    WrhsName = _msWarehouse.WrhsName,
                                    LocationCode = _stcStockValues.LocationCode,
                                    LocarionName = _msWrhsLocation.WLocationName,
                                    UnitCode = _stcStockValues.UnitCode,
                                    UnitName = _msUnit.UnitName,
                                    Qty = _stcStockValues.Qty
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                decimal _total = 0;

                foreach (var _row in _query)
                {
                    _total += _row.Qty;

                    _result.Add(new STCStockValue(_row.Wrhs, _row.WrhsName, _row.WrhsSubLed, _row.ProductCode, _row.ProductName, _row.LocationCode, _row.LocarionName, _row.UnitCode, _row.UnitName, _row.Qty));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~StockBalanceBL()
        {
        }
    }
}
