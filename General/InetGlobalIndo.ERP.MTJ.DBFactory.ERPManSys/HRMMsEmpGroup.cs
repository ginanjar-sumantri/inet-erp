using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMMsEmpGroup
    {
        private string _shiftName = "";

        public HRMMsEmpGroup(string _prmEmpGroupCode, string _prmEmpGroupName, String _prmScheduleType, String _prmShiftCode, String _prmShiftName)
        {
            this.EmpGroupCode = _prmEmpGroupCode;
            this.EmpGroupName = _prmEmpGroupName;
            this.ScheduleType = _prmScheduleType;
            this.ShiftCode = _prmShiftCode;
            this.ShiftName = _prmShiftName;
        }

        public HRMMsEmpGroup(string _prmEmpGroupCode, string _prmEmpGroupName)
        {
            this.EmpGroupCode = _prmEmpGroupCode;
            this.EmpGroupName = _prmEmpGroupName;
        }

        public string ShiftName
        {
            get
            {
                return this._shiftName;
            }
            set
            {
                this._shiftName = value;
            }
        }
    }
}
