using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class POSMsCreditCard
    {
        String _accountName = "";

        public POSMsCreditCard(String _prmCreditCardCode, String _prmCreditCardName, String _prmAccount, String _prmAccountName)
        {
            this.CreditCardCode = _prmCreditCardCode;
            this.CreditCardName = _prmCreditCardName;
            this.CreditCardTypeCode = _prmAccountName;
            this.Account = _prmAccount;
            this.AccountName = _prmAccountName;
        }

        public POSMsCreditCard(String _prmCreditCardCode, String _prmCreditCardName)
        {
            this.CreditCardCode = _prmCreditCardCode;
            this.CreditCardName = _prmCreditCardName;
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
