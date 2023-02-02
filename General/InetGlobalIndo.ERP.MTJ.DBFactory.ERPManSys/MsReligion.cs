using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsReligion
    {
        public MsReligion(string _prmReligionCode, string _prmReligionName)
        {
            this.ReligionCode = _prmReligionCode;
            this.ReligionName = _prmReligionName;
        }
    }
}
