using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINDPCustReturDt
    {
        public FINDPCustReturDt(string _prmDPNo, string _prmCurrCode, decimal _prmTotalForex, string _prmRemark)
        {
            this.DPNo = _prmDPNo;
            this.CurrCode = _prmCurrCode;
            this.TotalForex = _prmTotalForex;
            this.Remark = _prmRemark;
        }

        public FINDPCustReturDt(string _prmDPNo)
        {
            this.DPNo = _prmDPNo;
        }
    }
}
