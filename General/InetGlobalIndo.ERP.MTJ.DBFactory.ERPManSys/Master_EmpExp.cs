using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_EmpExp
    {
        string _companyCityName = "";
        string _jobTitleName = "";

        public Master_EmpExp(Guid _prmEmpExpCode, string _prmEmpNmbr, string _prmCompanyName, string _prmCompanyCity, string _prmCompanyCityName, string _prmJobTitle, string _prmJobTitleName, DateTime _prmStartDate, DateTime _prmEndDate, string _prmAddress1, string _prmAddress2, string _prmPhone, string _prmCurrCode, decimal _prmLastSalary)
        {
            this.EmpExpCode = _prmEmpExpCode;
            this.EmpNumb = _prmEmpNmbr;
            this.CompanyName = _prmCompanyName;
            this.CompanyCity = _prmCompanyCity;
            this.CompanyCityName = _prmCompanyCityName;
            this.JobTitle = _prmJobTitle;
            this.JobTitleName = _prmJobTitleName;
            this.StartDate = _prmStartDate;
            this.EndDate = _prmEndDate;
            this.Address1 = _prmAddress1;
            this.Address2 = _prmAddress2;
            this.Phone = _prmPhone;
            this.CurrCode = _prmCurrCode;
            this.LastSalary = _prmLastSalary;
        }

        public string CompanyCityName
        {
            get
            {
                return this._companyCityName;
            }
            set
            {
                this._companyCityName = value;
            }
        }

        public string JobTitleName
        {
            get
            {
                return this._jobTitleName;
            }
            set
            {
                this._jobTitleName = value;
            }
        }
    }
}
