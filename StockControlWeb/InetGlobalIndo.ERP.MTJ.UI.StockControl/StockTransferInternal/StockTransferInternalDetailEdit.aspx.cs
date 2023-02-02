using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockTransferInternal
{
    public partial class StockTransferInternalDetailEdit : StockTransferInternalBase
    {
        private StockTransferInternalBL _stcTransInternal = new StockTransferInternalBL();
        private StockTransRequestBL _stockTransReq = new StockTransRequestBL();
        private ProductBL _product = new ProductBL();
        private WarehouseBL _warehouse = new WarehouseBL();
        private UnitBL _unit = new UnitBL();
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
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetAttribute();
                this.ClearLabel();
                this.ShowData();
            }
        }

        private void SetAttribute()
        {
            this.QtyTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowData()
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _locationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);
            string _locationCode2 = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey2), ApplicationConfig.EncryptionKey);

            STCTransferDt _stcTransInternalDt = this._stcTransInternal.GetSingleSTCTransferDt(_transNo, _productCode, _locationCode, _locationCode2);

            this.ProductTextBox.Text = _product.GetProductNameByCode(_stcTransInternalDt.ProductCode);
            this.LocationSrcTextBox.Text = _warehouse.GetWarehouseLocationNameByCode(_stcTransInternalDt.LocationSrc);
            this.LocationDestTextBox.Text = _warehouse.GetWarehouseLocationNameByCode(_stcTransInternalDt.LocationDest);
            this.QtyTextBox.Text = (_stcTransInternalDt.Qty == 0) ? "0" : _stcTransInternalDt.Qty.ToString("#,##0.##");
            this.UnitTextBox.Text = _unit.GetUnitNameByCode(_stcTransInternalDt.Unit);
            this.RemarkTextBox.Text = _stcTransInternalDt.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _locationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);
            string _locationCode2 = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey2), ApplicationConfig.EncryptionKey);

            STCTransferDt _stcTransInternalDt = this._stcTransInternal.GetSingleSTCTransferDt(_transNo, _productCode, _locationCode, _locationCode2);

            _stcTransInternalDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcTransInternalDt.Remark = this.RemarkTextBox.Text;

            bool _result = this._stcTransInternal.EditSTCTransferDt(_stcTransInternalDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }
    }
}