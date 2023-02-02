using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Collections.Generic;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.BillOfLading
{
    public partial class BillOfLadingSOAdd : BillOfLadingBase
    {
        private BillOfLadingBL _billOfLadingBL = new BillOfLadingBL();
        private CustomerBL _custBL = new CustomerBL();
        private SupplierBL _suppBL = new SupplierBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
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

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.TransDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.TransDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.SetAttribute();

                this.ShowCustomer();
                

                this.ClearData();

            }
        }

        private void SetAttribute()
        {
            this.TransDateTextBox.Attributes.Add("ReadOnly", "True");
            
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
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
            this.CustNameDropDownList.SelectedValue = "null";
            this.RemarkTextBox.Text = "";
            
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            STCSJHd _stcSJHd = new STCSJHd();

            _stcSJHd.Status = BillOfLadingDataMapper.GetStatus(TransStatus.OnHold);
            _stcSJHd.TransDate = new DateTime(DateFormMapper.GetValue(this.TransDateTextBox.Text).Year, DateFormMapper.GetValue(this.TransDateTextBox.Text).Month, DateFormMapper.GetValue(this.TransDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcSJHd.CustCode = this.CustNameDropDownList.SelectedValue;
            
            _stcSJHd.CarNo = this.CarNoTextBox.Text;
            _stcSJHd.Driver = this.DriverTextBox.Text;
            _stcSJHd.Remark = this.RemarkTextBox.Text;
            _stcSJHd.CreatedBy = HttpContext.Current.User.Identity.Name;
            _stcSJHd.CreatedDate = DateTime.Now;
            _stcSJHd.EditBy = HttpContext.Current.User.Identity.Name;
            _stcSJHd.EditDate = DateTime.Now;


            //List<STCSJDt> _listDetail = new List<STCSJDt>();

            //if (this.FgLocationCheckBox.Checked == true)
            //{
            //    List<MKTDODt> _mktDODt = _deliveryOrderBL.GetListMKTDODt(this.SONoDropDownList.SelectedValue);

            //    foreach (MKTDODt _item in _mktDODt)
            //    {
            //        STCSJDt _stcSJDt = new STCSJDt();

            //        _stcSJDt.DONo = _item.TransNmbr;
            //        _stcSJDt.ProductCode = _item.ProductCode;
            //        _stcSJDt.LocationCode = this.WrhsLocationDropDownList.SelectedValue;
            //        _stcSJDt.Unit = _item.Unit;
            //        _stcSJDt.Qty = _item.Qty;
            //        _stcSJDt.Remark = "";
            //        _stcSJDt.QtyLoss = 0;
            //        _stcSJDt.QtyRetur = 0;
            //        _stcSJDt.QtyReceive = _item.Qty;

            //        _listDetail.Add(new STCSJDt("", _stcSJDt.ProductCode, _stcSJDt.LocationCode, _stcSJDt.DONo, _stcSJDt.Qty, _stcSJDt.Unit, _stcSJDt.Remark, _stcSJDt.QtyLoss, _stcSJDt.QtyRetur, _stcSJDt.QtyReceive));
            //    }
            //}
            //string _result = this._billOfLadingBL.AddSTCSJHd(_stcSJHd, _listDetail);

            String _result = this._billOfLadingBL.AddSTCSJHd(_stcSJHd);

            if (_result != "")
            {
                Response.Redirect(this._viewPageSo + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
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

        public void ShowCustomer()
        {
            this.CustNameDropDownList.Items.Clear();
            this.CustNameDropDownList.DataTextField = "CustName";
            this.CustNameDropDownList.DataValueField = "CustCode";
            this.CustNameDropDownList.DataSource = this._custBL.GetListCustomerForDDL();
            this.CustNameDropDownList.DataBind();
            this.CustNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void CustNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CustNameDropDownList.SelectedValue != "null")
            {
                //this.ShowSONo();
            }
        }
    }
}