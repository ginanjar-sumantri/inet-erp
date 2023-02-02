using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINPayNonTradeCr
    {
        private string _payName = "";
        private string _bankName = "";

        public FINPayNonTradeCr(int _prmItemNo, string _prmPayType, string _prmDocumentNo, decimal _prmAmountForex, string _prmRemark, DateTime? _prmDueDate, string _prmBankPayment, decimal? _prmBankExpense)
        {
            this.ItemNo = _prmItemNo;
            this.PayType = _prmPayType;
            this.DocumentNo = _prmDocumentNo;
            this.AmountForex = _prmAmountForex;
            this.Remark = _prmRemark;
            this.DueDate = _prmDueDate;
            this.BankPayment = _prmBankPayment;
            this.BankExpense = _prmBankExpense;
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
    }
}
