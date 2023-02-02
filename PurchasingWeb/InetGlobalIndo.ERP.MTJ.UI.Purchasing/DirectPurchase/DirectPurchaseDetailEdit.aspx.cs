using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.DirectPurchase
{
    public partial class DirectPurchaseDetailEdit : DirectPurchaseBase
    {
        private DirectPurchaseBL _DirectPurchaseBL = new DirectPurchaseBL();
        private ProductBL _productBL = new ProductBL();
        private UnitBL _unitBL = new UnitBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private SupplierBL _supplierBL = new SupplierBL();
        private CustomerBL _customerBL = new CustomerBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();

        private byte _decimalPlace = 0;

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
                this.ShowData();
            }
        }


        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribute()
        {
            this.PriceTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountTextBox.Attributes.Add("ReadOnly", "True");

            this.QtyOrderTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.QtyOrderTextBox.ClientID + ");");
            this.QtyOrderTextBox.Attributes.Add("OnBlur", "BlurAmount(" + QtyOrderTextBox.ClientID + "," + PriceTextBox.ClientID + "," + AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PriceTextBox.Attributes.Add("OnBlur", "BlurAmount(" + QtyOrderTextBox.ClientID + "," + PriceTextBox.ClientID + "," + AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        protected void ShowData()
        {
            String _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            String _transNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            String _warehouseCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._wrhsKey), ApplicationConfig.EncryptionKey);
            String _LocationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);

            PRCTrDirectPurchaseDt _directPurchaseDt = _DirectPurchaseBL.GetSingleDirectPurchaseDt(_transNmbr, _productCode, _warehouseCode, _LocationCode);

            this.WarehouseCodeTextBox.Text = _warehouseBL.GetWarehouseNameByCode(_warehouseCode);
            char _fgSubled = _warehouseBL.GetWarehouseFgSubledByCode(_directPurchaseDt.WrhsCode);
            if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.WarehouseSubledTextBox.Text = "";
            }
            else
            {
                if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.WarehouseSubledTextBox.Text = _customerBL.GetNameByCode(_directPurchaseDt.WrhsSubLed);
                }
                else if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    this.WarehouseSubledTextBox.Text = _supplierBL.GetSuppNameByCode(_directPurchaseDt.WrhsSubLed);
                }
            }
            this.WarehouseLocationTextBox.Text = _warehouseBL.GetWarehouseLocationNameByCode(_LocationCode);
            this.TransNmbrHiddenField.Value = _directPurchaseDt.TransNmbr;
            this.ProductCodeTextBox.Text = _directPurchaseDt.ProductCode;
            this.ProductNameTextBox.Text = _productBL.GetProductNameByCode(_productCode);
            this.QtyOrderTextBox.Text = Convert.ToDecimal(_directPurchaseDt.QtyOrder).ToString("#,##0.##");
            this.UnitTextBox.Text = _directPurchaseDt.UnitOrder;
            this.RemarkTextBox.Text = _directPurchaseDt.Remark;
            this.PriceTextBox.Text = _directPurchaseDt.Price.ToString("#,##0.##");
            this.AmountTextBox.Text = _directPurchaseDt.Amount.ToString("#,##0.##");
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ProductCodeTextBox.Text != "" && this.ProductNameTextBox.Text != "" && this.QtyOrderTextBox.Text != "")
            {
                MsProductConvert _msProductConvert = _DirectPurchaseBL.GetSingleProductConvert(this.ProductCodeTextBox.Text, this.UnitTextBox.Text);

                string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
                String _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
                String _warehouseCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._wrhsKey), ApplicationConfig.EncryptionKey);
                String _LocationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);

                PRCTrDirectPurchaseDt _DirectPurchaseDt = _DirectPurchaseBL.GetSingleDirectPurchaseDt(_transNo, _productCode, _warehouseCode, _LocationCode);

                _DirectPurchaseDt.QtyOrder = Convert.ToDecimal(this.QtyOrderTextBox.Text);
                _DirectPurchaseDt.UnitOrder = this.UnitTextBox.Text;
                _DirectPurchaseDt.Qty = ((_msProductConvert == null)? Convert.ToDecimal(this.QtyOrderTextBox.Text):(Convert.ToDecimal(this.QtyOrderTextBox.Text)*_msProductConvert.Rate));
                _DirectPurchaseDt.Unit = ((_msProductConvert == null)? this.UnitTextBox.Text : _msProductConvert.UnitConvert);
                _DirectPurchaseDt.Remark = this.RemarkTextBox.Text;

                bool _result = this._DirectPurchaseBL.EditDirectPurchaseDtAndEditDirectPurchaseHd(_DirectPurchaseDt, Convert.ToDecimal(this.AmountTextBox.Text));

                if (_result == true)
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.WarningLabel.Text = "Your Failed Add Data";
                }
            }
            else
            {
                this.WarningLabel.Text = "Please Insert Product Code, Product Name & Qty";
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