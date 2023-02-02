using System;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.CustomControl;
using System.Web;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.Role
{
    public partial class RoleAdd : RoleBase
    {
        private RoleBL _roleBL = new RoleBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowMenu();

                this.ClearLabel();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearData()
        {
            this.RoleNameTextBox.Text = "";
        }

        protected void ShowMenu()
        {
            this.MenuCheckBoxList.ClearSelection();
            this.MenuCheckBoxList.Items.Clear();
            this.MenuCheckBoxList.DataSource = new TopNavigationMenu().GetListTopMenu(new UserBL().GetCompanyId(HttpContext.Current.User.Identity.Name));
            this.MenuCheckBoxList.DataValueField = "MenuID";
            this.MenuCheckBoxList.DataTextField = "Text";
            this.MenuCheckBoxList.DataBind();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            String _menuList = "";
            var _hasilMenu = new TopNavigationMenu().GetListTopMenu(new UserBL().GetCompanyId(HttpContext.Current.User.Identity.Name));

            for (var i = 0; i < _hasilMenu.Count; i++)
            {
                if (this.MenuCheckBoxList.Items[i].Selected == true)
                {
                    if (_menuList == "")
                    {
                        _menuList += this.MenuCheckBoxList.Items[i].Value;
                    }
                    else
                    {
                        _menuList += "," + this.MenuCheckBoxList.Items[i].Value;
                    }
                }
            }

            bool _result = _roleBL.Add(false, this.RoleNameTextBox.Text, _menuList);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Add Data";
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