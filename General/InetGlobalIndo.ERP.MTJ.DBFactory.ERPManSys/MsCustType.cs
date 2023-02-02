using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsCustType
    {
        public MsCustType(string _prmCustTypeCode, string _prmCustTypeName)
        {
            this.CustTypeCode = _prmCustTypeCode;
            this.CustTypeName = _prmCustTypeName;
        }
    }
}
