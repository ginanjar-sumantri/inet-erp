using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class POSMsReason
    {
        public POSMsReason(int _prmReasonCode, String _prmReasonName)
        {
            this.ReasonCode = _prmReasonCode;
            this.ReasonName = _prmReasonName;
        }
    }
}
