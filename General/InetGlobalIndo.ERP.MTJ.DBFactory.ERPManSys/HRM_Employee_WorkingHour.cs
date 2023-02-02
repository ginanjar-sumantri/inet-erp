using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_Employee_WorkingHour
    {
        public string _workingHourName = "";

        public HRM_Employee_WorkingHour(Guid _prmEmployeeWorkingHourCode, string _prmEmpNumb, Guid _prmWorkingHourCode, String _prmWorkingHourName, DateTime _prmStartDate, DateTime _prmEndDate)
        {
            this.EmployeeWorkingHourCode = _prmEmployeeWorkingHourCode;
            this.EmpNumb = _prmEmpNumb;
            this.WorkingHourCode = _prmWorkingHourCode;
            this.WorkingHourName = _prmWorkingHourName;
            this.StartDate = _prmStartDate;
            this.EndDate = _prmEndDate;
        }

        public string WorkingHourName
        {
            get
            {
                return this._workingHourName;
            }
            set
            {
                this._workingHourName = value;
            }
        }
    }
}
