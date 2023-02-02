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
using VTSWeb.Common;
using VTSWeb.SystemConfig;
using VTSWeb.Database;

namespace VTSWeb.UI
{
    public partial class EmployeeView : EmployeeBase
    {
        private EmployeeBL _employeeBL = new EmployeeBL();

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
            MsEmployee _msEmployee = this._employeeBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.EmpNoTextBox.Text = _msEmployee.EmpNumb;
            this.EmpNameTextBox.Text = _msEmployee.EmpName;
            this.JobTitleTextBox.Text = _msEmployee.JobTitle;
            this.JobLevelTextBox.Text = _msEmployee.JobLevel;
            this.ActiveCheckBox.Checked = _msEmployee.Active;
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

