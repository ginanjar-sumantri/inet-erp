using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.Membership
{
    public partial class master_Company_aspnet_Role
    {
        public string _name = "";

        public master_Company_aspnet_Role(Guid _prmCompanyId, Guid _prmRoleID, string _prmName)
        {
            this.CompanyID = _prmCompanyId;
            this.RoleId = _prmRoleID;
            this.Name = _prmName;
        }

        public master_Company_aspnet_Role(Guid _prmCompanyId, Guid _prmRoleID)
        {
            this.CompanyID = _prmCompanyId;
            this.RoleId = _prmRoleID;
        }

        public master_Company_aspnet_Role(Guid _prmRoleID, string _prmName)
        {
            this.RoleId = _prmRoleID;
            this.Name = _prmName;
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
