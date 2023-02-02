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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PaymentNonPurchase
{
    public partial class PaymentNonPurchaseCrEdit : PaymentNonPurchaseBase
    {
        private AccountBL _accountBL = new AccountBL();
        private SubledBL _subledBL = new SubledBL();
        private PaymentNonPurchaseBL _paymentNonPurchaseBL = new PaymentNonPurchaseBL();
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

                this.ShowPayment();
                this.ShowBankPayment();

                this.ClearLabel();
                this.ShowData();
                this.SetAttribute();
            }
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.NominalTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.NominalTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.BankChargesTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.BankChargesTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.DateTextBox.Attributes.Add("ReadOnly", "true");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowBankPayment()
        {
            string _currCodeHeader = _paymentNonPurchaseBL.GetCurrCodeHeader(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_currCodeHeader != "")
            {
                this.BankPaymentDropDownList.Items.Clear();
                this.BankPaymentDropDownList.DataTextField = "PayName";
                this.BankPaymentDropDownList.DataValueField = "PayCode";
                this.BankPaymentDropDownList.DataSource = this._paymentBL.GetListDDLbyViewMsBankPayment(_currCodeHeader);
                this.BankPaymentDropDownList.DataBind();
                this.BankPaymentDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
            else
            {
                this.BankPaymentDropDownList.Items.Clear();
                this.BankPaymentDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
        }

        public void ShowPayment()
        {
            string _currCodeHeader = _paymentNonPurchaseBL.GetCurrCodeHeader(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_currCodeHeader != "")
            {
                this.PaymentTypeDropDownList.Items.Clear();
                this.PaymentTypeDropDownList.DataTextField = "PayName";
                this.PaymentTypeDropDownList.DataValueField = "PayCode";
                this.PaymentTypeDropDownList.DataSource = this._paymentBL.GetListDDLbyViewMsPayType(_currCodeHeader);
                this.PaymentTypeDropDownList.DataBind();
                this.PaymentTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
            else
            {
                this.PaymentTypeDropDownList.Items.Clear();
                this.PaymentTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
        }

        protected void PaymentTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            char _fgMode = this._paymentBL.GetFgMode(this.PaymentTypeDropDownList.SelectedValue);

            if (_fgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Giro))
            {
                this.FgGiroHiddenField.Value = (YesNoDataMapper.GetYesNo(YesNo.Yes)).ToString();

                this.DocumentNoRequiredFieldValidator.Enabled = true;

                this.BankPaymentDropDownList.Enabled = true;

                this.ImageSpan.Visible = true;

                this.BankChargesTextBox.Text = "";
                this.BankChargesTextBox.Attributes.Add("ReadOnly", "true");
                this.BankChargesTextBox.Attributes.Add("style", "background-color:#cccccc");
                this.BankChargesTextBox.Attributes.Remove("OnBlur");
            }
            else
            {
                this.FgGiroHiddenField.Value = (YesNoDataMapper.GetYesNo(YesNo.No)).ToString();

                this.DocumentNoRequiredFieldValidator.Enabled = false;

                this.BankPaymentDropDownList.SelectedValue = "null";
                this.BankPaymentDropDownList.Enabled = false;

                this.DateTextBox.Text = "";
                this.ImageSpan.Visible = false;

                if (_fgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Bank))
                {
                    this.BankChargesTextBox.Attributes.Remove("ReadOnly");
                    this.BankChargesTextBox.Attributes.Add("style", "background-color:#ffffff");
                    this.BankChargesTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.BankChargesTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
                }
                else
                {
                    this.BankChargesTextBox.Text = "";
                    this.BankChargesTextBox.Attributes.Add("ReadOnly", "true");
                    this.BankChargesTextBox.Attributes.Add("style", "background-color:#cccccc");
                    this.BankChargesTextBox.Attributes.Remove("OnBlur");
                }
            }
        }

        public void ShowData()
        {
            FINPayNonTradeCr _finPayNonTradeCr = this._paymentNonPurchaseBL.GetSingleFINPayNonTradeCr(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

            string _currCodeHeader = _paymentNonPurchaseBL.GetCurrCodeHeader(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            byte _decimalPlace = _currBL.GetDecimalPlace(_currCodeHeader);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            this.PaymentTypeDropDownList.SelectedValue = _finPayNonTradeCr.PayType;
            this.FgGiroHiddenField.Value = _finPayNonTradeCr.FgGiro.ToString();

            this.NominalTextBox.Text = (_finPayNonTradeCr.AmountForex == 0 ? "0" : _finPayNonTradeCr.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));

            char _fgMode = this._paymentBL.GetFgMode(_finPayNonTradeCr.PayType);

            if (_fgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Giro))
            {
                this.FgGiroHiddenField.Value = (YesNoDataMapper.GetYesNo(YesNo.Yes)).ToString();

                this.DocumentNoTextBox.Text = _finPayNonTradeCr.DocumentNo;
                this.DocumentNoRequiredFieldValidator.Enabled = true;

                this.BankPaymentDropDownList.Enabled = true;
                this.BankPaymentDropDownList.SelectedValue = _finPayNonTradeCr.BankPayment;
                this.DateTextBox.Text = DateFormMapper.GetValue(_finPayNonTradeCr.DueDate);

                this.ImageSpan.Visible = true;
                if (_finPayNonTradeCr.BankExpense != null)
                {
                    this.BankChargesTextBox.Text = (_finPayNonTradeCr.BankExpense == 0) ? "0" : Convert.ToDecimal(_finPayNonTradeCr.BankExpense).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                }
                else
                {
                    this.BankChargesTextBox.Text = "0";
                }
                this.BankChargesTextBox.Attributes.Add("ReadOnly", "true");
                this.BankChargesTextBox.Attributes.Add("style", "background-color:#cccccc");
                this.BankChargesTextBox.Attributes.Remove("OnBlur");
            }
            else
            {
                this.FgGiroHiddenField.Value = (YesNoDataMapper.GetYesNo(YesNo.No)).ToString();

                this.DocumentNoTextBox.Text = _finPayNonTradeCr.DocumentNo;
                this.DocumentNoRequiredFieldValidator.Enabled = false;

                this.BankPaymentDropDownList.SelectedValue = "null";
                this.BankPaymentDropDownList.Enabled = false;

                this.DateTextBox.Text = DateFormMapper.GetValue(_finPayNonTradeCr.DueDate);
                this.ImageSpan.Visible = false;

                if (_fgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Bank))
                {
                    this.BankChargesTextBox.Attributes.Remove("ReadOnly");
                    this.BankChargesTextBox.Attributes.Add("style", "background-color:#ffffff");
                    this.BankChargesTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.BankChargesTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
                }
                else
                {
                    this.BankChargesTextBox.Attributes.Add("ReadOnly", "true");
                    this.BankChargesTextBox.Attributes.Add("style", "background-color:#cccccc");
                    this.BankChargesTextBox.Attributes.Remove("OnBlur");
                }
                if (_finPayNonTradeCr.BankExpense != null)
                {
                    this.BankChargesTextBox.Text = (_finPayNonTradeCr.BankExpense == 0) ? "0" : Convert.ToDecimal(_finPayNonTradeCr.BankExpense).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                }
                else
                {
                    this.BankChargesTextBox.Text = "0";
                }
            }

            this.RemarkTextBox.Text = _finPayNonTradeCr.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINPayNonTradeCr _finPayNonTradeCr = this._paymentNonPurchaseBL.GetSingleFINPayNonTradeCr(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

            _finPayNonTradeCr.PayType = this.PaymentTypeDropDownList.SelectedValue;
            _finPayNonTradeCr.DocumentNo = this.DocumentNoTextBox.Text;
            _finPayNonTradeCr.AmountForex = Convert.ToDecimal(this.NominalTextBox.Text);
            _finPayNonTradeCr.Remark = this.RemarkTextBox.Text;
            _finPayNonTradeCr.FgGiro = Convert.ToChar(this.FgGiroHiddenField.Value);
            _finPayNonTradeCr.BankPayment = (this.BankPaymentDropDownList.SelectedValue != "null") ? this.BankPaymentDropDownList.SelectedValue : "";
            if (this.DateTextBox.Text != "")
            {
                _finPayNonTradeCr.DueDate = DateFormMapper.GetValue(this.DateTextBox.Text);
            }
            else
            {
                _finPayNonTradeCr.DueDate = null;
            }
            _finPayNonTradeCr.BankExpense = (this.BankChargesTextBox.Text == "") ? 0 : Convert.ToDecimal(this.BankChargesTextBox.Text);

            bool _result = this._paymentNonPurchaseBL.EditFINPayNonTradeCr(_finPayNonTradeCr);

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