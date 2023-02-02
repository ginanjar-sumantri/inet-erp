using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.CashierPrinter
{
    public partial class CashierPrinterAdd : CashierPrinterBase
    {
        private CashierPrinterBL _cashierPrinterBL = new CashierPrinterBL();
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

            this.CodeTextBox.Text = "";
            this.NameTextBox.Text = "";
            this.HostNameTextBox.Text = "";
            this.LocationTextBox.Text = "";
            this.PrinterIPAddressTextBox.Text = "";
            this.PrinterNameTextBox.Text = "";
        }

        protected void CheckValidData()
        {
            this.ClearLabel();
            POSMsCashierPrinter _posMsCashierPrinter = this._cashierPrinterBL.GetSingle(this.CodeTextBox.Text);
            if (_posMsCashierPrinter != null)
                this.WarningLabel.Text = " Code " + this.CodeTextBox.Text + " Already Exist.";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                POSMsCashierPrinter _msCashierPrinter = new POSMsCashierPrinter();

                _msCashierPrinter.CashierPrinterCode = this.CodeTextBox.Text;
                _msCashierPrinter.CashierPrinterName = this.NameTextBox.Text;
                _msCashierPrinter.HostName = this.HostNameTextBox.Text;
                _msCashierPrinter.Location = this.LocationTextBox.Text;
                _msCashierPrinter.IPAddress = this.PrinterIPAddressTextBox.Text;
                _msCashierPrinter.PrinterName = this.PrinterNameTextBox.Text;
                _msCashierPrinter.FgActive = this.FgActiveCheckBox.Checked == true ? 'Y' : 'N';
                _msCashierPrinter.Remark = this.RemarkTextBox.Text;
                _msCashierPrinter.CreatedBy = HttpContext.Current.User.Identity.Name;
                _msCashierPrinter.CreatedDate = DateTime.Now;

                bool _result = this._cashierPrinterBL.Add(_msCashierPrinter);

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