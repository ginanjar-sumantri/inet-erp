using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class POSTrSettlementDtPaymentType
    {
        private String _reference = "";
        private String _divisi = "";
        private DateTime _transDate = DateTime.Now;

        public POSTrSettlementDtPaymentType(String _prmTransNmbr, String _prmPaymentType,
            Decimal _prmPaymentAmount, String _prmCardType, String _prmEDCReference,
            String _prmCardNumber, String _prmAccount, Char _prmFgSubLed)
        {
            this.TransNmbr = _prmTransNmbr;
            this.PaymentType = _prmPaymentType;
            this.CardType = _prmCardType;
            this.PaymentAmount = _prmPaymentAmount;
            this.EDCReference = _prmEDCReference;
            this.CardNumber = _prmCardNumber;
            this.Account = _prmAccount;
            this.FgSubLed = _prmFgSubLed;
        }

        public POSTrSettlementDtPaymentType(String _prmPaymentType, Decimal _prmPaymentAmount)
        {
            this.PaymentType = _prmPaymentType;
            this.PaymentAmount = _prmPaymentAmount;
        }

        public POSTrSettlementDtPaymentType(String _prmTransNmbr,DateTime _prmTransDate, /*String _prmReference
            ,*/ String _prmPaymentType, /*String _prmDivisi,*/ Decimal _prmAmountTransaction, String _prmCardType)
        {
            this.TransNmbr = _prmTransNmbr;
            this.TransDate = _prmTransDate;
            /*this.Reference = _prmReference;*/
            this.PaymentType = _prmPaymentType;
            /*this.Divisi = _prmDivisi;*/
            this.PaymentAmount = _prmAmountTransaction;
            this.CardType = _prmCardType;
        }

        public DateTime TransDate
        {
            get
            {
                return this._transDate;
            }
            set
            {
                this._transDate = value;
            }
        }

        public String Divisi
        {
            get
            {
                return this._divisi;
            }
            set
            {
                this._divisi = value;
            }
        }

        public String Reference
        {
            get
            {
                return this._reference;
            }
            set
            {
                this._reference = value;
            }
        }

    }
}