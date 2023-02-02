using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class NCC_MsPeriod
    {
        public NCC_MsPeriod(string _prmPeriodID,int _prmYear, DateTime _prmStartDate,DateTime _prmEndDate)
        {
            this.PeriodID = _prmPeriodID;
            this.Year = _prmYear;
            this.StartDate = _prmStartDate;
            this.EndDate = _prmEndDate;
        }

    }
}
