using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PaymentTrade
{
    public partial class PaymentTradeDbAdd : PaymentTradeBase
    {
        private PaymentTradeBL _paymentTradeBL = new PaymentTradeBL();
        private PaymentBL _paymentBL = new PaymentBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currBL = new CurrencyBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowInvoiceNo();

                this.SetAttribute();
                this.ClearData();
            }
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribute()
        {
            this.CurrCodeTextBox.Attributes.Add("ReadOnly", "True");
            this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
            this.APInvoiceTextBox.Attributes.Add("ReadOnly", "True");
            this.APBalanceTextBox.Attributes.Add("ReadOnly", "True");
            this.APPaidTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNInvoiceTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNBalanceTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountInvoiceTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountBalanceTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountHomeTextBox.Attributes.Add("ReadOnly", "True");
            this.FgValueTextBox.Attributes.Add("ReadOnly", "True");

            this.AmountForexTextBox.Attributes.Add("OnBlur", "CalculateAmountHome(" + this.AmountForexTextBox.ClientID + "," + this.ForexRateTextBox.ClientID + "," + this.PPNPaidTextBox.ClientID + "," + this.APPaidTextBox.ClientID + "," + this.PPNRateTextBox.ClientID + "," + this.AmountHomeTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");
            this.PPNPaidTextBox.Attributes.Add("OnBlur", "CalculateAmountHome(" + this.AmountForexTextBox.ClientID + "," + this.ForexRateTextBox.ClientID + "," + this.PPNPaidTextBox.ClientID + "," + this.APPaidTextBox.ClientID + "," + this.PPNRateTextBox.ClientID + "," + this.AmountHomeTextBox.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowInvoiceNo()
        {
            string _suppCodeHeader = _paymentTradeBL.GetSuppCodeHeader(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_suppCodeHeader != "")
            {
                this.InvoiceNoDropDownList.Items.Clear();
                this.InvoiceNoDropDownList.DataTextField = "FileNmbr";
                this.InvoiceNoDropDownList.DataValueField = "InvoiceNo";
                this.InvoiceNoDropDownList.DataSource = this._paymentTradeBL.GetListInvoiceNoForDDL(_suppCodeHeader);
                this.InvoiceNoDropDownList.DataBind();
                this.InvoiceNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
            else
            {
                this.InvoiceNoDropDownList.Items.Clear();
                this.InvoiceNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
        }

        public void ClearData()
        {
            this.ClearLabel();

            this.InvoiceNoDropDownList.SelectedValue = "null";
            this.CurrCodeTextBox.Text = "";
            this.ForexRateTextBox.Text = "0";
            this.APInvoiceTextBox.Text = "0";
            this.APBalanceTextBox.Text = "0";
            this.APPaidTextBox.Text = "0";
            this.PPNRateTextBox.Text = "0";
            this.PPNInvoiceTextBox.Text = "0";
            this.PPNBalanceTextBox.Text = "0";
            this.PPNPaidTextBox.Text = "0";
            this.AmountForexTextBox.Text = "0";
            this.AmountInvoiceTextBox.Text = "0";
            this.AmountBalanceTextBox.Text = "0";
            this.AmountHomeTextBox.Text = "0";
            this.RemarkTextBox.Text = "";
            this.FgValueTextBox.Text = "";
            this.DecimalPlaceHiddenField.Value = "";
        }

        protected void InvoiceNoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.InvoiceNoDropDownList.SelectedValue != "null")
            {
                V_FNAPPosting _v_FNAPPosting = _paymentTradeBL.GetSingleV_FNAPPosting(this.InvoiceNoDropDownList.SelectedValue);
                FINAPPosting _finApposting = _paymentTradeBL.GetSingleFINAPPosting(this.InvoiceNoDropDownList.SelectedValue);

                this.CurrCodeTextBox.Text = _v_FNAPPosting.Currency;
                byte _decimalPlace = _currBL.GetDecimalPlace(this.CurrCodeTextBox.Text);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                byte _decimalPlace2 = _currBL.GetDecimalPlace(_currBL.GetCurrDefault());
                this.DecimalPlaceHiddenField2.Value = CurrencyDataMapper.GetDecimal(_decimalPlace2);

                this.ForexRateTextBox.Text = _v_FNAPPosting.Forex_Rate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                decimal _apInvoice = Convert.ToDecimal(((_v_FNAPPosting.Amount_Invoice - _finApposting.AmountPPN) == null) ? 0 : (_v_FNAPPosting.Amount_Invoice - _finApposting.AmountPPN));
                this.APInvoiceTextBox.Text = (_apInvoice == 0) ? "0" : _apInvoice.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                decimal _amountPaid = Convert.ToDecimal(((_v_FNAPPosting.Amount_Paid - _finApposting.BalancePPN) == null) ? 0 : (_v_FNAPPosting.Amount_Paid - _finApposting.BalancePPN));
                this.APBalanceTextBox.Text = (_amountPaid == 0) ? "0" : _amountPaid.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                decimal _ppnRate = Convert.ToDecimal((_finApposting.PPNRate == null) ? 0 : _finApposting.PPNRate);
                this.PPNRateTextBox.Text = (_ppnRate == 0) ? "0" : _ppnRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                decimal _ppnInvoice = Convert.ToDecimal((_finApposting.AmountPPN == null) ? 0 : _finApposting.AmountPPN);
                this.PPNInvoiceTextBox.Text = (_ppnInvoice == 0) ? "0" : _ppnInvoice.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                decimal _balancePPN = Convert.ToDecimal((_finApposting.BalancePPN == null) ? 0 : _finApposting.BalancePPN);
                this.PPNBalanceTextBox.Text = (_balancePPN == 0) ? "0" : _balancePPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                decimal _amountInvoice = _finApposting.Amount + _ppnInvoice;
                decimal _amountBalance = _finApposting.Balance + _balancePPN;
                this.AmountInvoiceTextBox.Text = (_amountInvoice == 0) ? "0" : _amountInvoice.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.AmountBalanceTextBox.Text = (_amountBalance == 0) ? "0" : _amountBalance.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.AmountForexTextBox.Text = ((_amountInvoice - _amountBalance) == 0) ? "0" : (_amountInvoice - _amountBalance).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                decimal _sisaPPN = _ppnInvoice - _balancePPN;
                decimal _amountForex = Convert.ToDecimal(this.AmountForexTextBox.Text);
                if (_sisaPPN > _amountForex)
                {
                    this.PPNPaidTextBox.Text = this.AmountForexTextBox.Text;
                }
                else
                {
                    this.PPNPaidTextBox.Text = (_sisaPPN == 0) ? "0" : _sisaPPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                }
                decimal _ppnPaid = Convert.ToDecimal((this.PPNPaidTextBox.Text == "") ? "0" : this.PPNPaidTextBox.Text);
                this.APPaidTextBox.Text = ((_amountForex - _ppnPaid) == 0) ? "0" : (_amountForex - _ppnPaid).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                decimal _apPaid = Convert.ToDecimal(this.APPaidTextBox.Text);
                this.AmountHomeTextBox.Text = ((_apPaid * _v_FNAPPosting.Forex_Rate) + (_ppnPaid * _ppnRate)).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace2));
                this.FgValueTextBox.Text = _finApposting.FgValue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            else
            {
                this.ClearData();
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            FINPayTradeDb _finPayTradeDb = new FINPayTradeDb();
            V_FNAPPosting _v_FNAPPosting = _paymentTradeBL.GetSingleV_FNAPPosting(this.InvoiceNoDropDownList.SelectedValue);

            _finPayTradeDb.TransNmbr = _transNo;
            _finPayTradeDb.InvoiceNo = this.InvoiceNoDropDownList.SelectedValue;
            _finPayTradeDb.SuppInvoice = _v_FNAPPosting.Supp_Code;
            _finPayTradeDb.CurrCode = this.CurrCodeTextBox.Text;
            _finPayTradeDb.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _finPayTradeDb.APInvoice = Convert.ToDecimal((this.APInvoiceTextBox.Text == "") ? "0" : this.APInvoiceTextBox.Text);
            _finPayTradeDb.APBalance = Convert.ToDecimal((this.APBalanceTextBox.Text == "") ? "0" : this.APBalanceTextBox.Text);
            _finPayTradeDb.APPaid = Convert.ToDecimal((this.APPaidTextBox.Text == "") ? "0" : this.APPaidTextBox.Text);
            _finPayTradeDb.PPNRate = Convert.ToDecimal((this.PPNRateTextBox.Text == "") ? "0" : this.PPNRateTextBox.Text);
            _finPayTradeDb.PPNInvoice = Convert.ToDecimal((this.PPNInvoiceTextBox.Text == "") ? "0" : this.PPNInvoiceTextBox.Text);
            _finPayTradeDb.PPNBalance = Convert.ToDecimal((this.PPNBalanceTextBox.Text == "") ? "0" : this.PPNBalanceTextBox.Text);
            _finPayTradeDb.PPNPaid = Convert.ToDecimal((this.PPNPaidTextBox.Text == "") ? "0" : this.PPNPaidTextBox.Text);
            _finPayTradeDb.AmountForex = Convert.ToDecimal((this.AmountForexTextBox.Text == "") ? "0" : this.AmountForexTextBox.Text);
            _finPayTradeDb.AmountInvoice = Convert.ToDecimal((this.AmountInvoiceTextBox.Text == "") ? "0" : this.AmountInvoiceTextBox.Text);
            _finPayTradeDb.AmountBalance = Convert.ToDecimal((this.AmountBalanceTextBox.Text == "") ? "0" : this.AmountBalanceTextBox.Text);
            _finPayTradeDb.AmountHome = Convert.ToDecimal((this.AmountHomeTextBox.Text == "") ? "0" : this.AmountHomeTextBox.Text);
            _finPayTradeDb.Remark = this.RemarkTextBox.Text;
            _finPayTradeDb.FgValue = Convert.ToDecimal(this.FgValueTextBox.Text);

            bool _result = this._paymentTradeBL.AddFINPayTradeDb(_finPayTradeDb);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}