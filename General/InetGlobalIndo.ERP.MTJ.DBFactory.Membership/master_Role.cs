using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.Membership
{
    public partial class master_Role
    {
        public master_Role(Guid _prmRoleId, Boolean _prmSystemRole)
        {
            this.RoleId = _prmRoleId;
            this.SystemRole = _prmSystemRole;
        }
    }
}
