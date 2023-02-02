using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTMsPartShip
    {
        public PORTMsPartShip(Guid _prmPartShipCode, string _prmPartID, string _prmPartName)
        {
            this.PartShipCode = _prmPartShipCode;
            this.PartID = _prmPartID;
            this.PartName = _prmPartName;
        }
    }
}
