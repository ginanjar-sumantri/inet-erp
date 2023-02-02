using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINSuppInvDt2
    {
        private string _productName = "";
        private string _wrhsName = "";
        private string _locationName = "";

        public FINSuppInvDt2(string _prmTransNmbr, string _prmProductCode, string _prmProductName, string _prmWrhsCode, string _prmWrhsName, string _prmWrhsSubLed, string _prmLocationCode, string _prmLocationName, decimal _prmQty, string _prmUnit, decimal? _prmPriceForex, decimal? _prmAmountForex, string _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.WrhsCode = _prmWrhsCode;
            this.WrhsName = _prmWrhsName;
            this.WrhsSubLed = _prmWrhsSubLed;
            this.LocationCode = _prmLocationCode;
            this.LocationName = _prmLocationName;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.PriceForex = _prmPriceForex;
            this.AmountForex = _prmAmountForex;
            this.Remark = _prmRemark;
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
