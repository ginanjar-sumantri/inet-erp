using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.Promo
{
    public partial class PromoProductAdd : PromoBase
    {
        private PromoBL _promoBL = new PromoBL();
        private PermissionBL _permBL = new PermissionBL();
        private ProductBL _productBL = new ProductBL();

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
                this.PageTitleLiteral.Text = this._pageTitleDetail2Literal;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ProductNameTextBox.Attributes.Add("ReadOnly", "True");
                this.FreeProductNameTextBox.Attributes.Add("ReadOnly", "True");
                this.MinValueTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.MinValueTextBox.ClientID + ")");
                this.MaxQtyTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.MaxQtyTextBox.ClientID + ")");
                this.AmountTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.AmountTextBox.ClientID + ")");
                this.FreeQtyTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.FreeQtyTextBox.ClientID + ")");

                this.btnSearchProduct.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findProduct&configCode=product','_popSearch','maximize=1,toolbar=0,location=0,status=0,scrollbars=1')";
                this.btnSearchProduct2.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findFreeProduct&configCode=product','_popSearch','maximize=1,toolbar=0,location=0,status=0,scrollbars=1')";
                this.ProductCodeTextBox.Attributes.Add("onblur", "findProduct");

                String spawnJS = "<script language='JavaScript'>\n";

                spawnJS += "function findProduct(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.ProductCodeTextBox.ClientID + "').value = dataArray[0];\n";
                spawnJS += "document.getElementById('" + this.ProductNameTextBox.ClientID + "').value = dataArray[1];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                spawnJS += "function findFreeProduct(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.FreeProductCodeTextBox.ClientID + "').value = dataArray[0];\n";
                spawnJS += "document.getElementById('" + this.FreeProductNameTextBox.ClientID + "').value = dataArray[1];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                spawnJS += "</script>\n";
                this.javascriptReceiver.Text = spawnJS;

                this.ClearData();
                this.PromoTypeRBL_SelectedIndexChanged(null, null);
                this.FgMinTypeRBL_SelectedIndexChanged(null, null);
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            this.ClearLabel();

            String _code = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            this.PromoCodeTextBox.Text = _code;
            this.ProductCodeTextBox.Text = "";
            this.ProductNameTextBox.Text = "";
            this.FgMinTypeRBL.SelectedIndex = 0;
            this.MinValueTextBox.Text = "0";
            this.MaxQtyTextBox.Text = "0";
            this.PromoTypeRBL.SelectedIndex = 0;
            this.AmountTypeRBL.SelectedIndex = 0;
            this.AmountTextBox.Text = "0";
            this.FreeProductCodeTextBox.Text = "";
            this.FreeProductNameTextBox.Text = "";
            this.FreeQtyTextBox.Text = "0";
            this.FgMultipleCheckBox.Checked = false;
            this.FgActiveCheckBox.Checked = false;
            this.RemarkTextBox.Text = "";
        }

        protected void CheckValidData()
        {
            if (this.ProductNameTextBox.Text == "")
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Check Your Product Code. ";
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
            //else if (this.PromoTypeRBL.SelectedValue == "D")
            //{
            //    if (this.AmountTextBox.Text == "")
            //        this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Fill Your Amount. ";
            //    else
            //        if (Convert.ToDecimal(this.AmountTextBox.Text) == 0)
            //            this.WarningLabel.Text = this.WarningLabel.Text + " " + "Amount Must More Then 0. ";
            //}
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                POSMsPromoProduct _posMsPromoProduct = new POSMsPromoProduct();

                _posMsPromoProduct.PromoCode = this.PromoCodeTextBox.Text;
                _posMsPromoProduct.ProductCode = this.ProductCodeTextBox.Text;
                _posMsPromoProduct.FgMin = Convert.ToChar(this.FgMinTypeRBL.SelectedValue);
                _posMsPromoProduct.MinValue = Convert.ToDecimal(this.MinValueTextBox.Text);
                _posMsPromoProduct.MaksimalQty = Convert.ToDecimal(this.MaxQtyTextBox.Text);
                _posMsPromoProduct.FgPromoType = Convert.ToChar(this.PromoTypeRBL.SelectedValue);
                if (this.PromoTypeRBL.SelectedValue == "D" & this.AmountTypeRBL.Enabled == true)
                {
                    _posMsPromoProduct.AmountType = this.AmountTypeRBL.SelectedValue.ToString();
                    _posMsPromoProduct.Amount = Convert.ToDecimal(this.AmountTextBox.Text);
                }
                //else if (this.PromoTypeRBL.SelectedValue == "D" & this.AmountTypeRBL.Enabled == false)
                //{
                //    POSMsPromoMember _posMsPromoMember = this._promoBL.GetSinglePOSMsPromoMember(this.PromoCodeTextBox.Text, "");
                //    _posMsPromoProduct.AmountType = _posMsPromoMember.AmountType.ToString();
                //    _posMsPromoProduct.Amount = _posMsPromoMember.Amount;
                //}
                else if (this.PromoTypeRBL.SelectedValue == "P")
                {
                    _posMsPromoProduct.FreeProductCode = this.FreeProductCodeTextBox.Text;
                    _posMsPromoProduct.FreeQty = Convert.ToDecimal(this.FreeQtyTextBox.Text);
                    _posMsPromoProduct.FgMultiple = (this.FgMultipleCheckBox.Checked == true) ? true : false;
                }
                _posMsPromoProduct.FgActive = (this.FgActiveCheckBox.Checked == true) ? 'Y' : 'N';
                _posMsPromoProduct.Remark = this.RemarkTextBox.Text;
                _posMsPromoProduct.CreatedBy = HttpContext.Current.User.Identity.Name;
                _posMsPromoProduct.CreatedDate = DateTime.Now;

                String _result = this._promoBL.AddPOSMsPromoProduct(_posMsPromoProduct);

                if (_result == "")
                {
                    Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "You Failed Add Data" + _result;
                }
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        protected void PromoTypeRBL_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.AmountTypeRBL.Enabled = false;
            this.AmountTextBox.Enabled = false;
            this.FreeProductCodeTextBox.Enabled = false;
            this.FreeProductNameTextBox.Enabled = false;
            this.FreeQtyTextBox.Enabled = false;

            if (this.PromoTypeRBL.SelectedValue == "P")
            {
                this.FreeProductCodeTextBox.Enabled = true;
                this.FreeProductNameTextBox.Enabled = true;
                this.FreeQtyTextBox.Enabled = true;
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

        protected void ProductCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.ProductCodeTextBox.Text != "")
            {
                this.ProductNameTextBox.Text = this._productBL.GetProductNameByCode(this.ProductCodeTextBox.Text.Trim());
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
            if (this.FgMinTypeRBL.SelectedValue == "Q")
                this.MinimumValueLiteral.Text = "Minimum Qty";
            else
                this.MinimumValueLiteral.Text = "Minimum Payment";
        }

    }
}