using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsFAMaintenanceAcc
    {
        private string _accountName = "";

        public MsFAMaintenanceAcc(string _faCode, string _currCode, string _account, string _prmAccountName)
        {
            this.FAMaintenance = _faCode;
            this.CurrCode = _currCode;
            this.Account = _account;
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
