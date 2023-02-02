using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.SupplierNote
{
    public partial class SupplierNoteDetail2Add : SupplierNoteBase
    {
        private SupplierNoteBL _supplierNoteBL = new SupplierNoteBL();
        private UnitBL _unitBL = new UnitBL();
        private ProductBL _productBL = new ProductBL();
        private PermissionBL _permBL = new PermissionBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private SupplierBL _supplierBL = new SupplierBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
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

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                //this.ShowProduct();
                this.ShowWarehouse();
                this.ShowUnit();

                this.ClearData();
                this.SetAttribute();
            }

            if (this.tempProductCode.Value != this.ProductPicker1.ProductCode)
            {
                this.tempProductCode.Value = this.ProductPicker1.ProductCode;

                if (this.ProductPicker1.ProductCode != "null")
                {
                    this.UnitDropDownList.SelectedValue = _productBL.GetUnitCodeByCode(this.ProductPicker1.ProductCode);
                }
                else
                {
                    this.UnitDropDownList.SelectedValue = "null";
                }
                this.QtyTextBox.Text = "0";
                this.PriceTextBox.Text = "0";
                this.AmountTextBox.Text = "0";
            }

        }

        public void SetAttribute()
        {
            this.AmountTextBox.Attributes.Add("ReadOnly", "True");

            this.QtyTextBox.Attributes.Add("OnBlur", "Count(" + this.QtyTextBox.ClientID + "," + this.PriceTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PriceTextBox.Attributes.Add("OnBlur", "Count(" + this.QtyTextBox.ClientID + "," + this.PriceTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearData()
        {
            this.ClearLabel();

            this.WarehouseDropDownList.SelectedValue = "null";
            this.WarehouseFgSubledHiddenField.Value = "";
            this.LocationDropDownList.Items.Clear();
            this.LocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.LocationDropDownList.SelectedValue = "null";
            this.WarehouseSubledDropDownList.Items.Clear();
            this.WarehouseSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.WarehouseSubledDropDownList.Enabled = false;
            //this.ProductDropDownList.SelectedValue = "null";
            this.QtyTextBox.Text = "0";
            this.UnitDropDownList.SelectedValue = "null";
            this.PriceTextBox.Text = "0";
            this.AmountTextBox.Text = "0";
            this.RemarkTextBox.Text = "";
            this.DecimalPlaceHiddenField.Value = "";

            FINSuppInvHd _finSuppInvHd = _supplierNoteBL.GetSingleFINSuppInvHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finSuppInvHd.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
        }

        protected void ShowWarehouse()
        {
            this.WarehouseDropDownList.Items.Clear();
            this.WarehouseDropDownList.DataTextField = "WrhsName";
            this.WarehouseDropDownList.DataValueField = "WrhsCode";
            this.WarehouseDropDownList.DataSource = this._warehouseBL.GetListForDDLActive();
            this.WarehouseDropDownList.DataBind();
            this.WarehouseDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowCustomer()
        {
            this.WarehouseSubledDropDownList.Items.Clear();
            this.WarehouseSubledDropDownList.DataTextField = "CustName";
            this.WarehouseSubledDropDownList.DataValueField = "CustCode";
            this.WarehouseSubledDropDownList.DataSource = this._customerBL.GetListCustomerForDDL();
            this.WarehouseSubledDropDownList.DataBind();
            this.WarehouseSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowSupplier()
        {
            this.WarehouseSubledDropDownList.Items.Clear();
            this.WarehouseSubledDropDownList.DataTextField = "SuppName";
            this.WarehouseSubledDropDownList.DataValueField = "SuppCode";
            this.WarehouseSubledDropDownList.DataSource = this._supplierBL.GetListDDLSupp();
            this.WarehouseSubledDropDownList.DataBind();
            this.WarehouseSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void WarehouseDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.WarehouseDropDownList.SelectedValue != "null")
            {
                this.ShowWrhsLocation();

                char _tempFgSubled = this._warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue);

                this.WarehouseFgSubledHiddenField.Value = _tempFgSubled.ToString();

                if (_tempFgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.ShowCustomer();

                    this.WarehouseSubledDropDownList.Enabled = true;
                }
                else if (_tempFgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    this.ShowSupplier();

                    this.WarehouseSubledDropDownList.Enabled = true;
                }
                else if (_tempFgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
                {
                    this.WarehouseSubledDropDownList.Items.Clear();
                    this.WarehouseSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                    this.WarehouseSubledDropDownList.Enabled = false;
                }
                else
                {
                    this.WarehouseSubledDropDownList.Items.Clear();
                    this.WarehouseSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                    this.WarehouseSubledDropDownList.Enabled = false;
                }

            }
            else
            {
                this.LocationDropDownList.Items.Clear();
                this.LocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.LocationDropDownList.SelectedValue = "null";

                this.WarehouseFgSubledHiddenField.Value = "";

                this.WarehouseSubledDropDownList.Items.Clear();
                this.WarehouseSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.WarehouseSubledDropDownList.Enabled = false;
            }
        }

        public void ShowWrhsLocation()
        {
            this.LocationDropDownList.Items.Clear();
            this.LocationDropDownList.DataTextField = "WLocationName";
            this.LocationDropDownList.DataValueField = "WLocationCode";
            this.LocationDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(this.WarehouseDropDownList.SelectedValue);
            this.LocationDropDownList.DataBind();
            this.LocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        //public void ShowProduct()
        //{
        //    this.ProductDropDownList.Items.Clear();
        //    this.ProductDropDownList.DataTextField = "ProductName";
        //    this.ProductDropDownList.DataValueField = "ProductCode";
        //    this.ProductDropDownList.DataSource = this._productBL.GetListProductForDDLStock();
        //    this.ProductDropDownList.DataBind();
        //    this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        public void ShowUnit()
        {
            this.UnitDropDownList.Items.Clear();
            this.UnitDropDownList.DataTextField = "UnitName";
            this.UnitDropDownList.DataValueField = "UnitCode";
            this.UnitDropDownList.DataSource = this._unitBL.GetListForDDL();
            this.UnitDropDownList.DataBind();
            this.UnitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            FINSuppInvDt2 _finSuppInvDt2 = new FINSuppInvDt2();

            _finSuppInvDt2.TransNmbr = _transNo;
            _finSuppInvDt2.ProductCode = this.ProductPicker1.ProductCode ;
            _finSuppInvDt2.WrhsCode = this.WarehouseDropDownList.SelectedValue;
            if (this.WarehouseSubledDropDownList.SelectedValue != "null")
            {
                _finSuppInvDt2.WrhsSubLed = this.WarehouseSubledDropDownList.SelectedValue;
            }
            else
            {
                _finSuppInvDt2.WrhsSubLed = "";
            }
            _finSuppInvDt2.LocationCode = this.LocationDropDownList.SelectedValue;
            _finSuppInvDt2.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _finSuppInvDt2.Unit = this.UnitDropDownList.SelectedValue;
            _finSuppInvDt2.PriceForex = (this.PriceTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PriceTextBox.Text);
            _finSuppInvDt2.AmountForex = (this.AmountTextBox.Text == "") ? 0 : Convert.ToDecimal(this.AmountTextBox.Text);
            _finSuppInvDt2.Remark = this.RemarkTextBox.Text;

            bool _result = this._supplierNoteBL.AddFINSuppInvDt2(_finSuppInvDt2);

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

        //protected void ProductDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.ProductPicker1.ProductCode != "null")
        //    {
        //        this.UnitDropDownList.SelectedValue = _productBL.GetUnitCodeByCode(this.ProductPicker1.ProductCode );
        //    }
        //    else
        //    {
        //        this.UnitDropDownList.SelectedValue = "null";
        //    }
        //    this.QtyTextBox.Text = "0";
        //    this.PriceTextBox.Text = "0";
        //    this.AmountTextBox.Text = "0";
        //}
    }
}