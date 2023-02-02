using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsShipment
    {
        public MsShipment(string _prmShipmentCode ,string _prmShipmentName )
        {
            this.ShipmentCode = _prmShipmentCode;
            this.ShipmentName = _prmShipmentName;
        }

        public MsShipment(string _prmShipmentCode)
        {
            this.ShipmentCode = _prmShipmentCode;
           
        }
    }
}
