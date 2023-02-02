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
using VTSWeb.Common;
using VTSWeb.SystemConfig;
using VTSWeb.DataMapping;
using VTSweb.DataMapping;


namespace VTSWeb.UI
{
    public partial class CustomerEdit : CustomerBase 
    {
        private MsCustomerBL _customerBL = new MsCustomerBL();
        private MsCityBL _cityBL = new MsCityBL();
        private MsCustTypeBL _custTypeBL = new MsCustTypeBL();
        
        //private EmployeeBL _employeeBL = new EmployeeBL();

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
                this.SaveButton.ImageUrl = "../images/save.jpg";
                this.ResetButton.ImageUrl = "../images/reset.jpg";
                this.CancelButton.ImageUrl = "../images/cancel.jpg";
                //this.Employee();

                this.PageTitleLiteral.Text = this._pageTitleLiteral;
                this.CustomerCodeTextBox.Attributes.Add("ReadOnly", "True");
                this.ClearLabel();
                this.SetAttribut();
                
                this.ShowData();
                this.ShowCity();
                this.ShowCustType();


            }
        }
        private void SetAttribut()
        {
            this.CustomerZipCodeTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.CustomerZipCodeTextBox.ClientID + "," + this.CustomerZipCodeTextBox.ClientID + ",500" + ");");
            this.CustomerPhoneTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.CustomerPhoneTextBox.ClientID + "," + this.CustomerPhoneTextBox.ClientID + ",500" + ");");
            //this.CustomerLimitTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.CustomerLimitTextBox.ClientID + "," + this.CustomerLimitTextBox.ClientID + ",500" + ");");
            //this.CustomerUseLimitTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.CustomerUseLimitTextBox.ClientID + "," + this.CustomerUseLimitTextBox.ClientID + ",500" + ");");
            this.CustomerFaxTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.CustomerFaxTextBox.ClientID + "," + this.CustomerFaxTextBox.ClientID + ",500" + ");");
            this.CustomerContHpTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.CustomerContHpTextBox.ClientID + "," + this.CustomerContHpTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
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

        public void ShowData()
        {
            MsCustomer _msCustomer = this._customerBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.CustomerCodeTextBox.Text = _msCustomer.CustCode;
            this.CustomerNameTextBox.Text = _msCustomer.CustName;
            this.CustTypeDropDownList.Text = _msCustomer.CustType;
            this.CustomerAddressTextBox.Text = _msCustomer.Address1;
            this.CustomerAddress2TextBox.Text = _msCustomer.Address2;
            this.CityDropDownList.Text = _msCustomer.City;
            this.CustomerZipCodeTextBox.Text = _msCustomer.ZipCode;
            this.CustomerPhoneTextBox.Text = _msCustomer.Phone;
            this.CustomerFaxTextBox.Text = _msCustomer.Fax;
            this.CustomerEmailTextBox.Text = _msCustomer.Email;
            this.CustomerContactMailTextBox.Text = _msCustomer.ContactEmail;
            this.CustomerContNameTextBox.Text = _msCustomer.ContactName;
            this.CustomerContTitleTextBox.Text = _msCustomer.ContactTitle;
            this.CustomerContHpTextBox.Text = _msCustomer.ContactHP;
            this.CustomerFgActiveChecked.Checked = CustomerDataMapping.GetActive((Char)_msCustomer.FgActive);
           
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsCustomer _msCustomer = this._customerBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

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


            bool _result = this._customerBL.Edit(_msCustomer);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
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
}
}

