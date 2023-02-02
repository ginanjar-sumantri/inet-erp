using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.Membership
{
    public partial class master_Company_aspnet_User
    {
        public string _name = "";

        public master_Company_aspnet_User(Guid _prmCompanyId, Guid _prmUserID, string _prmName)
        {
            this.CompanyID = _prmCompanyId;
            this.UserID = _prmUserID;
            this.Name = _prmName;
        }

        public master_Company_aspnet_User(Guid _prmUserID, string _prmName)
        {
            this.UserID = _prmUserID;
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
