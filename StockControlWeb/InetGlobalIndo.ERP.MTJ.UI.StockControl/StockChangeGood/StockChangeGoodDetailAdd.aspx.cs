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

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockChangeGood
{
    public partial class StockChangeGoodDetailAdd : StockChangeGoodBase
    {
        private StockChangeGoodBL _stockChangeGoodBL = new StockChangeGoodBL();
        private ProductBL _productBL = new ProductBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
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

                this.SetAttribute();
                this.ClearData();                
            }
            if ( this.ProductPicker1.ProductCode != "" )
                this.UnitTextBox.Text = _productBL.GetUnitNameByCode(this.ProductPicker1.ProductCode);
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.UnitTextBox.Attributes.Add("ReadOnly", "True");

            this.QtyTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ClearData()
        {
            this.ClearLabel();

            //this.ShowProduct();
            this.ShowLocationSrc();
            this.ShowLocationDest();

            this.QtyTextBox.Attributes.Remove("ReadOnly");
            this.QtyTextBox.Attributes.Add("Style", "background-color: #FFFFFF");
            this.QtyTextBox.Text = "0";
            this.UnitTextBox.Text = "";
            this.RemarkTextBox.Text = "";
        }

        //private void ShowProduct()
        //{
        //    STCChangeHd _stcChangeHd = _stockChangeGoodBL.GetSingleSTCChangeHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

        //    this.ProductSrcDropDownList.Items.Clear();
        //    this.ProductSrcDropDownList.DataValueField = "ProductCode";
        //    this.ProductSrcDropDownList.DataTextField = "ProductName";
        //    this.ProductSrcDropDownList.DataSource = this._productBL.GetListProductForDDLStockByWrhsAndWrhsSub(_stcChangeHd.WrhsSrc, _stcChangeHd.WrhsSrcSubLed);
        //    this.ProductSrcDropDownList.DataBind();
        //    this.ProductSrcDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

        //    this.ProductDestDropDownList.Items.Clear();
        //    this.ProductDestDropDownList.DataValueField = "ProductCode";
        //    this.ProductDestDropDownList.DataTextField = "ProductName";
        //    this.ProductDestDropDownList.DataSource = this._productBL.GetListProductForDDLActiveAndStock();
        //    this.ProductDestDropDownList.DataBind();
        //    this.ProductDestDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        private void ShowLocationSrc()
        {
            STCChangeHd _stcChangeHd = this._stockChangeGoodBL.GetSingleSTCChangeHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.LocationSrcDropDownList.Items.Clear();
            this.LocationSrcDropDownList.DataTextField = "WLocationName";
            this.LocationSrcDropDownList.DataValueField = "WLocationCode";
            this.LocationSrcDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(_stcChangeHd.WrhsSrc);
            this.LocationSrcDropDownList.DataBind();
            this.LocationSrcDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowLocationDest()
        {
            STCChangeHd _stcChangeHd = this._stockChangeGoodBL.GetSingleSTCChangeHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.LocationDestDropDownList.Items.Clear();
            this.LocationDestDropDownList.DataTextField = "WLocationName";
            this.LocationDestDropDownList.DataValueField = "WLocationCode";
            this.LocationDestDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(_stcChangeHd.WrhsDest);
            this.LocationDestDropDownList.DataBind();
            this.LocationDestDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCChangeDt _stcChangeDt = new STCChangeDt();
            //MsProduct _msProduct = this._productBL.GetSingleProduct(this.ProductSrcDropDownList.SelectedValue);
            MsProduct _msProduct = this._productBL.GetSingleProduct(this.ProductPicker1.ProductCode );

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            _stcChangeDt.TransNmbr = _transNo;
            //_stcChangeDt.ProductSrc = this.ProductSrcDropDownList.SelectedValue;
            _stcChangeDt.ProductSrc = this.ProductPicker1.ProductCode ;
            _stcChangeDt.LocationSrc = this.LocationSrcDropDownList.SelectedValue;
            //_stcChangeDt.ProductDest = this.ProductDestDropDownList.SelectedValue;
            _stcChangeDt.ProductDest = this.ProductPicker21.ProductCode ;
            _stcChangeDt.LocationDest = this.LocationDestDropDownList.SelectedValue;
            _stcChangeDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcChangeDt.Unit = _productBL.GetUnitCodeByCode(this.ProductPicker1.ProductCode);
            _stcChangeDt.TotalCost = 0;
            _stcChangeDt.PriceCost = 0;
            _stcChangeDt.Remark = this.RemarkTextBox.Text;

            bool _result = this._stockChangeGoodBL.AddSTCChangeDt(_stcChangeDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        //protected void ProductSrcDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.ProductSrcDropDownList.SelectedValue == "null")
        //    {
        //        this.UnitTextBox.Text = "";
        //    }
        //    else
        //    {
        //        this.UnitTextBox.Text = _productBL.GetUnitNameByCode(this.ProductSrcDropDownList.SelectedValue);
        //    }
        //}
    }
}