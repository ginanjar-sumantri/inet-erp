using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_DefaultAcc
    {
        private string _accountName = "";
        private string _currCode = "";

        public Master_DefaultAcc(string _prmSetCode, string _prmAccount)
        {
            this.SetCode = _prmSetCode;
            this.Account = _prmAccount;
        }

        public Master_DefaultAcc(string _prmSetCode, string _prmAccount, string _currCode)
        {
            this.SetCode = _prmSetCode;
            this.Account = _prmAccount;
            this.CurrCode = _currCode;
        }

        public Master_DefaultAcc(string _prmSetCode, string _prmAccount, string _prmAccName, string _currCode)
        {
            this.SetCode = _prmSetCode;
            this.Account = _prmAccount;
            this.AccountName = _prmAccName;
            this.CurrCode = _currCode;
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

        public string CurrCode
        {
            get
            {
                return this._currCode;
            }
            set
            {
                this._currCode = value;
            }
        }
    }
}
