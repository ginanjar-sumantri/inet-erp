using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.TransType
{
    public partial class TransTypeEdit : TransTypeBase
    {
        private TransTypeBL _TransTypeBL = new TransTypeBL();
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
                this.PageTitleLiteral.Text = this._transPageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.SetButtonPermission();

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
            }
        }

        private void SetButtonPermission()
        {
            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.SaveButton.Visible = false;
                this.SaveAndViewDetailButton.Visible = false;
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.ActiveCheckBox.Attributes.Add("OnClick", " EnableDisableButtonAuthor('" + this.SaveAndViewDetailButton.ClientID + "');");
        }

        private void ShowData()
        {
            MsTransType _msTranType = this._TransTypeBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.CodeTextBox.Text = _msTranType.TransTypeCode;
            this.NameTextBox.Text = _msTranType.TransTypeName;
            this.ActiveCheckBox.Checked = TransTypeDataMapper.IsActive(_msTranType.FgAccount);
        }

        protected bool simpen()
        {
            MsTransType _msTransType = this._TransTypeBL.GetSingle(this.CodeTextBox.Text);
            _msTransType.TransTypeName = this.NameTextBox.Text;
            _msTransType.FgAccount = TransTypeDataMapper.IsActive(this.ActiveCheckBox.Checked);

            _msTransType.UserID = HttpContext.Current.User.Identity.Name;
            _msTransType.UserDate = DateTime.Now;

            bool _result = this._TransTypeBL.Edit(_msTransType);
            return _result;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            bool _result = simpen();
            if (_result == true)
            {
                Response.Redirect(this._transHomePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(_transHomePage);
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(this._homePage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.CodeTextBox.Text, ApplicationConfig.EncryptionKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, EventArgs e)
        {
            bool _result = simpen();
            if (_result == true)
            {
                Response.Redirect(this._homePage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.CodeTextBox.Text, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }
    }
}