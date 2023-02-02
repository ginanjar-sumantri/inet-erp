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
    public partial class CustomerAddressEdit : CustomerBase
    {
        private CustomerBL _custBL = new CustomerBL();
        private CountryBL _countryBL = new CountryBL();
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
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

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral3;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowCountryDropdownlist();

                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ShowData()
        {
            MsCustAddress _msCustAddress = this._custBL.GetSingleCustAddress(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._deliveryCodeKey), ApplicationConfig.EncryptionKey));

            this.CustCodeTextBox.Text = _msCustAddress.CustCode;
            this.DeliveryCodeTextBox.Text = _msCustAddress.DeliveryCode;
            this.DeliveryNameTextBox.Text = _msCustAddress.DeliveryName;
            this.Address1TextBox.Text = _msCustAddress.DeliveryAddr1;
            this.Address2TextBox.Text = _msCustAddress.DeliveryAddr2;
            this.CountryDropDownList.SelectedValue = _msCustAddress.Country;
            this.PostalCodeTextBox.Text = _msCustAddress.ZipCode;
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
            MsCustAddress _msCustAddress = this._custBL.GetSingleCustAddress(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._deliveryCodeKey), ApplicationConfig.EncryptionKey));

            _msCustAddress.CustCode = this.CustCodeTextBox.Text;
            _msCustAddress.DeliveryCode = this.DeliveryCodeTextBox.Text;
            _msCustAddress.DeliveryName = this.DeliveryNameTextBox.Text;
            _msCustAddress.DeliveryAddr1 = this.Address1TextBox.Text;
            _msCustAddress.DeliveryAddr2 = this.Address2TextBox.Text;
            if (this.CountryDropDownList.SelectedValue != "null")
            {
                _msCustAddress.Country = this.CountryDropDownList.SelectedValue;
            }
            _msCustAddress.ZipCode = this.PostalCodeTextBox.Text;
            _msCustAddress.UserID = HttpContext.Current.User.Identity.Name;
            _msCustAddress.UserDate = DateTime.Now;

            bool _result = this._custBL.EditCustAddress(_msCustAddress);

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
            this.ShowData();
        }
    }
}