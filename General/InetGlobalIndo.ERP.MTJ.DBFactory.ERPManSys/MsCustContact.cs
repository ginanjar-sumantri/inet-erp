using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsCustContact
    {
        private string _CountryName = "";

        public MsCustContact(string _prmCustCode, int _prmItemNo, string _prmContactName, string _prmContactTitle, string _prmAddress1, string _prmCountry, string _prmCountryName, string _prmPhone, string _prmEmail)
        {
            this.CustCode = _prmCustCode;
            this.ItemNo = _prmItemNo;
            this.ContactName = _prmContactName;
            this.ContactTitle = _prmContactTitle;
            this.Address1 = _prmAddress1;
            this.Country = _prmCountry;
            this.CountryName = _prmCountryName;
            this.Phone = _prmPhone;
            this.Email = _prmEmail;
        }

        public string CountryName
        {
            get
            {
                return this._CountryName;
            }
            set
            {
                this._CountryName = value;
            }
        }
    }
}
