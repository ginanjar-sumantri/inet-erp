using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_AbsenceTypeLeaveAllowed
    {
        public HRM_AbsenceTypeLeaveAllowed(Guid _prmAbsenceTypeLeaveAllowedCode, Guid _prmAbsenceTypeCode, byte _prmMaxDaysAllowed, byte _prmBeginMonth, byte _prmEndingMonth)
        {
            this.AbsenceTypeLeaveAllowedCode = _prmAbsenceTypeLeaveAllowedCode;
            this.AbsenceTypeCode = _prmAbsenceTypeCode;
            this.MaxDaysAllowed = _prmMaxDaysAllowed;
            this.BeginMonth = _prmBeginMonth;
            this.EndingMonth = _prmEndingMonth;
        }
    }
}
