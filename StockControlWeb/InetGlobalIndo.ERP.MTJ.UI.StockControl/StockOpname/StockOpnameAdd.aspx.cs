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
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockOpname
{
    public partial class StockOpnameAdd : StockOpnameBase
    {
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private StockOpnameBL _stockOpnameBL = new StockOpnameBL();
        private CustomerBL _customerBL = new CustomerBL();
        private SupplierBL _supplierBL = new SupplierBL();
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
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearData()
        {
            this.ClearLabel();

            this.DateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.WarehouseDropDownList.SelectedValue = "null";
            this.WarehouseFgSubledHiddenField.Value = "";

            this.WarehouseSubledDropDownList.Items.Clear();
            this.WarehouseSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.WarehouseSubledDropDownList.Enabled = false;

            this.OperatorTextBox.Text = "";
            this.RemarkTextBox.Text = "";
        }

        protected void ShowWarehouse()
        {
            this.WarehouseDropDownList.Items.Clear();
            this.WarehouseDropDownList.DataTextField = "WrhsName";
            this.WarehouseDropDownList.DataValueField = "WrhsCode";
            this.WarehouseDropDownList.DataSource = this._warehouseBL.GetListForDDLActive();
            this.WarehouseDropDownList.DataBind();
            this.WarehouseDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
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

        protected void ShowSupplier()
        {
            this.WarehouseSubledDropDownList.Items.Clear();
            this.WarehouseSubledDropDownList.DataTextField = "SuppName";
            this.WarehouseSubledDropDownList.DataValueField = "SuppCode";
            this.WarehouseSubledDropDownList.DataSource = this._supplierBL.GetListDDLSupp();
            this.WarehouseSubledDropDownList.DataBind();
            this.WarehouseSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
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
                else if (_tempFgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    this.ShowSupplier();

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

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            STCOpnameHd _stcOpnameHd = new STCOpnameHd();

            _stcOpnameHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcOpnameHd.Status = StockOpnameDataMapper.GetStatus(TransStatus.OnHold);
            _stcOpnameHd.WrhsCode = this.WarehouseDropDownList.SelectedValue;
            _stcOpnameHd.WrhsFgSubLed = Convert.ToChar(this.WarehouseFgSubledHiddenField.Value);
            if (this.WarehouseSubledDropDownList.SelectedValue != "null")
            {
                _stcOpnameHd.WrhsSubLed = this.WarehouseSubledDropDownList.SelectedValue;
            }
            else
            {
                _stcOpnameHd.WrhsSubLed = "";
            }
            _stcOpnameHd.Operator = this.OperatorTextBox.Text;
            _stcOpnameHd.Remark = this.RemarkTextBox.Text;
            _stcOpnameHd.DoneAdjust = YesNoDataMapper.GetYesNo(YesNo.No);

            _stcOpnameHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcOpnameHd.DatePrep = DateTime.Now;

            string _result = this._stockOpnameBL.AddSTCOpnameHd(_stcOpnameHd);

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

    }
}