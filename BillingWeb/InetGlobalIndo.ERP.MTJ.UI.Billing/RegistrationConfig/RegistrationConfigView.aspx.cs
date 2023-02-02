using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.RegistrationConfig
{
    public partial class RegistrationConfigView : RegistrationConfigBase
    {
        private RegistrationConfigBL _regConfigBL = new RegistrationConfigBL();
        private PermissionBL _permBL = new PermissionBL();
        private ProductBL _productBL = new ProductBL();

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

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.SetButtonPermission();
                this.ShowPaymentStatus();

                this.ClearLabel();
                this.ShowData();
            }
        }

        public void ShowPaymentStatus()
        {
            this.PaymentStatusRadioButtonList.Items.Insert(0, new ListItem(PaymentStatusDataMapper.GetPaymentStatusText(PaymentStatus.After), PaymentStatusDataMapper.GetPaymentStatusValue(PaymentStatus.After).ToString()));
            this.PaymentStatusRadioButtonList.Items.Insert(1, new ListItem(PaymentStatusDataMapper.GetPaymentStatusText(PaymentStatus.Before), PaymentStatusDataMapper.GetPaymentStatusValue(PaymentStatus.Before).ToString()));
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            BILMsRegistrationConfig _config = this._regConfigBL.GetSingleRegistrationConfig(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.RegistrationCodeTextBox.Text = _config.RegCode;
            this.RegistrationNameTextBox.Text = _config.RegName;
            this.PaymentStatusRadioButtonList.SelectedValue = _config.PaymentStatus;
            this.RegProductCodeTextBox.Text = _config.RegistrationProductCode + " - " +_productBL.GetProductNameByCode(_config.RegistrationProductCode);
            this.RegFeeTextBox.Text = Convert.ToDecimal(_config.RegistrationFee).ToString("#,##0.##");
            this.InstallationProductCodeTextBox.Text = _config.InstalationProductCode+ " - " +_productBL.GetProductNameByCode(_config.InstalationProductCode);
            this.InstalationFeeTextBox.Text = Convert.ToDecimal(_config.InstalationFee).ToString("#,##0.##");
            this.DepositProductCodeTextBox.Text = _config.DepositProductCode+" - " + _productBL.GetProductNameByCode(_config.DepositProductCode);
            this.DepositTextBox.Text = Convert.ToDecimal(_config.Deposit).ToString("#,##0.##");
            this.RemarkTextBox.Text = _config.Description;
            this.RecurringFirstChargeTextBox.Text = _config.RecurringFirstCharge.ToString();
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }
    }
}