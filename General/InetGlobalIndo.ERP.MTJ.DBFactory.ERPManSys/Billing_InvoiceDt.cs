using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Billing_InvoiceDt
    {
        private string _custBillAccount = "";
        private string _productName = "";

        public Billing_InvoiceDt(Guid _prmInvoiceDt, Guid _prmInvoiceHd, string _prmCustBillAccount, string _prmCustBillDescription, string _prmProductName, decimal _prmAmountForex, string _prmRemark)
        {
            this.InvoiceDt = _prmInvoiceDt;
            this.InvoiceHd = _prmInvoiceHd;
            this.CustBillAccount = _prmCustBillAccount;
            this.CustBillDescription = _prmCustBillDescription;
            this.ProductName = _prmProductName;
            this.AmountForex = _prmAmountForex;
            this.Remark = _prmRemark;
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
