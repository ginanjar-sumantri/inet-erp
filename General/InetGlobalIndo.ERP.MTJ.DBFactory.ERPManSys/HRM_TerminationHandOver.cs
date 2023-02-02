using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_TerminationHandOver
    {
        public string _empName = "";

        public HRM_TerminationHandOver(Guid _prmTerminationRequestCode, string _prmEmpNumb, string _prmEmpName, byte _prmStatus, DateTime _prmStartDate, DateTime _prmEndDate, string _prmRemark)
        {
            this.TerminationRequestCode = _prmTerminationRequestCode;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.Status = _prmStatus;
            this.StartDate = _prmStartDate;
            this.EndDate = _prmEndDate;
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
