using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.DirectSales
{
    public partial class DirectSalesDetailEdit : DirectSalesBase
    {
        private DirectSalesBL _directSalesBL = new DirectSalesBL();
        private ProductBL _productBL = new ProductBL();
        private UnitBL _unitBL = new UnitBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private SupplierBL _supplierBL = new SupplierBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private CustomerBL _customerBL = new CustomerBL();
        private DirectPurchaseBL _directPurchaseBL = new DirectPurchaseBL();

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
            this.AmountTextBox.Attributes.Add("ReadOnly", "True");

            this.QtyOrderTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.QtyOrderTextBox.Attributes.Add("OnBlur", "BlurAmount(" + QtyOrderTextBox.ClientID + "," + PriceTextBox.ClientID + "," + AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PriceTextBox.Attributes.Add("OnBlur", "BlurAmount(" + QtyOrderTextBox.ClientID + "," + PriceTextBox.ClientID + "," + AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        protected void ShowData()
        {
            String _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            String _transNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            String _warehouseCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._Warehouse), ApplicationConfig.EncryptionKey);
            String _LocationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._Location), ApplicationConfig.EncryptionKey);

            SALTrDirectSalesDt _directSalesDt = _directSalesBL.GetSingleDirectSalesDt(_transNmbr, _productCode, _warehouseCode, _LocationCode);
            MsProduct _msProduct = _productBL.GetSingleProduct(_productCode);

            MsWarehouse _msWarehouse = _warehouseBL.GetSingle(_directSalesDt.WrhsCode);
            MsCustomer _msCustomer = _customerBL.GetSingleCust(_directSalesDt.WrhsSubLed);
            MsWrhsLocation _msWrhsLocation = _warehouseBL.GetSingleWrhsLocation(_directSalesDt.WLocationCode);

            char _fgSubled = _warehouseBL.GetWarehouseFgSubledByCode(_directSalesDt.WrhsCode);
            if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.WarehouseSubledTextBox.Text = "";
            }
            else
            {
                if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.WarehouseSubledTextBox.Text = _customerBL.GetNameByCode(_directSalesDt.WrhsSubLed);
                }
                else if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    this.WarehouseSubledTextBox.Text = _supplierBL.GetSuppNameByCode(_directSalesDt.WrhsSubLed);
                }
            }

            this.WarehouseCodeTextBox.Text = _msWarehouse.WrhsName;
            this.WarehouseLocationTextBox.Text = _msWrhsLocation.WLocationName;
            this.TransNmbrHiddenField.Value = _directSalesDt.TransNmbr;
            this.ProductCodeTextBox.Text = _directSalesDt.ProductCode;
            this.ProductNameTextBox.Text = _msProduct.ProductName;
            this.QtyOrderTextBox.Text = Convert.ToDecimal(_directSalesDt.QtyOrder).ToString("0");
            this.UnitOrderTextBox.Text = _directSalesDt.UnitOrder;
            this.PriceTextBox.Text = _directSalesDt.Price.ToString("#,##0.00");
            this.AmountTextBox.Text = _directSalesDt.Amount.ToString("#,##0.00");
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ProductCodeTextBox.Text != "" && this.ProductNameTextBox.Text != "" && this.QtyOrderTextBox.Text != "")
            {
                //String _unit = _productBL.GetSingleProduct(this.ProductCodeTextBox.Text).Unit;
                MsProductConvert _msProductConvert = _directPurchaseBL.GetSingleProductConvert(this.ProductCodeTextBox.Text, this.UnitOrderTextBox.Text);

                string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
                String _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
                String _warehouseCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._Warehouse), ApplicationConfig.EncryptionKey);
                String _LocationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._Location), ApplicationConfig.EncryptionKey);

                SALTrDirectSalesDt _directSalesDt = _directSalesBL.GetSingleDirectSalesDt(_transNo, _productCode, _warehouseCode, _LocationCode);

                //MsProduct _msProduct = this._productBL.GetSingleProduct(this.ProductCodeTextBox.Text);

                _directSalesDt.QtyOrder = Convert.ToDecimal(this.QtyOrderTextBox.Text);
                _directSalesDt.UnitOrder = this.UnitOrderTextBox.Text;
                _directSalesDt.Qty = ((_msProductConvert == null)? Convert.ToDecimal(this.QtyOrderTextBox.Text):(_msProductConvert.Rate * Convert.ToDecimal(this.QtyOrderTextBox.Text)));
                _directSalesDt.Unit = ((_msProductConvert == null) ? this.UnitOrderTextBox.Text : _msProductConvert.UnitConvert);
                _directSalesDt.Price = Convert.ToDecimal(this.PriceTextBox.Text);

                bool _result = this._directSalesBL.EditDirectSalesDtAndEditDirectSalesHd(_directSalesDt, Convert.ToDecimal(this.AmountTextBox.Text));

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