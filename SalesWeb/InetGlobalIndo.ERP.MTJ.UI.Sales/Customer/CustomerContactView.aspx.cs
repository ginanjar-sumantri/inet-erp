using System;
using System.Web;
using System.Web.UI;
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
    public partial class CustomerContactView : CustomerBase
    {
        private CustomerBL _custBL = new CustomerBL();
        private ReligionBL _religionBL = new ReligionBL();
        private CountryBL _countryBL = new CountryBL();
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral2;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.SetButtonPermission();

                this.ClearLabel();
                this.ShowData();
            }
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
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
            this.ReligionTextBox.Text = _religionBL.GetReligionNameByCode(_msCustContact.Religion);
            this.CountryTextBox.Text = _countryBL.GetCountryNameByCode(_msCustContact.Country);
            this.PostalCodeTextBox.Text = _msCustContact.ZipCode;
            this.PhoneTextBox.Text = _msCustContact.Phone;
            this.FaxTextBox.Text = _msCustContact.Fax;
            this.EmailTextBox.Text = _msCustContact.Email;
            this.RemarkTextBox.Text = _msCustContact.Remark;
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_editDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + _itemCodeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._itemCodeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}