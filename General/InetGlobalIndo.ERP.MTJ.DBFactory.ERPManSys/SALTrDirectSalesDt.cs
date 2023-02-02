using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class SALTrDirectSalesDt
    {
        public string _productName = "";
        public string _wrhsName = "";
        public string _wrhsSubledName = "";
        public string _wlocationName = "";

        public SALTrDirectSalesDt(String _prmTransNmbr, string _prmProductCode, string _prmProductName, Decimal _prmQty, Decimal _prmPrice,
            Decimal _prmAmount, String _prmWareHouse, char _prmWareHouseFgSubLed, String _prmWareHouseSubLed, String _prmWareHouseLocation,
            string _prmWrhsName, string _prmWrhsSubledName, string _prmWrhsLocationName)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.Qty = _prmQty;
            this.Price = _prmPrice;
            this.Amount = _prmAmount;
            this.WrhsCode = _prmWareHouse ;
            this.WrhsFgSubLed = _prmWareHouseFgSubLed;
            this.WrhsSubLed =_prmWareHouseSubLed;
            this.WLocationCode = _prmWareHouseLocation;
            this.WrhsName = _prmWrhsName;
            this.WrhsSubledName = _prmWrhsSubledName;
            this.WLocationName = _prmWrhsLocationName;
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
        public string WrhsSubledName
        {
            get
            {
                return this._wrhsSubledName;
            }
            set
            {
                this._wrhsSubledName = value;
            }
        }
        public string WLocationName
        {
            get
            {
                return this._wlocationName;
            }
            set
            {
                this._wlocationName = value;
            }
        }
    }
}
