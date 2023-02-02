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
using VTSWeb.SystemConfig;
using VTSWeb.Common;
using VTSweb.DataMapping;

namespace VTSWeb.UI
{
    public partial class CustomerAdd : CustomerBase
    {
        private MsCustomerBL _customerBL = new MsCustomerBL();
        private MsCityBL _cityBL = new MsCityBL();
        private MsCustTypeBL _custTypeBL = new MsCustTypeBL();
        

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
                this.SetAttribute();

                this.ClearLabel();
                this.ClearData();
                this.ShowCity();
                this.ShowCustType();

                this.SaveButton.ImageUrl = "../images/save.jpg";
                this.ResetButton.ImageUrl = "../images/reset.jpg";
                this.CancelButton.ImageUrl = "../images/cancel.jpg";
            }
        }
        private void SetAttribute()
        {
            this.CustomerZipCodeTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.CustomerZipCodeTextBox.ClientID + "," + this.CustomerZipCodeTextBox.ClientID + ",500" + ");");
            this.CustomerPhoneTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.CustomerPhoneTextBox.ClientID + "," + this.CustomerPhoneTextBox.ClientID + ",500" + ");");
            this.CustomerFaxTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.CustomerFaxTextBox.ClientID + "," + this.CustomerFaxTextBox.ClientID + ",500" + ");");
            this.CustomerContHpTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.CustomerContHpTextBox.ClientID + "," + this.CustomerContHpTextBox.ClientID + ",500" + ");");

            //this.CustomerLimitTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.CustomerLimitTextBox.ClientID + "," + this.CustomerLimitTextBox.ClientID + ",500" + ");");
            //this.CustomerUseLimitTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.CustomerUseLimitTextBox.ClientID + "," + this.CustomerUseLimitTextBox.ClientID + ",500" + ");");

        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearData()
        {
            this.CustomerCodeTextBox.Text = "";
            this.CustomerContNameTextBox.Text = "";
            this.CustomerAddressTextBox.Text = "";
            this.CustomerAddress2TextBox.Text = "";
            this.CustomerZipCodeTextBox.Text = "";
            this.CustomerPhoneTextBox.Text = "";
            this.CustomerFaxTextBox.Text = "";
            this.CustomerEmailTextBox.Text = "";
            this.CustomerContactMailTextBox.Text = "";
            this.CustomerContNameTextBox.Text = "";
            this.CustomerContTitleTextBox.Text = "";
            this.CustomerContHpTextBox.Text = "";
            CustomerDataMapping.GetActive(this.CustomerFgActiveChecked.Checked);

        }
        public void ShowCustType()
        {
            this.CustTypeDropDownList.Items.Clear();
            this.CustTypeDropDownList.DataTextField = "CustTypeName";
            this.CustTypeDropDownList.DataValueField = "CustTypeCode";
            this.CustTypeDropDownList.DataSource = this._custTypeBL.GetCustTypeForDDL();
            this.CustTypeDropDownList.DataBind();
            this.CustTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }
        public void ShowCity()
        {
            this.CityDropDownList.Items.Clear();
            this.CityDropDownList.DataTextField = "CityName";
            this.CityDropDownList.DataValueField = "CityCode";
            this.CityDropDownList.DataSource = this._cityBL.GetCityForDDL();
            this.CityDropDownList.DataBind();
            this.CityDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }
 

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsCustomer _msCustomer = new MsCustomer();
         
            _msCustomer.CustCode = this.CustomerCodeTextBox.Text;
            _msCustomer.CustName = this.CustomerNameTextBox.Text;
            _msCustomer.CustType = this.CustTypeDropDownList.SelectedValue;
            _msCustomer.Address1 = this.CustomerAddressTextBox.Text;
            _msCustomer.Address2 = this.CustomerAddress2TextBox.Text;
            _msCustomer.City = this.CityDropDownList.SelectedValue;
            _msCustomer.ZipCode = this.CustomerZipCodeTextBox.Text;
            _msCustomer.Phone = this.CustomerPhoneTextBox.Text;
            _msCustomer.Fax = this.CustomerFaxTextBox.Text;
            _msCustomer.Email = this.CustomerEmailTextBox.Text;
            _msCustomer.ContactEmail = this.CustomerContactMailTextBox.Text;
            _msCustomer.ContactName = this.CustomerContNameTextBox.Text;
            _msCustomer.ContactTitle = this.CustomerContTitleTextBox.Text;
            _msCustomer.ContactHP = this.CustomerContHpTextBox.Text;
            _msCustomer.FgActive = CustomerDataMapping.GetActive(this.CustomerFgActiveChecked.Checked);

            
            bool _result = this._customerBL.Add(_msCustomer);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
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
