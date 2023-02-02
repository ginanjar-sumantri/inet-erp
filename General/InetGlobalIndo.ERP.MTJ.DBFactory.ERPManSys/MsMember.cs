using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsMember
    {
        private String _prmMemberTypeName = "";

        public MsMember(String _prmMemberCode, String _prmMemberName, String _prmReligionCode,
            String _prmMemberTypeCode, int _prmIdentityType, String _prmCountryCode, String _prmIdentityNumber,
            Byte _prmGender, DateTime _prmDateOfBirth, String _prmPlaceOfBirth, String _prmAddress,
            String _prmTelephone1, String _prmTelephone2, String _prmHandPhone1, String _prmHandPhone2,
            String _prmEmail, String _prmReferenceCode, DateTime _prmActivationDate, String _prmHobby,
            String _prmSourceInfo, Byte _prmStatus)
        {
            this.MemberCode = _prmMemberCode;
            this.MemberName = _prmMemberName;
            this.ReligionCode = _prmReligionCode;
            this.MemberTypeCode = _prmMemberTypeCode;
            this.IdentityType = _prmIdentityType;
            this.IdentityNumber = _prmIdentityNumber;
            this.Gender = _prmGender;
            this.DateOfBirth = _prmDateOfBirth;
            this.PlaceOfBirth = _prmPlaceOfBirth;
            this.Address = _prmAddress;
            this.Telephone1 = _prmTelephone1;
            this.Telephone2 = _prmTelephone2;
            this.HandPhone1 = _prmHandPhone1;
            this.HandPhone2 = _prmHandPhone2;
            this.Email = _prmEmail;
            this.ReferenceCode = _prmReferenceCode;
            this.ActivationDate = _prmActivationDate;
            this.Hobby = _prmHobby;
            this.SourceInfo = _prmSourceInfo;
            this.Status = _prmStatus;
        }

        public MsMember(String _prmMemberCode, String _prmMemberTypeCode, String _prmMemberTypeName,
             String _prmMemberName, Byte _prmGender , String _prmHandPhone1, String _prmEmail, Byte _prmStatus)
        {
            this.MemberCode = _prmMemberCode;
            this.MemberTypeCode = _prmMemberTypeCode;
            this.MemberTypeName = _prmMemberTypeName;
            this.MemberName = _prmMemberName;
            this.Gender = _prmGender;
            this.HandPhone1 = _prmHandPhone1;
            this.Email = _prmEmail;
            this.Status = _prmStatus;
        }

        public MsMember(String _prmMemberCode, String _prmMemberTypeCode, String _prmMemberTypeName,
            byte _prmMemberTitle, String _prmMemberName, Byte _prmGender, String _prmCompany,
            String _prmJobTtlCode, String _prmJobLvlCode, byte _prmSalary, Guid _prmEducationCode, 
            String _prmAddress, String _prmCityCode, String _prmZipCode, String _prmTelephone1, 
            String _prmHandPhone1, String _prmEmail, Byte _prmStatus)
        {
            this.MemberCode = _prmMemberCode;
            this.MemberTypeCode = _prmMemberTypeCode;
            this.MemberTypeName = _prmMemberTypeName;
            this.MemberTitle = _prmMemberTitle;
            this.MemberName = _prmMemberName;
            this.Gender = _prmGender;
            this.Company = _prmCompany;
            this.JobTtlCode = _prmJobTtlCode;
            this.JobLvlCode = _prmJobLvlCode;
            this.Salary = _prmSalary;
            this.EducationCode = _prmEducationCode;
            this.Address = _prmAddress;
            this.CityCode = _prmCityCode;
            this.ZipCode = _prmZipCode;
            this.Telephone1 = _prmTelephone1;
            this.HandPhone1 = _prmHandPhone1;
            this.Email = _prmEmail;
            this.Status = _prmStatus;
        }

        public MsMember(String _prmMemberCode, String _prmMemberTypeCode, String _prmMemberTypeName,
            byte _prmMemberTitle, String _prmMemberName, Byte _prmGender, String _prmCompany,
            String _prmJobTtlCode, String _prmJobLvlCode, byte _prmSalary, Guid _prmEducationCode,
            String _prmAddress, String _prmCityCode, String _prmZipCode, String _prmTelephone1,
            String _prmHandPhone1, String _prmEmail, Byte _prmStatus, String _prmBarcode)
        {
            this.MemberCode = _prmMemberCode;
            this.MemberTypeCode = _prmMemberTypeCode;
            this.MemberTypeName = _prmMemberTypeName;
            this.MemberTitle = _prmMemberTitle;
            this.MemberName = _prmMemberName;
            this.Gender = _prmGender;
            this.Company = _prmCompany;
            this.JobTtlCode = _prmJobTtlCode;
            this.JobLvlCode = _prmJobLvlCode;
            this.Salary = _prmSalary;
            this.EducationCode = _prmEducationCode;
            this.Address = _prmAddress;
            this.CityCode = _prmCityCode;
            this.ZipCode = _prmZipCode;
            this.Telephone1 = _prmTelephone1;
            this.HandPhone1 = _prmHandPhone1;
            this.Email = _prmEmail;
            this.Status = _prmStatus;
            this.Barcode = _prmBarcode;
        }

        public String MemberTypeName
        {
            get
            {
                return this._prmMemberTypeName;
            }
            set
            {
                this._prmMemberTypeName = value;
            }
        }
    }
}
