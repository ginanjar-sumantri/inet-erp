using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Port_PPHTDt
    {
        private string _shipName = "";
        private decimal? _loadAmount = 0;
        private int _jumlahABK = 0;

        public Port_PPHTDt(Guid _prmPPHTCode, Guid _prmCrewAssignmentCode, string _prmShipName, decimal? _prmLoadAmount, int _prmJumlahABK, decimal _prmAmount)
        {
            this.PPHTCode = _prmPPHTCode;
            this.CrewAssignmentCode = _prmCrewAssignmentCode;
            this.ShipName = _prmShipName;
            this.LoadAmount = _prmLoadAmount;
            this.JumlahABK = _prmJumlahABK;
            this.Amount = _prmAmount;
        }

        public Port_PPHTDt(Guid _prmPPHTCode, Guid _prmCrewAssignmentCode, decimal _prmAmount, string _prmUser, DateTime _prmDateTime)
        {
            this.PPHTCode = _prmPPHTCode;
            this.CrewAssignmentCode = _prmCrewAssignmentCode;
            this.Amount = _prmAmount;
            this.InsertBy = _prmUser;
            this.InsertDate = _prmDateTime;
            this.EditBy = _prmUser;
            this.EditDate = _prmDateTime;
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
