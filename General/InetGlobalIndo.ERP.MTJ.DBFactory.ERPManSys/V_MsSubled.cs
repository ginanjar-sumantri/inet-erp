using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class V_MsSubled
    {
        public V_MsSubled(string _prmSubLedNo, string _prmSubLedName)
        {
            this.SubLed_No = _prmSubLedNo;
            this.SubLed_Name = _prmSubLedName;
        }

        ~V_MsSubled()
        {
        }
    }
}