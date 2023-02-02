using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockAdjustment
{
    public partial class StockAdjustmentDetailAdd : StockAdjustmentBase
    {
        private StockAdjustmentBL _stockAdjustBL = new StockAdjustmentBL();
        private StockOpnameBL _stockOpnameBL = new StockOpnameBL();
        private ProductBL _product = new ProductBL();
        private WarehouseBL _warehouse = new WarehouseBL();
        private UnitBL _unit = new UnitBL();
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

                this.ClearLabel();
                this.SetAttribute();
                this.ClearData();
            }

            if (this.ProductPicker1.ProductCode != "")
            {
                MsProduct _msProduct = this._product.GetSingleProduct(this.ProductPicker1.ProductCode);
                this.UnitTextBox.Text = _unit.GetUnitNameByCode(_msProduct.Unit);
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.UnitTextBox.Attributes.Add("ReadOnly", "True");

            this.QtyTextBox.Attributes.Add("OnKeyUp", "formatangka(" + this.QtyTextBox.ClientID + ");");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
            this.PriceTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab");
        }

        private void ClearData()
        {
            STCAdjustHd _stcAdjustHd = this._stockAdjustBL.GetSingleSTCAdjustHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_stcAdjustHd.OpnameNo != "null")
            {
                //this.ShowProductOpname();
                //this.ProductPicker1.Visible = false;
                //this.ShowLocationOpname();
                this.ShowLocation();
                //this.QtyTextBox.Attributes.Add("ReadOnly", "True");
                //this.QtyTextBox.Attributes.Add("Style", "background-color: #CCCCCC");
            }
            else
            {
                //this.ShowProduct();
                //this.ProductDropDownList.Visible = false;
                this.ShowLocation();
                this.QtyTextBox.Attributes.Remove("ReadOnly");
                this.QtyTextBox.Attributes.Add("Style", "background-color: #FFFFFF");
            }
            this.RemarkTextBox.Text = "";
            this.QtyTextBox.Text = "0";
            this.UnitTextBox.Text = "";
            this.PriceTextBox.Text = "0";
        }

        //private void ShowProduct()
        //{
        //    this.ProductDropDownList.Items.Clear();
        //    this.ProductDropDownList.DataTextField = "ProductName";
        //    this.ProductDropDownList.DataValueField = "ProductCode";
        //    this.ProductDropDownList.DataSource = this._product.GetListProductForDDLStock();
        //    this.ProductDropDownList.DataBind();
        //    this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        //private void ShowProductOpname()
        //{
        //    STCAdjustHd _stcAdjustHd = this._stockAdjustBL.GetSingleSTCAdjustHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

        //    this.ProductDropDownList.Items.Clear();
        //    this.ProductDropDownList.DataTextField = "ProductName";
        //    this.ProductDropDownList.DataValueField = "ProductCode";
        //    this.ProductDropDownList.DataSource = this._stockOpnameBL.GetListProductFromSTCOpnameForDDL(_stcAdjustHd.OpnameNo);
        //    this.ProductDropDownList.DataBind();
        //    this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        private void ShowLocation()
        {
            STCAdjustHd _stcAdjustHd = this._stockAdjustBL.GetSingleSTCAdjustHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.LocationDropDownList.Items.Clear();
            this.LocationDropDownList.DataTextField = "WLocationName";
            this.LocationDropDownList.DataValueField = "WLocationCode";
            this.LocationDropDownList.DataSource = this._warehouse.GetListWrhsLocationByCodeForDDL(_stcAdjustHd.WrhsCode);
            this.LocationDropDownList.DataBind();
            this.LocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowLocationOpname()
        {
            STCAdjustHd _stcAdjustHd = this._stockAdjustBL.GetSingleSTCAdjustHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.LocationDropDownList.Items.Clear();
            this.LocationDropDownList.DataTextField = "WLocationName";
            this.LocationDropDownList.DataValueField = "WLocationCode";
            this.LocationDropDownList.DataSource = this._stockOpnameBL.GetListLocationFromSTCOpnameDt(_stcAdjustHd.OpnameNo, this.ProductPicker1.ProductCode);
            this.LocationDropDownList.DataBind();
            this.LocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.PriceTextBox.Text == "")
            {
                this.PriceTextBox.Text = "0";
            }
           
            STCAdjustDt _stcAdjustDt = new STCAdjustDt();
            //MsProduct _msProduct = this._product.GetSingleProduct(this.ProductDropDownList.SelectedValue);

            STCAdjustHd _stcAdjustHd = this._stockAdjustBL.GetSingleSTCAdjustHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            MsProduct _msProduct = new MsProduct();
            //if (_stcAdjustHd.OpnameNo != "null")
            //    _msProduct = this._product.GetSingleProduct(this.ProductDropDownList.SelectedValue);
            //else
            _msProduct = this._product.GetSingleProduct(this.ProductPicker1.ProductCode);

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            _stcAdjustDt.TransNmbr = _transNo;
            _stcAdjustDt.ProductCode = this.ProductPicker1.ProductCode;
            _stcAdjustDt.LocationCode = this.LocationDropDownList.SelectedValue;
            _stcAdjustDt.Unit = _msProduct.Unit;
            _stcAdjustDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcAdjustDt.FgAdjust = Convert.ToChar(this.FgAdjustDropDownList.SelectedValue);
            _stcAdjustDt.TotalCost = Convert.ToDecimal(this.PriceTextBox.Text) * Convert.ToDecimal(this.QtyTextBox.Text);
            _stcAdjustDt.PriceCost = Convert.ToDecimal(this.PriceTextBox.Text);
            _stcAdjustDt.Remark = this.RemarkTextBox.Text;

            bool _result = this._stockAdjustBL.AddSTCAdjustDt(_stcAdjustDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
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

        protected void LocationDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.LocationDropDownList.SelectedValue != "null")
            {
                //STCAdjustHd _stcAdjustHd = this._stockAdjustBL.GetSingleSTCAdjustHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                //if (_stcAdjustHd.OpnameNo != "null")
                //{
                //    STCOpnameDt _stcOpnameDt = this._stockOpnameBL.GetSingleSTCOpnameDt(_stcAdjustHd.OpnameNo, this.ProductPicker1.ProductCode, this.LocationDropDownList.SelectedValue);

                //    this.QtyTextBox.Text = (_stcOpnameDt.QtyOpname == 0) ? "0" : Math.Abs(_stcOpnameDt.QtyOpname).ToString("#,##0.##");
                //    if (_stcOpnameDt.QtyOpname > 0)
                //    {
                //        this.FgAdjustDropDownList.SelectedValue = "+";
                //    }
                //    else
                //    {
                //        this.FgAdjustDropDownList.SelectedValue = "-";
                //    }
                //    this.FgAdjustDropDownList.Attributes.Add("Disabled", "True");
                //    this.UnitTextBox.Text = _unit.GetUnitNameByCode(_stcOpnameDt.Unit);
                //}
                //else
                //{
                MsProduct _msProduct = this._product.GetSingleProduct(this.ProductPicker1.ProductCode);

                this.QtyTextBox.Text = "0";
                this.UnitTextBox.Text = _unit.GetUnitNameByCode(_msProduct.Unit);
                //}
            }
            else
            {
                this.QtyTextBox.Text = "0";
                this.UnitTextBox.Text = "";
            }
        }

        //protected void ProductDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.ProductDropDownList.SelectedValue != "null")
        //    {
        //        STCAdjustHd _stcAdjustHd = this._stockAdjustBL.GetSingleSTCAdjustHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

        //        if (_stcAdjustHd.OpnameNo != "null")
        //        {
        //            this.ShowLocationOpname();
        //        }
        //        else
        //        {
        //            this.ShowLocation();
        //        }
        //    }
        //    else
        //    {
        //        this.LocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //        this.LocationDropDownList.SelectedValue = "null";
        //        this.QtyTextBox.Text = "0";
        //        this.UnitTextBox.Text = "";
        //    }
        //}

        protected void FgAdjustDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.FgAdjustDropDownList.SelectedValue == "-")
            {
                this.PriceTextBox.Text = "0";
                this.PriceTextBox.Attributes.Add("ReadOnly", "True");
                this.PriceTextBox.Attributes.Add("style", "background-color:#cccccc");
            }
            else
            {
                this.PriceTextBox.Text = "0";
                this.PriceTextBox.Attributes.Remove("ReadOnly");
                this.PriceTextBox.Attributes.Add("style", "background-color:#ffffff");
            }
        }
    }
}