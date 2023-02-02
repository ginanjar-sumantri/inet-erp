using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PaymentType
{
    public partial class PaymentTypeEdit : PaymentTypeBase
    {
        private PaymentBL _paymentBL = new PaymentBL();
        private AccountBL _accountBL = new AccountBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private BankBL _bankBL = new BankBL();
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

                this.ShowAccount();
                this.ShowBank();

                this.ShowData();
                this.ClearLabel();
                this.SetAttribute();
            }
        }

        private void SetAttribute()
        {
            this.ModeDropDownList.Attributes.Add("OnChange", "EnableOrDisable(" + this.ModeDropDownList.ClientID + "," + this.BankDropDownList.ClientID + "," + this.NoRekeningTextBox.ClientID + "," + this.NoRekeningOwnerTextBox.ClientID + "," + this.AccBankChargeTextBox.ClientID + "," + this.AccBankChargeDropDownList.ClientID + "," + this.ExpenseGiroTextBox.ClientID + "," + this.AccCustChargeTextBox.ClientID + "," + this.AccCustChargeDropDownList.ClientID + "," + this.CustChargeRevenueTextBox.ClientID + "," + this.FgBankChargeRadioButtonList.ClientID + "," + this.CustChargeRevenueRadioButtonList.ClientID + ");");

            this.ExpenseGiroTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.ExpenseGiroTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.ExpenseGiroTextBox.ClientID + "," + this.BankChargeDecimalPlaceHiddenField.ClientID + ");");
            this.CustChargeRevenueTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.CustChargeRevenueTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CustChargeRevenueTextBox.ClientID + "," + this.CustChargeDecimalPlaceHiddenField.ClientID + ");");
            this.NoRekeningTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");

            this.AccountDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountDropDownList.ClientID + "," + this.AccountTextBox.ClientID + ");");
            this.AccountTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountDropDownList.ClientID + "," + this.AccountTextBox.ClientID + ");");

            this.AccBankChargeDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccBankChargeDropDownList.ClientID + "," + this.AccBankChargeTextBox.ClientID + ");");
            this.AccBankChargeTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccBankChargeDropDownList.ClientID + "," + this.AccBankChargeTextBox.ClientID + ");");

            this.AccCustChargeDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccCustChargeDropDownList.ClientID + "," + this.AccCustChargeTextBox.ClientID + ");");
            this.AccCustChargeTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccCustChargeDropDownList.ClientID + "," + this.AccCustChargeTextBox.ClientID + ");");
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            MsPayType _msPayType = this._paymentBL.GetSinglePaymentType(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlaceBankCharge = _currencyBL.GetDecimalPlace(_accountBL.GetCurrByAccCode(_msPayType.AccBankCharge));
            byte _decimalPlaceCustCharge = _currencyBL.GetDecimalPlace(_accountBL.GetCurrByAccCode(_msPayType.AccCustCharge));

            this.CustChargeDecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlaceCustCharge);
            this.BankChargeDecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlaceBankCharge);
            this.PaymentCodeTextBox.Text = _msPayType.PayCode;
            this.PaymentNameTextBox.Text = _msPayType.PayName;
            this.AccountTextBox.Text = _msPayType.Account;
            this.AccountDropDownList.SelectedValue = _msPayType.Account;
            this.ModeDropDownList.SelectedValue = _msPayType.FgMode.ToString();
            if (_msPayType.FgMode != 'B')
            {
                this.BankDropDownList.Attributes.Add("Disabled", "True");

                this.NoRekeningTextBox.Attributes.Add("ReadOnly", "True");
                this.NoRekeningTextBox.Attributes.Add("style", "background-color:#CCCCCC");

                this.NoRekeningOwnerTextBox.Attributes.Add("ReadOnly", "True");
                this.NoRekeningOwnerTextBox.Attributes.Add("style", "background-color:#CCCCCC");

                this.AccBankChargeTextBox.Attributes.Add("ReadOnly", "True");
                this.AccBankChargeTextBox.Attributes.Add("style", "background-color:#CCCCCC");

                this.AccBankChargeDropDownList.Attributes.Add("Disabled", "True");

                this.ExpenseGiroTextBox.Attributes.Add("ReadOnly", "True");
                this.ExpenseGiroTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                this.ExpenseGiroTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.ExpenseGiroTextBox.ClientID + "," + this.BankChargeDecimalPlaceHiddenField.ClientID + ");");

                this.AccCustChargeTextBox.Attributes.Add("ReadOnly", "True");
                this.AccCustChargeTextBox.Attributes.Add("style", "background-color:#CCCCCC");

                this.AccCustChargeDropDownList.Attributes.Add("Disabled", "True");

                this.CustChargeRevenueTextBox.Attributes.Add("ReadOnly", "True");
                this.CustChargeRevenueTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                this.CustChargeRevenueTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CustChargeRevenueTextBox.ClientID + "," + this.CustChargeDecimalPlaceHiddenField.ClientID + ");");
            }
            this.CustChargeRevenueRadioButtonList.SelectedValue = PaymentDataMapper.IsCheckedBool(_msPayType.FgCustCharge);
            this.FgBankChargeRadioButtonList.SelectedValue = PaymentDataMapper.IsCheckedBool(_msPayType.FgBankCharge);
            this.TypeDropDownList.SelectedValue = _msPayType.FgType.ToString();
            if (_msPayType.Bank != "" || _msPayType.Bank != null)
            {
                this.BankDropDownList.SelectedValue = _msPayType.Bank;
            }
            this.NoRekeningTextBox.Text = _msPayType.NoRekening;
            this.NoRekeningOwnerTextBox.Text = _msPayType.NoRekeningOwner;
            this.AccBankChargeTextBox.Text = _msPayType.AccBankCharge;
            this.AccCustChargeTextBox.Text = _msPayType.AccCustCharge;
            if (_msPayType.AccBankCharge != "" || _msPayType.AccBankCharge != null)
            {
                this.AccBankChargeDropDownList.SelectedValue = _msPayType.AccBankCharge;
            }
            if (_msPayType.AccCustCharge != "" || _msPayType.AccCustCharge != null)
            {
                this.AccCustChargeDropDownList.SelectedValue = _msPayType.AccCustCharge;
            }
            this.ExpenseGiroTextBox.Text = (_msPayType.ExpenseGiro == 0) ? "0" : _msPayType.ExpenseGiro.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceBankCharge));
            this.CustChargeRevenueTextBox.Text = (_msPayType.CustRevenue == 0) ? "0" : _msPayType.CustRevenue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceCustCharge));
        }

        public void ShowAccount()
        {
            this.AccountDropDownList.DataTextField = "AccountName";
            this.AccountDropDownList.DataValueField = "Account";
            this.AccountDropDownList.DataSource = this._accountBL.GetListForDDL();
            this.AccountDropDownList.DataBind();
            this.AccountDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

            this.AccBankChargeDropDownList.DataTextField = "AccountName";
            this.AccBankChargeDropDownList.DataValueField = "Account";
            this.AccBankChargeDropDownList.DataSource = this._accountBL.GetListForDDL();
            this.AccBankChargeDropDownList.DataBind();
            this.AccBankChargeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

            this.AccCustChargeDropDownList.DataTextField = "AccountName";
            this.AccCustChargeDropDownList.DataValueField = "Account";
            this.AccCustChargeDropDownList.DataSource = this._accountBL.GetListForDDL();
            this.AccCustChargeDropDownList.DataBind();
            this.AccCustChargeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowBank()
        {
            this.BankDropDownList.DataTextField = "BankName";
            this.BankDropDownList.DataValueField = "BankCode";
            this.BankDropDownList.DataSource = this._bankBL.GetList();
            this.BankDropDownList.DataBind();
            this.BankDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsPayType _msPayType = this._paymentBL.GetSinglePaymentType(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msPayType.PayName = this.PaymentNameTextBox.Text;
            _msPayType.Account = this.AccountTextBox.Text;
            _msPayType.FgMode = Convert.ToChar(this.ModeDropDownList.SelectedValue);
            _msPayType.FgType = Convert.ToChar(this.TypeDropDownList.SelectedValue);
            if (this.BankDropDownList.SelectedValue != "null")
            {
                _msPayType.Bank = this.BankDropDownList.SelectedValue;
            }
            else
            {
                _msPayType.Bank = "";
            }
            _msPayType.NoRekening = this.NoRekeningTextBox.Text;
            _msPayType.NoRekeningOwner = this.NoRekeningOwnerTextBox.Text;
            _msPayType.AccBankCharge = this.AccBankChargeTextBox.Text;
            _msPayType.ExpenseGiro = (this.ExpenseGiroTextBox.Text == "") ? 0 : Convert.ToDecimal(this.ExpenseGiroTextBox.Text);
            _msPayType.AccCustCharge = this.AccCustChargeTextBox.Text;
            _msPayType.CustRevenue = (this.CustChargeRevenueTextBox.Text == "") ? 0 : Convert.ToDecimal(this.CustChargeRevenueTextBox.Text);
            _msPayType.FgBankCharge = PaymentDataMapper.IsCheckedBool(this.FgBankChargeRadioButtonList.SelectedValue);
            _msPayType.FgCustCharge = PaymentDataMapper.IsCheckedBool(this.CustChargeRevenueRadioButtonList.SelectedValue);

            _msPayType.UserID = HttpContext.Current.User.Identity.Name;
            _msPayType.UserDate = DateTime.Now;

            bool _result = this._paymentBL.EditPaymentType(_msPayType);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }

        protected void AccBankChargeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.AccBankChargeDropDownList.SelectedValue != "null")
            {
                byte _decimalPlace = _currencyBL.GetDecimalPlace(_accountBL.GetCurrByAccCode(this.AccBankChargeDropDownList.SelectedValue));
                this.BankChargeDecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                this.ExpenseGiroTextBox.Attributes.Add("OnBlur", "return ChangeFormat2(" + this.ExpenseGiroTextBox.ClientID + "," + this.BankChargeDecimalPlaceHiddenField.ClientID + ");");
            }
            else
            {
                this.ExpenseGiroTextBox.Text = "0";
            }
        }

        protected void AccCustChargeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.AccCustChargeDropDownList.SelectedValue != "null")
            {
                byte _decimalPlace = _currencyBL.GetDecimalPlace(_accountBL.GetCurrByAccCode(this.AccCustChargeDropDownList.SelectedValue));
                this.CustChargeDecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                this.CustChargeRevenueTextBox.Attributes.Add("OnBlur", "return ChangeFormat2(" + this.CustChargeRevenueTextBox.ClientID + "," + this.CustChargeDecimalPlaceHiddenField.ClientID + ");");
            }
            else
            {
                this.CustChargeRevenueTextBox.Text = "0";
            }
        }
    }
}