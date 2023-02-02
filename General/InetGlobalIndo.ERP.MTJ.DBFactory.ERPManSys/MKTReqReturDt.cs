using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MKTReqReturDt
    {
        private string _productName = "";

        public MKTReqReturDt(string _prmTransNmbr, string _prmProductCode, string _prmProductName, decimal _prmQty, string _prmUnit, decimal _prmPriceForex, decimal _prmAmountForex)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.PriceForex = _prmPriceForex;
            this.AmountForex = _prmAmountForex;
        }

        public MKTReqReturDt(string _prmTransNmbr, string _prmProductCode, decimal _prmQty, string _prmUnit, decimal _prmPriceForex, decimal _prmAmountForex)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ProductCode = _prmProductCode;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.PriceForex = _prmPriceForex;
            this.AmountForex = _prmAmountForex;
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
    }
}
