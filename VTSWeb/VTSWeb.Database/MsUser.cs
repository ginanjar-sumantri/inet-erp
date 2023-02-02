using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTSWeb.Database
{
    public partial class MsUser
    {
        public MsUser(String _prmUserName, String _prmLoweredUserName, String _prmPassword, String _prmEmail, Int32 _prmPermissionLevelCode)
        {
            this.UserName = _prmUserName;
            this.LoweredUserName = _prmLoweredUserName;
            this.Password = _prmPassword;
            this.Email = _prmEmail;
            this.PermissionLevelCode = _PermissionLevelCode;
        }
    }
}
