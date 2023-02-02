using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.Membership
{
    public partial class master_RoleMenu
    {
        public master_RoleMenu(Guid _prmRoleId, short _prmMenuId)
        {
            this.RoleId = _prmRoleId;
            this.MenuId = _prmMenuId;
        }
    }
}
