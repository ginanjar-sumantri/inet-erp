using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYMsRatePPhPesangon
    {
        public PAYMsRatePPhPesangon(int _prmPPhCode, decimal _prmStartValue, decimal _prmEndValue, decimal _prmRateNPWP, decimal? _prmRateNoNPWP)
        {
            this.PPhCode = _prmPPhCode;
            this.StartValue = _prmStartValue;
            this.EndValue = _prmEndValue;
            this.RateNPWP = _prmRateNPWP;
            this.RateNoNPWP = _prmRateNoNPWP;
        }
    }
}
