using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTSWeb.Database
{
    public partial class MsRackServer
    {
        public MsRackServer(String _prmRackCode, String _prmRackName, String _prmRemark)
        {
            this.RackCode = _prmRackCode;
            this.RackName = _prmRackName;
            this.Remark = _prmRemark;
        }
        public MsRackServer(String _prmRackCode, String _prmRackName)
        {
            this.RackCode = _prmRackCode;
            this.RackName = _prmRackName;
        }
    }
}
