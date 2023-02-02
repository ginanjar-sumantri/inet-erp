using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Home.City
{
    public partial class CityAdd : CityBase
    {
        private CityBL _cityBL = new CityBL();
        private RegionalBL _regionalBL = new RegionalBL();
        private CountryBL _countryBL = new CountryBL();
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowRegional();
                this.ShowCountry();

                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowRegional()
        {
            this.RegionalDropDownList.DataTextField = "RegionalName";
            this.RegionalDropDownList.DataValueField = "RegionalCode";
            this.RegionalDropDownList.DataSource = this._regionalBL.GetList();
            this.RegionalDropDownList.DataBind();
            this.RegionalDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowCountry()
        {
            this.CountryDropDownList.DataTextField = "CountryName";
            this.CountryDropDownList.DataValueField = "CountryCode";
            this.CountryDropDownList.DataSource = this._countryBL.GetList();
            this.CountryDropDownList.DataBind();
            this.CountryDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ClearData()
        {
            this.ClearLabel();

            this.CityCodeTextBox.Text = "";
            this.CityNameTextBox.Text = "";
            this.RegionalDropDownList.SelectedValue = "null";
            this.CountryDropDownList.SelectedValue = "null";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsCity _msCity = new MsCity();

            _msCity.CityCode = this.CityCodeTextBox.Text;
            _msCity.CityName = this.CityNameTextBox.Text;
            _msCity.Regional = this.RegionalDropDownList.SelectedValue;
            _msCity.Country = this.CountryDropDownList.SelectedValue;
            _msCity.UserID = HttpContext.Current.User.Identity.Name;
            _msCity.UserDate = DateTime.Now;

            bool _result = this._cityBL.Add(_msCity);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}