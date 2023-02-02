using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Billing_SupplierInvoiceDt
    {
        public Billing_SupplierInvoiceDt(Guid _prmSupplierInvoiceDtCode, Guid _prmSupplierInvoiceHdCode, string _prmItemDescription, decimal _prmAmountForex, string _prmRemark)
        {
            this.SupplierInvoiceDtCode = _prmSupplierInvoiceDtCode;
            this.SupplierInvoiceHdCode = _prmSupplierInvoiceHdCode;
            this.ItemDescription = _prmItemDescription;
            this.AmountForex = _prmAmountForex;
            this.Remark = _prmRemark;
        }

        public Billing_SupplierInvoiceDt(Guid _prmSupplierInvoiceDtCode, Guid _prmSupplierInvoiceHdCode, string _prmItemDescription, decimal _prmAmountForex, string _prmRemark,string _prmAccount)
        {
            this.SupplierInvoiceDtCode = _prmSupplierInvoiceDtCode;
            this.SupplierInvoiceHdCode = _prmSupplierInvoiceHdCode;
            this.ItemDescription = _prmItemDescription;
            this.AmountForex = _prmAmountForex;
            this.Remark = _prmRemark;
            this.Account = _prmAccount;
        }
    }
}
