using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsTransType : Base
    {
        public MsTransType(string _prmTransTypeCode, string _prmTransTypeName, char? _prmfgAccount)
        {
            this.TransTypeCode = _prmTransTypeCode;
            this.TransTypeName = _prmTransTypeName;
            this.FgAccount = _prmfgAccount;
        }
        public MsTransType(string _prmTransTypeCode, string _prmTransTypeName)
        {
            this.TransTypeCode = _prmTransTypeCode;
            this.TransTypeName = _prmTransTypeName;

        }
    }
}
