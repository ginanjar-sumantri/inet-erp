using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsSuppType
    {
        public MsSuppType(string _prmSuppTypeCode, string _prmSuppTypeName)
        {
            this.SuppTypeCode = _prmSuppTypeCode;
            this.SuppTypeName = _prmSuppTypeName;
        }
    }
}
