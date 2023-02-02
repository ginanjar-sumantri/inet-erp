using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsTermDt
    {
        public MsTermDt(string _prmTermCode, int _prmPeriod, string _prmTypeRange, int _prmXRange, decimal _prmPercentBase, decimal _prmPercentPPn)
        {
            this.TermCode = _prmTermCode;
            this.Period = _prmPeriod;
            this.TypeRange = _prmTypeRange;
            this.XRange = _prmXRange;
            this.PercentBase = _prmPercentBase;
            this.PercentPPn = _prmPercentPPn;

        }
    }
}
