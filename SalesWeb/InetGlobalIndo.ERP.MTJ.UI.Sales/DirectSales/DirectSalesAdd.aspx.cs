using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.DirectSales
{
    public partial class DirectSalesAdd : DirectSalesBase
    {
        private CustomerBL _cust = new CustomerBL();
        private CurrencyBL _currency = new CurrencyBL();
        private CurrencyRateBL _currencyRate = new CurrencyRateBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private TermBL _term = new TermBL();
        private SalesOrderBL _salesOrder = new SalesOrderBL();
        private PermissionBL _permBL = new PermissionBL();
        private SupplierBL _supplierBL = new SupplierBL();
        private CustomerBL _customerBL = new CustomerBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private PaymentBL _paymentBL = new PaymentBL();
        private ProductBL _productBL = new ProductBL();
        private POSBL _posBL = new POSBL();

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

                this.btnSearchCustomer.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findCustomer&configCode=customer','_popSearch','width=1200,height=1000,toolbar=0,location=0,status=0,scrollbars=1')";

                String spawnJS = "<script language='JavaScript'>\n";

                ////////////////////DECLARE FUNCTION FOR CATCHING PRODUCT SEARCH
                spawnJS += "function findProduct(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.productCode.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.getElementById('" + this.txtProductName.ClientID + "').value = dataArray [1];\n";
                spawnJS += "document.getElementById('" + this.PriceTextBox.ClientID + "').value = dataArray [2];\n";
                spawnJS += "document.getElementById('" + this.UnitHiddenField.ClientID + "').value = dataArray [4];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                ////////////////////DECLARE FUNCTION FOR CATCHING CUSTOMER SEARCH
                spawnJS += "function findCustomer(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.CustNmbrTextBox.ClientID + "').value = dataArray[0];\n";
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
                spawnJS += "document.getElementById('" + this.BaseForexTextBox.ClientID + "').value / 100 ) ;";
                spawnJS += "}\n";

                ////////////////////ONCHANGE DISCOUNT AMOUNT
                spawnJS += "function setfee() {\n";
                spawnJS += "document.getElementById('" + this.discountValue.ClientID + "').value = Math.round(";
                spawnJS += "document.getElementById('" + this.discount.ClientID + "').value * ";
                spawnJS += "document.getElementById('" + this.BaseForexTextBox.ClientID + "').value / 100 ) ;";
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

                this.WrhsSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.LocationNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

                this.ShowAttribute();
                this.ClearData();

            }

            this.btnSearchProductCode.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findProduct&configCode=productWithSalePrice&paramWhere=B.CurrCodesamadenganpetik" + this.CurrDropDownList.SelectedValue + "petik','_popSearch','width=500,height=300,toolbar=0,location=0,status=0,scrollbars=1')";

            if (this.boughtItems.Value != "")
                this.showBoughtItem();
            else
                this.perulanganDataDibeli.Text = "";

            if (this.EditRowHiddenField.Value != "")
            {
                String _strGenerateTable = "";
                //Decimal _subTotal = 0;
                String[] _BoughtItems = this.boughtItems.Value.Split('^');
                //String[] _editData = _BoughtItems[Convert.ToInt16(this.EditRowHiddenField.Value)].Split('|');
                foreach (String _dataRow in _BoughtItems)
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
                        _strGenerateTable += "<td></td></tr>";
                    }
                    if (this.EditRowHiddenField.Value == _dataField[0])
                    {
                        this.productCode.Text = _dataField[1];
                        this.productName.Value = _dataField[2];
                        this.txtProductName.Text = _dataField[2];
                        this.qty.Text = _dataField[3];
                        this.UnitDDL();
                        if (this.UnitDropDownList.Items.Count != 0)
                            this.UnitDropDownList.SelectedValue = _dataField[4];
                        else
                            this.UnitDropDownList.Items.Insert(0, new ListItem(_dataField[4], _dataField[4]));

                        this.PriceTextBox.Text = _dataField[5];
                        this.LineTotalHiddenField.Value = _dataField[6];
                        this.LineTotalTextBox.Text = _dataField[6];
                        this.WarehouseCodeDropDownList.SelectedValue = _dataField[7];
                        WarehouseCodeDropDownListSelectedIndexChanged();
                        this.EditPostBackHiddenField.Value = this.EditRowHiddenField.Value;
                        this.EditRowHiddenField.Value = "";
                    }

                }
                //this.BaseForexTextBox.Text = _subTotal.ToString();
                this.perulanganDataDibeli.Text = _strGenerateTable;
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.CustNmbrTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalAmountTextBox.Attributes.Add("ReadOnly", "True");
            this.txtProductName.Attributes.Add("ReadOnly", "True");
            this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
            this.BaseForexTextBox.Attributes.Add("ReadOnly", "True");
            //this.PriceTextBox.Attributes.Add("ReadOnly", "True");
            this.LineTotalTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNAmountTextBox.Attributes.Add("ReadOnly", "True");

            this.qty.Attributes.Add("onkeyup", "numericInput(this);calculateLineTotal(this)");
            this.qty.Attributes.Add("onkeydown", "if(event.keyCode==13){return enterQty();}");

            this.PriceTextBox.Attributes.Add("onkeyup", "numericInput(this);calculateLineTotal(this)");
            this.PriceTextBox.Attributes.Add("OnBlur", "numericInput(this);calculateLineTotal(this)");
            this.PriceTextBox.Attributes.Add("onkeydown", "if(event.keyCode==13){return enterQty();}");

            this.StampFeeTextBox.Attributes.Add("onkeyup", "numericInput(this)");
            this.StampFeeTextBox.Attributes.Add("onchange", "numericInput(this);document.forms[0].submit()");

            this.OtherFeeTextBox.Attributes.Add("onkeyup", "numericInput(this)");
            this.OtherFeeTextBox.Attributes.Add("onchange", "numericInput(this);document.forms[0].submit()");

            this.tax.Attributes.Add("onkeyup", "numericInput(this)");
            this.tax.Attributes.Add("onchange", "numericInput(this);document.forms[0].submit()");

            this.discount.Attributes.Add("onkeyup", "numericInput(this)");
            this.discount.Attributes.Add("onkeydown", "if(event.keyCode==13){return setDiscAmount();}");
            this.discount.Attributes.Add("onchange", "numericInput(this);setDiscAmount();document.forms[0].submit()");
            this.discountValue.Attributes.Add("onkeyup", "numericInput(this)");
            this.discountValue.Attributes.Add("onchange", "numericInput(this);document.forms[0].submit()");

            this.ForexRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.BaseForexTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.PPNAmountTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.ForexRateTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.ForexRateTextBox.ClientID + ");");
        }

        private void ShowPayType()
        {
            this.PayTypeDropDownList.Items.Clear();
            this.PayTypeDropDownList.DataTextField = "PayName";
            this.PayTypeDropDownList.DataValueField = "PayCode";
            this.PayTypeDropDownList.DataSource = this._paymentBL.GetListDDLDPCustomer(this.CurrDropDownList.SelectedValue);
            this.PayTypeDropDownList.DataBind();
            this.PayTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowWarehouse()
        {
            this.WarehouseCodeDropDownList.DataTextField = "WrhsName";
            this.WarehouseCodeDropDownList.DataValueField = "WrhsName";
            this.WarehouseCodeDropDownList.DataSource = this._warehouseBL.GetListForDDLActive();
            this.WarehouseCodeDropDownList.DataBind();
            this.WarehouseCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
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

        protected void UnitDDL()
        {
            this.UnitDropDownList.ClearSelection();
            this.UnitDropDownList.Items.Clear();
            this.UnitDropDownList.DataTextField = "UnitCode";
            this.UnitDropDownList.DataValueField = "UnitCode";
            this.UnitDropDownList.DataSource = this._productBL.GetListMsProductConvertForDDL(this.productCode.Text);
            this.UnitDropDownList.DataBind();
        }

        protected void WarehouseCodeDropDownListSelectedIndexChanged()
        {
            if (this.WarehouseCodeDropDownList.SelectedValue != "null")
            {
                char _fgSubled = _warehouseBL.GetWarehouseFgSubledByName(this.WarehouseCodeDropDownList.SelectedValue);

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

        protected void UnitDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.UnitDropDownList.SelectedValue != null && this.productCode.Text != "" && this.CurrDropDownList.SelectedValue != null)
            {
                this.PriceTextBox.Text = _posBL.getPriceData(this.productCode.Text, this.CurrDropDownList.SelectedValue, this.UnitDropDownList.SelectedValue).ToString("");
            }
        }

        protected void WarehouseCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

            WarehouseCodeDropDownListSelectedIndexChanged();
            //if (this.WarehouseCodeDropDownList.SelectedValue != "null")
            //{
            //    char _fgSubled = _warehouseBL.GetWarehouseFgSubledByName(this.WarehouseCodeDropDownList.SelectedValue);

            //    this.ShowLocation();

            //    if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            //    {
            //        this.WrhsSubledDropDownList.Enabled = false;
            //        this.WrhsSubledDropDownList.SelectedValue = "null";
            //    }
            //    else
            //    {
            //        this.WrhsSubledDropDownList.Enabled = true;
            //        if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            //        {
            //            this.ShowCust();
            //        }
            //        else if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
            //        {
            //            this.ShowSupp();
            //        }
            //    }
            //}
            //else
            //{
            //    this.LocationNameDropDownList.Items.Clear();
            //    this.LocationNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            //    this.LocationNameDropDownList.SelectedValue = "null";
            //    this.WrhsSubledDropDownList.Items.Clear();
            //    this.WrhsSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            //    this.WrhsSubledDropDownList.SelectedValue = "null";

            //}
        }

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ClearLabel();

            this.UnitDropDownList.Items.Clear();
            this.UnitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

            this.DateTextBox.Text = DateFormMapper.GetValue(now);

            this.ForexRateTextBox.Text = "1";
        }

        private void SetCurrRate()
        {
            byte _decimalPlace = _currencyBL.GetDecimalPlace(this.CurrDropDownList.SelectedValue);
            this.ForexRateTextBox.Text = _currencyRateBL.GetSingleLatestCurrRate(this.CurrDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            if (this.ForexRateTextBox.Text == "0") this.ForexRateTextBox.Text = "1";

            MsCurrency _msCurrency = this._currencyBL.GetSingle(this.CurrDropDownList.SelectedValue);
            if (_msCurrency.FgHome == 'N')
            {
                this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                this.ForexRateTextBox.Attributes.Remove("Style");
            }
            else
            {
                this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                this.ForexRateTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
            }
        }

        protected void CurrDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetCurrRate();
            this.itemCount.Value = "0";
            this.boughtItems.Value = "";
            this.perulanganDataDibeli.Text = "";
            this.discountValue.Text = "0";
        }

        protected void btnAddLine_Click(object sender, EventArgs e)
        {
            if (this.EditPostBackHiddenField.Value == "")
            {
                if (this.WarehouseCodeDropDownList.SelectedValue != "null" && this.LocationNameDropDownList.SelectedValue != "null")
                {
                    if (this.itemCount.Value != "0")
                    {
                        String[] _rowDetil = this.boughtItems.Value.Split('^');

                        foreach (String _dataDetil in _rowDetil)
                        {
                            String[] _rowData = _dataDetil.Split('|');
                            SALTrDirectSalesDt _addTrDirectSales = new SALTrDirectSalesDt();

                            ////if (_rowData[1] == this.productCode.Text && _rowData[6] == this.WarehouseCodeDropDownList.SelectedValue && _rowData[8] == this.LocationNameDropDownList.SelectedValue)
                            if (_rowData[1] == this.productCode.Text && _rowData[7] == this.WarehouseCodeDropDownList.SelectedValue && _rowData[9] == this.LocationNameDropDownList.SelectedValue)
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
                            this.boughtItems.Value += this.txtProductName.Text + "|" + this.qty.Text + "|" + this.UnitDropDownList.SelectedValue + "|";
                            this.boughtItems.Value += this.PriceTextBox.Text + "|" + this.LineTotalTextBox.Text + "|";
                            this.boughtItems.Value += ((this.WarehouseCodeDropDownList.SelectedValue == "null") ? "" : this.WarehouseCodeDropDownList.Text) + "|" + (((this.WrhsSubledDropDownList.SelectedValue) == "null") ? "" : this.WrhsSubledDropDownList.Text) + "|" + ((this.LocationNameDropDownList.SelectedValue == "null") ? "" : this.LocationNameDropDownList.Text);
                            _subTotal += Convert.ToDecimal(this.LineTotalTextBox.Text);
                        }
                        else
                        {
                            this.boughtItems.Value += "^" + this.itemCount.Value + "|" + this.productCode.Text + "|";
                            this.boughtItems.Value += this.txtProductName.Text + "|" + this.qty.Text + "|" + this.UnitDropDownList.SelectedValue + "|";
                            this.boughtItems.Value += this.PriceTextBox.Text + "|" + this.LineTotalTextBox.Text + "|";
                            this.boughtItems.Value += ((this.WarehouseCodeDropDownList.SelectedValue == "null") ? "" : this.WarehouseCodeDropDownList.Text) + "|" + (((this.WrhsSubledDropDownList.SelectedValue) == "null") ? "" : this.WrhsSubledDropDownList.Text) + "|" + ((this.LocationNameDropDownList.SelectedValue == "null") ? "" : this.LocationNameDropDownList.Text);
                            _subTotal += Convert.ToDecimal(this.LineTotalTextBox.Text);
                        }

                        this.LineTotalHiddenField.Value = _subTotal.ToString();
                        this.productCode.Text = "";
                        this.productCode.Focus();
                        this.productName.Value = "";
                        this.txtProductName.Text = "";
                        this.PriceTextBox.Text = "0";
                        this.qty.Text = "";
                        this.UnitDropDownList.Items.Clear();
                        this.UnitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                        this.LineTotalTextBox.Text = "0";
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
            else/// save edit
            {

                if (this.WarehouseCodeDropDownList.SelectedValue != "null" && this.LocationNameDropDownList.SelectedValue != "null")
                {
                    if (this.itemCount.Value != "0")
                    {
                        String[] _rowDetil = this.boughtItems.Value.Split('^');

                        foreach (String _dataDetil in _rowDetil)
                        {
                            String[] _rowData = _dataDetil.Split('|');
                            SALTrDirectSalesDt _addTrDirectSales = new SALTrDirectSalesDt();

                            if (_rowData[0] != this.EditPostBackHiddenField.Value)
                            {
                                //if (_rowData[1] == this.productCode.Text && _rowData[6] == this.WarehouseCodeDropDownList.SelectedValue && _rowData[8] == this.LocationNameDropDownList.SelectedValue)
                                if (_rowData[1] == this.productCode.Text && _rowData[7] == this.WarehouseCodeDropDownList.SelectedValue && _rowData[9] == this.LocationNameDropDownList.SelectedValue)
                                {
                                    this.WarningLabel.Text = "ProductCode, Warehouse & Warehouse Location has been add";
                                    return;
                                }
                            }
                            else
                            {
                                this.WarningLabel.Text = "";
                            }
                        }

                    }
                    if (this.txtProductName.Text != "" && this.qty.Text != "")
                    {
                        //this.itemCount.Value = (Convert.ToInt16(this.itemCount.Value) + 1).ToString();

                        Decimal _subTotal = this.LineTotalHiddenField.Value == "" ? 0 : Convert.ToDecimal(this.LineTotalHiddenField.Value);
                        String[] _rowDetil = this.boughtItems.Value.Split('^');
                        this.boughtItems.Value = "";
                        foreach (String _dataDetil in _rowDetil)
                        {
                            String[] _rowData = _dataDetil.Split('|');
                            if (_rowData[0] == this.EditPostBackHiddenField.Value)
                            {
                                if (_rowData[0] == "1")
                                {
                                    this.boughtItems.Value = _rowData[0] + "|" + this.productCode.Text + "|";
                                    this.boughtItems.Value += this.txtProductName.Text + "|" + this.qty.Text + "|" + this.UnitDropDownList.SelectedValue + "|";
                                    this.boughtItems.Value += this.PriceTextBox.Text + "|" + this.LineTotalTextBox.Text + "|";
                                    this.boughtItems.Value += ((this.WarehouseCodeDropDownList.SelectedValue == "null") ? "" : this.WarehouseCodeDropDownList.Text) + "|" + (((this.WrhsSubledDropDownList.SelectedValue) == "null") ? "" : this.WrhsSubledDropDownList.Text) + "|" + ((this.LocationNameDropDownList.SelectedValue == "null") ? "" : this.LocationNameDropDownList.Text);
                                    _subTotal += Convert.ToDecimal(this.LineTotalTextBox.Text);
                                }
                                else
                                {
                                    this.boughtItems.Value += "^" + _rowData[0] + "|" + this.productCode.Text + "|";
                                    this.boughtItems.Value += this.txtProductName.Text + "|" + this.qty.Text + "|" + this.UnitDropDownList.SelectedValue + "|";
                                    this.boughtItems.Value += this.PriceTextBox.Text + "|" + this.LineTotalTextBox.Text + "|";
                                    this.boughtItems.Value += ((this.WarehouseCodeDropDownList.SelectedValue == "null") ? "" : this.WarehouseCodeDropDownList.Text) + "|" + (((this.WrhsSubledDropDownList.SelectedValue) == "null") ? "" : this.WrhsSubledDropDownList.Text) + "|" + ((this.LocationNameDropDownList.SelectedValue == "null") ? "" : this.LocationNameDropDownList.Text);
                                    _subTotal += Convert.ToDecimal(this.LineTotalTextBox.Text);
                                }

                            }
                            else
                            {
                                if (_rowData[0] == "1")
                                {
                                    this.boughtItems.Value = _rowData[0] + "|" + _rowData[1] + "|";
                                    this.boughtItems.Value += _rowData[2] + "|" + _rowData[3] + "|" + _rowData[4] + "|";
                                    this.boughtItems.Value += _rowData[5] + "|" + _rowData[6] + "|";
                                    this.boughtItems.Value += ((_rowData[7] == "null") ? "" : _rowData[7]) + "|" + (((_rowData[8]) == "null") ? "" : _rowData[8]) + "|" + ((_rowData[9] == "null") ? "" : _rowData[9]);
                                    _subTotal += Convert.ToDecimal(this.LineTotalTextBox.Text);
                                }
                                else
                                {
                                    this.boughtItems.Value += "^" + _rowData[0] + "|" + _rowData[1] + "|";
                                    this.boughtItems.Value += _rowData[2] + "|" + _rowData[3] + "|" + _rowData[4] + "|";
                                    this.boughtItems.Value += _rowData[5] + "|" + _rowData[6] + "|";
                                    this.boughtItems.Value += ((_rowData[7] == "null") ? "" : _rowData[7]) + "|" + (((_rowData[8]) == "null") ? "" : _rowData[8]) + "|" + ((_rowData[9] == "null") ? "" : _rowData[9]);
                                    _subTotal += Convert.ToDecimal(this.LineTotalTextBox.Text);
                                }
                            }
                        }
                        //if (this.itemCount.Value == "1")
                        //{
                        //    this.boughtItems.Value = this.itemCount.Value + "|" + this.productCode.Text + "|";
                        //    this.boughtItems.Value += this.txtProductName.Text + "|" + this.qty.Text + "|";
                        //    this.boughtItems.Value += this.PriceTextBox.Text + "|" + this.LineTotalTextBox.Text + "|";
                        //    this.boughtItems.Value += ((this.WarehouseCodeDropDownList.SelectedValue == "null") ? "" : this.WarehouseCodeDropDownList.Text) + "|" + (((this.WrhsSubledDropDownList.SelectedValue) == "null") ? "" : this.WrhsSubledDropDownList.Text) + "|" + ((this.LocationNameDropDownList.SelectedValue == "null") ? "" : this.LocationNameDropDownList.Text);
                        //    _subTotal += Convert.ToDecimal(this.LineTotalTextBox.Text);
                        //}
                        //else
                        //{
                        //    this.boughtItems.Value += "^" + this.itemCount.Value + "|" + this.productCode.Text + "|";
                        //    this.boughtItems.Value += this.txtProductName.Text + "|" + this.qty.Text + "|";
                        //    this.boughtItems.Value += this.PriceTextBox.Text + "|" + this.LineTotalTextBox.Text + "|";
                        //    this.boughtItems.Value += ((this.WarehouseCodeDropDownList.SelectedValue == "null") ? "" : this.WarehouseCodeDropDownList.Text) + "|" + (((this.WrhsSubledDropDownList.SelectedValue) == "null") ? "" : this.WrhsSubledDropDownList.Text) + "|" + ((this.LocationNameDropDownList.SelectedValue == "null") ? "" : this.LocationNameDropDownList.Text);
                        //    _subTotal += Convert.ToDecimal(this.LineTotalTextBox.Text);
                        //}

                        this.LineTotalHiddenField.Value = _subTotal.ToString();
                        this.productCode.Text = "";
                        this.productCode.Focus();
                        this.productName.Value = "";
                        this.txtProductName.Text = "";
                        this.PriceTextBox.Text = "0";
                        this.qty.Text = "";
                        this.UnitDropDownList.Items.Clear();
                        this.UnitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                        this.LineTotalTextBox.Text = "0";
                        this.BaseForexTextBox.Text = _subTotal.ToString();
                        this.ShowWarehouse();
                        this.WrhsSubledDropDownList.SelectedValue = "null";
                        this.LocationNameDropDownList.SelectedValue = "null";
                        this.WrhsSubledDropDownList.Enabled = false;
                        this.EditPostBackHiddenField.Value = "";
                        this.showBoughtItem();

                    }
                }
                else
                {
                    this.WarningLabel.Text = "Warehouse & Warehouse Location Must Be Select";
                }

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
                    if (this.EditPostBackHiddenField.Value == "")
                        _strGenerateTable += "<td><input type='button' onClick='editBoughtItem(\"" + _dataField[0] + "\");' value='Edit'> <input type='button' onClick='deleteItem(\"" + this.boughtItems.ClientID + "\"," + _dataField[0] + ",\"" + this.itemCount.ClientID + "\");' value='Delete'></td></tr>";
                    else
                        _strGenerateTable += "<td></td></tr>";
                }
                _subTotal += Convert.ToDecimal(_dataField[6]);
            }
            this.BaseForexTextBox.Text = _subTotal.ToString();
            this.perulanganDataDibeli.Text = _strGenerateTable;
            this.discount.Text = Math.Round(Convert.ToDecimal(this.discountValue.Text) / Convert.ToDecimal(this.BaseForexTextBox.Text) * 100, 2).ToString();
            this.discountValue.Text = (Convert.ToDecimal(this.discount.Text) * Convert.ToDecimal(this.BaseForexTextBox.Text) / 100).ToString("#,##0.00");
            this.PPNAmountTextBox.Text = ((Convert.ToDecimal(this.BaseForexTextBox.Text) - (Convert.ToDecimal(this.discountValue.Text))) * Convert.ToDecimal(this.tax.Text) / 100).ToString("#,##0.00");
            this.TotalAmountTextBox.Text = (((Convert.ToDecimal(this.BaseForexTextBox.Text) - Convert.ToDecimal(this.discountValue.Text)) + Convert.ToDecimal(this.PPNAmountTextBox.Text) + Convert.ToDecimal(this.StampFeeTextBox.Text) + Convert.ToDecimal(this.OtherFeeTextBox.Text))).ToString("#,##0.00");
        }

        protected void productCode_TextChanged(object sender, EventArgs e)
        {
            String[] _productData = new String[4];
            if (this.UnitHiddenField.Value == "")
            {
                _productData = _posBL.getProductData(this.productCode.Text, this.CurrDropDownList.SelectedValue).Split('|');
            }
            else
            {
                _productData = _posBL.getProductData(this.productCode.Text, this.CurrDropDownList.SelectedValue, this.UnitHiddenField.Value).Split('|');
                this.UnitHiddenField.Value = "";
            }

            if (_productData.Length > 0 && _productData[0] != "")
            {
                this.txtProductName.Text = _productData[0];
                this.productName.Value = _productData[0];
                this.PriceTextBox.Text = Convert.ToDecimal(_productData[2]).ToString("0.00");
                this.UnitDDL();
                if (this.UnitDropDownList.Items.Count != 0)
                    this.UnitDropDownList.SelectedValue = _productData[1];
                else
                    this.UnitDropDownList.Items.Insert(0, new ListItem(_productData[1], _productData[1]));

                this.qty.Focus();
            }
        }

        //protected void CheckValidData()
        //{
        //    String _productCode = "";
        //    int _countProduct = 0;

        //    this.WarningLabel.Text = "";
        //    String[] _detailTransaksi = this.boughtItems.Value.Split('^');
        //    foreach (var _item in _detailTransaksi)
        //    {
        //        String[] _rowData = _item.Split('|');
        //        _productCode = _rowData[1];

        //        _countProduct = 0;
        //        String[] _check = this.boughtItems.Value.Split('^');
        //        foreach (var _item2 in _check)
        //        {
        //            String[] _rowData2 = _item2.Split('|');
        //            if (_rowData2[1] == _productCode)
        //            {
        //                _countProduct += 1;
        //            }
        //        }
        //        if (_countProduct > 1)
        //        {
        //            this.WarningLabel.Text = "Found " + _countProduct + " Product " + _rowData[1] + " - " + this._productBL.GetProductNameByCode(_rowData[1]) + " in this Transaction. Please change your data.";
        //            break;
        //        }
        //    }
        //}

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.boughtItems.Value != "" & this.WarningLabel.Text == "")
            {
                DirectSalesBL _DirectSalesBL = new DirectSalesBL();
                SALTrDirectSalesHd _addSalTrDirectSalesHd = new SALTrDirectSalesHd();
                _addSalTrDirectSalesHd.Date = DateFormMapper.GetValue(this.DateTextBox.Text);
                _addSalTrDirectSalesHd.Status = DirectSalesDataMapper.GetStatusByte(TransStatus.OnHold);
                _addSalTrDirectSalesHd.CustCode = this.CustNmbrTextBox.Text;
                _addSalTrDirectSalesHd.FileNo = "";
                _addSalTrDirectSalesHd.PayCode = this.PayTypeDropDownList.SelectedValue;
                _addSalTrDirectSalesHd.CurrCode = this.CurrDropDownList.Text;
                _addSalTrDirectSalesHd.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
                _addSalTrDirectSalesHd.BaseForex = Convert.ToDecimal(this.BaseForexTextBox.Text);
                _addSalTrDirectSalesHd.DiscPercent = Convert.ToDecimal(this.discount.Text);
                _addSalTrDirectSalesHd.DiscAmount = Convert.ToDecimal(this.discountValue.Text);
                _addSalTrDirectSalesHd.PPNPercent = Convert.ToDecimal(this.tax.Text);
                _addSalTrDirectSalesHd.PPNAmount = Convert.ToDecimal(this.PPNAmountTextBox.Text);
                _addSalTrDirectSalesHd.OtherFee = Convert.ToDecimal(this.OtherFeeTextBox.Text);
                _addSalTrDirectSalesHd.StampFee = Convert.ToDecimal(this.StampFeeTextBox.Text);
                _addSalTrDirectSalesHd.TotalAmount = Convert.ToDecimal(this.TotalAmountTextBox.Text);
                _addSalTrDirectSalesHd.CreatedBy = HttpContext.Current.User.Identity.Name;
                _addSalTrDirectSalesHd.CreatedDate = DateTime.Now;
                _addSalTrDirectSalesHd.EditBy = HttpContext.Current.User.Identity.Name;
                _addSalTrDirectSalesHd.EditDate = DateTime.Now;

                if (this.Remark.Text != "")
                    _addSalTrDirectSalesHd.Remark = this.Remark.Text;
                else
                    _addSalTrDirectSalesHd.Remark = "";

                String _generatedTransNmbr = _DirectSalesBL.postingTransaksi(_addSalTrDirectSalesHd, this.boughtItems.Value);

                if (_generatedTransNmbr != "")
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_generatedTransNmbr, ApplicationConfig.EncryptionKey)));
                }
            }
            else
            {
                if (this.WarningLabel.Text == "")
                    this.WarningLabel.Text = "Silakan Isi Kolom Detail";
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
    }
}