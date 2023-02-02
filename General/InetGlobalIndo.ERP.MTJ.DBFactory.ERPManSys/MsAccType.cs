using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsAccType
    {
        public MsAccType(char _prmAccTypeCode, string _prmAccTypeName,string _prmFgType)
        {
            this.AccTypeCode = _prmAccTypeCode;
            this.AccTypeName = _prmAccTypeName;
            this.FgType = _prmFgType;
            
        }
    }
}
