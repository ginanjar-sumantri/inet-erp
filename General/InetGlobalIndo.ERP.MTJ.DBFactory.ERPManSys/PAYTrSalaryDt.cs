using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYTrSalaryDt
    {
        private string _payrollGroupName = "";

        public PAYTrSalaryDt(String _prmTransNmbr, String _prmPayrollGroup, String _prmPayrollGroupName, DateTime _prmStartDate, DateTime _prmEndDate)
        {
            this.TransNmbr = _prmTransNmbr;
            this.PayrollGroup = _prmPayrollGroup;
            this.PayrollGroupName = _prmPayrollGroupName;
            this.StartDate = _prmStartDate;
            this.EndDate = _prmEndDate;
        }

        public string PayrollGroupName
        {
            get
            {
                return this._payrollGroupName;
            }
            set
            {
                this._payrollGroupName = value;
            }
        }
    }
}
