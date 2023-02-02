using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTSWeb.Database
{
    public partial class MsRegional
    {
        public MsRegional(String _prmRegionalCode, String _prmRegionalName)
        {
            this.RegionalCode = _prmRegionalCode;
            this.RegionalName = _prmRegionalName;
    
        }

        public MsRegional(String _prmRegionalName)
        {
            this.RegionalName = _prmRegionalName;
        }
    }
}