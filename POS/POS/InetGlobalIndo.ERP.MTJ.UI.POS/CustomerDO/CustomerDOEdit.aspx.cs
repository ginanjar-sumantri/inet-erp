using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using System.Web.UI.WebControls;
using BusinessRule.POSInterface;



namespace InetGlobalIndo.ERP.MTJ.UI.POS.CustomerDO
{
    public partial class CustomerDOEdit : CustomerDOBase
    {
        private CustomerDOBL _customerDOBL = new CustomerDOBL();
        private PermissionBL _permBL = new PermissionBL();
        private CityBL _cityBL = new CityBL();

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
                this.PageTitleLiteral.Text = this._pageTitleLiteral;
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowCity();
                this.ShowData();
            }

        }
        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }


        protected void ShowCity()
        {
            this.CityDropDownList.Items.Clear();
            this.CityDropDownList.DataTextField = "CityName";
            this.CityDropDownList.DataValueField = "CityCode";
            this.CityDropDownList.DataSource = this._cityBL.GetListCityForDDL();
            this.CityDropDownList.DataBind();
            this.CityDropDownList.Items.Insert(0, new ListItem("[Choose One]","null"));
 
        }

        public void ShowData()
        {
            POSMsCustomerDO _msCustomerDO = this._customerDOBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.CustomerDOCodeTextBox.Text = _msCustomerDO.CustDOCode;
            this.CustomerDONameTextBox.Text = _msCustomerDO.Name;
            this.Address1TextBox.Text = _msCustomerDO.Address1;
            this.Address2TextBox.Text = _msCustomerDO.Address2;
            this.CityDropDownList.Text = _msCustomerDO.City;
            this.ZipCodeTextBox.Text = _msCustomerDO.ZipCode;
            this.TelephoneTextBox.Text = _msCustomerDO.Phone;
            this.HandPhoneTextBox.Text = _msCustomerDO.HP;
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            POSMsCustomerDO _msCustomerDO = this._customerDOBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msCustomerDO.CustDOCode = this.CustomerDOCodeTextBox.Text;
            _msCustomerDO.Name = this.CustomerDONameTextBox.Text;
            _msCustomerDO.Address1 = this.Address1TextBox.Text;
            _msCustomerDO.Address2 = this.Address2TextBox.Text;
            _msCustomerDO.City = this.CityDropDownList.SelectedValue;
            _msCustomerDO.ZipCode = this.ZipCodeTextBox.Text;
            _msCustomerDO.Phone = this.TelephoneTextBox.Text;
            _msCustomerDO.HP = this.HandPhoneTextBox.Text;


            bool result = this._customerDOBL.Edit(_msCustomerDO);

            if (result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }

        }
        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
            this.ClearLabel();
        }
}
}
