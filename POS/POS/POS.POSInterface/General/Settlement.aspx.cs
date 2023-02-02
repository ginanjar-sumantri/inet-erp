using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using BusinessRule.POS;
using BusinessRule.POSInterface;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Tour;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;

namespace POS.POSInterface.General
{
    public partial class Settlement : GeneralBase
    {
        private CurrencyBL _currBL = new CurrencyBL();
        private CashierBL _cashierBL = new CashierBL();
        private POSRetailBL _retailBL = new POSRetailBL();
        private POSInternetBL _internetBL = new POSInternetBL();
        private POSCafeBL _cafeBL = new POSCafeBL();
        private POSTableStatusHistoryBL _tableHistBL = new POSTableStatusHistoryBL();
        private CreditCardBL _creditCardBL = new CreditCardBL();
        private DebitCardBL _debitCardBL = new DebitCardBL();
        private CreditCardTypeBL _cardTypeBL = new CreditCardTypeBL();
        private TicketingBL _ticketingBL = new TicketingBL();
        private HotelBL _hotelBL = new HotelBL();
        private POSConfigurationBL _pOSConfigurationBL = new POSConfigurationBL();
        private KitchenBL _kitchenBL = new KitchenBL();
        private POSPrintingBL _printingBL = new POSPrintingBL();
        private POSPhotocopyBL _photocopyBL = new POSPhotocopyBL();
        private POSGraphicBL _graphicBL = new POSGraphicBL();
        private POSShippingBL _shippingBL = new POSShippingBL();
        private ProductBL _productBL = new ProductBL();
        private CompanyBL _companyBL = new CompanyBL();
        
        //private CashierPrinterBL _cashierPrinterBL = new CashierPrinterBL();

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _nomor = 0;

