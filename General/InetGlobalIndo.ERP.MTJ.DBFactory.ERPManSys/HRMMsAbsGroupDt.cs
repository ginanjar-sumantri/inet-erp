using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMMsAbsGroupDt
    {
        private string _absenceTypeName = "";

        public HRMMsAbsGroupDt(string _prmAbsenceGroup, string _prmAbsenceTypeCode, string _prmAbsenceTypeName)
        {
            this.AbsenceGroup = _prmAbsenceGroup;
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
