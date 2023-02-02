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
using VTSWeb.Database;
using VTSWeb.BusinessRule;
using VTSWeb.SystemConfig;
using VTSWeb.Common;

namespace VTSWeb.UI
{
    public partial class EmployeeAdd : EmployeeBase
    {
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
            this.EmpNoTextBox.Text = "";
            this.EmpNameTextBox.Text = "";
            this.JobTitleTextBox.Text = "";
            this.JobLevelTextBox.Text = "";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsEmployee _msEmployee = new MsEmployee();

            _msEmployee.EmpNumb = this.EmpNoTextBox.Text;
            _msEmployee.EmpName = this.EmpNameTextBox.Text;
            _msEmployee.JobLevel = this.JobLevelTextBox.Text;
            _msEmployee.JobTitle = this.JobTitleTextBox.Text;
            _msEmployee.Active = this.ActiveCheckBox.Checked;

            bool _result = this._employeeBL.Add(_msEmployee);

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