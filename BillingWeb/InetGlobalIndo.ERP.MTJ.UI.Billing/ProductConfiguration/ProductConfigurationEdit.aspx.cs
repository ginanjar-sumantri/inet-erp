using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.UI.Billing.BillingInvoice;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.ProductConfiguration
{
    public partial class ProductConfigurationEdit : ProductConfigurationBase
    {
        private ProductConfigurationBL _productConfigurationBL = new ProductConfigurationBL();
        private PermissionBL _permBL = new PermissionBL();
        private ProductBL _productBL = new ProductBL();

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
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.SetAttribute();

                this.ClearLabel();
                this.ShowData();
            }
        }

        private void SetAttribute()
        {
            this.ContractMonthTextBox.Attributes.Add("OnKeyUp", "formatangka(" + this.ContractMonthTextBox.ClientID + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ShowData()
        {
            Master_ProductExtension _masterProductExtension = this._productConfigurationBL.GetSingleProductExtension(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.ProductCodeTextbox.Text = _masterProductExtension.ProductCode;
            this.ProductNameTextBox.Text = this._productBL.GetProductNameByCode(_masterProductExtension.ProductCode);
            this.IsPostponeCheckbox.Checked = _masterProductExtension.IsPostponeAllowed;
            this.ChangePriceCheckBox.Checked = Convert.ToBoolean(_masterProductExtension.IsChangePriceAllowed);
            this.MRCCheckBox.Checked = Convert.ToBoolean(_masterProductExtension.IsMRC);
            this.ContractMonthTextBox.Text = Convert.ToString(_masterProductExtension.MinContractMonth);

        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            Master_ProductExtension _masterProductExtension = this._productConfigurationBL.GetSingleProductExtension(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _masterProductExtension.IsPostponeAllowed = this.IsPostponeCheckbox.Checked;
            _masterProductExtension.IsChangePriceAllowed = this.ChangePriceCheckBox.Checked;
            _masterProductExtension.IsMRC = this.MRCCheckBox.Checked;
            _masterProductExtension.MinContractMonth = Convert.ToInt32(this.ContractMonthTextBox.Text);

            _masterProductExtension.EditBy = HttpContext.Current.User.Identity.Name;
            _masterProductExtension.EditDate = DateTime.Now;

            bool _result = this._productConfigurationBL.EditProductExtension(_masterProductExtension);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}
