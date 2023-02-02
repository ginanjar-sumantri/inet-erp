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
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Product
{
    public partial class ProductDetailAdd : ProductBase
    {
        private ProductBL _productConvertBL = new ProductBL();
        private UnitBL _unitBL = new UnitBL();
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
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                //this.ShowUnit();
                this.ShowUnitConvert();

                this.ClearLabel();
                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.RateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.UnitTextBox.Attributes.Add("ReadOnly", "True");
            this.UnitTextBox.Attributes.Add("style", "background-color: #CCCCCC");
        }

        public void ClearData()
        {
            MsProduct _msProduct = new MsProduct();
            _msProduct = _productConvertBL.GetSingleProduct(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            this.ClearLabel();
            this.UnitConvertDropDownList.SelectedValue = "null";
            //this.UnitDropDownList.SelectedValue = "null";
            this.UnitTextBox.Text = _msProduct.Unit;
            this.RateTextBox.Text = "0";
        }

        //private void ShowUnit()
        //{
        //    this.UnitDropDownList.DataTextField = "UnitName";
        //    this.UnitDropDownList.DataValueField = "UnitCode";
        //    this.UnitDropDownList.DataSource = this._unitBL.GetListForDDL();
        //    this.UnitDropDownList.DataBind();
        //    this.UnitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        private void ShowUnitConvert()
        {
            this.UnitConvertDropDownList.DataTextField = "UnitName";
            this.UnitConvertDropDownList.DataValueField = "UnitCode";
            this.UnitConvertDropDownList.DataSource = this._unitBL.GetListForDDL();
            this.UnitConvertDropDownList.DataBind();
            this.UnitConvertDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsProductConvert _msProductConvert = new MsProductConvert();

            _msProductConvert.ProductCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            _msProductConvert.UnitCode = this.UnitConvertDropDownList.SelectedValue;//this.UnitDropDownList.SelectedValue;
            _msProductConvert.UnitConvert =this.UnitTextBox.Text;  
            _msProductConvert.Rate = Convert.ToDecimal(this.RateTextBox.Text);

            bool _result = this._productConvertBL.AddProductConvert(_msProductConvert);

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

        protected void ChangesLabel()
        {
            if(this.UnitConvertDropDownList.SelectedValue != null && this.RateTextBox.Text != "")
                this.UnitLabel.Text = "1 " + ((this.UnitConvertDropDownList.SelectedValue == null)? " " : this.UnitConvertDropDownList.SelectedItem.Text) + " equals " + ((this.RateTextBox.Text == "")? " " : this.RateTextBox.Text) + " " + this.UnitTextBox.Text;
        }

        protected void UnitConvertDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ChangesLabel();
        }

        protected void RateTextBox_TextChanged(object sender, EventArgs e)
        {
            this.ChangesLabel();
        }
}
}