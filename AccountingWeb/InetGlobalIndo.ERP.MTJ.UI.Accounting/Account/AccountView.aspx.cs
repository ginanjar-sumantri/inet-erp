using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;


namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.Account
{
    public partial class AccountView : AccountBase
    {
        private AccountBL _accountBL = new AccountBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private AccClassBL _accClassBL = new AccClassBL();
        private SubledBL _subledBL = new SubledBL();
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = _pageTitleLiteral;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.SetButtonPermission();

                this.ShowSubledRadioButtonList();

                this.ShowData();
            }
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }


        protected void ShowSubledRadioButtonList()
        {
            this.SubledRBL.Items.Clear();
            this.SubledRBL.DataSource = _subledBL.GetList();
            this.SubledRBL.DataTextField = "SubledName";
            this.SubledRBL.DataValueField = "SubledCode";
            this.SubledRBL.DataBind();
        }

        private void ShowData()
        {
            var _account = _accountBL.GetViewMsAccount(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.BranchAccountTextBox.Text = _accountBL.GetBranchAccountIDByCode(_account.BranchAccCode) + " - " + _accountBL.GetBranchAccountNameByCode(_account.BranchAccCode);
            this.CodeTextBox.Text = _account.AccClass;
            this.CodeNameTextBox.Text = _account.AccClassName;
            this.DetailTextBox.Text = _account.Detail;
            this.CurrCodeTextBox.Text = _account.CurrCode;
            this.DescTextBox.Text = _account.AccountName;

            if (_account.FgNormal == 'D')
            {
                this.RadioButtonList1.Items.FindByValue("D").Selected = true;
            }
            else
            {
                this.RadioButtonList1.Items.FindByValue("C").Selected = true;
            }

            this.SubledRBL.SelectedValue = _account.FgSubLed.ToString();

            if (_account.FgActive == 'Y')
            {
                this.checkbox1.Checked = true;
            }
            else
            {
                this.checkbox1.Checked = false;
            }

        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }
    }
}