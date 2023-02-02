using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.Promo
{
    public partial class PromoFreeProductEdit : PromoBase
    {
        private PromoBL _promoBL = new PromoBL();
        private PermissionBL _permBL = new PermissionBL();
        private MemberTypeBL _memberTypeBL = new MemberTypeBL();
        private ProductBL _productBL = new ProductBL();

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
                this.PageTitleLiteral.Text = this._pageTitleDetail2Literal;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.FreeQtyTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.FreeQtyTextBox.ClientID + ")");

                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            POSMsPromoFreeProduct _posMsPromoFreeProduct = this._promoBL.GetSinglePOSMsPromoFreeProduct(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));
            this.PromoCodeTextBox.Text = _posMsPromoFreeProduct.PromoCode;
            this.FreeProductCodeTextBox.Text = _posMsPromoFreeProduct.FreeProductCode;
            this.FreeProductNameTextBox.Text = this._productBL.GetProductNameByCode(_posMsPromoFreeProduct.FreeProductCode);
            this.FreeQtyTextBox.Text = Convert.ToDecimal(_posMsPromoFreeProduct.FreeQty).ToString("#,#");
            this.FgActiveCheckBox.Checked = (_posMsPromoFreeProduct.FgActive == 'Y') ? true : false;
            this.RemarkTextBox.Text = _posMsPromoFreeProduct.Remark;
        }

        protected void CheckValidData()
        {
            if (this.FreeQtyTextBox.Text == "")
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Fill Your Free Qty. ";
            else
                if (Convert.ToDecimal(this.FreeQtyTextBox.Text) == 0)
                    this.WarningLabel.Text = this.WarningLabel.Text + " " + "Free Qty Must More Then 0. ";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                POSMsPromoFreeProduct _posMsPromoFreeProduct = this._promoBL.GetSinglePOSMsPromoFreeProduct(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

                _posMsPromoFreeProduct.FreeQty = Convert.ToDecimal(this.FreeQtyTextBox.Text);
                _posMsPromoFreeProduct.FgActive = (this.FgActiveCheckBox.Checked == true) ? 'Y' : 'N';
                _posMsPromoFreeProduct.Remark = this.RemarkTextBox.Text;
                _posMsPromoFreeProduct.ModifiedBy = HttpContext.Current.User.Identity.Name;
                _posMsPromoFreeProduct.ModifiedDate = DateTime.Now;

                bool _result = this._promoBL.EditPOSMsPromoFreeProduct(_posMsPromoFreeProduct);

                if (_result == true)
                {
                    Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "You Failed Edit Data";
                }
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}