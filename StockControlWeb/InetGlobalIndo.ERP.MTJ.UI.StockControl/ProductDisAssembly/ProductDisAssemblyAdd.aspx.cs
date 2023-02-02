using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using System.Web.UI.WebControls;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.ProductDisAssembly
{
    public partial class ProductDisAssemblyAdd : ProductDisAssemblyBase
    {
        private ProductBL _productBL = new ProductBL();
        private ProductAssemblyBL _productAssemblyBL = new ProductAssemblyBL();
        private PermissionBL _permBL = new PermissionBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private SupplierBL _supplierBL = new SupplierBL();
        private CustomerBL _customerBL = new CustomerBL();

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

                this.btnSearchProduct.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findProduct&configCode=productFormula2','_popSearch','width=400,height=550,toolbar=0,location=0,status=0,scrollbars=1')";
                String spawnJS = "<script language='JavaScript'>\n";
                ////////////////////DECLARE FUNCTION FOR CATCHING SJ NO SEARCH
                spawnJS += "function findProduct(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.ProductCodeTextBox.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.getElementById('" + this.ProductNameTextBox.ClientID + "').value = dataArray [1];\n";
                spawnJS += "}\n";

                ///////////////////FUNCTION FOR KEYPRESS ENTER ON PRODUCTCODE
                spawnJS += "function enterProductCode() {\n";
                spawnJS += "document.getElementById('" + this.ProductNameTextBox.ClientID + "').focus();\n";
                spawnJS += "return false;\n";
                spawnJS += "}\n";

                spawnJS += "</script>\n";

                this.javascriptReceiver.Text = spawnJS;

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowWarehouse();

                this.ShowAttribute();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowAttribute()
        {
            this.ProductCodeTextBox.Attributes.Add("onkeydown", "if(event.keyCode==13){return enterProductCode();}");

            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
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

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ClearLabel();

            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            this.ProductCodeTextBox.Text = "";
            this.ProductNameTextBox.Text = "";
            this.QtyTextBox.Text = "0";
            this.ReferenceTextBox.Text = "";
            this.RemarkTextBox.Text = "";
            this.LocationNameDropDownList.Items.Clear();
            this.LocationNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.LocationNameDropDownList.SelectedValue = "null";
            this.WrhsSubledDropDownList.Items.Clear();
            this.WrhsSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.WrhsSubledDropDownList.SelectedValue = "null";
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            STCTrProductAssembly _stcTrProductAssembly = new STCTrProductAssembly();

            char _fgSubled = _warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseCodeDropDownList.SelectedValue);

            _stcTrProductAssembly.TransDate = DateTime.Now;
            _stcTrProductAssembly.Status = TransactionDataMapper.GetStatusByte(TransStatus.OnHold);
            _stcTrProductAssembly.Reference = this.ReferenceTextBox.Text;
            _stcTrProductAssembly.ProductCode = this.ProductCodeTextBox.Text;
            _stcTrProductAssembly.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcTrProductAssembly.WrhsCode = this.WarehouseCodeDropDownList.SelectedValue;
            _stcTrProductAssembly.WrhsFgSubled = _fgSubled;
            _stcTrProductAssembly.WrhsLocationCode = this.LocationNameDropDownList.SelectedValue;
            if (this.WrhsSubledDropDownList.SelectedValue != "null")
            {
                _stcTrProductAssembly.WrhsSubled = this.WrhsSubledDropDownList.SelectedValue;
            }
            else
            {
                _stcTrProductAssembly.WrhsSubled = "";
            }
            _stcTrProductAssembly.Remark = this.RemarkTextBox.Text;
            _stcTrProductAssembly.CreateBy = HttpContext.Current.User.Identity.Name;
            _stcTrProductAssembly.CreateDate = DateTime.Now;
            _stcTrProductAssembly.EditBy = HttpContext.Current.User.Identity.Name;
            _stcTrProductAssembly.EditDate = DateTime.Now;
            _stcTrProductAssembly.fgAssembly = false;

            string _result = this._productAssemblyBL.AddSTCTrProductAssembly(_stcTrProductAssembly);

            if (_result != "")
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Add Data";
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

        protected void WarehouseCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.WarehouseCodeDropDownList.SelectedValue != "null")
            {
                char _fgSubled = _warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseCodeDropDownList.SelectedValue);

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

        public void ShowCust()
        {
            this.WrhsSubledDropDownList.Items.Clear();
            this.WrhsSubledDropDownList.DataTextField = "CustName";
            this.WrhsSubledDropDownList.DataValueField = "CustCode";
            this.WrhsSubledDropDownList.DataSource = this._customerBL.GetListCustomerForDDL();
            this.WrhsSubledDropDownList.DataBind();
            this.WrhsSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
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
    }
}