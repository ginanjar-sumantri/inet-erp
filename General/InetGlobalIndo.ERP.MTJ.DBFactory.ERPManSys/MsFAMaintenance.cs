using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsFAMaintenance:Base
    {
        public MsFAMaintenance(string _faCode, string _faName,char _fgAddValue)
        {
            this.FAMaintenanceCode = _faCode;
            this.FAMaintenanceName = _faName;
            this.FgAddValue = _fgAddValue;
        }

        public MsFAMaintenance(string _faCode, string _faName)
        {
            this.FAMaintenanceCode = _faCode;
            this.FAMaintenanceName = _faName;
        }
    }
}
