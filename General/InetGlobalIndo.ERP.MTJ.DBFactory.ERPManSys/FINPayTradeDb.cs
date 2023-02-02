using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINPayTradeDb
    {
        private string _suppName = "";
        private string _fileNmbr = "";

        public FINPayTradeDb(string _prmInvoiceNo, string _prmFileNmbr, string _prmSuppInvoice, string _prmSuppName, string _prmCurrCode, decimal? _prmApPaid, decimal? _prmPPNPaid, decimal _prmAmountForex, decimal? _prmAmountHome, decimal? _prmFgValue, string _prmRemark)
        {
            this.InvoiceNo = _prmInvoiceNo;
            this.FileNmbr = _prmFileNmbr;
            this.SuppInvoice = _prmSuppInvoice;
            this.SuppName = _prmSuppName;
            this.CurrCode = _prmCurrCode;
            this.APPaid = _prmApPaid;
            this.PPNPaid = _prmPPNPaid;
            this.AmountForex = _prmAmountForex;
            this.AmountHome = _prmAmountHome;
            this.FgValue = _prmFgValue;
            this.Remark = _prmRemark;
        }

        public FINPayTradeDb(string _prmInvoiceNo, string _prmFileNmbr)
        {
            this.InvoiceNo = _prmInvoiceNo;
            this.FileNmbr = _prmFileNmbr;
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
