using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITV.BusinessRule;
using ITV.DataAccess.ITVDatabase;
using ITV.UI.ApplicationClass;

namespace ITV.UI.Role
{
    public partial class Role : RoleBase
    {
        private RoleBL _roleBL = new RoleBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.ShowRoleDDL();
            }
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            this.RoleNameTextBox.Text = "";
            this.SystemCheckBox.Checked = false;
        }

        private void ShowRoleDDL()
        {
            this.RoleDropDownList.Items.Clear();
            this.RoleDropDownList.DataSource = this._roleBL.GetListRoleForDDL(ref _errMessage);
            this.RoleDropDownList.DataValueField = "UsrmSRolesID";
            this.RoleDropDownList.DataTextField = "RoleName";
            this.RoleDropDownList.DataBind();
        }

        private void ShowData()
        {
            UsRMSRoles _msRole = this._roleBL.GetSingleRole(this.RoleIDHiddenField.Value, ref _errMessage);
            this.RoleNameTextBox.Text = _msRole.RoleName;
            this.SystemCheckBox.Checked = Convert.ToBoolean(_msRole.SystemRole);
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            Boolean _result = false;

            if (this.RoleIDHiddenField.Value == "")
            {
                UsRMSRoles _msRoles = new UsRMSRoles();
                _msRoles.RoleName = this.RoleNameTextBox.Text;
                _msRoles.LoweredRoleName = this.RoleNameTextBox.Text.ToLower();
                _msRoles.SystemRole = this.SystemCheckBox.Checked;

                _result = this._roleBL.AddRole(_msRoles, ref _errMessage);
            }
            else
            {
                UsRMSRoles _msRoles = this._roleBL.GetSingleRole(this.RoleIDHiddenField.Value, ref _errMessage);
                _msRoles.RoleName = this.RoleNameTextBox.Text;
                _msRoles.LoweredRoleName = this.RoleNameTextBox.Text.ToLower();
                _msRoles.SystemRole = this.SystemCheckBox.Checked;

                _result = this._roleBL.EditRole(_msRoles, ref _errMessage);
            }

            if (_errMessage != "")
            {
                this.WarningLabel.Text = _errMessage;
            }
            else
            {
                this.ClearLabel();
                this.ClearData();
                this.ShowRoleDDL();
            }
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            this.RoleIDHiddenField.Value = this.RoleDropDownList.SelectedValue;
            this.ShowData();
        }
    }
}