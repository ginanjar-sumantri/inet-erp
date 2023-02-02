using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.Radius
{
    public partial class RadiusEdit : RadiusBase
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
                
                this.ClearLabel();
                this.ShowData();

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
                this.CustomerTextBox.Attributes.Add("ReadOnly", "True");
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ShowData()
        {
            BILMsRadius _msRadius = this._radiusBL.GetSingleRadius(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.RadiusNameTextBox.Text = _msRadius.RadiusName;
            this.CustomerTextBox.Text = this._custBL.GetNameByCode(_msRadius.CustCode);
            this.custCode.Value = _msRadius.CustCode;
            this.RadiusIPTextBox.Text = _msRadius.RadiusIP;
            this.RadiusUserNameTextBox.Text = _msRadius.RadiusUserName;
            this.RadiusPwdTextBox.Text = _msRadius.RadiusPwd;
            this.RadiusDbNameTextBox.Text = _msRadius.RadiusDbName;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            BILMsRadius _msRadius = this._radiusBL.GetSingleRadius(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msRadius.RadiusName = this.RadiusNameTextBox.Text;
            _msRadius.CustCode = this.custCode.Value;
            _msRadius.RadiusIP = this.RadiusIPTextBox.Text;
            _msRadius.RadiusUserName = this.RadiusUserNameTextBox.Text;
            _msRadius.RadiusPwd = Rijndael.Encrypt(this.RadiusPwdTextBox.Text, ApplicationConfig.PasswordEncryptionKey);
            _msRadius.RadiusDbName = this.RadiusDbNameTextBox.Text;

            bool _result = this._radiusBL.Edit(_msRadius);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
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
