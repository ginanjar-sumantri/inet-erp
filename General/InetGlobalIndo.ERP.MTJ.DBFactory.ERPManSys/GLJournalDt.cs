using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
   [Serializable]
    public partial class GLJournalDt : Base
    {
        private string _accountName = "";
        private string _subledName = "";

        public GLJournalDt(string _prmTransClass, int _prmItemNo, string _prmAccount, string _prmAccountName, string _prmSubLedger, string _prmSubLedgerName, string _prmCurrCode, decimal _prmForexRate, decimal _prmDebitForex, decimal _prmCreditForex, decimal _prmDebitHome, decimal _prmCreditHome)
        {
            this.TransClass = _prmTransClass;
            this.ItemNo = _prmItemNo;
            this.Account = _prmAccount;
            this.AccountName = _prmAccountName;
            this.SubLed = _prmSubLedger;
            this.SubLed_Name = _prmSubLedgerName;
            this.CurrCode = _prmCurrCode;
            this.ForexRate = _prmForexRate;
            this.DebitForex = _prmDebitForex;
            this.CreditForex = _prmCreditForex;
            this.DebitHome = _prmDebitHome;
            this.CreditHome = _prmCreditHome;
        }

        public GLJournalDt(int _prmItemNo)
        {
            this.ItemNo = _prmItemNo;
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

        public string SubLed_Name
        {
            get
            {
                return this._subledName;
            }
            set
            {
                this._subledName = value;
            }
        }
    }
}
