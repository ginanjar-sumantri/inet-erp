using System;
using System.Web;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.TransType
{
    public partial class TransTypeAdd : TransTypeBase
    {
        private TransTypeBL _transTypeBL = new TransTypeBL();
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
                this.PageTitleLiteral.Text = this._transPageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.SetUpButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/set_detail.jpg";

                this.SetButtonPermission();

                this.SetAttribute();
                this.ClearLabel();
                this.ClearData();
            }
        }

        private void SetButtonPermission()
        {
            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.SetUpButton.Visible = false;
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.ActiveCheckBox.Attributes.Add("OnClick", " EnableDisableButtonAuthor('" + this.SetUpButton.ClientID + "');");
        }

        protected void ClearData()
        {
            this.CodeTextBox.Text = "";
            this.NameTextBox.Text = "";
            this.ActiveCheckBox.Checked = true;
        }

        protected bool simpen()
        {
            MsTransType _msTransType = new MsTransType();

            _msTransType.TransTypeCode = this.CodeTextBox.Text;
            _msTransType.TransTypeName = this.NameTextBox.Text;
            _msTransType.FgAccount = TransTypeDataMapper.IsActive(this.ActiveCheckBox.Checked);

            _msTransType.UserID = HttpContext.Current.User.Identity.Name;
            _msTransType.UserDate = DateTime.Now;

            bool _result = this._transTypeBL.Add(_msTransType);

            return _result;
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            bool _result = simpen();

            if (_result == true)
            {
                Response.Redirect(this._transHomePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void SetUpButton_Click(object sender, EventArgs e)
        {
            bool _result = simpen();

            if (_result == true)
            {
                Response.Redirect(this._homePage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.CodeTextBox.Text, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(_transHomePage);
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }
    }
}