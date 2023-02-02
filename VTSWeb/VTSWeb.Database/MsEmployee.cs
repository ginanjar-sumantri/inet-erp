using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTSWeb.Database
{
    public partial class MsEmployee
    {
        public MsEmployee(String _prmEmpNumb, String _prmEmpName)
        {
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
        }
        public MsEmployee(String _prmEmpNumb, String _prmEmpName, String _prmJobTitle, String _prmJobLevel, Boolean _prmActive)
        {
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.JobTitle = _prmJobTitle;
            this.JobLevel = _prmJobLevel;
            this.Active = _prmActive;
        }
    }
}
