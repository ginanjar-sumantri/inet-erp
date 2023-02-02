using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.CustomControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.DirectSales
{
    public partial class DirectSalesDetailAdd : DirectSalesBase
    {
        private DirectSalesBL _directSalesBL = new DirectSalesBL();
        private ProductBL _productBL = new ProductBL();
        private PriceGroupBL _priceGroupBL = new PriceGroupBL();
        private UnitBL _unitBL = new UnitBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private SupplierBL _supplierBL = new SupplierBL();
        private CustomerBL _customerBL = new CustomerBL();
        private DirectPurchaseBL _directPurchaseBL = new DirectPurchaseBL();
        private POSBL _posBL = new POSBL();

        private byte _decimalPlace = 0;
        private String _currCode = "";
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
                String spawnJS = "<script language='JavaScript'>\n";

                ////////////////////DECLARE FUNCTION FOR CATCHING PRODUCT SEARCH
                spawnJS += "function findProduct(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.ProductCodeTextBox.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.getElementById('" + this.ProductNameTextBox.ClientID + "').value = dataArray [1];\n";
                spawnJS += "document.getElementById('" + this.PriceTextBox.ClientID + "').value = dataArray [2];\n";
                spawnJS += "document.getElementById('" + this.UnitHiddenField.ClientID + "').value = dataArray [4];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                spawnJS += "</script>\n";
                this.javascriptReceiver.Text = spawnJS;


                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.WrhsSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.LocationNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

                this.ShowWarehouse();
                this.ClearData();
                this.SetAttribute();
            }

            _currCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeCurrKey), ApplicationConfig.EncryptionKey);
            this.btnSearchProductCode.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findProduct&configCode=productWithSalePrice&paramWhere=B.CurrCodesamadenganpetik" + _currCode + "petik','_popSearch','width=500,height=300,toolbar=0,location=0,status=0,scrollbars=1')";

        }

        public void ShowSupp()
        {
            this.WrhsSubledDropDownList.Items.Clear();
            this.WrhsSubledDropDownList.DataTextField = "SuppName";
            this.WrhsSubledDropDownList.DataValueField = "SuppCode";
            this.WrhsSubledDropDownList.DataSource = this._supplierBL.GetListDDLSupp();
            this.WrhsSubledDropDownList.DataBind();
            this.WrhsSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowWarehouse()
        {
            this.WarehouseCodeDropDownList.DataTextField = "WrhsName";
            this.WarehouseCodeDropDownList.DataValueField = "WrhsCode";
            this.WarehouseCodeDropDownList.DataSource = this._warehouseBL.GetListForDDLActive();
            this.WarehouseCodeDropDownList.DataBind();
            this.WarehouseCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowLocation()
        {
            this.LocationNameDropDownList.Items.Clear();
            this.LocationNameDropDownList.DataTextField = "WLocationName";
            this.LocationNameDropDownList.DataValueField = "WLocationCode";
            this.LocationNameDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(this.WarehouseCodeDropDownList.SelectedValue);
            this.LocationNameDropDownList.DataBind();
            this.LocationNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowCust()
        {
            this.WrhsSubledDropDownList.Items.Clear();
            this.WrhsSubledDropDownList.DataTextField = "CustName";
            this.WrhsSubledDropDownList.DataValueField = "CustCode";
            this.WrhsSubledDropDownList.DataSource = this._customerBL.GetListCustomerForDDL();
            this.WrhsSubledDropDownList.DataBind();
            this.WrhsSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribute()
        {
            //this.PriceTextBox.Attributes.Add("ReadOnly", "True");

            this.AmountTextBox.Attributes.Add("ReadOnly", "True");

            this.QtyOrderTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.QtyOrderTextBox.Attributes.Add("OnBlur", "BlurAmount(" + QtyOrderTextBox.ClientID + "," + PriceTextBox.ClientID + "," + AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PriceTextBox.Attributes.Add("OnBlur", "BlurAmount(" + QtyOrderTextBox.ClientID + "," + PriceTextBox.ClientID + "," + AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        private void ClearData()
        {
            this.ClearLabel();

            this.QtyOrderTextBox.Text = "0";

            this.PriceTextBox.Text = "0";
            this.AmountTextBox.Text = "0";
            this.ProductCodeTextBox.Text = "";
            this.ProductNameTextBox.Text = "";
            this.WarehouseCodeDropDownList.SelectedValue = "null";
            this.WrhsSubledDropDownList.Enabled = true;
            this.WrhsSubledDropDownList.SelectedValue = "null";
            this.LocationNameDropDownList.SelectedValue = "null";

            this.UnitDropDownList.Items.Clear();
            this.UnitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

            SALTrDirectSalesHd _directSalesHd = this._directSalesBL.GetSingleDirectSalesHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            _decimalPlace = _currencyBL.GetDecimalPlace(_directSalesHd.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ProductCodeTextBox.Text != "" && this.ProductNameTextBox.Text != "" && this.QtyOrderTextBox.Text != "")
            {
                SALTrDirectSalesDt _salTrDirectSalesDt = _directSalesBL.GetSingleDirectSalesDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), this.ProductCodeTextBox.Text, this.WarehouseCodeDropDownList.SelectedValue, this.LocationNameDropDownList.SelectedValue);
                if (_salTrDirectSalesDt == null)
                {
                    //String _unit = _productBL.GetSingleProduct(this.ProductCodeTextBox.Text).Unit;
                    SALTrDirectSalesDt _directSalesDt = new SALTrDirectSalesDt();
                    MsProductConvert _msProductConvert = _directPurchaseBL.GetSingleProductConvert(this.ProductCodeTextBox.Text, this.UnitDropDownList.SelectedValue);

                    string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

                    _directSalesDt.TransNmbr = _transNo;

                    _directSalesDt.ProductCode = this.ProductCodeTextBox.Text;
                    _directSalesDt.QtyOrder = Convert.ToDecimal(this.QtyOrderTextBox.Text);
                    _directSalesDt.UnitOrder = this.UnitDropDownList.SelectedValue;
                    _directSalesDt.Qty = ((_msProductConvert==null )? Convert.ToDecimal(this.QtyOrderTextBox.Text): (Convert.ToDecimal(this.QtyOrderTextBox.Text)*_msProductConvert.Rate));
                    _directSalesDt.Unit = ((_msProductConvert == null) ? this.UnitDropDownList.SelectedValue : _msProductConvert.UnitConvert);
                    _directSalesDt.Price = Convert.ToDecimal(this.PriceTextBox.Text);
                    _directSalesDt.Amount = Convert.ToDecimal(this.AmountTextBox.Text);
                    _directSalesDt.WrhsCode = this.WarehouseCodeDropDownList.SelectedValue;
                    _directSalesDt.WrhsFgSubLed = Convert.ToChar(this.FgSubLedHiddenField.Value);
                    _directSalesDt.WrhsSubLed = (this.WrhsSubledDropDownList.SelectedValue == "null") ? "" : this.WrhsSubledDropDownList.SelectedValue;
                    _directSalesDt.WLocationCode = this.LocationNameDropDownList.SelectedValue;

                    bool _result = this._directSalesBL.AddDirectSales(Convert.ToDecimal(this.AmountTextBox.Text), _directSalesDt);

                    if (_result == true)
                    {
                        Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                    }
                    else
                    {
                        this.WarningLabel.Text = "Your Failed Add Data";
                    }
                }
                else 
                {
                    this.WarningLabel.Text = "Product Code & Warehouse has been inputted";
                }
            }
            else
            {
                this.WarningLabel.Text = "Please Insert Product Code & Product Name";
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

        protected void WarehouseCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.WarehouseCodeDropDownList.SelectedValue != "null")
            {
                char _fgSubled = _warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseCodeDropDownList.SelectedValue);
                this.FgSubLedHiddenField.Value = _fgSubled.ToString();
                this.ShowLocation();

                if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
                {
                    this.WrhsSubledDropDownList.Enabled = false;
                    this.WrhsSubledDropDownList.SelectedValue = "null";
                }
                else
                {
                    this.WrhsSubledDropDownList.Enabled = true;
                    if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                    {
                        this.ShowCust();
                    }
                    else if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                    {
                        this.ShowSupp();
                    }
                }
            }
            else
            {
                this.LocationNameDropDownList.Items.Clear();
                this.LocationNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.LocationNameDropDownList.SelectedValue = "null";
                this.WrhsSubledDropDownList.Items.Clear();
                this.WrhsSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.WrhsSubledDropDownList.SelectedValue = "null";

            }
        }
        protected void ProductCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.ProductCodeTextBox.Text != "")
            {
                //SearchBL _searchBL = new SearchBL();

                String[] _productData = new String[4];
                if (this.UnitHiddenField.Value == "")
                {
                    _productData = _posBL.getProductData(this.ProductCodeTextBox.Text, _currCode).Split('|');
                }
                else
                {
                    _productData = _posBL.getProductData(this.ProductCodeTextBox.Text, _currCode, this.UnitHiddenField.Value).Split('|');
                    this.UnitHiddenField.Value = "";
                }

                if (_productData.Length > 0 && _productData[0] != "")
                {
                    this.ProductNameTextBox.Text = _productData[0];
                    this.UnitDDL(this.ProductCodeTextBox.Text);

                    if (this.UnitDropDownList.Items.Count != 0)
                        this.UnitDropDownList.SelectedValue = _productData[1];
                    else
                        this.UnitDropDownList.Items.Insert(0, new ListItem(_productData[1], _productData[1]));

                    this.SalesPrice();
                }
            }
        }

        protected void UnitDDL(String _prmProductCode) 
        {
            this.UnitDropDownList.Items.Clear();
            this.UnitDropDownList.DataTextField = "UnitCode";
            this.UnitDropDownList.DataValueField = "UnitCode";
            this.UnitDropDownList.DataSource = this._directSalesBL.GetUnitForDDL(_prmProductCode, _currCode);
            this.UnitDropDownList.DataBind();
            //this.UnitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));   
        }

        private void SalesPrice() 
        {
            this.PriceTextBox.Text = _directSalesBL.GetPriceForDDL(this.ProductCodeTextBox.Text, this.UnitDropDownList.SelectedValue, _currCode).ToString("0.00");
        }
        protected void UnitDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SalesPrice();
        }
}
}