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
    public partial class PromoFreeProductAdd : PromoBase
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

                this.FreeProductNameTextBox.Attributes.Add("ReadOnly", "True");
                this.FreeQtyTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.FreeQtyTextBox.ClientID + ")");

                this.btnSearchProduct.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findProduct&configCode=product','_popSearch','maximize=1,toolbar=0,location=0,status=0,scrollbars=1')";
                this.FreeProductCodeTextBox.Attributes.Add("onblur", "findFreeProduct");

                String spawnJS = "<script language='JavaScript'>\n";

                spawnJS += "function findFreeProduct(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.FreeProductCodeTextBox.ClientID + "').value = dataArray[0];\n";
                spawnJS += "document.getElementById('" + this.FreeProductNameTextBox.ClientID + "').value = dataArray[1];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                spawnJS += "</script>\n";
                this.javascriptReceiver.Text = spawnJS;

                this.ClearData();
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
            this.FreeProductCodeTextBox.Text = "";
            this.FreeProductNameTextBox.Text = "";
            this.FreeQtyTextBox.Text = "0";
            this.FgActiveCheckBox.Checked = false;
            this.RemarkTextBox.Text = "";
        }

        protected void CheckValidData()
        {
            if (this.FreeProductNameTextBox.Text == "")
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Check Your Free Product Code. ";
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
                POSMsPromoFreeProduct _posMsPromoFreeProduct = new POSMsPromoFreeProduct();

                _posMsPromoFreeProduct.PromoCode = this.PromoCodeTextBox.Text;
                _posMsPromoFreeProduct.FreeProductCode = this.FreeProductCodeTextBox.Text;
                _posMsPromoFreeProduct.FreeQty = Convert.ToDecimal(this.FreeQtyTextBox.Text);
                _posMsPromoFreeProduct.FgActive = (this.FgActiveCheckBox.Checked == true) ? 'Y' : 'N';
                _posMsPromoFreeProduct.Remark = this.RemarkTextBox.Text;
                _posMsPromoFreeProduct.CreatedBy = HttpContext.Current.User.Identity.Name;
                _posMsPromoFreeProduct.CreatedDate = DateTime.Now;

                String _result = this._promoBL.AddPOSMsPromoFreeProduct(_posMsPromoFreeProduct);

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

        protected void FreeProductCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.FreeProductCodeTextBox.Text != "")
            {
                this.FreeProductNameTextBox.Text = this._productBL.GetProductNameByCode(this.FreeProductCodeTextBox.Text.Trim());
            }
        }
    }
}