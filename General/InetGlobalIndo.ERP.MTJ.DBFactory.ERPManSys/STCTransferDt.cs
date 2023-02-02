using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCTransferDt
    {
        private string _productName = "";

        public STCTransferDt(string _prmTransNmbr, string _prmProductCode, string _prmProductName, string _prmLocationSrc, string _prmLocationDest, decimal _prmQty, string _prmUnit, string _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.LocationSrc = _prmLocationSrc;
            this.LocationDest = _prmLocationDest;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
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
    }
}
