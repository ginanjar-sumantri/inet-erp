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

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockBeginning
{
    public partial class StockBeginningDetailAdd : StockBeginningBase
    {
        private StockBeginningBL _stockBeginBL = new StockBeginningBL();
        private ProductBL _productBL = new ProductBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private UnitBL _unitBL = new UnitBL();
        private PermissionBL _permBL = new PermissionBL();
        private PriceGroupBL _priceGroupBL = new PriceGroupBL();

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

                this.ShowLocation();
                //this.ShowProduct();

                this.SetAttribute();
                this.ClearLabel();
                this.ClearData();
            }

            if (this.ProductPicker1.ProductCode != "")
            {
                string _type;
                bool _IsUsingPg;

                MsProduct _msProduct = this._productBL.GetSingleProduct(this.ProductPicker1.ProductCode);
                this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_msProduct.Unit);
                _type = _msProduct.ProductType;
                _IsUsingPg = this._productBL.GetSingleProductType(_type).IsUsingPG;
                if (Convert.ToInt32(this.QtyTextBox.Text) == 0 || Convert.ToDecimal(this.PriceTextBox.Text) == 0)
                {
                    if (_IsUsingPg == true)
                    {
                        this.PriceTextBox.Text = this._priceGroupBL.GetAmountForexByPGCode(_msProduct.PriceGroupCode).ToString();
                    }
                    else
                    {
                        this.PriceTextBox.Text = "0";
                    }
                }
            }
            else
            {
                this.UnitTextBox.Text = "";
            }

        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.UnitTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalTextBox.Attributes.Add("ReadOnly", "True");

            this.QtyTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.QtyTextBox.Attributes.Add("OnBlur", "Adjust(" + QtyTextBox.ClientID + ", " + PriceTextBox.ClientID + ", " + TotalTextBox.ClientID + ");");
            this.PriceTextBox.Attributes.Add("OnBlur", "Calculate(" + QtyTextBox.ClientID + ", " + PriceTextBox.ClientID + ", " + TotalTextBox.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ClearData()
        {
            this.QtyTextBox.Text = "0";
            this.PriceTextBox.Text = "0";
            this.TotalTextBox.Text = "0";
            this.UnitTextBox.Text = "";
            //this.ProductDropDownList.SelectedValue = "null";
            this.ProductPicker1.ProductName = "";
            this.ProductPicker1.ProductCode = "";
            this.LocationDropDownList.SelectedValue = "null";
            this.RemarkTextBox.Text = "";
        }

        //private void ShowProduct()
        //{
        //    this.ProductDropDownList.Items.Clear();
        //    this.ProductDropDownList.DataTextField = "ProductName";
        //    this.ProductDropDownList.DataValueField = "ProductCode";
        //    this.ProductDropDownList.DataSource = this._productBL.GetListProductForDDLStock();
        //    this.ProductDropDownList.DataBind();
        //    this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        private void ShowLocation()
        {
            STCAdjustHd _stcAdjustHd = this._stockBeginBL.GetSingleSTCAdjustHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.LocationDropDownList.Items.Clear();
            this.LocationDropDownList.DataTextField = "WLocationName";
            this.LocationDropDownList.DataValueField = "WLocationCode";
            this.LocationDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(_stcAdjustHd.WrhsCode);
            this.LocationDropDownList.DataBind();
            this.LocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCAdjustDt _stcAdjustDt = new STCAdjustDt();
            //MsProduct _msProduct = this._productBL.GetSingleProduct(this.ProductDropDownList.SelectedValue);
            MsProduct _msProduct = this._productBL.GetSingleProduct(this.ProductPicker1.ProductCode);

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            _stcAdjustDt.TransNmbr = _transNo;
            //_stcAdjustDt.ProductCode = this.ProductDropDownList.SelectedValue;
            _stcAdjustDt.ProductCode = this.ProductPicker1.ProductCode;
            _stcAdjustDt.LocationCode = this.LocationDropDownList.SelectedValue;
            _stcAdjustDt.Unit = _msProduct.Unit;
            _stcAdjustDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            //_stcAdjustDt.FgAdjust = Convert.ToChar(this.AdjustHidden.Value);
            _stcAdjustDt.FgAdjust = '+';
            _stcAdjustDt.TotalCost = Convert.ToDecimal(this.TotalTextBox.Text);
            _stcAdjustDt.PriceCost = Convert.ToDecimal(this.PriceTextBox.Text);
            _stcAdjustDt.Remark = this.RemarkTextBox.Text;

            bool _result = this._stockBeginBL.AddSTCAdjustDt(_stcAdjustDt);

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

        //protected void ProductDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.ProductDropDownList.SelectedValue != "null")
        //    {
        //        MsProduct _msProduct = this._productBL.GetSingleProduct(this.ProductDropDownList.SelectedValue);

        //        this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_msProduct.Unit);
        //    }
        //    else
        //    {
        //        this.UnitTextBox.Text = "";
        //    }
        //}


    }
}