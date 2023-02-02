using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_Default
    {
        private string _accountName = "";
        private string _account = "";

        public Master_Default(string _prmSetCode, string _prmSetDesc)
        {
            this.SetCode = _prmSetCode;
            this.SetDescription = _prmSetDesc;
        }

        public Master_Default(string _prmSetCode, string _prmSetDesc, string _prmAccount, string _prmAccName)
        {
            this.SetCode = _prmSetCode;
            this.SetDescription = _prmSetDesc;
            this.Account = _prmAccount;
            this.AccountName = _prmAccName;
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

        public string Account
        {
            get
            {
                return this._account;
            }
            set
            {
                this._account = value;
            }
        }
    }
}
