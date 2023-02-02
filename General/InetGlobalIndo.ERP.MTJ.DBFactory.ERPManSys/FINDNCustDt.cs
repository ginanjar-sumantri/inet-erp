using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINDNCustDt
    {
        private string _accountName = "";
        private string _unitName = "";
        private string _subledName = "";

        public FINDNCustDt(int _prmItemNo, string _prmAccount, string _prmAccountName, decimal _prmAmountForex, string _prmSubled, string _prmSubledName, decimal _prmPriceForex, decimal _prmQty, string _prmUnit, string _prmUnitName, string _prmRemark)
        {
            this.ItemNo = _prmItemNo;
            this.Account = _prmAccount;
            this.AccountName = _prmAccountName;
            this.AmountForex = _prmAmountForex;
            this.SubLed = _prmSubled;
            this.SubledName = _prmSubledName;
            this.PriceForex = _prmPriceForex;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.UnitName = _prmUnitName;
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

        public string UnitName
        {
            get
            {
                return this._unitName;
            }
            set
            {
                this._unitName = value;
            }
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
    }
}
