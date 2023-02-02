using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMSLibrary
{
    public partial class MsUser
    {
        private string _organizationName = "";

        public MsUser(String _prmOrganizationName, String _prmUserID, bool _prmFgAdmin, String _prmPackageName, String _prmEmail)
        {
            this.OrganizationName = _prmOrganizationName;
            this.UserID = _prmUserID;
            this.fgAdmin = _prmFgAdmin;
            this.PackageName = _prmPackageName;
            this.Email = _prmEmail;
        }

        public string OrganizationName
        {
            get
            {
                return this._organizationName;
            }
            set
            {
                this._organizationName = value;
            }
        }
    }
}
