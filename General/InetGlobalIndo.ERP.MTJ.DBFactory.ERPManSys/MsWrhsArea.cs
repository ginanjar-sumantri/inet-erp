using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsWrhsArea
    {
        public MsWrhsArea(string _prmWrhsAreaCode, string _prmWrhsAreaName, string _prmAddress1, string _prmAddress2)
        {
            this.WrhsAreaCode = _prmWrhsAreaCode;
            this.WrhsAreaName = _prmWrhsAreaName;
            this.Address1 = _prmAddress1;
            this.Address2 = _prmAddress2;
        }

        public MsWrhsArea(string _prmWrhsAreaCode, string _prmWrhsAreaName)
        {
            this.WrhsAreaCode = _prmWrhsAreaCode;
            this.WrhsAreaName = _prmWrhsAreaName;
        }
    }
}
