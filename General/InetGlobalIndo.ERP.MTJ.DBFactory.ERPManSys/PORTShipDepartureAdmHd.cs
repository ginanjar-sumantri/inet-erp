using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTShipDepartureAdmHd
    {
        private Guid _shipCode;

        public PORTShipDepartureAdmHd(Guid _prmShipArrivalCode, Guid _prmShipCode, DateTime _prmDepartureDate, string _prmSIBCode)
        {
            this.HdShipArrivalCode = _prmShipArrivalCode;
            this.ShipCode = _prmShipCode;
            this.ShipDepartureDate = _prmDepartureDate;
            this.SIBCode = _prmSIBCode;

        }

        public Guid ShipCode
        {
            get
            {
                return this._shipCode;
            }
            set
            {
                this._shipCode = value;
            }
        }
    }
}
