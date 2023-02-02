using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrLeaveProcessDt
    {
        private string _empName = "";

        public HRMTrLeaveProcessDt(int _prmTransNmbr, int _prmEmpNumb, String _prmEmployeeId, String _prmEmpName, int? _prmNewLeaveDay, DateTime? _prmStartEffective, DateTime? _prmEndEffective, int? _prmDefaultLeave, int? _prmDeductionLeave)
        {
            this.ProcessYear = _prmTransNmbr;
            this.ProcessPeriod = _prmEmpNumb;
            this.EmployeeId = _prmEmployeeId;
            this.EmpName = _prmEmpName;
            this.NewLeaveDay = _prmNewLeaveDay;
            this.StartEffective = _prmStartEffective;
            this.EndEffective = _prmEndEffective;
            this.DefaultLeave = _prmDefaultLeave;
            this.DeductionLeave = _prmDeductionLeave;
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
