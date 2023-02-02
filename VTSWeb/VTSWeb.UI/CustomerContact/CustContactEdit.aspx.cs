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
    public partial class CustContactEdit : CustContactBase
    {
        private MsCustContactBL _CustContactBL = new MsCustContactBL();
        private MsCustomerBL _CustomerBL = new MsCustomerBL();
        private MsReligionBL _ReligionBL = new MsReligionBL();
        private MsCountryBL _CountryBL = new MsCountryBL();

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
                this.SetAttribute(); 
                this.ItemTextBox.Attributes.Add("ReadOnly", "True");
                this.CustNameTextBox.Attributes.Add("ReadOnly", "True");

                this.ClearLabel();
                this.ShowData();
                this.ShowReligion();
                this.ShowCountry();

                this.SaveButton.ImageUrl = "../images/save.jpg";
                this.ResetButton.ImageUrl = "../images/reset.jpg";
                this.CancelButton.ImageUrl = "../images/cancel.jpg";
            }
        }
        private void SetAttribute()
        {
            this.ZipCodeTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.ZipCodeTextBox.ClientID + "," + this.ZipCodeTextBox.ClientID + ",500" + ");");
            this.PhoneTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.PhoneTextBox.ClientID + "," + this.PhoneTextBox.ClientID + ",500" + ");");
            this.FaxTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.FaxTextBox.ClientID + "," + this.FaxTextBox.ClientID + ",500" + ");");
            //this.CustContact LimitTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.CustContact LimitTextBox.ClientID + "," + this.CustContact LimitTextBox.ClientID + ",500" + ");");
            //this.CustContact UseLimitTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.CustContact UseLimitTextBox.ClientID + "," + this.CustContact UseLimitTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowReligion()
        {
            this.ReligionDropDownList.Items.Clear();
            this.ReligionDropDownList.DataTextField = "ReligionName";
            this.ReligionDropDownList.DataValueField = "ReligionCode";
            this.ReligionDropDownList.DataSource = this._ReligionBL.GetReligonForDDL();
            this.ReligionDropDownList.DataBind();
            this.ReligionDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowCountry()
        {
            this.CountryDropDownList.Items.Clear();
            this.CountryDropDownList.DataTextField = "CountryName";
            this.CountryDropDownList.DataValueField = "CountryCode";
            this.CountryDropDownList.DataSource = this._CountryBL.GetCountryForDDL();
            this.CountryDropDownList.DataBind();
            this.CountryDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }
        public void ShowData()
        {
            MsCustContact _msCustContact = this._CustContactBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            //MsUser_MsEmployee _msUser_msEmployee = this._employeeBL.GetSingleEmpForUser(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            //String _EmpName = this._employeeBL.GetEmployeeNameByCode(_msUser_msEmployee.EmpNumb);

            this.CustNameTextBox.Text =(_CustomerBL.GetSingle(_msCustContact.CustCode)).CustName;
            this.ItemTextBox.Text = Convert.ToString(_msCustContact.ItemNo);
            this.NameTextBox.Text = _msCustContact.ContactName;
            this.TitleTextBox.Text = _msCustContact.ContactTitle;
            this.TypeDropDownList.SelectedValue = _msCustContact.ContactTitle;
            this.ReligionDropDownList.SelectedValue = _msCustContact.Religion;
            this.BirthdayTextBox.Text = DateFormMapping.GetValue(_msCustContact.Birthday);
            this.RemarkTextBox.Text = _msCustContact.Remark;
            this.Address1TextBox.Text = _msCustContact.Address1;
            this.Address2TextBox.Text = _msCustContact.Address2;
            this.CountryDropDownList.SelectedValue = _msCustContact.Country;
            this.ZipCodeTextBox.Text = _msCustContact.ZipCode;
            this.PhoneTextBox.Text = _msCustContact.Phone;
            this.FaxTextBox.Text = _msCustContact.Fax;
            this.EmailTextBox.Text = _msCustContact.Email;
            this.FgAccessChecked.Checked = CustomerContactDataMapping.GetChekCustContact((Char)_msCustContact.FgAccess);
            this.FgGoodsInChecked.Checked = CustomerContactDataMapping.GetChekCustContact((Char)_msCustContact.FgGoodsIn);
            this.FgGoodsOutChecked.Checked = CustomerContactDataMapping.GetChekCustContact((Char)_msCustContact.FgGoodsOut);
            this.FgAdditionalVisitorChecked.Checked = CustomerContactDataMapping.GetChekCustContact((Char)_msCustContact.FgAdditionalVisitor);
            this.FgContactAuthorizationChecked.Checked = CustomerContactDataMapping.GetChekCustContact((Char)_msCustContact.FgAuthorizationContact);
            this.CardIDTextBox.Text = _msCustContact.CardID;

        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            //HttpCookie cookie = Request.Cookies[ApplicationConfig.CookiesPreferences];
            MsCustContact _msCustContact = this._CustContactBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msCustContact.ContactName = this.NameTextBox.Text;
            _msCustContact.ContactTitle = this.TitleTextBox.Text;
            _msCustContact.ContactType = this.TypeDropDownList.SelectedValue;
            _msCustContact.Religion = this.ReligionDropDownList.SelectedValue;
            _msCustContact.Birthday = DateFormMapping.GetValue(this.BirthdayTextBox.Text);
            _msCustContact.Remark = this.RemarkTextBox.Text;
            _msCustContact.Address1 = this.Address1TextBox.Text;
            _msCustContact.Address2 = this.Address2TextBox.Text;
            _msCustContact.Country = this.CountryDropDownList.SelectedValue;
            _msCustContact.ZipCode = this.ZipCodeTextBox.Text;
            _msCustContact.Phone = this.PhoneTextBox.Text;
            _msCustContact.Fax = this.FaxTextBox.Text;
            _msCustContact.Email = this.EmailTextBox.Text;

            _msCustContact.FgAccess = CustomerContactDataMapping.GetChekCustContact(this.FgAccessChecked.Checked);
            _msCustContact.FgGoodsIn = CustomerContactDataMapping.GetChekCustContact(this.FgGoodsInChecked.Checked);
            _msCustContact.FgGoodsOut = CustomerContactDataMapping.GetChekCustContact(this.FgGoodsOutChecked.Checked);
            _msCustContact.FgAdditionalVisitor = CustomerContactDataMapping.GetChekCustContact(this.FgAdditionalVisitorChecked.Checked);
            _msCustContact.FgAuthorizationContact = CustomerContactDataMapping.GetChekCustContact(this.FgContactAuthorizationChecked.Checked);
            _msCustContact.CardID = this.CardIDTextBox.Text;

            bool _result = this._CustContactBL.Edit(_msCustContact);

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

