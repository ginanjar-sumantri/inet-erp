using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYTrSalaryProcess
    {
        private string _empName = "";
        private string _payrollName = "";

        public PAYTrSalaryProcess(string _prmTransNmbr, String _prmEmpNumb, String _prmEmpName, String _prmPayroll,
            String _prmPayrollName, DateTime _prmStartDate, DateTime _prmEndDate, decimal _prmTotal)
        {
            this.TransNmbr = _prmTransNmbr;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.Payroll = _prmPayroll;
            this.PayrollName = _prmPayrollName;
            this.StartDate = _prmStartDate;
            this.EndDate = _prmEndDate;
            this.Total = _prmTotal;
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
