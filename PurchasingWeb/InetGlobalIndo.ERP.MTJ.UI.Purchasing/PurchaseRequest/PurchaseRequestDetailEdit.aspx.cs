using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseRequest
{
    public partial class PurchaseRequestDetailEdit : PurchaseRequestBase
    {
        private NameValueCollectionExtractor _nvcExtractor;
        private PurchaseRequestBL _purchaseRequestBL = new PurchaseRequestBL();
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
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.ClearLabel();
                this.ShowData();
                this.SetAttribute();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void SetAttribute()
        {
            this.QtyTextBox.Attributes.Add("OnKeyDown", "return Numeric();");
            this.UnitTextBox.Attributes.Add("ReadOnly", "true");
            this.DateTextBox.Attributes.Add("ReadOnly", "true");
            this.EstPriceTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.EstPriceTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowData()
        {
            PRCRequestHd _prcRequestHd = this._purchaseRequestBL.GetSinglePRCRequestHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            PRCRequestDt _prcRequestDt = this._purchaseRequestBL.GetSinglePRCRequestDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItem), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_prcRequestHd.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            this.ProductTextBox.Text = _prcRequestDt.ProductCode + " - " + this._productBL.GetProductNameByCode(_prcRequestDt.ProductCode);
            this.SpecificationTextBox.Text = _prcRequestDt.Specification;
            this.QtyTextBox.Text = (_prcRequestDt.Qty == 0) ? "0" : _prcRequestDt.Qty.ToString("###");
            bool _isUsingPG = this._productBL.GetSingleProductType(this._productBL.GetSingleProduct(_prcRequestDt.ProductCode).ProductType).IsUsingPG;
            if (_isUsingPG == true)
            {
                this.EstPriceTextBox.Attributes.Add("ReadOnly", "True");
                this.EstPriceTextBox.Attributes.Add("style", "background-color:#cccccc");
            }
            else
            {
                this.EstPriceTextBox.Attributes.Remove("ReadOnly");
                this.EstPriceTextBox.Attributes.Add("style", "background-color:#FFFFFF");
            }
            this.EstPriceTextBox.Text = (_prcRequestDt.EstPrice == 0) ? "0" : _prcRequestDt.EstPrice.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.UnitTextBox.Text = this._unitBL.GetUnitNameByCode(_prcRequestDt.Unit);
            this.DateTextBox.Text = DateFormMapper.GetValue(_prcRequestDt.RequireDate);
            this.RemarkTextBox.Text = _prcRequestDt.Remark;

        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            PRCRequestDt _prcRequestDt = this._purchaseRequestBL.GetSinglePRCRequestDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItem), ApplicationConfig.EncryptionKey));

            _prcRequestDt.Specification = this.SpecificationTextBox.Text;
            _prcRequestDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _prcRequestDt.EstPrice = Convert.ToDecimal(this.EstPriceTextBox.Text);
            _prcRequestDt.RequireDate = DateFormMapper.GetValue(this.DateTextBox.Text);
            _prcRequestDt.Remark = this.RemarkTextBox.Text;

            _prcRequestDt.EditBy = HttpContext.Current.User.Identity.Name;
            _prcRequestDt.EditDate = DateTime.Now;

            bool _result = this._purchaseRequestBL.EditPRCRequestDt(_prcRequestDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
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