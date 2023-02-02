using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Finance_BankReconAccount : Base
    {
        private string _accountName = "";
        private string _subledName = "";

        public Finance_BankReconAccount(Guid _prmBankReconAccountCode, Guid _prmBankReconCode,
            string _prmAccount, string _prmAccountName, char? _prmFgSubled, string _prmSubled,
            string _prmSubledName, decimal _prmForexRate, bool _prmFgValue, decimal _prmAmountForex, string _prmRemark)
        {
            this.BankReconAccountCode = _prmBankReconAccountCode;
            this.BankReconCode = _prmBankReconCode;
            this.Account = _prmAccount;
            this.AccountName = _prmAccountName;
            this.SubLed = _prmSubled;
            this.SubLedName = _prmSubledName;
            this.FgSubLed = _prmFgSubled;
            this.ForexRate = _prmForexRate;
            this.FgValue = _prmFgValue;
            this.AmountForex = _prmAmountForex;
            this.Remark = _prmRemark;
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

        public string SubLedName
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
