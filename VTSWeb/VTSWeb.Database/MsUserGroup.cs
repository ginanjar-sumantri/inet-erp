using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTSWeb.Database
{
    public partial class MsUserGroup
    {
        public MsUserGroup(String _prmUserGroupCode, String _prmUserGroupName, Boolean _prmFgAdmin)
        {
            this.UserGroupCode = _prmUserGroupCode;
            this.UserGroupName = _prmUserGroupName;
            this.FgAdmin = _prmFgAdmin;
        }

        public MsUserGroup(String _prmUserGroupCode, String _prmUserGroupName)
        {
            this.UserGroupCode = _prmUserGroupCode;
            this.UserGroupName = _prmUserGroupName;
        }

    }
}
