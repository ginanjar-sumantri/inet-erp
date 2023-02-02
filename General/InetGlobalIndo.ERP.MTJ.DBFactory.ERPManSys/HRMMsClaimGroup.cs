using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMMsClaimGroup
    {
        public HRMMsClaimGroup(string _prmClaimGrpCode, string _prmClaimGrpName)
        {
            this.ClaimGrpCode = _prmClaimGrpCode;
            this.ClaimGrpName = _prmClaimGrpName;
        }
    }
}
