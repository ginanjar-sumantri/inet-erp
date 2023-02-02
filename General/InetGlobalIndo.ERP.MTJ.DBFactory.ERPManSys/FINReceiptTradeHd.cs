using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINReceiptTradeHd
    {
        private string _customerName = "";

        public FINReceiptTradeHd(String _prmReceiptNo,string _prmFileNmbr ,DateTime _prmTransDate, Char _prmStatus, String _prmCustCode
       , String _prmCustName, String _prmRemark)
        {
            this.TransNmbr = _prmReceiptNo;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.CustCode = _prmCustCode;
            this.CustomerName = _prmCustName;
            this.Remark = _prmRemark;
        }

        public string CustomerName
        {
            get
            {
                return this._customerName;
            }
            set
            {
                this._customerName = value;
            }
        }
    }
}
