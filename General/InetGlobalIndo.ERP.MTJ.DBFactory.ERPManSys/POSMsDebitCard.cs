using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class POSMsDebitCard
    {
        String _accountName = "";

        public POSMsDebitCard(String _prmDebitCardCode, String _prmDebitCardName, String _prmAccount, String _prmAccountName)
        {
            this.DebitCardCode = _prmDebitCardCode;
            this.DebitCardName = _prmDebitCardName;
            this.Account = _prmAccount;
            this.AccountName = _prmAccountName;
        }

        public POSMsDebitCard(String _prmDebitCardCode, String _prmDebitCardName)
        {
            this.DebitCardCode = _prmDebitCardCode;
            this.DebitCardName = _prmDebitCardName;
        }

        public String AccountName
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
