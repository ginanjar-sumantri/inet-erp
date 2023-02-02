using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTPlanShipLoading
    {

        public PORTPlanShipLoading(Guid ShipArrivalCode, decimal AmountLoad, string AmountLoadUnit,
                        decimal ExpectAmount, String ExpextAmountUnit, DateTime ExpectDate)
        {
            this.PlanShipArrivalCode                = ShipArrivalCode;
            this.AmountUnload                       = AmountLoad;
            this.AmountUnloadUnitCode               = AmountLoadUnit;
            this.ExpectAmountUnload                 = ExpectAmount;
            this.ExpectAmountUnloadUnitCode         = ExpextAmountUnit;
            this.ExpectDateUnload                   = ExpectDate;
        }        
    }
}
