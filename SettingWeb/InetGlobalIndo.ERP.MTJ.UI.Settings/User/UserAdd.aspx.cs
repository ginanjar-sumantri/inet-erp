using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.Authentication;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.SystemConfig;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.User
{
    public partial class UserAdd : UserBase
    {
        private UserBL _userBL = new UserBL();
        private EmployeeBL _empBL = new EmployeeBL();
        private User_EmployeeBL _userEmpBL = new User_EmployeeBL();
        private RoleBL _roleBL = new RoleBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowEmployee();
                this.ShowRoleCheckBoxList();

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
            this.UserNameTextBox.Text = "";
            this.PasswordTextBox.Text = "";
            this.PasswordCfmTextBox.Text = "";
            this.EmailTextBox.Text = "";
            this.QuestionTextBox.Text = "";
            this.AnswerTextBox.Text = "";
            this.EmployeeDropDownList.SelectedValue = "null";
            this.RoleCheckBoxList.ClearSelection();
        }

        public void ShowEmployee()
        {
            this.EmployeeDropDownList.DataTextField = "EmpName";
            this.EmployeeDropDownList.DataValueField = "EmpNumb";
            this.EmployeeDropDownList.DataSource = this._empBL.GetListEmpForDDLUserNameAdd();
            this.EmployeeDropDownList.DataBind();
            this.EmployeeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowRoleCheckBoxList()
        {
            this.RoleCheckBoxList.DataTextField = "RoleName";
            this.RoleCheckBoxList.DataValueField = "RoleName";
            this.RoleCheckBoxList.DataSource = this._roleBL.GetListRoleName();
            this.RoleCheckBoxList.DataBind();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MembershipService _service = new MembershipService();
            MembershipCreateStatus _outStatus;
            MembershipUser _result = _service.CreateUser(this.UserNameTextBox.Text, this.PasswordTextBox.Text, this.EmailTextBox.Text, this.QuestionTextBox.Text, this.AnswerTextBox.Text, true, out _outStatus);

            if (_outStatus != MembershipCreateStatus.Success)
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Create User";
            }
            else
            {
                // create master_userextension
                _userBL.addPasswordExtension(_userBL.GetUserIDByName(this.UserNameTextBox.Text).ToString(), this.PasswordTextBox.Text);                

                // create user_employee if employee diisi
                if (this.EmployeeDropDownList.SelectedValue != "null")
                {
                    string _userId = _userBL.GetUserIdByUserName(_result.UserName);
                    User_Employee _userEmployee = new User_Employee();

                    _userEmployee.UserId = new Guid(_userId);
                    _userEmployee.EmployeeId = this.EmployeeDropDownList.SelectedValue;

                    bool _resultUserEmp = _userEmpBL.Add(_userEmployee);

                    if (_resultUserEmp == false)
                    {
                        this.ClearLabel();
                        this.WarningLabel.Text = "You Failed Add Employee Data";
                    }
                }

                // akumulasi role, masukkan ke _role
                string[] _role;
                String _tempRole = "";
                for (int i = 0; i < this.RoleCheckBoxList.Items.Count; i++)
                {
                    if (this.RoleCheckBoxList.Items[i].Selected == true)
                    {
                        if (_tempRole == "")
                        {
                            _tempRole = this.RoleCheckBoxList.Items[i].Value;
                        }
                        else
                        {
                            _tempRole = _tempRole + "," + this.RoleCheckBoxList.Items[i].Value;
                        }
                    }
                }

                // cek role kosong atau tidak (kalau ada isi jalankan AssignUserToRoles)
                if (_tempRole != "")
                {
                    _role = _tempRole.Split(',');

                    bool _resultRole = _service.AssignUserToRoles(_result.UserName, _role);

                    if (_resultRole != true)
                    {
                        this.ClearLabel();
                        this.WarningLabel.Text = "You Failed Add Role(s)";
                    }
                }

                Response.Redirect(this._homePage);
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }
    }
}