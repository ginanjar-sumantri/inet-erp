using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTSWeb.Database
{
    public partial class MsPurpose
    {
        public MsPurpose(String _prmPurposeCode, String _prmPurposeName, String _prmRemark)
        {
            this.PurposeCode = _prmPurposeCode;
            this.PurposeName = _prmPurposeName;
            this.Remark = _prmRemark;
        }
    }
    public partial class MsPurpose
    {
        public MsPurpose(String _prmPurposeCode, String _prmPurposeName)
        {
            this.PurposeCode = _prmPurposeCode;
            this.PurposeName = _prmPurposeName;
        }
    }
}
