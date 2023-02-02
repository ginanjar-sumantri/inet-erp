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
    public partial class ProductSalesPriceAdd : ProductBase
    {
        private ProductBL _productBL = new ProductBL();
        private CurrencyBL _currBL = new CurrencyBL();
        private PermissionBL _permBL = new PermissionBL();
        private UnitBL _unitBL = new UnitBL();

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
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowCurr();
                this.ShowUnit();

                this.ClearLabel();
                this.ClearData();
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

        public void ClearData()
        {
            this.ClearLabel();
            this.CurrDropDownList.SelectedValue = "null";
            this.SalesPriceTextBox.Text = "0";
            this.FgActiveCheckBox.Checked = false;
            this.RemarkTextBox.Text = "";
        }

        private void ShowCurr()
        {
            this.CurrDropDownList.DataTextField = "CurrCode";
            this.CurrDropDownList.DataValueField = "CurrCode";
            this.CurrDropDownList.DataSource = this._currBL.GetListAll();
            this.CurrDropDownList.DataBind();
            this.CurrDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowUnit()
        {
            this.UnitDropDownList.DataTextField = "UnitCode";
            this.UnitDropDownList.DataValueField = "UnitCode";
            this.UnitDropDownList.DataSource = this._unitBL.GetListForDDLProductConvert(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            this.UnitDropDownList.DataBind();
            this.UnitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            Master_ProductSalesPrice _msProductSalesPrice = new Master_ProductSalesPrice();

            _msProductSalesPrice.ProductCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            _msProductSalesPrice.CurrCode = this.CurrDropDownList.SelectedValue;
            _msProductSalesPrice.SalesPrice = Convert.ToDecimal(this.SalesPriceTextBox.Text);
            _msProductSalesPrice.UnitCode = this.UnitDropDownList.SelectedValue;
            _msProductSalesPrice.FgActive = (this.FgActiveCheckBox.Checked == true) ? 'Y' : 'N';
            _msProductSalesPrice.Remark = this.RemarkTextBox.Text;
            _msProductSalesPrice.CreatedBy = HttpContext.Current.User.Identity.Name;
            _msProductSalesPrice.CreatedDate = DateTime.Now;
            _msProductSalesPrice.ModifiedBy = HttpContext.Current.User.Identity.Name;
            _msProductSalesPrice.ModifiedDate = DateTime.Now;


            bool _result = this._productBL.AddProductSalesPrice(_msProductSalesPrice);

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

        protected void CurrDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CurrDropDownList.SelectedValue != "null")
            {
                byte _decimalPlace = _currBL.GetDecimalPlace(this.CurrDropDownList.SelectedValue);

                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                this.SalesPriceTextBox.Attributes.Add("OnBlur", "return ChangeFormat2(" + this.SalesPriceTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            }
            else
            {
                this.DecimalPlaceHiddenField.Value = "";
            }
        }
    }
}