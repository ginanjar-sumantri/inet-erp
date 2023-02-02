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

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.SalesOrder
{
    public partial class SalesOrderDetailView : SalesOrderBase
    {
        private SalesOrderBL _salesOrderBL = new SalesOrderBL();
        private ProductBL _productBL = new ProductBL();
        private UnitBL _unitBL = new UnitBL();
        private PermissionBL _permBL = new PermissionBL();
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

                MKTSOHd _salesOrderHd = this._salesOrderBL.GetSingleMKTSOHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisiKey), ApplicationConfig.EncryptionKey)));

                if (_salesOrderHd.Status != SalesOrderDataMapper.GetStatus(TransStatus.Posted))
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
            MKTSODt _salesOrderDt = this._salesOrderBL.GetSingleMKTSODt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisiKey), ApplicationConfig.EncryptionKey)), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey));
            MKTSOHd _salesOrderHd = this._salesOrderBL.GetSingleMKTSOHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisiKey), ApplicationConfig.EncryptionKey)));

            byte _decimalPlace = _currencyBL.GetDecimalPlace(_salesOrderHd.CurrCode);

            this.ProductTextBox.Text = _productBL.GetProductNameByCode(_salesOrderDt.ProductCode);
            this.ProductCodeTextBox.Text = _salesOrderDt.ProductCode;
            this.SpecificationTextBox.Text = _salesOrderDt.Specification;
            this.QtyOrderTextBox.Text = (_salesOrderDt.QtyOrder == 0) ? "0" : _salesOrderDt.QtyOrder.ToString("#,###.##");
            this.UnitOrderTextBox.Text = _unitBL.GetUnitNameByCode(_salesOrderDt.UnitOrder);
            decimal _price = Convert.ToDecimal((_salesOrderDt.Price == null) ? 0 : _salesOrderDt.Price);
            this.PriceTextBox.Text = (_price == 0) ? "0" : _price.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            decimal _amount = Convert.ToDecimal((_salesOrderDt.Amount == null) ? 0 : _salesOrderDt.Amount);
            this.AmountTextBox.Text = (_amount == 0) ? "0" : _amount.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            decimal _qty = Convert.ToDecimal((_salesOrderDt.Qty == null) ? 0 : _salesOrderDt.Qty);
            this.QtyTextBox.Text = _qty.ToString("#,###.##");
            this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_salesOrderDt.Unit);
            this.RemarkTextBox.Text = _salesOrderDt.Remark;
            this.StatusLabel.Text = SalesOrderDataMapper.GetStatusTextDetail(_salesOrderDt.DoneClosing);
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeRevisiKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeRevisiKey)) + "&" + this._productKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._productKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeRevisiKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeRevisiKey)));
        }
    }
}