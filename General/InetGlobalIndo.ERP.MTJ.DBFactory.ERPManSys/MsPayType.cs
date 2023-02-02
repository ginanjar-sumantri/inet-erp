using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsPayType
    {
        private string _accountName = "";
        private string _bankName = "";

        public MsPayType(string _prmPayCode, string _prmPayName, string _prmAccount, string _prmAccountName, string _prmBankName)
        {
            this.PayCode = _prmPayCode;
            this.PayName = _prmPayName;
            this.Account = _prmAccount;
            this.AccountName = _prmAccountName;
            this.BankName = _prmBankName;
        }

        public MsPayType(string _prmPayCode, string _prmPayName)
        {
            this.PayCode = _prmPayCode;
            this.PayName = _prmPayName;
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

        public string BankName
        {
            get
            {
                return this._bankName;
            }
            set
            {
                this._bankName = value;
            }
        }
    }
}
