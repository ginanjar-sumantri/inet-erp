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

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockTransferInternal
{
    public partial class StockTransferInternalAdd : StockTransferInternalBase
    {
        private WarehouseBL _wrhsBL = new WarehouseBL();
        private StockTransferInternalBL _stcTransInternalBL = new StockTransferInternalBL();
        //private StockTransRequestBL _stcTransReq = new StockTransRequestBL();
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


                this.ShowWarehouseArea();

                this.SetAttribute();
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

        public void ShowWarehouseArea()
        {
            this.WrhsAreaDropDownList.Items.Clear();
            this.WrhsAreaDropDownList.DataTextField = "WrhsAreaName";
            this.WrhsAreaDropDownList.DataValueField = "WrhsAreaCode";
            this.WrhsAreaDropDownList.DataSource = this._wrhsBL.GetListWrhsAreaForDDL();
            this.WrhsAreaDropDownList.DataBind();
            this.WrhsAreaDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowWarehouseDest()
        {
            this.WrhsDestDropDownList.Items.Clear();
            this.WrhsDestDropDownList.DataTextField = "WrhsName";
            this.WrhsDestDropDownList.DataValueField = "WrhsCode";
            this.WrhsDestDropDownList.DataSource = this._wrhsBL.GetWarehouseActiveAndWrhsArea(this.WrhsAreaDropDownList.SelectedValue);
            this.WrhsDestDropDownList.DataBind();
            this.WrhsDestDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowWarehouseSrc()
        {
            this.WrhsSrcDropDownList.Items.Clear();
            this.WrhsSrcDropDownList.DataTextField = "WrhsName";
            this.WrhsSrcDropDownList.DataValueField = "WrhsCode";
            this.WrhsSrcDropDownList.DataSource = this._wrhsBL.GetWarehouseActiveAndWrhsArea(this.WrhsAreaDropDownList.SelectedValue);
            this.WrhsSrcDropDownList.DataBind();
            this.WrhsSrcDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowCustSrc()
        {
            this.SrcSubledDropDownList.Items.Clear();
            this.SrcSubledDropDownList.DataTextField = "CustName";
            this.SrcSubledDropDownList.DataValueField = "CustCode";
            this.SrcSubledDropDownList.DataSource = this._custBL.GetListCustomerForDDL();
            this.SrcSubledDropDownList.DataBind();
            this.SrcSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowCustDest()
        {
            this.DestSubledDropDownList.Items.Clear();
            this.DestSubledDropDownList.DataTextField = "CustName";
            this.DestSubledDropDownList.DataValueField = "CustCode";
            this.DestSubledDropDownList.DataSource = this._custBL.GetListCustomerForDDL();
            this.DestSubledDropDownList.DataBind();
            this.DestSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowSuppSrc()
        {
            this.SrcSubledDropDownList.Items.Clear();
            this.SrcSubledDropDownList.DataTextField = "SuppName";
            this.SrcSubledDropDownList.DataValueField = "SuppCode";
            this.SrcSubledDropDownList.DataSource = this._suppBL.GetListDDLSupp();
            this.SrcSubledDropDownList.DataBind();
            this.SrcSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowSuppDest()
        {
            this.DestSubledDropDownList.Items.Clear();
            this.DestSubledDropDownList.DataTextField = "SuppName";
            this.DestSubledDropDownList.DataValueField = "SuppCode";
            this.DestSubledDropDownList.DataSource = this._suppBL.GetListDDLSupp();
            this.DestSubledDropDownList.DataBind();
            this.DestSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ClearData()
        {
            this.ClearLabel();

            DateTime now = DateTime.Now;

            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            //this.WrhsAreaDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.WrhsAreaDropDownList.SelectedValue = "null";
            this.WrhsSrcDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.WrhsSrcDropDownList.SelectedValue = "null";
            this.WrhsDestDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.WrhsDestDropDownList.SelectedValue = "null";
            this.OperatorTextBox.Text = "";
            this.RemarkTextBox.Text = "";
            this.SrcSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.SrcSubledDropDownList.SelectedValue = "null";
            this.DestSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.DestSubledDropDownList.SelectedValue = "null";
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            STCTransferHd _stcTransferHd = new STCTransferHd();

            _stcTransferHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcTransferHd.Status = StockTransferInternalDataMapper.GetStatus(TransStatus.OnHold);
            _stcTransferHd.WrhsArea = this.WrhsAreaDropDownList.SelectedValue;
            _stcTransferHd.WrhsSrc = this.WrhsSrcDropDownList.SelectedValue;
            _stcTransferHd.WrhsSrcFgSubLed = _wrhsBL.GetWarehouseFgSubledByCode(this.WrhsSrcDropDownList.SelectedValue);
            if (this.SrcSubledDropDownList.SelectedValue != "null")
            {
                _stcTransferHd.WrhsSrcSubLed = this.SrcSubledDropDownList.SelectedValue;
            }
            else
            {
                _stcTransferHd.WrhsSrcSubLed = "";
            }
            _stcTransferHd.WrhsDest = this.WrhsDestDropDownList.SelectedValue;
            _stcTransferHd.WrhsDestFgSubled = _wrhsBL.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue);
            if (this.DestSubledDropDownList.SelectedValue != "null")
            {
                _stcTransferHd.WrhsDestSubLed = this.DestSubledDropDownList.SelectedValue;
            }
            else
            {
                _stcTransferHd.WrhsDestSubLed = "";
            }
            _stcTransferHd.Operator = this.OperatorTextBox.Text;
            _stcTransferHd.Remark = this.RemarkTextBox.Text;
            _stcTransferHd.FgProcess = YesNoDataMapper.GetYesNo(YesNo.No);
            _stcTransferHd.TGType = YesNoDataMapper.GetYesNo(YesNo.Yes).ToString();
            _stcTransferHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcTransferHd.DatePrep = DateTime.Now;

            string _result = this._stcTransInternalBL.AddSTCTransferHd(_stcTransferHd);

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

        //protected void RequestNoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.RequestNoDropDownList.SelectedValue != "null")
        //    {
        //        this.WrhsAreaSrcTextBox.Text = _wrhs.GetWrhsAreaNameByCode(_stcTransReq.GetWrhsAreaSrcFromVSTTransferReqForSJ(this.RequestNoDropDownList.SelectedValue));
        //        this.WrhsAreaDestTextBox.Text = _wrhs.GetWrhsAreaNameByCode(_stcTransReq.GetWrhsAreaDestFromVSTTransferReqForSJ(this.RequestNoDropDownList.SelectedValue));
        //        this.ShowWarehouseDest();
        //        this.ShowWarehouseSrc();
        //    }
        //    else
        //    {
        //        this.WrhsAreaDestTextBox.Text = "";
        //        this.WrhsAreaSrcTextBox.Text = "";
        //        this.WrhsSrcDropDownList.SelectedValue = "null";
        //        this.WrhsDestDropDownList.SelectedValue = "null";
        //    }
        //}

        protected void WrhsSrcDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_wrhsBL.GetWarehouseFgSubledByCode(this.WrhsSrcDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.SrcSubledDropDownList.Enabled = false;
            }
            else
            {
                this.SrcSubledDropDownList.Enabled = true;
                if (_wrhsBL.GetWarehouseFgSubledByCode(this.WrhsSrcDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.ShowCustSrc();
                }
                else if (_wrhsBL.GetWarehouseFgSubledByCode(this.WrhsSrcDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    this.ShowSuppSrc();
                }
            }
        }
        protected void WrhsDestDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_wrhsBL.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.DestSubledDropDownList.Enabled = false;
            }
            else
            {
                this.DestSubledDropDownList.Enabled = true;
                if (_wrhsBL.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.ShowCustDest();
                }
                else if (_wrhsBL.GetWarehouseFgSubledByCode(this.WrhsDestDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    this.ShowSuppDest();
                }
            }
        }

        protected void WrhsAreaDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.WrhsAreaDropDownList.SelectedValue == "null")
            {
                this.WrhsSrcDropDownList.Items.Clear();
                this.WrhsSrcDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.WrhsSrcDropDownList.SelectedValue = "null";

                this.WrhsDestDropDownList.Items.Clear();
                this.WrhsDestDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.WrhsDestDropDownList.SelectedValue = "null";
            }
            else
            {
                this.ShowWarehouseSrc();
                this.ShowWarehouseDest();
            }
        }
    }
}