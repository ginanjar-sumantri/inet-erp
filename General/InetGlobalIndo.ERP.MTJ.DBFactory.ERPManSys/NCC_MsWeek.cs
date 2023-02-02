using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class NCC_MsWeek
    {
        public NCC_MsWeek(string _prmWeekID, int _prmYear, DateTime _prmStartDate, DateTime _prmEndDate)
        {
            this.WeekID = _prmWeekID;
            this.Year = _prmYear;
            this.StartDate = _prmStartDate;
            this.EndDate = _prmEndDate;
        }

    }
}
