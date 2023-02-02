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
    public partial class RadiusView : RadiusBase
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

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.SetAttribute();

                this.SetButtonPermission();
                this.ShowData();
            }
        }

        private void SetAttribute()
        {
            this.RadiusNameTextBox.Attributes.Add("ReadOnly", "True");
            this.CustomerTextBox.Attributes.Add("ReadOnly", "True");
            this.RadiusIPTextBox.Attributes.Add("ReadOnly", "True");
            this.RadiusUserNameTextBox.Attributes.Add("ReadOnly", "True");
            this.RadiusPwdTextBox.Attributes.Add("ReadOnly", "True");
            this.RadiusDbNameTextBox.Attributes.Add("ReadOnly", "True");
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        protected void ShowData()
        {
            BILMsRadius _msRadius = this._radiusBL.GetSingleRadius(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.RadiusNameTextBox.Text = _msRadius.RadiusName;
            this.CustomerTextBox.Text = this._custBL.GetNameByCode(_msRadius.CustCode);
            this.RadiusIPTextBox.Text = _msRadius.RadiusIP;
            this.RadiusUserNameTextBox.Text = _msRadius.RadiusUserName;
            this.RadiusPwdTextBox.Text = _msRadius.RadiusPwd;
            this.RadiusDbNameTextBox.Text = _msRadius.RadiusDbName;
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }
    }
}
