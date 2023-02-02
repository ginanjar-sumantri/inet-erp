using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.Membership
{
    public partial class master_Menu
    {
        public master_Menu(String _prmModuleID, String _prmText)
        {
            this.ModuleID = _prmModuleID;
            this.Text = _prmText;
        }

        public master_Menu(short _prmMenuID, String _prmText)
        {
            this.MenuID = _prmMenuID;
            this.Text = _prmText;
        }

        private String _roleID;
        public String RoleID
        {
            get { return this._roleID; }
            set { this._roleID = value; }
        }


    }
}
