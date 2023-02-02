using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class POSMsMenuServiceType
    {

        public POSMsMenuServiceType(String _prmMenuServiceTypeCode, String _prmMenuServiceTypeName)
        {
            this.MenuServiceTypeCode = _prmMenuServiceTypeCode;
            this.MenuServiceTypeName = _prmMenuServiceTypeName;
        }

        ~POSMsMenuServiceType() { }
    }
}
