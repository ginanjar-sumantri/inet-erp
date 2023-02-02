using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.DirectPurchase
{
    public partial class DirectPurchaseAdd : DirectPurchaseBase
    {
        private DirectPurchaseBL _directPurchaseBL = new DirectPurchaseBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private TermBL _term = new TermBL();
        private PermissionBL _permBL = new PermissionBL();
        private SupplierBL _supplierBL = new SupplierBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private CustomerBL _customerBL = new CustomerBL();
        private PaymentBL _paymentBL = new PaymentBL();
        private ProductBL _productBL = new ProductBL();

        string _imgPPNDate = "ppn_date_start";

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

            if (!this.Page.IsPostBack == true)
            {
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.btnSearchSupplier.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findSupplier&configCode=supplier','_popSearch','width=400,height=550,toolbar=0,location=0,status=0,scrollbars=1')";

                String spawnJS = "<script language='JavaScript'>\n";

                ////////////////////DECLARE FUNCTION FOR CATCHING PRODUCT SEARCH
                spawnJS += "function findProduct(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.productCode.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.getElementById('" + this.txtProductName.ClientID + "').value = dataArray [1];\n";
                spawnJS += "document.getElementById('" + this.PriceTextBox.ClientID + "').value = dataArray [2];\n";
                spawnJS += "document.getElementById('" + this.UnitHidenField.ClientID + "').value = dataArray [3];\n";
                spawnJS += " document.forms[0].submit();\n";
                spawnJS += "}\n";

                ////////////////////DECLARE FUNCTION FOR CATCHING CUSTOMER SEARCH
                spawnJS += "function findSupplier(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.SuppNmbrTextBox.ClientID + "').value = dataArray[0];\n";
                spawnJS += "document.getElementById('" + this.SupplierNameTextBox.ClientID + "').value = dataArray[1];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                ////////////////////DECLARE FUNCTION FOR CALCULATING LINETOTAL
                spawnJS += "function calculateLineTotal(x) {\n";
                spawnJS += "if ( x.value != '' ) {\n";
                spawnJS += "document.getElementById('" + this.LineTotalTextBox.ClientID + "').value = (document.getElementById('" + this.qty.ClientID + "').value * document.getElementById('" + this.PriceTextBox.ClientID + "').value).toFixed(2);\n";
                //spawnJS += "document.getElementById('" + this.txtLineTotal.ClientID + "').value = (document.getElementById('" + this.qty.ClientID + "').value * document.getElementById('" + this.price.ClientID + "').value).toFixed(2);\n";
                spawnJS += "}\n}\n";

                ///////////////////FUNCTION FOR KEYPRESS ENTER ON QTY
                spawnJS += "function enterQty() {\n";
                spawnJS += "document.getElementById('" + this.btnAddLine.ClientID + "').focus();\n";
                spawnJS += "return false;\n";
                spawnJS += "}\n";

                ///////////////////FUNCTION FOR KEYPRESS ENTER ON PRODUCTCODE
                spawnJS += "function enterProductCode() {\n";
                spawnJS += "document.getElementById('" + this.qty.ClientID + "').focus();\n";
                spawnJS += "return false;\n";
                spawnJS += "}\n";

                ////////////////////ONCHANGE DISCOUNT AMOUNT
                spawnJS += "function setDiscAmount() {\n";
                spawnJS += "document.getElementById('" + this.discountValue.ClientID + "').value = Math.round(";
                spawnJS += "document.getElementById('" + this.discount.ClientID + "').value * ";
                spawnJS += "document.getElementById('" + this.BaseForexTextBox.ClientID + "').value / 100 ); \n";
                spawnJS += "document.getElementById('" + this.TotalAmountTextBox.ClientID + "').value = Math.round(";
                spawnJS += "document.getElementById('" + this.BaseForexTextBox.ClientID + "').value - document.getElementById('" + this.discountValue.ClientID + "').value + ";
                spawnJS += "document.getElementById('" + this.PPNAmountTextBox.ClientID + "').value + document.getElementById('" + this.PPHAmountTextBox.ClientID + "').value); \n";
                spawnJS += "}\n";

                ////////////////////ONCHANGE DISCOUNT
                spawnJS += "function setDisc() {\n";
                spawnJS += "if (parseFloat(document.getElementById('" + this.BaseForexTextBox.ClientID + "').value) != 0){\n";
                spawnJS += "document.getElementById('" + this.discount.ClientID + "').value = Math.round(";
                spawnJS += "(parseFloat(document.getElementById('" + this.discountValue.ClientID + "').value) / ";
                spawnJS += "parseFloat(document.getElementById('" + this.BaseForexTextBox.ClientID + "').value)) * 100 ); \n";
                spawnJS += "}\n";
                spawnJS += "else {\n";
                spawnJS += "document.getElementById('" + this.discount.ClientID + "').value = 0; \n";
                spawnJS += "}\n";
                spawnJS += "document.getElementById('" + this.TotalAmountTextBox.ClientID + "').value = Math.round(";
                spawnJS += "parseFloat(document.getElementById('" + this.BaseForexTextBox.ClientID + "').value) - parseFloat(document.getElementById('" + this.discountValue.ClientID + "').value) + ";
                spawnJS += "parseFloat(document.getElementById('" + this.PPNAmountTextBox.ClientID + "').value) + parseFloat(document.getElementById('" + this.PPHAmountTextBox.ClientID + "').value)); \n";
                spawnJS += "}\n";

                ////////////////////ONCHANGE PPN AMOUNT
                spawnJS += "function setPPNAmount() {\n";
                spawnJS += "document.getElementById('" + this.PPNAmountTextBox.ClientID + "').value = Math.round(";
                spawnJS += "document.getElementById('" + this.PPNTextBox.ClientID + "').value * ";
                spawnJS += "(document.getElementById('" + this.BaseForexTextBox.ClientID + "').value - document.getElementById('" + this.discountValue.ClientID + "').value) / 100 ) ; \n";
                spawnJS += "document.getElementById('" + this.TotalAmountTextBox.ClientID + "').value = Math.round(";
                spawnJS += "parseFloat(document.getElementById('" + this.BaseForexTextBox.ClientID + "').value) - parseFloat(document.getElementById('" + this.discountValue.ClientID + "').value) + ";
                spawnJS += "parseFloat(document.getElementById('" + this.PPNAmountTextBox.ClientID + "').value) + parseFloat(document.getElementById('" + this.PPHAmountTextBox.ClientID + "').value)); \n";
                spawnJS += "}\n";

                ////////////////////ONCHANGE PPH AMOUNT
                spawnJS += "function setPPHAmount() {\n";
                spawnJS += "document.getElementById('" + this.PPHAmountTextBox.ClientID + "').value = Math.round(";
                spawnJS += "document.getElementById('" + this.PPHTextBox.ClientID + "').value * ";
                spawnJS += "(document.getElementById('" + this.BaseForexTextBox.ClientID + "').value - document.getElementById('" + this.discountValue.ClientID + "').value) / 100 ) ;\n";
                spawnJS += "document.getElementById('" + this.TotalAmountTextBox.ClientID + "').value = Math.round(";
                spawnJS += "parseFloat(document.getElementById('" + this.BaseForexTextBox.ClientID + "').value) - parseFloat(document.getElementById('" + this.discountValue.ClientID + "').value) + ";
                spawnJS += "parseFloat(document.getElementById('" + this.PPNAmountTextBox.ClientID + "').value) + parseFloat(document.getElementById('" + this.PPHAmountTextBox.ClientID + "').value)); \n";
                spawnJS += "}\n";

                spawnJS += "</script>\n";

                this.javascriptReceiver.Text = spawnJS;

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowWarehouse();
                this.ShowCurr();
                this.ShowPayType();

                this.ShowAttribute();
                this.ClearData();

            }
            this.btnSearchProductCode.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findProduct&configCode=product&paramWhere=PurchaseCurrsamadenganpetik" + this.CurrDropDownList.SelectedValue + "petik','_popSearch','width=500,height=300,toolbar=0,location=0,status=0,scrollbars=1')";

            if (this.boughtItems.Value != "")
                this.showBoughtItem();
            else
                this.perulanganDataDibeli.Text = "";
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.SuppNmbrTextBox.Attributes.Add("ReadOnly", "True");
            this.SupplierNameTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalAmountTextBox.Attributes.Add("ReadOnly", "True");
            this.txtProductName.Attributes.Add("ReadOnly", "True");
            this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
            this.BaseForexTextBox.Attributes.Add("ReadOnly", "True");
            //this.PriceTextBox.Attributes.Add("ReadOnly", "True");
            this.LineTotalTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNAmountTextBox.Attributes.Add("ReadOnly", "True");
            this.PPHAmountTextBox.Attributes.Add("ReadOnly", "True");
            //this.UnitTextBox.Attributes.Add("ReadOnly", "True");

            this.qty.Attributes.Add("onkeyup", "numericInput(this);calculateLineTotal(this)");
            this.qty.Attributes.Add("onkeydown", "if(event.keyCode==13){return enterQty();}");

            this.PriceTextBox.Attributes.Add("onkeyup", "numericInput(this);calculateLineTotal(this)");
            this.PriceTextBox.Attributes.Add("OnBlur", "numericInput(this);calculateLineTotal(this)");
            this.PriceTextBox.Attributes.Add("onkeydown", "if(event.keyCode==13){return enterQty();}");

            this.PPNTextBox.Attributes.Add("onkeyup", "numericInput(this)");
            this.PPNTextBox.Attributes.Add("onkeydown", "if(event.keyCode==13){return setPPNAmount();}");
            this.PPNTextBox.Attributes.Add("onchange", "numericInput(this);setPPNAmount();document.forms[0].submit()");

            this.PPHTextBox.Attributes.Add("onkeyup", "numericInput(this)");
            this.PPHTextBox.Attributes.Add("onkeydown", "if(event.keyCode==13){return setPPHAmount();}");
            this.PPHTextBox.Attributes.Add("onchange", "numericInput(this);setPPHAmount();document.forms[0].submit()");

            this.discount.Attributes.Add("onkeyup", "numericInput(this)");
            this.discount.Attributes.Add("onkeydown", "if(event.keyCode==13){return setDiscAmount();}");
            this.discount.Attributes.Add("onchange", "numericInput(this);setDiscAmount();document.forms[0].submit()");
            this.discountValue.Attributes.Add("onkeyup", "numericInput(this)");
            this.discountValue.Attributes.Add("onkeydown", "if(event.keyCode==13){return setDisc();}");
            this.discountValue.Attributes.Add("onchange", "numericInput(this);setDisc();document.forms[0].submit()");

            this.ForexRateTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.ForexRateTextBox.ClientID + ");");
            this.ForexRateTextBox.Attributes.Add("OnBlur", "return ChangeFormat2(" + this.ForexRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        private void ShowWarehouse()
        {
            this.WarehouseCodeDropDownList.Items.Clear();
            this.WarehouseCodeDropDownList.DataTextField = "WrhsName";
            this.WarehouseCodeDropDownList.DataValueField = "WrhsName";
            this.WarehouseCodeDropDownList.DataSource = this._warehouseBL.GetListForDDLActive();
            this.WarehouseCodeDropDownList.DataBind();
            this.WarehouseCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowPayType()
        {
            this.PayTypeDropDownList.Items.Clear();
            this.PayTypeDropDownList.DataTextField = "PayName";
            this.PayTypeDropDownList.DataValueField = "PayCode";
            this.PayTypeDropDownList.DataSource = this._paymentBL.GetListDDLDPSuppPay(this.CurrDropDownList.SelectedValue);
            this.PayTypeDropDownList.DataBind();
            this.PayTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowLocation()
        {
            this.LocationNameDropDownList.Items.Clear();
            this.LocationNameDropDownList.DataTextField = "WLocationName";
            this.LocationNameDropDownList.DataValueField = "WLocationName";
            this.LocationNameDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByNameForDDL(this.WarehouseCodeDropDownList.SelectedValue);
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

        public void ShowCurr()
        {
            this.CurrDropDownList.Items.Clear();
            this.CurrDropDownList.DataTextField = "CurrCode";
            this.CurrDropDownList.DataValueField = "CurrCode";
            this.CurrDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrDropDownList.DataBind();
            this.CurrDropDownList.SelectedValue = _currencyBL.GetCurrDefault();
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

        protected void WarehouseCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.WarehouseCodeDropDownList.SelectedValue != "null")
            {
                //this.UnitDDL();
                //if (this.UnitDropDownList.Items.Count != 0)
                //    this.UnitDropDownList.SelectedValue = this.UnitHidenField.Value;
                //else
                //    this.UnitDropDownList.Items.Insert(0, new ListItem(this.UnitHidenField.Value, this.UnitHidenField.Value));

                //this.UnitHidenField.Value = "";

                char _fgSubled = _warehouseBL.GetWarehouseFgSubledByName(this.WarehouseCodeDropDownList.SelectedValue);

                this.ShowLocation();

                if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
                {
                    this.WrhsSubledDropDownList.Items.Clear();
                    this.WrhsSubledDropDownList.Enabled = false;
                    this.WrhsSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
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

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ClearLabel();

            this.UnitDropDownList.Items.Clear();
            this.UnitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

            this.WrhsSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.LocationNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            this.boughtItems.Value = "";
            this.SupplierNameTextBox.Text = "";
            this.SuppNmbrTextBox.Text = "";
            this.BaseForexTextBox.Text = "0";
            this.discount.Text = "0";
            this.discountValue.Text = "0";
            this.PPNTextBox.Text = "0";
            this.PPNAmountTextBox.Text = "0";
            this.PPHTextBox.Text = "0";
            this.PPHAmountTextBox.Text = "0";
            this.TotalAmountTextBox.Text = "0";
            this.ForexRateTextBox.Text = "1";
            this.Remark.Text = "";
            this.PayTypeDropDownList.SelectedValue = "null";
            this.TransTypeDDL.SelectedValue = "CS";
        }

        private void SetCurrRate()
        {
            byte _decimalPlace = _currencyBL.GetDecimalPlace(this.CurrDropDownList.SelectedValue);
            string _currDefault = _currencyBL.GetCurrDefault();
            this.DecimalPlaceHiddenField.Value = _decimalPlace.ToString();

            if (this.CurrDropDownList.SelectedValue == _currDefault)
            {
                this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                this.ForexRateTextBox.Attributes.Add("Style", "background-color:#CCCCCC;");
                this.ForexRateTextBox.Text = "1";
            }
            else
            {
                this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                this.ForexRateTextBox.Attributes.Add("Style", "background-color:#FFFFFF;");
                this.ForexRateTextBox.Text = _currencyRateBL.GetSingleLatestCurrRate(this.CurrDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
        }

        protected void CurrDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CurrDropDownList.SelectedValue != "null")
            {
                this.SetCurrRate();
                this.ShowPayType();
                this.boughtItems.Value = "";
                this.BaseForexTextBox.Text = "0";
                this.discount.Text = "0";
                this.discountValue.Text = "0";
                this.PPNTextBox.Text = "0";
                this.PPNAmountTextBox.Text = "0";
                this.PPHTextBox.Text = "0";
                this.PPHAmountTextBox.Text = "0";
                this.TotalAmountTextBox.Text = "0";
            }
        }

        protected void btnAddLine_Click(object sender, EventArgs e)
        {
            if (this.WarehouseCodeDropDownList.SelectedValue != "null" && this.LocationNameDropDownList.SelectedValue != "null")
            {
                if (this.itemCount.Value != "0")
                {
                    String[] _rowDetil = this.boughtItems.Value.Split('^');

                    foreach (String _dataDetil in _rowDetil)
                    {
                        String[] _rowData = _dataDetil.Split('|');
                        PRCTrDirectPurchaseDt _addTrDirectPurchase = new PRCTrDirectPurchaseDt();

                        if (_rowData[1] == this.productCode.Text && _rowData[6] == this.WarehouseCodeDropDownList.SelectedValue && _rowData[8] == this.LocationNameDropDownList.SelectedValue)
                        {
                            this.WarningLabel.Text = "ProductCode, Warehouse & Warehouse Location has been add";
                            return;
                        }
                    }

                }
                if (this.txtProductName.Text != "" && this.qty.Text != "")
                {
                    this.itemCount.Value = (Convert.ToInt16(this.itemCount.Value) + 1).ToString();

                    Decimal _subTotal = this.LineTotalHiddenField.Value == "" ? 0 : Convert.ToDecimal(this.LineTotalHiddenField.Value);
                    if (this.itemCount.Value == "1")
                    {
                        this.boughtItems.Value = this.itemCount.Value + "|" + this.productCode.Text + "|";
                        this.boughtItems.Value += this.txtProductName.Text + "|";
                        this.boughtItems.Value += ((this.WarehouseCodeDropDownList.SelectedValue == "null") ? "" : this.WarehouseCodeDropDownList.Text) + "|" + (((this.WrhsSubledDropDownList.SelectedValue) == "null") ? "" : this.WrhsSubledDropDownList.Text) + "|" + ((this.LocationNameDropDownList.SelectedValue == "null") ? "" : this.LocationNameDropDownList.Text) + "|";
                        this.boughtItems.Value += this.qty.Text + "|" + this.UnitDropDownList.SelectedValue + "|" + this.PriceTextBox.Text + "|" + this.LineTotalTextBox.Text + "|";
                        this.boughtItems.Value += this.RemarkTextBox.Text;
                        _subTotal += Convert.ToDecimal(this.LineTotalTextBox.Text);
                    }
                    else
                    {
                        this.boughtItems.Value += "^" + this.itemCount.Value + "|" + this.productCode.Text + "|";
                        this.boughtItems.Value += this.txtProductName.Text + "|";
                        this.boughtItems.Value += ((this.WarehouseCodeDropDownList.SelectedValue == "null") ? "" : this.WarehouseCodeDropDownList.Text) + "|" + (((this.WrhsSubledDropDownList.SelectedValue) == "null") ? "" : this.WrhsSubledDropDownList.Text) + "|" + ((this.LocationNameDropDownList.SelectedValue == "null") ? "" : this.LocationNameDropDownList.Text) + "|";
                        this.boughtItems.Value += this.qty.Text + "|" + this.UnitDropDownList.SelectedValue + "|" + this.PriceTextBox.Text + "|" + this.LineTotalTextBox.Text + "|";
                        this.boughtItems.Value += this.RemarkTextBox.Text;
                        _subTotal += Convert.ToDecimal(this.LineTotalTextBox.Text);
                    }

                    this.LineTotalHiddenField.Value = _subTotal.ToString();
                    this.productCode.Text = "";
                    this.productCode.Focus();
                    this.productName.Value = "";
                    this.txtProductName.Text = "";
                    this.PriceTextBox.Text = "0";
                    this.PriceTextBox.Text = "";
                    this.qty.Text = "";
                    this.LineTotalTextBox.Text = "0";
                    this.UnitDropDownList.Items.Clear();
                    this.UnitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                    //this.UnitTextBox.Text = "";
                    //this.Remark.Text = "";
                    this.BaseForexTextBox.Text = _subTotal.ToString();
                    this.ShowWarehouse();
                    this.WrhsSubledDropDownList.SelectedValue = "null";
                    this.LocationNameDropDownList.SelectedValue = "null";
                    this.WrhsSubledDropDownList.Enabled = false;
                    this.showBoughtItem();
                }
            }
            else
            {
                this.WarningLabel.Text = "Warehouse & Warehouse Location Must Be Select";
            }
        }
        protected void showBoughtItem()
        {
            String _strGenerateTable = "";
            String[] _dataItem = this.boughtItems.Value.Split('^');
            Decimal _subTotal = 0;
            foreach (String _dataRow in _dataItem)
            {
                _strGenerateTable += "<tr height='20px'>";
                String[] _dataField = _dataRow.Split('|');
                foreach (String _data in _dataField)
                {
                    _strGenerateTable += "<td>" + _data + "</td>";
                }
                if (this.postingState.Value == "1")
                {
                    _strGenerateTable += "<td></td></tr>";
                }
                else
                {
                    _strGenerateTable += "<td><input type='button' onClick='deleteItem(\"" + this.boughtItems.ClientID + "\"," + _dataField[0] + ",\"" + this.itemCount.ClientID + "\",\"" + this.BaseForexTextBox.ClientID + "\",\"" + this.discount.ClientID + "\",\"" + this.discountValue.ClientID + "\",\"" + this.PPNTextBox.ClientID + "\",\"" + this.PPNAmountTextBox.ClientID + "\",\"" + this.PPHTextBox.ClientID + "\",\"" + this.PPHAmountTextBox.ClientID + "\",\"" + this.TotalAmountTextBox.ClientID + "\");' value='Delete'></td></tr>";
                }
                _subTotal += Convert.ToDecimal(_dataField[9]);
            }
            byte _decimalPlace = _currencyBL.GetDecimalPlace(this.CurrDropDownList.SelectedValue);

            this.BaseForexTextBox.Text = _subTotal.ToString();
            this.perulanganDataDibeli.Text = _strGenerateTable;
            this.discountValue.Text = (Convert.ToDecimal(this.BaseForexTextBox.Text) * ((this.discount.Text == "") ? 0 : Convert.ToDecimal(this.discount.Text)) / 100).ToString();
            //this.discount.Text = Math.Round(Convert.ToDecimal(this.discountValue.Text) / Convert.ToDecimal(this.BaseForexTextBox.Text) * 100, 2).ToString("#,##0.##");
            this.PPNAmountTextBox.Text = ((Convert.ToDecimal(this.BaseForexTextBox.Text) - (Convert.ToDecimal(this.discountValue.Text))) * Convert.ToDecimal(this.PPNTextBox.Text) / 100).ToString();
            this.PPHAmountTextBox.Text = ((Convert.ToDecimal(this.BaseForexTextBox.Text) - (Convert.ToDecimal(this.discountValue.Text))) * Convert.ToDecimal(this.PPHTextBox.Text) / 100).ToString();
            this.TotalAmountTextBox.Text = ((_subTotal - (Convert.ToDecimal(this.discountValue.Text))) + Convert.ToDecimal(this.PPNAmountTextBox.Text) + Convert.ToDecimal(this.PPHAmountTextBox.Text)).ToString();
        }

        protected void productCode_TextChanged(object sender, EventArgs e)
        {
            String[] _productData = _productBL.GetProductByCodeAndCurr(this.productCode.Text, this.CurrDropDownList.SelectedValue).Split('|');

            if (_productData.Length > 0 && _productData[0] != "")
            {
                this.txtProductName.Text = _productData[0];
                this.productName.Value = _productData[0];
                //this.PriceTextBox.Text = Convert.ToDecimal(_productData[2]).ToString("#,##0.##");
                //this.UnitTextBox.Text = _productData[1];
                this.UnitDDL();
                if (this.UnitDropDownList.Items.Count != 0)
                    this.UnitDropDownList.SelectedValue = _productData[1];
                else
                    this.UnitDropDownList.Items.Insert(0, new ListItem(_productData[1], _productData[1]));
                this.qty.Focus();
            }
        }

        protected void UnitDDL()
        {
            this.UnitDropDownList.ClearSelection();
            this.UnitDropDownList.Items.Clear();
            this.UnitDropDownList.DataTextField = "UnitCode";
            this.UnitDropDownList.DataValueField = "UnitCode";
            this.UnitDropDownList.DataSource = this._productBL.GetListMsProductConvertForDDL(this.productCode.Text);
            this.UnitDropDownList.DataBind();
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            bool _in = false;

            if (this.TransTypeDDL.SelectedValue == "CS")
            {
                if (this.PayTypeDropDownList.SelectedValue != "null")
                {
                    _in = true;
                }
            }
            else
            {
                _in = true;
            }

            if (_in)
            {
                if (this.boughtItems.Value != "")
                {
                    PRCTrDirectPurchaseHd _prcTrDirectPurchaseHd = new PRCTrDirectPurchaseHd();

                    DateTime _transDate = DateFormMapper.GetValue(this.DateTextBox.Text);

                    _prcTrDirectPurchaseHd.TransDate = Convert.ToDateTime(_transDate);
                    _prcTrDirectPurchaseHd.Status = DirectPurchaseDataMapper.GetStatusByte(TransStatus.OnHold);
                    _prcTrDirectPurchaseHd.SuppCode = this.SuppNmbrTextBox.Text;
                    _prcTrDirectPurchaseHd.FileNmbr = "";
                    _prcTrDirectPurchaseHd.CurrCode = this.CurrDropDownList.Text;
                    _prcTrDirectPurchaseHd.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
                    _prcTrDirectPurchaseHd.TransType = this.TransTypeDDL.SelectedValue;
                    if (this.TransTypeDDL.SelectedValue == "CS")
                    {
                        _prcTrDirectPurchaseHd.PayCode = this.PayTypeDropDownList.SelectedValue;
                    }
                    _prcTrDirectPurchaseHd.BaseForex = Convert.ToDecimal(this.BaseForexTextBox.Text);
                    _prcTrDirectPurchaseHd.Disc = Convert.ToDecimal(this.discount.Text);
                    _prcTrDirectPurchaseHd.DiscForex = Convert.ToDecimal(this.discountValue.Text);
                    _prcTrDirectPurchaseHd.PPN = Convert.ToDecimal(this.PPNTextBox.Text);
                    _prcTrDirectPurchaseHd.PPNForex = Convert.ToDecimal(this.PPNAmountTextBox.Text);
                    _prcTrDirectPurchaseHd.PPh = Convert.ToDecimal(this.PPHTextBox.Text);
                    _prcTrDirectPurchaseHd.PPhForex = Convert.ToDecimal(this.PPHAmountTextBox.Text);
                    _prcTrDirectPurchaseHd.TotalForex = Convert.ToDecimal(this.TotalAmountTextBox.Text);
                    _prcTrDirectPurchaseHd.CreatedBy = HttpContext.Current.User.Identity.Name;
                    _prcTrDirectPurchaseHd.CreatedDate = DateTime.Now;
                    _prcTrDirectPurchaseHd.EditBy = HttpContext.Current.User.Identity.Name;
                    _prcTrDirectPurchaseHd.EditDate = DateTime.Now;
                    _prcTrDirectPurchaseHd.Remark = this.Remark.Text;

                    String _generatedTransNmbr = _directPurchaseBL.Add(_prcTrDirectPurchaseHd, this.boughtItems.Value);

                    if (_generatedTransNmbr != "")
                    {
                        Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_generatedTransNmbr, ApplicationConfig.EncryptionKey)));
                    }
                }
                else
                {
                    this.WarningLabel.Text = "Please Fill the Detail";
                }
            }
            else
            {
                this.WarningLabel.Text = "Please choice Payment Type";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        protected void TransTypeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.TransTypeDDL.SelectedValue == "CS")
            {
                this.PayTypeDropDownList.Enabled = true;
            }
            else
            {
                this.PayTypeDropDownList.Enabled = false;
            }
        }

        //protected void UnitHidenField_ValueChanged(object sender, EventArgs e)
        //{
        //    this.UnitDDL();
        //    if (this.UnitDropDownList.Items.Count != 0)
        //        this.UnitDropDownList.SelectedValue = this.UnitHidenField.Value;
        //    else
        //        this.UnitDropDownList.Items.Insert(0, new ListItem(this.UnitHidenField.Value, this.UnitHidenField.Value));

        //    this.UnitHidenField.Value = "";
        //}
    }
}