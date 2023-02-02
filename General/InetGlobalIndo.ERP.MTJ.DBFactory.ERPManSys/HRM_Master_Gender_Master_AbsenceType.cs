using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_Master_Gender_Master_AbsenceType
    {
        private string _genderName = "";
        private string _absenceTypeName = "";
        
        public HRM_Master_Gender_Master_AbsenceType(string _prmGenderCode, string _prmGenderName, String _prmAbsenceTypeCode, string _prmAbsenceTypeName)
        {
            this.GenderCode = _prmGenderCode;
            this.GenderName = _prmGenderName;
            this.AbsenceTypeCode = _prmAbsenceTypeCode;
            this.AbsenceTypeName = _prmAbsenceTypeName;
        }

        public string GenderName
        {
            get
            {
                return this._genderName;
            }
            set
            {
                this._genderName = value;
            }
        }

        public string AbsenceTypeName
        {
            get
            {
                return this._absenceTypeName;
            }
            set
            {
                this._absenceTypeName = value;
            }
        }
    }
}
