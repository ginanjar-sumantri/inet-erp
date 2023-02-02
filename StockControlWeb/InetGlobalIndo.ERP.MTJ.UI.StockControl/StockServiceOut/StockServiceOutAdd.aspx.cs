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
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using System.Collections.Generic;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockServiceOut
{
    public partial class StockServiceOutAdd : StockServiceOutBase
    {
        private StockServiceInBL _stockServiceInBL = new StockServiceInBL();
        private StockServiceOutBL _stockServiceOutBL = new StockServiceOutBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private CustomerBL _customerBL = new CustomerBL();
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
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ClearLabel();
                this.SetAttribute();
                this.ShowWarehouse();
                this.ShowCustomer2();
                this.ClearData();
                this.ShowRRNo(this.CustomerDropDownList.SelectedValue);
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
            this.CustomerDropDownList.AutoPostBack = true;
            this.RRNoDropDownList.AutoPostBack = true;
            this.WarehouseDropDownList.AutoPostBack = true;
        }

        protected void ShowWarehouse()
        {
            this.WarehouseDropDownList.Items.Clear();
            this.WarehouseDropDownList.DataTextField = "WrhsName";
            this.WarehouseDropDownList.DataValueField = "WrhsCode";
            this.WarehouseDropDownList.DataSource = this._warehouseBL.GetListForDDLCustomerSubled();
            this.WarehouseDropDownList.DataBind();
            this.WarehouseDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowRRNo(String _prmCustCode)
        {
            this.RRNoDropDownList.Items.Clear();
            this.RRNoDropDownList.DataTextField = "TransNmbr";
            this.RRNoDropDownList.DataValueField = "TransNmbr";
            this.RRNoDropDownList.DataSource = this._stockServiceOutBL.GetListSTCServiceInDtPosting(_prmCustCode);
            this.RRNoDropDownList.DataBind();
            this.RRNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowCustomer()
        {
            this.WarehouseSubledDropDownList.Items.Clear();
            this.WarehouseSubledDropDownList.DataTextField = "CustName";
            this.WarehouseSubledDropDownList.DataValueField = "CustCode";
            this.WarehouseSubledDropDownList.DataSource = this._customerBL.GetListCustomerForDDL();
            this.WarehouseSubledDropDownList.DataBind();
            this.WarehouseSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowCustomer2()
        {
            this.CustomerDropDownList.Items.Clear();
            this.CustomerDropDownList.DataTextField = "CustName";
            this.CustomerDropDownList.DataValueField = "CustCode";
            this.CustomerDropDownList.DataSource = this._customerBL.GetListCustomerForDDL();
            this.CustomerDropDownList.DataBind();
            this.CustomerDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void WarehouseDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.WarehouseDropDownList.SelectedValue != "null")
            {
                char _tempFgSubled = this._warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue);

                this.WarehouseFgSubledHiddenField.Value = _tempFgSubled.ToString();

                if (_tempFgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.ShowCustomer();

                    this.WarehouseSubledDropDownList.Enabled = true;
                }
                else if (_tempFgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
                {
                    this.WarehouseSubledDropDownList.Items.Clear();
                    this.WarehouseSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                    this.WarehouseSubledDropDownList.Enabled = false;
                }
                else
                {
                    this.WarehouseSubledDropDownList.Items.Clear();
                    this.WarehouseSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                    this.WarehouseSubledDropDownList.Enabled = false;
                }

            }
            else
            {
                this.WarehouseFgSubledHiddenField.Value = "";

                this.WarehouseSubledDropDownList.Items.Clear();
                this.WarehouseSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.WarehouseSubledDropDownList.Enabled = false;
            }
        }

        protected void ClearData()
        {
            this.ClearLabel();

            this.DateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.CustomerDropDownList.SelectedValue = "null";
            this.WarehouseDropDownList.SelectedValue = "null";
            this.WarehouseFgSubledHiddenField.Value = "";

            this.WarehouseSubledDropDownList.Items.Clear();
            this.WarehouseSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.WarehouseSubledDropDownList.Enabled = false;

            this.ReceivedByTextBox.Text = "";
            this.RemarkTextBox.Text = "";
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            STCServiceOutHd _stcServiceOutHd = new STCServiceOutHd();

            _stcServiceOutHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcServiceOutHd.Status = StockServiceOutDataMapper.GetStatus(TransStatus.OnHold);
            if (this.CustomerDropDownList.SelectedValue != "null")
            {
                _stcServiceOutHd.CustCode = this.CustomerDropDownList.SelectedValue;
            }
            else
            {
                _stcServiceOutHd.CustCode = null;
            }
            _stcServiceOutHd.WrhsCode = this.WarehouseDropDownList.SelectedValue;
            _stcServiceOutHd.WrhsFgSubLed = Convert.ToChar(this.WarehouseFgSubledHiddenField.Value);
            if (this.WarehouseSubledDropDownList.SelectedValue != "null")
            {
                _stcServiceOutHd.WrhsSubLed = this.WarehouseSubledDropDownList.SelectedValue;
            }
            else
            {
                _stcServiceOutHd.WrhsSubLed = "";
            }

            _stcServiceOutHd.IssuedBy = this.ReceivedByTextBox.Text;
            _stcServiceOutHd.Remark = this.RemarkTextBox.Text;

            _stcServiceOutHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcServiceOutHd.DatePrep = DateTime.Now;
            _stcServiceOutHd.RRNo = RRNoDropDownList.SelectedValue;
            //string _result = this._stockServiceOutBL.AddSTCServiceOutHd(_stcServiceOutHd);
            string _result = this._stockServiceOutBL.AddSTCServiceOutHdDt(_stcServiceOutHd);

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

        protected void CustomerDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowRRNo(this.CustomerDropDownList.SelectedValue);
        }

        protected void RRNoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            STCServiceInHd _sTCServiceInHd = this._stockServiceInBL.GetSingleSTCServiceInHd(this.RRNoDropDownList.SelectedValue);
            if (_sTCServiceInHd != null)
            {
                this.WarehouseDropDownList.SelectedValue = _sTCServiceInHd.WrhsCode;
                this.WarehouseDropDownList_SelectedIndexChanged(null, null);
            }
        }
    }
}