using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTPlanShipArrival
    {
        private string _shipName = "";
        private string _flagName = "";
        private decimal _shipGRT = 0;
        private decimal _shipLength = 0;
        private decimal _shipWidth = 0;
        private decimal _shipDepth = 0;
        private decimal _amountUnload = 0;
        private string _amountUnloadUnit = "";
        private decimal _expectAmountUnload = 0;
        private string _expectAmountUnloadUnit = "";
        private DateTime _dateUnload = new DateTime();
        //private decimal _shipGRT = 0;

        public PORTPlanShipArrival(
                Guid _prmHdShipCode, string _prmDocNo, string _prmShipName, string _prmFlagName, decimal _prmShipGRT, Decimal _prmShipLength,
                Decimal _prmShipWidth, Decimal _prmShipDepth, Decimal? _prmAmountUnload, String _prmAmountUnloadUnit,
                DateTime _prmPlantTimeArrival,String _prmTime, String _prmNowPosition, Decimal? _prmExpectedAmountUnload,
                String _prmExpectedAmountUnloadUnit, DateTime _prmExpectedAmountUnloadDate,
                String _prmPlanFrom, DateTime? _prmPlanFromDate, String _prmPlanTo, DateTime? _prmPlanToDate,
                String _prmDocDescription
            )
        {
            this.PlanShipArrivalCode = _prmHdShipCode;
            this.DocNo = _prmDocNo;
            this.ShipName = _prmShipName;
            this.FlagName = _prmFlagName;
            this.ShipGRT = _prmShipGRT;
            this.ShipLength = _prmShipLength;
            this.ShipWidth = _prmShipWidth;
            this.ShipDepth = _prmShipDepth;
            this.Time = _prmTime;
            this.AmountUnload = (_prmAmountUnload == null) ? 0 : (decimal)_prmAmountUnload;
            this.AmountUnloadUnitCode = _prmAmountUnloadUnit;
            this.PlanTimeArrival = _prmPlantTimeArrival;
            this.NowPosition = _prmNowPosition;
            this.ExpectAmountUnload = (_prmExpectedAmountUnload == null) ? 0 : (decimal)_prmExpectedAmountUnload;
            this.ExpectAmountUnloadUnitCode = _prmExpectedAmountUnloadUnit;
            this.ExpectDateUnload = _prmExpectedAmountUnloadDate;
            this.PlanFrom = _prmPlanFrom;
            this.PlanFromDate = _prmPlanFromDate;
            this.PlanTo = _prmPlanTo;
            this.PlanToDate = _prmPlanToDate;
            this.DocDescription = _prmDocDescription;
        }

        public PORTPlanShipArrival(
                Guid _prmHdShipCode, Guid _prmShipCode, string _prmFlagName, decimal _prmShipGRT, Decimal _prmShipLength,
                Decimal _prmShipWidth, Decimal _prmShipDepth, 
                DateTime _prmPlantTimeArrival, String _prmTime, String _prmNowPosition,              
                String _prmPlanFrom, DateTime? _prmPlanFromDate, String _prmPlanTo, DateTime? _prmPlanToDate,
                String _prmDocDescription
            )
        {
            this.PlanShipArrivalCode = _prmHdShipCode;
            this.ShipCode = _prmShipCode;
            this.FlagName = _prmFlagName;
            this.ShipGRT = _prmShipGRT;
            this.ShipLength = _prmShipLength;
            this.ShipWidth = _prmShipWidth;
            this.ShipDepth = _prmShipDepth;
            this.Time = _prmTime;
           
            this.PlanTimeArrival = _prmPlantTimeArrival;
            this.NowPosition = _prmNowPosition;
           
            this.PlanFrom = _prmPlanFrom;
            this.PlanFromDate = _prmPlanFromDate;
            this.PlanTo = _prmPlanTo;
            this.PlanToDate = _prmPlanToDate;
            this.DocDescription = _prmDocDescription;
        }

        public PORTPlanShipArrival(Guid _prmPlanShipArrivalCode, string _prmDocNo)
        {
            this.PlanShipArrivalCode = _prmPlanShipArrivalCode;
            this.DocNo = _prmDocNo;
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

        public decimal ShipGRT
        {
            get
            {
                return this._shipGRT;
            }
            set
            {
                this._shipGRT = value;
            }
        }

        public decimal ShipLength
        {
            get
            {
                return this._shipLength;
            }
            set
            {
                this._shipLength = value;
            }
        }

        public decimal ShipWidth
        {
            get
            {
                return this._shipWidth;
            }
            set
            {
                this._shipWidth = value;
            }
        }

        public decimal ShipDepth
        {
            get
            {
                return this._shipDepth;
            }
            set
            {
                this._shipDepth = value;
            }
        }

        public decimal AmountUnload
        {
            get
            {
                return this._amountUnload;
            }
            set
            {
                this._amountUnload = value;
            }
        }

        public decimal ExpectAmountUnload
        {
            get
            {
                return this._expectAmountUnload;
            }
            set
            {
                this._expectAmountUnload = value;
            }
        }

        public string AmountUnloadUnitCode
        {
            get
            {
                return this._amountUnloadUnit;
            }
            set
            {
                this._amountUnloadUnit = value;
            }
        }

        public string ExpectAmountUnloadUnitCode
        {
            get
            {
                return this._expectAmountUnloadUnit;
            }
            set
            {
                this._expectAmountUnloadUnit = value;
            }
        }

        public DateTime ExpectDateUnload
        {
            get
            {
                return this._dateUnload;
            }
            set
            {
                this._dateUnload = value;
            }
        }
    }
}
