using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_Master_WorkingHour
    {
        public HRM_Master_WorkingHour(Guid _prmWorkingHourCode, string _prmWorkingHourName, bool _prmIsActive, string _prmDescription)
        {
            this.WorkingHourCode = _prmWorkingHourCode;
            this.WorkingHourName = _prmWorkingHourName;
            this.IsActive = _prmIsActive;
            this.Description = _prmDescription;
        }

        public HRM_Master_WorkingHour(Guid _prmWorkingHourCode, string _prmWorkingHourName)
        {
            this.WorkingHourCode = _prmWorkingHourCode;
            this.WorkingHourName = _prmWorkingHourName;
        }
    }
}
