using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.DPCustomerRetur
{
    public partial class DPCustomerReturPayAdd : DPCustomerReturBase
    {
        private PaymentBL _paymentBL = new PaymentBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private FINDPCustomerReturBL _finDPCustomerReturBL = new FINDPCustomerReturBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();

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
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
               
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetAttribute();
                this.ShowPayType();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.CurrRateTextBox.Attributes.Add("OnBlur", "CalculateAmountTotal(" + this.CurrRateTextBox.ClientID + "," + this.AmountForexTextBox.ClientID + "," + this.AmountTotalTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHiddenFieldHome.ClientID + " );");
            this.AmountForexTextBox.Attributes.Add("OnBlur", "CalculateAmountTotal(" + this.CurrRateTextBox.ClientID + "," + this.AmountForexTextBox.ClientID + "," + this.AmountTotalTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHiddenFieldHome.ClientID + ");");
            this.BankExpenseTextBox.Attributes.Add("OnBlur", "FormatCurrency2(" + this.BankExpenseTextBox.ClientID + "," + this.DecimalPlaceHiddenFieldHome.ClientID + ");");
        }

        public void SetAttribute()
        {
            this.CurrCodeTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountTotalTextBox.Attributes.Add("ReadOnly", "True");
            this.DateTextBox.Attributes.Add("ReadOnly", "True");

            this.CurrRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.AmountForexTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.BankExpenseTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");

            //this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat(" + this.CurrRateTextBox.ClientID + ");");
            //this.AmountForexTextBox.Attributes.Add("OnBlur", "ChangeFormat(" + this.AmountForexTextBox.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.SetAttributeRate();
        }

        public void ClearData()
        {
            this.ClearLabel();
            this.PayTypeDropDownList.SelectedValue = "null";
            this.FgGiroHiddenField.Value = "";
            this.DocumentNoTextBox.Text = "";
            this.DocNoRequiredFieldValidator.Enabled = false;
            this.CurrCodeTextBox.Text = "";
            this.CurrRateTextBox.Text = "";
            this.AmountForexTextBox.Text = "0";
            this.AmountTotalTextBox.Text = "0";
            this.RemarkTextBox.Text = "";
            this.BankPaymentDropDownList.Items.Clear();
            this.BankPaymentDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.BankPaymentDropDownList.SelectedValue = "null";
            this.BankPaymentDropDownList.Enabled = false;
            this.BankPaymentCustomValidator.Enabled = false;
            this.DateTextBox.Text = "";
            this.ImageSpan.Visible = false;
            this.BankExpenseTextBox.Text = "";
            this.BankExpenseTextBox.Attributes.Add("ReadOnly", "true");
            this.BankExpenseTextBox.Attributes.Add("style", "background-color:#cccccc");
            this.BankExpenseTextBox.Attributes.Remove("OnBlur");
            this.ClearDataNumeric();
        }

        public void ShowPayType()
        {
            this.PayTypeDropDownList.Items.Clear();
            this.PayTypeDropDownList.DataTextField = "PayName";
            this.PayTypeDropDownList.DataValueField = "PayCode";
            this.PayTypeDropDownList.DataSource = this._paymentBL.GetListDDLPayment();
            this.PayTypeDropDownList.DataBind();
            this.PayTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowBankPayment()
        {
            this.BankPaymentDropDownList.Items.Clear();
            this.BankPaymentDropDownList.DataTextField = "PayName";
            this.BankPaymentDropDownList.DataValueField = "PayCode";
            this.BankPaymentDropDownList.DataSource = this._paymentBL.GetListDDLbyViewMsBankPayment(this.CurrCodeTextBox.Text);
            this.BankPaymentDropDownList.DataBind();
            this.BankPaymentDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void PayTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.PayTypeDropDownList.SelectedValue != "null")
            {
                char _fgMode = this._paymentBL.GetFgMode(this.PayTypeDropDownList.SelectedValue);

                this.CurrCodeTextBox.Text = this._paymentBL.GetCurrCodeByCode(this.PayTypeDropDownList.SelectedValue);

                this.SetCurrRate();

                if (_fgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Giro))
                {
                    this.FgGiroHiddenField.Value = (YesNoDataMapper.GetYesNo(YesNo.Yes)).ToString();

                    this.DocNoRequiredFieldValidator.Enabled = true;

                    this.BankPaymentDropDownList.Enabled = true;
                    this.BankPaymentCustomValidator.Enabled = true;
                    this.ShowBankPayment();

                    this.ImageSpan.Visible = true;
                    this.DateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);

                    this.BankExpenseTextBox.Text = "";
                    this.BankExpenseTextBox.Attributes.Add("ReadOnly", "true");
                    this.BankExpenseTextBox.Attributes.Add("style", "background-color:#cccccc");
                    this.BankExpenseTextBox.Attributes.Remove("OnBlur");

                }
                else
                {
                    this.FgGiroHiddenField.Value = (YesNoDataMapper.GetYesNo(YesNo.No)).ToString();

                    this.DocNoRequiredFieldValidator.Enabled = false;

                    this.BankPaymentDropDownList.Enabled = false;
                    this.BankPaymentCustomValidator.Enabled = false;
                    this.BankPaymentDropDownList.Items.Clear();
                    this.BankPaymentDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

                    this.ImageSpan.Visible = false;
                    this.DateTextBox.Text = "";

                    if (_fgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Bank))
                    {
                        this.BankExpenseTextBox.Attributes.Remove("ReadOnly");
                        this.BankExpenseTextBox.Attributes.Add("style", "background-color:#ffffff");
                        this.BankExpenseTextBox.Attributes.Add("OnBlur", "ChangeFormat(" + this.BankExpenseTextBox.ClientID + ");");
                    }
                    else
                    {
                        this.BankExpenseTextBox.Text = "";
                        this.BankExpenseTextBox.Attributes.Add("ReadOnly", "true");
                        this.BankExpenseTextBox.Attributes.Add("style", "background-color:#cccccc");
                        this.BankExpenseTextBox.Attributes.Remove("OnBlur");
                    }
                }
            }
            else
            {
                this.ClearData();
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            int _maxItemNo = this._finDPCustomerReturBL.GetMaxNoItemFINDPCustReturPay(_transNo);

            FINDPCustReturPay _finDPCustomerReturPay = new FINDPCustReturPay();
            _finDPCustomerReturPay.TransNmbr = _transNo;
            _finDPCustomerReturPay.ItemNo = _maxItemNo + 1;
            _finDPCustomerReturPay.PayType = this.PayTypeDropDownList.SelectedValue;
            _finDPCustomerReturPay.FgGiro = Convert.ToChar(this.FgGiroHiddenField.Value);
            _finDPCustomerReturPay.DocumentNo = this.DocumentNoTextBox.Text;
            _finDPCustomerReturPay.CurrCode = this.CurrCodeTextBox.Text;
            _finDPCustomerReturPay.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finDPCustomerReturPay.AmountForex = Convert.ToDecimal(this.AmountForexTextBox.Text);
            _finDPCustomerReturPay.AmountHome = Convert.ToDecimal(this.AmountTotalTextBox.Text);
            _finDPCustomerReturPay.Remark = this.RemarkTextBox.Text;

            if (this.BankPaymentDropDownList.SelectedValue != "null")
            {
                _finDPCustomerReturPay.BankPayment = this.BankPaymentDropDownList.SelectedValue;
            }

            if (this.DateTextBox.Text.Trim() != "")
            {
                _finDPCustomerReturPay.DueDate = DateFormMapper.GetValue(this.DateTextBox.Text);
            }

            if (this.BankExpenseTextBox.Text != "")
            {
                _finDPCustomerReturPay.BankExpense = Convert.ToDecimal(this.BankExpenseTextBox.Text);
            }

            bool _result = this._finDPCustomerReturBL.AddFINDPCustReturPay(_finDPCustomerReturPay);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        private void ClearDataNumeric()
        {
            this.CurrRateTextBox.Text = "0";
            this.DecimalPlaceHiddenField.Value = "";
            this.DecimalPlaceHiddenFieldHome.Value = "";
            this.CurrCodeTextBox.Text = "0";
            this.CurrCodeTextBox.Text = "";
            this.AmountForexTextBox.Text = "0";
            this.AmountForexTextBox.Text = "0";
        }

        private void DisableRate()
        {
            this.CurrRateTextBox.Text = "1";
            this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateTextBox.Attributes.Add("Style", "background-color:#cccccc");
        }

        private void EnableRate()
        {
            this.CurrRateTextBox.Attributes.Remove("ReadOnly");
            this.CurrRateTextBox.Attributes.Add("Style", "background-color:#FFFFFF");

        }

        private void SetCurrRate()
        {
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrCodeTextBox.Text);
            byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(this._currencyBL.GetCurrDefault());
            this.CurrRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeTextBox.Text).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.DecimalPlaceHiddenFieldHome.Value = CurrencyDataMapper.GetDecimal(_decimalPlaceHome);

            if (this.CurrCodeTextBox.Text.Trim().ToLower() == _currencyBL.GetCurrDefault().Trim().ToLower())
            {
                this.DisableRate();
            }
            else
            {
                this.EnableRate();
            }
        }

    }
}