using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.SupplierNote
{
    public partial class SupplierNoteDetailEdit : SupplierNoteBase
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

        public void SetAttribute()
        {
            this.AmountTextBox.Attributes.Add("ReadOnly", "True");
            this.UnitTextBox.Attributes.Add("ReadOnly", "True");
            this.PriceTextBox.Attributes.Add("ReadOnly", "True");
            //this.PriceTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            //this.PriceTextBox.Attributes.Add("OnBlur", "Count(" + this.QtyTextBox.ClientID + "," + this.PriceTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.QtyTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.QtyTextBox.Attributes.Add("OnBlur", "Count(" + this.QtyTextBox.ClientID + "," + this.PriceTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowData()
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _rrCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._rrKey), ApplicationConfig.EncryptionKey);

            FINSuppInvDt _finSuppInvDt = this._supplierNoteBL.GetSingleFINSuppInvDt(_transNo, _rrCode, _productCode);
            FINSuppInvHd _finSuppInvHd = _supplierNoteBL.GetSingleFINSuppInvHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finSuppInvHd.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
 
            this.RRNoTextBox.Text = _receivingPOBL.GetFileNmbrSTCReceiveHd(_finSuppInvDt.RRNo);
            this.ProductTextBox.Text = _productBL.GetProductNameByCode(_finSuppInvDt.ProductCode);
            this.PONoTextBox.Text = _poBL.GetFileNmbrPRCPOHd(_finSuppInvDt.PONo, 0);
            this.QtyTextBox.Text = (_finSuppInvDt.Qty == 0) ? "0" : _finSuppInvDt.Qty.ToString("#,##0");
            this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_finSuppInvDt.Unit);
            this.PriceTextBox.Text = (((_finSuppInvDt.PriceForex == null) ? 0 : Convert.ToDecimal(_finSuppInvDt.PriceForex)) == 0) ? "0" : ((_finSuppInvDt.PriceForex == null) ? 0 : Convert.ToDecimal(_finSuppInvDt.PriceForex)).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountTextBox.Text = (((_finSuppInvDt.AmountForex == null) ? 0 : Convert.ToDecimal(_finSuppInvDt.AmountForex)) == 0) ? "0" : ((_finSuppInvDt.AmountForex == null) ? 0 : Convert.ToDecimal(_finSuppInvDt.AmountForex)).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _finSuppInvDt.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _rrCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._rrKey), ApplicationConfig.EncryptionKey);

            FINSuppInvDt _finSuppInvDt = this._supplierNoteBL.GetSingleFINSuppInvDt(_transNo, _rrCode, _productCode);

            _finSuppInvDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _finSuppInvDt.AmountForex = Convert.ToDecimal(this.AmountTextBox.Text);
            _finSuppInvDt.Remark = this.RemarkTextBox.Text;

            bool _result = this._supplierNoteBL.EditFINSuppInvDt(_finSuppInvDt);

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
            this.ShowData();
        }
    }
}