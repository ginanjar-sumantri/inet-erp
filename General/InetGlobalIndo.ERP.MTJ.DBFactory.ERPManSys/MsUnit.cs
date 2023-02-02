using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsUnit
    {
        public MsUnit(string _prmBankCode, string _prmBankName)
        {
            this.UnitCode = _prmBankCode;
            this.UnitName = _prmBankName;
        }
    }
}
