using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTMsShipType
    {
        public PORTMsShipType(Guid _prmShipTypeCode, string _prmShipTypeName)
        {
            this.ShipTypeCode = _prmShipTypeCode;
            this.ShipTypeName = _prmShipTypeName;
        }
    }
}
