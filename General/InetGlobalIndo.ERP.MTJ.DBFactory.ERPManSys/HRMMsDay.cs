using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMMsDay
    {
        public HRMMsDay(int _prmDayCode, String _prmDayName)
        {
            this.DayCode = _prmDayCode;
            this.DayName = _prmDayName;
        }
    }
}
