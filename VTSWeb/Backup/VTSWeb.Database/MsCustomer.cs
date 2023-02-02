using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTSWeb.Database
{
    public partial class MsCustomer
    {
        public MsCustomer(String _prmCustCode, String _prmCustName, String _prmCustType, String _prmAddress1, String _prmAddress2,
            String _prmZipCode, String _prmPhone,String _prmFax, String _prmEmail,
            Char _prmFgActive, String _prmContactName, String _prmContactTitle, String _prmContactHP, String _prmContactEmail
            )
        {
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
            this.CustType = _prmCustType;
            this.Address1 = _prmAddress1;
            this.Address2 = _prmAddress2;
            this.ZipCode = _prmZipCode;
            this.Phone = _prmPhone;
            this.Fax = _prmFax;
            this.Email = _prmEmail;
            this.FgActive = _prmFgActive ;
            this.ContactName = _prmContactName;
            this.ContactTitle = _prmContactTitle;
            this.ContactHP = _prmContactHP;
            this.ContactEmail = _prmContactEmail;

        }
    }
    public partial class MsCustomer
    {
        public MsCustomer(String _prmCustCode, String _prmCustName)
        {
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
        }
    }
}