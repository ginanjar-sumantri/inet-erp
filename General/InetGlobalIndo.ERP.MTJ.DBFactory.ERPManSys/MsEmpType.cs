using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsEmpType
    {
        public MsEmpType(string _prmEmpTypeCode, string _prmEmpTypeName, char _prmHaveLeave, Boolean? _prmFgPermanent)
        {
            this.EmpTypeCode = _prmEmpTypeCode;
            this.EmpTypeName = _prmEmpTypeName;
            this.HaveLeave = _prmHaveLeave;
            this.FgPermanent = _prmFgPermanent;
        }

        public MsEmpType(string _prmEmpTypeCode, string _prmEmpTypeName)
        {
            this.EmpTypeCode = _prmEmpTypeCode;
            this.EmpTypeName = _prmEmpTypeName;
        }
    }
}
