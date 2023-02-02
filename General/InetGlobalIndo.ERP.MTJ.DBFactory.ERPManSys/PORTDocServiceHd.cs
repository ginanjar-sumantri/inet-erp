using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTDocServiceHd
    {
        private string _shipName = "";
        private string _empName = "";

        public PORTDocServiceHd(Guid _prmHdDocServiceCode, string _prmFormID, Guid _prmShipCode, string _prmShipName, string _prmEmpCode, string _prmEmpName, DateTime _prmDateIn, DateTime _prmDateOut, byte _prmStatus)
        {
            this.HdDocServiceCode = _prmHdDocServiceCode;
            this.FormID = _prmFormID;
            this.ShipCode = _prmShipCode;
            this.ShipName = _prmShipName;
            this.EmpNumb = _prmEmpCode;
            this.EmpName = _prmEmpName;
            this.DateIn = _prmDateIn;
            this.DateOut = _prmDateOut;
            this.Status = _prmStatus;
        }

        public string ShipName
        {
            get
            {
                return this._shipName;
            }
            set
            {
                this._shipName = value;
            }
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
