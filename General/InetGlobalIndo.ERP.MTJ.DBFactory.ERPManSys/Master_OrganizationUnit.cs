using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_OrganizationUnit
    {
        public Master_OrganizationUnit(string _prmOrgUnit, string _prmDescription, string _prmParentOrgUnit, string _prmAddress, string _prmContactInfo, string _prmNote, byte _prmFgActive)
        {
            this.OrgUnit = _prmOrgUnit;
            this.Description = _prmDescription;
            this.ParentOrgUnit = _prmParentOrgUnit;
            this.Address = _prmAddress;
            this.ContactInfo = _prmContactInfo;
            this.Note = _prmNote;
            this.FgActive = _prmFgActive;
        }

        public Master_OrganizationUnit(string _prmOrgUnit, string _prmDescription)
        {
            this.OrgUnit = _prmOrgUnit;
            this.Description = _prmDescription;
        }
    }
}
