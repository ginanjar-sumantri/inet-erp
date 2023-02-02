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
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.ReceivingPO
{
    public partial class ReceivingPO_ReceivingPOEdit : ReceivingPOBase
    {
        private ReceivingPOBL _receivingPOBL = new ReceivingPOBL();
        private PurchaseOrderBL _purchaseOrderBL = new PurchaseOrderBL();
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
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.SJSuppDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.SJSuppDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                this.ShowLocation();

                this.SetAttribute();
                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.SJSuppDateTextBox.Attributes.Add("ReadOnly", "True");
            this.FgLocationCheckBox.Attributes.Add("OnClick", "CheckUncheck(" + this.FgLocationCheckBox.ClientID + "," + this.WrhsLocationDropDownList.ClientID + ");");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
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
            STCReceiveHd _stcReceiveHd = this._receivingPOBL.GetSingleSTCReceiveHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.WrhsLocationDropDownList.Items.Clear();
            this.WrhsLocationDropDownList.DataTextField = "WLocationName";
            this.WrhsLocationDropDownList.DataValueField = "WLocationCode";
            this.WrhsLocationDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(_stcReceiveHd.WrhsCode);
            this.WrhsLocationDropDownList.DataBind();
            this.WrhsLocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowData()
        {
            STCReceiveHd _stcReceiveHd = this._receivingPOBL.GetSingleSTCReceiveHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNoTextBox.Text = _stcReceiveHd.TransNmbr;
            this.FileNoTextBox.Text = _stcReceiveHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_stcReceiveHd.TransDate);
            this.SJSuppDateTextBox.Text = DateFormMapper.GetValue(_stcReceiveHd.SJSupplierDate);
            this.RemarkTextBox.Text = _stcReceiveHd.Remark;
            this.SJSuppNoTextBox.Text = _stcReceiveHd.SJSupplierNo;
            this.CarNoTextBox.Text = _stcReceiveHd.CarNo;
            this.DriverTextBox.Text = _stcReceiveHd.Driver;
            this.SupplierTextBox.Text = _suppBL.GetSuppNameByCode(_stcReceiveHd.SuppCode);
            this.WarehouseTextBox.Text = _warehouseBL.GetWarehouseNameByCode(_stcReceiveHd.WrhsCode);
            if (_stcReceiveHd.WrhsFgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.SubledDropDownList.DataBind();
                this.SubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.SubledDropDownList.Enabled = false;
            }
            else
            {
                this.SubledDropDownList.Enabled = true;
                if (_stcReceiveHd.WrhsFgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.ShowCust();
                }
                else if (_stcReceiveHd.WrhsFgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    this.ShowSupp();
                }
            }
            this.SubledDropDownList.SelectedValue = _stcReceiveHd.WrhsSubLed;
            this.PONoTextBox.Text = (_stcReceiveHd.PONo == "null") ? "" : _purchaseOrderBL.GetFileNmbrPRCPOHd(_stcReceiveHd.PONo, 0);
            this.FgLocationCheckBox.Checked = _stcReceiveHd.FgLocation;
            if (_stcReceiveHd.FgLocation == true)
            {
                this.WrhsLocationDropDownList.Enabled = true;
            }
            else
            {
                this.WrhsLocationDropDownList.Enabled = false;
            }
            this.WrhsLocationDropDownList.SelectedValue = _stcReceiveHd.LocationCode;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCReceiveHd _stcReceiveHd = this._receivingPOBL.GetSingleSTCReceiveHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcReceiveHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            if (this.SubledDropDownList.SelectedValue != "null")
            {
                _stcReceiveHd.WrhsSubLed = this.SubledDropDownList.SelectedValue;
            }
            else
            {
                _stcReceiveHd.WrhsSubLed = "";
            }
            _stcReceiveHd.SJSupplierNo = this.SJSuppNoTextBox.Text;
            _stcReceiveHd.SJSupplierDate = DateFormMapper.GetValue(this.SJSuppDateTextBox.Text);
            _stcReceiveHd.CarNo = this.CarNoTextBox.Text;
            _stcReceiveHd.Driver = this.DriverTextBox.Text;
            _stcReceiveHd.Remark = this.RemarkTextBox.Text;
            _stcReceiveHd.EditBy = HttpContext.Current.User.Identity.Name;
            _stcReceiveHd.EditDate = DateTime.Now;
            _stcReceiveHd.FgLocation = this.FgLocationCheckBox.Checked;
            _stcReceiveHd.LocationCode = this.WrhsLocationDropDownList.SelectedValue;

            String _result = this._receivingPOBL.EditSTCReceiveHd(_stcReceiveHd);

            if (_result.Length > 0)
            {
                if (_result.Substring(0, 5) == ApplicationConfig.Error)
                {
                    this.WarningLabel.Text = _result.Substring(6, _result.Length - 6);
                }
                else
                {
                    Response.Redirect(this._homePage);
                }

            }
            else
            {
                Response.Redirect(this._homePage);
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            STCReceiveHd _stcReceiveHd = this._receivingPOBL.GetSingleSTCReceiveHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcReceiveHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            if (this.SubledDropDownList.SelectedValue != "null")
            {
                _stcReceiveHd.WrhsSubLed = this.SubledDropDownList.SelectedValue;
            }
            else
            {
                _stcReceiveHd.WrhsSubLed = "";
            }
            _stcReceiveHd.SJSupplierNo = this.SJSuppNoTextBox.Text;
            _stcReceiveHd.SJSupplierDate = DateFormMapper.GetValue(this.SJSuppDateTextBox.Text);
            _stcReceiveHd.CarNo = this.CarNoTextBox.Text;
            _stcReceiveHd.Driver = this.DriverTextBox.Text;
            _stcReceiveHd.Remark = this.RemarkTextBox.Text;
            _stcReceiveHd.EditBy = HttpContext.Current.User.Identity.Name;
            _stcReceiveHd.EditDate = DateTime.Now;
            _stcReceiveHd.FgLocation = this.FgLocationCheckBox.Checked;
            _stcReceiveHd.LocationCode = this.WrhsLocationDropDownList.SelectedValue;

            String _result = this._receivingPOBL.EditSTCReceiveHd(_stcReceiveHd);
            if (_result.Length > 0)
            {
                if (_result.Substring(0, 5) == ApplicationConfig.Error)
                {
                    this.WarningLabel.Text = _result.Substring(6, _result.Length - 6);
                }
                else
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
            }
            else
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
        }
    }
}