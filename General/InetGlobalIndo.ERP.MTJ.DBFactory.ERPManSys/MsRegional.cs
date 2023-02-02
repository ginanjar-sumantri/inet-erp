using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsRegional
    {
        public MsRegional(string _prmRegionalCode, string _prmRegionalName)
        {
            this.RegionalCode = _prmRegionalCode;
            this.RegionalName = _prmRegionalName;
        }
    }
}
