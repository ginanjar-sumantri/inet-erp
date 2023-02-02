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
using VTSWeb.Database;
using VTSWeb.Common;
using VTSWeb.SystemConfig;

namespace VTSWeb.UI
{
    public partial class UserEdit : UserBase
    {
        private UserBL _userBL = new UserBL();
        private EmployeeBL _employeeBL = new EmployeeBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            HttpCookie cookie = Request.Cookies[ApplicationConfig.CookiesPreferences];
            if (cookie == null)
            {
                Response.Redirect("..\\Login.aspx");
            }

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = "../images/save.jpg";
                this.SavePasswordImageButton.ImageUrl = "../images/save.jpg";
                this.ResetButton.ImageUrl = "../images/reset.jpg";
                this.CancelButton.ImageUrl = "../images/cancel.jpg";

                this.SetAttribute();

                this.Employee();
                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribute()
        {
        }

        public void ShowData()
        {
            MsUser _msUser = this._userBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            MsUser_MsEmployee _msUser_msEmployee = this._employeeBL.GetSingleEmpForUser(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            String _EmpName = this._employeeBL.GetEmployeeNameByCode(_msUser_msEmployee.EmpNumb);

            this.UserTextBox.Text = _msUser.UserName;
            this.EmailTextBox.Text = _msUser.Email;
            this.PassTextBox.Text = _msUser.Password;
            this.EmpDropDownList.SelectedValue = _msUser_msEmployee.EmpNumb;
        }

        protected void Employee()
        {
            this.EmpDropDownList.Items.Clear();
            this.EmpDropDownList.DataTextField = "EmpName";
            this.EmpDropDownList.DataValueField = "EmpNumb";
            this.EmpDropDownList.DataSource = this._employeeBL.GetListEmpForUserEdit(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            this.EmpDropDownList.DataBind();
            this.EmpDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsUser _msUser = this._userBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msUser.Email = this.EmailTextBox.Text;

            MsUser_MsEmployee _msUser_msEmployee = this._employeeBL.GetSingleEmpForUser(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            bool _result = this._userBL.EditUserEmployee(_msUser_msEmployee, _msUser_msEmployee.UserName, this.EmpDropDownList.SelectedValue);

            if (_result == true)
            {
                this.WarningLabel.Text = "You Successfully Changed User Profile";
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void SavePasswordImageButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.PassTextBox.Text.Trim() != "")
            {

                MsUser _msUser = this._userBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                _msUser.Password = this.PassTextBox.Text;

                bool _result = this._userBL.Edit(_msUser);

                if (_result == true)
                {
                    this.WarningLabel.Text = "You Successfully Changed User Password";
                }
                else
                {
                    this.WarningLabel.Text = "You Failed Edit Password";
                }
            }
            else
            {
                this.WarningLabel.Text = "Password must be filled";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
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

