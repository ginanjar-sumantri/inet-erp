using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DataMapping;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.Customer
{
    public partial class CustomerContactAdd : CustomerBase
    {
        private CustomerBL _custBL = new CustomerBL();
        private ReligionBL _religionBL = new ReligionBL();
        private CountryBL _countryBL = new CountryBL();
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

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
                this.PageTitleLiteral.Text = this._pageTitleLiteral2;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.BirthDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.BirthDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.ShowReligionDropdownlist();
                this.ShowCountryDropdownlist();

                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.BirthDateTextBox.Attributes.Add("ReadOnly", "True");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
            //this.ItemNoTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
        }

        protected void ClearData()
        {
            this.ClearLabel();

            this.CustCodeTextBox.Text = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            this.ContactTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.ContactTypeDropDownList.SelectedValue = "null";
            this.ReligionDropDownList.SelectedValue = "null";
            this.CountryDropDownList.SelectedValue = "null";
            this.Addr1TextBox.Text = "";
            this.Addr2TextBox.Text = "";
            this.BirthDateTextBox.Text = "";
            this.ContactNameTextBox.Text = "";
            this.ContactTitleTextBox.Text = "";
            this.EmailTextBox.Text = "";
            this.FaxTextBox.Text = "";
            //this.ItemNoTextBox.Text = "";
            this.PhoneTextBox.Text = "";
            this.PostalCodeTextBox.Text = "";
            this.RemarkTextBox.Text = "";
        }

        private void ShowReligionDropdownlist()
        {
            this.ReligionDropDownList.Items.Clear();
            this.ReligionDropDownList.DataSource = this._religionBL.GetList();
            this.ReligionDropDownList.DataValueField = "ReligionCode";
            this.ReligionDropDownList.DataTextField = "ReligionName";
            this.ReligionDropDownList.DataBind();
            this.ReligionDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowCountryDropdownlist()
        {
            this.CountryDropDownList.Items.Clear();
            this.CountryDropDownList.DataSource = this._countryBL.GetList();
            this.CountryDropDownList.DataValueField = "CountryCode";
            this.CountryDropDownList.DataTextField = "CountryName";
            this.CountryDropDownList.DataBind();
            this.CountryDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsCustContact _msCustContact = new MsCustContact();
            int _maxItemNo = this._custBL.GetMaxNoItem(this.CustCodeTextBox.Text);

            _msCustContact.CustCode = this.CustCodeTextBox.Text;
            _msCustContact.ItemNo = _maxItemNo + 1;
            _msCustContact.ContactName = this.ContactNameTextBox.Text;
            _msCustContact.ContactTitle = this.ContactTitleTextBox.Text;
            _msCustContact.ContactType = this.ContactTypeDropDownList.SelectedValue;
            _msCustContact.Address1 = this.Addr1TextBox.Text;
            _msCustContact.Address2 = this.Addr2TextBox.Text;
            if (this.BirthDateTextBox.Text == "")
            {
                _msCustContact.Birthday = null;
            }
            else
            {
                _msCustContact.Birthday = Convert.ToDateTime(this.BirthDateTextBox.Text);
            }
            if (this.ReligionDropDownList.SelectedValue != "null")
            {
                _msCustContact.Religion = this.ReligionDropDownList.SelectedValue;
            }
            if (this.CountryDropDownList.SelectedValue != "null")
            {
                _msCustContact.Country = this.CountryDropDownList.SelectedValue;
            }
            _msCustContact.ZipCode = this.PostalCodeTextBox.Text;
            _msCustContact.Phone = this.PhoneTextBox.Text;
            _msCustContact.Fax = this.FaxTextBox.Text;
            _msCustContact.Email = this.EmailTextBox.Text;
            _msCustContact.Remark = this.RemarkTextBox.Text;
            _msCustContact.UserID = HttpContext.Current.User.Identity.Name;
            _msCustContact.UserDate = DateTime.Now;

            bool _result = this._custBL.AddCustContact(_msCustContact);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
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
    }
}