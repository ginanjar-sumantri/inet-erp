using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsFALocation
    {
        public MsFALocation(string _prmFALocationCode, string _prmFALocationName)
        {
            this.FALocationCode = _prmFALocationCode;
            this.FALocationName = _prmFALocationName;
        }
    }
}
