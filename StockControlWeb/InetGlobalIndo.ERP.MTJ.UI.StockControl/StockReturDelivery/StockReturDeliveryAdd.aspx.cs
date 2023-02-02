using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockReturDelivery
{
    public partial class StockReturDeliveryAdd : StockReturDeliveryBase
    {
        private STCReturBL _stcReturBL = new STCReturBL();
        private CustomerBL _custBL = new CustomerBL();
        private SupplierBL _suppBL = new SupplierBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private RequestSalesReturBL _reqSalesReturBL = new RequestSalesReturBL();
        private BillOfLadingBL _bolBL = new BillOfLadingBL();
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

            if (!this.Page.IsPostBack == true)
            {
                this.SetAttribute();
                this.ShowCustomer();
                this.ShowWarehouse();
                this.ShowRRRetur();
                this.ClearData();
            }
        }

        private void SetAttribute()
        {
            this.TransDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.TransDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

            this.PageTitleLiteral.Text = this._pageTitleLiteral;

            this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
            this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
            this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

            this.TransDateTextBox.Attributes.Add("ReadOnly", "True");
            //this.FgLocationCheckBox.Attributes.Add("OnClick", "CheckUncheck(" + this.FgLocationCheckBox.ClientID + "," + this.WrhsLocationDropDownList.ClientID + ");");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void CheckValidData()
        {
            String _errorMsg = "";
            this.ClearLabel();

            if (this.CustNameDropDownList.SelectedValue == "null")
                _errorMsg = _errorMsg + " Please Choose One of Customer.";

            if (this.RRReturNoDropDownList.SelectedValue == "null")
                _errorMsg = _errorMsg + " Please Choose One of RR ReturNo.";

            if (this.WarehouseDropDownList.SelectedValue == "null")
                _errorMsg = _errorMsg + " Please Choose One of Warehouse.";

            if (this.SubledDropDownList.Enabled == true)
                if (this.SubledDropDownList.SelectedValue == "null")
                    _errorMsg = _errorMsg + " Please Choose One of Subled.";

            if (this.WrhsLocationDropDownList.Enabled == true)
                if (this.WrhsLocationDropDownList.SelectedValue == "null")
                    _errorMsg = _errorMsg + " Please Choose One of Warehouse Location.";

            this.WarningLabel.Text = _errorMsg.ToString();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                STCReturSJHd _stcReturSJHd = new STCReturSJHd();

                _stcReturSJHd.TransNmbr = AppModule.GetValue(TransactionType.StockReceivingRetur);
                _stcReturSJHd.Status = TransactionDataMapper.GetStatus(TransStatus.OnHold);
                _stcReturSJHd.TransDate = new DateTime(DateFormMapper.GetValue(this.TransDateTextBox.Text).Year, DateFormMapper.GetValue(this.TransDateTextBox.Text).Month, DateFormMapper.GetValue(this.TransDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                _stcReturSJHd.CustCode = this.CustNameDropDownList.SelectedValue;
                _stcReturSJHd.RRReturNo = this.RRReturNoDropDownList.SelectedValue;
                _stcReturSJHd.WrhsCode = this.WarehouseDropDownList.SelectedValue;
                _stcReturSJHd.WrhsFgSubLed = _warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue);
                _stcReturSJHd.WrhsSubLed = this.SubledDropDownList.SelectedValue;
                _stcReturSJHd.Remark = this.RemarkTextBox.Text;
                _stcReturSJHd.CreatedBy = HttpContext.Current.User.Identity.Name;
                _stcReturSJHd.CreatedDate = DateTime.Now;
                _stcReturSJHd.EditBy = HttpContext.Current.User.Identity.Name;
                _stcReturSJHd.EditDate = DateTime.Now;

                string _result = this._stcReturBL.AddSTCReturSJHd(_stcReturSJHd, this.FgLocationCheckBox.Checked, this.WrhsLocationDropDownList.SelectedValue);

                if (_result != "")
                {
                    Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_stcReturSJHd.TransNmbr, ApplicationConfig.EncryptionKey)));
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "Your Failed Add Data";
                }
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        private void ShowCustomer()
        {
            this.CustNameDropDownList.DataTextField = "CustName";
            this.CustNameDropDownList.DataValueField = "CustCode";
            this.CustNameDropDownList.DataSource = this._custBL.GetListCustomerForDDL();
            this.CustNameDropDownList.DataBind();
            this.CustNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowRRRetur()
        {
            this.RRReturNoDropDownList.DataTextField = "FileNmbr";
            this.RRReturNoDropDownList.DataValueField = "TransNmbr";
            //this.ReqReturNoDropDownList.DataSource = this._reqSalesReturBL.GetListReqReturForDDL(this.CustNameDropDownList.SelectedValue);
            this.RRReturNoDropDownList.DataSource = this._stcReturBL.GetSTCReturRRHdForDDL(this.CustNameDropDownList.SelectedValue);
            this.RRReturNoDropDownList.DataBind();
            this.RRReturNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowLocation()
        {
            this.WrhsLocationDropDownList.Items.Clear();
            this.WrhsLocationDropDownList.DataTextField = "WLocationName";
            this.WrhsLocationDropDownList.DataValueField = "WLocationCode";
            this.WrhsLocationDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(this.WarehouseDropDownList.SelectedValue);
            this.WrhsLocationDropDownList.DataBind();
            this.WrhsLocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowWarehouse()
        {
            this.WarehouseDropDownList.Items.Clear();
            this.WarehouseDropDownList.DataTextField = "WrhsName";
            this.WarehouseDropDownList.DataValueField = "WrhsCode";
            this.WarehouseDropDownList.DataSource = this._warehouseBL.GetListForDDLActive();
            this.WarehouseDropDownList.DataBind();
            this.WarehouseDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowCust()
        {
            this.SubledDropDownList.Items.Clear();
            this.SubledDropDownList.DataTextField = "CustName";
            this.SubledDropDownList.DataValueField = "CustCode";
            this.SubledDropDownList.DataSource = this._custBL.GetListCustomerForDDL();
            this.SubledDropDownList.DataBind();
            this.SubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowSupp()
        {
            this.SubledDropDownList.Items.Clear();
            this.SubledDropDownList.DataTextField = "SuppName";
            this.SubledDropDownList.DataValueField = "SuppCode";
            this.SubledDropDownList.DataSource = this._suppBL.GetListDDLSupp();
            this.SubledDropDownList.DataBind();
            this.SubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ClearLabel();

            this.TransDateTextBox.Text = DateFormMapper.GetValue(now);
            //this.RRReturNoDropDownList.Items.Clear();
            //this.RRReturNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.RRReturNoDropDownList.SelectedIndex = 0;
            this.CustNameDropDownList.SelectedIndex = 0;
            this.RemarkTextBox.Text = "";
            this.WarehouseDropDownList.SelectedIndex = 0;
            this.SubledDropDownList.Enabled = false;
            this.SubledDropDownList.SelectedIndex = 0;
            this.FgLocationCheckBox.Checked = false;
            this.WrhsLocationDropDownList.SelectedIndex = 0;
            this.WrhsLocationDropDownList.Enabled = false;
        }

        protected void CustNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CustNameDropDownList.SelectedValue != "null")
            {
                this.ShowRRRetur();
            }
            else
            {
                this.RRReturNoDropDownList.Items.Clear();
                this.RRReturNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.RRReturNoDropDownList.SelectedValue = "null";
            }
        }

        protected void WarehouseDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.SubledDropDownList.Enabled = false;
                this.SubledDropDownList.SelectedValue = "null";
            }
            else
            {
                this.SubledDropDownList.Enabled = true;
                if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.ShowCust();
                }
                else if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    this.ShowSupp();
                }
            }
        }

        protected void FgLocationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.FgLocationCheckBox.Checked == true)
            {
                this.WrhsLocationDropDownList.Enabled = true;
                this.ShowLocation();
            }
            else
            {
                this.WrhsLocationDropDownList.SelectedIndex = 0;
                this.WrhsLocationDropDownList.Enabled = false;
            }
        }
    }
}