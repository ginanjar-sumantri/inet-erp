using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.SupplierNote
{
    public partial class SupplierNoteDetail2View : SupplierNoteBase
    {
        private SupplierNoteBL _supplierNoteBL = new SupplierNoteBL();
        private UnitBL _unitBL = new UnitBL();
        private ProductBL _productBL = new ProductBL();
        private PermissionBL _permBL = new PermissionBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private CustomerBL _customerBL = new CustomerBL();
        private SupplierBL _supplierBL = new SupplierBL();
        private AccountBL _accountBL = new AccountBL();
        private CurrencyBL _currencyBL = new CurrencyBL();

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

                FINSuppInvHd _finSuppInvHd = this._supplierNoteBL.GetSingleFINSuppInvHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                if (_finSuppInvHd.Status != SupplierNoteDataMapper.GetStatus(TransStatus.Posted))
                {
                    this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                }
                else
                {
                    this.EditButton.Visible = false;
                }
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowData();
            }
        }

        public void ShowData()
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _wrhsCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._wrhsKey), ApplicationConfig.EncryptionKey);
            string _wrhsSubled = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._subledKey), ApplicationConfig.EncryptionKey);
            string _wrhsLoc = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);

            FINSuppInvDt2 _finSuppInvDt2 = this._supplierNoteBL.GetSingleFINSuppInvDt2(_transNo, _wrhsCode, _wrhsSubled, _wrhsLoc, _productCode);
            FINSuppInvHd _finSuppInvHd = _supplierNoteBL.GetSingleFINSuppInvHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finSuppInvHd.CurrCode);

            char _fgSubled = _warehouseBL.GetWarehouseFgSubledByCode(_finSuppInvDt2.WrhsCode);

            if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            {
                this.WarehouseSubledTextBox.Text = _customerBL.GetNameByCode(_finSuppInvDt2.WrhsSubLed);
            }
            else if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
            {
                this.WarehouseSubledTextBox.Text = _supplierBL.GetSuppNameByCode(_finSuppInvDt2.WrhsSubLed);
            }
            else if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.WarehouseSubledTextBox.Text = "";
            }
            else
            {
                this.WarehouseSubledTextBox.Text = "";
            }
            this.WarehouseTextBox.Text = _warehouseBL.GetWarehouseNameByCode(_finSuppInvDt2.WrhsCode);
            this.LocationTextBox.Text = _warehouseBL.GetWarehouseLocationNameByCode(_finSuppInvDt2.LocationCode);
            this.ProductTextBox.Text = _productBL.GetProductNameByCode(_finSuppInvDt2.ProductCode);
            this.QtyTextBox.Text = (_finSuppInvDt2.Qty == 0) ? "0" : _finSuppInvDt2.Qty.ToString("#,###.##");
            this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_finSuppInvDt2.Unit);
            this.PriceTextBox.Text = (_finSuppInvDt2.PriceForex == 0 || _finSuppInvDt2.PriceForex == null) ? "0" : Convert.ToDecimal(_finSuppInvDt2.PriceForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountTextBox.Text = (_finSuppInvDt2.PriceForex == 0 || _finSuppInvDt2.AmountForex == null) ? "0" : Convert.ToDecimal(_finSuppInvDt2.AmountForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _finSuppInvDt2.Remark;
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage2 + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._productKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._productKey)) + "&" + this._wrhsKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._wrhsKey)) + "&" + this._subledKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._subledKey)) + "&" + this._locationKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._locationKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}