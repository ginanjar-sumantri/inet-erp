using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINDPSuppDt
    {
        private string _bankPaymentName = "";

        public FINDPSuppDt(int _prmItemNo, string _prmPayType, DateTime? _prmDueDate, string _prmBankPaymentName, decimal _prmAmountForex)
        {
            this.ItemNo = _prmItemNo;
            this.PayType = _prmPayType;
            this.DueDate = _prmDueDate;
            this.BankPaymentName = _prmBankPaymentName;
            this.AmountForex = _prmAmountForex;
        }

        public string BankPaymentName
        {
            get
            {
                return this._bankPaymentName;
            }
            set
            {
                this._bankPaymentName = value;
            }
        }
    }
}
