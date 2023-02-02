using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMMsPeriodSchedule
    {
        public HRMMsPeriodSchedule(string _prmPeriodeCode, int _prmPeriodYear, int _prmPeriodMonth, DateTime _prmStartDate, DateTime _prmEndDate)
        {
            this.PeriodeCode = _prmPeriodeCode;
            this.PeriodYear = _prmPeriodYear;
            this.PeriodMonth = _prmPeriodMonth;
            this.StartDate = _prmStartDate;
            this.EndDate = _prmEndDate;
        }

        public HRMMsPeriodSchedule(string _prmPeriodeCode)
        {
            this.PeriodeCode = _prmPeriodeCode;
        }
    }
}
