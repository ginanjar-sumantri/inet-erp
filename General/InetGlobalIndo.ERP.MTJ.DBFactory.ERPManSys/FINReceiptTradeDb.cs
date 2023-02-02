using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINReceiptTradeDb
    {
        private string _accBankName = "";
        private string _accChargeName = "";
        private string _payTypeName = "";

        public FINReceiptTradeDb(String _prmReceiptNo, int _prmItemNo, String _prmReceiptType, String _prmPayName, String _prmDocumentNo,
        String _prmCurrCode, Decimal _prmForexRate, Decimal _prmAmountForex,Decimal? _prmAmountHome, String _prmRemark,
        Char _prmFgGiro, Char? _prmFgDP, String _prmBankGiro,
        DateTime? _prmDueDate, Decimal? _prmBankExpense, Decimal? _prmCustRevenue, String _prmAccBank, String _prmAccBankName, Char? _prmFgBank, String _prmAccCharges, String _prmAccChargeName, Char? _prmFgCharges,
            Decimal? _prmReceiptForex, Decimal? _prmReceiptHome)
        {
            this.TransNmbr      = _prmReceiptNo;
            this.ItemNo         = _prmItemNo;
            this.ReceiptType    = _prmReceiptType;
            this.PayName        = _prmPayName;
            this.DocumentNo     = _prmDocumentNo;
            this.CurrCode       = _prmCurrCode;
            this.ForexRate      = _prmForexRate;
            this.AmountForex    = _prmAmountForex;
            this.AmountHome     = _prmAmountHome;            
            this.Remark         = _prmRemark;
            this.FgGiro         = _prmFgGiro;
            this.FgDP           = _prmFgDP;
            this.BankGiro       = _prmBankGiro;
            this.DueDate        = _prmDueDate;
            this.BankExpense    = _prmBankExpense;
            this.CustRevenue    = _prmCustRevenue;
            this.AccBank        = _prmAccBank;
            this.AccBankName    = _prmAccBankName;
            this.FgBank         = _prmFgBank;
            this.AccCharges     = _prmAccCharges;
            this.AccChargeName  = _prmAccChargeName;
            this.FgCharges      = _prmFgCharges;
            this.ReceiptForex   = _prmReceiptForex;
            this.ReceiptHome    = _prmReceiptHome;
        }

        public string AccBankName
        {
            get
            {
                return this._accBankName;
            }
            set
            {
                this._accBankName = value;
            }
        }

        public string AccChargeName
        {
            get
            {
                return this._accChargeName;
            }
            set
            {
                this._accChargeName = value;
            }
        }

        public string PayName
        {
            get
            {
                return this._payTypeName;
            }
            set
            {
                this._payTypeName = value;
            }
        }
    }
}
