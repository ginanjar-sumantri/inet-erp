using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.Membership
{
    public partial class master_Database_aspnet_User
    {
        public string _name = "";

        public master_Database_aspnet_User(Guid _prmDatabaseID, Guid _prmUserID, string _prmName)
        {
            this.DatabaseID = _prmDatabaseID;
            this.UserID = _prmUserID;
            this.Name = _prmName;
        }

        public master_Database_aspnet_User(Guid _prmDatabaseID)
        {
            this.DatabaseID = _prmDatabaseID;
        }

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }
    }
}
