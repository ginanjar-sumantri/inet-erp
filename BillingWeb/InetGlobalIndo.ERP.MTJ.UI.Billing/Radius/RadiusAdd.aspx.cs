using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.Radius
{
    public partial class RadiusAdd : RadiusBase
    {
        private RadiusBL _radiusBL = new RadiusBL();
        private PermissionBL _permBL = new PermissionBL();
        private CustomerBL _custBL = new CustomerBL();

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

                this.ClearLabel();
                this.ClearData();

                this.btnSearchCustomer.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findCustomer&configCode=customer','_popSearch','width=800,height=300,toolbar=0,location=0,status=0,scrollbars=1')";
                String spawnJS = "<script language='JavaScript'>\n";
                ////////////////////DECLARE FUNCTION FOR CATCHING CUSTOMER SEARCH
                spawnJS += "function findCustomer(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.custCode.ClientID + "').value = dataArray[0];\n";
                spawnJS += "document.getElementById('" + this.CustomerTextBox.ClientID + "').value = dataArray[1];\n";
                spawnJS += "}\n";
                spawnJS += "</script>\n";
                this.javascriptReceiver.Text = spawnJS;
                this.CustomerTextBox.Attributes.Add("ReadOnly","True");
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ClearData()
        {
            this.RadiusNameTextBox.Text = "";
            this.CustomerTextBox.Text = "";
            this.RadiusIPTextBox.Text = "";
            this.RadiusUserNameTextBox.Text = "";
            this.RadiusPwdTextBox.Text = "";
            this.RadiusDbNameTextBox.Text = "";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            BILMsRadius _msRadius = new BILMsRadius();

            _msRadius.RadiusCode = Guid.NewGuid().ToString();
            _msRadius.RadiusName = this.RadiusNameTextBox.Text;
            _msRadius.CustCode = this.custCode.Value;
            _msRadius.RadiusIP = this.RadiusIPTextBox.Text;
            _msRadius.RadiusUserName = this.RadiusUserNameTextBox.Text;
            _msRadius.RadiusPwd = Rijndael.Encrypt(this.RadiusPwdTextBox.Text, ApplicationConfig.PasswordEncryptionKey);
            _msRadius.RadiusDbName = this.RadiusDbNameTextBox.Text;
            
            String _result = this._radiusBL.Add(_msRadius);

            if (_result == "")
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }
    }
}
