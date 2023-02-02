using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class V_GroupBy
    {
        public V_GroupBy(string _prmGroupCode, string _prmGroupName)
        {
            this.GroupCode = _prmGroupCode;
            this.GroupName = _prmGroupName;
        }

        ~V_GroupBy()
        {
        }
    }
}