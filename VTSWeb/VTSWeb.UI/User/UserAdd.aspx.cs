using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using VTSWeb.BusinessRule;
using VTSWeb.SystemConfig;
using VTSWeb.Common;
using VTSWeb.Database;

namespace VTSWeb.UI
{
    public partial class UserAdd : UserBase
    {
        private UserBL _userBL = new UserBL();
        private EmployeeBL _employeeBL = new EmployeeBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies[ApplicationConfig.CookiesPreferences];
            if (cookie == null)
            {
                Response.Redirect("..\\Login.aspx");
            }

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = "../images/save.jpg";
                this.ResetButton.ImageUrl = "../images/reset.jpg";
                this.CancelButton.ImageUrl = "../images/cancel.jpg";

                this.Employee();
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
            this.UserTextBox.Text = "";
            this.PassTextBox.Text = "";
            this.EmailTextBox.Text = "";
            this.ConfirmTextBox.Text = "";
        }

        protected void Employee()
        {
            this.EmpDropDownList.Items.Clear();
            this.EmpDropDownList.DataTextField = "EmpName";
            this.EmpDropDownList.DataValueField = "EmpNumb";
            this.EmpDropDownList.DataSource = this._employeeBL.GetListEmpForUser();
            this.EmpDropDownList.DataBind();
            this.EmpDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsUser _msUser = new MsUser();
            MsUser_MsEmployee _msUser_msEmployee = new MsUser_MsEmployee();

            _msUser.UserName = this.UserTextBox.Text;
            _msUser.LoweredUserName = this.UserTextBox.Text.ToLower();
            _msUser.Password = Rijndael.Encrypt(this.PassTextBox.Text, ApplicationConfig.EncryptionKey);
            _msUser.Email = this.EmailTextBox.Text;
            _msUser.PermissionLevelCode = 0;

            _msUser_msEmployee.UserName = this.UserTextBox.Text;
            _msUser_msEmployee.EmpNumb = this.EmpDropDownList.SelectedValue;

            bool _result = this._userBL.Add(_msUser, _msUser_msEmployee);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
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
