using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PRCReturDt
    {
        string _fileNmbr = "";
        string _productName = "";
        string _locationName = "";

        public PRCReturDt(string _prmTransNmbr, string _prmProductCode, string _prmLocationCode, string _prmLocationName, 
            decimal _prmQty, string _prmUnit, decimal _prmPrice, decimal _prmAmountForex, string _prmRemark, string _prmCreatedBy)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ProductCode = _prmProductCode;
            //this.LocationCode = _prmLocationCode;
            this.LocationName = _prmLocationName;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.Price = _prmPrice;
            this.AmountForex = _prmAmountForex;
            this.Remark = _prmRemark;
            //this.CreatedBy = _prmCreatedBy;
        }

        public PRCReturDt(string _prmTransNmbr, string _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
        }

        public string FileNmbr
        {
            get
            {
                return this._fileNmbr;
            }
            set
            {
                this._fileNmbr = value;
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
