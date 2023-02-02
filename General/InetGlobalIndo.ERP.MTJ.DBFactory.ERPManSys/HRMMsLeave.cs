using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMMsLeave
    {
        public HRMMsLeave(string _prmLeavesCode, string _prmLeavesName, int? _prmMaxTakenDays, int? _prmLeaveDays, Boolean? _prmfgIncludeHoliday)
        {
            this.LeavesCode = _prmLeavesCode;
            this.LeavesName = _prmLeavesName;
            this.MaxTakenDays = _prmMaxTakenDays;
            this.LeaveDays = _prmLeaveDays;
            this.fgIncludeHoliday = _prmfgIncludeHoliday;
        }

        public HRMMsLeave(string _prmLeavesCode, string _prmLeavesName)
        {
            this.LeavesCode = _prmLeavesCode;
            this.LeavesName = _prmLeavesName;
        }
    }
}
