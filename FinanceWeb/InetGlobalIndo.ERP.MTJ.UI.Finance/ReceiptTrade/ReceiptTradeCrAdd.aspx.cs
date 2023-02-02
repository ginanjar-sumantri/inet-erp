using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.ReceiptTrade
{
    public partial class ReceiptTradeCrAdd : ReceiptTradeBase
    {
        private CurrencyBL _currBL = new CurrencyBL();
        private FINReceiptTradeBL _receiptTradeBL = new FINReceiptTradeBL();
        private PermissionBL _permBL = new PermissionBL();

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

                this.ClearData();
                this.SetAttribute();
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
            this.ForexRateTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
            this.FgValueTextBox.Attributes.Add("ReadOnly", "True");

            this.ARInvoiceTextBox.Attributes.Add("ReadOnly", "True");
            this.ARBalanceTextBox.Attributes.Add("ReadOnly", "True");
            this.ARToBePaidTextBox.Attributes.Add("ReadOnly", "True");

            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNInvoiceTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNBalanceTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNToBePaidTextBox.Attributes.Add("ReadOnly", "True");

            this.AmountInvoiceTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountBalanceTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountToBePaidTextBox.Attributes.Add("ReadOnly", "True");

            this.ReceiptAmountBalanceTextBox.Attributes.Add("ReadOnly", "True");
            this.ReceiptPPNBalanceTextBox.Attributes.Add("ReadOnly", "True");

            this.TotalAmountBalanceTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalAmountForexTextBox.Attributes.Add("ReadOnly", "True");

            this.ForexRateTextBox.Attributes.Add("OnBlur", "return ChangeFormat2(" + this.ForexRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.ReceiptPPNTextBox.Attributes.Add("OnBlur", "return ChangeFormat2(" + this.ReceiptPPNTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.ReceiptAmountForexTextBox.Attributes.Add("OnBlur", "return ChangeFormat2(" + this.ReceiptAmountForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.ReceiptAmountForexTextBox.Attributes.Add("OnBlur", "CalculateAmountBalance(" + this.ARToBePaidTextBox.ClientID + "," + this.PPNToBePaidTextBox.ClientID + "," + this.AmountToBePaidTextBox.ClientID + "," + this.ReceiptAmountForexTextBox.ClientID + "," + this.ReceiptAmountBalanceTextBox.ClientID + "," + this.ReceiptPPNTextBox.ClientID + "," + this.ReceiptPPNBalanceTextBox.ClientID + "," + this.TotalAmountForexTextBox.ClientID + "," + this.TotalAmountBalanceTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");
            this.ReceiptPPNTextBox.Attributes.Add("OnBlur", "CalculateAmountBalance(" + this.ARToBePaidTextBox.ClientID + "," + this.PPNToBePaidTextBox.ClientID + "," + this.AmountToBePaidTextBox.ClientID + "," + this.ReceiptAmountForexTextBox.ClientID + "," + this.ReceiptAmountBalanceTextBox.ClientID + "," + this.ReceiptPPNTextBox.ClientID + "," + this.ReceiptPPNBalanceTextBox.ClientID + "," + this.TotalAmountForexTextBox.ClientID + "," + this.TotalAmountBalanceTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowInvoiceNo()
        {
            string _custCodeHeader = _receiptTradeBL.GetCustCodeHeader(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_custCodeHeader != "")
            {
                this.InvoiceNoDropDownList.Items.Clear();
                this.InvoiceNoDropDownList.DataTextField = "FileNmbr";
                this.InvoiceNoDropDownList.DataValueField = "InvoiceNo";
                this.InvoiceNoDropDownList.DataSource = this._receiptTradeBL.GetListInvoiceNoForDDL(_custCodeHeader);
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
            this.ARInvoiceTextBox.Text = "0";
            this.ARBalanceTextBox.Text = "0";
            this.ARToBePaidTextBox.Text = "0";
            this.PPNRateTextBox.Text = "0";
            this.PPNInvoiceTextBox.Text = "0";
            this.PPNBalanceTextBox.Text = "0";
            this.PPNToBePaidTextBox.Text = "0";
            this.AmountToBePaidTextBox.Text = "0";
            this.AmountInvoiceTextBox.Text = "0";
            this.AmountBalanceTextBox.Text = "0";
            this.ReceiptPPNBalanceTextBox.Text = "0";
            this.ReceiptPPNTextBox.Text = "0";
            this.ReceiptAmountForexTextBox.Text = "0";
            this.ReceiptAmountBalanceTextBox.Text = "0";
            this.TotalAmountForexTextBox.Text = "0";
            this.TotalAmountBalanceTextBox.Text = "0";
            this.RemarkTextBox.Text = "";
            this.FgValueTextBox.Text = "";
            this.DecimalPlaceHiddenField.Value = "";
            this.DecimalPlaceHiddenField2.Value = "";
        }

        protected void InvoiceNoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.InvoiceNoDropDownList.SelectedValue != "null")
            {
                V_FNARPosting _v_FNARPosting = _receiptTradeBL.GetSingleV_FNARPosting(this.InvoiceNoDropDownList.SelectedValue);
                FINARPosting _finArposting = _receiptTradeBL.GetSingleFINARPosting(this.InvoiceNoDropDownList.SelectedValue);

                byte _decimalPlace = _currBL.GetDecimalPlace(_finArposting.CurrCode);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                byte _decimalPlace2 = _currBL.GetDecimalPlace(_currBL.GetCurrDefault());
                this.DecimalPlaceHiddenField2.Value = CurrencyDataMapper.GetDecimal(_decimalPlace2);

                this.CurrCodeTextBox.Text = _v_FNARPosting.Currency;
                this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                this.ForexRateTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
                this.ForexRateTextBox.Text = (_v_FNARPosting.Forex_Rate == 0) ? "0" : _v_FNARPosting.Forex_Rate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                this.ARInvoiceTextBox.Text = (_v_FNARPosting.Amount_Invoice == 0) ? "0" : _v_FNARPosting.Amount_Invoice.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.ARBalanceTextBox.Text = (_v_FNARPosting.Amount_Paid == 0) ? "0" : _v_FNARPosting.Amount_Paid.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.ARToBePaidTextBox.Text = (_v_FNARPosting.Amount_Invoice - _v_FNARPosting.Amount_Paid == 0) ? "0" : (_v_FNARPosting.Amount_Invoice - _v_FNARPosting.Amount_Paid).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                decimal _ppnRate = Convert.ToDecimal((_finArposting.PPNRate == null) ? 0 : _finArposting.PPNRate);
                this.PPNRateTextBox.Text = (_ppnRate == 0) ? "0" : _ppnRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.PPNInvoiceTextBox.Text = (_finArposting.AmountPPN == 0) ? "0" : _finArposting.AmountPPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                decimal _balancePPN = Convert.ToDecimal((_finArposting.BalancePPN == null) ? 0 : _finArposting.BalancePPN);
                this.PPNBalanceTextBox.Text = (_balancePPN == 0) ? "0" : _balancePPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.PPNToBePaidTextBox.Text = (_finArposting.AmountPPN - _balancePPN == 0) ? "0" : (_finArposting.AmountPPN - _balancePPN).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                decimal _amountInvoice = _finArposting.Amount + _finArposting.AmountPPN;
                decimal _amountBalance = _finArposting.Balance + _balancePPN;
                this.AmountInvoiceTextBox.Text = (_amountInvoice == 0) ? "0" : _amountInvoice.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.AmountBalanceTextBox.Text = (_amountBalance == 0) ? "0" : _amountBalance.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.AmountToBePaidTextBox.Text = (_amountInvoice - _amountBalance == 0) ? "0" : (_amountInvoice - _amountBalance).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                this.ReceiptAmountForexTextBox.Text = this.ARToBePaidTextBox.Text;
                this.ReceiptAmountBalanceTextBox.Text = "0";
                this.ReceiptPPNTextBox.Text = this.PPNToBePaidTextBox.Text;
                this.ReceiptPPNBalanceTextBox.Text = "0";
                this.TotalAmountForexTextBox.Text = ((_v_FNARPosting.Amount_Invoice - _v_FNARPosting.Amount_Paid) + (_finArposting.AmountPPN - _balancePPN)).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.TotalAmountBalanceTextBox.Text = "0";

                this.FgValueTextBox.Text = (_finArposting.FgValue == 0) ? "0" : _finArposting.FgValue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            else
            {
                this.ClearData();
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            FINReceiptTradeCr _finPayTradeCr = new FINReceiptTradeCr();

            _finPayTradeCr.TransNmbr = _transNo;
            _finPayTradeCr.InvoiceNo = this.InvoiceNoDropDownList.SelectedValue;
            _finPayTradeCr.CurrCode = this.CurrCodeTextBox.Text;
            _finPayTradeCr.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _finPayTradeCr.ARInvoice = Convert.ToDecimal((this.ARInvoiceTextBox.Text == "") ? "0" : this.ARInvoiceTextBox.Text);
            _finPayTradeCr.ARBalance = Convert.ToDecimal((this.ARBalanceTextBox.Text == "") ? "0" : this.ARBalanceTextBox.Text);
            _finPayTradeCr.ARPaid = Convert.ToDecimal((this.ReceiptAmountForexTextBox.Text == "") ? "0" : this.ReceiptAmountForexTextBox.Text);
            _finPayTradeCr.PPnRate = Convert.ToDecimal((this.PPNRateTextBox.Text == "") ? "0" : this.PPNRateTextBox.Text);
            _finPayTradeCr.PPnInvoice = Convert.ToDecimal((this.PPNInvoiceTextBox.Text == "") ? "0" : this.PPNInvoiceTextBox.Text);
            _finPayTradeCr.PPnBalance = Convert.ToDecimal((this.PPNBalanceTextBox.Text == "") ? "0" : this.PPNBalanceTextBox.Text);
            _finPayTradeCr.PPnPaid = Convert.ToDecimal((this.ReceiptPPNTextBox.Text == "") ? "0" : this.ReceiptPPNTextBox.Text);
            _finPayTradeCr.AmountForex = Convert.ToDecimal((this.TotalAmountForexTextBox.Text == "") ? "0" : this.TotalAmountForexTextBox.Text);
            _finPayTradeCr.AmountInvoice = Convert.ToDecimal((this.AmountInvoiceTextBox.Text == "") ? "0" : this.AmountInvoiceTextBox.Text);
            _finPayTradeCr.AmountBalance = Convert.ToDecimal((this.AmountBalanceTextBox.Text == "") ? "0" : this.AmountBalanceTextBox.Text);
            decimal _amountHome = (_finPayTradeCr.ARPaid * _finPayTradeCr.ForexRate) + (Convert.ToDecimal(_finPayTradeCr.PPnRate) * _finPayTradeCr.PPnPaid);
            _finPayTradeCr.AmountHome = _amountHome;
            _finPayTradeCr.Remark = this.RemarkTextBox.Text;
            _finPayTradeCr.FgValue = Convert.ToDecimal(this.FgValueTextBox.Text);

            bool _result = this._receiptTradeBL.AddFINReceiptTradeCr(_finPayTradeCr);

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