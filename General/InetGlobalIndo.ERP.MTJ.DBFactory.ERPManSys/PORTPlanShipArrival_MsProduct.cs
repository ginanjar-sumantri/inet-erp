using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTPlanShipArrival_MsProduct
    {
        public PORTPlanShipArrival_MsProduct(string _prmProductCode, Guid _prmShipArrivalCode)
        {
            this.HdPlanShipArrivalCode = _prmShipArrivalCode;
            this.ProductCode = _prmProductCode;
        }
    }
}
