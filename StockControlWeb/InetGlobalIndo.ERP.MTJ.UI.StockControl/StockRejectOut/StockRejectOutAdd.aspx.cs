using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockRejectOut
{
    public partial class StockRejectOutAdd : StockRejectOutBase
    {
        private StockRejectOutBL _stockRejectOutBL = new StockRejectOutBL();
        //private StockOpnameBL _stockOpnameBL = new StockOpnameBL();
        //private StockTypeBL _stockTypeBL = new StockTypeBL();
        //private StockIssueRequestFABL _stockIssueReqFABL = new StockIssueRequestFABL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private CustomerBL _cust = new CustomerBL();
        private SupplierBL _supp = new SupplierBL();
        private PermissionBL _permBL = new PermissionBL();
        //private PurchaseOrderBL _purchaseOrderBL = new PurchaseOrderBL();
        private PurchaseReturBL _purchaseReturBL = new PurchaseReturBL();

        private string _homePage = "StockRejectOut.aspx";
        private string _detailPage = "StockRejectOutDetail.aspx";

        private string _codeKey = "code";

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

                this.ShowSupplierDDL();
                this.ShowWarehouse();
                this.ShowPurchase();
                this.ClearLabel();
                this.ClearData();
            }
        }

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
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
            this.SubledDropDownList.DataSource = this._cust.GetListCustomerForDDL();
            this.SubledDropDownList.DataBind();
            this.SubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowSupp()
        {
            this.SubledDropDownList.Items.Clear();
            this.SubledDropDownList.DataTextField = "SuppName";
            this.SubledDropDownList.DataValueField = "SuppCode";
            this.SubledDropDownList.DataSource = this._supp.GetListDDLSupp();
            this.SubledDropDownList.DataBind();
            this.SubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowPurchase()
        {
            this.PurchaseReturDropDownList.Items.Clear();
            this.PurchaseReturDropDownList.DataTextField = "RRNo";
            this.PurchaseReturDropDownList.DataValueField = "FileNmbr";
            this.PurchaseReturDropDownList.DataSource = this._purchaseReturBL.GetListPRBySupplierForDDL(this.SupplierDropDownList.SelectedValue);
            this.PurchaseReturDropDownList.DataBind();
            this.PurchaseReturDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowSupplierDDL()
        {
            this.SupplierDropDownList.Items.Clear();
            this.SupplierDropDownList.DataTextField = "SuppName";
            this.SupplierDropDownList.DataValueField = "SuppCode";
            this.SupplierDropDownList.DataSource = this._supp.GetListDDLSupp();
            this.SupplierDropDownList.DataBind();
            this.SupplierDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ClearData()
        {
            DateTime now = DateTime.Now;


            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            this.RemarkTextBox.Text = "";
            this.AttnTextBox.Text = "";
            this.SupplierDropDownList.SelectedValue = "null";
            this.WarehouseDropDownList.SelectedValue = "null";
            this.SubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.SubledDropDownList.SelectedValue = "null";
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            STCRejectOutHd _stcRejectOutHd = new STCRejectOutHd();

            _stcRejectOutHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcRejectOutHd.Status = StockRejectOutDataMapper.GetStatus(TransStatus.OnHold);
            _stcRejectOutHd.SuppCode = this.SupplierDropDownList.SelectedValue;
            _stcRejectOutHd.Attn = this.AttnTextBox.Text;
            _stcRejectOutHd.WrhsCode = this.WarehouseDropDownList.SelectedValue;
            _stcRejectOutHd.WrhsFgSubLed = _warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue);
            _stcRejectOutHd.PurchaseRetur = this.PurchaseReturDropDownList.SelectedValue;
            if (this.SubledDropDownList.SelectedValue != "null")
            {
                _stcRejectOutHd.WrhsSubLed = this.SubledDropDownList.SelectedValue;
            }
            else
            {
                _stcRejectOutHd.WrhsSubLed = "";
            }

            _stcRejectOutHd.Remark = this.RemarkTextBox.Text;
            _stcRejectOutHd.FgProcess = YesNoDataMapper.GetYesNo(YesNo.No);
            _stcRejectOutHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcRejectOutHd.DatePrep = DateTime.Now;
            _stcRejectOutHd.FgDeliveryBack = this._purchaseReturBL.GetSinglePRCReturHd(this.PurchaseReturDropDownList.SelectedValue).FgDeliveryBack;
            
            string _result = this._stockRejectOutBL.AddSTCRejectOutHd(_stcRejectOutHd);

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

        protected void SupplierDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SupplierDropDownList.SelectedValue == "null")
            {
                this.AttnTextBox.Text = "";
            }
            else
            {
                this.AttnTextBox.Text = _supp.GetSuppContact(this.SupplierDropDownList.SelectedValue);
            }
            this.ShowPurchase();
        }

        protected void PurchaseReturDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
}
}