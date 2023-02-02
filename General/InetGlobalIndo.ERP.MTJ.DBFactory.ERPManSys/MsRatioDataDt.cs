using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsRatioDataDt
    {
        public MsRatioDataDt(string _prmGroupCode, string _prmAccount)
        {
            this.GroupCode = _prmGroupCode;
            this.Account = _prmAccount;
        }
    }
}
