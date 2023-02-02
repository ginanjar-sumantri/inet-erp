using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
   public partial class PORTMsServiceType
    {
       public PORTMsServiceType(Guid _prmServiceTypeCode, string _prmServiceTypeName)
           {
               this.ServiceTypeCode = _prmServiceTypeCode;
               this.ServiceTypeName = _prmServiceTypeName;
           }
    }
}
