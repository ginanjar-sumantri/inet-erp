using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrLoanInDt2
    {
        private string _empName = "";

        public HRMTrLoanInDt2(String _prmTransNmbr, String _prmEmpNumb, String _prmEmpName, Int32 _prmPaymentNo, DateTime _prmClaimDate,
            Decimal _prmClaimAmount, Boolean _prmFgPay)
        {
            this.TransNmbr = _prmTransNmbr;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.PaymentNo = _prmPaymentNo;
            this.ClaimDate = _prmClaimDate;
            this.ClaimAmount = _prmClaimAmount;
            this.FgPay = _prmFgPay;
        }

        public string EmpName
        {
            get
            {
                return this._empName;
            }
            set
            {
                this._empName = value;
            }
        }
    }
}
