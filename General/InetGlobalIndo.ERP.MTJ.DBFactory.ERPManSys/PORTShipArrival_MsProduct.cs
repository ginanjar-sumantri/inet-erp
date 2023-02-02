using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTShipArrival_MsProduct
    {
        public PORTShipArrival_MsProduct(string _prmProductCode, Guid _prmShipArrivalCode)
        {
            this.HdShipArrivalCode = _prmShipArrivalCode;
            this.ProductCode = _prmProductCode;

        }
    }
}
