using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMSLibrary
{
    public partial class MSPackage
    {
        public MSPackage(String _prmPackageName, int? _prmSMSPerDay)
        {
            this.PackageName = _prmPackageName;
            this.SMSPerDay = _prmSMSPerDay;
        }
    }
}
