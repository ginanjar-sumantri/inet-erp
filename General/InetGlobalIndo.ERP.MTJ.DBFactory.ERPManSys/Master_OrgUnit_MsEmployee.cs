using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_OrgUnit_MsEmployee
    {
        private string _orgUnitDesc = "";
        private string _empName = "";

        public Master_OrgUnit_MsEmployee(string _prmOrgUnit, string _prmOrgUnitDesc, string _prmEmpNumb, string _prmEmpName, Boolean _prmIsPIC)
        {
            this.OrgUnit = _prmOrgUnit;
            this.Description = _prmOrgUnitDesc;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.IsPIC = _prmIsPIC;
        }

        public string EmpName
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

        public string Description
        {
            get
            {
                return this._orgUnitDesc;
            }
            set
            {
                this._orgUnitDesc = value;
            }
        }
    }
}
