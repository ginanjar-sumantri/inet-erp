using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrClaimPlafonDt
    {
        private string _claimName = "";

        public HRMTrClaimPlafonDt(String _prmTransNmbr, String _prmPlafonType, Decimal? _prmAmount, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.PlafonType = _prmPlafonType;
            this.Amount = _prmAmount;
            this.Remark = _prmRemark;
        }
    }
}
