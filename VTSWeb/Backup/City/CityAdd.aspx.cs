using System;
using System.Web;
using System.Web.UI;
using VTSWeb.Database;
using VTSWeb.BusinessRule;
using VTSWeb.SystemConfig;
using VTSWeb.Common;
using System.Web.UI.WebControls;

namespace VTSWeb.UI
{
    public partial class MsCityAdd : CityBase
    {
        private MsCityBL _cityBL = new MsCityBL();
        private MsRegionalBL _regionalBL = new MsRegionalBL();
        private MsCountryBL _countryBL = new MsCountryBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies[ApplicationConfig.CookiesPreferences];
            if (cookie == null)
            {
                Response.Redirect("..\\Login.aspx");
            }
            this.PageTitleLiteral.Text = this._pageTitleLiteral;
            this.ClearLabel();
            
            if (!this.Page.IsPostBack == true)
            {
                this.ClearLabel();
                this.ClearData();
                this.ShowRegional();
                this.ShowCountry();

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
            this.CityNameTextBox.Text = "";
            this.CityCodeTextBox.Text = "";
        }

        public void ShowRegional()
        {
            this.RegionalDropDownList.Items.Clear();
            this.RegionalDropDownList.DataTextField = "RegionalName";
            this.RegionalDropDownList.DataValueField = "RegionalCode";
            this.RegionalDropDownList.DataSource = this._regionalBL.GetRegionalForDDL();
            this.RegionalDropDownList.DataBind();
            this.RegionalDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }
        public void ShowCountry()
        {
            this.CountryDropDownList.Items.Clear();
            this.CountryDropDownList.DataTextField = "CountryName";
            this.CountryDropDownList.DataValueField = "CountryCode";
            this.CountryDropDownList.DataSource = this._countryBL.GetCountryForDDL();
            this.CountryDropDownList.DataBind();
            this.CountryDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }


        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsCity _msMsCity = new MsCity();

            _msMsCity.CityCode = this.CityCodeTextBox.Text;
            _msMsCity.CityName = this.CityNameTextBox.Text;
            _msMsCity.Regional = this.RegionalDropDownList.SelectedValue;
            _msMsCity.Country  = this.CountryDropDownList.SelectedValue;
            

            bool _result = this._cityBL.Add(_msMsCity);
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