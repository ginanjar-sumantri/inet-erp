using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTMsAgent
    {
        public PORTMsAgent(Guid _prmCode, string _prmName, string _prmID)
        {
            this.AgentCode = _prmCode;
            this.AgentName = _prmName;
            this.AgentID = _prmID;
        }

        public PORTMsAgent(Guid _prmCode, string _prmName)
        {
            this.AgentCode = _prmCode;
            this.AgentName = _prmName;                  
        }
    }
}
