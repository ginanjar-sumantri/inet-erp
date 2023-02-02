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
    public partial class CustomerView : CustomerBase
    {
        private MsCustomerBL _customerBL = new MsCustomerBL();
        private MsCityBL _cityBL = new MsCityBL();
        private MsCustTypeBL _custTypeBL = new MsCustTypeBL();

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
                this.EditButton.ImageUrl = "../images/edit2.jpg";
                this.CancelButton.ImageUrl = "../images/cancel.jpg";
                
                this.PageTitleLiteral.Text = this._pageTitleLiteral;
                this.CustomerCodeTextBox.Attributes.Add("ReadOnly", "True");
                this.ShowData();

            }
        }

        public void ShowData()
        {
            MsCustomer _msCustomer = this._customerBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            //MsUser_MsEmployee _msUser_msEmployee = this._employeeBL.GetSingleEmpForUser(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            //String _EmpName = this._employeeBL.GetEmployeeNameByCode(_msUser_msEmployee.EmpNumb);

            this.CustomerCodeTextBox.Text = _msCustomer.CustCode;
            this.CustomerNameTextBox.Text = _msCustomer.CustName;
            this.TypeTextBox.Text = _msCustomer.CustType;
            this.CustomerAddressTextBox.Text = _msCustomer.Address1;
            this.CustomerAddress2TextBox.Text = _msCustomer.Address2;
            this.CityTextBox.Text = _msCustomer.City;
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

