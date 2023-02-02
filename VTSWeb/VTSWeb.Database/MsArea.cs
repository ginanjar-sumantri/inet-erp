using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTSWeb.Database
{
    public partial class MsArea
    {
        public MsArea(String _prmAreaCode, String _prmAreaName, String _prmRemark)
        {
            this.AreaCode = _prmAreaCode;
            this.AreaName = _prmAreaName;
            this.Remark = _prmRemark;
        }
        public MsArea(String _prmAreaCode, String _prmAreaName)
        {
            this.AreaCode = _prmAreaCode;
            this.AreaName = _prmAreaName;
        }
    }
}
