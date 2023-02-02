using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTSWeb.Database
{
    public partial class MsUserGroupDt
    {
        private String _empName = "";

        public MsUserGroupDt(String _prmUserGroupCode, String _prmEmpNumb, String _prmEmpName)
        {
            this.UserGroupCode = _prmUserGroupCode;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
        }

        public String EmpName
        {
            get
            {
                return this._empName;
            }
            set
            {
                this._empName = value;
            }
        }
    }
}
