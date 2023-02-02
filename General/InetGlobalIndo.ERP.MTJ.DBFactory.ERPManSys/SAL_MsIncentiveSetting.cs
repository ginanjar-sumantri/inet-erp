using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class SAL_MsIncentiveSetting
    {
        public SAL_MsIncentiveSetting(Guid _prmIncentiveSetCode, byte _prmPercantage, decimal _prmTeamTarget, decimal _prmIndivMin, decimal _prmIndivMax, string _prmCurr, decimal _prmAmountForex, decimal _prmAmountHome, decimal _prmForexRate)
        {
            this.IncentiveSetCode = _prmIncentiveSetCode;
            this.IncentivePercentage = _prmPercantage;
            this.TeamTarget = _prmTeamTarget;
            this.IndivMinSales = _prmIndivMin;
            this.IndivMaxSales = _prmIndivMax;
            this.CurrCode = _prmCurr;
            this.AmountForex = _prmAmountForex;
            this.AmountHome = _prmAmountHome;
            this.ForexRate = _prmForexRate;
        }

    }
}
