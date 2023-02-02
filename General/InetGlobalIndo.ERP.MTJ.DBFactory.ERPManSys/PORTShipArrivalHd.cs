using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTShipArrivalHd
    {

        private string _agentID = "";
        private string _agentName = "";
        private string _shipName = "";
        private string _flagName = "";

        public PORTShipArrivalHd(
                Guid _prmHdShipCode, string _prmFormArriveID, Guid _prmAgentCode, string _prmAgentID, string _prmAgentName, string _prmShipName, string _prmFlagName,
                Decimal? _prmLoadAmount, string _prmUnitCode, string _prmNowPosition, DateTime? _prmBeginDepart1, DateTime? _prmEndingDepart1)
        {
            this.HdShipArrivalCode = _prmHdShipCode;
            this.FormArriveID = _prmFormArriveID;
            this.AgentCode = _prmAgentCode;
            this.AgentID = _prmAgentID;
            this.AgentName = _prmAgentName;
            this.ShipName = _prmShipName;
            this.FlagName = _prmFlagName;
            this.LoadAmount = (_prmLoadAmount == null) ? 0 : (decimal)_prmLoadAmount;
            this.UnitCode = _prmUnitCode;
            this.NowPosition = _prmNowPosition;
            this.BeginDepart1 = _prmBeginDepart1;
            this.EndingDepart1 = _prmEndingDepart1;
        }

        public PORTShipArrivalHd(Guid _prmHdShipCode, string _prmShipName)
        {
            this.HdShipArrivalCode = _prmHdShipCode;
            this.ShipName = _prmShipName;
        }

        public PORTShipArrivalHd(string _prmFormID, string _prmShipName)
        {
            this.FormArriveID = _prmFormID;
            this.ShipName = _prmShipName;
        }

        public string AgentID
        {
            get
            {
                return this._agentID;
            }
            set
            {
                this._agentID = value;
            }
        }

        public string AgentName
        {
            get
            {
                return this._agentName;
            }
            set
            {
                this._agentName = value;
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

        public string FlagName
        {
            get
            {
                return this._flagName;
            }
            set
            {
                this._flagName = value;
            }
        }

        //public decimal ShipGRT
        //{
        //    get
        //    {
        //        return this._shipGRT;
        //    }
        //    set
        //    {
        //        this._shipGRT = value;
        //    }
        //}

        //public decimal ShipLength
        //{
        //    get
        //    {
        //        return this._shipLength;
        //    }
        //    set
        //    {
        //        this._shipLength = value;
        //    }
        //}

        //public decimal ShipWidth
        //{
        //    get
        //    {
        //        return this._shipWidth;
        //    }
        //    set
        //    {
        //        this._shipWidth = value;
        //    }
        //}

        //public decimal ShipDepth
        //{
        //    get
        //    {
        //        return this._shipDepth;
        //    }
        //    set
        //    {
        //        this._shipDepth = value;
        //    }
        //}

        //public decimal AmountLoad
        //{
        //    get
        //    {
        //        return this._amountLoad;
        //    }
        //    set
        //    {
        //        this._amountLoad = value;
        //    }
        //}

        //public string AmountLoadUnitCode
        //{
        //    get
        //    {
        //        return this._amountLoadUnit;
        //    }
        //    set
        //    {
        //        this._amountLoadUnit = value;
        //    }
        //}
    }
}
