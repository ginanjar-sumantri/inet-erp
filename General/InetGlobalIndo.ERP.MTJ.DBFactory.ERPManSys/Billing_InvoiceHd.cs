using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Billing_InvoiceHd
    {
        private string _custName = "";
        private string _custEmail = "";

        public Billing_InvoiceHd(Guid _prmInvoiceHd, string _prmTransNmbr, string _prmCustName, char _prmStatus, string _prmCurrCode, decimal? _prmBaseForex, decimal? _prmPPNForex, decimal? _prmDiscForex, decimal? _prmStampFee, decimal? _prmOtherFee, decimal? _prmTotalForex)
        {
            this.InvoiceHd = _prmInvoiceHd;
            this.TransNmbr = _prmTransNmbr;
            this.CustName = _prmCustName;
            this.Status = _prmStatus;
            this.CurrCode = _prmCurrCode;
            this.BaseForex = _prmBaseForex;
            this.PPNForex = _prmPPNForex;
            this.DiscForex = _prmDiscForex;
            this.StampFee = _prmStampFee;
            this.OtherFee = _prmOtherFee;
            this.TotalForex = _prmTotalForex;
        }

        public Billing_InvoiceHd(Guid _prmInvoiceHd, String _prmTransNmbr, int _prmPeriod, int _prmYear, String _prmCustName, String _prmCustEmail)
        {
            this.InvoiceHd = _prmInvoiceHd;
            this.TransNmbr = _prmTransNmbr;
            this.Period = _prmPeriod;
            this.Year = _prmYear;
            this.CustName = _prmCustName;
            this.CustEmail = _prmCustEmail;
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

        public string CustEmail
        {
            get
            {
                return this._custEmail;
            }
            set
            {
                this._custEmail = value;
            }
        }
    }
}
