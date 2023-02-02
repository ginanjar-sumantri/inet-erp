using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.GiroPaymentChange
{
    public partial class GiroPaymentChangePaymentEdit : GiroPaymentChangeBase
    {
        private PaymentBL _paymentBL = new PaymentBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private FINChangeGiroOutBL _finChangeGiroOutBL = new FINChangeGiroOutBL();
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
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

                this.ShowPayType();

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "  );");
            this.AmountForexTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.AmountForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.BankExpenseTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.BankExpenseTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        public void SetAttribute()
        {
            this.CurrCodeTextBox.Attributes.Add("ReadOnly", "True");
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.SetAttributeRate();
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

        public void ShowData()
        {
            FINChangeGiroOutPay _finChangeGiroOutPay = this._finChangeGiroOutBL.GetSingleFINChangeGiroOutPay(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItem), ApplicationConfig.EncryptionKey));

            char _fgMode = this._paymentBL.GetFgMode(_finChangeGiroOutPay.PayType);

            this.PayTypeDropDownList.SelectedValue = _finChangeGiroOutPay.PayType;
            this.FgGiroHiddenField.Value = _finChangeGiroOutPay.FgGiro.ToString();
            this.DocumentNoTextBox.Text = _finChangeGiroOutPay.DocumentNo;
            this.CurrCodeTextBox.Text = _finChangeGiroOutPay.CurrCode;

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrCodeTextBox.Text);

            string _defaultCurrency = this._currencyBL.GetCurrDefault();
            if (this.CurrCodeTextBox.Text.Trim().ToLower() == _defaultCurrency.Trim().ToLower())
            {
                this.CurrRateTextBox.Text = "1";
                this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#cccccc");
            }
            else
            {
                this.CurrRateTextBox.Text = (_finChangeGiroOutPay.ForexRate == 0) ? "0" : _finChangeGiroOutPay.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#ffffff");
            }

            this.AmountForexTextBox.Text = (_finChangeGiroOutPay.AmountForex == 0) ? "0" : _finChangeGiroOutPay.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _finChangeGiroOutPay.Remark;

            if (_fgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Giro))
            {
                this.DocNoRequiredFieldValidator.Enabled = true;

                this.BankPaymentDropDownList.Enabled = true;
                this.BankPaymentCustomValidator.Enabled = true;
                this.ShowBankPayment();
                this.BankPaymentDropDownList.SelectedValue = _finChangeGiroOutPay.BankPayment;

                this.ImageSpan.Visible = true;
                this.DateTextBox.Text = DateFormMapper.GetValue(_finChangeGiroOutPay.DueDate);

                this.BankExpenseTextBox.Text = "";
                this.BankExpenseTextBox.Attributes.Add("ReadOnly", "true");
                this.BankExpenseTextBox.Attributes.Add("style", "background-color:#cccccc");
                this.BankExpenseTextBox.Attributes.Remove("OnBlur");
            }
            else
            {
                this.DocNoRequiredFieldValidator.Enabled = false;

                this.BankPaymentDropDownList.Enabled = false;
                this.BankPaymentCustomValidator.Enabled = false;
                this.BankPaymentDropDownList.Items.Clear();
                this.BankPaymentDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

                this.ImageSpan.Visible = false;
                this.DateTextBox.Text = "";

                if (_fgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Bank))
                {
                    decimal _tempBankExpense = Convert.ToDecimal(_finChangeGiroOutPay.BankExpense);
                    this.BankExpenseTextBox.Text = (_tempBankExpense == 0) ? "0" : _tempBankExpense.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)); ;

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

        public void ShowPayType()
        {
            this.PayTypeDropDownList.Items.Clear();
            this.PayTypeDropDownList.DataTextField = "PayName";
            this.PayTypeDropDownList.DataValueField = "PayCode";
            this.PayTypeDropDownList.DataSource = this._paymentBL.GetListDDLPayment();
            this.PayTypeDropDownList.DataBind();
            this.PayTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
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
        }

        protected void PayTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.PayTypeDropDownList.SelectedValue != "null")
            {
                char _fgMode = this._paymentBL.GetFgMode(this.PayTypeDropDownList.SelectedValue);

                this.CurrCodeTextBox.Text = this._paymentBL.GetCurrCodeByCode(this.PayTypeDropDownList.SelectedValue);

                decimal _tempCurrRate = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeTextBox.Text);

                byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrCodeTextBox.Text);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                string _defaultCurrency = this._currencyBL.GetCurrDefault();
                if (this.CurrCodeTextBox.Text.Trim().ToLower() == _defaultCurrency.Trim().ToLower())
                {
                    this.CurrRateTextBox.Text = "1";
                    this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.CurrRateTextBox.Attributes.Add("style", "background-color:#cccccc");
                }
                else
                {
                    this.CurrRateTextBox.Text = _tempCurrRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                    this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                    this.CurrRateTextBox.Attributes.Add("style", "background-color:#ffffff");
                }

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
                        this.BankExpenseTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.BankExpenseTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + " );");
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
            FINChangeGiroOutPay _finChangeGiroOutPay = this._finChangeGiroOutBL.GetSingleFINChangeGiroOutPay(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItem), ApplicationConfig.EncryptionKey));

            _finChangeGiroOutPay.PayType = this.PayTypeDropDownList.SelectedValue;
            _finChangeGiroOutPay.FgGiro = Convert.ToChar(this.FgGiroHiddenField.Value);
            _finChangeGiroOutPay.DocumentNo = this.DocumentNoTextBox.Text;
            _finChangeGiroOutPay.CurrCode = this.CurrCodeTextBox.Text;
            _finChangeGiroOutPay.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finChangeGiroOutPay.AmountForex = Convert.ToDecimal(this.AmountForexTextBox.Text);
            _finChangeGiroOutPay.Remark = this.RemarkTextBox.Text;

            if (this.BankPaymentDropDownList.SelectedValue != "null")
            {
                _finChangeGiroOutPay.BankPayment = this.BankPaymentDropDownList.SelectedValue;
            }
            else
            {
                _finChangeGiroOutPay.BankPayment = "";
            }

            if (this.DateTextBox.Text.Trim() != "")
            {
                _finChangeGiroOutPay.DueDate = DateFormMapper.GetValue(this.DateTextBox.Text);
            }
            else
            {
                _finChangeGiroOutPay.DueDate = null;
            }

            if (this.BankExpenseTextBox.Text != "")
            {
                _finChangeGiroOutPay.BankExpense = Convert.ToDecimal(this.BankExpenseTextBox.Text);
            }
            else
            {
                _finChangeGiroOutPay.BankExpense = null;
            }

            bool _result = this._finChangeGiroOutBL.EditFINChangeGiroOutPay(_finChangeGiroOutPay);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }

    }
}