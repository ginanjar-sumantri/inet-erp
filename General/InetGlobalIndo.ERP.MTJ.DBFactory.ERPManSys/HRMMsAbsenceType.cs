using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMMsAbsenceType
    {
        public HRMMsAbsenceType(String _prmAbsenceTypeCode, string _prmAbsenceTypeName, String _prmAbsenceTypeAlias, string _prmDescription,
            bool _prmIsActingRequired, bool _prmIsRelatedToFemaleGender, bool? _prmIsCutLeave)
        {
            this.AbsenceTypeCode = _prmAbsenceTypeCode;
            this.AbsenceTypeName = _prmAbsenceTypeName;
            this.AbsenceTypeAlias = _prmAbsenceTypeAlias;
            this.Description = _prmDescription;
            this.IsActingRequired = _prmIsActingRequired;
            this.IsRelatedToFemaleGender = _prmIsRelatedToFemaleGender;
            this.IsCutLeave = _prmIsCutLeave;
        }

        public HRMMsAbsenceType(String _prmAbsenceTypeCode, string _prmAbsenceTypeName)
        {
            this.AbsenceTypeCode = _prmAbsenceTypeCode;
            this.AbsenceTypeName = _prmAbsenceTypeName;
        }
    }
}
