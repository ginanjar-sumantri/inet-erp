using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINCNCustHd
    {
        private string _termName = "";
        private string _custName = "";

        public FINCNCustHd(string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, string _prmCurrCode, char _prmStatus, string _prmCustCode, string _prmCustName, string _prmInvoiceNo, string _prmTerm, string _prmTermName)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.CurrCode = _prmCurrCode;
            this.Status = _prmStatus;
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
            this.InvoiceNo = _prmInvoiceNo;
            this.Term = _prmTerm;
            this.TermName = _prmTermName;
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

        public string TermName
        {
            get
            {
                return this._termName;
            }
            set
            {
                this._termName = value;
            }
        }
    }
}
