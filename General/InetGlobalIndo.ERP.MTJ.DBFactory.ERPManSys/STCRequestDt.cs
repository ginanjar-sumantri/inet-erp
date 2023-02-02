using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCRequestDt
    {
        private string _productName = "";

        public STCRequestDt(string _prmTransNmbr, string _prmProductCode, string _prmProductName, decimal _prmQty, string _prmUnit, char? _prmDoneClosing, decimal? _prmQtyClose)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.DoneClosing = _prmDoneClosing;
            this.QtyClose = _prmQtyClose;
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
