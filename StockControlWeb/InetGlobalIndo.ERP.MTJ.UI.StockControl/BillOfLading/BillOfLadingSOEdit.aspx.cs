using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.BillOfLading
{
    public partial class BillOfLadingEdit : BillOfLadingBase
    {
        private BillOfLadingBL _billOfLadingBL = new BillOfLadingBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private CustomerBL _customerBL = new CustomerBL();
        private SupplierBL _supplierBL = new SupplierBL();
        private DeliveryOrderBL _deliveryOrderBL = new DeliveryOrderBL();
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";
                this.TransDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.TransDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.SetAttribute();
                //this.ShowCustomer();
                //this.ShowWarehouse();

                this.ClearLabel();
                this.ShowData();
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

        private void ShowData()
        {
            STCSJHd _stcSJHd = this._billOfLadingBL.GetSingleSTCSJHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNmbrTextBox.Text = _stcSJHd.TransNmbr;
            this.FileNmbrTextBox.Text = _stcSJHd.FileNmbr;
            this.TransDateTextBox.Text = DateFormMapper.GetValue(_stcSJHd.TransDate);
            //this.CustNameDropDownList.SelectedValue = _stcSJHd.CustCode;
            //if (_stcSJHd.CustCode != "null")
            //{
            //    this.ShowSONo();
            //    this.SONoDropDownList.SelectedValue = _stcSJHd.SONo;
            //}
            //else
            //{
            //    this.SONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            //    this.SONoDropDownList.SelectedValue = "null";
            //}
            //this.WarehouseCodeDropDownList.SelectedValue = _stcSJHd.WrhsCode;
            this.CarNoTextBox.Text = _stcSJHd.CarNo;
            this.DriverTextBox.Text = _stcSJHd.Driver;
            this.RemarkTextBox.Text = _stcSJHd.Remark;
            //this.StatusLabel.Text = BillOfLadingDataMapper.GetStatusText(_stcSJHd.Status);

            //char _fgSubled = _warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseCodeDropDownList.SelectedValue);
            //if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            //{
            //    this.WrhsSubledDropDownList.Enabled = false;
            //    this.WrhsSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            //    this.WrhsSubledDropDownList.SelectedValue = "null";
            //}
            //else
            //{
            //    this.WrhsSubledDropDownList.Enabled = true;
            //    if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            //    {
            //        this.ShowCust();
            //    }
            //    else if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
            //    {
            //        this.ShowSupp();
            //    }
            //    this.WrhsSubledDropDownList.SelectedValue = _stcSJHd.WrhsSubLed;
            //}
        }

        //private void ShowCustomer()
        //{
        //    this.CustNameDropDownList.DataTextField = "CustName";
        //    this.CustNameDropDownList.DataValueField = "CustCode";
        //    this.CustNameDropDownList.DataSource = this._customerBL.GetListCustomerForDDL();
        //    this.CustNameDropDownList.DataBind();
        //    this.CustNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        //private void ShowWarehouse()
        //{
        //    this.WarehouseCodeDropDownList.DataTextField = "WrhsName";
        //    this.WarehouseCodeDropDownList.DataValueField = "WrhsCode";
        //    this.WarehouseCodeDropDownList.DataSource = this._warehouseBL.GetListForDDLActive();
        //    this.WarehouseCodeDropDownList.DataBind();
        //    this.WarehouseCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        //private void ShowSONo()
        //{
        //    this.SONoDropDownList.Items.Clear();
        //    this.SONoDropDownList.DataTextField = "FileNmbr";
        //    this.SONoDropDownList.DataValueField = "TransNmbr";
        //    this.SONoDropDownList.DataSource = this._deliveryOrderBL.GetListSONoForDDL(this.CustNameDropDownList.SelectedValue);
        //    this.SONoDropDownList.DataBind();
        //    this.SONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        //private void ShowCust()
        //{
        //    this.WrhsSubledDropDownList.Items.Clear();
        //    this.WrhsSubledDropDownList.DataTextField = "CustName";
        //    this.WrhsSubledDropDownList.DataValueField = "CustCode";
        //    this.WrhsSubledDropDownList.DataSource = this._customerBL.GetListCustomerForDDL();
        //    this.WrhsSubledDropDownList.DataBind();
        //    this.WrhsSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        //private void ShowSupp()
        //{
        //    this.WrhsSubledDropDownList.Items.Clear();
        //    this.WrhsSubledDropDownList.DataTextField = "SuppName";
        //    this.WrhsSubledDropDownList.DataValueField = "SuppCode";
        //    this.WrhsSubledDropDownList.DataSource = this._supplierBL.GetListDDLSupp();
        //    this.WrhsSubledDropDownList.DataBind();
        //    this.WrhsSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        //protected void WarehouseCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    char _fgSubled = _warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseCodeDropDownList.SelectedValue);

        //    if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
        //    {
        //        this.WrhsSubledDropDownList.Enabled = false;
        //        this.WrhsSubledDropDownList.SelectedValue = "null";
        //    }
        //    else
        //    {
        //        this.WrhsSubledDropDownList.Enabled = true;
        //        if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
        //        {
        //            this.ShowCust();
        //        }
        //        else if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
        //        {
        //            this.ShowSupp();
        //        }
        //    }
        //}

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCSJHd _stcSJHd = this._billOfLadingBL.GetSingleSTCSJHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcSJHd.TransDate = new DateTime(DateFormMapper.GetValue(this.TransDateTextBox.Text).Year, DateFormMapper.GetValue(this.TransDateTextBox.Text).Month, DateFormMapper.GetValue(this.TransDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            //_stcSJHd.CustCode = this.CustNameDropDownList.SelectedValue;
            //_stcSJHd.SONo = this.SONoDropDownList.SelectedValue;
            //_stcSJHd.WrhsCode = this.WarehouseCodeDropDownList.SelectedValue;
            //if (this.WrhsSubledDropDownList.SelectedValue != "null")
            //{
            //    _stcSJHd.WrhsSubLed = this.WrhsSubledDropDownList.SelectedValue;
            //}
            //else
            //{
            //    _stcSJHd.WrhsSubLed = "";
            //}
            _stcSJHd.CarNo = this.CarNoTextBox.Text;
            _stcSJHd.Driver = this.DriverTextBox.Text;
            _stcSJHd.Remark = this.RemarkTextBox.Text;
            _stcSJHd.EditBy = HttpContext.Current.User.Identity.Name;
            _stcSJHd.EditDate = DateTime.Now;

            bool _result = this._billOfLadingBL.EditSTCSJHd(_stcSJHd);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPageSo + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            STCSJHd _stcSJHd = this._billOfLadingBL.GetSingleSTCSJHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcSJHd.TransDate = new DateTime(DateFormMapper.GetValue(this.TransDateTextBox.Text).Year, DateFormMapper.GetValue(this.TransDateTextBox.Text).Month, DateFormMapper.GetValue(this.TransDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            //_stcSJHd.CustCode = this.CustNameDropDownList.SelectedValue;
            //_stcSJHd.SONo = this.SONoDropDownList.SelectedValue;
            //_stcSJHd.WrhsCode = this.WarehouseCodeDropDownList.SelectedValue;
            //if (this.WrhsSubledDropDownList.SelectedValue != "null")
            //{
            //    _stcSJHd.WrhsSubLed = this.WrhsSubledDropDownList.SelectedValue;
            //}
            //else
            //{
            //    _stcSJHd.WrhsSubLed = "";
            //}
            _stcSJHd.CarNo = this.CarNoTextBox.Text;
            _stcSJHd.Driver = this.DriverTextBox.Text;
            _stcSJHd.Remark = this.RemarkTextBox.Text;
            _stcSJHd.EditBy = HttpContext.Current.User.Identity.Name;
            _stcSJHd.EditDate = DateTime.Now;

            bool _result = this._billOfLadingBL.EditSTCSJHd(_stcSJHd);

            if (_result == true)
            {

                Response.Redirect(this._viewPageSo + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        //protected void CustNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.CustNameDropDownList.SelectedValue != "null")
        //    {
        //        this.ShowSONo();
        //    }
        //    else
        //    {
        //        this.SONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //        this.SONoDropDownList.SelectedValue = "null";
        //    }
        //}
    }
}