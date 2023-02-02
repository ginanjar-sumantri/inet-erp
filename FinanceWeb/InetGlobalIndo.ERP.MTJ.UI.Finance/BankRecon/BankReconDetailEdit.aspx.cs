using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.BankRecon
{
    public partial class BankReconDetailEdit : BankReconBase
    {
        private BankReconBL _bankReconBL = new BankReconBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private AccountBL _accountBL = new AccountBL();
        private SubledBL _subledBL = new SubledBL();
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
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

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
            this.ForexRateTextBox.Attributes.Add("OnBlur", "Calculate(" + this.ForexRateTextBox.ClientID + ", " + this.AmountForexTextBox.ClientID + ", " + this.DecimalPlaceHiddenField.ClientID + "); ");
            this.AmountForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.ForexRateTextBox.ClientID + ", " + this.AmountForexTextBox.ClientID + ", " + this.DecimalPlaceHiddenField.ClientID + "); ");
        }

        protected void SetAttribute()
        {
            this.AccountTextBox.Attributes.Add("ReadOnly", "True");
            this.AccountNameTextBox.Attributes.Add("ReadOnly", "True");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.ForexRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot");
            this.AmountForexTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot");

            this.SetAttributeRate();
        }

        public void ShowSubled()
        {
            char _tempSubled = this._accountBL.GetAccountSubLed(this.AccountTextBox.Text);
            this.FgSubledHiddenField.Value = _tempSubled.ToString();

            this.SubLedgerDropDownList.DataTextField = "SubLed_Name";
            this.SubLedgerDropDownList.DataValueField = "SubLed_No";
            this.SubLedgerDropDownList.DataSource = this._subledBL.GetListSubled(Convert.ToChar(this.FgSubledHiddenField.Value));
            this.SubLedgerDropDownList.DataBind();
            this.SubLedgerDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowData()
        {
            this.DecimalPlaceHiddenField.Value = "";

            Finance_BankReconAccount _bankReconAccount = this._bankReconBL.GetSingleFinance_BankReconAccount(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItem), ApplicationConfig.EncryptionKey));

            string _currCode = this._accountBL.GetCurrByAccCode(this.AccountTextBox.Text);
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            this.AccountTextBox.Text = _bankReconAccount.Account;
            this.AccountNameTextBox.Text = this._accountBL.GetAccountNameByCode(_bankReconAccount.Account);
            this.ShowSubled();
            this.SubLedgerDropDownList.SelectedValue = _bankReconAccount.SubLed;
            this.FgSubledHiddenField.Value = _bankReconAccount.FgSubLed.ToString();
            this.ForexRateTextBox.Text = (_bankReconAccount.ForexRate == 0 ? "0" : _bankReconAccount.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));
            this.FgValueDDL.SelectedValue = BankReconDataMapper.GetFgValue(_bankReconAccount.FgValue);
            this.AmountForexTextBox.Text = (_bankReconAccount.AmountForex == 0 ? "0" : _bankReconAccount.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));
            this.RemarkTextBox.Text = _bankReconAccount.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            Finance_BankReconAccount _bankReconAccount = this._bankReconBL.GetSingleFinance_BankReconAccount(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItem), ApplicationConfig.EncryptionKey));

            _bankReconAccount.Account = this.AccountTextBox.Text;
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

            bool _result = this._bankReconBL.EditFinance_BankReconAccount(_bankReconAccount);

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