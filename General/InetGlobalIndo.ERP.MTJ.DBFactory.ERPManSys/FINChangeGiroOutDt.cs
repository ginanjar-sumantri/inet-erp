using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINChangeGiroOutDt
    {
        public FINChangeGiroOutDt(string _prmTransNmbr, string _prmOldGiro, DateTime? _prmPaymentDate, DateTime? _prmDueDate, string _prmBankPayment, string _prmCurrCode, decimal _prmForexRate, decimal _prmAmountForex)
        {
            this.TransNmbr = _prmTransNmbr;
            this.OldGiro = _prmOldGiro;
            this.PaymentDate = _prmPaymentDate;
            this.DueDate = _prmDueDate;
            this.BankPayment = _prmBankPayment;
            this.CurrCode = _prmCurrCode;
            this.ForexRate = _prmForexRate;
            this.AmountForex = _prmAmountForex;
        }
    }
}
