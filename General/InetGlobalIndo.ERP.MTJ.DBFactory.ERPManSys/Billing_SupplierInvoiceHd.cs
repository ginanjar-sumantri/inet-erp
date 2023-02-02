using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Billing_SupplierInvoiceHd
    {
        private string _suppName = "";
        public Billing_SupplierInvoiceHd(Guid _prmSupplierInvoiceHdCode, string _prmTransNmbr, string _prmSuppName, char _prmStatus, string _prmCurrCode, decimal? _prmBaseForex, decimal? _prmPPNForex, decimal? _prmDiscForex, decimal? _prmOtherFee, decimal? _prmStampFee, decimal? _prmTotalForex)
        {
            this.SupplierInvoiceHdCode = _prmSupplierInvoiceHdCode;
            this.TransNmbr = _prmTransNmbr;
            this.SuppName = _prmSuppName;
            this.Status = _prmStatus;
            this.CurrCode = _prmCurrCode;
            this.BaseForex = _prmBaseForex;
            this.PPNForex = _prmPPNForex;
            this.DiscForex = _prmDiscForex;
            this.OtherFee = _prmOtherFee;
            this.StampFee = _prmStampFee;
            this.TotalForex = _prmTotalForex;
        }

        public string SuppName
        {
            get
            {
                return this._suppName;
            }
            set
            {
                this._suppName = value;
            }
        }
    }
}
