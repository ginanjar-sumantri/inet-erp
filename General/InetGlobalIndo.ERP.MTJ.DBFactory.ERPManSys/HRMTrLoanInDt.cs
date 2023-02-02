using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrLoanInDt
    {
        private string _empName = "";

        public HRMTrLoanInDt(String _prmTransNmbr, String _prmEmpNumb, String _prmEmpName, DateTime _prmStartClaim, String _prmCurrCode,
            Decimal? _prmAmountLoan, int? _prmPayment, Decimal? _prmAmountClaim, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.StartClaim = _prmStartClaim;
            this.CurrCode = _prmCurrCode;
            this.AmountLoan = _prmAmountLoan;
            this.Payment = _prmPayment;
            this.AmountClaim = _prmAmountClaim;
            this.Remark = _prmRemark;
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
