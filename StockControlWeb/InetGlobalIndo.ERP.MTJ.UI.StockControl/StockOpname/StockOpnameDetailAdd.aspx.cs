using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockOpname
{
    public partial class StockOpnameDetailAdd : StockOpnameBase
    {
        private ProductBL _productBL = new ProductBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private StockOpnameBL _stockOpnameBL = new StockOpnameBL();
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
                //this.ShowProduct();
                this.ShowLocation();
                this.ClearData();
            }

            string _tempWarehouseCode = this._stockOpnameBL.GetWarehouseCodeByCode(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            string _subledCode = this._stockOpnameBL.GetSingleSTCOpnameHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).WrhsSubLed;
            decimal _sysQty;
            if (_subledCode != null)
            {
                //_sysQty = this._productBL.GetSystemQty(this.ProductDropDownList.SelectedValue, this.LocationDropDownList.SelectedValue, _tempWarehouseCode, _subledCode);
                _sysQty = this._productBL.GetSystemQty(this.ProductPicker1.ProductCode, this.LocationDropDownList.SelectedValue, _tempWarehouseCode, _subledCode);
                this.QtySystemTextBox.Text = (_sysQty == 0) ? "0" : _sysQty.ToString("#,##0.##");
            }
            else
            {
                //_sysQty = this._productBL.GetSystemQty(this.ProductDropDownList.SelectedValue, this.LocationDropDownList.SelectedValue, _tempWarehouseCode, "");
                _sysQty = this._productBL.GetSystemQty(this.ProductPicker1.ProductCode, this.LocationDropDownList.SelectedValue, _tempWarehouseCode, "");
                this.QtySystemTextBox.Text = (_sysQty == 0) ? "0" : _sysQty.ToString("#,##0.##");
            }

            if (this.ProductPicker1.ProductCode != "null")
            {
                this.UnitTextBox.Text = this._productBL.GetUnitNameByCode(this.ProductPicker1.ProductCode);
                _sysQty = this._productBL.GetSystemQty(this.ProductPicker1.ProductCode, this.LocationDropDownList.SelectedValue, _tempWarehouseCode, _subledCode);
                this.QtySystemTextBox.Text = (_sysQty == 0) ? "0" : _sysQty.ToString("#,##0.##");
            }
            else
            {
                this.UnitTextBox.Text = "";
                this.QtySystemTextBox.Text = "0";
            }
            this.QtyOpnameTextBox.Text = (Convert.ToDecimal(this.QtyActualTextBox.Text) - Convert.ToDecimal(this.QtySystemTextBox.Text) == 0) ? "0" : (Convert.ToDecimal(this.QtyActualTextBox.Text) - Convert.ToDecimal(this.QtySystemTextBox.Text)).ToString("#,##0.##");

        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void SetAttribute()
        {
            this.QtySystemTextBox.Attributes.Add("OnBlur", "Calculate(" + this.QtySystemTextBox.ClientID + "," + this.QtyActualTextBox.ClientID + "," + this.QtyOpnameTextBox.ClientID + ");");
            this.QtyActualTextBox.Attributes.Add("OnBlur", "Calculate(" + this.QtySystemTextBox.ClientID + "," + this.QtyActualTextBox.ClientID + "," + this.QtyOpnameTextBox.ClientID + ");");

            this.QtySystemTextBox.Attributes.Add("ReadOnly", "true");
            this.QtyActualTextBox.Attributes.Add("OnKeyDown", "return Numeric();");

            this.QtyOpnameTextBox.Attributes.Add("ReadOnly", "true");
            this.UnitTextBox.Attributes.Add("ReadOnly", "true");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        //protected void ShowProduct()
        //{
        //    this.ProductDropDownList.Items.Clear();
        //    this.ProductDropDownList.DataTextField = "ProductName";
        //    this.ProductDropDownList.DataValueField = "ProductCode";
        //    this.ProductDropDownList.DataSource = this._productBL.GetListProductForDDLActiveAndStock();
        //    this.ProductDropDownList.DataBind();
        //    this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        protected void ShowLocation()
        {
            string _tempWarehouseCode = this._stockOpnameBL.GetWarehouseCodeByCode(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.LocationDropDownList.Items.Clear();
            this.LocationDropDownList.DataTextField = "WLocationName";
            this.LocationDropDownList.DataValueField = "WLocationCode";
            this.LocationDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(_tempWarehouseCode);
            this.LocationDropDownList.DataBind();
            this.LocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        //protected void ProductDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string _tempWarehouseCode = this._stockOpnameBL.GetWarehouseCodeByCode(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
        //    string _subledCode = this._stockOpnameBL.GetSingleSTCOpnameHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).WrhsSubLed;
        //    decimal _sysQty;

        //    if (this.ProductDropDownList.SelectedValue != "null")
        //    {
        //        this.UnitTextBox.Text = this._productBL.GetUnitNameByCode(this.ProductDropDownList.SelectedValue);
        //        _sysQty = this._productBL.GetSystemQty(this.ProductDropDownList.SelectedValue, this.LocationDropDownList.SelectedValue, _tempWarehouseCode, _subledCode);
        //        this.QtySystemTextBox.Text = (_sysQty == 0) ? "0" : _sysQty.ToString("#,##0.##");
        //    }
        //    else
        //    {
        //        this.UnitTextBox.Text = "";
        //        this.QtySystemTextBox.Text = "0";
        //    }
        //    this.QtyOpnameTextBox.Text = (Convert.ToDecimal(this.QtyActualTextBox.Text) - Convert.ToDecimal(this.QtySystemTextBox.Text) == 0) ? "0" : (Convert.ToDecimal(this.QtyActualTextBox.Text) - Convert.ToDecimal(this.QtySystemTextBox.Text)).ToString("#,##0.##");
        //}

        protected void ClearData()
        {
            this.ClearLabel();

            //this.ProductDropDownList.SelectedValue = "null";
            this.ProductPicker1.ProductCode = "";
            this.ProductPicker1.ProductName = "";
            this.LocationDropDownList.SelectedValue = "null";
            this.QtySystemTextBox.Text = "0";
            this.QtyActualTextBox.Text = "0";
            this.QtyOpnameTextBox.Text = "0";
            this.UnitTextBox.Text = "";
            this.RemarkTextBox.Text = "";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCOpnameDt _stcOpnameDt = new STCOpnameDt();

            _stcOpnameDt.TransNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            //_stcOpnameDt.ProductCode = this.ProductDropDownList.SelectedValue;
            _stcOpnameDt.ProductCode = this.ProductPicker1.ProductCode ;
            _stcOpnameDt.LocationCode = this.LocationDropDownList.SelectedValue;
            _stcOpnameDt.QtySystem = Convert.ToDecimal(this.QtySystemTextBox.Text);
            _stcOpnameDt.QtyActual = Convert.ToDecimal(this.QtyActualTextBox.Text);
            _stcOpnameDt.QtyOpname = Convert.ToDecimal(this.QtyOpnameTextBox.Text);
            //_stcOpnameDt.Unit = this._productBL.GetUnitCodeByCode(this.ProductDropDownList.SelectedValue);
            _stcOpnameDt.Unit = this._productBL.GetUnitCodeByCode(this.ProductPicker1.ProductCode);
            _stcOpnameDt.Remark = this.RemarkTextBox.Text;

            bool _result = this._stockOpnameBL.AddSTCOpnameDt(_stcOpnameDt);

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
            this.ClearData();
        }


    }
}