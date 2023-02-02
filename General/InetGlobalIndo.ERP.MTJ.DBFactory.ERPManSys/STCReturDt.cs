using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCReturDt
    {
        string _productName = "";
        string _locationName = "";

        public STCReturDt(string _prmTransNmbr, string _prmProductCode, string _prmLocationCode, string _prmUnit, decimal _prmQty)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ProductCode = _prmProductCode;
            this.LocationCode = _prmLocationCode;
            this.Unit = _prmUnit;
            this.Qty = _prmQty;
        }

        public STCReturDt(string _prmTransNmbr, string _prmProductCode, string _prmProductName, string _prmLocationCode, string _prmLocationName, string _prmUnit, decimal _prmQty)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.LocationCode = _prmLocationCode;
            this.LocationName = _prmLocationName;
            this.Unit = _prmUnit;
            this.Qty = _prmQty;
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
