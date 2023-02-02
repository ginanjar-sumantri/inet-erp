using System;
using System.Web;
using System.Web.UI;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.UI.POS.Member;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.LinkServer
{
    public partial class LinkServerAdd : LinkServerBase
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

                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            this.ClearLabel();

            this.IPTextBox.Text = "";
            this.NameTextBox.Text = "";
            this.InstanceTextBox.Text = "";
            this.DatabaseTextBox.Text = "";
            this.RemoteUserTextBox.Text = "";
            this.RemotePasswordTextBox.Text = "";
            this.LocationTextBox.Text = "";
            this.ServerHOCheckBox.Checked = false;
            this.FgActiveCheckBox.Checked = false;
            this.RemarkTextBox.Text = "";
        }

        protected void CheckValidData()
        {
            this.ClearLabel();
            MsLinkServer _msLinkServer = this._synchronizeBL.GetSingle(this.IPTextBox.Text);
            if (_msLinkServer != null)
                this.WarningLabel.Text = "Your input Internet Protocol, already exist. ";
            if (this.ServerHOCheckBox.Checked == true)
            {
                String _IPServer = this._synchronizeBL.GetIPServerHO();
                if (_IPServer != "")
                    this.WarningLabel.Text += "Internet Protocol " + _IPServer + " is setting as Server HO. Please edit it first.";
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                MsLinkServer _msMsLinkServer = new MsLinkServer();

                _msMsLinkServer.Server_IP = this.IPTextBox.Text;
                _msMsLinkServer.Server_Name = this.NameTextBox.Text;
                _msMsLinkServer.Server_Instance = this.InstanceTextBox.Text;
                _msMsLinkServer.Server_Database = this.DatabaseTextBox.Text;
                _msMsLinkServer.Server_RemoteUser = this.RemoteUserTextBox.Text;
                _msMsLinkServer.Server_RemotePass = this.RemotePasswordTextBox.Text;
                _msMsLinkServer.Server_Location = this.LocationTextBox.Text;
                _msMsLinkServer.Server_HO = (this.ServerHOCheckBox.Checked == true) ? 'Y' : 'N';
                _msMsLinkServer.FgActive = (this.FgActiveCheckBox.Checked == true) ? 'Y' : 'N';
                _msMsLinkServer.Remark = this.RemarkTextBox.Text;
                _msMsLinkServer.CreatedBy = HttpContext.Current.User.Identity.Name;
                _msMsLinkServer.CreatedDate = DateTime.Now;

                bool _result = this._synchronizeBL.Add(_msMsLinkServer);

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