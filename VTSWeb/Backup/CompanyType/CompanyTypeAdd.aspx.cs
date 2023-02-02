using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VTSWeb.Database;
using VTSWeb.BusinessRule;
using VTSWeb.SystemConfig;

namespace VTSWeb.UI
{
    public partial class CompanyTypeAdd : CompanyTypeBase
    {
        private MsCustTypeBL _CustTypeBL = new MsCustTypeBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies[ApplicationConfig.CookiesPreferences];
            if (cookie == null)
            {
                Response.Redirect("..\\Login.aspx");
            }
            if (!this.Page.IsPostBack == true)
            {
                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = "../images/save.jpg";
                this.ResetButton.ImageUrl = "../images/reset.jpg";
                this.CancelButton.ImageUrl = "../images/cancel.jpg";
            }

        }
        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearData()
        {
            this.CompanyTypeCodeTextBox.Text = "";
            this.CompanyTypeNameTextBox.Text = "";

        }
        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsCustType _msCompanyType = new MsCustType();

            _msCompanyType.CustTypeCode = this.CompanyTypeCodeTextBox.Text;
            _msCompanyType.CustTypeName = this.CompanyTypeNameTextBox.Text;


            bool _result = this._CustTypeBL.Add(_msCompanyType);
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
            this.ClearData();
            this.ClearLabel();
        }
    }
}
