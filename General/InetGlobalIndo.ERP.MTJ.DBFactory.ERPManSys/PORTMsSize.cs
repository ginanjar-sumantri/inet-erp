using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTMsSize
    {
        public PORTMsSize(Guid _prmSizeCode, string _prmSizeType, string _prmDescription)
        {
            this.SizeCode = _prmSizeCode;
            this.SizeType = _prmSizeType;
            this.Description = _prmDescription;
        }
    }
}
