using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYMsPHKPayroll
    {
        private string _reasonName = "";
        private string _payrollName = "";

        public PAYMsPHKPayroll(string _prmPHKReason, string _prmReasonName, string _prmPayrollCode, string _prmPayrollName, decimal _prmPercentage)
        {
            this.PHKReason = _prmPHKReason;
            this.ReasonName = _prmReasonName;
            this.PayrollCode = _prmPayrollCode;
            this.PayrollName = _prmPayrollName;
            this.Percentage = _prmPercentage;
        }

        public string ReasonName
        {
            get
            {
                return this._reasonName;
            }
            set
            {
                this._reasonName = value;
            }
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
