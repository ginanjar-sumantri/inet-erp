using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINDPSuppReturPay
    {
        public FINDPSuppReturPay(string _prmTransNmbr, int _prmItemNo, string _prmReceiptType, string _prmDocumentNo, decimal _prmAmountForex, string _prmCurrCode, decimal _prmForexRate, string _prmRemark, string _prmBankGiro, DateTime? _prmDueDate, decimal? _prmBankExpense)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ItemNo = _prmItemNo;
            this.ReceiptType = _prmReceiptType;
            this.DocumentNo = _prmDocumentNo;
            this.AmountForex = _prmAmountForex;
            this.CurrCode = _prmCurrCode;
            this.ForexRate = _prmForexRate;
            this.Remark = _prmRemark;
            this.BankGiro = _prmBankGiro;
            this.DueDate = _prmDueDate;
            this.BankExpense = _prmBankExpense;
        }
    }
}
