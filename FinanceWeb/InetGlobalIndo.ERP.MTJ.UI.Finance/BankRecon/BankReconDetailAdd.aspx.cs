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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.BankRecon
{
    public partial class BankReconDetailAdd : BankReconBase
    {
        private BankReconBL _bankReconBL = new BankReconBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private AccountBL _accountBL = new AccountBL();
        private SubledBL _subledBL = new SubledBL();
        private JournalEntryBL _journalBL = new JournalEntryBL();
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

                this.ShowAccount();

                this.ClearLabel();
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
            this.ForexRateTextBox.Attributes.Add("OnBlur", "Calculate(" + this.ForexRateTextBox.ClientID + ", " + this.AmountForexTextBox.ClientID + ", " + this.DecimalPlaceHiddenField.ClientID + "); ");
            this.AmountForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.ForexRateTextBox.ClientID + ", " + this.AmountForexTextBox.ClientID + ", " + this.DecimalPlaceHiddenField.ClientID + "); ");
        }

        protected void SetAttribute()
        {
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.ForexRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot");
            this.AmountForexTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot");

            this.SetAttributeRate();
        }

        public void ClearData()
        {
            this.SubLedgerDropDownList.Items.Clear();
            this.SubLedgerDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.AccountTextBox.Text = "";
            this.AccountDropDownList.SelectedValue = "null";
            this.SubLedgerDropDownList.SelectedValue = "null";
            this.RemarkTextBox.Text = "";
            this.ForexRateTextBox.Text = "1";
            this.FgValueDDL.SelectedIndex = 0;
            this.AmountForexTextBox.Text = "0";
        }

        public void ShowSubled()
        {
            char _tempSubled = this._accountBL.GetAccountSubLed(this.AccountDropDownList.SelectedValue);
            this.FgSubledHiddenField.Value = _tempSubled.ToString();

            this.SubLedgerDropDownList.DataTextField = "SubLed_Name";
            this.SubLedgerDropDownList.DataValueField = "SubLed_No";
            this.SubLedgerDropDownList.DataSource = this._subledBL.GetListSubled(Convert.ToChar(this.FgSubledHiddenField.Value));
            this.SubLedgerDropDownList.DataBind();
            this.SubLedgerDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowAccount()
        {
            string _bankReconCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _currCodeHeader = _accountBL.GetCurrByAccCode(_bankReconBL.GetAccountByBankReconCode(new Guid(_bankReconCode)));

            this.AccountDropDownList.DataTextField = "AccountName";
            this.AccountDropDownList.DataValueField = "Account";
            this.AccountDropDownList.DataSource = this._accountBL.GetListForDDL(_currCodeHeader);
            this.AccountDropDownList.DataBind();
            this.AccountDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void AccountTextBox_TextChanged(object sender, EventArgs e)
        {
            bool _exist = this._accountBL.IsExistAccount(AppModule.GetValue(TransactionType.BankRecon), this.AccountTextBox.Text);
            if (_exist == true)
            {
                this.AccountDropDownList.SelectedValue = this.AccountTextBox.Text;

                string _currCode = this._accountBL.GetCurrByAccCode(this.AccountTextBox.Text);
                byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                if (_currCode == this._currencyBL.GetCurrDefault())
                {
                    this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.ForexRateTextBox.Text = "1";
                }
                else
                {
                    this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                }
            }
            else
            {
                this.AccountTextBox.Text = "";
                this.AccountDropDownList.SelectedValue = "null";
                this.DecimalPlaceHiddenField.Value = "";
                this.ForexRateTextBox.Text = "1";
            }
            this.FgValueDDL.SelectedIndex = 0;
            this.AmountForexTextBox.Text = "0";

            this.ShowSubled();
        }

        protected void AccountDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.AccountDropDownList.SelectedValue != "null")
            {
                this.AccountTextBox.Text = this.AccountDropDownList.SelectedValue;

                string _currCode = this._accountBL.GetCurrByAccCode(this.AccountTextBox.Text);
                byte _decimalPlace = this._currencyBL.GetDecimalPlace(this._accountBL.GetCurrByAccCode(this.AccountDropDownList.SelectedValue));
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                if (_currCode == this._currencyBL.GetCurrDefault())
                {
                    this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.ForexRateTextBox.Text = "1";
                }
                else
                {
                    this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                    this.ForexRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(_currCode).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                }
            }
            else
            {
                this.AccountTextBox.Text = "";
                this.DecimalPlaceHiddenField.Value = "";
                this.ForexRateTextBox.Text = "1";
            }
            this.FgValueDDL.SelectedIndex = 0;
            this.AmountForexTextBox.Text = "0";

            this.ShowSubled();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _bankReconCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            Finance_BankReconAccount _bankReconAccount = new Finance_BankReconAccount();

            _bankReconAccount.BankReconAccountCode = Guid.NewGuid();
            _bankReconAccount.BankReconCode = new Guid(_bankReconCode);
            _bankReconAccount.Account = this.AccountDropDownList.SelectedValue;
            if (this.SubLedgerDropDownList.SelectedValue != "null")
            {
                _bankReconAccount.FgSubLed = Convert.ToChar(this.FgSubledHiddenField.Value);
                _bankReconAccount.SubLed = this.SubLedgerDropDownList.SelectedValue;
            }
            else
            {
                _bankReconAccount.FgSubLed = 'N';
                _bankReconAccount.SubLed = null;
            }
            _bankReconAccount.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _bankReconAccount.FgValue = BankReconDataMapper.GetFgValue(this.FgValueDDL.SelectedValue);
            _bankReconAccount.AmountForex = Convert.ToDecimal(this.AmountForexTextBox.Text);
            _bankReconAccount.Remark = this.RemarkTextBox.Text;

            bool _result = this._bankReconBL.AddFinance_BankReconAccount(_bankReconAccount);

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
            this.ClearLabel();
            this.ClearData();
        }
    }
}