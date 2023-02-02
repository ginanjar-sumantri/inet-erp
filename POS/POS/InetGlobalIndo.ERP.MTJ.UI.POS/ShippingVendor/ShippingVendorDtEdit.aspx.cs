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
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.UI.POS.Member;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using System.Web.UI.WebControls;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.ShippingVendor
{
    public partial class ShippingVendorDtEdit : ShippingVendorBase
    {
        private VendorBL _vendorBL = new VendorBL();
        private PermissionBL _permBL = new PermissionBL();
        private ShippingBL _shippingBL = new ShippingBL();

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
                this.PageTitleLiteral.Text = this._pageTitleDetailLiteral;
                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.CodeTextBox.Attributes.Add("ReadOnly", "True");
                this.ShippingZoneTypeTextBox.Attributes.Add("ReadOnly", "True");
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
            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|');

            POSMsShippingVendorDt _posMsShippingVendorDt = this._vendorBL.GetSingleDt(_tempSplit[0], _tempSplit[1]);
            this.CodeTextBox.Text = _posMsShippingVendorDt.VendorCode;
            if (_posMsShippingVendorDt.FgActive == 'Y')
                this.FgActiveCheckBox.Checked = true;
            else
                this.FgActiveCheckBox.Checked = false;
            this.RemarkTextBox.Text = _posMsShippingVendorDt.Remark;
            String _shippingZoneTypeName = "";

            Char? _fgZone = this._vendorBL.GetSingle(this.CodeTextBox.Text).FgZone;

            if (_fgZone != 'Y')
            {
                this.ShippingZoneTypeLiteral.Text = "Shipping Type";
                _shippingZoneTypeName = this._shippingBL.GetSinglePOSMsShippingType(_tempSplit[1]).ShippingTypeName;
            }
            else
            {
                this.ShippingZoneTypeLiteral.Text = "Shipping Zone";
                _shippingZoneTypeName = this._shippingBL.GetSinglePOSMsZone(_tempSplit[1]).ZoneName;
            }
            this.ShippingZoneTypeTextBox.Text = _shippingZoneTypeName;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|');
            POSMsShippingVendorDt _posMsShippingVendorDt = this._vendorBL.GetSingleDt(_tempSplit[0], _tempSplit[1]);
            if (this.FgActiveCheckBox.Checked == true)
                _posMsShippingVendorDt.FgActive = 'Y';
            else
                _posMsShippingVendorDt.FgActive = 'N';
            _posMsShippingVendorDt.Remark = this.RemarkTextBox.Text;
            _posMsShippingVendorDt.ModifiedBy = HttpContext.Current.User.Identity.Name;
            _posMsShippingVendorDt.ModifiedDate = DateTime.Now;

            bool _result = this._vendorBL.EditSubmit();

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}