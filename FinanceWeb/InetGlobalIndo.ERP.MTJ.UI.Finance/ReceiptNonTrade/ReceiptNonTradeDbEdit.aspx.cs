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
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.ReceiptNonTrade
{
    public partial class ReceiptNonTradeDbEdit : ReceiptNonTradeBase
    {
        private PaymentNonTradeBL _paymentNonTradeBL = new PaymentNonTradeBL();
        private PaymentBL _paymentBL = new PaymentBL();
        private BankBL _bankBL = new BankBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();

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
                this.DateLiteral.Text = "<input id='button1' type='button' Style='visibility:visible' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowReceipt();
                this.ShowBankGiro();

                this.ClearLabel();
                this.ShowData();
                this.SetAttribute();
            }
        }

        private void SetAttribute()
        {
            this.NominalTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.NominalTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.NominalTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.BankExpenseTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.BankExpenseTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.BankExpenseTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.DateTextBox.Attributes.Add("ReadOnly", "true");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowBankGiro()
        {
            string _currCodeHeader = _paymentNonTradeBL.GetCurrCodeHeader(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_currCodeHeader != "")
            {
                this.BankGiroDropDownList.Items.Clear();
                this.BankGiroDropDownList.DataTextField = "BankName";
                this.BankGiroDropDownList.DataValueField = "BankCode";
                this.BankGiroDropDownList.DataSource = this._bankBL.GetList();
                this.BankGiroDropDownList.DataBind();
                this.BankGiroDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
            else
            {
                this.BankGiroDropDownList.Items.Clear();
                this.BankGiroDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
        }

        public void ShowReceipt()
        {
            string _currCodeHeader = _paymentNonTradeBL.GetCurrCodeHeader(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_currCodeHeader != "")
            {
                this.ReceiptTypeDropDownList.Items.Clear();
                this.ReceiptTypeDropDownList.DataTextField = "PayName";
                this.ReceiptTypeDropDownList.DataValueField = "PayCode";
                this.ReceiptTypeDropDownList.DataSource = this._paymentBL.GetListDDLReceiptNonTrade(_currCodeHeader);
                this.ReceiptTypeDropDownList.DataBind();
                this.ReceiptTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
            else
            {
                this.ReceiptTypeDropDownList.Items.Clear();
                this.ReceiptTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
        }

        protected void ReceiptTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            char _fgMode = this._paymentBL.GetFgMode(this.ReceiptTypeDropDownList.SelectedValue);

            if (_fgMode == PaymentDataMapper.GetModeReceiptType(ModeReceiptType.Giro))
            {
                this.FgGiroHiddenField.Value = (YesNoDataMapper.GetYesNo(YesNo.Yes)).ToString();

                this.DocumentNoRequiredFieldValidator.Enabled = true;

                this.BankGiroDropDownList.Enabled = true;
                this.BankGiroCustomValidator.Enabled = true;

                //this.headline_date_start.Attributes.Add("Style", "visibility:visible");
                this.DateLiteral.Text = "<input id='button1' type='button' Style='visibility:visible' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.BankExpenseTextBox.Text = "";
                this.BankExpenseTextBox.Attributes.Add("ReadOnly", "true");
                this.BankExpenseTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                this.BankExpenseTextBox.Attributes.Remove("OnBlur");
            }
            else
            {
                this.FgGiroHiddenField.Value = (YesNoDataMapper.GetYesNo(YesNo.No)).ToString();

                this.DocumentNoRequiredFieldValidator.Enabled = false;

                this.BankGiroDropDownList.SelectedValue = "null";
                this.BankGiroDropDownList.Enabled = false;
                this.BankGiroCustomValidator.Enabled = false;

                this.DateTextBox.Text = "";
                //this.headline_date_start.Attributes.Add("Style", "visibility:hidden");
                this.DateLiteral.Text = "<input id='button1' type='button' Style='visibility:hidden' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                if (_fgMode == PaymentDataMapper.GetModeReceiptType(ModeReceiptType.Bank))
                {
                    this.BankExpenseTextBox.Attributes.Remove("ReadOnly");
                    this.BankExpenseTextBox.Attributes.Add("style", "background-color:#ffffff");
                    this.BankExpenseTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.BankExpenseTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
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

        public void ShowData()
        {
            FINReceiptNonTradeDb _finReceiptNonTradeDb = this._paymentNonTradeBL.GetSingleFINReceiptNonTradeDb(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));
            FINReceiptNonTradeHd _finReceiptNonTrade = _paymentNonTradeBL.GetSingleFINReceiptNonTradeHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finReceiptNonTrade.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.ReceiptTypeDropDownList.SelectedValue = _finReceiptNonTradeDb.ReceiptType;
            this.FgGiroHiddenField.Value = _finReceiptNonTradeDb.FgGiro.ToString();
            char _fgMode = this._paymentBL.GetFgMode(this.ReceiptTypeDropDownList.SelectedValue);

            this.NominalTextBox.Text = (_finReceiptNonTradeDb.AmountForex == 0 ? "0" : _finReceiptNonTradeDb.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));

            if (_fgMode == PaymentDataMapper.GetModeReceiptType(ModeReceiptType.Giro))
            {
                this.FgGiroHiddenField.Value = (YesNoDataMapper.GetYesNo(YesNo.Yes)).ToString();

                this.DocumentNoRequiredFieldValidator.Enabled = true;
                this.DocumentNoTextBox.Text = _finReceiptNonTradeDb.DocumentNo;

                this.BankGiroDropDownList.Enabled = true;
                this.BankGiroCustomValidator.Enabled = true;
                this.BankGiroDropDownList.SelectedValue = _finReceiptNonTradeDb.BankGiro;

                this.DateTextBox.Text = DateFormMapper.GetValue(_finReceiptNonTradeDb.DueDate);
                //this.headline_date_start.Attributes.Add("Style", "visibility:visible");
                this.DateLiteral.Text = "<input id='button1' type='button' Style='visibility:visible' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.BankExpenseTextBox.Text = "";
                this.BankExpenseTextBox.Attributes.Add("ReadOnly", "true");
                this.BankExpenseTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                this.BankExpenseTextBox.Attributes.Remove("OnBlur");
            }
            else
            {
                this.FgGiroHiddenField.Value = (YesNoDataMapper.GetYesNo(YesNo.No)).ToString();

                this.DocumentNoRequiredFieldValidator.Enabled = false;
                this.DocumentNoTextBox.Text = "";

                this.BankGiroDropDownList.Enabled = false;
                this.BankGiroCustomValidator.Enabled = false;
                this.BankGiroDropDownList.SelectedValue = "null";

                this.DateTextBox.Text = "";
                //this.headline_date_start.Attributes.Add("Style", "visibility:hidden");
                this.DateLiteral.Text = "<input id='button1' type='button' Style='visibility:hidden' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                if (_fgMode == PaymentDataMapper.GetModeReceiptType(ModeReceiptType.Bank))
                {
                    this.BankExpenseTextBox.Attributes.Remove("ReadOnly");
                    this.BankExpenseTextBox.Attributes.Add("style", "background-color:#ffffff");
                    this.BankExpenseTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.BankExpenseTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
                    this.BankExpenseTextBox.Text = (_finReceiptNonTradeDb.BankExpense == null || _finReceiptNonTradeDb.BankExpense == 0) ? "0" : Convert.ToDecimal(_finReceiptNonTradeDb.BankExpense).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                }
                else
                {
                    this.BankExpenseTextBox.Text = "";
                    this.BankExpenseTextBox.Attributes.Add("ReadOnly", "true");
                    this.BankExpenseTextBox.Attributes.Add("style", "background-color:#cccccc");
                    this.BankExpenseTextBox.Attributes.Remove("OnBlur");
                }
            }

            this.RemarkTextBox.Text = _finReceiptNonTradeDb.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINReceiptNonTradeDb _finReceiptNonTradeDb = this._paymentNonTradeBL.GetSingleFINReceiptNonTradeDb(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

            _finReceiptNonTradeDb.ReceiptType = this.ReceiptTypeDropDownList.SelectedValue;
            _finReceiptNonTradeDb.DocumentNo = this.DocumentNoTextBox.Text;
            _finReceiptNonTradeDb.AmountForex = Convert.ToDecimal(this.NominalTextBox.Text);
            _finReceiptNonTradeDb.Remark = this.RemarkTextBox.Text;
            _finReceiptNonTradeDb.FgGiro = Convert.ToChar(this.FgGiroHiddenField.Value);
            _finReceiptNonTradeDb.BankGiro = (this.BankGiroDropDownList.SelectedValue != "null") ? this.BankGiroDropDownList.SelectedValue : "";
            if (this.DateTextBox.Text != "")
            {
                _finReceiptNonTradeDb.DueDate = DateFormMapper.GetValue(this.DateTextBox.Text);
            }
            else
            {
                _finReceiptNonTradeDb.DueDate = null;
            }
            _finReceiptNonTradeDb.BankExpense = (this.BankExpenseTextBox.Text == "") ? 0 : Convert.ToDecimal(this.BankExpenseTextBox.Text);

            bool _result = this._paymentNonTradeBL.EditFINReceiptNonTradeDb(_finReceiptNonTradeDb);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }
        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}