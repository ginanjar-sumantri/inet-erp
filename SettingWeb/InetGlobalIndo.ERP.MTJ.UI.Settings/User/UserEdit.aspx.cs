using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Authentication;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.User
{
    public partial class UserEdit : UserBase
    {
        private UserBL _userBL = new UserBL();
        private User_EmployeeBL _userEmpBL = new User_EmployeeBL();
        private EmployeeBL _empBL = new EmployeeBL();
        private RoleBL _roleBL = new RoleBL();
        private MembershipService _serviceBL = new MembershipService();

        private string _message = "";
        private string _userId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this.DummyButton.Attributes.Add("Style", "visibility:hidden");
            this.DummyButton.Enabled = false;

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveEmailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.SaveEmployeeButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.SaveRoleImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";

                if (this._nvcExtractor.GetValue(this._message) != "")
                {
                    this.WarningLabel.Text = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._message), ApplicationConfig.EncryptionKey);
                }

                this.ShowEmployee();
                this.ShowData();

                this.ShowRoleCBList();
                this.GetRoleData();

                this.ClearLabel();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            aspnet_User _aspuser = this._userBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            aspnet_Membership _aspmembership = this._userBL.GetSingleMembership(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            this.UserNameLabel.Text = _aspuser.UserName;
            this.EmailTextBox.Text = _aspmembership.Email;
            this._userId = _aspmembership.UserId.ToString();
            this.EmployeeDropDownList.SelectedValue = _userEmpBL.GetEmployeeIdByCode(_userId);
        }

        public void ShowEmployee()
        {
            aspnet_User _aspuser = this._userBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            String _empNumb = _userEmpBL.GetEmployeeIdByCode(_aspuser.UserId.ToString());

            this.EmployeeDropDownList.DataTextField = "EmpName";
            this.EmployeeDropDownList.DataValueField = "EmpNumb";
            this.EmployeeDropDownList.DataSource = this._empBL.GetListEmpForDDLUserNameEdit(_empNumb);
            this.EmployeeDropDownList.DataBind();
            this.EmployeeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowRoleCBList()
        {
            this.RoleCheckBoxList.DataTextField = "RoleName";
            this.RoleCheckBoxList.DataValueField = "RoleName";
            this.RoleCheckBoxList.DataSource = this._roleBL.GetListRoleName();
            this.RoleCheckBoxList.DataBind();
        }

        public void GetRoleData()
        {
            string[] _tempRole;
            _tempRole = _serviceBL.GetRolesForUser(this.UserNameLabel.Text);

            for (int i = 0; i < this.RoleCheckBoxList.Items.Count; i++)
            {
                for (int j = 0; j < _tempRole.Length; j++)
                {
                    if (this.RoleCheckBoxList.Items[i].Value == _tempRole[j])
                    {
                        this.RoleCheckBoxList.Items[i].Selected = true;
                    }
                }
            }
        }

        protected void SaveEmailButton_Click(object sender, ImageClickEventArgs e)
        {
            MembershipService _service = new MembershipService();

            bool _resultEmail = false;
            _resultEmail = _service.ChangeEmail(this.UserNameLabel.Text, this.EmailTextBox.Text);

            if (_resultEmail == true)
            {
                this.ClearLabel();
                //Response.Redirect(this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                this.WarningLabel.Text = "You succesfully edited the email";
            }
            else
            {
                this.WarningLabel.Text = "You Failed Change Email";
            }
        }

        protected void SaveEmployeeButton_Click(object sender, ImageClickEventArgs e)
        {
            bool _resultEmployee = false;

            _userId = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            bool _exist = _userEmpBL.IsUserEmpExists(_userId);

            if (_exist == true)
            {
                if (this.EmployeeDropDownList.SelectedValue != "null")
                {
                    User_Employee _userEmp = this._userEmpBL.GetSingle(_userId);
                    _userEmp.EmployeeId = this.EmployeeDropDownList.SelectedValue;

                    _resultEmployee = _userEmpBL.Edit(_userEmp);
                }
                else
                {
                    _resultEmployee = _userEmpBL.DeleteMulti(_userId);
                }
            }
            else
            {
                User_Employee _userEmp = new User_Employee();
                _userEmp.UserId = new Guid(_userId);
                _userEmp.EmployeeId = this.EmployeeDropDownList.SelectedValue;

                _resultEmployee = _userEmpBL.Add(_userEmp);
            }

            if (_resultEmployee == true)
            {
                this.ClearLabel();
                //Response.Redirect(this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                this.WarningLabel.Text = "You succesfully edited the employee";
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Change Employee";
            }
        }

        protected void SaveRoleImageButton_Click(object sender, ImageClickEventArgs e)
        {
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

            bool _result = _userBL.EditRoles(this.UserNameLabel.Text, _tempRole);

            this.ClearLabel();

            if (_result == true)
            {
                this.WarningLabel.Text = "You succesfully change Role(s)";
            }
            else
            {
                this.WarningLabel.Text = "You failed to change role(s)";
            }
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
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