using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Product
{
    public partial class ProductSalesPriceEdit : ProductBase
    {
        private ProductBL _productBL = new ProductBL();
        private CurrencyBL _currBL = new CurrencyBL();
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

                this.ClearLabel();
                this.ShowData();
                this.SetAttribute();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.SalesPriceTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.SalesPriceTextBox.Attributes.Add("OnBlur", "return ChangeFormat2(" + this.SalesPriceTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        public void ShowData()
        {
            Master_ProductSalesPrice _msProductSalesPrice = this._productBL.GetSingleProductSalesPrice(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._currKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.ClearLabel();

            byte _decimalPlace = _currBL.GetDecimalPlace(_msProductSalesPrice.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            this.CurrTextBox.Text = _msProductSalesPrice.CurrCode;
            this.SalesPriceTextBox.Text = _msProductSalesPrice.SalesPrice.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.FgActiveCheckBox.Checked = (_msProductSalesPrice.FgActive == 'Y') ? true : false;
            this.RemarkTextBox.Text = _msProductSalesPrice.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            Master_ProductSalesPrice _msProductSalesPrice = this._productBL.GetSingleProductSalesPrice(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._currKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msProductSalesPrice.SalesPrice = Convert.ToDecimal(this.SalesPriceTextBox.Text);
            _msProductSalesPrice.FgActive = (this.FgActiveCheckBox.Checked == true) ? 'Y' : 'N';
            _msProductSalesPrice.Remark = this.RemarkTextBox.Text;
            _msProductSalesPrice.ModifiedBy = HttpContext.Current.User.Identity.Name;
            _msProductSalesPrice.ModifiedDate = DateTime.Now;

            bool _result = this._productBL.EditProductSalesPrice(_msProductSalesPrice);

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
            this.ClearLabel();
            this.ShowData();
        }
    }
}