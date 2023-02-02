using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Billing_CustomerInvoiceHd
    {
        private string _custName = "";
        public Billing_CustomerInvoiceHd(Guid _prmCustomerInvoiceHdCode, string _prmTransNmbr, string _prmCustName, char _prmStatus, string _prmCurrCode, decimal? _prmBaseForex, decimal? _prmPPNForex, decimal? _prmDiscForex, decimal? _prmOtherFee, decimal? _prmStampFee, decimal? _prmCommissionExpense, decimal? _prmTotalForex)
        {
            this.CustomerInvoiceHdCode = _prmCustomerInvoiceHdCode;
            this.TransNmbr = _prmTransNmbr;
            this.CustName = _prmCustName;
            this.Status = _prmStatus;
            this.CurrCode = _prmCurrCode;
            this.BaseForex = _prmBaseForex;
            this.PPNForex = _prmPPNForex;
            this.DiscForex = _prmDiscForex;
            this.OtherFee = _prmOtherFee;
            this.StampFee = _prmStampFee;
            this.CommissionExpense = _prmCommissionExpense;
            this.TotalForex = _prmTotalForex;
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
    }
}
