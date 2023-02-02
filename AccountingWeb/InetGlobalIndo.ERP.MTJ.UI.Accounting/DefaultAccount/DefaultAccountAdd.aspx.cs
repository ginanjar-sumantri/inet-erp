using System;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.DefaultAccount
{
    public partial class DefaultAccountAdd : DefaultAccountBase
    {
        private SetupBL _setupBL = new SetupBL();
        private AccountBL _accountBL = new AccountBL();
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
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                //this.ShowAccount();

                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        //public void ShowAccount()
        //{
        //    this.AccountRBL.Items.Clear();
        //    this.AccountRBL.DataTextField = "AccountName";
        //    this.AccountRBL.DataValueField = "Account";
        //    this.AccountRBL.DataSource = this._accountBL.GetListForDDL();
        //    this.AccountRBL.DataBind();
        //}

        public void ClearData()
        {
            this.ClearLabel();

            this.SetCodeTextBox.Text = "";
            this.SetDescriptionTextBox.Text = "";
            //this.AccountRBL.SelectedValue = "";
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            this.ClearData();
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            Master_Default _msDefault = new Master_Default();
            MsSetup _msSetup = new MsSetup();

            _msDefault.SetCode = this.SetCodeTextBox.Text;
            _msDefault.SetDescription = this.SetDescriptionTextBox.Text;

            _msSetup.SetGroup = SetupDataMapper.GetSetupGroup(SetupStatus.Account);
            _msSetup.SetCode = this.SetCodeTextBox.Text;
            _msSetup.SetDescription = this.SetDescriptionTextBox.Text;
            //_msSetup.SetValue = this.AccountRBL.SelectedValue;

            bool _result = false;

            if (this._setupBL.AddMasterDefault(_msDefault) == true && this._setupBL.Add(_msSetup) == true)
            {
                _result = true;
            }

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Default Account";
            }
        }
    }
}