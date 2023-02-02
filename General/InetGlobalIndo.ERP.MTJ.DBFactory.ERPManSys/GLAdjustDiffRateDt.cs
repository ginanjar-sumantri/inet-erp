using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class GLAdjustDiffRateDt
    {
        public GLAdjustDiffRateDt(String _prmTransNmbr, String _prmCurrCode, Decimal _prmForexRate)
        {
            this.TransNmbr = _prmTransNmbr;
            this.CurrCode = _prmCurrCode;
            this.ForexRate = _prmForexRate;
        }
    }
}
