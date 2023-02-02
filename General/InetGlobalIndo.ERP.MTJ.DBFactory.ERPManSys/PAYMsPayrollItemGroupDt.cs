using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYMsPayrollItemGroupDt
    {
        private string _payrollName = "";

        public PAYMsPayrollItemGroupDt(string _prmPayrollGrpCode, string _prmPayrollCode, string _prmPayrollName)
        {
            this.PayrollGrpCode = _prmPayrollGrpCode;
            this.PayrollCode = _prmPayrollCode;
            this.PayrollName = _prmPayrollName;
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
