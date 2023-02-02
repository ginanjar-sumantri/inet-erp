using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINChangeGiroInDt
    {
        public FINChangeGiroInDt(string _prmTransNmbr, string _prmOldGiro, DateTime _prmReceiptDate, DateTime _prmDueDate, string _prmBankGiro, decimal _prmAmountForex)
        {
            this.TransNmbr = _prmTransNmbr;
            this.OldGiro = _prmOldGiro;
            this.ReceiptDate = _prmReceiptDate;
            this.DueDate = _prmDueDate;
            this.BankGiro = _prmBankGiro;
            this.AmountForex = _prmAmountForex;
        }
    }
}
