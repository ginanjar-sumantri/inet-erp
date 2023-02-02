using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class V_FINPettyReceive
    {
        public V_FINPettyReceive(string _prmPayCode, string _prmPayName)
        {
            this.PayCode = _prmPayCode;
            this.Payname = _prmPayName;
        }

        ~V_FINPettyReceive()
        {
        }
    }
}