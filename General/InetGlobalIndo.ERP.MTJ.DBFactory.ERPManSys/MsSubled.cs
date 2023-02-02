using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsSubled :Base
    {
        public MsSubled ( char _subledCode,  string _subledName)
        {
            this.SubledCode = _subledCode;
            this.SubledName = _subledName;
        }
    }
}
