using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.Membership
{
    public partial class master_Company_master_Database
    {
        private string _name = "";
        private byte _status = 0;

        public master_Company_master_Database(Guid _prmCompanyId, Guid _prmDatabaseID, string _prmName)
        {
            this.CompanyID = _prmCompanyId;
            this.DatabaseID = _prmDatabaseID;
            this.Name = _prmName;
        }

        public master_Company_master_Database(Guid _prmCompanyId, Guid _prmDatabaseID, string _prmName, byte _prmStatus)
        {
            this.CompanyID = _prmCompanyId;
            this.DatabaseID = _prmDatabaseID;
            this.Name = _prmName;
            this._status = _prmStatus;
        }

        public master_Company_master_Database(Guid _prmDatabaseID, string _prmName)
        {
            this.DatabaseID = _prmDatabaseID;
            this.Name = _prmName;
        }

        public byte Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
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
