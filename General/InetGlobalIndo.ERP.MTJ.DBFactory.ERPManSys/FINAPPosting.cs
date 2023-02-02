using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINAPPosting
    {
        public FINAPPosting(string _prmInvoiceNo, DateTime _prmInvoiceDate, string _prmSuppCode,
            string _prmCurrCode, decimal _prmForexRate, decimal _prmOriginalRate, string _prmSuppInvoice,
            decimal _prmAmount, decimal _prmBalance, decimal _prmPPN, decimal _prmPPNRate,
            decimal _prmAmountPPN, decimal _prmBalancePPN, int _prmFgValue)
        {
            this.InvoiceNo = _prmInvoiceNo;
            this.InvoiceDate = _prmInvoiceDate;
            this.SuppCode = _prmSuppCode;
            this.CurrCode = _prmCurrCode;
            this.ForexRate = _prmForexRate;
            this.OriginalRate = _prmOriginalRate;
            this.SuppInvoice = _prmSuppInvoice;
            this.Amount = _prmAmount;
            this.Balance = _prmBalance;
            this.PPN = _prmPPN;
            this.PPNRate = _prmPPNRate;
            this.AmountPPN = _prmAmountPPN;
            this.BalancePPN = _prmBalancePPN;
            this.FgValue = _prmFgValue;
        }

        public FINAPPosting(string _prmInvoiceNo)
        {
            this.InvoiceNo = _prmInvoiceNo;
        }

        public FINAPPosting(string _prmInvoiceNo, string _prmFileNmbr)
        {
            this.InvoiceNo = _prmInvoiceNo;
            this.FileNmbr = _prmFileNmbr;
        }
    }
}
