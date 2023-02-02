using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTSWeb.Database
{
    public partial class MsReligion
    {
        public MsReligion(String _prmReligionCode, String _prmReligionName)
        {
            this.ReligionCode = _prmReligionCode;
            this.ReligionName = _prmReligionName;

        }
    }
}