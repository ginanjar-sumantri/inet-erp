using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.UI.POS.Member;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.Common;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.ShippingVendor
{
    public partial class ShippingVendorDtAdd : ShippingVendorBase
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

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
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
                this.ShowCode();
                this.ShowShippingZoneType();
                this.ClearData();
            }
        }

        protected void ShowCode()
        {
            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|');
            this.CodeTextBox.Text = _tempSplit[0];
        }

        protected void ShowShippingZoneType()
        {
            Char? _fgZone = this._vendorBL.GetSingle(this.CodeTextBox.Text).FgZone;
            this.ShippingZoneTypeDropDownList.Items.Clear();
            if (_fgZone != 'Y')
            {
                this.ShippingZoneTypeLiteral.Text = "Shipping Zone";

                this.ShippingZoneTypeDropDownList.DataTextField = "ShippingTypeName";
                this.ShippingZoneTypeDropDownList.DataValueField = "ShippingTypeCode";
                this.ShippingZoneTypeDropDownList.DataSource = this._shippingBL.GetListPOSMsShippingType(0, 1000, "", "");
            }
            else
            {
                this.ShippingZoneTypeLiteral.Text = "Shipping Type";

                this.ShippingZoneTypeDropDownList.DataTextField = "ZoneName";
                this.ShippingZoneTypeDropDownList.DataValueField = "ZoneCode";
                this.ShippingZoneTypeDropDownList.DataSource = this._shippingBL.GetListPOSMsZone(0, 1000, "", "");
            }
            this.ShippingZoneTypeDropDownList.DataBind();
            this.ShippingZoneTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            this.ClearLabel();
            this.ShippingZoneTypeDropDownList.SelectedIndex = 0;
            this.FgActiveCheckBox.Checked = false;
            this.RemarkTextBox.Text = "";
        }

        protected void CheckValidData()
        {
            String _errorMsg = "";
            this.ClearLabel();
            if (this.ShippingZoneTypeDropDownList.SelectedValue == "null")
                _errorMsg = _errorMsg + " Please Choose One of Shipping.";

            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|');
            POSMsShippingVendorDt _posMsShippingVendorDt = this._vendorBL.GetSingleDt(_tempSplit[0], this.ShippingZoneTypeDropDownList.SelectedValue);
            if (_posMsShippingVendorDt != null)
                _errorMsg = _errorMsg + "Shipping Type = " + this.ShippingZoneTypeDropDownList.SelectedItem + " Already Exist.";

            this.WarningLabel.Text = _errorMsg.ToString();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                POSMsShippingVendorDt _posMsShippingVendorDt = new POSMsShippingVendorDt();
                _posMsShippingVendorDt.VendorCode = this.CodeTextBox.Text;
                _posMsShippingVendorDt.ShippingZonaTypeCode = this.ShippingZoneTypeDropDownList.SelectedValue;
                if (this.FgActiveCheckBox.Checked == true)
                    _posMsShippingVendorDt.FgActive = 'Y';
                else
                    _posMsShippingVendorDt.FgActive = 'N';
                _posMsShippingVendorDt.Remark = this.RemarkTextBox.Text;
                _posMsShippingVendorDt.CreatedBy = HttpContext.Current.User.Identity.Name;
                _posMsShippingVendorDt.CreatedDate = DateTime.Now;
                _posMsShippingVendorDt.ModifiedBy = "";
                _posMsShippingVendorDt.ModifiedDate = this._defaultdate;

                bool _result = this._vendorBL.AddDt(_posMsShippingVendorDt);

                if (_result == true)
                {
                    Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.WarningLabel.Text = "You Failed Add Data";
                }
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}