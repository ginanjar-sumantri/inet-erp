using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VTSWeb.Database;
using VTSWeb.SystemConfig;
using VTSWeb.BusinessRule;
using VTSWeb.Common;

namespace VTSWeb.UI
{
    public partial class CityEdit : CityBase
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
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = this._pageTitleLiteral;
                this.CityCodeTextBox.Attributes.Add("ReadOnly", "True");
                this.ShowData();
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
            this.CityCodeTextBox.Text = "";
            this.CityNameTextBox.Text = "";
            this.RegionalDropDownList.SelectedValue = "";
            this.CountryDropDownList.SelectedValue = "";

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
        
        public void ShowData()
        {
            MsCity _msMsCity = this._cityBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.CityCodeTextBox.Text = _msMsCity.CityCode;
            this.CityNameTextBox.Text = _msMsCity.CityName;
            this.RegionalDropDownList.Text = _msMsCity.Regional;
            this.CountryDropDownList.Text = _msMsCity.Country; 
           
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
        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsCity _msMsCity = this._cityBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msMsCity.CityName = this.CityNameTextBox.Text;
            _msMsCity.Regional = this.RegionalDropDownList.SelectedValue;
            _msMsCity.Country = this.CountryDropDownList.SelectedValue;

            bool _result = this._cityBL.Edit(_msMsCity);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }

        }
    }


}