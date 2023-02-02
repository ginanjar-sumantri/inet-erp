using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINMsCashFlowGroupDt
    {
        string _accountName = "";

        public FINMsCashFlowGroupDt(string _prmCashFlowGroupCode, string _prmAccount, String _prmAccountName)
        {
            this.CashFlowGroupCode = _prmCashFlowGroupCode;
            this.Account = _prmAccount;
            this.AccountName = _prmAccountName;
        }

        public string AccountName
        {
            get
            {
                return this._accountName;
            }
            set
            {
                this._accountName = value;
            }
        }
    }
}
