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
    public partial class EmployeeEdit : EmployeeBase
    {
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
                this.ResetButton.ImageUrl = "../images/reset.jpg";
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
            MsEmployee _msEmployee = this._employeeBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.EmpNoTextBox.Text = _msEmployee.EmpNumb;
            this.EmpNameTextBox.Text = _msEmployee.EmpName;
            this.JobTitleTextBox.Text = _msEmployee.JobTitle;
            this.JobLevelTextBox.Text = _msEmployee.JobLevel;
            this.ActiveCheckBox.Checked = _msEmployee.Active;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsEmployee _msEmployee = this._employeeBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msEmployee.EmpName = this.EmpNameTextBox.Text;
            _msEmployee.JobTitle = this.JobTitleTextBox.Text;
            _msEmployee.JobLevel = this.JobLevelTextBox.Text;
            _msEmployee.Active = this.ActiveCheckBox.Checked;

            bool _result = this._employeeBL.Edit(_msEmployee);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
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
