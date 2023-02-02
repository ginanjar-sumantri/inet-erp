using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.LimitAuthorization
{
    public partial class LimitAuthorizationAdd : LimitAuthorizationBase
    {
        private LimitAuthorizationBL _limitAuthorBL = new LimitAuthorizationBL();
        private TransTypeBL _transTypeBL = new TransTypeBL();
        private RoleBL _roleBL = new RoleBL();
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

                this.ShowRoleDropDownList();
                this.ShowTransTypeDropDownList();

                this.ClearLabel();
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
            this.LimitTextBox.Attributes.Add("OnBlur", "ChangeFormat(" + this.LimitTextBox.ClientID + ");");
        }

        private void ShowRoleDropDownList()
        {
            this.RoleDropDownList.Items.Clear();
            this.RoleDropDownList.DataSource = this._roleBL.GetListRole();
            this.RoleDropDownList.DataValueField = "RoleId";
            this.RoleDropDownList.DataTextField = "RoleName";
            this.RoleDropDownList.DataBind();
            this.RoleDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowTransTypeDropDownList()
        {
            this.TransTypeDropDownList.Items.Clear();
            this.TransTypeDropDownList.DataSource = this._transTypeBL.GetListTransTypeForDDL();
            this.TransTypeDropDownList.DataValueField = "TransTypeCode";
            this.TransTypeDropDownList.DataTextField = "TransTypeName";
            this.TransTypeDropDownList.DataBind();
            this.TransTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearData()
        {
            this.RoleDropDownList.SelectedValue = "null";
            this.TransTypeDropDownList.SelectedValue = "null";
            this.LimitTextBox.Text = "0";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            Master_LimitAuthorization _msLimitAuthor = new Master_LimitAuthorization();

            _msLimitAuthor.RoleID = new Guid(this.RoleDropDownList.SelectedValue);
            _msLimitAuthor.TransTypeCode = this.TransTypeDropDownList.SelectedValue;
            _msLimitAuthor.Limit = (Convert.ToDecimal(this.LimitTextBox.Text));

            _msLimitAuthor.InsertBy = HttpContext.Current.User.Identity.Name;
            _msLimitAuthor.InsertDate = DateTime.Now;
            _msLimitAuthor.EditBy = HttpContext.Current.User.Identity.Name;
            _msLimitAuthor.EditDate = DateTime.Now;

            bool _result = this._limitAuthorBL.AddLimitAuthor(_msLimitAuthor);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }
    }
}