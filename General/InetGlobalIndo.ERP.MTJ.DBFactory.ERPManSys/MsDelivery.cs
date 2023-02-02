using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsDelivery
    {
        public MsDelivery(string _prmDeliveryCode, string _prmDeliveryName)
        {
            this.DeliveryCode = _prmDeliveryCode;
            this.DeliveryName = _prmDeliveryName;
        }

        public MsDelivery(string _prmDeliveryCode, string _prmDeliveryName, string _prmAddress1)
        {
            this.DeliveryCode = _prmDeliveryCode;
            this.DeliveryName = _prmDeliveryName;
            this.Address1 = _prmAddress1;
        }
    }
}
