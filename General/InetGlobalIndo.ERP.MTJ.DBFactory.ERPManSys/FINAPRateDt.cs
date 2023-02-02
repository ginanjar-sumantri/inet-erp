using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINAPRateDt
    {
        string _suppName = "";
        string _fileNoInvoice = "";

        public FINAPRateDt(string _prmInvoiceNo, string _prmFileNoInvoice, string _prmSuppCode, string _prmSuppName, decimal _prmForexRate, decimal _prmAmountForex, decimal _prmAmountHome, decimal _prmNewAmountHome, bool _prmIsApplyToPPN)
        {
            this.InvoiceNo = _prmInvoiceNo;
            this.FileNoInvoice = _prmFileNoInvoice;
            this.SuppCode = _prmSuppCode;
            this.SuppName = _prmSuppName;
            this.ForexRate = _prmForexRate;
            this.AmountForex = _prmAmountForex;
            this.AmountHome = _prmAmountHome;
            this.NewAmountHome = _prmNewAmountHome;
            this.IsApplyToPPN = _prmIsApplyToPPN;
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

        public string FileNoInvoice
        {
            get
            {
                return this._fileNoInvoice;
            }
            set
            {
                this._fileNoInvoice = value;
            }
        }
    }
}
