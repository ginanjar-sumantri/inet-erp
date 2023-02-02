using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINCustInvDt
    {
        private string _productName = "";
        private string _unitName = "";
        private string _fileNmbr = "";

        public FINCustInvDt(string _prmSJNo, string _prmFileNmbr, string _prmProductCode, string _prmProductName, string _prmSONo, decimal _prmQty, string _prmUnit, string _prmUnitName, decimal _prmPriceForex, decimal _prmAmountForex, string _prmRemark)
        {
            this.SJNo = _prmSJNo;
            this.FileNmbr = _prmFileNmbr;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.SONo = _prmSONo;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.UnitName = _prmUnitName;
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
    }
}
