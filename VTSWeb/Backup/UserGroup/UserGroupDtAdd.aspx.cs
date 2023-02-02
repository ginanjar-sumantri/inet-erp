using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VTSWeb.Database;
using VTSWeb.BusinessRule;
using VTSWeb.SystemConfig;
using VTSWeb.Common;

namespace VTSWeb.UI
{
    public partial class UserGroupDtAdd : UserGroupBase
    {
        private UserGroupBL _UserGroupBL = new UserGroupBL();
        private EmployeeBL _employeeBL = new EmployeeBL();
        private PermissionBL _permBL = new PermissionBL();

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

                this.SetAttribute();

                this.ShowEmployee();
                this.ClearData();
            }
        }

        protected void SetAttribute()
        {

        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearData()
        {
            this.ClearLabel();

            this.EmployeeDropDownList.SelectedValue = "null";
        }

        public void ShowEmployee()
        {
            this.EmployeeDropDownList.Items.Clear();
            this.EmployeeDropDownList.DataTextField = "EmpName";
            this.EmployeeDropDownList.DataValueField = "EmpNumb";
            this.EmployeeDropDownList.DataSource = this._employeeBL.GetListEmpForAssignment(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            this.EmployeeDropDownList.DataBind();
            this.EmployeeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsUserGroupDt _msAccountGroupDt = new MsUserGroupDt();

            _msAccountGroupDt.UserGroupCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            _msAccountGroupDt.EmpNumb = this.EmployeeDropDownList.SelectedValue;

            bool _result = this._UserGroupBL.AddUserGroupDt(_msAccountGroupDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}