using System;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.DefaultAccount
{
    public partial class DefaultAccountDtEdit : DefaultAccountBase
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

                this.ClearData();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowData()
        {
            string _setCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _setVal = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeValue), ApplicationConfig.EncryptionKey);

            Master_Default _msDefault = this._setupBL.GetSingleMasterDefault(_setCode);

            this.SetCodeTextBox.Text = _msDefault.SetCode;
            this.SetDescriptionTextBox.Text = _msDefault.SetDescription;
            this.AccountRBL.SelectedValue = _setVal;
        }

        public void ShowAccount()
        {
            this.AccountRBL.Items.Clear();
            this.AccountRBL.DataTextField = "AccountName";
            this.AccountRBL.DataValueField = "Account";
            this.AccountRBL.DataSource = this._accountBL.GetListForDDL();
            this.AccountRBL.DataBind();
        }

        public void ClearData()
        {
            this.ClearLabel();
            this.ShowData();
            this.AccountRBL.SelectedValue = "";
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
            string _setCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _setVal = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeValue), ApplicationConfig.EncryptionKey);
            bool _result = false;
            bool _result2 = false;

            Master_DefaultAcc _msDefaultAcc = this._setupBL.GetSingleMasterDefaultAcc(_setCode, _setVal);
            MsSetup _msSetup = this._setupBL.GetSingle(_setCode);

            _msDefaultAcc.Account = this.AccountRBL.SelectedValue;
            _msSetup.SetValue = this.AccountRBL.SelectedValue;

            _result = this._setupBL.Edit(_msSetup);
            _result2 = this._setupBL.EditMasterDefaultAcc(_msDefaultAcc);

            if (_result == true && _result2 == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_setCode, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Setup";
            }
        }
    }
}