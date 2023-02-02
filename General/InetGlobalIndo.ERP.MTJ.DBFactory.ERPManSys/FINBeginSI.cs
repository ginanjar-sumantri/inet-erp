using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINBeginSI
    {
        private string _suppName = "";

        public FINBeginSI(string _prmInvoiceNo, DateTime _prmTransDate, char _prmStatus, string _prmSuppCode, string _prmSuppName, string _prmCurrCode, DateTime _prmDueDate, int _prmTerm)
        {
            this.InvoiceNo = _prmInvoiceNo;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.SuppCode = _prmSuppCode;
            this.SuppName = _prmSuppName;
            this.CurrCode = _prmCurrCode;
            this.DueDate = _prmDueDate;
            this.Term = _prmTerm;
        }

        public FINBeginSI(string _prmInvoiceNo)
        {
            this.InvoiceNo = _prmInvoiceNo;
        }

        public string SuppName
        {
            get
            {
                return this._suppName;
            }
            set
            {
                this._suppName = value;
            }
        }
    }
}
