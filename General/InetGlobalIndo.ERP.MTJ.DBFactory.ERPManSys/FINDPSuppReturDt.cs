using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINDPSuppReturDt
    {
        public FINDPSuppReturDt(string _prmTransNmbr, string _prmDPNo, string _prmCurrCode, decimal _prmForexRate, decimal _prmBaseForex, decimal _prmPPN, decimal _prmPPNForex, decimal _prmTotalForex, string _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.DPNo = _prmDPNo;
            this.CurrCode = _prmCurrCode;
            this.ForexRate = _prmForexRate;
            this.BaseForex = _prmBaseForex;
            this.PPN = _prmPPN;
            this.PPNForex = _prmPPNForex;
            this.TotalForex = _prmTotalForex;
            this.Remark = _prmRemark;
        }
    }
}
