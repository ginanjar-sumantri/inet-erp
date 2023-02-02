using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsTransType_MsAccount : Base
    {
        private string _accountName = "";
        private string _transTypeName = "";

        public MsTransType_MsAccount(string _prmTransTypeCode, string _prmTransTypeName, string _prmAccount, string _prmAccountName)
        {
            this.TransTypeCode = _prmTransTypeCode;
            this.TransTypeName = _prmTransTypeName;
            this.Account = _prmAccount;
            this.AccountName = _prmAccountName;
        }

        public MsTransType_MsAccount(string _prmTransTypeCode, string _prmAccount)
        {
            this.TransTypeCode = _prmTransTypeCode;
            this.Account = _prmAccount;
        }

        public string TransTypeName
        {
            get
            {
                return this._transTypeName;
            }
            set
            {
                this._transTypeName = value;
            }
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
