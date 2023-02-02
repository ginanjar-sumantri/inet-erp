using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCReceiveDt
    {
        string _fileNmbr = "";

        public STCReceiveDt(string _prmTransNmbr, string _prmProductCode, string _prmLocationCode, decimal _prmQty, string _prmUnit, string _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ProductCode = _prmProductCode;
            this.LocationCode = _prmLocationCode;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.Remark = _prmRemark;
        }

        public STCReceiveDt(string _prmTransNmbr, string _prmFileNmbr)
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
    }
}
