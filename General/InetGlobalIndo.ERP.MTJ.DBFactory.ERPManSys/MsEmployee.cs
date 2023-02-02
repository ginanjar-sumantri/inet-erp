using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsEmployee
    {
        public MsEmployee(string _prmEmpNumb, string _prmEmpName, string _prmJobLevel, string _prmJobTitle, string _prmEmpTypeName, char _prmFgActive)
        {
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.JobLevel = _prmJobLevel;
            this.JobTitle = _prmJobTitle;
            this.EmpType = _prmEmpTypeName;
            this.FgActive = _prmFgActive;
        }

        public MsEmployee(string _prmEmpNumb, string _prmEmpName, string _prmJobLevel, string _prmJobTitle, string _prmEmpTypeName,
            String _prmBankCode, String _prmBankRekNo, String _prmBankRekName, DateTime _prmStartDate, String _prmWorkPlace)
        {
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.JobLevel = _prmJobLevel;
            this.JobTitle = _prmJobTitle;
            this.EmpType = _prmEmpTypeName;
            this.BankCode = _prmBankCode;
            this.BankRekNo = _prmBankRekNo;
            this.BankRekName = _prmBankRekName;
            this.StartDate = _prmStartDate;
            this.WorkPlace = _prmWorkPlace;
        }

        public MsEmployee(String _prmEmpNumb, String _prmEmpName, String _prmGender, String _prmBirthPlace, DateTime _prmBirthDate
            , String _prmTribe, String _prmAbsenceCardNo, String _prmTypeCard, String _prmIDCard, String _prmReligion, String _prmBloodType
            , Int32 _prmWeight, Int32 _prmHeight, String _prmHandPhone1, String _prmHandPhone2, String _prmEmail, Char _prmFgPPh21,
            String _prmNPWP, String _prmMaritalSt, String _prmMaritalTax, DateTime _prmStartDate, DateTime _prmTakenLeave, DateTime _prmEndDate
            , String _prmPensiunNo, String _prmJamSosTekNo, DateTime _prmJamSosTekDate, String _prmResAddr1, String _prmResAddr2, String _prmResZipCode
            , String _prmResCity, String _prmResPhone, String _prmOriAddr1, String _prmOriAddr2, String _prmOriZipCode, String _prmOriCity, String _prmOriPhone
            , String _prmSalaryType, Char _prmFgActive, String _prmSKNo, String _prmJobLevel, String _prmJobTitle, String _prmEmpType, String _prmWorkPlace, String _prmOldEmpNumb
            , String _prmUserID, DateTime _prmUserDate, Char _prmFgSales, Char _prmFgDriver, Char _prmFgOperator, Char _prmFgTeknisi
            , String _prmNationality, DateTime _prmMaritalDate, String _prmMaritalDocNo, String _prmPhoto, String _prmFingerPrintID, String _prmMethodSalary
            , DateTime _prmContractEndDate, String _prmBankCode, String _prmBankRekNo, String _prmBankRekName
            , DateTime _prmNPWPDate)
        {
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.Gender = _prmGender;
            this.BirthPlace = _prmBirthPlace;
            this.BirthDate = _prmBirthDate;
            this.Tribe = _prmTribe;
            this.AbsenceCardNo = _prmAbsenceCardNo;
            this.TypeCard = _prmTypeCard;
            this.IDCard = _prmIDCard;
            this.Religion = _prmReligion;
            this.BloodType = _prmBloodType;
            this.Weight = _prmWeight;
            this.Height = _prmHeight;
            this.HandPhone1 = _prmHandPhone1;
            this.HandPhone2 = _prmHandPhone2;
            this.Email = _prmEmail;
            this.FgPPh21 = _prmFgPPh21;
            this.NPWP = _prmNPWP;
            this.MaritalSt = _prmMaritalSt;
            this.MaritalTax = _prmMaritalTax;
            this.StartDate = _prmStartDate;
            this.TakenLeave = _prmTakenLeave;
            this.EndDate = _prmEndDate;
            this.PensiunNo = _prmPensiunNo;
            this.JamSosTekNo = _prmJamSosTekNo;
            this.JamSosTekDate = _prmJamSosTekDate;
            this.ResAddr1 = _prmResAddr1;
            this.ResAddr2 = _prmResAddr2;
            this.ResZipCode = _prmResZipCode;
            this.ResCity = _prmResCity;
            this.ResPhone = _prmResPhone;
            this.OriAddr1 = _prmOriAddr1;
            this.OriAddr2 = _prmOriAddr2;
            this.OriZipCode = _prmOriZipCode;
            this.OriCity = _prmOriCity;
            this.OriPhone = _prmOriPhone;
            this.SalaryType = _prmSalaryType;
            this.FgActive = _prmFgActive;
            this.SKNo = _prmSKNo;
            this.JobLevel = _prmJobLevel;
            this.JobTitle = _prmJobTitle;
            this.EmpType = _prmEmpType;
            this.WorkPlace = _prmWorkPlace;
            this.OldEmpNumb = _prmOldEmpNumb;
            this.UserID = _prmUserID;
            this.UserDate = _prmUserDate;
            this.FgSales = _prmFgSales;
            this.FgDriver = _prmFgDriver;
            this.FgOperator = _prmFgOperator;
            this.FgTeknisi = _prmFgTeknisi;
            this.Nationality = _prmNationality;
            this.MaritalDate = _prmMaritalDate;
            this.MaritalDocNo = _prmMaritalDocNo;
            this.Photo = _prmPhoto;
            this.FingerPrintID = _prmFingerPrintID;
            this.MethodSalary = _prmMethodSalary;
            this.ContractEndDate = _prmContractEndDate;
            this.BankCode = _prmBankCode;
            this.BankRekNo = _prmBankRekNo;
            this.BankRekName = _prmBankRekName;
            this.NPWPDate = _prmNPWPDate;
        }

        public MsEmployee(string _prmEmpNumb, string _prmEmpName)
        {
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
        }

        public MsEmployee(string _prmEmpNumb, string _prmEmpName, string _prmEmail)
        {
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.Email = _prmEmail;
        }
    }
}
