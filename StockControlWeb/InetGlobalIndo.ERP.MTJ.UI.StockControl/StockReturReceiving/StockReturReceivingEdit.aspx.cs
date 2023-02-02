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
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockReturReceiving
{
    public partial class StockReturReceivingEdit : StockReturReceivingBase
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

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.SetAttribute();
                this.ClearLabel();
                //this.ShowCustomer();
                this.ShowLocation();
                this.ShowWarehouse();
                this.ShowData();
            }
        }

        private void SetAttribute()
        {
            this.TransDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.TransDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

            this.PageTitleLiteral.Text = this._pageTitleLiteral;

            this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
            this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
            this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
            this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
            this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

            this.TransDateTextBox.Attributes.Add("ReadOnly", "True");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowData()
        {
            STCReturRRHd _stcReturRRHd = this._stcReturBL.GetSingleSTCReturRRHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNmbrTextBox.Text = _stcReturRRHd.TransNmbr;
            this.FileNoTextBox.Text = _stcReturRRHd.FileNmbr;
            this.TransDateTextBox.Text = DateFormMapper.GetValue(_stcReturRRHd.TransDate);
            //this.CustNameDropDownList.SelectedValue = _stcReturRRHd.CustCode;
            //if (CustNameDropDownList.SelectedValue != "null")
            //{
            //    this.ShowReqRetur();
            //    this.ReqReturNoDropDownList.SelectedValue = _stcReturRRHd.ReqReturNo;
            //}
            //else
            //{
            //    this.ReqReturNoDropDownList.Items.Clear();
            //    this.ReqReturNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            //    this.ReqReturNoDropDownList.SelectedValue = "null";
            //}
            this.CustNameTextBox.Text = this._custBL.GetNameByCode(_stcReturRRHd.CustCode);
            this.ReqReturNoTextBox.Text = this._reqSalesReturBL.GetFileNmbrMKTReqReturHd(_stcReturRRHd.ReqReturNo);
            this.WarehouseDropDownList.SelectedValue = _stcReturRRHd.WrhsCode;
            this.ShowLocation();
            this.WarehouseDropDownList_SelectedIndexChanged(null, null);
            if (_stcReturRRHd.WrhsFgSubLed.ToString().Trim().ToLower() == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer).ToString().Trim().ToLower())
            {
                this.SubledDropDownList.SelectedValue = _stcReturRRHd.WrhsSubLed;
            }
            else if (_stcReturRRHd.WrhsFgSubLed.ToString().Trim().ToLower() == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier).ToString().Trim().ToLower())
            {
                this.SubledDropDownList.SelectedValue = _stcReturRRHd.WrhsSubLed;
            }
            else if (_stcReturRRHd.WrhsFgSubLed.ToString().Trim().ToLower() == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled).ToString().Trim().ToLower())
            {
                this.SubledDropDownList.SelectedValue = "";
            }
            else
            {
                this.SubledDropDownList.SelectedValue = "";
            }
            if (_stcReturRRHd.WrhsFgSubLed != 'N')
            {
                String _locationCode = this._stcReturBL.GetLocationCodeSTCReturSJDt(_stcReturRRHd.TransNmbr);
                this.WrhsLocationDropDownList.SelectedValue = _locationCode;
            }
            else
            {
                this.WrhsLocationDropDownList.SelectedIndex = 0;
            }
            this.RemarkTextBox.Text = _stcReturRRHd.Remark;
        }

        //private void ShowCustomer()
        //{
        //    this.CustNameDropDownList.DataTextField = "CustName";
        //    this.CustNameDropDownList.DataValueField = "CustCode";
        //    this.CustNameDropDownList.DataSource = this._custBL.GetListCustomerForDDL();
        //    this.CustNameDropDownList.DataBind();
        //    this.CustNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        //private void ShowReqRetur()
        //{
        //    this.ReqReturNoDropDownList.Items.Clear();
        //    this.ReqReturNoDropDownList.DataTextField = "FileNmbr";
        //    this.ReqReturNoDropDownList.DataValueField = "TransNmbr";
        //    this.ReqReturNoDropDownList.DataSource = this._reqSalesReturBL.GetListReqReturForDDL(this.CustNameDropDownList.SelectedValue);
        //    this.ReqReturNoDropDownList.DataBind();
        //    this.ReqReturNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        private void ShowWarehouse()
        {
            this.WarehouseDropDownList.DataTextField = "WrhsName";
            this.WarehouseDropDownList.DataValueField = "WrhsCode";
            this.WarehouseDropDownList.DataSource = this._warehouseBL.GetListForDDLActive();
            this.WarehouseDropDownList.DataBind();
            this.WarehouseDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
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

        protected void CheckValidData()
        {
            String _errorMsg = "";
            this.ClearLabel();

            //if (this.CustNameDropDownList.SelectedValue == "null")
            //    _errorMsg = _errorMsg + " Please Choose One of Customer.";

            //if (this.ReqReturNoDropDownList.SelectedValue == "null")
            //    _errorMsg = _errorMsg + " Please Choose One of RR ReturNo.";

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
                STCReturRRHd _stcReturRRHd = this._stcReturBL.GetSingleSTCReturRRHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                _stcReturRRHd.TransDate = new DateTime(DateFormMapper.GetValue(this.TransDateTextBox.Text).Year, DateFormMapper.GetValue(this.TransDateTextBox.Text).Month, DateFormMapper.GetValue(this.TransDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                //_stcReturRRHd.CustCode = this.CustNameDropDownList.SelectedValue;
                //_stcReturRRHd.ReqReturNo = this.ReqReturNoDropDownList.SelectedValue;
                _stcReturRRHd.WrhsCode = this.WarehouseDropDownList.SelectedValue;
                _stcReturRRHd.WrhsFgSubLed = _warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue);
                _stcReturRRHd.WrhsSubLed = this.SubledDropDownList.SelectedValue;
                _stcReturRRHd.Remark = this.RemarkTextBox.Text;

                _stcReturRRHd.EditBy = HttpContext.Current.User.Identity.Name;
                _stcReturRRHd.EditDate = DateTime.Now;

                bool _result = this._stcReturBL.EditSTCReturRRHd(_stcReturRRHd, this.FgLocationCheckBox.Checked, this.WrhsLocationDropDownList.SelectedValue);

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
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                STCReturRRHd _stcReturRRHd = this._stcReturBL.GetSingleSTCReturRRHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                _stcReturRRHd.TransDate = new DateTime(DateFormMapper.GetValue(this.TransDateTextBox.Text).Year, DateFormMapper.GetValue(this.TransDateTextBox.Text).Month, DateFormMapper.GetValue(this.TransDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                //_stcReturRRHd.CustCode = this.CustNameDropDownList.SelectedValue;
                //_stcReturRRHd.ReqReturNo = this.ReqReturNoDropDownList.SelectedValue;
                _stcReturRRHd.WrhsCode = this.WarehouseDropDownList.SelectedValue;
                _stcReturRRHd.WrhsFgSubLed = _warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue);
                _stcReturRRHd.WrhsSubLed = this.SubledDropDownList.SelectedValue;
                _stcReturRRHd.Remark = this.RemarkTextBox.Text;

                _stcReturRRHd.EditBy = HttpContext.Current.User.Identity.Name;
                _stcReturRRHd.EditDate = DateTime.Now;

                bool _result = this._stcReturBL.EditSTCReturRRHd(_stcReturRRHd, this.FgLocationCheckBox.Checked, this.WrhsLocationDropDownList.SelectedValue);

                if (_result == true)
                {
                    Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.WarningLabel.Text = "Your Failed Edit Data";
                }
            }
        }

        //protected void CustNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.CustNameDropDownList.SelectedValue != "null")
        //    {
        //        this.ShowReqRetur();
        //    }
        //    else
        //    {
        //        this.ReqReturNoDropDownList.Items.Clear();
        //        this.ReqReturNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //        this.ReqReturNoDropDownList.SelectedValue = "null";
        //    }
        //}

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