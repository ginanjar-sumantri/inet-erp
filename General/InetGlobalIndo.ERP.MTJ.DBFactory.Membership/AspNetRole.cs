using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.Membership
{
    public partial class aspnet_Role
    {
        public aspnet_Role(Guid _prmRoleId, string _prmRoleName, string _prmDescription)
        {
            this.RoleId = _prmRoleId;
            this.RoleName = _prmRoleName;
            this.Description = _prmDescription;
        }

        public aspnet_Role(Guid _prmRoleId, string _prmRoleName)
        {
            this.RoleId = _prmRoleId;
            this.RoleName = _prmRoleName;
        }

        public aspnet_Role(string _prmRoleName)
        {
            this.RoleName = _prmRoleName;
        }
    }
}
