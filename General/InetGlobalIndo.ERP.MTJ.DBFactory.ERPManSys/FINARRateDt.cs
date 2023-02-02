using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINARRateDt
    {
        private string _custName = "";

        public FINARRateDt(string _prmTransNmbr, string _prmInvoiceNo, string _prmCustCode, string _prmCustName, decimal _prmForexRate, decimal _prmAmountForex, decimal _prmAmountHome, decimal _prmNewAmountHome, bool _prmIsApplyToPPN)
        {
            this.TransNmbr = _prmTransNmbr;
            this.InvoiceNo = _prmInvoiceNo;
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
            this.ForexRate = _prmForexRate;
            this.AmountForex = _prmAmountForex;
            this.AmountHome = _prmAmountHome;
            this.NewAmountHome = _prmNewAmountHome;
            this.IsApplyToPPN = _prmIsApplyToPPN;
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