        private bool _prmDO = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.ShowPanel(POSSettlementButtonType.Cash);
                this.SetAttribute();
                this.ClearLabel();
                this.ClearData();
                this.cekDO();
                this.ShowData();
                this.CashButton_Click(null, null);
            }
        }

        private void SetAttribute()
        {
            this.AmountPaymentTextBox.Attributes.Add("onfocus", "$('#currActiveInput').val(this.id);");
            this.CreditCardReferenceTextBox.Attributes.Add("onfocus", "$('#currActiveInputCredit').val(this.id)");
            this.CreditCardNominalTextBox.Attributes.Add("onfocus", "$('#currActiveInputCredit').val(this.id);");
            this.DebitCardReferenceTextBox.Attributes.Add("onfocus", "$('#currActiveInputDebit').val(this.id);");
            this.DebitCardNominalTextBox.Attributes.Add("onfocus", "$('#currActiveInputDebit').val(this.id);");
            this.VoucherNoTextBox.Attributes.Add("onfocus", "$('#currActiveInputVoucher').val(this.id);");
            this.VoucherNominalTextBox.Attributes.Add("onfocus", "$('#currActiveInputVoucher').val(this.id);");

            this.AmountPaymentTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.AmountPaymentTextBox.ClientID + "," + this.AmountPaymentTextBox.ClientID + ",500" + ");");
            this.CreditCardNominalTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.CreditCardNominalTextBox.ClientID + "," + this.CreditCardNominalTextBox.ClientID + ",500" + ")");
            this.DebitCardNominalTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.DebitCardNominalTextBox.ClientID + "," + this.DebitCardNominalTextBox.ClientID + ",500" + ")");
            this.VoucherNominalTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.VoucherNominalTextBox.ClientID + "," + this.VoucherNominalTextBox.ClientID + ",500" + ")");
            this.CreditCardReferenceTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.CreditCardReferenceTextBox.ClientID + "," + this.CreditCardReferenceTextBox.ClientID + ",500" + ")");
            this.DebitCardReferenceTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.DebitCardReferenceTextBox.ClientID + "," + this.DebitCardReferenceTextBox.ClientID + ",500" + ")");
        }

        private void ShowPanel(POSSettlementButtonType _prmAction)
        {
            if (_prmAction == POSSettlementButtonType.Cash)
            {
                this.CashPanel.Visible = true;
                this.CreditCardPanel.Visible = false;
                this.DebitPanel.Visible = false;
                this.VoucherPanel.Visible = false;
                this.SplitCashPanel.Visible = false;
            }
            else if (_prmAction == POSSettlementButtonType.CreditCard)
            {
                this.CashPanel.Visible = false;
                this.CreditCardPanel.Visible = true;
                this.DebitPanel.Visible = false;
                this.VoucherPanel.Visible = false;
                this.SplitCashPanel.Visible = false;
            }
            else if (_prmAction == POSSettlementButtonType.Debit)
            {
                this.CashPanel.Visible = false;
                this.CreditCardPanel.Visible = false;
                this.DebitPanel.Visible = true;
                this.VoucherPanel.Visible = false;
                this.SplitCashPanel.Visible = false;
            }
            else if (_prmAction == POSSettlementButtonType.Voucher)
            {
                this.CashPanel.Visible = false;
                this.CreditCardPanel.Visible = false;
                this.DebitPanel.Visible = false;
                this.VoucherPanel.Visible = true;
                this.SplitCashPanel.Visible = false;
            }
            else if (_prmAction == POSSettlementButtonType.SplitCash)
            {
                this.CashPanel.Visible = false;
                this.CreditCardPanel.Visible = false;
                this.DebitPanel.Visible = false;
                this.VoucherPanel.Visible = false;
                this.SplitCashPanel.Visible = true;
            }
        }

        private void ShowPanel()
        {
            this.ProductPromoPanel.Visible = false;
            this.NumberInputPanel.Visible = true;
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
            this.WarningLabelPay.Text = "";
        }

        private void ClearData()
        {
            Decimal _default = 0;

            //this.DiscountLiteral.Text = _default.ToString("#,##0.00");
            //this.SubtotalLiteral.Text = _default.ToString("#,##0.00");
            //this.TaxLiteral.Text = _default.ToString("#,##0.00");
            //this.ServiceChargeLiteral.Text = _default.ToString("#,##0.00");
            //this.DPReceivedLiteral.Text = _default.ToString("#,##0.00");
            //this.OtherFeeLiteral.Text = _default.ToString("#,##0.00");
            //this.RoundLiteral.Text = _default.ToString("#,##0.00");
            //this.TotalLiteral.Text = _default.ToString("#,##0.00");

            //this.CashPaymentLiteral.Text = _default.ToString("#,##0.00");
            //this.CreditCardLiteral.Text = _default.ToString("#,##0.00");
            //this.DebitCardLiteral.Text = _default.ToString("#,##0.00");
            //this.VoucherLiteral.Text = _default.ToString("#,##0.00");
            //this.ChangeLiteral.Text = _default.ToString("#,##0.00");

            //this.ChangeDueLiteral.Text = _default.ToString("#,##0.00");

            this.DiscountLiteral.Text = _default.ToString("#,##0");
            this.SubtotalLiteral.Text = _default.ToString("#,##0");
            this.TaxLiteral.Text = _default.ToString("#,##0");
            this.ServiceChargeLiteral.Text = _default.ToString("#,##0");
            this.DPReceivedLiteral.Text = _default.ToString("#,##0");
            this.OtherFeeLiteral.Text = _default.ToString("#,##0");
            this.RoundLiteral.Text = _default.ToString("#,##0");
            this.TotalLiteral.Text = _default.ToString("#,##0");
            this.BankChargeLiteral.Text = _default.ToString("#,##0");

            this.CashPaymentLiteral.Text = _default.ToString("#,##0");
            this.CreditCardLiteral.Text = _default.ToString("#,##0");
            this.DebitCardLiteral.Text = _default.ToString("#,##0");
            this.VoucherLiteral.Text = _default.ToString("#,##0");
            this.ChangeLiteral.Text = _default.ToString("#,##0");

            this.ChangeDueLiteral.Text = _default.ToString("#,##0");
            this.AmountPaymentTextBox.Text = "0";

            //this.TransactionHiddenField.Value = "";
            this.CreditCardHiddenField.Value = "";
            this.DebitHiddenField.Value = "";

            this.ProductPromoPanel.Visible = false;
        }

        private void ShowData()
        {
            try
            {
                String _selectedTransNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
                if (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._settleType), ApplicationConfig.EncryptionKey) == POSTrSettlementDataMapper.GetSettleType(POSTrSettleType.Paid))
                {
                    this.SettlementType.Text = "SETTLEMENT";
                }
                if (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._settleType), ApplicationConfig.EncryptionKey) == POSTrSettlementDataMapper.GetSettleType(POSTrSettleType.DP))
                {
                    this.SettlementType.Text = "DOWN PAYMENT";
                }

                String[] _transNmbr = _selectedTransNmbr.Split(',');

                Decimal _discount = 0;
                Decimal _subTotal = 0;
                Decimal _tax = 0;
                Decimal _pb1 = 0;
                Decimal _serviceCharge = 0;
                Decimal _dpReceive = 0;
                Decimal _otherFee = 0;
                Decimal _round = 0;
                Decimal _grandTotal = 0;

                Decimal _cashPaymentAmount = 0;
                Decimal _creditCardPaymentAmount = 0;
                Decimal _debitCardPaymentAmount = 0;
                Decimal _giftVoucherAmount = 0;
                Decimal _changeAmount = 0;
                Decimal _changeDueAmount = 0;


                foreach (var _item in _transNmbr)
                {
                    V_POSReferenceNotYetPayList _data = new V_POSReferenceNotYetPayList();

                    if (_prmDO == false)
                    {
                        _data = this._cashierBL.GetSingleReferenceNotPay(_item);
                    }
                    else if (_prmDO == true)
                    {
                        V_POSReferenceNotYetPayListAll _dataDO = this._cashierBL.GetFirstReferenceNotPayAll(_item);
                        _data.TransNmbr = _dataDO.TransNmbr;
                        _data.TransType = _dataDO.TransType;
                        _data.ReferenceNo = _dataDO.ReferenceNo;
                        _data.DoneSettlement = _dataDO.DoneSettlement;
                        _data.DeliveryStatus = _dataDO.DeliveryStatus;
                        _data.MemberID = _dataDO.MemberID;
                        _data.CustName = _dataDO.CustName;
                        _data.CustPhone = _dataDO.CustPhone;
                        _data.date = _dataDO.date;
                    }

                    if (_data.TransType.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Retail).Trim().ToLower())
                    {
                        POSTrRetailHd _retailHd = this._retailBL.GetSingle(_data.TransNmbr);

                        _discount += (_retailHd.DiscForex == null) ? 0 : Convert.ToDecimal(_retailHd.DiscForex);
                        _tax += (_retailHd.PPNForex == null) ? 0 : Convert.ToDecimal(_retailHd.PPNForex);
                        _pb1 += (_retailHd.PB1Forex == null) ? 0 : Convert.ToDecimal(_retailHd.PB1Forex);
                        _serviceCharge += (_retailHd.ServiceChargeAmount == null) ? 0 : Convert.ToDecimal(_retailHd.ServiceChargeAmount);
                        //_subTotal += (_retailHd.TotalForex == null) ? 0 : Convert.ToDecimal(_retailHd.TotalForex);
                        _subTotal += (_retailHd.SubTotalForex == null) ? 0 : Convert.ToDecimal(_retailHd.SubTotalForex);
                        //_dpReceive += (_retailHd.DPForex == null) ? 0 : Convert.ToDecimal(_retailHd.DPForex);
                        _dpReceive += (_retailHd.DPPaid == null) ? 0 : Convert.ToDecimal(_retailHd.DPPaid);
                        _otherFee += (_retailHd.OtherForex == null) ? 0 : Convert.ToDecimal(_retailHd.OtherForex);
                        _grandTotal += (_retailHd.TotalForex == null) ? 0 : Convert.ToDecimal(_retailHd.TotalForex);
                        //_dpReceive += (_retailHd.DPPaid == null) ? 0 : Convert.ToDecimal(_retailHd.DPPaid);

                        //_changeAmount += (_retailHd.TotalForex == null) ? 0 : Convert.ToDecimal(_retailHd.TotalForex);
                        //_changeDueAmount += (_retailHd.TotalForex == null) ? 0 : Convert.ToDecimal(_retailHd.TotalForex);

                        this.CashierHiddenField.Value = _retailHd.UserPrep;
                        if (this.TransactionHiddenField.Value == "")
                        {
                            this.TransactionHiddenField.Value = _retailHd.TransNmbr + "," + _retailHd.TransType;
                        }
                        else
                        {
                            this.TransactionHiddenField.Value += "|" + _retailHd.TransNmbr + "," + _retailHd.TransType;
                        }
                    }
                    else if (_data.TransType.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Internet).Trim().ToLower())
                    {
                        POSTrInternetHd _internetHd = this._internetBL.GetSinglePOSTrInternetHd(_data.TransNmbr);

                        _discount += (_internetHd.DiscForex == null) ? 0 : Convert.ToDecimal(_internetHd.DiscForex);
                        _tax += (_internetHd.PPNForex == null) ? 0 : Convert.ToDecimal(_internetHd.PPNForex);
                        _pb1 += (_internetHd.PB1Forex == null) ? 0 : Convert.ToDecimal(_internetHd.PB1Forex);
                        _serviceCharge += (_internetHd.ServiceChargeAmount == null) ? 0 : Convert.ToDecimal(_internetHd.ServiceChargeAmount);
                        //_subTotal += (_internetHd.TotalForex == null) ? 0 : Convert.ToDecimal(_internetHd.TotalForex);
                        _subTotal += (_internetHd.SubTotalForex == null) ? 0 : Convert.ToDecimal(_internetHd.SubTotalForex);
                        //_dpReceive += (_internetHd.DPForex == null) ? 0 : Convert.ToDecimal(_internetHd.DPForex);
                        _dpReceive += (_internetHd.DPPaid == null) ? 0 : Convert.ToDecimal(_internetHd.DPPaid);
                        _otherFee += (_internetHd.OtherForex == null) ? 0 : Convert.ToDecimal(_internetHd.OtherForex);
                        _grandTotal += (_internetHd.TotalForex == null) ? 0 : Convert.ToDecimal(_internetHd.TotalForex);
                        //_changeAmount += (_internetHd.TotalForex == null) ? 0 : Convert.ToDecimal(_internetHd.TotalForex);
                        //_changeDueAmount += (_internetHd.TotalForex == null) ? 0 : Convert.ToDecimal(_internetHd.TotalForex);

                        this.CashierHiddenField.Value = _internetHd.UserPrep;

                        if (this.TransactionHiddenField.Value == "")
                        {
                            this.TransactionHiddenField.Value = _internetHd.TransNmbr + "," + _internetHd.TransType + "," + _internetHd.ReferenceNo;
                        }
                        else
                        {
                            this.TransactionHiddenField.Value += "|" + _internetHd.TransNmbr + "," + _internetHd.TransType + "," + _internetHd.ReferenceNo;
                        }
                    }

                    else if (_data.TransType.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe).Trim().ToLower())
                    {
                        POSTrCafeHd _cafeHd = this._cafeBL.GetSinglePOSTrCafeHd(_data.TransNmbr);

                        _discount += (_cafeHd.DiscForex == null) ? 0 : Convert.ToDecimal(_cafeHd.DiscForex);
                        _tax += (_cafeHd.PPNForex == null) ? 0 : Convert.ToDecimal(_cafeHd.PPNForex);
                        _pb1 += (_cafeHd.PB1Forex == null) ? 0 : Convert.ToDecimal(_cafeHd.PB1Forex);
                        _serviceCharge += (_cafeHd.ServiceChargeAmount == null) ? 0 : Convert.ToDecimal(_cafeHd.ServiceChargeAmount);
                        //_subTotal += (_cafeHd.TotalForex == null) ? 0 : Convert.ToDecimal(_cafeHd.TotalForex);
                        _subTotal += (_cafeHd.SubTotalForex == null) ? 0 : Convert.ToDecimal(_cafeHd.SubTotalForex);
                        //_dpReceive += (_cafeHd.DPForex == null) ? 0 : Convert.ToDecimal(_cafeHd.DPForex);
                        _dpReceive += (_cafeHd.DPPaid == null) ? 0 : Convert.ToDecimal(_cafeHd.DPPaid);
                        _otherFee += (_cafeHd.OtherForex == null) ? 0 : Convert.ToDecimal(_cafeHd.OtherForex);
                        _grandTotal += (_cafeHd.TotalForex == null) ? 0 : Convert.ToDecimal(_cafeHd.TotalForex);

                        this.CashierHiddenField.Value = _cafeHd.UserPrep;

                        if (this.TransactionHiddenField.Value == "")
                        {
                            this.TransactionHiddenField.Value = _cafeHd.TransNmbr + "," + _cafeHd.TransType + "," + _cafeHd.ReferenceNo;
                        }
                        else
                        {
                            this.TransactionHiddenField.Value += "|" + _cafeHd.TransNmbr + "," + _cafeHd.TransType + "," + _cafeHd.ReferenceNo;
                        }
                    }
                    else if (_data.TransType.Trim().ToLower() == AppModule.GetValue(TransactionType.Ticketing).Trim().ToLower())
                    {
                        POSTrTicketingHd _ticketingHd = this._ticketingBL.GetSinglePOSTrTicketingHd(_data.TransNmbr);

                        _discount += (_ticketingHd.DiscForex == null) ? 0 : Convert.ToDecimal(_ticketingHd.DiscForex);
                        _tax += (_ticketingHd.PPNForex == null) ? 0 : Convert.ToDecimal(_ticketingHd.PPNForex);
                        //_pb1 += (_ticketingHd.PB1Forex == null) ? 0 : Convert.ToDecimal(_ticketingHd.PB1Forex);
                        //_serviceCharge += (_ticketingHd.ServiceChargeAmount == null) ? 0 : Convert.ToDecimal(_ticketingHd.ServiceChargeAmount);
                        //_subTotal += (_ticketingHd.TotalForex == null) ? 0 : Convert.ToDecimal(_ticketingHd.TotalForex);
                        _subTotal += (_ticketingHd.SubTotalForex == null) ? 0 : Convert.ToDecimal(_ticketingHd.SubTotalForex);
                        //_dpReceive += (_ticketingHd.DPForex == null) ? 0 : Convert.ToDecimal(_ticketingHd.DPForex);
                        _dpReceive += (_ticketingHd.DPPaid == null) ? 0 : Convert.ToDecimal(_ticketingHd.DPPaid);
                        _otherFee += (_ticketingHd.OtherForex == null) ? 0 : Convert.ToDecimal(_ticketingHd.OtherForex);
                        _grandTotal += (_ticketingHd.TotalForex == null) ? 0 : Convert.ToDecimal(_ticketingHd.TotalForex);

                        //_changeAmount += (_internetHd.TotalForex == null) ? 0 : Convert.ToDecimal(_internetHd.TotalForex);
                        //_changeDueAmount += (_internetHd.TotalForex == null) ? 0 : Convert.ToDecimal(_internetHd.TotalForex);

                        this.CashierHiddenField.Value = _ticketingHd.UserPrep;

                        if (this.TransactionHiddenField.Value == "")
                        {
                            this.TransactionHiddenField.Value = _ticketingHd.TransNmbr + "," + _ticketingHd.TransType + "," + _ticketingHd.ReferenceNo;
                        }
                        else
                        {
                            this.TransactionHiddenField.Value += "|" + _ticketingHd.TransNmbr + "," + _ticketingHd.TransType + "," + _ticketingHd.ReferenceNo;
                        }
                    }
                    else if (_data.TransType.Trim().ToLower() == AppModule.GetValue(TransactionType.Hotel).Trim().ToLower())
                    {
                        POSTrHotelHd _hotelHd = this._hotelBL.GetSinglePOSTrHotelHd(_data.TransNmbr);

                        _discount += (_hotelHd.DiscForex == null) ? 0 : Convert.ToDecimal(_hotelHd.DiscForex);
                        _tax += (_hotelHd.PPNForex == null) ? 0 : Convert.ToDecimal(_hotelHd.PPNForex);
                        //_pb1 += (_hotelHd.PB1Forex == null) ? 0 : Convert.ToDecimal(_hotelHd.PB1Forex);
                        //_serviceCharge += (_hotelHd.ServiceChargeAmount == null) ? 0 : Convert.ToDecimal(_hotelHd.ServiceChargeAmount);
                        //_subTotal += (_hotelHd.TotalForex == null) ? 0 : Convert.ToDecimal(_hotelHd.TotalForex);
                        _subTotal += (_hotelHd.SubTotalForex == null) ? 0 : Convert.ToDecimal(_hotelHd.SubTotalForex);
                        //_dpReceive += (_hotelHd.DPForex == null) ? 0 : Convert.ToDecimal(_hotelHd.DPForex);
                        _dpReceive += (_hotelHd.DPPaid == null) ? 0 : Convert.ToDecimal(_hotelHd.DPPaid);
                        _otherFee += (_hotelHd.OtherForex == null) ? 0 : Convert.ToDecimal(_hotelHd.OtherForex);
                        _grandTotal += (_hotelHd.TotalForex == null) ? 0 : Convert.ToDecimal(_hotelHd.TotalForex);

                        //_changeAmount += (_internetHd.TotalForex == null) ? 0 : Convert.ToDecimal(_internetHd.TotalForex);
                        //_changeDueAmount += (_internetHd.TotalForex == null) ? 0 : Convert.ToDecimal(_internetHd.TotalForex);

                        this.CashierHiddenField.Value = _hotelHd.UserPrep;

                        if (this.TransactionHiddenField.Value == "")
                        {
                            this.TransactionHiddenField.Value = _hotelHd.TransNmbr + "," + _hotelHd.TransType + "," + _hotelHd.ReferenceNo;
                        }
                        else
                        {
                            this.TransactionHiddenField.Value += "|" + _hotelHd.TransNmbr + "," + _hotelHd.TransType + "," + _hotelHd.ReferenceNo;
                        }
                    }

                    else if (_data.TransType.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Printing).Trim().ToLower())
                    {
                        POSTrPrintingHd _printingHd = this._printingBL.GetSinglePOSTrPrintingHd(_data.TransNmbr);

                        _discount += (_printingHd.DiscForex == null) ? 0 : Convert.ToDecimal(_printingHd.DiscForex);
                        _tax += (_printingHd.PPNForex == null) ? 0 : Convert.ToDecimal(_printingHd.PPNForex);
                        _pb1 += (_printingHd.PB1Forex == null) ? 0 : Convert.ToDecimal(_printingHd.PB1Forex);
                        _serviceCharge += (_printingHd.ServiceChargeAmount == null) ? 0 : Convert.ToDecimal(_printingHd.ServiceChargeAmount);
                        //_subTotal += (_printingHd.TotalForex == null) ? 0 : Convert.ToDecimal(_printingHd.TotalForex);
                        _subTotal += (_printingHd.SubTotalForex == null) ? 0 : Convert.ToDecimal(_printingHd.SubTotalForex);
                        //_dpReceive += (_printingHd.DPForex == null) ? 0 : Convert.ToDecimal(_printingHd.DPForex);
                        _dpReceive += (_printingHd.DPPaid == null) ? 0 : Convert.ToDecimal(_printingHd.DPPaid);
                        _otherFee += (_printingHd.OtherForex == null) ? 0 : Convert.ToDecimal(_printingHd.OtherForex);
                        _grandTotal += (_printingHd.TotalForex == null) ? 0 : Convert.ToDecimal(_printingHd.TotalForex);
                        //_changeAmount += (_printingHd.TotalForex == null) ? 0 : Convert.ToDecimal(_printingHd.TotalForex);
                        //_changeDueAmount += (_printingHd.TotalForex == null) ? 0 : Convert.ToDecimal(_printingHd.TotalForex);

                        this.CashierHiddenField.Value = _printingHd.UserPrep;

                        if (this.TransactionHiddenField.Value == "")
                        {
                            this.TransactionHiddenField.Value = _printingHd.TransNmbr + "," + _printingHd.TransType + "," + _printingHd.ReferenceNo;
                        }
                        else
                        {
                            this.TransactionHiddenField.Value += "|" + _printingHd.TransNmbr + "," + _printingHd.TransType + "," + _printingHd.ReferenceNo;
                        }
                    }

                    else if (_data.TransType.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Photocopy).Trim().ToLower())
                    {
                        POSTrPhotocopyHd _photocopyHd = this._photocopyBL.GetSinglePOSTrPhotocopyHd(_data.TransNmbr);

                        _discount += (_photocopyHd.DiscForex == null) ? 0 : Convert.ToDecimal(_photocopyHd.DiscForex);
                        _tax += (_photocopyHd.PPNForex == null) ? 0 : Convert.ToDecimal(_photocopyHd.PPNForex);
                        _pb1 += (_photocopyHd.PB1Forex == null) ? 0 : Convert.ToDecimal(_photocopyHd.PB1Forex);
                        _serviceCharge += (_photocopyHd.ServiceChargeAmount == null) ? 0 : Convert.ToDecimal(_photocopyHd.ServiceChargeAmount);
                        //_subTotal += (_photocopyHd.TotalForex == null) ? 0 : Convert.ToDecimal(_photocopyHd.TotalForex);
                        _subTotal += (_photocopyHd.SubTotalForex == null) ? 0 : Convert.ToDecimal(_photocopyHd.SubTotalForex);
                        //_dpReceive += (_photocopyHd.DPForex == null) ? 0 : Convert.ToDecimal(_photocopyHd.DPForex);
                        _dpReceive += (_photocopyHd.DPPaid == null) ? 0 : Convert.ToDecimal(_photocopyHd.DPPaid);
                        _otherFee += (_photocopyHd.OtherForex == null) ? 0 : Convert.ToDecimal(_photocopyHd.OtherForex);
                        _grandTotal += (_photocopyHd.TotalForex == null) ? 0 : Convert.ToDecimal(_photocopyHd.TotalForex);
                        //_changeAmount += (_photocopyHd.TotalForex == null) ? 0 : Convert.ToDecimal(_photocopyHd.TotalForex);
                        //_changeDueAmount += (_photocopyHd.TotalForex == null) ? 0 : Convert.ToDecimal(_photocopyHd.TotalForex);

                        this.CashierHiddenField.Value = _photocopyHd.UserPrep;

                        if (this.TransactionHiddenField.Value == "")
                        {
                            this.TransactionHiddenField.Value = _photocopyHd.TransNmbr + "," + _photocopyHd.TransType + "," + _photocopyHd.ReferenceNo;
                        }
                        else
                        {
                            this.TransactionHiddenField.Value += "|" + _photocopyHd.TransNmbr + "," + _photocopyHd.TransType + "," + _photocopyHd.ReferenceNo;
                        }
                    }

                    else if (_data.TransType.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Graphic).Trim().ToLower())
                    {
                        POSTrGraphicHd _graphicHd = this._graphicBL.GetSinglePOSTrGraphicHd(_data.TransNmbr);

                        _discount += (_graphicHd.DiscForex == null) ? 0 : Convert.ToDecimal(_graphicHd.DiscForex);
                        _tax += (_graphicHd.PPNForex == null) ? 0 : Convert.ToDecimal(_graphicHd.PPNForex);
                        _pb1 += (_graphicHd.PB1Forex == null) ? 0 : Convert.ToDecimal(_graphicHd.PB1Forex);
                        _serviceCharge += (_graphicHd.ServiceChargeAmount == null) ? 0 : Convert.ToDecimal(_graphicHd.ServiceChargeAmount);
                        //_subTotal += (_graphicHd.TotalForex == null) ? 0 : Convert.ToDecimal(_graphicHd.TotalForex);
                        _subTotal += (_graphicHd.SubTotalForex == null) ? 0 : Convert.ToDecimal(_graphicHd.SubTotalForex);
                        //_dpReceive += (_graphicHd.DPForex == null) ? 0 : Convert.ToDecimal(_graphicHd.DPForex);
                        _dpReceive += (_graphicHd.DPPaid == null) ? 0 : Convert.ToDecimal(_graphicHd.DPPaid);
                        _otherFee += (_graphicHd.OtherForex == null) ? 0 : Convert.ToDecimal(_graphicHd.OtherForex);
                        _grandTotal += (_graphicHd.TotalForex == null) ? 0 : Convert.ToDecimal(_graphicHd.TotalForex);
                        //_changeAmount += (_graphicHd.TotalForex == null) ? 0 : Convert.ToDecimal(_graphicHd.TotalForex);
                        //_changeDueAmount += (_graphicHd.TotalForex == null) ? 0 : Convert.ToDecimal(_graphicHd.TotalForex);

                        this.CashierHiddenField.Value = _graphicHd.UserPrep;

                        if (this.TransactionHiddenField.Value == "")
                        {
                            this.TransactionHiddenField.Value = _graphicHd.TransNmbr + "," + _graphicHd.TransType + "," + _graphicHd.ReferenceNo;
                        }
                        else
                        {
                            this.TransactionHiddenField.Value += "|" + _graphicHd.TransNmbr + "," + _graphicHd.TransType + "," + _graphicHd.ReferenceNo;
                        }
                    }
                    else if (_data.TransType.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Shipping).Trim().ToLower())
                    {
                        POSTrShippingHd _shippingHd = this._shippingBL.GetSinglePOSTrShippingHd(_data.TransNmbr);

                        _discount += (_shippingHd.DiscForex == null) ? 0 : Convert.ToDecimal(_shippingHd.DiscForex);
                        _tax += (_shippingHd.PPNForex == null) ? 0 : Convert.ToDecimal(_shippingHd.PPNForex);
                        _pb1 += (_shippingHd.PB1Forex == null) ? 0 : Convert.ToDecimal(_shippingHd.PB1Forex);
                        _serviceCharge += (_shippingHd.ServiceChargeAmount == null) ? 0 : Convert.ToDecimal(_shippingHd.ServiceChargeAmount);
                        _subTotal += (_shippingHd.SubTotalForex == null) ? 0 : Convert.ToDecimal(_shippingHd.SubTotalForex);
                        _dpReceive += (_shippingHd.DPPaid == null) ? 0 : Convert.ToDecimal(_shippingHd.DPPaid);
                        _otherFee += (_shippingHd.OtherForex == null) ? 0 : Convert.ToDecimal(_shippingHd.OtherForex);
                        _grandTotal += (_shippingHd.TotalForex == null) ? 0 : Convert.ToDecimal(_shippingHd.TotalForex);

                        this.CashierHiddenField.Value = _shippingHd.UserPrep;

                        if (this.TransactionHiddenField.Value == "")
                        {
                            this.TransactionHiddenField.Value = _shippingHd.TransNmbr + "," + _shippingHd.TransType + "," + _shippingHd.ReferenceNo;
                        }
                        else
                        {
                            this.TransactionHiddenField.Value += "|" + _shippingHd.TransNmbr + "," + _shippingHd.TransType + "," + _shippingHd.ReferenceNo;
                        }
                    }
                }

                //assign value2 to label
                //this.DiscountLiteral.Text = _discount.ToString("#,##0.00");
                //this.TaxLiteral.Text = (_tax + _pb1).ToString("#,##0.00");
                //this.TaxHiddenField.Value = _tax.ToString();
                //this.pb1HiddenField.Value = _pb1.ToString();
                //this.ServiceChargeLiteral.Text = _serviceCharge.ToString("#,##0.00");
                //this.SubtotalLiteral.Text = _subTotal.ToString("#,##0.00");
                //this.DPReceivedLiteral.Text = _dpReceive.ToString("#,##0.00");
                //this.OtherFeeLiteral.Text = _otherFee.ToString("#,##0.00");
                //this.TotalLiteral.Text = _grandTotal.ToString("#,##0.00");

                //this.CashPaymentLiteral.Text = _cashPaymentAmount.ToString("#,##0.00");
                //this.CreditCardLiteral.Text = _creditCardPaymentAmount.ToString("#,##0.00");
                //this.DebitCardLiteral.Text = _debitCardPaymentAmount.ToString("#,##0.00");
                //this.VoucherLiteral.Text = _giftVoucherAmount.ToString("#,##0.00");
                //this.ChangeLiteral.Text = _changeAmount.ToString("#,##0.00");
                //this.ChangeDueLiteral.Text = _changeDueAmount.ToString("#,##0.00");

                this.DiscountLiteral.Text = _discount.ToString("#,##0");
                this.TaxLiteral.Text = (_tax + _pb1).ToString("#,##0");
                this.TaxHiddenField.Value = _tax.ToString("#,##0");
                this.pb1HiddenField.Value = _pb1.ToString("#,##0");
                this.ServiceChargeLiteral.Text = _serviceCharge.ToString("#,##0");
                this.SubtotalLiteral.Text = _subTotal.ToString("#,##0");
                this.DPReceivedLiteral.Text = _dpReceive.ToString("#,##0");
                this.OtherFeeLiteral.Text = _otherFee.ToString("#,##0");
                this.OtherFeeLiteral.Text = _otherFee.ToString("#,##0");
                //pembulatan setelah discon
                //CompanyConfiguration _companyConfiguration = this._pOSConfigurationBL.GetSingle("POSRounding");
                //Int32 POSRound = Convert.ToInt32(_companyConfiguration.SetValue);
                //Decimal Round = _grandTotal % POSRound;
                //this.RoundLiteral.Text = Round.ToString("#,##0");
                //this.TotalLiteral.Text = (_grandTotal - Round - _dpReceive).ToString("#,##0");
                this.RoundLiteral.Text = _round.ToString("#,##0");
                this.TotalLiteral.Text = (_grandTotal - _round - _dpReceive).ToString("#,##0");

                this.CashPaymentLiteral.Text = _cashPaymentAmount.ToString("#,##0");
                this.CreditCardLiteral.Text = _creditCardPaymentAmount.ToString("#,##0");
                this.DebitCardLiteral.Text = _debitCardPaymentAmount.ToString("#,##0");
                this.VoucherLiteral.Text = _giftVoucherAmount.ToString("#,##0");
                this.ChangeLiteral.Text = _changeAmount.ToString("#,##0");
                this.ChangeDueLiteral.Text = _changeDueAmount.ToString("#,##0");
                //this.BankChargeLiteral.Text = _changeDueAmount.ToString("#,##0");

                this.AmountPaymentTextBox.Focus();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void CashButton_Click(object sender, ImageClickEventArgs e)
        {
            this.WarningLabel.Text = "";
            this.GetDiscountPromo(POSSettlementButtonType.Cash, "");
            this.ShowPanel(POSSettlementButtonType.Cash);
            this.AmountPaymentTextBox.Focus();
            this.ShowPanel();
        }

        protected void CreditCardButton_Click(object sender, ImageClickEventArgs e)
        {
            decimal _totalPayed = Convert.ToDecimal(this.CashPaymentLiteral.Text) + Convert.ToDecimal(this.CreditCardLiteral.Text) + Convert.ToDecimal(this.DebitCardLiteral.Text) + Convert.ToDecimal(this.VoucherLiteral.Text);
            if (_totalPayed <= 0 || _totalPayed < Convert.ToDecimal(this.TotalLiteral.Text))
            {
                this.WarningLabel.Text = "";
                this.ShowPanel(POSSettlementButtonType.CreditCard);
                this.ShowListCardType();
                this.CreditCardReferenceTextBox.Focus();
            }
            this.ShowPanel();
        }

        protected void DebitCardButton_Click(object sender, ImageClickEventArgs e)
        {

            decimal _totalPayed = Convert.ToDecimal(this.CashPaymentLiteral.Text) + Convert.ToDecimal(this.CreditCardLiteral.Text) + Convert.ToDecimal(this.DebitCardLiteral.Text) + Convert.ToDecimal(this.VoucherLiteral.Text);
            if (_totalPayed <= 0 || _totalPayed < Convert.ToDecimal(this.TotalLiteral.Text))
            {
                this.WarningLabel.Text = "";
                this.ShowPanel(POSSettlementButtonType.Debit);
                this.ShowDebitCard();
                this.DebitCardReferenceTextBox.Focus();
            }
            this.ShowPanel();
        }

        protected void VoucherButton_Click(object sender, ImageClickEventArgs e)
        {

            this.GetDiscountPromo(POSSettlementButtonType.Voucher, "");
            decimal _totalPayed = Convert.ToDecimal(this.CashPaymentLiteral.Text) + Convert.ToDecimal(this.CreditCardLiteral.Text) + Convert.ToDecimal(this.DebitCardLiteral.Text) + Convert.ToDecimal(this.VoucherLiteral.Text);
            if (_totalPayed <= 0 || _totalPayed < Convert.ToDecimal(this.TotalLiteral.Text))
            {
                this.WarningLabel.Text = "";
                this.ShowPanel(POSSettlementButtonType.Voucher);
                this.VoucherNoTextBox.Focus();
            }
            this.ShowPanel();
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._cashierPage);
            //String[] _transaction = this.TransactionHiddenField.Value.Split('|');
            //String[] _transType = _transaction[0].Split(',');

            //String _floor = "";
            //if (_transType[1].ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe).Trim().ToLower())
            //{
            //    _floor = _internetBL.GetSinglePOSMsInternetFloor("Cafe");
            //    Response.Redirect("../Cafe/POSCafe.aspx" + "?" + "selectedFloor=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_floor, ApplicationConfig.EncryptionKey)));
            //}
            //else if (_transType[1].ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Internet).Trim().ToLower())
            //{
            //    _floor = _internetBL.GetSinglePOSMsInternetFloor("Internet");
            //    Response.Redirect("../Internet/POSInternet.aspx" + "?" + "selectedFloor=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_floor, ApplicationConfig.EncryptionKey)));
            //}
            //else
            //{
            //    Response.Redirect(this._cashierPage);
            //}
        }

        protected void btnOKPanelNumber_Click(object sender, EventArgs e)
        {
            if (this.AmountPaymentTextBox.Text == "")
                this.AmountPaymentTextBox.Text = "0";

            if (Convert.ToInt64(this.AmountPaymentTextBox.Text) > 0)
            {
                //this.GetDiscountPromo(POSSettlementButtonType.Cash, "");
                Decimal _cash = Convert.ToDecimal(this.AmountPaymentTextBox.Text);
                //Decimal _change = 0;
                //Decimal _credit = 0;
                //Decimal _debit = 0;
                //Decimal _voucher = 0;
                //this.CashPaymentLiteral.Text = _cash.ToString("#,##0.00");
                this.CashPaymentLiteral.Text = _cash.ToString("#,##0");
                this.CheckDiscountPromo();
                //this.CreditCardLiteral.Text = _credit.ToString("#,##0.00");
                //this.DebitCardLiteral.Text = _debit.ToString("#,##0.00");
                //this.VoucherLiteral.Text = _voucher.ToString("#,##0.00");

                Decimal _change = 0;
                Decimal _total = Convert.ToDecimal(this.TotalLiteral.Text);
                Decimal _credit = Convert.ToDecimal(this.CreditCardLiteral.Text);
                Decimal _debit = Convert.ToDecimal(this.DebitCardLiteral.Text);
                Decimal _voucher = Convert.ToDecimal(this.VoucherLiteral.Text);

                //Decimal _change = (_cash + Convert.ToDecimal(this.DebitCardLiteral.Text) + Convert.ToDecimal(this.CreditCardLiteral.Text)) - Convert.ToDecimal(this.TotalLiteral.Text);

                _change = (_cash + _credit + _debit + _voucher) - _total;

                if (_cash + _credit + _debit + _voucher >= _total)
                {
                    if (_voucher > _total)
                    {
                        //_cash = 0;
                        //this.CashPaymentLiteral.Text = _cash.ToString("#,##0.00");
                        this.CashPaymentLiteral.Text = _cash.ToString("#,##0");
                        _change = _cash;
                    }
                }
                //this.ChangeLiteral.Text = _change.ToString("#,##0.00");
                //this.ChangeDueLiteral.Text = _change.ToString("#,##0.00");
                this.ChangeLiteral.Text = _change.ToString("#,##0");
                this.ChangeDueLiteral.Text = _change.ToString("#,##0");

                this.AmountPaymentTextBox.Text = "0";
            }
            else
            {
                //this.WarningLabel.Text = "Input Your Nominal Correctly";
            }

        }

        private void ShowListCardType()
        {
            try
            {
                this.CardTypeRepeater.DataSource = this._cardTypeBL.GetList();
                this.CardTypeRepeater.DataBind();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        private void ShowListCreditCard(String _prmCardType)
        {
            try
            {
                this.CreditCardButtonRepeater.DataSource = this._creditCardBL.GetList(_prmCardType);
                this.CreditCardButtonRepeater.DataBind();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        private void ShowDebitCard()
        {
            try
            {
                this.DebitCardRepeater.DataSource = this._debitCardBL.GetList();
                this.DebitCardRepeater.DataBind();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        //private void disableBtnOKPanelNumber()
        //{
        //    if (this.CashPaymentLiteral.Text != "" || this.CreditCardLiteral.Text != "" || this.DebitCardLiteral.Text != "" || this.VoucherLiteral.Text != "")
        //    {
        //        this.btnOKPanelNumber.OnClientClick = false.ToString;
        //        this.btnOKPanelNumberVoucher.OnClientClick = false.ToString;
        //    }
        //}

        protected void btnOKPanelNumberCredit_Click(object sender, EventArgs e)
        {
        }

        protected void btnOKPanelNumberDebit_Click(object sender, EventArgs e)
        {
        }

        protected void btnOKPanelNumberVoucher_Click(object sender, EventArgs e)
        {
            if (this.VoucherNoTextBox.Text != "")
            {
                if (this.VoucherNominalTextBox.Text == "")
                    this.VoucherNominalTextBox.Text = "0";
                if (Convert.ToDecimal(this.VoucherNominalTextBox.Text) > 0)
                {
                    Decimal _voucherValue = Convert.ToDecimal(this.VoucherNominalTextBox.Text);
                    //this.VoucherLiteral.Text = _voucherValue.ToString("#,##0.00");
                    this.VoucherLiteral.Text = _voucherValue.ToString("#,##0");
                    this.CheckDiscountPromo();

                    Decimal _change = 0;
                    Decimal _total = Convert.ToDecimal(this.TotalLiteral.Text);
                    Decimal _cash = Convert.ToDecimal(this.CashPaymentLiteral.Text);
                    Decimal _credit = Convert.ToDecimal(this.CreditCardLiteral.Text);
                    Decimal _debit = Convert.ToDecimal(this.DebitCardLiteral.Text);
                    Decimal _voucher = Convert.ToDecimal(this.VoucherLiteral.Text);

                    _change = (_cash + _credit + _debit + _voucher) - _total;

                    if (_cash + _credit + _debit + _voucher >= _total)
                    {
                        if (_voucher > _total)
                        {
                            _cash = 0;
                            //this.CashPaymentLiteral.Text = _cash.ToString("#,##0.00");
                            this.CashPaymentLiteral.Text = _cash.ToString("#,##0");
                            _change = 0;
                        }
                    }

                    //this.ChangeLiteral.Text = _change.ToString("#,##0.00");
                    //this.ChangeDueLiteral.Text = _change.ToString("#,##0.00");
                    this.ChangeLiteral.Text = _change.ToString("#,##0");
                    this.ChangeDueLiteral.Text = _change.ToString("#,##0");

                    this.VoucherNoHiddenField.Value = "";
                    this.VoucherNoHiddenField.Value = this.VoucherNoTextBox.Text;

                    this.ShowPanel(POSSettlementButtonType.Cash);
                    this.AmountPaymentTextBox.Focus();
                }
                else
                {
                    this.WarningLabel.Text = "Voucher Nominal Not Valid";
                }
            }
            else
            {
                this.WarningLabel.Text = "Voucher No Not Valid";
            }
        }

        protected void cekDO()
        {
            //if (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._settleType), ApplicationConfig.EncryptionKey).Substring(0, 2) == "DO" + POSTrSettlementDataMapper.GetSettleType(POSTrSettleType.Paid))
            if (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._settleType), ApplicationConfig.EncryptionKey).Substring(0, 2) == "DO")
            {
                this.SettlementType.Text = "SETTLEMENT";
                _prmDO = true;
            }
        }

        protected void PayImageButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                String _newSettleType = "";
                this.cekDO();
                if (_prmDO == true)
                {
                    _newSettleType = POSTrSettlementDataMapper.GetSettleType(POSTrSettleType.Paid);
                }
                else
                {
                    _newSettleType = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._settleType), ApplicationConfig.EncryptionKey);
                }
                if (_newSettleType == POSTrSettlementDataMapper.GetSettleType(POSTrSettleType.Paid) && Convert.ToDecimal(this.ChangeLiteral.Text) >= 0 || _newSettleType == POSTrSettlementDataMapper.GetSettleType(POSTrSettleType.DP) && Convert.ToDecimal(this.ChangeLiteral.Text) < 0)
                {
                    POSTrSettlementHd _posTrSettlementHd = new POSTrSettlementHd();
                    _posTrSettlementHd.TransDate = DateTime.Now;
                    _posTrSettlementHd.Status = POSTrSettlementDataMapper.GetStatus(POSTrSettlementStatus.OnHold);

                    if (_newSettleType == POSTrSettlementDataMapper.GetSettleType(POSTrSettleType.Paid))
                    {
                        _posTrSettlementHd.SettleType = POSTrSettlementDataMapper.GetSettleType(POSTrSettleType.Paid);
                    }
                    if (_newSettleType == POSTrSettlementDataMapper.GetSettleType(POSTrSettleType.DP))
                    {
                        _posTrSettlementHd.SettleType = POSTrSettlementDataMapper.GetSettleType(POSTrSettleType.DP);
                    }

                    _posTrSettlementHd.CashierID = HttpContext.Current.User.Identity.Name;
                    _posTrSettlementHd.CurrCode = this._currBL.GetCurrDefault();
                    _posTrSettlementHd.ForexRate = 1;
                    _posTrSettlementHd.SubTotalForex = Convert.ToDecimal(this.SubtotalLiteral.Text);
                    _posTrSettlementHd.DiscPercentage = 0;
                    _posTrSettlementHd.DiscForex = Convert.ToDecimal(this.DiscountLiteral.Text);
                    //int _tax = Convert.ToInt32(new CompanyConfig().GetSingle(CompanyConfigure.POSTaxCharge).SetValue);
                    _posTrSettlementHd.PPNPercentage = 0;
                    _posTrSettlementHd.PPNForex = Convert.ToDecimal(this.TaxHiddenField.Value);//Convert.ToDecimal(this.TaxLiteral.Text);
                    _posTrSettlementHd.PB1Forex = Convert.ToDecimal(this.pb1HiddenField.Value);
                    _posTrSettlementHd.OtherForex = Convert.ToDecimal(this.OtherFeeLiteral.Text);
                    _posTrSettlementHd.BankCharge = Convert.ToDecimal(this.BankChargeLiteral.Text);
                    _posTrSettlementHd.TotalForex = Convert.ToDecimal(this.TotalLiteral.Text);
                    //_posTrSettlementHd.Remark = "";
                    _posTrSettlementHd.Remark = this.DiscountHiddenField.Value;
                    _posTrSettlementHd.FakturPajakNmbr = "";
                    _posTrSettlementHd.FakturPajakDate = DateTime.Now;
                    _posTrSettlementHd.FakturPajakRate = 0;
                    _posTrSettlementHd.ServiceChargesForex = Convert.ToDecimal(this.ServiceChargeLiteral.Text);
                    _posTrSettlementHd.DownPaymentForex = Convert.ToDecimal(this.DPReceivedLiteral.Text);
                    _posTrSettlementHd.TotalCashPayment = Convert.ToDecimal(this.CashPaymentLiteral.Text);
                    _posTrSettlementHd.TotalDebitCardPayment = Convert.ToDecimal(this.DebitCardLiteral.Text);
                    _posTrSettlementHd.TotalCreditCardPayment = Convert.ToDecimal(this.CreditCardLiteral.Text);
                    _posTrSettlementHd.TotalVoucherPayment = Convert.ToDecimal(this.VoucherLiteral.Text);
                    _posTrSettlementHd.ChangeAmountForex = Convert.ToDecimal(this.ChangeLiteral.Text);
                    _posTrSettlementHd.UserPrep = this.CashierHiddenField.Value;
                    _posTrSettlementHd.DatePrep = DateTime.Now;
                    _posTrSettlementHd.UserAppr = this.CashierHiddenField.Value;
                    _posTrSettlementHd.DateAppr = DateTime.Now;
                    _posTrSettlementHd.Rounding = Convert.ToDecimal(this.RoundLiteral.Text);

                    DateTime _now = DateTime.Now;
                    List<POSTrSettlementDtRefTransaction> _listPOSSettlementRefTrans = new List<POSTrSettlementDtRefTransaction>();
                    List<POSTableStatusHistory> _listPOSTableHist = new List<POSTableStatusHistory>();
                    List<String> _listHistoryUpdate = new List<String>(); //_oldID, _itemDuration(accum)

                    if (this.TransactionHiddenField.Value != "")
                    {
                        String[] _transaction = this.TransactionHiddenField.Value.Split('|');

                        int _tableHistoryID = this._tableHistBL.GetNewID();
                        //int _tableHistoryIDForCafe = this._tableHistBL.GetNewIDCafe();
                        foreach (var _item in _transaction)
                        {
                            String[] _data = _item.Split(',');
                            POSTrSettlementDtRefTransaction _settlement = new POSTrSettlementDtRefTransaction();
                            _settlement.TransNmbr = _posTrSettlementHd.TransNmbr;
                            _settlement.TransType = _data[1];
                            _settlement.ReferenceNmbr = _data[0];

                            _listPOSSettlementRefTrans.Add(_settlement);

                            //if (_data[1] == POSTransTypeDataMapper.GetTransType(POSTransType.Retail))
                            //{
                            //    //POSTrRetailHd _retailHd = this.db.POSTrRetailHds.Single(_temp => _temp.TransNmbr == _data[0]);
                            //    //_retailHd.Status = POSTrRetailDataMapper.GetStatus(POSTrRetailStatus.Posted);
                            //}
                            //else 
                            if (_data[1] == POSTransTypeDataMapper.GetTransType(POSTransType.Internet))
                            {
                                POSTrInternetHd _inetHd = this._internetBL.GetSinglePOSTrInternetHd(_data[0]);

                                int _minuteDuration = 0;
                                _minuteDuration = this._internetBL.GetItemDurationPerInternet(_inetHd.TransNmbr);

                                POSTableStatusHistory _tableStatusHist = null;
                                String _existID = "";
                                Boolean _isHistoryExist = this._tableHistBL.IsTableHistoryExist(_data[1], Convert.ToInt32(_inetHd.FloorNmbr), Convert.ToInt32(_inetHd.TableNmbr), _now, ref _existID);
                                if (_isHistoryExist == false)
                                {
                                    _tableStatusHist = new POSTableStatusHistory();
                                    _tableStatusHist.ID = _tableHistoryID;
                                    _tableStatusHist.FloorNmbr = Convert.ToInt32(_inetHd.FloorNmbr);
                                    _tableStatusHist.FloorType = POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet);
                                    _tableStatusHist.TableID = Convert.ToInt32(_inetHd.TableNmbr);
                                    _tableStatusHist.StartTime = _now;
                                    _tableStatusHist.EndTime = _now.AddMinutes(_minuteDuration);
                                    _tableStatusHist.Status = 2;
                                    _tableStatusHist.StillActive = true;

                                    _listPOSTableHist.Add(_tableStatusHist);
                                    _tableHistoryID += 1;
                                }
                                else
                                {
                                    List<String> _listHistoryUpdateAdd = new List<String>();
                                    List<String> _listHistoryUpdateRemove = new List<String>();

                                    if (_listHistoryUpdate.Count == 0)
                                    {
                                        //if masih kosong
                                        _listHistoryUpdateAdd.Add(_existID.ToString() + "," + _minuteDuration);
                                    }
                                    else
                                    {
                                        //cek data ID, bila ada yang sama, itemduration ditambah, bila belum add baru ke list
                                        foreach (var _row in _listHistoryUpdate)
                                        {
                                            String[] _dataField = _row.Split(',');

                                            if (_dataField[0] == _existID)
                                            {
                                                _minuteDuration = (Convert.ToInt32(_dataField[1]) + _minuteDuration);
                                                _listHistoryUpdateRemove.Add(_dataField[0] + "," + _dataField[1]);
                                            }

                                            _listHistoryUpdateAdd.Add(_existID.ToString() + "," + _minuteDuration);
                                        }
                                    }

                                    //update list history table yang akan diupdate
                                    foreach (var _itemRow in _listHistoryUpdateAdd)
                                    {
                                        _listHistoryUpdate.Add(_itemRow);
                                    }
                                    foreach (var _itemRow in _listHistoryUpdateRemove)
                                    {
                                        if (_listHistoryUpdate.Exists(_temp => _temp == _itemRow))
                                        {
                                            _listHistoryUpdate.Remove(_itemRow);
                                        }
                                    }
                                }
                            }
                            else if (_data[1] == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe))
                            {
                                int _minuteDuration = 0;
                                //_minuteDuration = this._cafeBL.GetItemDurationPerCafe(_inetHd.TransNmbr);
                                CompanyConfiguration _companyConfiguration = this._pOSConfigurationBL.GetSingle("POSCafeTimeLimit");
                                _minuteDuration = Convert.ToInt16(_companyConfiguration.SetValue);

                                POSTrCafeHd _inetHd = this._cafeBL.GetSinglePOSTrCafeHd(_data[0]);

                                String _tableJoin = "";
                                if (_inetHd.TableExtension == null)
                                {
                                    _tableJoin = Convert.ToString(_inetHd.TableNmbr);
                                }
                                else
                                {
                                    _tableJoin = _inetHd.TableExtension;
                                }

                                if (_tableJoin != "")
                                {
                                    String[] _splitTable = _tableJoin.Split(',');
                                    int num = 0;
                                    foreach (var _itemSplit in _splitTable)
                                    {
                                        String _tableNmbr = _splitTable[num];
                                        num++;
                                        POSTableStatusHistory _tableStatusHist = null;
                                        String _existID = "";
                                        Boolean _isHistoryExist = this._tableHistBL.IsTableHistoryExist(_data[1], Convert.ToInt32(_inetHd.FloorNmbr), Convert.ToInt32(_tableNmbr), _now, ref _existID);
                                        if (_isHistoryExist == false)
                                        {
                                            _tableStatusHist = new POSTableStatusHistory();
                                            _tableStatusHist.ID = _tableHistoryID;
                                            _tableStatusHist.FloorNmbr = Convert.ToInt32(_inetHd.FloorNmbr);
                                            _tableStatusHist.FloorType = POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe);
                                            _tableStatusHist.TableID = Convert.ToInt32(_tableNmbr);
                                            _tableStatusHist.StartTime = _now;
                                            _tableStatusHist.EndTime = _now.AddMinutes(_minuteDuration);
                                            _tableStatusHist.Status = 2;
                                            _tableStatusHist.StillActive = true;

                                            _listPOSTableHist.Add(_tableStatusHist);
                                            _tableHistoryID += 1;
                                        }
                                        else
                                        {
                                            List<String> _listHistoryUpdateAdd = new List<String>();
                                            List<String> _listHistoryUpdateRemove = new List<String>();

                                            if (_listHistoryUpdate.Count == 0)
                                            {
                                                _listHistoryUpdateAdd.Add(_existID.ToString() + "," + _minuteDuration);
                                            }
                                            else
                                            {
                                                //cek data ID, bila ada yang sama, itemduration ditambah, bila belum add baru ke list
                                                foreach (var _row in _listHistoryUpdate)
                                                {
                                                    String[] _dataField = _row.Split(',');

                                                    if (_dataField[0] == _existID)
                                                    {
                                                        _minuteDuration = (Convert.ToInt32(_dataField[1]) + _minuteDuration);
                                                        _listHistoryUpdateRemove.Add(_dataField[0] + "," + _dataField[1]);
                                                    }

                                                    _listHistoryUpdateAdd.Add(_existID.ToString() + "," + _minuteDuration);
                                                }
                                            }

                                            //update list history table yang akan diupdate
                                            foreach (var _itemRow in _listHistoryUpdateAdd)
                                            {
                                                _listHistoryUpdate.Add(_itemRow);
                                            }
                                            foreach (var _itemRow in _listHistoryUpdateRemove)
                                            {
                                                if (_listHistoryUpdate.Exists(_temp => _temp == _itemRow))
                                                {
                                                    _listHistoryUpdate.Remove(_itemRow);
                                                }
                                            }
                                        }
                                    }
                                }
                                //POSTableStatusHistory _tableStatusHist = null;
                                //String _existID = "";
                                //Boolean _isHistoryExist = this._tableHistBL.IsTableHistoryExist(Convert.ToInt32(_inetHd.FloorNmbr), Convert.ToInt32(_inetHd.TableNmbr), _now, ref _existID);
                                //if (_isHistoryExist == false)
                                //{
                                //    _tableStatusHist = new POSTableStatusHistory();
                                //    _tableStatusHist.ID = _tableHistoryID;
                                //    _tableStatusHist.FloorNmbr = Convert.ToInt32(_inetHd.FloorNmbr);
                                //    _tableStatusHist.FloorType = POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe);
                                //    _tableStatusHist.TableID = Convert.ToInt32(_inetHd.TableNmbr);
                                //    _tableStatusHist.StartTime = _now;
                                //    _tableStatusHist.EndTime = _now.AddMinutes(_minuteDuration);
                                //    _tableStatusHist.Status = 2;
                                //    _tableStatusHist.StillActive = true;

                                //    _listPOSTableHist.Add(_tableStatusHist);
                                //    _tableHistoryID += 1;
                                //}
                                //else
                                //{
                                //    List<String> _listHistoryUpdateAdd = new List<String>();
                                //    List<String> _listHistoryUpdateRemove = new List<String>();

                                //    if (_listHistoryUpdate.Count == 0)
                                //    {
                                //        _listHistoryUpdateAdd.Add(_existID.ToString() + "," + _minuteDuration);
                                //    }
                                //    else
                                //    {
                                //        //cek data ID, bila ada yang sama, itemduration ditambah, bila belum add baru ke list
                                //        foreach (var _row in _listHistoryUpdate)
                                //        {
                                //            String[] _dataField = _row.Split(',');

                                //            if (_dataField[0] == _existID)
                                //            {
                                //                _minuteDuration = (Convert.ToInt32(_dataField[1]) + _minuteDuration);
                                //                _listHistoryUpdateRemove.Add(_dataField[0] + "," + _dataField[1]);
                                //            }

                                //            _listHistoryUpdateAdd.Add(_existID.ToString() + "," + _minuteDuration);
                                //        }
                                //    }

                                //    //update list history table yang akan diupdate
                                //    foreach (var _itemRow in _listHistoryUpdateAdd)
                                //    {
                                //        _listHistoryUpdate.Add(_itemRow);
                                //    }
                                //    foreach (var _itemRow in _listHistoryUpdateRemove)
                                //    {
                                //        if (_listHistoryUpdate.Exists(_temp => _temp == _itemRow))
                                //        {
                                //            _listHistoryUpdate.Remove(_itemRow);
                                //        }
                                //    }
                                //}
                            }
                        }
                    }

                    //cari account Cashier klo tidak exist ambil dari MsSetup
                    String _account = this._cashierBL.GetAccountCashierByCashierID(_posTrSettlementHd.CashierID);
                    if (_account == "")
                    {
                        _account = new SetupBL().GetSingle("Cash").SetValue;
                    }

                    AccountBL _accountBL = new AccountBL();
                    String _creditCardAkum = "";
                    String _debitCardAkum = "";
                    if (this.CreditCardHiddenField.Value != "")
                    {
                        String[] _creditCardValue = this.CreditCardHiddenField.Value.Split(',');
                        //String _credit = "";
                        //_credit = _creditCardValue[1];
                        //String _credit = _creditCardBL.GetCreditCardType(_creditCardValue[0]);

                        String _credit = _creditCardValue[1];
                        String _accountTemp = _creditCardBL.GetSingle(_creditCardValue[1]).Account;
                        _credit += "," + _account;
                        _credit += "," + _creditCardValue[2];
                        _credit += "," + _accountBL.GetAccountSubLed(_account);
                        _creditCardAkum = _credit;
                        //_settlePayType.CardNumber = _creditCard[2];
                    }
                    if (this.DebitHiddenField.Value != "")
                    {
                        //String _debitCardValue = this.DebitHiddenField.Value;
                        String[] _debitCardValue = this.DebitHiddenField.Value.Split(',');
                        //String _debit = "";
                        //_debit = _debitCardValue;
                        String _debit = this._debitCardBL.GetMemberNameByCode(_debitCardValue[0]);
                        String _accountTemp = _debitCardBL.GetSingle(_debitCardValue[0]).Account;
                        _debit += "," + _account;
                        _debit += "," + _accountBL.GetAccountSubLed(_account);
                        _debit += "," + _debitCardValue[1];
                        _debitCardAkum = _debit;
                    }

                    //old//String _result = this._cashierBL.SettlementPosting(_posTrSettlementHd, this.TransactionHiddenField.Value, this.CreditCardHiddenField.Value, this.DebitHiddenField.Value, this.VoucherNoHiddenField.Value);
                    //String _result = this._cashierBL.SettlementPosting(_posTrSettlementHd, this.TransactionHiddenField.Value, _creditCardAkum, _debitCardAkum, this.VoucherNoHiddenField.Value, _listPOSSettlementRefTrans, _listPOSTableHist, _account, _listHistoryUpdate, _newSettleType);
                    String _typeRef = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._settleType), ApplicationConfig.EncryptionKey);
                    String _result = this._cashierBL.SettlementPosting(_posTrSettlementHd, this.TransactionHiddenField.Value, _creditCardAkum, _debitCardAkum, this.VoucherNoHiddenField.Value, _listPOSSettlementRefTrans, _listPOSTableHist, _account, _listHistoryUpdate, _newSettleType, _prmDO, _typeRef);

                    //String[] _resultSplit = _result.Split('|');
                    this.ProductPromoPanel.Visible = false;
                    this.NumberInputPanel.Visible = true;
                    if (_result == "")
                    {
                        this.Print(_posTrSettlementHd.TransNmbr);
                        this.ClearLabel();
                        this.ClearData();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Payment Success');", true);
                        //this.BackButton_Click(null, null);
                    }
                    else
                    {
                        this.WarningLabelPay.Text = _result;
                    }
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        private int m_currentPageIndex;

        private IList<Stream> m_streams;

        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            //this.Label3.Text = "line 3";
            Stream stream = null;
            try
            {
                stream = new MemoryStream();
                //stream = new FileStream(Request.ServerVariables["APPL_PHYSICAL_PATH"] + name + "." + fileNameExtension, FileMode.Create);
                m_streams.Add(stream);
                //this.Label3.Text = Request.ServerVariables["APPL_PHYSICAL_PATH"] + name + "." + fileNameExtension;
            }
            catch (Exception Ex)
            {

                //this.Label4.Text = Ex.ToString();
            }

            //this.Label4.Text = "line 4";
            return stream;
        }

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);
            ev.Graphics.DrawImage(pageImage, ev.PageBounds);
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        public String GetDefaultPrinter()
        {
            //this.Label4.Text = "";
            PrinterSettings settings = new PrinterSettings();
            String printer;

            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                printer = PrinterSettings.InstalledPrinters[i];
                settings.PrinterName = printer;
                //this.Label4.Text = this.Label4.Text + " , " + i.ToString() + " " + settings.IsDefaultPrinter.ToString();
                //this.Label5.Text = printer;
                if (settings.IsDefaultPrinter)
                    return printer;
            }

            //this.Label4.Text = PrinterSettings.InstalledPrinters[0];
            //this.Label4.Text = PrinterSettings.InstalledPrinters[1];
            //this.Label4.Text = PrinterSettings.InstalledPrinters[2];
            //this.Label4.Text = PrinterSettings.InstalledPrinters[3];
            //this.Label4.Text = PrinterSettings.InstalledPrinters[0];

            //foreach (String printer in PrinterSettings.InstalledPrinters)
            //{
            //    settings.PrinterName = printer;
            //    this.Label4.Text = this.Label4.Text + " , " + settings.PrinterName;
            //    if (settings.IsDefaultPrinter)
            //        return printer;
            //}
            return string.Empty;
        }

        protected void Print(String _prmTransNmbr)
        {
            try
            {
                //List<POSTrSettlementDtRefTransaction> _refTrans = this._cashierBL.GetRefTrans(_prmTransNmbr);

                //foreach (var _item in _refTrans)
                //{
                String _companyAddress = this._companyBL.GetSingleDefault().PrimaryAddress;
                ReportDataSource _reportDataSource1 = new ReportDataSource();

                _reportDataSource1 = this._cashierBL.ReportSendToCustomer(_prmTransNmbr, _companyAddress);

                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

                String _reportPath = "General/ReportSendToCustomer.rdlc";
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath;

                ReportParameter[] _reportParam = new ReportParameter[2];
                _reportParam[0] = new ReportParameter("TransNmbr", _prmTransNmbr, false);
                _reportParam[1] = new ReportParameter("CompanyAddress", _companyAddress, false);
                //_reportParam[1] = new ReportParameter("TransType", _item.TransType, false);

                this.ReportViewer1.LocalReport.SetParameters(_reportParam);
                this.ReportViewer1.LocalReport.Refresh();

                LocalReport report = new LocalReport();
                report = this.ReportViewer1.LocalReport;

                try
                {
                    string deviceInfo =
                              "<DeviceInfo>" +
                              "  <OutputFormat>EMF</OutputFormat>" +
                              "  <PageWidth>4in</PageWidth>" +
                              "  <PageHeight>7.5in</PageHeight>" +
                              "  <MarginTop>0.0in</MarginTop>" +
                              "  <MarginLeft>0.0in</MarginLeft>" +
                              "  <MarginRight>0.0in</MarginRight>" +
                              "  <MarginBottom>0.0in</MarginBottom>" +
                              "</DeviceInfo>";

                    Warning[] warnings;
                    m_streams = new List<Stream>();

                    report.Render("Image", deviceInfo, CreateStream, out warnings);
                    foreach (Stream stream in m_streams)
                        stream.Position = 0;

                    m_currentPageIndex = 0;

                    //String printerName = this.GetDefaultPrinter();
                    //String _cashierPrinterCode = this._kitchenBL.GetSingleDt("0001").KitchenCode;
                    //POSMsKitchen _pOSMsKitchen = this._kitchenBL.GetSingle("0001");
                    //String printerName = "\\\\" + _pOSMsKitchen.KitchenPrinterIPAddress + "\\" + _pOSMsKitchen.KitchenPrinterName;

                    String hostIP = Request.ServerVariables["REMOTE_ADDR"];
                    String printerName = "";
                    //V_POSMsCashierPrinter _vPOSMsCashierPrinter = this._cashierPrinterBL.GetDefaultPrinter();
                    POSMsCashierPrinter _vPOSMsCashierPrinter = this._cashierBL.GetDefaultPrinter(hostIP);
                    if (_vPOSMsCashierPrinter != null)
                    {
                        printerName = "\\\\" + _vPOSMsCashierPrinter.IPAddress + "\\" + _vPOSMsCashierPrinter.PrinterName;
                    }

                    if (m_streams == null || m_streams.Count == 0) return;

                    PrintDocument printDoc = new PrintDocument();
                    printDoc.PrinterSettings.PrinterName = printerName;

                    PaperSize _paperSize = new PaperSize();
                    _paperSize.Width = 400; //850
                    _paperSize.Height = 750;

                    printDoc.DefaultPageSettings.PaperSize = _paperSize;

                    if (!printDoc.PrinterSettings.IsValid)
                    {
                        this.WarningLabel.Text = String.Format("Can't find printer \"{0}\".", printerName);
                        return;
                    }

                    printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                    printDoc.Print();

                    if (m_streams != null)
                    {
                        foreach (Stream stream in m_streams)
                            stream.Close();
                        m_streams = null;
                    }
                }
                catch (Exception ex)
                {
                    foreach (Stream stream in m_streams)
                        stream.Close();
                    m_streams = null;
                    this.WarningLabel.Text = "Sorry, Printer Not Include. ";
                }
                //}

                bool _hasil = this._cashierBL.CekFgSendToKitchen(_prmTransNmbr);
                if (_hasil & _prmDO == false)
                {
                    //List<POSTrSettlementDtRefTransaction> _refTrans = this._cashierBL.GetRefTrans(_prmTransNmbr);
                    //_refTrans = this._cashierBL.GetRefTrans(_prmTransNmbr);

                    //foreach (var _item in _refTrans)
                    //{

                    List<POSMsKitchen> _posMsKitchen = this._cashierBL.GetPrinterKitchen(_prmTransNmbr);

                    foreach (var _item in _posMsKitchen)
                    {
                        _reportDataSource1 = new ReportDataSource();

                        _reportDataSource1 = this._cashierBL.ReportSendToKitchen(_prmTransNmbr, _item.KitchenCode);

                        this.ReportViewer2.LocalReport.DataSources.Clear();
                        this.ReportViewer2.LocalReport.DataSources.Add(_reportDataSource1);

                        _reportPath = "General/ReportSendToKitchen.rdlc";
                        this.ReportViewer2.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath;

                        _reportParam = new ReportParameter[2];
                        _reportParam[0] = new ReportParameter("TransNmbr", _prmTransNmbr, false);
                        _reportParam[1] = new ReportParameter("kitchenCode", _item.KitchenCode, false);

                        this.ReportViewer2.LocalReport.SetParameters(_reportParam);
                        this.ReportViewer2.LocalReport.Refresh();

                        report = new LocalReport();
                        report = this.ReportViewer2.LocalReport;

                        try
                        {
                            string deviceInfo =
                              "<DeviceInfo>" +
                              "  <OutputFormat>EMF</OutputFormat>" +
                              "  <PageWidth>4in</PageWidth>" +
                              "  <PageHeight>7.5in</PageHeight>" +
                              "  <MarginTop>0.0in</MarginTop>" +
                              "  <MarginLeft>0.0in</MarginLeft>" +
                              "  <MarginRight>0.0in</MarginRight>" +
                              "  <MarginBottom>0.0in</MarginBottom>" +
                              "</DeviceInfo>";

                            Warning[] warnings;
                            m_streams = new List<Stream>();

                            report.Render("Image", deviceInfo, CreateStream, out warnings);
                            foreach (Stream stream in m_streams)
                                stream.Position = 0;

                            m_currentPageIndex = 0;

                            //String printerName = this.GetDefaultPrinter();
                            //String printerName = "\\\\192.168.100.16\\Generic";
                            //POSMsKitchen _pOSMsKitchen = this._kitchenBL.GetSingle("0001");
                            //String printerName = "\\\\" + _pOSMsKitchen.KitchenPrinterIPAddress + "\\" + _pOSMsKitchen.KitchenPrinterName;
                            String printerName = "\\\\" + _item.KitchenPrinterIPAddress + "\\" + _item.KitchenPrinterName;

                            //String printerName = _configBL.GetConfig("SetPrinterName");
                            ////PrinterSettings.InstalledPrinters[1].ToString();
                            ////new PrinterSettings().PrinterName;
                            ////this.GetDefaultPrinter();
                            if (m_streams == null || m_streams.Count == 0) return;

                            PrintDocument printDoc = new PrintDocument();
                            printDoc.PrinterSettings.PrinterName = printerName;

                            PaperSize _paperSize = new PaperSize();
                            _paperSize.Width = 400; //850
                            _paperSize.Height = 750;

                            printDoc.DefaultPageSettings.PaperSize = _paperSize;

                            if (!printDoc.PrinterSettings.IsValid)
                            {
                                this.WarningLabel.Text = String.Format("Can't find printer \"{0}\".", printerName);
                                return;
                            }

                            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                            printDoc.Print();

                            if (m_streams != null)
                            {
                                foreach (Stream stream in m_streams)
                                    stream.Close();
                                m_streams = null;
                            }

                            //_scope.Complete();
                            //this.Label5.Text = "line 5.2";
                        }
                        catch (Exception ex)
                        {
                            foreach (Stream stream in m_streams)
                                stream.Close();
                            m_streams = null;
                            //this.Label1.Text = ex.Message;
                            //throw ex;

                            this.WarningLabel.Text = "Sorry, Printer Not Include. ";//+ ex.Message.ToString() + "\n" + ex.StackTrace.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void CardTypeRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            POSMsCreditCardType _temp = (POSMsCreditCardType)e.Item.DataItem;

            Button _cardTypeButton = (Button)e.Item.FindControl("CreditTypeButton");
            _cardTypeButton.Text = _temp.CreditCardTypeName;
            _cardTypeButton.CommandArgument = _temp.CreditCardTypeCode;
        }

        protected void CardTypeRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            this.CreditCardHiddenField.Value = "";
            this.CreditCardHiddenField.Value = e.CommandArgument.ToString();
            this.ShowListCreditCard(e.CommandArgument.ToString());
        }

        protected void CreditCardButtonRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            POSMsCreditCard _temp = (POSMsCreditCard)e.Item.DataItem;

            Button _creditCardButton = (Button)e.Item.FindControl("CreditCardButton");
            _creditCardButton.Text = _temp.CreditCardName;
            _creditCardButton.CommandArgument = _temp.CreditCardCode;
        }

        protected void CreditCardButtonRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //this.GetDiscountPromo(POSSettlementButtonType.CreditCard, e.CommandArgument.ToString() + "," + this.CreditCardHiddenField.Value);
            //if (this.CreditCardReferenceTextBox.Text.Length > 14)
            //{
            //this.CreditCardHiddenField.Value = "";
            POSMsCreditCard _posMsCreditCard = this._creditCardBL.GetSingle(e.CommandArgument.ToString());
            this.CreditCardHiddenField.Value += "," + e.CommandArgument.ToString();
            this.CreditCardHiddenField.Value += "," + this.CreditCardReferenceTextBox.Text;

            Decimal _change = 0;
            Decimal _creditCard = Convert.ToDecimal(this.TotalLiteral.Text) - Convert.ToDecimal(this.CashPaymentLiteral.Text) - Convert.ToDecimal(this.VoucherLiteral.Text) - Convert.ToDecimal(this.DebitCardLiteral.Text);
            Decimal _CreditCardNominalValue = 0;
            if (this.CreditCardNominalTextBox.Text == "")
                this.CreditCardNominalTextBox.Text = "0";
            if (Convert.ToUInt32(this.CreditCardNominalTextBox.Text) > 0)
            {
                Decimal _bankCharge = Convert.ToDecimal(this.CreditCardNominalTextBox.Text) * Convert.ToDecimal(_posMsCreditCard.CustomerCharge) / 100;
                Decimal _grandTotal = Convert.ToDecimal(this.TotalLiteral.Text) + _bankCharge;
                _CreditCardNominalValue = Convert.ToDecimal(this.CreditCardNominalTextBox.Text) + _bankCharge;
                //this.CreditCardLiteral.Text = _CreditCardNominalValue.ToString("#,##0.00");
                this.CreditCardLiteral.Text = _CreditCardNominalValue.ToString("#,##0");
                this.BankChargeLiteral.Text = _bankCharge.ToString("#,##0");
                this.TotalLiteral.Text = _grandTotal.ToString("#,##0");
                this.CheckDiscountPromo();

                //Decimal _change = 0;
                Decimal _total = Convert.ToDecimal(this.TotalLiteral.Text);
                Decimal _cash = Convert.ToDecimal(this.CashPaymentLiteral.Text);
                Decimal _credit = Convert.ToDecimal(this.CreditCardLiteral.Text);
                Decimal _debit = Convert.ToDecimal(this.DebitCardLiteral.Text);
                Decimal _voucher = Convert.ToDecimal(this.VoucherLiteral.Text);

                _change = (_cash + _credit + _debit + _voucher) - _total;
            }
            else
            {
                Decimal _bankCharge = _creditCard * Convert.ToDecimal(_posMsCreditCard.CustomerCharge) / 100;
                Decimal _grandTotal = Convert.ToDecimal(this.TotalLiteral.Text) + Convert.ToDecimal(this.RoundLiteral.Text) + _bankCharge;
                _CreditCardNominalValue = _creditCard + _bankCharge + Convert.ToDecimal(this.RoundLiteral.Text);

                //this.CreditCardLiteral.Text = _creditCard.ToString("#,##0.00");
                this.CreditCardLiteral.Text = _CreditCardNominalValue.ToString("#,##0");
                this.BankChargeLiteral.Text = _bankCharge.ToString("#,##0");
                this.TotalLiteral.Text = _grandTotal.ToString("#,##0");
                this.CheckDiscountPromo();
                //e.CommandArgument.ToString() + "," + this.CreditCardHiddenField.Value
                _change = Convert.ToDecimal(this.TotalLiteral.Text) - Convert.ToDecimal(this.CashPaymentLiteral.Text) - _CreditCardNominalValue - Convert.ToDecimal(this.DebitCardLiteral.Text) - Convert.ToDecimal(this.VoucherLiteral.Text);
            }
            //decimal _change = Convert.ToDecimal(this.TotalLiteral.Text) - Convert.ToDecimal(this.CashPaymentLiteral.Text) - Convert.ToDecimal(this.CreditCardLiteral.Text) - Convert.ToDecimal(this.VoucherLiteral.Text);
            //this.ChangeLiteral.Text = _change.ToString("#,##0.00");
            //this.ChangeDueLiteral.Text = _change.ToString("#,##0.00");
            this.ChangeLiteral.Text = _change.ToString("#,##0");
            this.ChangeDueLiteral.Text = _change.ToString("#,##0");

            this.ShowPanel(POSSettlementButtonType.Cash);
            this.AmountPaymentTextBox.Focus();
            //}
            //else
            //{
            //    this.WarningLabel.Text = "Credit Card No Not Valid";
            //}

        }

        protected void DebitCardRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            POSMsDebitCard _temp = (POSMsDebitCard)e.Item.DataItem;

            Button _debitCardButton = (Button)e.Item.FindControl("DebitCardButton");
            _debitCardButton.Text = _temp.DebitCardName;
            _debitCardButton.CommandArgument = _temp.DebitCardCode;
        }

        protected void DebitCardRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //this.GetDiscountPromo(POSSettlementButtonType.Debit, e.CommandArgument.ToString());
            //if (this.DebitCardReferenceTextBox.Text.Length > 14)
            //{
            this.DebitHiddenField.Value = "";
            this.DebitHiddenField.Value = e.CommandArgument.ToString();
            this.DebitHiddenField.Value += "," + this.DebitCardReferenceTextBox.Text;

            Decimal _change = 0;
            Decimal _debitCard = Convert.ToDecimal(this.TotalLiteral.Text) - Convert.ToDecimal(this.CashPaymentLiteral.Text) - Convert.ToDecimal(this.VoucherLiteral.Text) - Convert.ToDecimal(this.CreditCardLiteral.Text);

            if (this.DebitCardNominalTextBox.Text == "")
                this.DebitCardNominalTextBox.Text = "0";
            if (Convert.ToUInt32(this.DebitCardNominalTextBox.Text) > 0)
            {
                Decimal _DebitCardNominalValue = Convert.ToDecimal(this.DebitCardNominalTextBox.Text);
                //this.DebitCardLiteral.Text = _DebitCardNominalValue.ToString("#,##0.00");
                this.DebitCardLiteral.Text = _DebitCardNominalValue.ToString("#,##0");
                this.CheckDiscountPromo();

                Decimal _total = Convert.ToDecimal(this.TotalLiteral.Text);
                Decimal _cash = Convert.ToDecimal(this.CashPaymentLiteral.Text);
                Decimal _credit = Convert.ToDecimal(this.CreditCardLiteral.Text);
                Decimal _debit = Convert.ToDecimal(this.DebitCardLiteral.Text);
                Decimal _voucher = Convert.ToDecimal(this.VoucherLiteral.Text);

                _change = (_cash + _credit + _debit + _voucher) - _total;

            }
            else
            {
                //this.DebitCardLiteral.Text = _debitCard.ToString("#,##0.00");
                this.DebitCardLiteral.Text = _debitCard.ToString("#,##0");
                this.CheckDiscountPromo();
                _change = Convert.ToDecimal(this.TotalLiteral.Text) - Convert.ToDecimal(this.CashPaymentLiteral.Text) - _debitCard - Convert.ToDecimal(this.CreditCardLiteral.Text) - Convert.ToDecimal(this.VoucherLiteral.Text);
            }

            //this.ChangeLiteral.Text = _change.ToString("#,##0.00");
            //this.ChangeDueLiteral.Text = _change.ToString("#,##0.00");
            this.ChangeLiteral.Text = _change.ToString("#,##0");
            this.ChangeDueLiteral.Text = _change.ToString("#,##0");
            this.ShowPanel(POSSettlementButtonType.Cash);
            this.AmountPaymentTextBox.Focus();
            //}
            //else
            //{
            //    this.WarningLabel.Text = "Debit Card No Not Valid";
            //}
        }

        protected void GetDiscountPromo(POSSettlementButtonType TypePayment, String _debitCreditCode)
        {
            try
            {
                Decimal _discountItem = 0;
                Decimal _discountSubtotal = 0;
                Decimal _round = 0;
                Decimal _grandTotal = 0;
                String _typeDiscon = "";
                int _typePayment = 0;
                String _promo = "";

                String _typeDisconNon = "";
                Decimal _discountItemNon = 0;
                Decimal _discountSubtotalNon = 0;
                Decimal _discMax = 0;
                Decimal _discMaxNon = 0;


                this.DiscountLiteral.Text = "0.00";

                if (TypePayment == POSSettlementButtonType.CreditCard)
                {
                    _typePayment = 1;
                }
                else if (TypePayment == POSSettlementButtonType.Debit)
                {
                    _typePayment = 2;
                }
                else if (TypePayment == POSSettlementButtonType.Voucher)
                {
                    _typePayment = 3;
                }
                else if (TypePayment == POSSettlementButtonType.Cash)
                {
                    _typePayment = 4;
                }

                String _selectedTransNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
                DateTime _now = DateTime.Now;
                List<POSTrPromoItem> _posTrPromoItem = this._cashierBL.GetAllPromo(_selectedTransNmbr, _now, _typePayment, _debitCreditCode, Convert.ToDecimal(this.SubtotalLiteral.Text));
                foreach (var _itempromo in _posTrPromoItem)
                {
                    String _qtyproduct = "0";
                    string[] _split = _selectedTransNmbr.Split(',');
                    foreach (var _item in _split)
                    {
                        if (_qtyproduct == "")
                            _qtyproduct = "0";
                        _qtyproduct = Convert.ToDecimal(Convert.ToDecimal(_qtyproduct) + this._cashierBL.GetProductQty(_item, _itempromo.ProductCode)).ToString("#,#");
                    }

                    if (_itempromo.FreeProductCode != null)
                    {
                        if (_itempromo.ProductCode == "")
                        {
                            _promo += _itempromo.Remark + " FREE " + Convert.ToDecimal(_itempromo.FreeQty).ToString("#,#") + " " + this._productBL.GetProductNameByCode(_itempromo.FreeProductCode) + " (" + _itempromo.FreeProductCode + ") .";
                            if (_typeDiscon != _itempromo.Remark + ". ")
                                _typeDiscon += _itempromo.Remark + ". ";
                        }
                        else
                        {
                            _promo += "BUY " + _qtyproduct + " " + this._productBL.GetProductNameByCode(_itempromo.ProductCode) + " (" + _itempromo.ProductCode + ") FREE " + Convert.ToDecimal(_itempromo.FreeQty).ToString("#,#") + " " + this._productBL.GetProductNameByCode(_itempromo.FreeProductCode) + " (" + _itempromo.FreeProductCode + ") .";
                            _typeDiscon += _itempromo.Remark + ". ";
                        }
                    }
                    else if (_itempromo.Disc != null)
                    {
                        _promo += "BUY " + _qtyproduct + " " + this._productBL.GetProductNameByCode(_itempromo.ProductCode) + " (" + _itempromo.ProductCode + ") GET " + _itempromo.Remark + " : " + Convert.ToDecimal(_itempromo.Disc).ToString("#,#") + ".";
                        _discountItem += (_itempromo.Disc == null) ? 0 : Convert.ToDecimal(_itempromo.Disc);
                        _typeDiscon += _itempromo.Remark + ". ";
                    }
                }

                String[] _transNmbr = _selectedTransNmbr.Split(',');
                foreach (var _item in _transNmbr)
                {
                    V_POSReferenceNotYetPayList _data = new V_POSReferenceNotYetPayList();
                    this.cekDO();
                    if (_prmDO == false)
                    {
                        _data = this._cashierBL.GetSingleReferenceNotPay(_item);
                    }
                    else if (_prmDO == true)
                    {
                        V_POSReferenceNotYetPayListAll _dataDO = this._cashierBL.GetFirstReferenceNotPayAll(_item);
                        _data.TransNmbr = _dataDO.TransNmbr;
                        _data.TransType = _dataDO.TransType;
                        _data.MemberID = _dataDO.MemberID;
                        _data.date = _dataDO.date;
                    }
                    //    List<POSTrPromoItem> _posTrPromoItem = this._cashierBL.GetPromo(_data.TransNmbr, _now, _typePayment, _debitCreditCode, Convert.ToDecimal(this.TotalLiteral.Text), _data.TransType, _data.MemberID);
                    //    foreach (var _itempromo in _posTrPromoItem)
                    //    {
                    //        String _qtyproduct = Convert.ToDecimal(this._cashierBL.GetProductQty(_data.TransNmbr, _data.TransType, _itempromo.ProductCode)).ToString("#,#");
                    //        if (_itempromo.FreeProductCode != null)
                    //        {
                    //            _promo += "BUY " + _qtyproduct + " " + this._productBL.GetProductNameByCode(_itempromo.ProductCode) + " (" + _itempromo.ProductCode + ") FREE " + Convert.ToDecimal(_itempromo.FreeQty).ToString("#,#") + " " + this._productBL.GetProductNameByCode(_itempromo.FreeProductCode) + " (" + _itempromo.FreeProductCode + ") .";
                    //            _typeDiscon += _itempromo.Remark + ". ";
                    //        }
                    //        else if (_itempromo.Disc != null)
                    //        {
                    //            _promo += "BUY " + _qtyproduct + " " + this._productBL.GetProductNameByCode(_itempromo.ProductCode) + " (" + _itempromo.ProductCode + ") GET " + _itempromo.Remark + " : " + Convert.ToDecimal(_itempromo.Disc).ToString("#,#") + ".";
                    //            _discountItem += (_itempromo.Disc == null) ? 0 : Convert.ToDecimal(_itempromo.Disc);
                    //            _typeDiscon += _itempromo.Remark + ". ";
                    //        }
                    //    }
                    if (_promo == "" & _discountItem == 0)
                    {
                        //take biggest disc member-non member : item-subtotal
                        List<POSTrAllDiscon> _pOSTrAllDiscon2 = this._cashierBL.GetDiscount(_data.TransNmbr, _now, _typePayment, _debitCreditCode, Convert.ToDecimal(this.SubtotalLiteral.Text), _data.TransType, "");
                        foreach (var _items in _pOSTrAllDiscon2)
                        {
                            if (Convert.ToDecimal(_items.TotalDisconItem) > 0)
                            {
                                if (_typeDisconNon != _items.TypeDiscon)
                                    _typeDisconNon += _items.TypeDiscon + ". ";
                                _discountItemNon += Convert.ToDecimal(_items.TotalDisconItem);
                            }
                            if (Convert.ToDecimal(_items.TotalDisconSubtotal) > 0)
                                _discountSubtotalNon = Convert.ToDecimal(_items.TotalDisconSubtotal);
                        }

                        List<POSTrAllDiscon> _pOSTrAllDiscon = this._cashierBL.GetDiscount(_data.TransNmbr, _now, _typePayment, _debitCreditCode, Convert.ToDecimal(this.SubtotalLiteral.Text), _data.TransType, _data.MemberID);
                        foreach (var _items in _pOSTrAllDiscon)
                        {
                            if (Convert.ToDecimal(_items.TotalDisconItem) > 0)
                            {
                                if (_typeDiscon != _items.TypeDiscon)
                                    _typeDiscon += _items.TypeDiscon + ". ";
                                _discountItem += Convert.ToDecimal(_items.TotalDisconItem);
                            }
                            if (Convert.ToDecimal(_items.TotalDisconSubtotal) > 0)
                                _discountSubtotal = Convert.ToDecimal(_items.TotalDisconSubtotal);
                        }
                    }
                }
                this.ProductPromoPanel.Visible = false;
                this.NumberInputPanel.Visible = true;

                if (_promo != "")
                {
                    //this.WarningLabel.Text = _promo;
                    this.ProductPromoPanel.Visible = true;
                    this.NumberInputPanel.Visible = false;
                    this.DiscountLiteral.Text = _discountItem.ToString("#,##0");
                    List<ListItem> _listProduct = new List<ListItem>();
                    string[] _product = _promo.Split('.');
                    foreach (var _row in _product)
                    {
                        _listProduct.Add(new ListItem(_row));
                    }
                    this.ListRepeaterPromo.DataSource = _listProduct;
                    this.ListRepeaterPromo.DataBind();
                }
                else
                {
                    this.ListRepeaterPromo.DataSource = null;
                    this.ListRepeaterPromo.DataBind();

                    if (_discountItem > _discountSubtotal)
                    {
                        _discMax = _discountItem;
                    }
                    else
                    {
                        _discMax = _discountSubtotal;
                    }

                    if (_discountItemNon > _discountSubtotalNon)
                    {
                        _discMaxNon = _discountItemNon;
                    }
                    else
                    {
                        _discMaxNon = _discountSubtotalNon;
                    }

                    if (_discMaxNon > _discMax)
                    {
                        //update table POSTrAllDiscon
                        foreach (var _item in _transNmbr)
                        {
                            V_POSReferenceNotYetPayList _data = new V_POSReferenceNotYetPayList();
                            this.cekDO();
                            if (_prmDO == false)
                            {
                                _data = this._cashierBL.GetSingleReferenceNotPay(_item);
                            }
                            else if (_prmDO == true)
                            {
                                V_POSReferenceNotYetPayListAll _dataDO = this._cashierBL.GetFirstReferenceNotPayAll(_item);
                                _data.TransNmbr = _dataDO.TransNmbr;
                                _data.TransType = _dataDO.TransType;
                                _data.MemberID = _dataDO.MemberID;
                                //_data.date = _dataDO.date;
                            }
                            List<POSTrAllDiscon> _pOSTrAllDiscon = this._cashierBL.GetDiscount(_data.TransNmbr, _now, _typePayment, _debitCreditCode, Convert.ToDecimal(this.TotalLiteral.Text), _data.TransType, "");
                        }
                        _typeDiscon = _typeDisconNon;
                        this.DiscountLiteral.Text = _discMaxNon.ToString("#,##0");
                    }
                    else
                    {
                        this.DiscountLiteral.Text = _discMax.ToString("#,##0");
                    }
                }

                //promo by FgPayment sudah dipindah diatas dan digabung dengan Promo
                //foreach (var _item in _transNmbr)
                //{
                //    V_POSReferenceNotYetPayList _data = new V_POSReferenceNotYetPayList();
                //    this.cekDO();
                //    if (_prmDO == false)
                //    {
                //        _data = this._cashierBL.GetSingleReferenceNotPay(_item);
                //    }
                //    else if (_prmDO == true)
                //    {
                //        V_POSReferenceNotYetPayListAll _dataDO = this._cashierBL.GetFirstReferenceNotPayAll(_item);
                //        _data.TransNmbr = _dataDO.TransNmbr;
                //        _data.TransType = _dataDO.TransType;
                //        _data.MemberID = _dataDO.MemberID;
                //        //_data.date = _dataDO.date;
                //    }
                //    _promo = "";
                //    this.WarningLabel.Text = "";
                //    List<POSTrPromoItem> _posTrPromoItem = this._cashierBL.GetPromoByFgPayment(_data.TransNmbr, _now, _typePayment, _debitCreditCode, Convert.ToDecimal(this.TotalLiteral.Text), _data.TransType, _data.MemberID);
                //    foreach (var _itempromo in _posTrPromoItem)
                //    {
                //        this.ProductPromoPanel.Visible = true;
                //        this.NumberInputPanel.Visible = false;
                //        _promo += _itempromo.Remark + " FREE " + Convert.ToDecimal(_itempromo.FreeQty).ToString("#,#") + " " + this._productBL.GetProductNameByCode(_itempromo.FreeProductCode) + " (" + _itempromo.FreeProductCode + ") .";
                //        if (_typeDiscon != _itempromo.Remark + ". ")
                //            _typeDiscon += _itempromo.Remark + ". ";
                //        this.WarningLabel.Text += _promo;
                //    }
                //}

                _grandTotal = Convert.ToDecimal(this.TaxLiteral.Text) + Convert.ToDecimal(this.ServiceChargeLiteral.Text) + Convert.ToDecimal(this.SubtotalLiteral.Text) + Convert.ToDecimal(this.OtherFeeLiteral.Text) + Convert.ToDecimal(this.BankChargeLiteral.Text) - Convert.ToDecimal(this.DPReceivedLiteral.Text) - Convert.ToDecimal(this.DiscountLiteral.Text);
                CompanyConfiguration _companyConfiguration = this._pOSConfigurationBL.GetSingle("POSRounding");
                Int32 POSRound = Convert.ToInt32(_companyConfiguration.SetValue);
                if (POSRound > 0 & Convert.ToDecimal(this.BankChargeLiteral.Text) == 0)
                {
                    _round = _grandTotal % POSRound;
                    this.RoundLiteral.Text = _round.ToString("#,##0");
                    this.TotalLiteral.Text = (_grandTotal - _round).ToString("#,##0");
                }
                else
                {
                    _round = 0;
                    this.RoundLiteral.Text = _round.ToString("#,##0");
                    this.TotalLiteral.Text = (_grandTotal).ToString("#,##0");
                }
                this.DiscountHiddenField.Value = _typeDiscon;
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void errorhandler(Exception ex)
        {
            //ErrorHandler.Record(ex, EventLogEntryType.Error);
            String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "SETTLEMENT");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
        }

        protected void CheckDiscountPromo()
        {
            if (Convert.ToDecimal(this.CreditCardLiteral.Text) > 0)
            {
                String[] _creditcard = this.CreditCardHiddenField.Value.Split(',');
                this.GetDiscountPromo(POSSettlementButtonType.CreditCard, _creditcard[1] + ',' + _creditcard[0]);
            }
            if (Convert.ToDecimal(this.DebitCardLiteral.Text) > 0)
            {
                String[] _debitcard = this.DebitHiddenField.Value.Split(',');
                this.GetDiscountPromo(POSSettlementButtonType.Debit, _debitcard[0]);
            }
            if (Convert.ToDecimal(this.VoucherLiteral.Text) > 0)
                this.GetDiscountPromo(POSSettlementButtonType.Voucher, "");
            if (Convert.ToDecimal(this.CashPaymentLiteral.Text) > 0)
                this.GetDiscountPromo(POSSettlementButtonType.Cash, "");
        }

        protected void ListRepeaterPromo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ListItem _temp = (ListItem)e.Item.DataItem;

                //if (_temp.Value != "")
                //{
                //    Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                //    _no = _page * _maxrow;
                //    _no += 1;
                //    _no = _nomor + _no;
                //    _noLiteral.Text = _no.ToString();
                //    _nomor += 1;
                //}

                Literal _product = (Literal)e.Item.FindControl("ProductLiteral");
                _product.Text = HttpUtility.HtmlEncode(_temp.Value);

            }
        }
    }
}