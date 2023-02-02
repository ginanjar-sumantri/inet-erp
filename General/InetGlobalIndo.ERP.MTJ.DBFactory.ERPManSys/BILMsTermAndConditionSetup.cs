using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class BILMsTermAndConditionSetup
    {
        public BILMsTermAndConditionSetup(Byte _prmID, Byte _prmType, String _prmBody)
        {
            this.ID = _prmID;
            this.Type = _prmType;
            this.Body = _prmBody;
        }
    }
}
