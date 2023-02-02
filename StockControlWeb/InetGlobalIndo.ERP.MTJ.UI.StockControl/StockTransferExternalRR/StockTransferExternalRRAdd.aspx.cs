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

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockTransferExternalRR
{
    public partial class StockTransferExternalRRAdd : StockTransferExternalRRBase
    {
        private WarehouseBL _wrhs = new WarehouseBL();
        private StockTransferExternalRRBL _stcTransInBL = new StockTransferExternalRRBL();
        private StockTransferExternalSJBL _stcTransOutBL = new StockTransferExternalSJBL();
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

                this.SetAttribute();

                this.ShowSJNo();
                this.ClearLabel();
                this.ClearData();
            }
        }

        private void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowSJNo()
        {
            this.SJNoDropDownList.Items.Clear();
            this.SJNoDropDownList.DataTextField = "FileNmbr";
            this.SJNoDropDownList.DataValueField = "TransNmbr";
            this.SJNoDropDownList.DataSource = this._stcTransOutBL.GetSJNoFromVSTTransferSJForRR();
            this.SJNoDropDownList.DataBind();
            this.SJNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        //public void ShowWarehouseDest()
        //{
        //    this.WrhsDestDropDownList.Items.Clear();
        //    this.WrhsDestDropDownList.DataTextField = "WrhsName";
        //    this.WrhsDestDropDownList.DataValueField = "WrhsCode";
        //    this.WrhsDestDropDownList.DataSource = this._wrhs.GetWarehouseActiveAndWrhsArea();
        //    this.WrhsDestDropDownList.DataBind();
        //    this.WrhsDestDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        //public void ShowCustDest()
        //{
        //    this.DestSubledDropDownList.Items.Clear();
        //    this.DestSubledDropDownList.DataTextField = "CustName";
        //    this.DestSubledDropDownList.DataValueField = "CustCode";
        //    this.DestSubledDropDownList.DataSource = this._cust.GetListCustomerForDDL();
        //    this.DestSubledDropDownList.DataBind();
        //    this.DestSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        //public void ShowSuppDest()
        //{
        //    this.DestSubledDropDownList.Items.Clear();
        //    this.DestSubledDropDownList.DataTextField = "SuppName";
        //    this.DestSubledDropDownList.DataValueField = "SuppCode";
        //    this.DestSubledDropDownList.DataSource = this._supp.GetListDDLSupp();
        //    this.DestSubledDropDownList.DataBind();
        //    this.DestSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            this.SJNoDropDownList.SelectedValue = "null";
            this.WrhsSrcTextBox.Text = "";
            this.SrcSubledTextBox.Text = "";
            this.WrhsDestTextBox.Text = "";
            this.DestSubledTextBox.Text = "";
            this.DriverTextBox.Text = "";
            this.CarNoTextBox.Text = "";
            this.OperatorTextBox.Text = "";
            this.RemarkTextBox.Text = "";
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            STCTransInHd _stcTransInHd = new STCTransInHd();

            _stcTransInHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcTransInHd.Status = StockTransferExternalRRDataMapper.GetStatus(TransStatus.OnHold);
            _stcTransInHd.TransReff = this.SJNoDropDownList.SelectedValue;
            _stcTransInHd.WrhsSrc = _stcTransOutBL.GetWrhsSrcFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue);
            _stcTransInHd.WrhsSrcFgSubLed = _wrhs.GetWarehouseFgSubledByCode(_stcTransOutBL.GetWrhsSrcFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue));
            _stcTransInHd.WrhsSrcSubLed = _stcTransOutBL.GetWrhsSrcSubledFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue);
            _stcTransInHd.WrhsDest = _stcTransOutBL.GetWrhsDestFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue);
            _stcTransInHd.WrhsDestFgSubLed = _wrhs.GetWarehouseFgSubledByCode(_stcTransOutBL.GetWrhsDestFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue));
            _stcTransInHd.WrhsDestSubLed = _stcTransOutBL.GetWrhsDestSubledFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue);
            _stcTransInHd.Driver = this.DriverTextBox.Text;
            _stcTransInHd.CarNo = this.CarNoTextBox.Text;
            _stcTransInHd.Operator = this.OperatorTextBox.Text;
            _stcTransInHd.Remark = this.RemarkTextBox.Text;
            _stcTransInHd.FgProcess = YesNoDataMapper.GetYesNo(YesNo.No);
            _stcTransInHd.TGType = (YesNoDataMapper.GetYesNo(YesNo.No)).ToString();
            _stcTransInHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcTransInHd.DatePrep = DateTime.Now;

            string _result = this._stcTransInBL.AddSTCTransInHd(_stcTransInHd);

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

        protected void SJNoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SJNoDropDownList.SelectedValue != "null")
            {
                this.WrhsDestTextBox.Text = _wrhs.GetWarehouseNameByCode(_stcTransOutBL.GetWrhsDestFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue));
                if (_wrhs.GetWarehouseFgSubledByCode(_stcTransOutBL.GetWrhsDestFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue)) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.DestSubledTextBox.Text = _custBL.GetNameByCode(_stcTransOutBL.GetWrhsDestSubledFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue));
                }
                else if (_wrhs.GetWarehouseFgSubledByCode(_stcTransOutBL.GetWrhsDestFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue)) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    this.DestSubledTextBox.Text = _suppBL.GetSuppNameByCode(_stcTransOutBL.GetWrhsDestSubledFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue));
                }
                else if (_wrhs.GetWarehouseFgSubledByCode(_stcTransOutBL.GetWrhsSrcFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue)) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
                {
                    this.DestSubledTextBox.Text = "";
                }
                this.WrhsSrcTextBox.Text = _wrhs.GetWarehouseNameByCode(_stcTransOutBL.GetWrhsSrcFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue));
                if (_wrhs.GetWarehouseFgSubledByCode(_stcTransOutBL.GetWrhsSrcFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue)) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.SrcSubledTextBox.Text = _custBL.GetNameByCode(_stcTransOutBL.GetWrhsSrcSubledFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue));
                }
                else if (_wrhs.GetWarehouseFgSubledByCode(_stcTransOutBL.GetWrhsSrcFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue)) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    this.SrcSubledTextBox.Text = _suppBL.GetSuppNameByCode(_stcTransOutBL.GetWrhsSrcSubledFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue));
                }
                else if (_wrhs.GetWarehouseFgSubledByCode(_stcTransOutBL.GetWrhsSrcFromVSTTransferSJForRR(this.SJNoDropDownList.SelectedValue)) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
                {
                    this.SrcSubledTextBox.Text = "";
                }
            }
            else
            {
                this.WrhsSrcTextBox.Text = "";
                this.SrcSubledTextBox.Text = "";
                this.WrhsDestTextBox.Text = "";
                this.DestSubledTextBox.Text = "";
            }
        }

        //protected void WrhsDestDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (_wrhs.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
        //    {
        //        this.DestSubledDropDownList.Enabled = false;
        //    }
        //    else
        //    {
        //        this.DestSubledDropDownList.Enabled = true;
        //        if (_wrhs.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
        //        {
        //            this.ShowCustDest();
        //        }
        //        else if (_wrhs.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
        //        {
        //            this.ShowSuppDest();
        //        }
        //    }
        //}
    }
}