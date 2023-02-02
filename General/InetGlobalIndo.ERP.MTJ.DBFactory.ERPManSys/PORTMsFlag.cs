using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTMsFlag
    {
        public PORTMsFlag(Guid _prmFlagCode, string _prmFlagID, string _prmFlagName)
        {
            this.FlagCode = _prmFlagCode;
            this.FlagID = _prmFlagID;
            this.FlagName = _prmFlagName;
        }

        public PORTMsFlag(Guid _prmFlagCode, string _prmFlagName)
        {
            this.FlagCode = _prmFlagCode;          
            this.FlagName = _prmFlagName;
        }
    }
}
