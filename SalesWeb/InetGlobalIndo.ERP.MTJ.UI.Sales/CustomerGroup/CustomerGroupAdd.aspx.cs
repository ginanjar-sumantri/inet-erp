using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.CustomerGroup
{
    public partial class CustomerGroupAdd : CustomerGroupBase
    {
        private CustomerBL _customerBL = new CustomerBL();
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

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

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
            this.FgPPhCheckBox.Attributes.Add("OnClick", "DoEnableOrDisable(" + this.FgPPhCheckBox.ClientID + ", " + this.PPhTextBox.ClientID + ");");
            this.PPhTextBox.Attributes.Add("OnBlur", "ChangeFormat(" + this.PPhTextBox.ClientID + ");");
        }

        public void ClearData()
        {
            this.ClearLabel();
            this.CustGroupCodeTextBox.Text = "";
            this.CustGroupNameTextBox.Text = "";
            this.TypeDropDownList.SelectedValue = "LOKAL";
            this.FgPKPCheckBox.Checked = false;
            this.FgPPhCheckBox.Checked = false;
            this.PPhTextBox.Text = "0";
            this.PPhTextBox.Attributes.Add("ReadOnly", "True");
            this.PPhTextBox.Attributes.Add("BackColor", "#CCCCCC");
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            MsCustGroup _msCustGroup = new MsCustGroup();

            _msCustGroup.CustGroupCode = this.CustGroupCodeTextBox.Text;
            _msCustGroup.CustGroupName = this.CustGroupNameTextBox.Text;
            _msCustGroup.CustGroupType = this.TypeDropDownList.SelectedValue;
            _msCustGroup.FgPKP = CustomerDataMapper.IsChecked(this.FgPKPCheckBox.Checked);
            _msCustGroup.FgPPh = CustomerDataMapper.IsChecked(this.FgPPhCheckBox.Checked);
            _msCustGroup.PPH = Convert.ToDecimal(this.PPhTextBox.Text);
            _msCustGroup.UserID = HttpContext.Current.User.Identity.Name;
            _msCustGroup.UserDate = DateTime.Now;

            bool _result = this._customerBL.AddCustGroup(_msCustGroup);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.CustGroupCodeTextBox.Text, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
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