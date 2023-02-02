using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.Supplier
{
    public partial class SupplierContactAdd : SupplierBase
    {
        private SupplierBL _suppBL = new SupplierBL();
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

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral2; 

                this.SuppCodeTextBox.Text = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
                //this.ItemNoTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.SetAtttibute();
                this.ClearLabel();
                this.ShowCountryDropdownlist();
            }
        }

        protected void SetAtttibute()
        {
            this.PhoneTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.FaxTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowCountryDropdownlist()
        {
            this.CountryDropDownList.DataSource = this._suppBL.GetListCountryForDDL();
            this.CountryDropDownList.DataValueField = "CountryCode";
            this.CountryDropDownList.DataTextField = "CountryName";
            this.CountryDropDownList.DataBind();
            this.CountryDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsSuppContact _msSuppContact = new MsSuppContact();
            int _maxItemNo = this._suppBL.GetMaxNoItem(this.SuppCodeTextBox.Text);

            _msSuppContact.SuppCode = this.SuppCodeTextBox.Text;
            _msSuppContact.ItemNo = _maxItemNo + 1;
            _msSuppContact.ContactName = this.ContactNameTextBox.Text;
            _msSuppContact.ContactTitle = this.ContactTitleTextBox.Text;
            _msSuppContact.Address1 = this.Addr1TextBox.Text;
            _msSuppContact.Address2 = this.Addr2TextBox.Text;
            if (this.CountryDropDownList.SelectedValue != "null")
            {
                _msSuppContact.Country = this.CountryDropDownList.SelectedValue;
            }
            _msSuppContact.PostCode = this.PostalCodeTextBox.Text;
            _msSuppContact.Telephone = this.PhoneTextBox.Text;
            _msSuppContact.Fax = this.FaxTextBox.Text;
            _msSuppContact.Email = this.EmailTextBox.Text;

            bool _result = this._suppBL.AddSuppContact(_msSuppContact);

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
            Response.Redirect(_addDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}