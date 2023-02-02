using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockAdjustment
{
    public partial class StockAdjustmentDetailView : StockAdjustmentBase
    {
        private StockAdjustmentBL _stockAdjustBL = new StockAdjustmentBL();
        private StockOpnameBL _stockOpnameBL = new StockOpnameBL();
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

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                STCAdjustHd _stcAdjustHd = this._stockAdjustBL.GetSingleSTCAdjustHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                if (_stcAdjustHd.Status != StockAdjustmentDataMapper.GetStatus(TransStatus.Posted))
                {
                    this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                }
                else
                {
                    this.EditButton.Visible = false;
                }
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowData();
                this.SetButtonPermission();
            }
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        private void ShowData()
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _locationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);

            STCAdjustHd _stcAdjustHd = this._stockAdjustBL.GetSingleSTCAdjustHd(_transNo);
            STCAdjustDt _stcAdjustDt = this._stockAdjustBL.GetSingleSTCAdjustDt(_transNo, _productCode, _locationCode);
            MsProduct _msProduct = this._product.GetSingleProduct(_stcAdjustDt.ProductCode);

            this.ProductTextBox.Text = _product.GetProductNameByCode(_productCode);
            this.LocationTextBox.Text = _warehouse.GetWarehouseLocationNameByCode(_locationCode);
            if (_stcAdjustHd.OpnameNo != "null")
            {
                this.QtyTextBox.Text = (_stockOpnameBL.GetQtyOpnameSTCOpnameDt(_stcAdjustHd.OpnameNo, _productCode, _locationCode) == 0) ? "0" : _stockOpnameBL.GetQtyOpnameSTCOpnameDt(_stcAdjustHd.OpnameNo, _productCode, _locationCode).ToString("#,##0.##");
                if (_stockOpnameBL.GetQtyOpnameSTCOpnameDt(_stcAdjustHd.OpnameNo, _productCode, _locationCode) > 0)
                {
                    this.FgAdjustLabel.Text = "+";
                }
                else
                {
                    this.FgAdjustLabel.Text = "-";
                }
                this.UnitTextBox.Text = _unit.GetUnitNameByCode(_stockOpnameBL.GetUnitSTCOpnameDt(_stcAdjustHd.OpnameNo));
            }
            else
            {
                this.QtyTextBox.Text = (_stcAdjustDt.Qty == 0) ? "0" : _stcAdjustDt.Qty.ToString("#,##0.##");
                this.UnitTextBox.Text = _unit.GetUnitNameByCode(_msProduct.Unit);
                this.FgAdjustLabel.Text = _stcAdjustDt.FgAdjust.ToString();
            }
            if (this.FgAdjustLabel.Text == "+")
            {
                this.DetailUpdatePanel.Visible = true;
                this.PriceTextBox.Text = _stcAdjustDt.PriceCost.ToString();
            }
            else
            {
                this.DetailUpdatePanel.Visible = false;
            }
            this.RemarkTextBox.Text = _stcAdjustDt.Remark;
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._productKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._productKey)) + "&" + this._locationKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._locationKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}