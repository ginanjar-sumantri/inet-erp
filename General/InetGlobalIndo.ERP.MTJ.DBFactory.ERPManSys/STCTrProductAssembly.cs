using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCTrProductAssembly
    {
        string _productName = "";

        public STCTrProductAssembly(String _prmTransNmbr, String _prmFileNmbr, DateTime _prmTransDate, String _prmProductCode, String _prmProductName, Byte _prmStatus,
            String _prmReference, Decimal _prmQty, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.Status = _prmStatus;
            this.Reference = _prmReference;
            this.Qty = _prmQty;
            this.Remark = _prmRemark;
        }

        public STCTrProductAssembly(String _prmTransNmbr, String _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
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
