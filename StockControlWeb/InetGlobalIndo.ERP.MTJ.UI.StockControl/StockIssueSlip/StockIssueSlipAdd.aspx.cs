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
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockIssueSlip
{
    public partial class StockIssueSlipAdd : StockIssueSlipBase
    {
        private StockIssueSlipBL _stockIssueSlipBL = new StockIssueSlipBL();
        private StockIssueRequestBL _stockIssueRequestBL = new StockIssueRequestBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private CustomerBL _custBL = new CustomerBL();
        private SupplierBL _suppBL = new SupplierBL();
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
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowRequestNo();
                this.ShowWarehouse();

                this.SetAttribute();
                this.ClearLabel();
                this.ClearData();                
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowRequestNo()
        {
            this.RequestNoDropDownList.Items.Clear();
            this.RequestNoDropDownList.DataTextField = "FileNmbr";
            this.RequestNoDropDownList.DataValueField = "TransNmbr";
            this.RequestNoDropDownList.DataSource = this._stockIssueRequestBL.GetListReqNoBySIRForDDL();
            this.RequestNoDropDownList.DataBind();
            this.RequestNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
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

        private void ShowLocation()
        {
            this.WrhsLocationDropDownList.Items.Clear();
            this.WrhsLocationDropDownList.DataTextField = "WLocationName";
            this.WrhsLocationDropDownList.DataValueField = "WLocationCode";
            this.WrhsLocationDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(this.WarehouseDropDownList.SelectedValue);
            this.WrhsLocationDropDownList.DataBind();
            this.WrhsLocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.WarningLabel.Text = "";
            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            this.RemarkTextBox.Text = "";
            this.RequestByTextBox.Text = "";
            this.WarehouseDropDownList.SelectedValue = "null";
            this.SubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.SubledDropDownList.SelectedValue = "null";
            this.RequestNoDropDownList.SelectedValue = "null";
            this.WrhsLocationDropDownList.SelectedIndex = 0;
            this.WrhsLocationDropDownList.Enabled = false;
        }

        protected void CheckValidData()
        {
            String _errorMsg = "";
            this.ClearLabel();

            if (this.RequestNoDropDownList.SelectedValue == "null")
                _errorMsg = _errorMsg + " Please Choose One of Request No.";

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

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            STCIssueSlipHd _stcIssueSlipHd = new STCIssueSlipHd();

            _stcIssueSlipHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcIssueSlipHd.Status = StockIssueSlipDataMapper.GetStatus(TransStatus.OnHold);
            _stcIssueSlipHd.WrhsCode = this.WarehouseDropDownList.SelectedValue;
            _stcIssueSlipHd.WrhsFgSubLed = _warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue);
            if (this.SubledDropDownList.SelectedValue != "null")
            {
                _stcIssueSlipHd.WrhsSubLed = this.SubledDropDownList.SelectedValue;
            }
            else
            {
                _stcIssueSlipHd.WrhsSubLed = " ";
            }
            _stcIssueSlipHd.RequestNo = this.RequestNoDropDownList.SelectedValue;
            _stcIssueSlipHd.RequestBy = this.RequestByTextBox.Text;
            _stcIssueSlipHd.Remark = this.RemarkTextBox.Text;
            _stcIssueSlipHd.FgProcess = YesNoDataMapper.GetYesNo(YesNo.No);
            _stcIssueSlipHd.WISType = AppModule.GetValue(TransactionType.StockIssueSlip);
            _stcIssueSlipHd.CreatedBy = HttpContext.Current.User.Identity.Name;
            _stcIssueSlipHd.CreatedDate = DateTime.Now;
            _stcIssueSlipHd.EditBy = HttpContext.Current.User.Identity.Name;
            _stcIssueSlipHd.EditDate = DateTime.Now;

            string _result = this._stockIssueSlipBL.AddSTCIssueSlipHd(_stcIssueSlipHd);

            if (_result != "")
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.ClearLabel();
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