using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINBeginCI
    {
        private string _custName = "";

        public FINBeginCI(string _prmInvoiceNo, DateTime _prmTransDate, string _prmCurrCode, char _prmStatus, string _prmCustCode, string _prmCustName, DateTime _prmDueDate)
        {
            this.InvoiceNo = _prmInvoiceNo;
            this.TransDate = _prmTransDate;
            this.CurrCode = _prmCurrCode;
            this.Status = _prmStatus;
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
            this.DueDate = _prmDueDate;
        }

        public FINBeginCI(string _prmInvoiceNo)
        {
            this.InvoiceNo = _prmInvoiceNo;
        }

        public string CustName
        {
            get
            {
                return this._custName;
            }
            set
            {
                this._custName = value;
            }
        }
    }
}
