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
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.LinkServer
{
    public partial class LinkServerView : LinkServerBase
    {
        private SynchronizeBL _synchronizeBL = new SynchronizeBL();
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
            this.IPTextBox.Attributes.Add("ReadOnly", "True");
            this.NameTextBox.Attributes.Add("ReadOnly", "True");
            this.InstanceTextBox.Attributes.Add("ReadOnly", "True");
            this.DatabaseTextBox.Attributes.Add("ReadOnly", "True");
            this.RemoteUserTextBox.Attributes.Add("ReadOnly", "True");
            this.RemotePasswordTextBox.Attributes.Add("ReadOnly", "True");
            this.LocationTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("ReadOnly", "True");
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        public void ShowData()
        {
            MsLinkServer _msLinkServer = this._synchronizeBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.IPTextBox.Text = _msLinkServer.Server_IP;
            this.NameTextBox.Text = _msLinkServer.Server_Name;
            this.InstanceTextBox.Text = _msLinkServer.Server_Instance;
            this.DatabaseTextBox.Text = _msLinkServer.Server_Database;
            this.RemoteUserTextBox.Text = _msLinkServer.Server_RemoteUser;
            this.RemotePasswordTextBox.Text = _msLinkServer.Server_RemotePass;
            this.LocationTextBox.Text = _msLinkServer.Server_Location;
            this.ServerHOCheckBox.Checked = _msLinkServer.Server_HO == 'Y' ? true : false;
            this.FgActiveCheckBox.Checked = (_msLinkServer.FgActive == 'Y') ? true : false;
            this.RemarkTextBox.Text = _msLinkServer.Remark;
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