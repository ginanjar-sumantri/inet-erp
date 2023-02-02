using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYMsRatePPh
    {
        public PAYMsRatePPh(int _prmPPhCode, Decimal _prmStartValue, Decimal _prmEndValue, Decimal _prmRateNPWP, Decimal? _prmRateNoNPWP)
        {
            this.PPhCode = _prmPPhCode;
            this.StartValue = _prmStartValue;
            this.EndValue = _prmEndValue;
            this.RateNPWP = _prmRateNPWP;
            this.RateNoNPWP = _prmRateNoNPWP;
        }
    }
}
