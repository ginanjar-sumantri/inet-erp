using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCChangeDt
    {
        private string _productSrcName = "";
        private string _locationSrcName = "";
        private string _productDestName = "";
        private string _locationDestName = "";

        public STCChangeDt(string _prmTransNmbr, string _prmProductSrc, string _prmProductSrcName,
            string _prmLocationSrc, string _prmLocationSrcName, string _prmProductDest, string _prmProductDestName,
            string _prmLocationDest, string _prmLocationDestName, decimal _prmQty, string _prmUnit)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ProductSrc = _prmProductSrc;
            this.ProductSrcName = _prmProductSrcName;
            this.LocationSrc = _prmLocationSrc;
            this.LocationSrcName = _prmLocationSrcName;
            this.ProductDest = _prmProductDest;
            this.ProductDestName = _prmProductDestName;
            this.LocationDest = _prmLocationDest;
            this.LocationDestName = _prmLocationDestName;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
        }

        public string ProductSrcName
        {
            get
            {
                return this._productSrcName;
            }
            set
            {
                this._productSrcName = value;
            }
        }

        public string LocationSrcName
        {
            get
            {
                return this._locationSrcName;
            }
            set
            {
                this._locationSrcName = value;
            }
        }

        public string ProductDestName
        {
            get
            {
                return this._productDestName;
            }
            set
            {
                this._productDestName = value;
            }
        }

        public string LocationDestName
        {
            get
            {
                return this._locationDestName;
            }
            set
            {
                this._locationDestName = value;
            }
        }
    }
}
