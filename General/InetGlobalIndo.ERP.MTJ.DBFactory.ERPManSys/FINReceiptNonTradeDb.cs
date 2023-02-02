using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINReceiptNonTradeDb
    {
        private string _receiptName = "";
        private string _bankGiroName = "";

        public FINReceiptNonTradeDb(int _prmItemNo, string _prmReceiptType, string _prmReceiptName, string _prmDocumentNo, decimal _prmAmountForex, string _prmRemark, DateTime? _prmDueDate, string _prmBankGiro, string _prmBankGiroName, decimal? _prmBankExpense)
        {
            this.ItemNo = _prmItemNo;
            this.ReceiptType = _prmReceiptType;
            this.ReceiptName = _prmReceiptName;
            this.DocumentNo = _prmDocumentNo;
            this.AmountForex = _prmAmountForex;
            this.Remark = _prmRemark;
            this.DueDate = _prmDueDate;
            this.BankGiro = _prmBankGiro;
            this.BankGiroName = _prmBankGiroName;
            this.BankExpense = _prmBankExpense;
        }

        public string ReceiptName
        {
            get
            {
                return this._receiptName;
            }
            set
            {
                this._receiptName = value;
            }
        }

        public string BankGiroName
        {
            get
            {
                return this._bankGiroName;
            }
            set
            {
                this._bankGiroName = value;
            }
        }
    }
}
