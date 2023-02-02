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
    public partial class PaymentNonPurchaseCrAdd : PaymentNonPurchaseBase
    {
        private AccountBL _account = new AccountBL();
        private SubledBL _subled = new SubledBL();
        private PaymentNonPurchaseBL _paymentNonPurchase = new PaymentNonPurchaseBL();
        private PaymentBL _payment = new PaymentBL();
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
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowPayment();
                this.ShowBankPayment();

                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void ClearLabel()
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

        public void ClearData()
        {
            this.ClearLabel();

            this.PaymentTypeDropDownList.SelectedValue = "null";
            this.FgGiroHiddenField.Value = "";
            this.DocumentNoTextBox.Text = "";
            this.DocumentNoRequiredFieldValidator.Enabled = false;
            this.NominalTextBox.Text = "0";
            this.BankPaymentDropDownList.SelectedValue = "null";
            this.BankPaymentDropDownList.Enabled = false;
            this.BankPaymentCustomValidator.Enabled = false;
            this.DateTextBox.Text = "";
            this.ImageSpan.Visible = false;
            this.BankChargesTextBox.Text = "";
            this.BankChargesTextBox.Attributes.Add("ReadOnly", "true");
            this.BankChargesTextBox.Attributes.Add("style", "background-color:#cccccc");
            this.RemarkTextBox.Text = "";
            this.DecimalPlaceHiddenField.Value = "";
            
            string _currCodeHeader = _paymentNonPurchase.GetCurrCodeHeader(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            byte _decimalPlace = _currBL.GetDecimalPlace(_currCodeHeader);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
        }

        public void ShowBankPayment()
        {
            string _currCodeHeader = _paymentNonPurchase.GetCurrCodeHeader(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            
            if (_currCodeHeader != "")
            {
                this.BankPaymentDropDownList.Items.Clear();
                this.BankPaymentDropDownList.DataTextField = "PayName";
                this.BankPaymentDropDownList.DataValueField = "PayCode";
                this.BankPaymentDropDownList.DataSource = this._payment.GetListDDLbyViewMsBankPayment(_currCodeHeader);
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
            string _currCodeHeader = _paymentNonPurchase.GetCurrCodeHeader(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_currCodeHeader != "")
            {
                this.PaymentTypeDropDownList.Items.Clear();
                this.PaymentTypeDropDownList.DataTextField = "PayName";
                this.PaymentTypeDropDownList.DataValueField = "PayCode";
                this.PaymentTypeDropDownList.DataSource = this._payment.GetListDDLbyViewMsPayType(_currCodeHeader);
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
            char _fgMode = this._payment.GetFgMode(this.PaymentTypeDropDownList.SelectedValue);

            if (_fgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Giro))
            {
                this.FgGiroHiddenField.Value = (YesNoDataMapper.GetYesNo(YesNo.Yes)).ToString();

                this.DocumentNoRequiredFieldValidator.Enabled = true;

                this.BankPaymentDropDownList.Enabled = true;
                this.BankPaymentCustomValidator.Enabled = true;

                this.DateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
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
                this.BankPaymentCustomValidator.Enabled = false;

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

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            int _maxItemNo = this._paymentNonPurchase.GetMaxNoItemFINPayNonTradeCr(_transNo);

            FINPayNonTradeCr _finPayNonTradeCr = new FINPayNonTradeCr();

            _finPayNonTradeCr.TransNmbr = _transNo;
            _finPayNonTradeCr.ItemNo = _maxItemNo + 1;
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


            bool _result = this._paymentNonPurchase.AddFINPayNonTradeCr(_finPayNonTradeCr);

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