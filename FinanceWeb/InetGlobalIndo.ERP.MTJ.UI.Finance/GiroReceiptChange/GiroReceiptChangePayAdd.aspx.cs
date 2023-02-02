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
    public partial class GiroReceiptChangePayAdd : GiroReceiptChangeBase
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

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.DueDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowReceiptType();
                this.ShowBankGiro();
                this.SetCurrRate();
                this.SetAttribute();
                this.ClearData();
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
            this.ImageSpan.Visible = false;
            this.FgGiroHiddenField.Value = "";
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
            FINChangeGiroInPay _finChangeGiroInPay = new FINChangeGiroInPay();

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            int _maxItemNo = this._finChangeGiroInBL.GetMaxNoItemFINChangeGiroInPay(_transNo);

            _finChangeGiroInPay.TransNmbr = _transNo;
            _finChangeGiroInPay.ItemNo = _maxItemNo + 1;
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

            bool _result = this._finChangeGiroInBL.AddFINChangeGiroInPay(_finChangeGiroInPay);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
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
                    this.ImageSpan.Visible = true;

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
                    this.ImageSpan.Visible = false;

                    if (_fgMode == PaymentDataMapper.GetModeReceiptType(ModeReceiptType.Bank))
                    {
                        this.BankExpenseTextBox.Attributes.Remove("ReadOnly");
                        this.BankExpenseTextBox.Attributes.Add("style", "background-color:#ffffff");
                        this.BankExpenseTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.BankExpenseTextBox.ClientID + "," + this.DecimalPlaceHiddenFieldHome.ClientID + "  );");
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

        private void SetCurrRate()
        {
            this.DecimalPlaceHiddenField.Value = this._currencyBL.GetCurrDefault();
        }
    }
}