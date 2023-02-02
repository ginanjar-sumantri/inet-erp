using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINPayNonTradeDb
    {
        private string _accountName = "";
        private string _subledName = "";

        public FINPayNonTradeDb(int _prmItemNo, string _prmAccount, string _prmAccountName, string _prmSubLed, string _prmSubledName, decimal _prmAmountForex, string _prmRemark)
        {
            this.ItemNo = _prmItemNo;
            this.Account = _prmAccount;
            this.AccountName = _prmAccountName;
            this.SubLed = _prmSubLed;
            this.SubledName = _prmSubledName;
            this.AmountForex = _prmAmountForex;
            this.Remark = _prmRemark;
        }

        public string SubledName
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
