using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_EmpType_AbsenceType
    {
        public string _absenceTypeName = "";

        public HRM_EmpType_AbsenceType(Guid _prmEmpTypeAbsTypeCode, string _prmEmpTypeCode, String _prmAbsenceTypeCode, string _prmAbsenceTypeName)
        {
            this.EmpTypeAbsTypeCode = _prmEmpTypeAbsTypeCode;
            this.EmpTypeCode = _prmEmpTypeCode;
            this.AbsenceTypeCode = _prmAbsenceTypeCode;
            this.AbsenceTypeName = _prmAbsenceTypeName;
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
