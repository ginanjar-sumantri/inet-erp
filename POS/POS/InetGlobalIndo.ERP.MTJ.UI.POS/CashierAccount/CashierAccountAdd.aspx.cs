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
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.CashierAccount
{
    public partial class CashierAccountAdd : CashierAccountBase
    {
        private CashierAccountBL _cashierAccountBL = new CashierAccountBL();
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

                this.btnSearchEmployee.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findEmployee&configCode=employee','_popSearch','maximize=1,toolbar=0,location=0,status=0,scrollbars=1')"; 
                
                String spawnJS = "<script language='JavaScript'>\n";

                ////////////////////DECLARE FUNCTION FOR CATCHING SUPPLIER SEARCH
                spawnJS += "function findEmployee(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.CashierCodeTextBox.ClientID + "').value = dataArray[0];\n";
                spawnJS += "document.getElementById('" + this.EmployeeNmbrHiddenField.ClientID + "').value = dataArray[1];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                spawnJS += "</script>\n";
                this.javascriptReceiver.Text = spawnJS;

                this.ShowAccount();
                this.SetAttribute();
                this.ClearData();
                
                
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.AccountDDL.Attributes.Add("OnChange", "Selected(" + this.AccountDDL.ClientID + "," + this.AccountCodeTextBox.ClientID + ");");
            this.AccountCodeTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountDDL.ClientID + "," + this.AccountCodeTextBox.ClientID + ");");
        }

        private void ClearData()
        {
            this.ClearLabel();

            this.CashierCodeTextBox.Text = "";
            //this.AccountDDL.Text = "";
        }

        private void ShowAccount()
        {
            //this.AccountDDL.Items.Clear();
            this.AccountDDL.DataTextField = "AccountName";
            this.AccountDDL.DataValueField = "Account";
            this.AccountDDL.DataSource = this._accountBL.GetListForDDL();
            this.AccountDDL.DataBind();
            this.AccountDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            POSMsCashierAccount _msCashierAcc = new POSMsCashierAccount();

            _msCashierAcc.CashierEmpNmbr = this.EmployeeNmbrHiddenField.Value;
            _msCashierAcc.Account = this.AccountDDL.SelectedValue;

            bool _result = this._cashierAccountBL.Add(_msCashierAcc);

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