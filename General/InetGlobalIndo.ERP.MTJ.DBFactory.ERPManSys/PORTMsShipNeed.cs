using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTMsShipNeed
    {
        public PORTMsShipNeed(Guid _prmCode, string _prmName, string _prmID)
        {
            this.ShipNeedCode = _prmCode;
            this.ShipNeedName = _prmName;
            this.ShipNeedID = _prmID;                      
        }
    }
}
