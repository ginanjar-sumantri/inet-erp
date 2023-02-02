using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINPettyReceiveDt
    {
        private string _subledName = "";
        private string _accountName = "";

        public FINPettyReceiveDt(string _prmTransNumber, int _prmItemNo, string _prmAccount, string _prmAccountName, char _prmFgSubled,
                            string _prmSubled, string _prmSubledName, string _prmRemark, decimal _prmAmountForex)
        {
            this.TransNmbr = _prmTransNumber;
            this.ItemNo = _prmItemNo;
            this.Account = _prmAccount;
            this.AccountName = _prmAccountName;
            this.FgSubLed = _prmFgSubled;
            this.SubLed = _prmSubled;
            this.SubledName = _prmSubledName;
            this.Remark = _prmRemark;
            this.AmountForex = _prmAmountForex;
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
