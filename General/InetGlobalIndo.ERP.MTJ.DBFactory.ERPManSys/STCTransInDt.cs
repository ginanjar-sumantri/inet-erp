using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCTransInDt
    {
        private string _productName = "";

        public STCTransInDt(string _prmTransNmbr, string _prmProductCode, string _prmProductName, string _prmLocationSrc, string _prmLocationCode, decimal _prmQtySJ, decimal _prmQty, decimal _prmQtyLoss, string _prmUnit)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.LocationSrc = _prmLocationSrc;
            this.LocationCode = _prmLocationCode;
            this.QtySJ = _prmQtySJ;
            this.Qty = _prmQty;
            this.QtyLoss = _prmQtyLoss;
            this.Unit = _prmUnit;
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
