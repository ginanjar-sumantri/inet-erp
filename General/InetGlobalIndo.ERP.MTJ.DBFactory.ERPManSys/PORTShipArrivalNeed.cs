using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTShipArrivalNeed
    {
        private string _needName = "";

        public PORTShipArrivalNeed(Guid _prmHdShipCode, Guid _prmDtShipCode, string _prmShipNeedName)
        {
            this.HdShipArrivalCode = _prmHdShipCode;
            this.DtShipArrivalNeedCode = _prmDtShipCode;
            this.NeedName = _prmShipNeedName;
        }

        public PORTShipArrivalNeed(Guid _prmShipNeedCode, string _prmShipNeedName)
        {
            this.ShipNeedCode = _prmShipNeedCode;
            this.NeedName = _prmShipNeedName;

        }

        public string NeedName
        {
            get
            {
                return this._needName;
            }
            set
            {
                this._needName = value;
            }
        }
    }
}
