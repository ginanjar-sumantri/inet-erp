using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMMsLoan
    {
        private string _payrollName = "";

        public HRMMsLoan(string _prmLoanCode, string _prmLoanName)
        {
            this.LoanCode = _prmLoanCode;
            this.LoanName = _prmLoanName;
        }

        public HRMMsLoan(string _prmLoanCode, string _prmLoanName, String _prmPayrollCode, String _prmPayrollName, char? _prmFgMandatory)
        {
            this.LoanCode = _prmLoanCode;
            this.LoanName = _prmLoanName;
            this.PayrollCode = _prmPayrollCode;
            this.PayrollName = _prmPayrollName;
            this.FgMandatory = _prmFgMandatory;
        }

        public string PayrollName
        {
            get
            {
                return this._payrollName;
            }
            set
            {
                this._payrollName = value;
            }
        }
    }
}
