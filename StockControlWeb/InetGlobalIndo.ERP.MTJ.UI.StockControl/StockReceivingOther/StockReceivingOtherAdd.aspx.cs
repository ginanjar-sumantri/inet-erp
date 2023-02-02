using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockReceivingOther
{
    public partial class StockReceivingOtherAdd : StockReceivingOtherBase
    {
        private StockReceivingOtherBL _stockReceivingOtherBL = new StockReceivingOtherBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private StockTypeBL _stockTypeBL = new StockTypeBL();
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
                this.SJDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.SJDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ClearLabel();
                this.SetAttribute();
                this.ShowWarehouse();
                this.ShowStockType();
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
            this.SJDateTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearData()
        {
            this.ClearLabel();

            this.DateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.WarehouseDropDownList.SelectedValue = "null";
            this.StockTypeDropDownList.SelectedValue = "null";
            this.ReceiveFromTextBox.Text = "";
            this.CarNoTextBox.Text = "";
            this.DriverTextBox.Text = "";
            this.SJNoTextBox.Text = "";
            this.SJDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.RemarkTextBox.Text = "";
        }

        protected void ShowWarehouse()
        {
            this.WarehouseDropDownList.Items.Clear();
            this.WarehouseDropDownList.DataTextField = "WrhsName";
            this.WarehouseDropDownList.DataValueField = "WrhsCode";
            this.WarehouseDropDownList.DataSource = this._warehouseBL.GetListForDDLNoSubled();
            this.WarehouseDropDownList.DataBind();
            this.WarehouseDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowStockType()
        {
            this.StockTypeDropDownList.Items.Clear();
            this.StockTypeDropDownList.DataTextField = "StockTypeName";
            this.StockTypeDropDownList.DataValueField = "StockTypeCode";
            this.StockTypeDropDownList.DataSource = this._stockTypeBL.GetListForDDLStockReceivingOther();
            this.StockTypeDropDownList.DataBind();
            this.StockTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            STCRROtherHd _stcRROtherHd = new STCRROtherHd();

            _stcRROtherHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcRROtherHd.Status = StockReceivingOtherDataMapper.GetStatus(TransStatus.OnHold);
            _stcRROtherHd.WrhsCode = this.WarehouseDropDownList.SelectedValue;
            _stcRROtherHd.WrhsFgSubLed = WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled);
            _stcRROtherHd.StockType = this.StockTypeDropDownList.SelectedValue;
            _stcRROtherHd.ReceiveFrom = this.ReceiveFromTextBox.Text;
            _stcRROtherHd.CarNo = this.CarNoTextBox.Text;
            _stcRROtherHd.Driver = this.DriverTextBox.Text;
            _stcRROtherHd.SJReffNo = this.SJNoTextBox.Text;
            _stcRROtherHd.SJReffDate = DateFormMapper.GetValue(this.SJDateTextBox.Text);
            _stcRROtherHd.Remark = this.RemarkTextBox.Text;
            _stcRROtherHd.FgProcess = YesNoDataMapper.GetYesNo(YesNo.No);
            _stcRROtherHd.RRType = StockReceivingOtherDataMapper.GetRRTypeStatus(RRTypeStatus.Other).ToString();

            _stcRROtherHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcRROtherHd.DatePrep = DateTime.Now;

            string _result = this._stockReceivingOtherBL.AddSTCRROtherHd(_stcRROtherHd);

            if (_result != "")
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
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
    }
}