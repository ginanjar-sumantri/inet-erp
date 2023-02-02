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
    public partial class PromoProductEdit : PromoBase
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
                this.MinValueTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.MinValueTextBox.ClientID + ")");
                this.MaxQtyTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.MaxQtyTextBox.ClientID + ")");
                this.AmountTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.AmountTextBox.ClientID + ")");
                this.FreeQtyTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.FreeQtyTextBox.ClientID + ")");

                this.ClearLabel();
                this.ShowData();
                this.PromoTypeRBL_SelectedIndexChanged(null, null);
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            POSMsPromoProduct _posMsPromoProduct = this._promoBL.GetSinglePOSMsPromoProduct(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

            this.PromoCodeTextBox.Text = _posMsPromoProduct.PromoCode;
            this.ProductCodeTextBox.Text = _posMsPromoProduct.ProductCode;
            this.ProductNameTextBox.Text = this._productBL.GetProductNameByCode(_posMsPromoProduct.ProductCode);
            this.FgMinTypeRBL.SelectedValue = _posMsPromoProduct.FgMin.ToString();
            this.FgMinTypeRBL_SelectedIndexChanged(null, null);
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
                this.FreeProductCodeTextBox_TextChanged(null, null);
                this.FreeQtyTextBox.Text = Convert.ToDecimal(_posMsPromoProduct.FreeQty).ToString("#,#");
                this.FgMultipleCheckBox.Checked = (_posMsPromoProduct.FgMultiple == true) ? true : false;
            }
            this.FgActiveCheckBox.Checked = (_posMsPromoProduct.FgActive == 'Y') ? true : false;
            this.RemarkTextBox.Text = _posMsPromoProduct.Remark;
        }

        protected void CheckValidData()
        {
            if (this.MinValueTextBox.Text == "")
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Fill Your Minimum Value. ";
            if (this.MaxQtyTextBox.Text == "")
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Fill Your Maximum Qty. ";
            else
                if (Convert.ToDecimal(this.MaxQtyTextBox.Text) == 0)
                    this.WarningLabel.Text = this.WarningLabel.Text + " " + "Maximum Qty Must More Then 0. ";

            if (this.PromoTypeRBL.SelectedValue == "P")
            {
                if (this.FreeProductNameTextBox.Text == "")
                    this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Check Your Free Product Code. ";
                if (this.FreeQtyTextBox.Text == "")
                    this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Fill Your Free Qty. ";
                else
                    if (Convert.ToDecimal(this.FreeQtyTextBox.Text) == 0)
                        this.WarningLabel.Text = this.WarningLabel.Text + " " + "Free Qty Must More Then 0. ";
            }
            else if (this.PromoTypeRBL.SelectedValue == "D")
            {
                if (this.AmountTextBox.Text == "")
                    this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Fill Your Amount. ";
                else
                    if (Convert.ToDecimal(this.AmountTextBox.Text) == 0)
                        this.WarningLabel.Text = this.WarningLabel.Text + " " + "Amount Must More Then 0. ";
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                POSMsPromoProduct _posMsPromoProduct = this._promoBL.GetSinglePOSMsPromoProduct(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

                _posMsPromoProduct.FgMin = Convert.ToChar(this.FgMinTypeRBL.SelectedValue);
                _posMsPromoProduct.MinValue = Convert.ToDecimal(this.MinValueTextBox.Text);
                _posMsPromoProduct.MaksimalQty = Convert.ToDecimal(this.MaxQtyTextBox.Text);
                _posMsPromoProduct.FgPromoType = Convert.ToChar(this.PromoTypeRBL.SelectedValue);
                if (this.PromoTypeRBL.SelectedValue == "D")
                {
                    _posMsPromoProduct.AmountType = this.AmountTypeRBL.SelectedValue.ToString();
                    _posMsPromoProduct.Amount = Convert.ToDecimal(this.AmountTextBox.Text);
                    _posMsPromoProduct.FreeProductCode = null;
                    _posMsPromoProduct.FreeQty = null;
                    _posMsPromoProduct.FgMultiple = null;
                }
                else if (this.PromoTypeRBL.SelectedValue == "P")
                {
                    _posMsPromoProduct.AmountType = null;
                    _posMsPromoProduct.Amount = null;
                    _posMsPromoProduct.FreeProductCode = this.FreeProductCodeTextBox.Text;
                    _posMsPromoProduct.FreeQty = Convert.ToDecimal(this.FreeQtyTextBox.Text);
                    _posMsPromoProduct.FgMultiple = (this.FgMultipleCheckBox.Checked == true) ? true : false;
                }
                _posMsPromoProduct.FgActive = (this.FgActiveCheckBox.Checked == true) ? 'Y' : 'N';
                _posMsPromoProduct.Remark = this.RemarkTextBox.Text;
                _posMsPromoProduct.ModifiedBy = HttpContext.Current.User.Identity.Name;
                _posMsPromoProduct.ModifiedDate = DateTime.Now;

                bool _result = this._promoBL.EditPOSMsPromoProduct(_posMsPromoProduct);

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

        protected void PromoTypeRBL_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.AmountTypeRBL.Enabled = false;
            this.AmountTextBox.Enabled = false;
            this.FreeProductCodeTextBox.Enabled = false;
            this.FreeProductNameTextBox.Enabled = false;
            this.FreeQtyTextBox.Enabled = false;
            this.FgMultipleCheckBox.Enabled = false;

            if (this.PromoTypeRBL.SelectedValue == "P")
            {
                this.FreeProductCodeTextBox.Enabled = true;
                this.FreeProductNameTextBox.Enabled = true;
                this.FreeQtyTextBox.Enabled = true;
                this.FgMultipleCheckBox.Enabled = true;
            }
            else if (this.PromoTypeRBL.SelectedValue == "D")
            {
                bool _fgMember = (this._promoBL.GetSingle(this.PromoCodeTextBox.Text).FgMember == false) ? false : true;
                if (_fgMember == false)
                {
                    this.AmountTypeRBL.Enabled = true;
                    this.AmountTextBox.Enabled = true;
                }
            }
        }

        protected void FreeProductCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.FreeProductCodeTextBox.Text != "")
            {
                this.FreeProductNameTextBox.Text = this._productBL.GetProductNameByCode(this.FreeProductCodeTextBox.Text.Trim());
            }
        }

        protected void FgMinTypeRBL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.FgMinTypeRBL.SelectedValue == "Q")
                this.MinimumValueLiteral.Text = "Minimum Qty";
            else
                this.MinimumValueLiteral.Text = "Minimum Payment";
        }
}
}