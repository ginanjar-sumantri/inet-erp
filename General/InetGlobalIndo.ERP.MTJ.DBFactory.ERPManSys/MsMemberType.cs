using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsMemberType
    {
        public MsMemberType(String _prmMemberTypeCode, String _prmMemberTypeName)
        {
            this.MemberTypeCode = _prmMemberTypeCode;
            this.MemberTypeName = _prmMemberTypeName;
        }
    }
}
