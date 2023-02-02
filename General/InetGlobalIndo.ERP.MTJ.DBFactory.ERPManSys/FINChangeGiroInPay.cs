using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINChangeGiroInPay
    {
        public FINChangeGiroInPay(string _prmTransNmbr, int _prmItemNo, string _prmReceiptType, DateTime? _prmDueDate, string _prmBankGiro, decimal _prmAmountForex)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ItemNo = _prmItemNo;
            this.ReceiptType = _prmReceiptType;
            this.DueDate = _prmDueDate;
            this.BankGiro = _prmBankGiro;
            this.AmountForex = _prmAmountForex;
        }
    }
}
