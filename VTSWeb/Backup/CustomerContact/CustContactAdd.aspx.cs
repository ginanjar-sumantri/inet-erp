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
using VTSWeb.DataMapping;
using VTSweb.DataMapping;

namespace VTSWeb.UI
{
    public partial class CustContactAdd : CustContactBase
    {
        private MsCustContactBL _CustContactBL = new MsCustContactBL();
        private MsCustomerBL _CustomerBL = new MsCustomerBL();
        private MsReligionBL _ReligionBL = new MsReligionBL();
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
                this.PageTitleLiteral.Text = this._pageTitleLiteral;
                this.SetAttribute();

                this.ClearLabel();
                this.ClearData();
                this.ShowCustomer();
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

            //this.CustomerLimitTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.CustomerLimitTextBox.ClientID + "," + this.CustomerLimitTextBox.ClientID + ",500" + ");");
            //this.CustomerUseLimitTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.CustomerUseLimitTextBox.ClientID + "," + this.CustomerUseLimitTextBox.ClientID + ",500" + ");");

        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearData()
        {
            DateTime _now = DateTime.Now;
            this.NameTextBox.Text = "";
            this.TitleTextBox.Text = "";
            this.RemarkTextBox.Text = "";
            this.Address1TextBox.Text = "";
            this.Address2TextBox.Text = "";
            this.ZipCodeTextBox.Text = "";
            this.PhoneTextBox.Text = "";
            this.FaxTextBox.Text = "";
            this.EmailTextBox.Text = "";
            this.BirthdayTextBox.Text = DateFormMapping.GetValue(_now);
        }
        public void ShowCustomer()
        {
            this.CustomerDropDownList.Items.Clear();
            this.CustomerDropDownList.DataTextField = "CustName";
            this.CustomerDropDownList.DataValueField = "CustCode";
            this.CustomerDropDownList.DataSource = this._CustomerBL.GetCustomerForDDL();
            this.CustomerDropDownList.DataBind();
            this.CustomerDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
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
       
        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsCustContact _msCustContact = new MsCustContact();

            _msCustContact.CustCode = this.CustomerDropDownList.SelectedValue;
            _msCustContact.ItemNo = this._CustContactBL.GetMaxItemNoByCode(this.CustomerDropDownList.SelectedValue) + 1;
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
            _msCustContact.CardID = this.CardIDTextBox.Text;
            _msCustContact.FgAccess = CustomerContactDataMapping.GetChekCustContact(this.FgAccessChecked.Checked);
            _msCustContact.FgAdditionalVisitor =  CustomerContactDataMapping.GetChekCustContact(this.FgAdditionalVisitorChecked.Checked);
            _msCustContact.FgAuthorizationContact =  CustomerContactDataMapping.GetChekCustContact(this.FgContactAuthorizationChecked.Checked);
            _msCustContact.FgGoodsIn =  CustomerContactDataMapping.GetChekCustContact(this.FgGoodsInChecked.Checked);
            _msCustContact.FgGoodsOut =  CustomerContactDataMapping.GetChekCustContact(this.FgGoodsOutChecked.Checked);


            bool _result = this._CustContactBL.Add(_msCustContact);

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
