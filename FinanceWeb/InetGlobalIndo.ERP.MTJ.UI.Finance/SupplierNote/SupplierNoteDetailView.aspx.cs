using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.SupplierNote
{
    public partial class SupplierNoteDetailView : SupplierNoteBase
    {
        private SupplierNoteBL _supplierNoteBL = new SupplierNoteBL();
        private ReceivingPOBL _receivingPOBL = new ReceivingPOBL();
        private UnitBL _unitBL = new UnitBL();
        private ProductBL _productBL = new ProductBL();
        private PurchaseOrderBL _poBL = new PurchaseOrderBL();
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

                this.SetButtonPermission();

                this.ShowData();
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

        public void ShowData()
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _rrCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._rrKey), ApplicationConfig.EncryptionKey);

            FINSuppInvDt _finSuppInvDt = this._supplierNoteBL.GetSingleFINSuppInvDt(_transNo, _rrCode, _productCode);
            FINSuppInvHd _finSuppInvHd = _supplierNoteBL.GetSingleFINSuppInvHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finSuppInvHd.CurrCode);
            this.RRNoTextBox.Text = _receivingPOBL.GetFileNmbrSTCReceiveHd(_finSuppInvDt.RRNo);
            this.ProductTextBox.Text = _productBL.GetProductNameByCode(_finSuppInvDt.ProductCode);
            this.PONoTextBox.Text = _poBL.GetFileNmbrPRCPOHd(_finSuppInvDt.PONo, 0);
            this.QtyTextBox.Text = (_finSuppInvDt.Qty == 0) ? "0" : _finSuppInvDt.Qty.ToString("#,###.##");
            this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_finSuppInvDt.Unit);
            this.PriceTextBox.Text = (((_finSuppInvDt.PriceForex == null) ? 0 : Convert.ToDecimal(_finSuppInvDt.PriceForex)) == 0) ? "0" : ((_finSuppInvDt.PriceForex == null) ? 0 : Convert.ToDecimal(_finSuppInvDt.PriceForex)).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountTextBox.Text = (((_finSuppInvDt.AmountForex == null) ? 0 : Convert.ToDecimal(_finSuppInvDt.AmountForex)) == 0) ? "0" : ((_finSuppInvDt.AmountForex == null) ? 0 : Convert.ToDecimal(_finSuppInvDt.AmountForex)).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _finSuppInvDt.Remark;
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._rrKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._rrKey)) + "&" + this._productKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._productKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}