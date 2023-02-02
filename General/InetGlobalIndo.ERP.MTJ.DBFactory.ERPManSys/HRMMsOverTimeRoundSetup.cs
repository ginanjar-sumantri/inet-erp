using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMMsOverTimeRoundSetup
    {
        public HRMMsOverTimeRoundSetup(int _prmStartMinute, int? _prmEndMinute, Decimal? _prmOTHour)
        {
            this.StartMinute = _prmStartMinute;
            this.EndMinute = _prmEndMinute;
            this.OTHour = _prmOTHour;
        }
    }
}
