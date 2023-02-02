using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class GLAdjustDiffRateDt2
    {
        public GLAdjustDiffRateDt2(String _prmTransNmbr, String _prmAccount, String _prmCurrCode,
            Char _prmFgSubLed, String _prmSubled, Decimal _prmTotalForex, Decimal _prmTotalNewHome, 
            Decimal _prmTotalOldHome, Decimal _prmTotalAdjustHome)
        {
            this.TransNmbr = _prmTransNmbr;
            this.Account = _prmAccount;
            this.CurrCode = _prmCurrCode;
            this.FgSubLed = _prmFgSubLed;
            this.SubLed = _prmSubled;
            this.TotalForex = _prmTotalForex;
            this.TotalNewHome = _prmTotalNewHome;
            this.TotalOldHome = _prmTotalOldHome;
            this.TotalAdjustHome = _prmTotalAdjustHome;
        }
    }
}

