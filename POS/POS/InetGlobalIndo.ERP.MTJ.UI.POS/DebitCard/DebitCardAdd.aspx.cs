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

namespace InetGlobalIndo.ERP.MTJ.UI.POS.DebitCard
{
    public partial class DebitCardAdd : DebitCardBase
    {
        private DebitCardBL _debitCardBL = new DebitCardBL();
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
        }

        private void ClearData()
        {
            this.ClearLabel();

            this.DebitCardCodeTextBox.Text = "";
            this.DebitCardNameTextBox.Text = "";
            //this.AccountTextBox.Text = "";
        }

        protected void ShowAccount()
        {
            this.AccountDropDownList.Items.Clear();
            this.AccountDropDownList.DataTextField = "AccountName";
            this.AccountDropDownList.DataValueField = "Account";
            this.AccountDropDownList.DataSource = this._accountBL.GetListForDDL();
            this.AccountDropDownList.DataBind();
            this.AccountDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            POSMsDebitCard _posMsDebitCard = new POSMsDebitCard();

            _posMsDebitCard.DebitCardCode = this.DebitCardCodeTextBox.Text;
            _posMsDebitCard.DebitCardName = this.DebitCardNameTextBox.Text;
            _posMsDebitCard.Account = this.AccountDropDownList.SelectedValue;

            bool _result = this._debitCardBL.Add(_posMsDebitCard);

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