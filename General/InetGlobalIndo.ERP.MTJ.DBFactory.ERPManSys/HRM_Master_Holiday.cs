using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_Master_Holiday
    {
        public HRM_Master_Holiday(DateTime _prmHolidayDate, string _prmDescription, Boolean? _prmIsCutLeave)
        {
            this.HolidayDate = _prmHolidayDate;
            this.Description = _prmDescription;
            this.isCutLeave = _prmIsCutLeave;
        }

        public HRM_Master_Holiday(DateTime _prmHolidayDate, string _prmDescription)
        {
            this.HolidayDate = _prmHolidayDate;
            this.Description = _prmDescription;
        }
    }
}
