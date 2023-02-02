using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.UI.POS.Member;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using System.Web.UI.WebControls;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.CashierAccount
{
    public partial class CashierAccountEdit : CashierAccountBase
    {
        private CashierAccountBL _cashierAcountBL = new CashierAccountBL();
        private AccountBL _accountBL = new AccountBL();
        private EmployeeBL _employeeBL = new EmployeeBL();
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

                this.CashierCodeTextBox.Attributes.Add("ReadOnly", "True");

                this.ShowAccount();
                this.ClearLabel();
                this.ShowData();
                this.SettAttribute();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SettAttribute()
        {
            this.AccountDDL.Attributes.Add("OnChange", "Selected(" + this.AccountDDL.ClientID + "," + this.AccountCodeTextBox.ClientID + ");"); 
            this.AccountCodeTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountDDL.ClientID + "," + this.AccountCodeTextBox.ClientID + ");");
        }

        public void ShowAccount()
        {
            this.AccountDDL.Items.Clear();
            this.AccountDDL.DataTextField = "AccountName";
            this.AccountDDL.DataValueField = "Account";
            this.AccountDDL.DataSource = this._accountBL.GetListForDDL();
            this.AccountDDL.DataBind();
            this.AccountDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowData()
        {
            POSMsCashierAccount _msCashierAccount = this._cashierAcountBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.CashierCodeTextBox.Text  = (this._employeeBL.GetSingleEmp(_msCashierAccount.CashierEmpNmbr)).EmpName;
            this.AccountDDL.SelectedValue = _msCashierAccount.Account;
            this.AccountCodeTextBox.Text = _msCashierAccount.Account;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            POSMsCashierAccount _msCashierAccount = this._cashierAcountBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msCashierAccount.Account = this.AccountDDL.SelectedValue;

            bool _result = this._cashierAcountBL.Edit(_msCashierAccount);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}