using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_CustBillAccount
    {
        private string _custName = "";
        private string _productName = "";

        public Master_CustBillAccount(Guid _prmCustBillCode, string _prmCustBillAccount, string _prmCustCode, string _prmCustName, string _prmProductCode, string _prmProductName, string _prmCustBillDescription, string _prmCurrCode, decimal? _prmAmountForex, bool? _prmFgActive)
        {
            this.CustBillCode = _prmCustBillCode;
            this.CustBillAccount = _prmCustBillAccount;
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.CustBillDescription = _prmCustBillDescription;
            this.CurrCode = _prmCurrCode;
            this.AmountForex = _prmAmountForex;
            this.fgActive = _prmFgActive;
        }

        public Master_CustBillAccount(Guid _prmCustBillCode, string _prmCustBillAccount)
        {
            this.CustBillCode = _prmCustBillCode;
            this.CustBillAccount = _prmCustBillAccount;
        }

        public Master_CustBillAccount(Guid _prmCustBillCode, string _prmCustCode, string _prmCustName)
        {
            this.CustBillCode = _prmCustBillCode;
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
        }

        public Master_CustBillAccount(string _prmCustBillAccount, string _prmCustCode, string _prmCustName)
        {
            this.CustBillAccount = _prmCustBillAccount;
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
        }


        public string CustName
        {
            get
            {
                return this._custName;
            }
            set
            {
                this._custName = value;
            }
        }

        public string ProductName
        {
            get
            {
                return this._productName;
            }
            set
            {
                this._productName = value;
            }
        }
    }
}
