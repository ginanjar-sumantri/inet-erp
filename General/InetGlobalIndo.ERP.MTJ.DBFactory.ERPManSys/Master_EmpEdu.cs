using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_EmpEdu
    {
        string _eduName = "";
        string _cityName = "";

        public Master_EmpEdu(Guid _prmEmpEduCode, string _prmEmpNmbr, Guid _prmEducationCode, string _prmEducationName, string _prmDegree, string _prmInstitution, string _prmCityCode, string _prmCityName, int? _prmStartYear, int? _prmGraduated)
        {
            this.EmpEduCode = _prmEmpEduCode;
            this.EmpNmbr = _prmEmpNmbr;
            this.EducationCode = _prmEducationCode;
            this.EducationName = _prmEducationName;
            this.Degree = _prmDegree;
            this.Institution = _prmInstitution;
            this.CityCode = _prmCityCode;
            this.CityName = _prmCityName;
            this.StartYear = _prmStartYear;
            this.Graduated = _prmGraduated;
        }

        public string EducationName
        {
            get
            {
                return this._eduName;
            }
            set
            {
                this._eduName = value;
            }
        }

        public string CityName
        {
            get
            {
                return this._cityName;
            }
            set
            {
                this._cityName = value;
            }
        }
    }
}
