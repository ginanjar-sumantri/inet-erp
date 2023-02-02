using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCStockValue
    {
        private string _wrhsName = "";
        private string _unitName = "";
        private string _productName = "";
        private string _locationName = "";

        public STCStockValue(string _prmWrhsCode, string _prmWrhsName, string _prmWrhsSubLed, string _prmProductCode, string _prmProductName, string _prmLocationCode, string _prmLocationName, string _prmUnitCode, string _prmUnitName, Decimal _prmQty)
        {
            this.WrhsCode = _prmWrhsCode;
            this.WrhsName = _prmWrhsName;
            this.WrhsSubLed = _prmWrhsSubLed;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.LocationCode = _prmLocationCode;
            this.LocationName = _prmLocationName;
            this.UnitCode = _prmUnitCode;
            this.UnitName = _prmUnitName;
            this.Qty = _prmQty;
        }

        public string WrhsName
        {
            get
            {
                return this._wrhsName;
            }
            set
            {
                this._wrhsName = value;
            }
        }
        public string UnitName
        {
            get
            {
                return this._unitName;
            }
            set
            {
                this._unitName = value;
            }
        }
        public string ProductName
        {
            get
            {
                return this._productName;
            }
            set
            {
                this._productName = value;
            }
        }
        public string LocationName
        {
            get
            {
                return this._locationName;
            }
            set
            {
                this._locationName = value;
            }
        }

    }
}
