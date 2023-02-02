using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsAccGroupDt : Base
    {
        private string _accountName = "";
        //private string _transTypeName = "";

        public MsAccGroupDt(string _prmGroupType, string _prmAccount)
        {
            this.GroupType = _prmGroupType;
            this.Account = _prmAccount;

        }

        public MsAccGroupDt(string _prmGroupType, string _prmAccount, string _prmAccountName)
        {
            this.GroupType = _prmGroupType;
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
