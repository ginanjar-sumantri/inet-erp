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
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Collections.Generic;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.DeliveryOrder
{
    public partial class DeliveryOrderAdd : DeliveryOrderBase
    {
        private CustomerBL _cust = new CustomerBL();
        private DeliveryOrderBL _deliveryOrder = new DeliveryOrderBL();
        private SalesOrderBL _salesOrder = new SalesOrderBL();
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
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.ShowCust();
                this.ShowSONo();

                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.POCustNoTextBox.Attributes.Add("ReadOnly", "True");
            this.DeliveryToTextBox.Attributes.Add("ReadOnly", "True");
            this.DeliveryDateTextBox.Attributes.Add("ReadOnly", "True");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowCust()
        {
            this.CustDropDownList.Items.Clear();
            this.CustDropDownList.DataTextField = "CustName";
            this.CustDropDownList.DataValueField = "CustCode";
            this.CustDropDownList.DataSource = this._cust.GetListCustomerForDDL();
            this.CustDropDownList.DataBind();
            this.CustDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowSONo()
        {
            this.SONoDropDownList.Items.Clear();
            this.SONoDropDownList.DataTextField = "FileNmbr";
            this.SONoDropDownList.DataValueField = "TransNmbr";
            this.SONoDropDownList.DataSource = this._salesOrder.GetSONoFromVMKSOForDO(this.CustDropDownList.SelectedValue);
            this.SONoDropDownList.DataBind();
            this.SONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

            //this.SONoDropDownList.Items.Clear();
            //this.SONoDropDownList.DataTextField = "TransNmbr";
            //this.SONoDropDownList.DataValueField = "TransNmbr";
            //this.SONoDropDownList.DataSource = this._salesOrder.GetTransNoFromMKTSOProduct();
            //this.SONoDropDownList.DataBind();
            //this.SONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

        }

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ClearLabel();

            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            this.CustDropDownList.SelectedValue = "null";
            this.SONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.SONoDropDownList.SelectedValue = "null";
            this.DeliveryToTextBox.Text = "";
            this.DeliveryDateTextBox.Text = "";
            this.RemarkTextBox.Text = "";
            this.POCustNoTextBox.Text = "";
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            MKTDOHd _mktDOHd = new MKTDOHd();

            _mktDOHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _mktDOHd.Status = DeliveryOrderDataMapper.GetStatus(TransStatus.OnHold);
            _mktDOHd.CustCode = this.CustDropDownList.SelectedValue;
            _mktDOHd.SONo = this.SONoDropDownList.SelectedValue;
            _mktDOHd.POCustNo = this.POCustNoTextBox.Text;
            _mktDOHd.Remark = this.RemarkTextBox.Text;
            _mktDOHd.DeliveryTo = _salesOrder.GetDeliveryToFromVMKSOForDO(this.SONoDropDownList.SelectedValue);
            _mktDOHd.DeliveryDate = DateFormMapper.GetValue(this.DeliveryDateTextBox.Text);
            _mktDOHd.DOType = AppModule.GetValue(TransactionType.DeliveryOrder);
            _mktDOHd.CreatedBy = HttpContext.Current.User.Identity.Name;
            _mktDOHd.CreatedDate = DateTime.Now;
            _mktDOHd.EditBy = HttpContext.Current.User.Identity.Name;
            _mktDOHd.EditDate = DateTime.Now;

            List<MKTDODt> _mktDODtList = new List<MKTDODt>();

            List<MKTSOProduct> _mktSODtList = _salesOrder.GetListMKTSOProduct(this.SONoDropDownList.SelectedValue, _salesOrder.GetLastRevisiMKTSOHd(this.SONoDropDownList.SelectedValue));

            foreach (MKTSOProduct _item in _mktSODtList)
            {
                MKTDODt _mktDODt = new MKTDODt();

                _mktDODt.ProductCode = _item.ProductCode;
                _mktDODt.ItemID = _item.ItemID;
                _mktDODt.Unit = _item.Unit;
                _mktDODt.Qty = (_item.Qty == null) ? 0 : Convert.ToDecimal(_item.Qty);
                _mktDODt.Remark = "";
                _mktDODt.DoneClosing = DeliveryOrderDataMapper.GetStatusDetail(DeliveryOrderStatusDt.Open);
                _mktDODt.QtyClose = 0;
                _mktDODt.QtySJ = 0;

                //_mktDODtList.Add(new MKTDODt("", _mktDODt.ProductCode, _mktDODt.Qty, _mktDODt.Unit, _mktDODt.Remark, _mktDODt.DoneClosing, _mktDODt.QtyClose, _mktDODt.QtySJ));
                _mktDODtList.Add(_mktDODt);
            }

            string _result = this._deliveryOrder.AddMKTDOHd(_mktDOHd, _mktDODtList);

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

        protected void SONoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SONoDropDownList.SelectedValue != "null")
            {
                this.POCustNoTextBox.Text = _salesOrder.GetPOCustNoFromVMKSOForDO(this.SONoDropDownList.SelectedValue);
                this.DeliveryToTextBox.Text = _cust.GetCustAddressNameByCode(this.CustDropDownList.SelectedValue, _salesOrder.GetDeliveryToFromVMKSOForDO(this.SONoDropDownList.SelectedValue));
                this.DeliveryDateTextBox.Text = _salesOrder.GetDeliveryDateFromVMKSOForDO(this.SONoDropDownList.SelectedValue);
            }
            else
            {
                this.POCustNoTextBox.Text = "";
                this.DeliveryToTextBox.Text = "";
                this.DeliveryDateTextBox.Text = "";
            }
        }

        protected void CustDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CustDropDownList.SelectedValue != "null")
            {
                this.ShowSONo();
            }
            else
            {
                this.SONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.SONoDropDownList.SelectedValue = "null";
            }
        }
    }
}