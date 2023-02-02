using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTShipApproval
    {
        private string _empName = "";

        public PORTShipApproval(Guid _prmShipArrivalCode, string _prmEmpNumb, string _prmEmpName, DateTime _prmInsertDate, byte _prmStatus)
        {
            this.HdShipArrivalCode = _prmShipArrivalCode;
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
