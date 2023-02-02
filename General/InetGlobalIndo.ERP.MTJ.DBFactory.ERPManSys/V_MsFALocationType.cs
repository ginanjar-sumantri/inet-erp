using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class V_MsFALocationType
    {
        public V_MsFALocationType(string _prmCode, string _prmName)
        {
            this.Code = _prmCode;
            this.Name = _prmName;
        }

        ~V_MsFALocationType()
        {
        }
    }
}
