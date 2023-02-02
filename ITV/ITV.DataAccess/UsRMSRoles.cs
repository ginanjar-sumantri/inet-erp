using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITV.DataAccess.ITVDatabase
{
    public partial class UsRMSRoles : Base
    {
        public UsRMSRoles(long _prmUsrmSRolesID, String _prmRoleName)
        {
            this.UsrmSRolesID = _prmUsrmSRolesID;
            this.RoleName = _prmRoleName;
        }

        public UsRMSRoles(String _prmRoleName, String _prmLoweredRoleName, Boolean _prmSystemRole)
        {
            this.RoleName = _prmRoleName;
            this.LoweredRoleName = _prmLoweredRoleName;
            this.SystemRole = _prmSystemRole;
        }
    }
}
