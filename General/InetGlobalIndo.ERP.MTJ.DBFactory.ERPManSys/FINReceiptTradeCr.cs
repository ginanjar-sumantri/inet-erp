using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINReceiptTradeCr
    {
        private string _accountName = "";
        private string _fileNmbr = "";

        public FINReceiptTradeCr(String _prmReceiptNo, String _prmInvoiceNo, String _prmFileNmbr, String _prmCurrCode, Decimal _prmForexRate,
        Decimal _prmArBalance,Decimal _prmArInvoice,Decimal _prmArPaid,Decimal _prmPPnBalance,Decimal _prmPPnInvoice,Decimal _prmPPnPaid, 
        Decimal? _prmPPnRate, Decimal _prmAmountInvoice, Decimal _prmAmountBalance, Decimal _prmAmountForex,Decimal? _prmAmountHome, String _prmRemark,
        String _prmAccount,String _prmAccountName, Char? _prmFgSubLed, Decimal? _prmFgValue)
        {
            this.TransNmbr      = _prmReceiptNo;
            this.InvoiceNo      = _prmInvoiceNo;
            this.FileNmbr       = _prmFileNmbr;
            this.CurrCode       = _prmCurrCode;
            this.ForexRate      = _prmForexRate;
            this.ARBalance      = _prmArBalance;
            this.ARInvoice      = _prmArInvoice;
            this.ARPaid         = _prmArPaid;
            this.PPnBalance     = _prmPPnBalance;
            this.PPnInvoice     = _prmPPnInvoice;
            this.PPnPaid        = _prmPPnPaid;
            this.PPnRate        = _prmPPnRate;
            this.AmountInvoice  = _prmAmountInvoice;
            this.AmountBalance  = _prmAmountBalance;
            this.AmountForex    = _prmAmountForex;
            this.AmountHome     = _prmAmountHome;
            this.Remark         = _prmRemark;
            this.Account        = _prmAccount;
            this.AccountName    = _prmAccountName;
            this.FgSubLed       = _prmFgSubLed;
            this.FgValue        = _prmFgValue;
        }

        public FINReceiptTradeCr(String _prmInvoiceNo, String _prmFileNmbr)
        {
            this.InvoiceNo = _prmInvoiceNo;
            this.FileNmbr = _prmFileNmbr;
        }

        public string AccountName
        {
            get
            {
                return this._accountName;
            }
            set
            {
                this._accountName = value;
            }
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
