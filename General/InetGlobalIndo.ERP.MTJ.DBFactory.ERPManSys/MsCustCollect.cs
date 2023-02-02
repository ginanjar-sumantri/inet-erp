using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsCustCollect
    {
        public MsCustCollect(string _prmCustCode, string _prmCustCollect)
        {
            this.CustCode = _prmCustCode;
            this.CustCollect = _prmCustCollect;
        }
    }
}
