using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class GLBudget
    {
        string _orgUnitName = "";

        public GLBudget(Guid _prmBudgetCode, DateTime _prmStartDate, DateTime _prmEndDate, string _prmOrgUnit, string _prmOrgUnitName, byte _prmStatus, string _prmRemark)
        {
            this.BudgetCode = _prmBudgetCode;
            this.StartDate = _prmStartDate;
            this.EndDate = _prmEndDate;
            this.OrgUnit = _prmOrgUnit;
            this.OrgUnitName = _prmOrgUnitName;
            this.Status = _prmStatus;
            this.Remark = _prmRemark;
        }

        public string OrgUnitName
        {
            get
            {
                return this._orgUnitName;
            }
            set
            {
                this._orgUnitName = value;
            }
        }
    }
}
