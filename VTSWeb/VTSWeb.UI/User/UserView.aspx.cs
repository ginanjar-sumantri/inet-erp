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
    public partial class UserView : UserBase
    {
        private UserBL _userBL = new UserBL();
        private EmployeeBL _employeeBL = new EmployeeBL();
        //private PermissionBL _permissionBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies[ApplicationConfig.CookiesPreferences];
            if (cookie == null)
            {
                Response.Redirect("..\\Login.aspx");
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                MsUser _MsUser = this._userBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.EditButton.ImageUrl = "../images/edit2.jpg";

                this.CancelButton.ImageUrl = "../images/cancel.jpg";

                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            MsUser _msUser = this._userBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            MsUser_MsEmployee _msUser_msEmployee = this._employeeBL.GetSingleEmpForUser(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            String _EmpName = this._employeeBL.GetEmployeeNameByCode(_msUser_msEmployee.EmpNumb);

            this.UserTextBox.Text = _msUser.UserName;
            this.PassTextBox.Text = _msUser.Password;
            this.EmailTextBox.Text = _msUser.Email;
            this.EmpTextBox.Text = _msUser_msEmployee.EmpNumb + " - " + _EmpName;
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }


    }
}
