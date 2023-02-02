using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_AbsenceRequestActing
    {
        private string _empName = "";

        public HRM_AbsenceRequestActing(Guid _prmAbsenceRequestCode, string _prmEmpNumb,
            string _prmEmpName, DateTime _prmStartDate, DateTime _prmEndDate, string _prmRemark)
        {
            this.AbsenceRequestCode = _prmAbsenceRequestCode;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
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
