using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrLeaveAddDt
    {
        private string _empName = "";

        public HRMTrLeaveAddDt(String _prmTransNmbr, String _prmEmployeeId, String _prmEmpName, int? _prmAddLeaveDay, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.EmployeeId = _prmEmployeeId;
            this.EmpName = _prmEmpName;
            this.AddLeaveDay = _prmAddLeaveDay;
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
