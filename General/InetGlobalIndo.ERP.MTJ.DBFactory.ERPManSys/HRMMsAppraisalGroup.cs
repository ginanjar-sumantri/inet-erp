using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMMsAppraisalGroup
    {
        public HRMMsAppraisalGroup(string _prmAppraisalGrpCode, string _prmAppraisalGrpName)
        {
            this.AppraisalGrpCode = _prmAppraisalGrpCode;
            this.AppraisalGrpName = _prmAppraisalGrpName;
        }
    }
}
