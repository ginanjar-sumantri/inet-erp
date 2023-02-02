using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.BillOfLading
{
    public partial class BillOfLadingDetailAdd : BillOfLadingBase
    {
        private CustomerBL _custBL = new CustomerBL();
        private SupplierBL _suppBL = new SupplierBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private RequestSalesReturBL _requestSalesReturBL = new RequestSalesReturBL();
        private UnitBL _unitBL = new UnitBL();
        private BillOfLadingBL _billOfLadingBL = new BillOfLadingBL();
        private ProductBL _productBL = new ProductBL();
        private DeliveryOrderBL _deliveryOrderBL = new DeliveryOrderBL();
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
                //this.showLocation();
                //this.ShowDONo();
                //this.ShowProduct();
                this.ShowData();
                this.ShowWarehouse();
                this.ShowLocation();
                this.ShowCust();
                this.ShowSONo();
                this.ClearData();
            }
        }

        protected void ShowData() 
        {
            STCSJHd _stcSJHd = _billOfLadingBL.GetSingleSTCSJHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            this.CustNameTextBox.Text = _stcSJHd.TransNmbr;

            this.CustCodeHiddenField.Value = _stcSJHd.CustCode;
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
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

        private void ShowSONo()
        {
            this.SONoDropDownList.Items.Clear();
            this.SONoDropDownList.DataTextField = "FileNmbr";
            this.SONoDropDownList.DataValueField = "TransNmbr";
            this.SONoDropDownList.DataSource = this._deliveryOrderBL.GetListSONoForDDLInDO(this.CustCodeHiddenField.Value);
            this.SONoDropDownList.DataBind();
            this.SONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SetAttribute()
        {
            //this.FgLocationCheckBox.Attributes.Add("OnClick", "CheckUncheck(" + this.FgLocationCheckBox.ClientID + "," + this.WrhsLocationDropDownList.ClientID + ");");

            //this.QtyTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            //this.QtyTextBox.Attributes.Add("OnBlur", "return ChangeFormat(" + this.QtyTextBox.ClientID + ");");
            //this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        //private void ShowDONo()
        //{
        //    this.DONoDropDownList.Items.Clear();
        //    this.DONoDropDownList.DataTextField = "FileNmbr";
        //    this.DONoDropDownList.DataValueField = "TransNmbr";
        //    this.DONoDropDownList.DataSource = this._deliveryOrderBL.GetListDoForDDLForSJ(_billOfLadingBL.GetSingleSTCSJHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).SONo);
        //    this.DONoDropDownList.DataBind();
        //    this.DONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        //private void ShowProduct()
        //{
        //    this.ProductNameDropDownList.Items.Clear();
        //    this.ProductNameDropDownList.DataTextField = "ProductName";
        //    this.ProductNameDropDownList.DataValueField = "ProductCode";
        //    this.ProductNameDropDownList.DataSource = this._deliveryOrderBL.GetListProductForDDLForSJ(_billOfLadingBL.GetSingleSTCSJHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).SONo);
        //    this.ProductNameDropDownList.DataBind();
        //    this.ProductNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        //private void showLocation()
        //{
        //    STCSJHd _stcSJHd = this._billOfLadingBL.GetSingleSTCSJHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

        //    this.LocationNameDropDownList.Items.Clear();
        //    this.LocationNameDropDownList.DataTextField = "WLocationName";
        //    this.LocationNameDropDownList.DataValueField = "WLocationCode";
        //    this.LocationNameDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(_billOfLadingBL.GetSingleSTCSJHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).WrhsCode);
        //    this.LocationNameDropDownList.DataBind();
        //    this.LocationNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

        //    if (_stcSJHd.FgLocation == false)
        //    {
        //        this.LocationNameDropDownList.Enabled = true;
        //        this.LocationNameDropDownList.SelectedValue = "null";
        //    }
        //    else
        //    {
        //        this.LocationNameDropDownList.SelectedValue = _stcSJHd.LocationCode;
        //        this.LocationNameDropDownList.Enabled = false;
        //    }
        //}

        private void ClearData()
        {
            this.ClearLabel();

            //this.ProductNameDropDownList.SelectedValue = "null";
            //this.UnitCodeTextBox.Text = "";
            //this.QtyTextBox.Text = "";
            //this.RemarkTextBox.Text = "";

            this.WarehouseCodeDropDownList.SelectedValue = "null";
            //this.SONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.SONoDropDownList.SelectedValue = "null";
            this.WrhsSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.WrhsSubledDropDownList.SelectedValue = "null";
            //this.FgLocationCheckBox.Checked = false;
            //this.WrhsLocationDropDownList.Enabled = false;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCSJHd _stcSJHd = _billOfLadingBL.GetSingleSTCSJHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)); 

            _stcSJHd.SONo = this.SONoDropDownList.SelectedValue;
            _stcSJHd.WrhsCode = this.WarehouseCodeDropDownList.SelectedValue;
            _stcSJHd.WrhsFgSubLed = _warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseCodeDropDownList.SelectedValue);
            if (this.WrhsSubledDropDownList.SelectedValue != "null")
            {
                _stcSJHd.WrhsSubLed = this.WrhsSubledDropDownList.SelectedValue;
            }
            else
            {
                _stcSJHd.WrhsSubLed = "";
            }
            //_stcSJHd.FgLocation = this.FgLocationCheckBox.Checked;
            _stcSJHd.LocationCode = this.WrhsLocationDropDownList.SelectedValue;

            bool _result1 = this._billOfLadingBL.EditSTCSJHd(_stcSJHd);

            STCSJDtReference _stcSJDtReference = new STCSJDtReference();

            _stcSJDtReference.TransNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            _stcSJDtReference.ReferenceNmbr = this.SONoDropDownList.SelectedValue;
            _stcSJDtReference.TransDate = _billOfLadingBL.GetSingleMKTSOHd(this.SONoDropDownList.SelectedValue).TransDate;
            _stcSJDtReference.WrhsCode = this.WarehouseCodeDropDownList.SelectedValue;
            _stcSJDtReference.WrhsFgSubLed = _warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseCodeDropDownList.SelectedValue);
            //_stcSJDtReference.WrhsSubLed = this.WrhsSubledDropDownList.SelectedValue;
            if (this.WrhsSubledDropDownList.SelectedValue != "null")
            {
                _stcSJDtReference.WrhsSubLed = this.WrhsSubledDropDownList.SelectedValue;
            }
            else
            {
                _stcSJDtReference.WrhsSubLed = "";
            }
            _stcSJDtReference.LocationCode = this.WrhsLocationDropDownList.SelectedValue;
            _stcSJDtReference.CreatedBy = HttpContext.Current.User.Identity.Name;
            _stcSJDtReference.CreatedDate = DateTime.Now;
            _stcSJDtReference.EditBy = HttpContext.Current.User.Identity.Name;
            _stcSJDtReference.EditDate = DateTime.Now;

            bool _result = this._billOfLadingBL.AddSTCSJDtReference(_stcSJDtReference);

            if (_result != false && _result1 != false)
            {
                Response.Redirect(this._viewPageSo + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
            }

            //STCSJDt _stcSJDt = new STCSJDt();

            //_stcSJDt.TransNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            //_stcSJDt.DONo = this.SONoDropDownList.SelectedValue;
            //_stcSJDt.ProductCode = this.ProductNameDropDownList.SelectedValue;
            //_stcSJDt.LocationCode = this.LocationNameDropDownList.SelectedValue;
            //_stcSJDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            //_stcSJDt.Unit = this._productBL.GetSingleProduct(this.ProductNameDropDownList.SelectedValue).Unit;
            //_stcSJDt.Remark = this.RemarkTextBox.Text;
            //_stcSJDt.QtyLoss = 0;
            //_stcSJDt.QtyRetur = 0;
            //_stcSJDt.QtyReceive = Convert.ToDecimal(this.QtyTextBox.Text);

            //bool _result = this._billOfLadingBL.AddSTCSJDt(_stcSJDt);

            //if (_result != false)
            //{
            //    Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));

            //}
            //else
            //{
            //    this.ClearLabel();
            //    this.WarningLabel.Text = "Your Failed Add Data";
            //}
        }

        protected void WarehouseCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPageSo + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        //protected void DONoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (DONoDropDownList.SelectedValue != "null")
        //    {
        //        this.ShowProduct();
        //    }
        //    else
        //    {
        //        this.ProductNameDropDownList.Items.Clear(); 
        //        this.ProductNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //    }
        //}

        //protected void ProductNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.ProductNameDropDownList.SelectedValue != "null")
        //    {
        //        MKTDODt _mktDODt = _deliveryOrderBL.GetSingleMKTDODt(this.DONoDropDownList.SelectedValue, this.ProductNameDropDownList.SelectedValue);
        //        this.QtyTextBox.Text = _mktDODt.Qty.ToString("#,##0.##");
        //        this.UnitCodeTextBox.Text = _unitBL.GetUnitNameByCode(_mktDODt.Unit);
        //    }
        //    else
        //    {
        //        this.QtyTextBox.Text = "0";
        //        this.UnitCodeTextBox.Text = "";
        //    }
        //}
    }
}