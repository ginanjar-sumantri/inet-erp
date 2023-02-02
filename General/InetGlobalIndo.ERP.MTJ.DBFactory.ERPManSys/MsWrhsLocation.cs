using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsWrhsLocation
    {
        public MsWrhsLocation(string _prmWLocationCode, string _prmWLocationName)
        {
            this.WLocationCode = _prmWLocationCode;
            this.WLocationName = _prmWLocationName;

        }
    }
}
