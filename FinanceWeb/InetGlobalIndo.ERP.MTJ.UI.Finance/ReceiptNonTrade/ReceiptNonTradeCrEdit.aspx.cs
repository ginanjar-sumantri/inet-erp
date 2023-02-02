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
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.ReceiptNonTrade
{
    public partial class ReceiptNonTradeCrEdit : ReceiptNonTradeBase
    {
        private AccountBL _accountBL = new AccountBL();
        private SubledBL _subledBL = new SubledBL();
        private PaymentNonTradeBL _paymentNonTradeBL = new PaymentNonTradeBL();
        private EmployeeBL _empBL = new EmployeeBL();
        private User_EmployeeBL _userEmpBL = new User_EmployeeBL();
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
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowAccount();
                this.GetSubled();

                this.ClearLabel();
                this.ShowData();
                this.SetAttribute();
            }
        }

        private void SetAttribute()
        {
            this.NominalTextBox.Attributes.Add("OnKeyDown", "return Numeric();");
            this.NominalTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.NominalTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowAccount()
        {
            FINReceiptNonTradeHd _finReceiptNonTradeHd = this._paymentNonTradeBL.GetSingleFINReceiptNonTradeHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.AccountDropDownList.Items.Clear();
            this.AccountDropDownList.DataTextField = "AccountName";
            this.AccountDropDownList.DataValueField = "Account";
            this.AccountDropDownList.DataSource = this._accountBL.GetListAccountByTransType(_finReceiptNonTradeHd.CurrCode, _userEmpBL.GetEmployeeIDByUserName(HttpContext.Current.User.Identity.Name), AppModule.GetValue(TransactionType.ReceiptNonTrade));
            this.AccountDropDownList.DataBind();
            this.AccountDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void GetSubled()
        {
            char _tempSubled = this._accountBL.GetAccountSubLed(this.AccountDropDownList.SelectedValue);
            this.FgSubledHiddenField.Value = _tempSubled.ToString();

            this.SubledDropDownList.Items.Clear();
            this.SubledDropDownList.DataTextField = "SubLed_Name";
            this.SubledDropDownList.DataValueField = "SubLed_No";
            this.SubledDropDownList.DataSource = this._subledBL.GetListSubled(Convert.ToChar(this.FgSubledHiddenField.Value));
            this.SubledDropDownList.DataBind();
            this.SubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowData()
        {
            FINReceiptNonTradeCr _finReceiptNonTradeCr = this._paymentNonTradeBL.GetSingleFINReceiptNonTradeCr(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));
            FINReceiptNonTradeHd _finReceiptNonTradeHd = _paymentNonTradeBL.GetSingleFINReceiptNonTradeHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finReceiptNonTradeHd.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.AccountTextBox.Text = _finReceiptNonTradeCr.Account;
            this.AccountDropDownList.SelectedValue = _finReceiptNonTradeCr.Account;
            this.GetSubled();
            this.SubledDropDownList.SelectedValue = _finReceiptNonTradeCr.SubLed;
            this.FgSubledHiddenField.Value = _finReceiptNonTradeCr.FgSubLed.ToString();
            this.NominalTextBox.Text = (_finReceiptNonTradeCr.AmountForex == 0 ? "0" : _finReceiptNonTradeCr.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));
            this.RemarkTextBox.Text = _finReceiptNonTradeCr.Remark;
        }

        protected void AccountDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.AccountTextBox.Text = this.AccountDropDownList.SelectedValue;
            this.GetSubled();
        }

        protected void AccountTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.AccountDropDownList.SelectedValue = this.AccountTextBox.Text;
                this.GetSubled();
            }
            catch (Exception ex)
            {
                this.AccountTextBox.Text = "";
                this.AccountDropDownList.SelectedValue = "null";
            }

        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINReceiptNonTradeCr _finReceiptNonTradeCr = this._paymentNonTradeBL.GetSingleFINReceiptNonTradeCr(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

            _finReceiptNonTradeCr.Account = this.AccountTextBox.Text;
            if (this.SubledDropDownList.SelectedValue != "null")
            {
                _finReceiptNonTradeCr.FgSubLed = Convert.ToChar(this.FgSubledHiddenField.Value);
                _finReceiptNonTradeCr.SubLed = this.SubledDropDownList.SelectedValue;
            }
            else
            {
                _finReceiptNonTradeCr.FgSubLed = 'N';
                _finReceiptNonTradeCr.SubLed = null;
            }
            _finReceiptNonTradeCr.AmountForex = Convert.ToDecimal(this.NominalTextBox.Text);
            _finReceiptNonTradeCr.Remark = this.RemarkTextBox.Text;

            bool _result = this._paymentNonTradeBL.EditFINReceiptNonTradeCr(_finReceiptNonTradeCr);

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