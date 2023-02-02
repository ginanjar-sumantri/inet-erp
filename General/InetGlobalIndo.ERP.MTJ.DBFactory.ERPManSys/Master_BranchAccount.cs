using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_BranchAccount
    {
        public Master_BranchAccount(Guid _prmBranchAccCode, String _prmBranchAccID, string _prmBranchAccName)
        {
            this.BranchAccCode = _prmBranchAccCode;
            this.BranchAccID = _prmBranchAccID;
            this.BranchAccName = _prmBranchAccName;
        }
    }
}
