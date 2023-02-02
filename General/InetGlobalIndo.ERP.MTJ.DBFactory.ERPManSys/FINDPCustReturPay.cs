using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINDPCustReturPay
    {
        public FINDPCustReturPay(int _prmItemNo, string _prmPayType, string _prmDocumentNo, decimal _prmAmountHome, string _prmBankPayment, decimal? _prmBankExpense)
        {
            this.ItemNo = _prmItemNo;
            this.PayType = _prmPayType;
            this.DocumentNo = _prmDocumentNo;
            this.AmountHome = _prmAmountHome;
            this.BankPayment = _prmBankPayment;
            this.BankExpense = _prmBankExpense;
        }
    }
}
