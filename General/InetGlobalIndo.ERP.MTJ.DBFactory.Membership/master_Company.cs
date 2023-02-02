using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.Membership
{
    public partial class master_Company
    {
        public master_Company(Guid _prmCompanyId, string _prmName, string _prmLogo, string _prmAddress,string _prmCompanyTag)
        {
            this.CompanyID = _prmCompanyId;
            this.Name = _prmName;
            this.Logo = _prmLogo;
            this.PrimaryAddress = _prmAddress;
            this.CompanyTag = _prmCompanyTag;
        }

        public master_Company(Guid _prmCompanyId, string _prmName)
        {
            this.CompanyID = _prmCompanyId;
            this.Name = _prmName;
        }
    }
}
