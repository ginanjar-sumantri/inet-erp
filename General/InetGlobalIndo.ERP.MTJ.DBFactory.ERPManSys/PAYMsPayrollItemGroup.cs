using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYMsPayrollItemGroup
    {
        public PAYMsPayrollItemGroup(string _prmPayrollGrpCode, string _prmPayrollGrpName)
        {
            this.PayrollGrpCode = _prmPayrollGrpCode;
            this.PayrollGrpName = _prmPayrollGrpName;
        }
    }
}
