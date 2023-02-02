using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsBank
    {
        public MsBank(string _prmBankCode, string _prmBankName)
        {
            this.BankCode = _prmBankCode;
            this.BankName = _prmBankName;
        }
    }
}
