using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using System.Web.UI.WebControls;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.BillOfLading
{
    public partial class BillOfladingDetailEdit : BillOfLadingBase
    {
        private CustomerBL _custBL = new CustomerBL();
        private SupplierBL _suppBL = new SupplierBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private BillOfLadingBL _billOfLadingBL = new BillOfLadingBL();
        private ProductBL _productBL = new ProductBL();
        private UnitBL _unitBL = new UnitBL();
        private SalesOrderBL _salesOrderBL = new SalesOrderBL();
        private DeliveryOrderBL _deliveryOrderBL = new DeliveryOrderBL();
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
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
                this.ShowWarehouse();
                this.ShowLocation();
                this.ShowCust();
                this.ShowData();
            }
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
            this.WrhsLocationDropDownList.Items.Clear();
            this.WrhsLocationDropDownList.DataTextField = "WLocationName";
            this.WrhsLocationDropDownList.DataValueField = "WLocationCode";
            this.WrhsLocationDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(this.WarehouseCodeDropDownList.SelectedValue);
            this.WrhsLocationDropDownList.DataBind();
            this.WrhsLocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowCust()
        {
            this.WrhsSubledDropDownList.Items.Clear();
            this.WrhsSubledDropDownList.DataTextField = "CustName";
            this.WrhsSubledDropDownList.DataValueField = "CustCode";
            this.WrhsSubledDropDownList.DataSource = this._custBL.GetListCustomerForDDL();
            this.WrhsSubledDropDownList.DataBind();
            this.WrhsSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.QtyTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.QtyTextBox.Attributes.Add("OnBlur", "ChangeFormat(" + QtyTextBox.ClientID + ");");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ShowData()
        {
            string _transNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _locationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);
            string _DONoCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._DOKey), ApplicationConfig.EncryptionKey);
            String _wrhsCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._wrhsCode), ApplicationConfig.EncryptionKey);

            STCSJDt _stcSJDt = this._billOfLadingBL.GetSingleSTCSJDtSO(_transNmbr, _DONoCode, _productCode, _wrhsCode, _locationCode);

            //this.DONoTextBox.Text = this._deliveryOrderBL.GetFileNmbrMKTDOHd(_stcSJDt.DONo);
            this.DONoTextBox.Text = _stcSJDt.DONo;
            this.ProductCodeTextBox.Text = _stcSJDt.ProductCode;
            this.ProductNameTextBox.Text = this._productBL.GetProductNameByCode(_stcSJDt.ProductCode);
            //this.LocationNameTextBox.Text = this._warehouseBL.GetWarehouseLocationNameByCode(_stcSJDt.LocationCode);
            this.WarehouseCodeDropDownList.SelectedValue = _stcSJDt.WrhsCode;
            this.ChangeWarehouse();
            this.WrhsSubledDropDownList.SelectedValue = _stcSJDt.WrhsSubLed;
            this.WrhsLocationDropDownList.SelectedValue = _stcSJDt.LocationCode;

            this.QtyTextBox.Text = (_stcSJDt.Qty == 0) ? "0" : _stcSJDt.Qty.ToString("#,##0.##");
            this.UnitNameTextBox.Text = this._unitBL.GetUnitNameByCode(_stcSJDt.Unit);
            this.RemarkTextBox.Text = _stcSJDt.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _locationNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);
            string _doNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._DOKey), ApplicationConfig.EncryptionKey);
            String _wrhsCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._wrhsCode), ApplicationConfig.EncryptionKey);

            STCSJDt _stcSJDt = this._billOfLadingBL.GetSingleSTCSJDtSO(_transNmbr, _doNo, _productNo, _wrhsCode, _locationNo);

            _stcSJDt.WrhsCode = this.WarehouseCodeDropDownList.SelectedValue;
            //_stcSJDt.WrhsSubLed = this.WrhsSubledDropDownList.SelectedValue;
            _stcSJDt.LocationCode = this.WrhsLocationDropDownList.SelectedValue;
            _stcSJDt.WrhsFgSubLed = _warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseCodeDropDownList.SelectedValue);
            if (this.WrhsSubledDropDownList.SelectedValue != "null")
            {
                _stcSJDt.WrhsSubLed = this.WrhsSubledDropDownList.SelectedValue;
            }
            else
            {
                _stcSJDt.WrhsSubLed = "";
            }

            _stcSJDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcSJDt.QtyReceive = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcSJDt.Remark = this.RemarkTextBox.Text;

            bool _result = this._billOfLadingBL.EditSTCSJDt(_stcSJDt);

            if (_result == true)
            {
                Response.Redirect(this._viewPageSo + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPageSo + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        protected void WarehouseCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ChangeWarehouse();
        }

        private void ChangeWarehouse()
        {
            this.ShowLocation();

            char _fgSubled = _warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseCodeDropDownList.SelectedValue);

            if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.WrhsSubledDropDownList.Enabled = false;
                this.WrhsSubledDropDownList.SelectedValue = "null";
                this.WrhsSubledCustomValidator.Enabled = false;
            }
            else
            {
                this.WrhsSubledCustomValidator.Enabled = true;
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

        public void ShowSupp()
        {
            this.WrhsSubledDropDownList.Items.Clear();
            this.WrhsSubledDropDownList.DataTextField = "SuppName";
            this.WrhsSubledDropDownList.DataValueField = "SuppCode";
            this.WrhsSubledDropDownList.DataSource = this._suppBL.GetListDDLSupp();
            this.WrhsSubledDropDownList.DataBind();
            this.WrhsSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }
    }
}