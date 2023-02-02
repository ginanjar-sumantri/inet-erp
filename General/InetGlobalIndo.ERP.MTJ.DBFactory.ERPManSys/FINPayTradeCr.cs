using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINPayTradeCr
    {
        private string _payName = "";
        private string _bankName = "";
        private string _accountName = "";

        public FINPayTradeCr(int _prmItemNo, string _prmPayType, string _prmDocumentNo, decimal _prmAmountForex, string _prmRemark, string _prmBankPayment, decimal? _prmBankExpense)
        {
            this.ItemNo = _prmItemNo;
            this.PayType = _prmPayType;
            this.DocumentNo = _prmDocumentNo;
            this.AmountForex = _prmAmountForex;
            this.Remark = _prmRemark;
            //this.DueDate = _prmDueDate;
            this.BankPayment = _prmBankPayment;
            this.BankExpense = _prmBankExpense;
        }

        public FINPayTradeCr(int _prmItemNo, string _prmPayType, string _prmPayName, string _prmDocumentNo, decimal? _prmAmountHome, string _prmCurrCode, decimal? _prmBankExpense, string _prmRemark)
        {
            this.ItemNo = _prmItemNo;
            this.PayType = _prmPayType;
            this.PayName = _prmPayName;
            this.DocumentNo = _prmDocumentNo;
            this.AmountHome = _prmAmountHome;
            this.CurrCode = _prmCurrCode;
            this.BankExpense = _prmBankExpense;
            this.Remark = _prmRemark;
        }

        public string PayName
        {
            get
            {
                return this._payName;
            }
            set
            {
                this._payName = value;
            }
        }

        public string BankName
        {
            get
            {
                return this._bankName;
            }
            set
            {
                this._bankName = value;
            }
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
    }
}
