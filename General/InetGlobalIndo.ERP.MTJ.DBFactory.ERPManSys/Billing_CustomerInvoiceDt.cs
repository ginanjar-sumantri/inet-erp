using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Billing_CustomerInvoiceDt
    {

        public Billing_CustomerInvoiceDt(Guid _prmCustomerInvoiceDtCode, Guid _prmCustomerInvoiceHdCode, string _prmCustInvoiceDescription, decimal _prmAmountForex, string _prmRemark)
        {
            this.CustomerInvoiceDtCode = _prmCustomerInvoiceDtCode;
            this.CustomerInvoiceHdCode = _prmCustomerInvoiceHdCode;
            this.CustInvoiceDescription = _prmCustInvoiceDescription;
            this.AmountForex = _prmAmountForex;
            this.Remark = _prmRemark;
        }

    }
}
