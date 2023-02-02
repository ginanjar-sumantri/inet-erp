using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsSuppContact
    {
        public MsSuppContact(string _prmSuppCode, int _prmItemNo, string _prmContactName, string _prmPhone, string _prmEmail)
        {
            this.SuppCode = _prmSuppCode;
            this.ItemNo = _prmItemNo;
            this.ContactName = _prmContactName;
            this.Telephone = _prmPhone;
            this.Email = _prmEmail;
        }
    }
}
