using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.CashierPrinter
{
    public partial class CashierPrinterEdit : CashierPrinterBase
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
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.CodeTextBox.Attributes.Add("ReadOnly", "True");

                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            POSMsCashierPrinter _msCashierPrinter = this._cashierPrinterBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.CodeTextBox.Text = _msCashierPrinter.CashierPrinterCode;
            this.NameTextBox.Text = _msCashierPrinter.CashierPrinterName;
            this.HostNameTextBox.Text = _msCashierPrinter.HostName;
            this.LocationTextBox.Text = _msCashierPrinter.Location;
            this.PrinterIPAddressTextBox.Text = _msCashierPrinter.IPAddress;
            this.PrinterNameTextBox.Text = _msCashierPrinter.PrinterName;
            this.FgActiveCheckBox.Checked = _msCashierPrinter.FgActive == 'Y' ? true : false;
            this.RemarkTextBox.Text = _msCashierPrinter.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            POSMsCashierPrinter _msCashierPrinter = this._cashierPrinterBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msCashierPrinter.CashierPrinterName = this.NameTextBox.Text;
            _msCashierPrinter.HostName = this.HostNameTextBox.Text;
            _msCashierPrinter.Location = this.LocationTextBox.Text;
            _msCashierPrinter.IPAddress = this.PrinterIPAddressTextBox.Text;
            _msCashierPrinter.PrinterName = this.PrinterNameTextBox.Text;
            _msCashierPrinter.FgActive = this.FgActiveCheckBox.Checked == true ? 'Y' : 'N';
            _msCashierPrinter.Remark = this.RemarkTextBox.Text;
            _msCashierPrinter.ModifiedBy = HttpContext.Current.User.Identity.Name;
            _msCashierPrinter.ModifiedDate = DateTime.Now;

            bool _result = this._cashierPrinterBL.Edit();

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}