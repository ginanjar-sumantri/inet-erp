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
    public partial class ProductAlternatifEdit : ProductBase
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

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.ProductCodeTextBox.Attributes.Add("ReadOnly", "True");
                this.AlternatifCodeTextBox.Attributes.Add("ReadOnly", "True");

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                //this.ShowCurr();

                this.ClearLabel();
                this.ClearData();
                this.ShowData();
                this.SetAttribute();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ShowData()
        {
            this.ProductCodeTextBox.Text = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            this.AlternatifCodeTextBox.Text = _productBL.GetProductName(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKeyAlternatif), ApplicationConfig.EncryptionKey));
        }

        protected void SetAttribute()
        {
            //this.SalesPriceTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            //this.SalesPriceTextBox.Attributes.Add("OnBlur", "return ChangeFormat2(" + this.SalesPriceTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        public void ClearData()
        {
            this.ClearLabel();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            String _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            String _alternatifCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKeyAlternatif), ApplicationConfig.EncryptionKey);

            MsProduct_Alternatif _msProduct_Alternatif = _productBL.GetSingleProductAlternatif(_productCode, _alternatifCode);

            //_msProduct_Alternatif.ProductCode = _productCode;
            //_msProduct_Alternatif.AlternatifCode = _alternatifCode;
            _msProduct_Alternatif.Remark = this.RemarkTextBox.Text;
            _msProduct_Alternatif.EditBy = HttpContext.Current.User.Identity.Name;
            _msProduct_Alternatif.EditDate = DateTime.Now;

            bool _result = this._productBL.EditProductAlternatif(_msProduct_Alternatif);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }
    }
}