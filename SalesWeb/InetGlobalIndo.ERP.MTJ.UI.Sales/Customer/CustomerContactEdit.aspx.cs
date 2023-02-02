using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.Customer
{
    public partial class CustomerContactEdit : CustomerBase
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

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral2;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowReligionDropdownlist();
                this.ShowCountryDropdownlist();

                this.SetAttribute();
                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ShowData()
        {
            MsCustContact _msCustContact = this._custBL.GetSingleCustContact(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._itemCodeKey), ApplicationConfig.EncryptionKey)));

            this.CustCodeTextBox.Text = _msCustContact.CustCode;
            this.ItemNoTextBox.Text = Convert.ToString(_msCustContact.ItemNo);
            this.ContactNameTextBox.Text = _msCustContact.ContactName;
            this.ContactTitleTextBox.Text = _msCustContact.ContactTitle;
            this.ContactTypeDropDownList.SelectedValue = _msCustContact.ContactType;
            this.Addr1TextBox.Text = _msCustContact.Address1;
            this.Addr2TextBox.Text = _msCustContact.Address2;
            this.BirthDateTextBox.Text = Convert.ToString(_msCustContact.Birthday);
            this.ReligionDropDownList.SelectedValue = _msCustContact.Religion;
            this.CountryDropDownList.SelectedValue = _msCustContact.Country;
            this.PostalCodeTextBox.Text = _msCustContact.ZipCode;
            this.PhoneTextBox.Text = _msCustContact.Phone;
            this.FaxTextBox.Text = _msCustContact.Fax;
            this.EmailTextBox.Text = _msCustContact.Email;
            this.RemarkTextBox.Text = _msCustContact.Remark;
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
            MsCustContact _msCustContact = this._custBL.GetSingleCustContact(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._itemCodeKey), ApplicationConfig.EncryptionKey)));

            _msCustContact.ContactName = this.ContactNameTextBox.Text;
            _msCustContact.ContactTitle = this.ContactTitleTextBox.Text;
            _msCustContact.ContactType = this.ContactTypeDropDownList.SelectedValue;
            _msCustContact.Address1 = this.Addr1TextBox.Text;
            _msCustContact.Address2 = this.Addr2TextBox.Text;
            _msCustContact.Birthday = Convert.ToDateTime(this.BirthDateTextBox.Text);
            _msCustContact.Religion = this.ReligionDropDownList.SelectedValue;
            _msCustContact.Country = this.CountryDropDownList.SelectedValue;
            _msCustContact.ZipCode = this.PostalCodeTextBox.Text;
            _msCustContact.Phone = this.PhoneTextBox.Text;
            _msCustContact.Fax = this.FaxTextBox.Text;
            _msCustContact.Email = this.EmailTextBox.Text;
            _msCustContact.Remark = this.RemarkTextBox.Text;
            _msCustContact.UserID = HttpContext.Current.User.Identity.Name;
            _msCustContact.UserDate = DateTime.Now;

            bool _result = this._custBL.EditCustContact(_msCustContact);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.CustCodeTextBox.Text, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}