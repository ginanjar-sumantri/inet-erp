using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_EmpLeaveDay
    {
        public Master_EmpLeaveDay(string _prmEmpNmbr, byte _prmLeaveDayRemain, DateTime? _prmExpiredDateLeaveRemain, byte _prmLeaveCurrent, DateTime? _prmExpiredDateLeaveCurrent)
        {
            this.EmpNumb = _prmEmpNmbr;
            this.LeaveDayRemain = _prmLeaveDayRemain;
            this.ExpiredDateLeaveRemain = _prmExpiredDateLeaveRemain;
            this.LeaveCurrent = _prmLeaveCurrent;
            this.ExpiredDateLeaveCurrent = _prmExpiredDateLeaveCurrent;
        }
    }
}
