using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.UI.POS.Member;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.ShippingZone
{
    public partial class ShippingZoneCountryAdd : ShippingZoneBase
    {
        private ShippingBL _shippingBL = new ShippingBL();
        private PermissionBL _permBL = new PermissionBL();
        private CountryBL _countryBL = new CountryBL();
        private CityBL _cityBL = new CityBL();

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

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleCountryDetailLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.EstimateTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.ShowCode();
                this.ShowCountry();
                //this.ShowCity();
                this.ClearData();
            }
        }

        protected void ShowCode()
        {
            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|');
            POSMsZone _posMsZone = this._shippingBL.GetSinglePOSMsZone(_tempSplit[0]);
            this.CodeTextBox.Text = _posMsZone.ZoneCode;
            this.NameTextBox.Text = _posMsZone.ZoneName;
        }

        protected void ShowCountry()
        {
            this.CountryDropDownList.Items.Clear();
            this.CountryDropDownList.DataTextField = "CountryName";
            this.CountryDropDownList.DataValueField = "CountryCode";
            this.CountryDropDownList.DataSource = this._countryBL.GetList();
            this.CountryDropDownList.DataBind();
            this.CountryDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowCity()
        {
            this.CityDropDownList.Items.Clear();
            this.CityDropDownList.DataTextField = "CityName";
            this.CityDropDownList.DataValueField = "CityCode";
            this.CityDropDownList.DataSource = this._cityBL.GetListCityForDDLByCountry(this.CountryDropDownList.SelectedValue);
            this.CityDropDownList.DataBind();
            this.CityDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            this.ClearLabel();
            this.CountryDropDownList.SelectedIndex = 0;
            this.CityDropDownList.SelectedIndex = 0;
            this.EstimateTextBox.Text = "0";
            this.FgActiveCheckBox.Checked = false;
            this.RemarkTextBox.Text = "";
        }

        protected void CheckValidData()
        {
            String _errorMsg = "";
            this.ClearLabel();
            if (this.CountryDropDownList.SelectedValue == "null")
                _errorMsg = _errorMsg + " Please Choose One of Country.";

            if (this.CityDropDownList.SelectedValue == "null")
                _errorMsg = _errorMsg + " Please Choose One of City.";

            if (this.EstimateTextBox.Text == "")
                this.EstimateTextBox.Text = "0";

            if (Convert.ToDecimal(this.EstimateTextBox.Text) <= 0)
                _errorMsg = _errorMsg + " Estimate Must More Than 0.";

            POSMsZoneCountry _posMsZoneCountry = this._shippingBL.GetSinglePOSMsZoneCountry(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), this.CountryDropDownList.SelectedValue, this.CityDropDownList.SelectedValue);
            if (_posMsZoneCountry != null)
                _errorMsg = _errorMsg + " Country : " + this.CountryDropDownList.SelectedItem + " With City = " + this.CityDropDownList.SelectedItem + " Already Exist.";

            this.WarningLabel.Text = _errorMsg.ToString();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                POSMsZoneCountry _POSMsZoneCountry = new POSMsZoneCountry();
                _POSMsZoneCountry.ZoneCode = this.CodeTextBox.Text;
                _POSMsZoneCountry.CountryCode = this.CountryDropDownList.SelectedValue;
                _POSMsZoneCountry.CityCode = this.CityDropDownList.SelectedValue;
                _POSMsZoneCountry.EstimationTime = this.EstimateTextBox.Text;
                if (this.FgActiveCheckBox.Checked == true)
                    _POSMsZoneCountry.FgActive = 'Y';
                else
                    _POSMsZoneCountry.FgActive = 'N';
                _POSMsZoneCountry.Remark = this.RemarkTextBox.Text;
                _POSMsZoneCountry.CreatedBy = HttpContext.Current.User.Identity.Name;
                _POSMsZoneCountry.CreatedDate = DateTime.Now;
                _POSMsZoneCountry.ModifiedBy = "";
                _POSMsZoneCountry.ModifiedDate = this._defaultdate;

                bool _result = this._shippingBL.AddPOSMsZoneCountry(_POSMsZoneCountry);

                if (_result == true)
                {
                    Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.WarningLabel.Text = "You Failed Add Data";
                }
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        protected void CountryDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowCity();
        }
}
}