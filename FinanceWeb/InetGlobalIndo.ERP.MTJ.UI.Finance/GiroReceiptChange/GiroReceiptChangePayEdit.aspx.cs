using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.GiroReceiptChange
{
    public partial class GiroReceiptChangePayEdit : GiroReceiptChangeBase
    {
        private FINChangeGiroInBL _finChangeGiroInBL = new FINChangeGiroInBL();
        private PaymentBL _payBL = new PaymentBL();
        private BankBL _bankBL = new BankBL();
        private CurrencyRateBL _currRateBL = new CurrencyRateBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
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

            _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.DueDateLiteral.Text = "<input id='button1' type='button' Style = 'visibility:visible' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowReceiptType();
                this.ShowBankGiro();
                this.SetCurrRate();
                this.SetAttribute();
                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.AmountForexTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + AmountForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + " );");
            this.BankExpenseTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + BankExpenseTextBox.ClientID + "," + this.DecimalPlaceHiddenFieldHome.ClientID + ");");
            this.RateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + RateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        protected void SetAttribute()
        {
            this.DueDateTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.SetAttributeRate();
        }

        private void ShowData()
        {
            FINChangeGiroInPay _finChangeGiroInPay = this._finChangeGiroInBL.GetSingleFINChangeGiroInPay(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._itemKey), ApplicationConfig.EncryptionKey)));

            char _fgMode = this._payBL.GetFgMode(_finChangeGiroInPay.ReceiptType);

            this.ReceiptTypeDropDownList.SelectedValue = _finChangeGiroInPay.ReceiptType;
            this.DocNoTextBox.Text = _finChangeGiroInPay.DocumentNo;
            this.CurrTextBox.Text = _finChangeGiroInPay.CurrCode;

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrTextBox.Text);
            byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(this._currencyBL.GetCurrDefault());

            string _defaultCurrency = this._currencyBL.GetCurrDefault();
            if (this.CurrTextBox.Text.Trim().ToLower() == _defaultCurrency.Trim().ToLower())
            {
                this.RateTextBox.Text = "1";
                this.RateTextBox.Attributes.Add("ReadOnly", "True");
                this.RateTextBox.Attributes.Add("style", "background-color:#cccccc");
            }
            else
            {
                this.RateTextBox.Text = (_finChangeGiroInPay.ForexRate == 0) ? "0" : _finChangeGiroInPay.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.RateTextBox.Attributes.Remove("ReadOnly");
                this.RateTextBox.Attributes.Add("style", "background-color:#ffffff");
            }

            this.AmountForexTextBox.Text = _finChangeGiroInPay.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DueDateTextBox.Text = DateFormMapper.GetValue(_finChangeGiroInPay.DueDate);
            this.BankGiroDropDownList.SelectedValue = _finChangeGiroInPay.BankGiro;
            this.RemarkTextBox.Text = _finChangeGiroInPay.Remark;
            this.BankExpenseTextBox.Text = Convert.ToDecimal(_finChangeGiroInPay.BankExpense).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
            this.FgGiroHiddenField.Value = _finChangeGiroInPay.FgGiro.ToString();
            if (_fgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Giro))
            {
                this.DocumentNoRequiredFieldValidator.Enabled = true;

                this.BankGiroDropDownList.Enabled = true;
                this.BankGiroCustomValidator.Enabled = true;
                this.ShowBankGiro();
                this.BankGiroDropDownList.SelectedValue = _finChangeGiroInPay.BankGiro;

                this.DueDateLiteral.Text = "<input id='button1' type='button' Style = 'visibility:visible' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                //this.headline_date_start.Attributes.Add("Style", "visibility:visible");
                this.DueDateTextBox.Text = DateFormMapper.GetValue(_finChangeGiroInPay.DueDate);

                this.BankExpenseTextBox.Text = "";
                this.BankExpenseTextBox.Attributes.Add("ReadOnly", "true");
                this.BankExpenseTextBox.Attributes.Add("style", "background-color:#cccccc");
                this.BankExpenseTextBox.Attributes.Remove("OnBlur");
            }
            else
            {
                this.DocumentNoRequiredFieldValidator.Enabled = false;

                this.BankGiroDropDownList.Enabled = false;
                this.BankGiroCustomValidator.Enabled = false;
                this.BankGiroDropDownList.Items.Clear();
                this.BankGiroDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

                //this.headline_date_start.Attributes.Add("Style", "visibility:hidden");
                this.DueDateLiteral.Text = "<input id='button1' type='button' Style = 'visibility:hidden' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.DueDateTextBox.Text = "";

                if (_fgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Bank))
                {
                    decimal _tempBankExpense = Convert.ToDecimal(_finChangeGiroInPay.BankExpense);
                    this.BankExpenseTextBox.Text = (_tempBankExpense == 0) ? "0" : _tempBankExpense.ToString("#,###.##"); ;

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

        private void ShowReceiptType()
        {
            this.ReceiptTypeDropDownList.Items.Clear();
            this.ReceiptTypeDropDownList.DataTextField = "PayName";
            this.ReceiptTypeDropDownList.DataValueField = "PayCode";
            this.ReceiptTypeDropDownList.DataSource = this._payBL.GetListDDLGiroReceiptChangePay();
            this.ReceiptTypeDropDownList.DataBind();
            this.ReceiptTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowBankGiro()
        {
            this.BankGiroDropDownList.Items.Clear();
            this.BankGiroDropDownList.DataTextField = "BankName";
            this.BankGiroDropDownList.DataValueField = "BankCode";
            this.BankGiroDropDownList.DataSource = this._bankBL.GetList();
            this.BankGiroDropDownList.DataBind();
            this.BankGiroDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINChangeGiroInPay _finChangeGiroInPay = this._finChangeGiroInBL.GetSingleFINChangeGiroInPay(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._itemKey), ApplicationConfig.EncryptionKey)));

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            _finChangeGiroInPay.ReceiptType = this.ReceiptTypeDropDownList.SelectedValue;
            _finChangeGiroInPay.DocumentNo = this.DocNoTextBox.Text;
            _finChangeGiroInPay.AmountForex = Convert.ToDecimal(this.AmountForexTextBox.Text);
            _finChangeGiroInPay.Remark = this.RemarkTextBox.Text;
            _finChangeGiroInPay.FgGiro = Convert.ToChar(this.FgGiroHiddenField.Value);
            if (this.BankExpenseTextBox.Text != "")
            {
                _finChangeGiroInPay.BankExpense = Convert.ToDecimal(this.BankExpenseTextBox.Text);
            }
            _finChangeGiroInPay.BankGiro = this.BankGiroDropDownList.SelectedValue;
            _finChangeGiroInPay.CurrCode = this.CurrTextBox.Text;
            _finChangeGiroInPay.ForexRate = Convert.ToDecimal(this.RateTextBox.Text);
            if (this.DueDateTextBox.Text != "")
            {
                _finChangeGiroInPay.DueDate = DateFormMapper.GetValue(this.DueDateTextBox.Text);
            }
            bool _result = this._finChangeGiroInBL.EditFINChangeGiroInPay(_finChangeGiroInPay);

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

        private void ClearData()
        {
            this.ClearLabel();

            this.ReceiptTypeDropDownList.SelectedValue = "null";
            this.DocNoTextBox.Text = "";
            this.CurrTextBox.Text = "";
            this.RateTextBox.Text = "";
            this.AmountForexTextBox.Text = "";
            this.DueDateTextBox.Text = "";
            this.BankGiroDropDownList.SelectedValue = "null";
            this.RemarkTextBox.Text = "";
            this.BankExpenseTextBox.Text = "";
            this.DueDateLiteral.Text = "<input id='button1' type='button' Style = 'visibility:hidden' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
            //this.headline_date_start.Visible = false;
            this.FgGiroHiddenField.Value = "";
        }

        protected void ReceiptTypeDropDownList_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (this.ReceiptTypeDropDownList.SelectedValue != "null")
            {
                this.CurrTextBox.Text = _payBL.GetCurrCodeByCode(this.ReceiptTypeDropDownList.SelectedValue);
                decimal _currRateValue = _currRateBL.GetSingleLatestCurrRate(this.CurrTextBox.Text);

                byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrTextBox.Text);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                string _defaultCurrency = this._currencyBL.GetCurrDefault();
                if (this.CurrTextBox.Text.Trim().ToLower() == _defaultCurrency.Trim().ToLower())
                {
                    this.RateTextBox.Attributes.Add("ReadOnly", "True");
                    this.RateTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
                    this.RateTextBox.Text = "1";
                }
                else
                {
                    this.RateTextBox.Attributes.Remove("ReadOnly");
                    this.RateTextBox.Attributes.Add("Style", "background-color:#FFFFFF");
                    this.RateTextBox.Text = (_currRateValue == 0 ? "0" : _currRateValue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));
                }

                char _fgMode = this._payBL.GetFgMode(this.ReceiptTypeDropDownList.SelectedValue);

                if (_fgMode == PaymentDataMapper.GetModeReceiptType(ModeReceiptType.Giro))
                {
                    this.FgGiroHiddenField.Value = (GiroReceiptDataMapper.GetYesNo(YesNo.Yes)).ToString();

                    this.DocumentNoRequiredFieldValidator.Enabled = true;

                    this.BankGiroDropDownList.Enabled = true;

                    this.DueDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
                    //this.headline_date_start.Attributes.Add("Style", "visibility:visible");
                    this.DueDateLiteral.Text = "<input id='button1' type='button' Style = 'visibility:visible' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                    this.BankExpenseTextBox.Text = "";
                    this.BankExpenseTextBox.Attributes.Add("ReadOnly", "true");
                    this.BankExpenseTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.BankExpenseTextBox.Attributes.Remove("OnBlur");
                }
                else
                {
                    this.FgGiroHiddenField.Value = (GiroReceiptDataMapper.GetYesNo(YesNo.No)).ToString();

                    this.DocumentNoRequiredFieldValidator.Enabled = false;

                    this.BankGiroDropDownList.SelectedValue = "null";
                    this.BankGiroDropDownList.Enabled = false;

                    this.DueDateTextBox.Text = "";
                    //this.headline_date_start.Attributes.Add("Style", "visibility:hidden");
                    this.DueDateLiteral.Text = "<input id='button1' type='button' Style = 'visibility:hidden' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                    if (_fgMode == PaymentDataMapper.GetModeReceiptType(ModeReceiptType.Bank))
                    {
                        this.BankExpenseTextBox.Attributes.Remove("ReadOnly");
                        this.BankExpenseTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                        this.BankExpenseTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.BankExpenseTextBox.ClientID + "," + this.DecimalPlaceHiddenFieldHome.ClientID + "  );");
                    }
                    else
                    {
                        this.BankExpenseTextBox.Text = "";
                        this.BankExpenseTextBox.Attributes.Add("ReadOnly", "true");
                        this.BankExpenseTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                        this.BankExpenseTextBox.Attributes.Remove("OnBlur");
                    }
                }
            }
            else
            {
                this.ClearData();
            }
        }

        private void SetCurrRate()
        {
            this.DecimalPlaceHiddenField.Value = this._currencyBL.GetCurrDefault();
        }
    }
}