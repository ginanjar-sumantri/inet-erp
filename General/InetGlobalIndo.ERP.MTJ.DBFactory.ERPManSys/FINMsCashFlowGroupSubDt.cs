using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINMsCashFlowGroupSubDt
    {
        private string _accountName = "";

        public FINMsCashFlowGroupSubDt(string _prmCFGroupSubCode, string _prmCAccount, string _prmAccountName)
        {
            this.CashFlowGroupSubCode = _prmCFGroupSubCode;
            this.Account = _prmCAccount;
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
