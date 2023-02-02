using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{


    public partial class PORTMsShip
    {
        private string _shipTypeName = "";
        private string _ShipFlagName = "";

        public PORTMsShip(Guid ShipCode, String ShipName)
        {
            this.ShipCode = ShipCode;
            this.ShipName = ShipName;
        }

        public PORTMsShip(Guid ShipCode, String ShipTypeName, String ShipRegNo, String ShipName, Decimal ShipGRT, Decimal ShipLength,
                                               Decimal ShipWidth, Decimal ShipDepth, String ShipFront, String ShipBack, String FlagName)
        {
            this.ShipCode = ShipCode;
            this.ShipRegNo = ShipRegNo;
            this.ShipTypeName = ShipTypeName;
            this.ShipName = ShipName;
            this.ShipGRT = ShipGRT;
            this.ShipSizeLength = ShipLength;
            this.ShipSizeWidth = ShipWidth;
            this.ShipSizeDepth = ShipDepth;
            this.ShipFront = ShipFront;
            this.ShipBack = ShipBack;
            this.FlagName = FlagName;
        }

        public PORTMsShip(Guid ShipCode, String ShipName, Decimal ShipGRT, Decimal ShipLength,
                           Decimal ShipWidth, Decimal ShipDepth, String FlagName)
        {
            this.ShipCode = ShipCode;
            this.ShipName = ShipName;
            this.ShipGRT = ShipGRT;
            this.ShipSizeLength = ShipLength;
            this.ShipSizeWidth = ShipWidth;
            this.ShipSizeDepth = ShipDepth;
            this.FlagName = FlagName;
        }

        public string ShipTypeName
        {
            get
            {
                return this._shipTypeName;
            }
            set
            {
                this._shipTypeName = value;
            }
        }

        public string FlagName
        {
            get
            {
                return this._ShipFlagName;
            }
            set
            {
                this._ShipFlagName = value;
            }
        }
    }


}
