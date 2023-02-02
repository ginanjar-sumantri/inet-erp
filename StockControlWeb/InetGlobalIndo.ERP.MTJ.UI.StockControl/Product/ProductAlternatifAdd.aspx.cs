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
    public partial class ProductAlternatifAdd : ProductBase
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

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                
                this.ShowCurr();
                
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
        }

        protected void SetAttribute()
        {
            //this.SalesPriceTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            //this.SalesPriceTextBox.Attributes.Add("OnBlur", "return ChangeFormat2(" + this.SalesPriceTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        public void ClearData()
        {
            this.ClearLabel();
            this.AlternatifCodeDropDownList.SelectedValue = "null";
        }

        private void ShowCurr()
        {
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            this.AlternatifCodeDropDownList.DataTextField = "ProductName";
            this.AlternatifCodeDropDownList.DataValueField = "ProductCode";
            this.AlternatifCodeDropDownList.DataSource = this._productBL.GetListProductAlternatifForDDL(_productCode);
            this.AlternatifCodeDropDownList.DataBind();
            this.AlternatifCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsProduct_Alternatif _msProduct_Alternatif = new MsProduct_Alternatif();

            _msProduct_Alternatif.ProductCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            _msProduct_Alternatif.AlternatifCode = this.AlternatifCodeDropDownList.SelectedValue;
            _msProduct_Alternatif.Remark = this.RemarkTextBox.Text;
            _msProduct_Alternatif.CreatedBy = HttpContext.Current.User.Identity.Name;
            _msProduct_Alternatif.CreatedDate = DateTime.Now;
            _msProduct_Alternatif.EditBy = HttpContext.Current.User.Identity.Name;
            _msProduct_Alternatif.EditDate = DateTime.Now;

            bool _result = this._productBL.AddProductAlternatif(_msProduct_Alternatif);

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