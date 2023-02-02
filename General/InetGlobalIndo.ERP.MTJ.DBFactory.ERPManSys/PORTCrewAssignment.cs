using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORT_CrewAssignment
    {
        private string _empName = "";
        private string _shipName = "";
        private decimal? _loadAmount = 0;
        private int _jumlahABK = 0;

        public PORT_CrewAssignment(Guid _prmCrewAssignmentCode, Guid _prmHdShipArrivalCode, string _prmShipName, string _prmEmpNumb, string _prmEmpName, byte _prmJobLevel, byte _prmOnboardTimeHH, byte _prmOnboardTimeMM, DateTime _prmOnboardDate, string _prmTransNmbr, byte _prmStatus)
        {
            this.CrewAssignmentCode = _prmCrewAssignmentCode;
            this.HdShipArrivalCode = _prmHdShipArrivalCode;
            this.ShipName = _prmShipName;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.JobLevel = _prmJobLevel;
            this.OnboardTimeHH = _prmOnboardTimeHH;
            this.OnboardTimeMM = _prmOnboardTimeMM;
            this.OnboardDate = _prmOnboardDate;
            this.TransNmbr = _prmTransNmbr;
            this.Status = _prmStatus;
        }

        public PORT_CrewAssignment(Guid _prmCrewAssignmentCode, string _prmShipName, decimal? _prmLoadAmount, int _prmJumlahABK)
        {
            this.CrewAssignmentCode = _prmCrewAssignmentCode;
            this.ShipName = _prmShipName;
            this.LoadAmount = _prmLoadAmount;
            this.JumlahABK = _prmJumlahABK;
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

        public decimal? LoadAmount
        {
            get
            {
                return this._loadAmount;
            }
            set
            {
                this._loadAmount = value;
            }
        }

        public int JumlahABK
        {
            get
            {
                return this._jumlahABK;
            }
            set
            {
                this._jumlahABK = value;
            }
        }
    }
}
