using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINSuppInvDt
    {
        private string _fileNmbr = "";
        private string _fileNoPO = "";

        public FINSuppInvDt(string _prmTransNmbr, string _prmRRNo, string _prmFileNmbr, string _prmProductCode, string _prmPONo, string _prmFileNoPO, decimal _prmQty, string _prmUnit, decimal? _prmPriceForex, decimal? _prmAmountForex, string _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.RRNo = _prmRRNo;
            this.FileNmbr = _prmFileNmbr;
            this.ProductCode = _prmProductCode;
            this.PONo = _prmPONo;
            this.FileNoPO = _prmFileNoPO;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.PriceForex = _prmPriceForex;
            this.AmountForex = _prmAmountForex;
            this.Remark = _prmRemark;
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

        public string FileNoPO
        {
            get
            {
                return this._fileNoPO;
            }
            set
            {
                this._fileNoPO = value;
            }
        }
    }
}
