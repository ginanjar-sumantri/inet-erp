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
    public partial class PromoProductView : PromoBase
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
                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
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
            POSMsPromoProduct _posMsPromoProduct = this._promoBL.GetSinglePOSMsPromoProduct(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

            this.PromoCodeTextBox.Text = _posMsPromoProduct.PromoCode;
            this.ProductCodeTextBox.Text = _posMsPromoProduct.ProductCode;
            this.ProductNameTextBox.Text = this._productBL.GetProductNameByCode(_posMsPromoProduct.ProductCode);
            this.FgMinTypeRBL.SelectedValue = _posMsPromoProduct.FgMin.ToString();
            this.MinValueTextBox.Text = Convert.ToDecimal(_posMsPromoProduct.MinValue).ToString("#,#");
            this.MaxQtyTextBox.Text = Convert.ToDecimal(_posMsPromoProduct.MaksimalQty).ToString("#,#");
            this.PromoTypeRBL.SelectedValue = _posMsPromoProduct.FgPromoType.ToString();
            if (_posMsPromoProduct.FgPromoType == 'D')
            {
                this.AmountTypeRBL.SelectedValue = _posMsPromoProduct.AmountType;
                this.AmountTextBox.Text = Convert.ToDecimal(_posMsPromoProduct.Amount).ToString("#,#");
            }
            else if (_posMsPromoProduct.FgPromoType == 'P')
            {
                this.FreeProductCodeTextBox.Text = _posMsPromoProduct.FreeProductCode;
                this.FreeProductNameTextBox.Text = this._productBL.GetProductNameByCode(_posMsPromoProduct.FreeProductCode);
                this.FreeQtyTextBox.Text = Convert.ToDecimal(_posMsPromoProduct.FreeQty).ToString("#,#");
                this.FgMultipleCheckBox.Checked = (_posMsPromoProduct.FgMultiple == true) ? true : false;
            }
            this.FgActiveCheckBox.Checked = (_posMsPromoProduct.FgActive == 'Y') ? true : false;
            this.RemarkTextBox.Text = _posMsPromoProduct.Remark;
            
            String _status = this._promoBL.GetSingle(this.PromoCodeTextBox.Text).Status.ToString();
            if (_status.Trim().ToLower() == POSPromoDataMapper.GetStatus(POSPromoStatus.Posting).ToString().ToLower())
            {
                this.EditButton.Visible = false;
            }
            else
            {
                this.EditButton.Visible = true;
            }
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage2 + "?" + this._codeItemKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ProductCodeTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}