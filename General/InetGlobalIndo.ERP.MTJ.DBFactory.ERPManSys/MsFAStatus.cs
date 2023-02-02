using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsFAStatus
    {
        public MsFAStatus(string _statusCode, string _statusName)
        {
            this.FAStatusCode = _statusCode;
            this.FAStatusName = _statusName;
        }
    }
}
