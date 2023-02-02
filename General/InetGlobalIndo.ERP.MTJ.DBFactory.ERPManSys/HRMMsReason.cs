using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMMsReason
    {
        public HRMMsReason(String _prmReasonCode, string _prmReasonName, string _prmDescription)
        {
            this.ReasonCode = _prmReasonCode;
            this.ReasonName = _prmReasonName;
            this.Description = _prmDescription;
        }

        public HRMMsReason(String _prmReasonCode, string _prmReasonName)
        {
            this.ReasonCode = _prmReasonCode;
            this.ReasonName = _prmReasonName;
        }
    }
}
