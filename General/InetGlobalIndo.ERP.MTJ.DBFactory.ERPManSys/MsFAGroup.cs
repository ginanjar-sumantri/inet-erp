using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsFAGroup
    {
        public MsFAGroup(string _prmFAGroupCode, string _prmFAGroupName)
        {
            this.FAGroupCode = _prmFAGroupCode;
            this.FAGroupName = _prmFAGroupName;
        }
    }
}
