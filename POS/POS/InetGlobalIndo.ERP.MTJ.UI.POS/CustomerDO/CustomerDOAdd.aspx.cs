using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using BusinessRule.POSInterface;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.CustomerDO
{
    public partial class CustomerDOAdd : CustomerDOBase
    {
        private CustomerDOBL _memberBL = new CustomerDOBL();
        private CityBL _cityBL = new CityBL();
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

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.SetAttribute();
                this.ShowCity();
                this.ClearData();
            }
        }

        protected void SetAttribute()
        {
            this.ZipCodeTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.ZipCodeTextBox.ClientID + ")");
            this.TelephoneTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.TelephoneTextBox.ClientID + ")");
            this.HandPhoneTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.HandPhoneTextBox.ClientID + ")");
        }

        protected void ShowCity()
        {
            this.CityDropDownList.Items.Clear();
            this.CityDropDownList.DataTextField = "CityName";
            this.CityDropDownList.DataValueField = "CityCode";
            this.CityDropDownList.DataSource = this._cityBL.GetListCityForDDL();
            this.CityDropDownList.DataBind();
            this.CityDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            DateTime _now = DateTime.Now;

            this.ClearLabel();

            this.CustomerDOCodeTextBox.Text = "";
            this.CustomerDONameTextBox.Text = "";
            this.Address1TextBox.Text = "";
            this.Address2TextBox.Text = "";
            this.CityDropDownList.SelectedValue = "null";
            this.ZipCodeTextBox.Text = "";
            this.TelephoneTextBox.Text = "";
            this.HandPhoneTextBox.Text = "";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            POSMsCustomerDO _msCustomerDO = new POSMsCustomerDO();

            _msCustomerDO.CustDOCode = this.CustomerDOCodeTextBox.Text;
            _msCustomerDO.Name = this.CustomerDONameTextBox.Text;
            _msCustomerDO.Address1 = this.Address1TextBox.Text;
            _msCustomerDO.Address2 = this.Address2TextBox.Text;
            _msCustomerDO.City = this.CityDropDownList.SelectedValue;
            _msCustomerDO.ZipCode = this.ZipCodeTextBox.Text;
            _msCustomerDO.Phone = this.TelephoneTextBox.Text;
            _msCustomerDO.HP = this.HandPhoneTextBox.Text;

            bool _result = this._memberBL.Add(_msCustomerDO);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}