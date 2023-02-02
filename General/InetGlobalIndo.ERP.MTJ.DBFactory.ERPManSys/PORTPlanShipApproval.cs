using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTPlanShipApproval
    {
        private string _empName = "";

        public PORTPlanShipApproval(Guid _prmPlanShipArrivalCode, string _prmEmpNumb, string _prmEmpName, DateTime _prmInsertDate, byte _prmStatus)
        {
            this.PlanShipArrivalCode = _prmPlanShipArrivalCode;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.InsertDate = _prmInsertDate;
            this.Status = _prmStatus;
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
    }
}
