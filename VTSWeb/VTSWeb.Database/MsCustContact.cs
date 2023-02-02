using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTSWeb.Database
{
    public partial class MsCustContact
    {
        string _custName = "";

        public MsCustContact(String _prmCustCode, String _prmCustName, int _prmItemNo, String _prmContactType, String _prmContactName, String _prmContactTitle, DateTime? _prmBirthday,
            String _prmReligion, String _prmAddress1, String _prmAddress2, String _prmCountry, String _prmZipCode, String _prmPhone,
            String _prmFax, String _prmEmail, String _prmRemark, Char _prmFgAccess, Char _prmFgGoodsIn, Char _prmFgGoodsOut, Char _prmFgAdditionalVisitor,
            Char _prmFgAuthorizationContact, String _prmCardID
            )
        {
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
            this.ItemNo   = _prmItemNo;
            this.ContactType = _prmContactType;
            this.ContactName = _prmContactName;
            this.ContactTitle = _prmContactTitle;
            this.Birthday = _prmBirthday;
            this.Religion = _prmReligion;
            this.Address1 = _prmAddress1;
            this.Address2 = _prmAddress2;
            this.Country = _prmCountry;
            this.ZipCode = _prmZipCode;
            this.Phone = _prmPhone;
            this.Fax = _prmFax;
            this.Email = _prmEmail;
            this.Remark = _prmRemark;
            this.FgAccess = _prmFgAccess;
            this.FgGoodsIn = _prmFgGoodsIn;
            this.FgGoodsOut = _prmFgGoodsOut;
            this.FgAdditionalVisitor = _prmFgAdditionalVisitor;
            this.FgAuthorizationContact = _prmFgAuthorizationContact;
            this.CardID = _prmCardID;

        }

        public MsCustContact( String _prmCustCode,String _prmContactName,Char _prmFgGoodsIn, Char _prmFgGoodsOut, String _prmCardID)
        {
            this.CustCode =  _prmCustCode; 
            this.ContactName = _prmContactName;            
            this.FgGoodsIn = _prmFgGoodsIn;
            this.FgGoodsOut = _prmFgGoodsOut;
            this.CardID = _prmCardID;

        }


        public MsCustContact(int _prmItemNo, String _prmContactName)
        {
            this.ItemNo = _prmItemNo;
            this.ContactName = _prmContactName;

        }
        public MsCustContact(string _prmCustCode, String _prmContactName)
        {
            this.CustCode = _prmCustCode;
            this.ContactName = _prmContactName;

        }
        public String CustName
        {
            get
            {
                return this._custName;
            }
            set
            {
                this._custName = value;
            }
        }
    }
}