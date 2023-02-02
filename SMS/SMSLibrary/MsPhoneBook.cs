using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMSLibrary
{
      
    public partial class MsPhoneBook
    {
            public MsPhoneBook(long _prmid, long _prmOrganizationID, String _prmUserID, String _prmName,
            String _prmPhoneNumber, String _prmCompany, DateTime? _prmDateOfBirth, String _prmReligion,
            String _prmEmail, String _prmCity, String _prmPhoneBookGroup, String _prmRemark,
            bool _prmfgBirthDay, String _prmBirthdayWishes, string _prmJobTitle, string _prmAddress, string _prmNameCardPicture)
        {
            this.id = _prmid;
            this.OrganizationID = _prmOrganizationID;
            this.UserID = _prmUserID;
            this.Name = _prmName;
            this.PhoneNumber = _prmPhoneNumber;
            this.Company = _prmCompany;
            this.DateOfBirth = _prmDateOfBirth;
            this.Religion = _prmReligion;
            this.Email = _prmEmail;
            this.City = _prmCity;
            this.PhoneBookGroup = _prmPhoneBookGroup;
            this.Remark = _prmRemark;
            this.fgBirthDay = _prmfgBirthDay;
            this.BirthdayWishes = _prmBirthdayWishes;
            this.JobTitle = _prmJobTitle;
            this.Address = _prmAddress;
            this.NameCardPicture = _prmNameCardPicture;
        }

        public MsPhoneBook(long _prmid, long _prmOrganizationID, String _prmUserID, String _prmName,
            String _prmPhoneNumber, String _prmEmail, String _prmPhoneBookGroup)
        {
            this.id = _prmid;
            this.OrganizationID = _prmOrganizationID;
            this.UserID = _prmUserID;
            this.Name = _prmName;
            this.PhoneNumber = _prmPhoneNumber;
            this.Email = _prmEmail;
            this.PhoneBookGroup = _prmPhoneBookGroup;
        }

        public MsPhoneBook(String _prmPhoneBookGroup)
        {
            this.PhoneBookGroup = _prmPhoneBookGroup;
        }

        public MsPhoneBook(long _prmid, String _prmName)
        {
            this.id = _prmid;
            this.Name = _prmName;
        }

        public MsPhoneBook(long _prmid, String _prmNameCardPicture, String _prmRemark)
        {
            this.id = _prmid;
            this.NameCardPicture = _prmNameCardPicture;
            this.Remark = _prmRemark;
        }

        public MsPhoneBook(long _prmid, String _prmName, String _prmPhoneNumber, String _prmPhoneBookGroup)
        {
            this.id = _prmid;
            this.Name = _prmName;
            this.PhoneNumber = _prmPhoneNumber;
            this.PhoneBookGroup = _prmPhoneBookGroup;
        }

        //public String CustName
        //{
        //    get
        //    {
        //        return this._custName;
        //    }
        //    set
        //    {
        //        this._custName = value;
        //    }
        //}
    }

}
