using System;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.Account
{
    public partial class AccountEdit : AccountBase
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

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = _pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowCurrCodeDropDownList();
                this.ShowSubledRadioButtonList();

                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowCurrCodeDropDownList()
        {
            this.CurrCodeDropDownList.Items.Clear();
            this.CurrCodeDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrCodeDropDownList.DataValueField = "CurrCode";
            this.CurrCodeDropDownList.DataTextField = "CurrCode";
            this.CurrCodeDropDownList.DataBind();
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
            MsAccount _msAccount = _accountBL.GetSingleAccount(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.AccClassNameTextBox.Text = _msAccount.AccClass;
            this.CurrCodeDropDownList.SelectedValue = _msAccount.CurrCode;
            this.BranchAccountTextBox.Text = _accountBL.GetBranchAccountIDByCode(_msAccount.BranchAccCode) + " - " + _accountBL.GetBranchAccountNameByCode(_msAccount.BranchAccCode);
            this.CodeTextBox.Text = _msAccount.AccClass;
            this.AccClassNameTextBox.Text = _accClassBL.GetAccClassNameByCode(_msAccount.AccClass);
            this.DetailTextBox.Text = _msAccount.Detail;
            this.DescTextBox.Text = _msAccount.AccountName;

            if (_msAccount.FgNormal == 'D')
            {
                this.SaldoNormalRBL.Items.FindByValue("D").Selected = true;
            }
            else
            {
                this.SaldoNormalRBL.Items.FindByValue("C").Selected = true;
            }

            this.SubledRBL.SelectedValue = _msAccount.FgSubLed.ToString();

            if (_msAccount.FgActive == 'Y')
            {
                this.FgActiveCheckBox.Checked = true;
            }
            else
            {
                this.FgActiveCheckBox.Checked = false;
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            MsAccount _msAccount = this._accountBL.GetSingleAccount(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msAccount.AccountName = this.DescTextBox.Text;
            _msAccount.FgSubLed = Convert.ToChar(this.SubledRBL.SelectedValue);
            _msAccount.FgNormal = Convert.ToChar(this.SaldoNormalRBL.SelectedValue);
            _msAccount.CurrCode = this.CurrCodeDropDownList.SelectedValue;

            if (this.FgActiveCheckBox.Checked == true)
            {
                _msAccount.FgActive = 'Y';
            }
            else
            {
                _msAccount.FgActive = 'N';
            }

            if (this._accountBL.EditAccount(_msAccount) == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}
