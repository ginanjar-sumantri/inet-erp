using System;
using System.Web;
using System.Web.UI;
using VTSWeb.Database;
using VTSWeb.BusinessRule;
using VTSWeb.SystemConfig;

namespace VTSWeb.UI
{
    public partial class MsCountryAdd : CountryBase
    {
        private MsCountryBL _CountryBL = new MsCountryBL();

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
            this.CountryCodeTextBox.Text = "";
            this.CountryNameTextBox.Text = "";
      

        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsCountry _msMsCountry = new MsCountry();

            _msMsCountry.CountryCode = this.CountryCodeTextBox.Text;
            _msMsCountry.CountryName = this.CountryNameTextBox.Text;


            bool _result = this._CountryBL.Add(_msMsCountry);
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