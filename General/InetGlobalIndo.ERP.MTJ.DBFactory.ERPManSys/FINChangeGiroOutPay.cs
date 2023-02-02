using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINChangeGiroOutPay
    {
        public FINChangeGiroOutPay(string _prmTransNmbr, int _prmItemNo, string _prmPayType, string _prmDocumentNo, string _prmCurrCode, decimal _prmForexRate, decimal _prmAmountForex, string _prmRemark, string _prmBankPayment, DateTime? _prmDueDate, decimal? _prmBankExpense)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ItemNo = _prmItemNo;
            this.PayType = _prmPayType;
            this.DocumentNo = _prmDocumentNo;
            this.CurrCode = _prmCurrCode;
            this.ForexRate = _prmForexRate;
            this.AmountForex = _prmAmountForex;
            this.Remark = _prmRemark;
            this.BankPayment = _prmBankPayment;
            this.DueDate = _prmDueDate;
            this.BankExpense = _prmBankExpense;
        }
    }
}
