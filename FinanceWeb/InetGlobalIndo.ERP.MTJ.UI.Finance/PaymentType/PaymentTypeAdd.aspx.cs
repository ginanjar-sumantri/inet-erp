using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PaymentType
{
    public partial class PaymentTypeAdd : PaymentTypeBase
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

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowAccount();
                this.ShowBank();

                this.ClearData();
                this.SetAttribute();
            }
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribute()
        {
            this.ModeDropDownList.Attributes.Add("OnChange", "EnableOrDisable(" + this.ModeDropDownList.ClientID + "," + this.BankDropDownList.ClientID + "," + this.NoRekeningTextBox.ClientID + "," + this.NoRekeningOwnerTextBox.ClientID + "," + this.AccBankChargeTextBox.ClientID + "," + this.AccBankChargeDropDownList.ClientID + "," + this.ExpenseGiroTextBox.ClientID + "," + this.AccCustChargeTextBox.ClientID + "," + this.AccCustChargeDropDownList.ClientID + "," + this.CustChargeRevenueTextBox.ClientID + "," + this.FgBankChargeRadioButtonList.ClientID + "," + this.CustChargeRevenueRadioButtonList.ClientID + ");");

            this.ExpenseGiroTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.ExpenseGiroTextBox.Attributes.Add("OnBlur", "return ChangeFormat2(" + this.ExpenseGiroTextBox.ClientID + "," + this.BankChargeDecimalPlaceHiddenField.ClientID + ");");
            this.CustChargeRevenueTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.CustChargeRevenueTextBox.Attributes.Add("OnBlur", "return ChangeFormat2(" + this.CustChargeRevenueTextBox.ClientID + "," + this.CustChargeDecimalPlaceHiddenField.ClientID + ");");
            this.NoRekeningTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");

            this.AccountDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountDropDownList.ClientID + "," + this.AccountTextBox.ClientID + ");");
            this.AccountTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountDropDownList.ClientID + "," + this.AccountTextBox.ClientID + ");");

            this.AccBankChargeDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccBankChargeDropDownList.ClientID + "," + this.AccBankChargeTextBox.ClientID + ");");
            this.AccBankChargeTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccBankChargeDropDownList.ClientID + "," + this.AccBankChargeTextBox.ClientID + ");");

            this.AccCustChargeDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccCustChargeDropDownList.ClientID + "," + this.AccCustChargeTextBox.ClientID + ");");
            this.AccCustChargeTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccCustChargeDropDownList.ClientID + "," + this.AccCustChargeTextBox.ClientID + ");");
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

        public void ClearData()
        {
            this.ClearLabel();
            this.PaymentCodeTextBox.Text = "";
            this.PaymentNameTextBox.Text = "";
            this.AccountTextBox.Text = "";
            this.AccountDropDownList.SelectedValue = "null";
            this.BankDropDownList.SelectedValue = "null";
            this.ModeDropDownList.SelectedValue = "B";
            this.TypeDropDownList.SelectedValue = "P";
            this.NoRekeningTextBox.Text = "";
            this.AccBankChargeTextBox.Text = "";
            this.AccCustChargeTextBox.Text = "";
            this.AccBankChargeDropDownList.SelectedValue = "null";
            this.AccCustChargeDropDownList.SelectedValue = "null";
            this.ExpenseGiroTextBox.Text = "0";
            this.CustChargeRevenueTextBox.Text = "0";
            this.CustChargeRevenueRadioButtonList.SelectedValue = "0";
            this.FgBankChargeRadioButtonList.SelectedValue = "0";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsPayType _msPayType = new MsPayType();

            _msPayType.PayCode = this.PaymentCodeTextBox.Text;
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
            _msPayType.ExpenseGiro = Convert.ToDecimal(this.ExpenseGiroTextBox.Text);
            _msPayType.AccCustCharge = this.AccCustChargeTextBox.Text;
            _msPayType.CustRevenue = Convert.ToDecimal(this.CustChargeRevenueTextBox.Text);
            _msPayType.FgBankCharge = PaymentDataMapper.IsCheckedBool(this.FgBankChargeRadioButtonList.SelectedValue);
            _msPayType.FgCustCharge = PaymentDataMapper.IsCheckedBool(this.CustChargeRevenueRadioButtonList.SelectedValue);

            _msPayType.UserID = HttpContext.Current.User.Identity.Name;
            _msPayType.UserDate = DateTime.Now;

            bool _result = this._paymentBL.AddPaymentType(_msPayType);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        protected void AccBankChargeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.AccBankChargeDropDownList.SelectedValue != "null")
            {
                byte _decimalPlace = _currencyBL.GetDecimalPlace(_accountBL.GetCurrByAccCode(this.AccBankChargeDropDownList.SelectedValue));
                this.BankChargeDecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                this.ExpenseGiroTextBox.Attributes.Add("OnBlur", "return ChangeFormat2(" + this.ExpenseGiroTextBox.ClientID + "," + this.BankChargeDecimalPlaceHiddenField.ClientID + ");");
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
        }
    }
}