using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.Membership
{
    public partial class master_Database
    {
        public master_Database(Guid _prmDatabaseId, string _prmName, string _prmServer, string _prmUID, string _prmPWD, byte _prmStatus)
        {
            this.DatabaseID = _prmDatabaseId;
            this.Name = _prmName;
            this.Server = _prmServer;
            this.UID = _prmUID;
            this.PWD = _prmPWD;
            this.Status = _prmStatus;
        }

        public master_Database(Guid _prmDatabaseId, string _prmName, byte _prmStatus)
        {
            this.DatabaseID = _prmDatabaseId;
            this.Name = _prmName;
            this.Status = _prmStatus;
        }

        public master_Database(Guid _prmDatabaseId, string _prmName)
        {
            this.DatabaseID = _prmDatabaseId;
            this.Name = _prmName;
        }
    }
}
