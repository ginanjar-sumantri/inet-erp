using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class BILTrUnSubscriptionDt
    {
        private string _companyName = "";
        private string _custBillAccount = "";
        private string _productName = "";
        private string _curr = "";
        private Decimal _amountForex = 0;
        private byte _typepayment = 0;
        private DateTime _activateDate;
        private DateTime _expiredDate;
        private string _custBillDescription = "";

        public BILTrUnSubscriptionDt(String _prmTransNmbr, Guid _prmCustBillCode)
        {
            this.TransNmbr = _prmTransNmbr;
            this.CustBillCode = _prmCustBillCode;
        }

        public BILTrUnSubscriptionDt(String _prmTransNmbr, String _prmCustBillAccount, String _prmProductName,
            String _prmCurr, Decimal _prmAmountForex, Byte _prmTypePayment, DateTime _prmActiveDate, DateTime _prmExpiredDate, Guid _prmCustBillCode, String _prmCustBillDescription)
        {
            this.TransNmbr = _prmTransNmbr;
            this.CustBillAccount = _prmCustBillAccount;
            this.ProductName = _prmProductName;
            this.Curr = _prmCurr;
            this.AmountForex = _prmAmountForex;
            this.Typepayment = _prmTypePayment;
            this.ActivateDate = _prmActiveDate;
            this.ExpiredDate = _prmExpiredDate;
            this.CustBillCode = _prmCustBillCode;
            this.CustBillDescription = _prmCustBillDescription;
        }

        public string CompanyName
        {
            get
            {
                return this._companyName;
            }
            set
            {
                this._companyName = value;
            }
        }

        public string CustBillAccount
        {
            get
            {
                return this._custBillAccount;
            }
            set
            {
                this._custBillAccount = value;
            }
        }

        public string CustBillDescription
        {
            get
            {
                return this._custBillDescription;
            }
            set
            {
                this._custBillDescription = value;
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

        public string Curr
        {
            get
            {
                return this._curr;
            }
            set
            {
                this._curr = value;
            }
        }

        public decimal AmountForex
        {
            get
            {
                return this._amountForex;
            }
            set
            {
                this._amountForex = value;
            }
        }

        public byte Typepayment
        {
            get
            {
                return this._typepayment;
            }
            set
            {
                this._typepayment = value;
            }
        }

        public DateTime ActivateDate
        {
            get
            {
                return this._activateDate;
            }
            set
            {
                this._activateDate = value;
            }
        }

        public DateTime ExpiredDate
        {
            get
            {
                return this._expiredDate;
            }
            set
            {
                this._expiredDate = value;
            }
        }
    }
}
