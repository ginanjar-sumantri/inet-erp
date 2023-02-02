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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.RequestSalesRetur
{
    public partial class RequestSalesReturDetailView : RequestSalesReturBase
    {
        private RequestSalesReturBL _requestSalesReturBL = new RequestSalesReturBL();
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

                MKTReqReturHd _mktReqReturHd = this._requestSalesReturBL.GetSingleMKTReqReturHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                this.ProductScrapTextBox.Attributes.Add("ReadOnly", "True");
                if (_mktReqReturHd.Status != RequestSalesReturDataMapper.GetStatus(TransStatus.Posted))
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
            MKTReqReturDt _mktReqReturDt = this._requestSalesReturBL.GetSingleMKTReqReturDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey));
            MKTReqReturHd _mktReqReturHd = this._requestSalesReturBL.GetSingleMKTReqReturHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = _currencyBL.GetDecimalPlace(_mktReqReturHd.CurrCode);
            this.ProductTextBox.Text = _productBL.GetProductNameByCode(_mktReqReturDt.ProductCode);
            this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_mktReqReturDt.Unit);
            this.ProductScrapTextBox.Text = ((_mktReqReturDt.ProductScrap.ToString() == "Y") ? "Yes" : "No");
            this.QtyTextBox.Text = (_mktReqReturDt.Qty == 0) ? "0" : _mktReqReturDt.Qty.ToString("#,###.##");
            this.PriceTextBox.Text = (_mktReqReturDt.PriceForex == 0) ? "0" : _mktReqReturDt.PriceForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountTextBox.Text = (_mktReqReturDt.AmountForex == 0) ? "0" : _mktReqReturDt.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _mktReqReturDt.Remark;
            //this.StatusLabel.Text = RequestSalesReturDataMapper.GetStatusTextDetail((_mktReqReturDt.DoneClosing == null) ? ' ' : Convert.ToChar(_mktReqReturDt.DoneClosing));
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._productKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._productKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}