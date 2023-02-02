using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Port_DocServiceHd_STCSJHd
    {
        private string _fileNmbr = "";
        private string _serviceTypeName = "";

        public Port_DocServiceHd_STCSJHd(Guid _prmDtDocServiceSJCode, string _prmTransNmbr, string _prmFileNmbr, string _prmItemName, string _prmServiceTypeName, decimal _prmItemPrice, decimal _prmQty, decimal _prmTotalPay)
        {
            this.DtDocServiceSJCode = _prmDtDocServiceSJCode;
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.ItemName = _prmItemName;
            this.ServiceTypeName = _prmServiceTypeName;
            this.ItemPrice = _prmItemPrice;
            this.Qty = _prmQty;
            this.TotalPay = _prmTotalPay;
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

        public string ServiceTypeName
        {
            get
            {
                return this._serviceTypeName;
            }
            set
            {
                this._serviceTypeName = value;
            }
        }
    }
}
