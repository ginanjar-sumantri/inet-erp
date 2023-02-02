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
    public partial class ProductDetailEdit : ProductBase
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

                //this.ShowUnitConvert();

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
                this.ChangesLabel();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.RateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.UnitConvertTextBox.Attributes.Add("ReadOnly", "True");
            this.UnitConvertTextBox.Attributes.Add("Style", "background-color: #CCCCCC");
        }

        public void ShowData()
        {
            MsProductConvert _msProductConvert = this._productConvertBL.GetSingleProductConvert(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._unitBLKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            //this.UnitConvertDropDownList.SelectedValue = _unitBL.GetUnitNameByCode(_msProductConvert.UnitCode);
            this.UnitConvertTextBox.Text = _unitBL.GetUnitNameByCode(_msProductConvert.UnitCode);
            this.UnitTextBox.Text = _msProductConvert.UnitConvert;
            this.RateTextBox.Text = (_msProductConvert.Rate == 0) ? "0" : _msProductConvert.Rate.ToString("#,##0.##");
        }

        //private void ShowUnitConvert()
        //{
        //    this.UnitConvertDropDownList.DataTextField = "UnitName";
        //    this.UnitConvertDropDownList.DataValueField = "UnitCode";
        //    this.UnitConvertDropDownList.DataSource = this._unitBL.GetListForDDL();
        //    this.UnitConvertDropDownList.DataBind();
        //    this.UnitConvertDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsProductConvert _msProductConvert = this._productConvertBL.GetSingleProductConvert(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._unitBLKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            //_msProductConvert.UnitConvert = //this.UnitConvertDropDownList.SelectedValue;
            _msProductConvert.Rate = Convert.ToDecimal(this.RateTextBox.Text);

            bool _result = this._productConvertBL.EditProductConvert(_msProductConvert);

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
            this.ShowData();
        }

        protected void ChangesLabel()
        {
            if (this.RateTextBox.Text != "")
                this.UnitLabel.Text = "1 " + ((this.UnitConvertTextBox.Text == "") ? " " : this.UnitConvertTextBox.Text) + " equals " + ((this.RateTextBox.Text == "") ? " " : this.RateTextBox.Text) + " " + this.UnitTextBox.Text;
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