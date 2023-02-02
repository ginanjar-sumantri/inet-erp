using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINDPCustDt
    {
        public FINDPCustDt(int _prmItemNo, string _prmReceiptType, DateTime? _prmDueDate, string _prmBankGiro, decimal _prmAmountForex)
        {
            this.ItemNo = _prmItemNo;
            this.ReceiptType = _prmReceiptType;
            this.DueDate = _prmDueDate;
            this.BankGiro = _prmBankGiro;
            this.AmountForex = _prmAmountForex;
        }
    }
}
