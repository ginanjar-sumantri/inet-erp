using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.CustomControl;
using System.Collections.Generic;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.Role
{
    public partial class RoleView : RoleBase
    {
        private RoleBL _roleBL = new RoleBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowMenu();

                this.ShowData();
            }
        }

        protected void ShowMenu()
        {
            Guid _companyId = new UserBL().GetCompanyId(HttpContext.Current.User.Identity.Name);
            this.MenuCheckBoxList.ClearSelection();
            this.MenuCheckBoxList.Items.Clear();
            this.MenuCheckBoxList.DataValueField = "MenuID";
            this.MenuCheckBoxList.DataTextField = "Text";
            this.MenuCheckBoxList.DataSource = new TopNavigationMenu().GetListTopMenu(_companyId);
            this.MenuCheckBoxList.DataBind();
        }

        public void ShowData()
        {
            aspnet_Role _aspRole = this._roleBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            master_Role _masterRole = this._roleBL.GetSingleMasterRole(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            this.RoleNameTextBox.Text = _aspRole.RoleName;
            this.SystemRoleCheckBox.Checked = _masterRole.SystemRole;

            if (_masterRole.SystemRole == true)
            {
                this.EditButton.Visible = false;
            }

            this.GetRoleMenuData(_aspRole.RoleId);
        }

        public void GetRoleMenuData(Guid _prmRoleID)
        {
            List<master_RoleMenu> _roleMenuList = _roleBL.GetListRoleMenu(_prmRoleID);
            for (int i = 0; i < this.MenuCheckBoxList.Items.Count; i++)
            {
                foreach (var _item in _roleMenuList)
                {
                    if (new TopNavigationMenu().GetMenuIDByMenuName(this.MenuCheckBoxList.Items[i].Text).ToString() == _item.MenuId.ToString())
                    {
                        this.MenuCheckBoxList.Items[i].Selected = true;
                    }
                }
            }
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }
    }
}