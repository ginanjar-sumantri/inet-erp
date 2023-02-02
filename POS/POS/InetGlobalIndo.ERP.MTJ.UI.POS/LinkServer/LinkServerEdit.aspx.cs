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
    public partial class LinkServerEdit : LinkServerBase
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

                this.IPTextBox.Attributes.Add("ReadOnly", "True");

                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
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

        protected void CheckValidData()
        {
            this.ClearLabel();
            if (this.ServerHOCheckBox.Checked == true)
            {
                String _IPServer = this._synchronizeBL.GetIPServerHO();
                if (_IPServer != this.IPTextBox.Text & _IPServer != "")
                    this.WarningLabel.Text = "Internet Protocol " + _IPServer + " is setting as Server HO. Please edit it first.";
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                MsLinkServer _msLinkServer = this._synchronizeBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
                _msLinkServer.Server_Name = this.NameTextBox.Text;
                _msLinkServer.Server_Instance = this.InstanceTextBox.Text;
                _msLinkServer.Server_Database = this.DatabaseTextBox.Text;
                _msLinkServer.Server_RemoteUser = this.RemoteUserTextBox.Text;
                _msLinkServer.Server_RemotePass = this.RemotePasswordTextBox.Text;
                _msLinkServer.Server_Location = this.LocationTextBox.Text;
                _msLinkServer.Server_HO = (this.ServerHOCheckBox.Checked == true) ? 'Y' : 'N';
                _msLinkServer.FgActive = (this.FgActiveCheckBox.Checked == true) ? 'Y' : 'N';
                _msLinkServer.Remark = this.RemarkTextBox.Text;
                _msLinkServer.ModifiedBy = HttpContext.Current.User.Identity.Name;
                _msLinkServer.ModifiedDate = DateTime.Now;

                bool _result = this._synchronizeBL.EditSubmit();

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