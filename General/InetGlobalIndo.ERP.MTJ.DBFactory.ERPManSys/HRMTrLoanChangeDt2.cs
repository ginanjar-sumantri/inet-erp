using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrLoanChangeDt2
    {
        public HRMTrLoanChangeDt2(String _prmTransNmbr, Int32 _prmPaymentNo, DateTime _prmClaimDate, Decimal _prmClaimAmount)
        {
            this.TransNmbr = _prmTransNmbr;
            this.PaymentNo = _prmPaymentNo;
            this.ClaimDate = _prmClaimDate;
            this.ClaimAmount = _prmClaimAmount;
        }
    }
}
