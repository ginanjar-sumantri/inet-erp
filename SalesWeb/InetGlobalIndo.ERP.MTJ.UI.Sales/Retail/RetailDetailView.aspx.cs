using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.NCC;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.Retail
{
    public partial class RetailDetailView : RetailBase
    {
        private RetailBL _retailBL = new RetailBL();
        private ProductBL _productBL = new ProductBL();
        private PhoneTypeBL _phoneTypeBL = new PhoneTypeBL();
        private PermissionBL _permBL = new PermissionBL();
        private NCPBL _ncpBL = new NCPBL();
        private CurrencyBL _currBL = new CurrencyBL();

        private byte _decimalPlace = 0;

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

                SAL_TrRetail _sal_TrRetail = this._retailBL.GetSingleSAL_TrRetail(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                if (_sal_TrRetail.Status != SalesOrderDataMapper.GetStatusByte(TransStatus.Approved))
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

        private void ShowData()
        {
            SAL_TrRetailItem _sal_TrRetailItem = this._retailBL.GetSingleSAL_TrRetailItem(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeDetail), ApplicationConfig.EncryptionKey)));
            String _currCode = this._retailBL.GetSingleSAL_TrRetail(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).CurrCode;

            _decimalPlace = _currBL.GetDecimalPlace(_currCode);

            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.ProductTextBox.Text = _productBL.GetProductNameByCode(_sal_TrRetailItem.ProductCode);
            String _productType = _productBL.GetSingleProduct(_sal_TrRetailItem.ProductCode).ProductType;
            Boolean _isUsingUniqueId = _productBL.GetSingleProductType(_productType).IsUsingUniqueID;
            if (_isUsingUniqueId == true)
            {
                this.SerialNmbrTR.Visible = true;
                this.PhoneTypeTR.Visible = true;
                this.PhoneTypeTextBox.Text = _phoneTypeBL.GetPhoneTypeDescByCode(_sal_TrRetailItem.PhoneTypeCode);
                this.SerialNumberTextBox.Text = _sal_TrRetailItem.SerialNumber;
            }
            else
            {
                this.SerialNmbrTR.Visible = false;
                this.PhoneTypeTR.Visible = false;
            }
            this.IMEITextBox.Text = _sal_TrRetailItem.IMEI;
            this.PriceTextBox.Text = _sal_TrRetailItem.Price.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.QtyTextBox.Text = _sal_TrRetailItem.Qty.ToString();
            this.DiscTextBox.Text = _sal_TrRetailItem.DiscountAmount.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountTextBox.Text = _sal_TrRetailItem.Total.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _sal_TrRetailItem.Remark;
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeDetail + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeDetail)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}