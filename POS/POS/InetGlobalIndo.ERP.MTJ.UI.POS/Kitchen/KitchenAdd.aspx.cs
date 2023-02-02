using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.Kitchen
{
    public partial class KitchenAdd : KitchenBase
    {
        private KitchenBL _kitchenBL = new KitchenBL();
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

                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            this.ClearLabel();

            this.KitchenCodeTextBox.Text = "";
            this.KitchenNameTextBox.Text = "";
        }
        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            POSMsKitchen _msKitchen = new POSMsKitchen();

            _msKitchen.KitchenCode = this.KitchenCodeTextBox.Text;
            _msKitchen.KitchenName = this.KitchenNameTextBox.Text;
            _msKitchen.Chef = this.ChefTextBox.Text;
            _msKitchen.Location = this.LocationTextBox.Text;
            _msKitchen.KitchenPrinterIPAddress = this.PrinterIPAddressTextBox.Text;
            _msKitchen.KitchenPrinterName = this.PrinterNameTextBox.Text;

            bool _result = this._kitchenBL.Add(_msKitchen);

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