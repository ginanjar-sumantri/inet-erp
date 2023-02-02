using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMMsOverTime
    {
        public HRMMsOverTime(Decimal _prmOvertimeHour, Decimal _prmWorkDay, Decimal _prmHoliday, Decimal _prmPublicHoliday)
        {
            this.OvertimeHour = _prmOvertimeHour;
            this.WorkDay = _prmWorkDay;
            this.Holiday = _prmHoliday;
            this.PublicHoliday = _prmPublicHoliday;
        }
    }
}
