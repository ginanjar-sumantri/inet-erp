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

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockBeginning
{
    public partial class StockBeginningDetailEdit : StockBeginningBase
    {
        private StockBeginningBL _stockBeginBL = new StockBeginningBL();
        private ProductBL _productBL = new ProductBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private UnitBL _unitBL = new UnitBL();
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

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.TotalTextBox.Attributes.Add("ReadOnly", "True");
            this.QtyTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.QtyTextBox.Attributes.Add("OnBlur", "Adjust(" + QtyTextBox.ClientID + ", " + PriceTextBox.ClientID + ", " + TotalTextBox.ClientID + ");");
            this.PriceTextBox.Attributes.Add("OnBlur", "Calculate(" + QtyTextBox.ClientID + ", " + PriceTextBox.ClientID + ", " + TotalTextBox.ClientID + ");");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ShowData()
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _locationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);

            STCAdjustHd _stcAdjustHd = this._stockBeginBL.GetSingleSTCAdjustHd(_transNo);
            STCAdjustDt _stcAdjustDt = this._stockBeginBL.GetSingleSTCAdjustDt(_transNo, _productCode, _locationCode);
            MsProduct _msProduct = this._productBL.GetSingleProduct(_stcAdjustDt.ProductCode);

            this.ProductTextBox.Text = _productBL.GetProductNameByCode(_productCode);
            this.LocationTextBox.Text = _warehouseBL.GetWarehouseLocationNameByCode(_locationCode);
            this.QtyTextBox.Text = (_stcAdjustDt.Qty == 0) ? "0" : _stcAdjustDt.Qty.ToString("#,##0.##");
            decimal _price = (_stcAdjustDt.PriceCost == null) ? 0 : Convert.ToDecimal(_stcAdjustDt.PriceCost);
            this.PriceTextBox.Text = (_price == 0) ? "0" : _price.ToString("#,##0.##");
            decimal _total = (_stcAdjustDt.TotalCost == null) ? 0 : Convert.ToDecimal(_stcAdjustDt.TotalCost);
            this.TotalTextBox.Text = (_total == 0) ? "0" : _total.ToString("#,##0.##");
            this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_msProduct.Unit);
            this.RemarkTextBox.Text = _stcAdjustDt.Remark;
            //this.FgAdjustLabel.Text = _stcAdjustDt.FgAdjust.ToString();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _locationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);

            STCAdjustDt _stcAdjustDt = this._stockBeginBL.GetSingleSTCAdjustDt(_transNo, _productCode, _locationCode);

            _stcAdjustDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcAdjustDt.TotalCost = Convert.ToDecimal(this.TotalTextBox.Text);
            _stcAdjustDt.PriceCost = Convert.ToDecimal(this.PriceTextBox.Text);
            _stcAdjustDt.Remark = this.RemarkTextBox.Text;

            bool _result = this._stockBeginBL.EditSTCAdjustDt(_stcAdjustDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}