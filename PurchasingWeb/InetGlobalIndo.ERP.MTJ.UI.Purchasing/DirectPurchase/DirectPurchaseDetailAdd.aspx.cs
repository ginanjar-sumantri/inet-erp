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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.CustomControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.DirectPurchase
{
    public partial class DirectPurchaseDetailAdd : DirectPurchaseBase
    {
        private DirectPurchaseBL _DirectPurchaseBL = new DirectPurchaseBL();
        private ProductBL _productBL = new ProductBL();
        private PriceGroupBL _priceGroupBL = new PriceGroupBL();
        private UnitBL _unitBL = new UnitBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private SupplierBL _supplierBL = new SupplierBL();
        private CustomerBL _customerBL = new CustomerBL();
        private DirectSalesBL _directSalesBL = new DirectSalesBL();

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
                spawnJS += "document.getElementById('" + this.UnitHiddenField.ClientID + "').value = dataArray [3];\n";
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

            _currCode = _DirectPurchaseBL.GetSingleDirectPurchaseHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).CurrCode;
            this.btnSearchProductCode.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findProduct&configCode=product&paramWhere=PurchaseCurrsamadenganpetik" + _currCode + "petik','_popSearch','width=500,height=300,toolbar=0,location=0,status=0,scrollbars=1')";
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
            //this.UnitTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountTextBox.Attributes.Add("ReadOnly", "True");

            this.QtyOrderTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.QtyOrderTextBox.Attributes.Add("OnBlur", "BlurAmount(" + QtyOrderTextBox.ClientID + "," + PriceTextBox.ClientID + "," + AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PriceTextBox.Attributes.Add("OnBlur", "BlurAmount(" + QtyOrderTextBox.ClientID + "," + PriceTextBox.ClientID + "," + AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        private void ClearData()
        {
            this.ClearLabel();

            this.QtyOrderTextBox.Text = "0";
            this.UnitDropDownList.Items.Clear();
            this.UnitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

            this.PriceTextBox.Text = "0";
            this.AmountTextBox.Text = "0";
            this.ProductCodeTextBox.Text = "";
            this.ProductNameTextBox.Text = "";
            this.WarehouseCodeDropDownList.SelectedValue = "null";
            this.WrhsSubledDropDownList.Enabled = true;
            this.WrhsSubledDropDownList.SelectedValue = "null";
            this.LocationNameDropDownList.SelectedValue = "null";

            PRCTrDirectPurchaseHd _DirectPurchaseHd = this._DirectPurchaseBL.GetSingleDirectPurchaseHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            _decimalPlace = _currencyBL.GetDecimalPlace(_DirectPurchaseHd.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ProductCodeTextBox.Text != "" && this.ProductNameTextBox.Text != "" && this.QtyOrderTextBox.Text != "")
            {
                PRCTrDirectPurchaseDt _PRCTrDirectPurchaseDt = _DirectPurchaseBL.GetSingleDirectPurchaseDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), this.ProductCodeTextBox.Text, this.WarehouseCodeDropDownList.SelectedValue, this.LocationNameDropDownList.SelectedValue);
                if (_PRCTrDirectPurchaseDt == null)
                {
                    PRCTrDirectPurchaseDt _DirectPurchaseDt = new PRCTrDirectPurchaseDt();
                    MsProductConvert _msProductConvert = _DirectPurchaseBL.GetSingleProductConvert(this.ProductCodeTextBox.Text, this.UnitDropDownList.SelectedValue);

                    string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

                    _DirectPurchaseDt.TransNmbr = _transNo;

                    _DirectPurchaseDt.ProductCode = this.ProductCodeTextBox.Text;
                    _DirectPurchaseDt.QtyOrder = Convert.ToDecimal(this.QtyOrderTextBox.Text);
                    _DirectPurchaseDt.UnitOrder = this.UnitDropDownList.SelectedValue;
                    _DirectPurchaseDt.Qty = ((_msProductConvert == null) ? Convert.ToDecimal(this.QtyOrderTextBox.Text) : (_msProductConvert.Rate * Convert.ToDecimal(this.QtyOrderTextBox.Text)));
                    _DirectPurchaseDt.Unit = ((_msProductConvert == null) ? this.UnitDropDownList.SelectedValue : _msProductConvert.UnitConvert);
                    _DirectPurchaseDt.Price = Convert.ToDecimal(this.PriceTextBox.Text);
                    _DirectPurchaseDt.Amount = Convert.ToDecimal(this.AmountTextBox.Text);
                    _DirectPurchaseDt.Remark = this.RemarkTextBox.Text;
                    _DirectPurchaseDt.WrhsCode = this.WarehouseCodeDropDownList.SelectedValue;
                    _DirectPurchaseDt.WrhsFgSubLed = Convert.ToChar(this.FgSubLedHiddenField.Value);
                    _DirectPurchaseDt.WrhsSubLed = (this.WrhsSubledDropDownList.SelectedValue == "null") ? "" : this.WrhsSubledDropDownList.SelectedValue;
                    _DirectPurchaseDt.LocationCode = this.LocationNameDropDownList.SelectedValue;

                    bool _result = this._DirectPurchaseBL.AddDirectPurchaseDt(Convert.ToDecimal(this.AmountTextBox.Text), _DirectPurchaseDt);

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
                SearchBL _searchBL = new SearchBL();
                this.ProductNameTextBox.Text = _searchBL.getProductData(this.ProductCodeTextBox.Text);

                this.UnitDDL(this.ProductCodeTextBox.Text);
                if (this.UnitHiddenField.Value != "" && this.UnitDropDownList.Items.Count != 0)
                {
                    this.UnitDropDownList.SelectedValue = this.UnitHiddenField.Value;
                    this.UnitHiddenField.Value = "";
                }
                else if (this.UnitDropDownList.Items.Count == 0 && this.UnitHiddenField.Value != "")
                {
                    this.UnitDropDownList.Items.Clear();
                    this.UnitDropDownList.Items.Insert(0, new ListItem(this.UnitHiddenField.Value, this.UnitHiddenField.Value));
                    this.UnitHiddenField.Value = "";
                }
            }
        }

        protected void UnitDDL(String _prmProductCode)
        {
            this.UnitDropDownList.Items.Clear();
            this.UnitDropDownList.DataTextField = "UnitCode";
            this.UnitDropDownList.DataValueField = "UnitCode";
            this.UnitDropDownList.DataSource = this._productBL.GetListMsProductConvertForDDL(_prmProductCode);
            this.UnitDropDownList.DataBind();
            //this.UnitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));   
        }
    }
}