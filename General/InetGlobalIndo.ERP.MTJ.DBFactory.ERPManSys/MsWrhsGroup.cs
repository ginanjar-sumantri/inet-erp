using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsWrhsGroup
    {
        public MsWrhsGroup(string _prmWrhsGroupCode, string _prmWrhsGroupName)
        {
            this.WrhsGroupCode = _prmWrhsGroupCode;
            this.WrhsGroupName = _prmWrhsGroupName;
        }
    }
}
