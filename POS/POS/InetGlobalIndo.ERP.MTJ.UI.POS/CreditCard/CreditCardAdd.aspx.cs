using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.UI.POS.Member;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using System.Web.UI.WebControls;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.CreditCard
{
    public partial class CreditCardAdd : CreditCardBase
    {
        private CreditCardBL _creditCardBL = new CreditCardBL();
        private CreditCardTypeBL _creditCardTypeBL = new CreditCardTypeBL();
        private PermissionBL _permBL = new PermissionBL();
        private AccountBL _accountBL = new AccountBL();

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

                this.ClearData();
                this.ShowCreditCardTypeCode();
                this.ShowAccount();
                this.SetAttribute();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.AccountDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountDropDownList.ClientID + "," + this.AccountCodeTextBox.ClientID + ");");
            this.AccountCodeTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountDropDownList.ClientID + "," + this.AccountCodeTextBox.ClientID + ");");
            this.AccountBankChargeDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountBankChargeDropDownList.ClientID + "," + this.AccountBankChargeTextBox.ClientID + ");");
            this.AccountBankChargeTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountBankChargeDropDownList.ClientID + "," + this.AccountBankChargeTextBox.ClientID + ");");
            this.BankChargeTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            this.CustomerChargeTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");

        }

        private void ClearData()
        {
            this.ClearLabel();

            this.CreditCardCodeTextBox.Text = "";
            this.CreditCardNameTextBox.Text = "";
            //this.CreditCardTypeCodeTextBox.Text = "";
            //this.AccountTextBox.Text = "";
        }

        protected void ShowCreditCardTypeCode()
        {
            this.CreditCardTypeCodeDropDownList.Items.Clear();
            this.CreditCardTypeCodeDropDownList.DataTextField = "CreditCardTypeName";
            this.CreditCardTypeCodeDropDownList.DataValueField = "CreditCardTypeCode";
            this.CreditCardTypeCodeDropDownList.DataSource = this._creditCardTypeBL.GetCreditCardTypeListDDL();
            this.CreditCardTypeCodeDropDownList.DataBind();
            this.CreditCardTypeCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowAccount()
        {
            this.AccountDropDownList.Items.Clear();
            this.AccountDropDownList.DataTextField = "AccountName";
            this.AccountDropDownList.DataValueField = "Account";
            this.AccountDropDownList.DataSource = this._accountBL.GetListForDDL();
            this.AccountDropDownList.DataBind();
            this.AccountDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

            this.AccountBankChargeDropDownList.Items.Clear();
            this.AccountBankChargeDropDownList.DataTextField = "AccountName";
            this.AccountBankChargeDropDownList.DataValueField = "Account";
            this.AccountBankChargeDropDownList.DataSource = this._accountBL.GetListForDDL();
            this.AccountBankChargeDropDownList.DataBind();
            this.AccountBankChargeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            POSMsCreditCard _posMsCreditCard = new POSMsCreditCard();

            _posMsCreditCard.CreditCardCode = this.CreditCardCodeTextBox.Text;
            _posMsCreditCard.CreditCardName = this.CreditCardNameTextBox.Text;
            _posMsCreditCard.CreditCardTypeCode = this.CreditCardTypeCodeDropDownList.SelectedValue;
            _posMsCreditCard.Account = this.AccountDropDownList.SelectedValue;
            _posMsCreditCard.BankCharge = Convert.ToDecimal(this.BankChargeTextBox.Text);
            _posMsCreditCard.CustomerCharge = Convert.ToDecimal(this.CustomerChargeTextBox.Text);
            _posMsCreditCard.AccountBankCharge = this.AccountBankChargeDropDownList.SelectedValue;

            bool _result = this._creditCardBL.Add(_posMsCreditCard);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}