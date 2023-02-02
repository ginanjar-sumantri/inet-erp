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
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockAdjustment
{
    public partial class StockAdjustmentAdd : StockAdjustmentBase
    {
        private StockAdjustmentBL _stockAdjustBL = new StockAdjustmentBL();
        private StockOpnameBL _stockOpnameBL = new StockOpnameBL();
        private StockTypeBL _stockTypeBL = new StockTypeBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private CustomerBL _cust = new CustomerBL();
        private SupplierBL _supp = new SupplierBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRate = new CurrencyRateBL();

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
                this.DateTextLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowOpnameNo();
                this.ShowStockType();
                this.ShowWarehouse();
                this.ShowCurrency();

                this.SetAttribute();
                this.ClearLabel();
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

        public void ShowOpnameNo()
        {
            this.OpnameNoDropDownList.Items.Clear();
            this.OpnameNoDropDownList.DataTextField = "FileNmbr";
            this.OpnameNoDropDownList.DataValueField = "TransNmbr";
            this.OpnameNoDropDownList.DataSource = this._stockOpnameBL.GetListSTCOpnameForDDL();
            this.OpnameNoDropDownList.DataBind();
            this.OpnameNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowStockType()
        {
            this.StockTypeDropDownList.Items.Clear();
            this.StockTypeDropDownList.DataTextField = "StockTypeName";
            this.StockTypeDropDownList.DataValueField = "StockTypeCode";
            this.StockTypeDropDownList.DataSource = this._stockTypeBL.GetListStockTypeDtForDDL();
            this.StockTypeDropDownList.DataBind();
            this.StockTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowWarehouse()
        {
            this.WarehouseDropDownList.Items.Clear();
            this.WarehouseDropDownList.DataTextField = "WrhsName";
            this.WarehouseDropDownList.DataValueField = "WrhsCode";
            this.WarehouseDropDownList.DataSource = this._warehouseBL.GetListForDDLActive();
            this.WarehouseDropDownList.DataBind();
            this.WarehouseDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowCust()
        {
            this.SubledDropDownList.Items.Clear();
            this.SubledDropDownList.DataTextField = "CustName";
            this.SubledDropDownList.DataValueField = "CustCode";
            this.SubledDropDownList.DataSource = this._cust.GetListCustomerForDDL();
            this.SubledDropDownList.DataBind();
            this.SubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowSupp()
        {
            this.SubledDropDownList.Items.Clear();
            this.SubledDropDownList.DataTextField = "SuppName";
            this.SubledDropDownList.DataValueField = "SuppCode";
            this.SubledDropDownList.DataSource = this._supp.GetListDDLSupp();
            this.SubledDropDownList.DataBind();
            this.SubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowCurrency()
        {
            this.CurrencyDropDownList.DataTextField = "CurrCode";
            this.CurrencyDropDownList.DataValueField = "CurrCode";
            this.CurrencyDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrencyDropDownList.DataBind();
            this.CurrencyDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            this.OpnameNoDropDownList.SelectedValue = "null";
            this.RemarkTextBox.Text = "";
            this.OperatorTextBox.Text = "";
            this.WarehouseDropDownList.SelectedValue = "null";
            this.SubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.SubledDropDownList.SelectedValue = "null";
            this.StockTypeDropDownList.SelectedValue = "null";
            this.CurrencyDropDownList.SelectedValue = "null";
            this.ForexRateTextBox.Text = "0";
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            STCAdjustHd _stcAdjustHd = new STCAdjustHd();

            _stcAdjustHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcAdjustHd.Status = StockAdjustmentDataMapper.GetStatus(TransStatus.OnHold);
            _stcAdjustHd.OpnameNo = this.OpnameNoDropDownList.SelectedValue;
            _stcAdjustHd.Remark = this.RemarkTextBox.Text;
            _stcAdjustHd.Operator = this.OperatorTextBox.Text;
            _stcAdjustHd.WrhsCode = this.WarehouseDropDownList.SelectedValue;
            _stcAdjustHd.WrhsFgSubLed = _warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue);
            if (this.SubledDropDownList.SelectedValue != "null")
            {
                _stcAdjustHd.WrhsSubled = this.SubledDropDownList.SelectedValue;
            }
            else
            {
                _stcAdjustHd.WrhsSubled = "";
            }
            _stcAdjustHd.StockType = this.StockTypeDropDownList.SelectedValue;
            _stcAdjustHd.FgProcess = YesNoDataMapper.GetYesNo(YesNo.No);
            _stcAdjustHd.AdjustType = "Adjust";
            _stcAdjustHd.CurrCode = this.CurrencyDropDownList.SelectedValue;
            _stcAdjustHd.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _stcAdjustHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcAdjustHd.DatePrep = DateTime.Now;

            string _result = this._stockAdjustBL.AddSTCAdjustHd(_stcAdjustHd);

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
            this.ClearLabel();
            this.ClearData();
        }

        protected void OpnameNoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            STCOpnameHd _stcOpnameHd = this._stockOpnameBL.GetSingleSTCOpnameHd(this.OpnameNoDropDownList.SelectedValue);

            if (OpnameNoDropDownList.SelectedValue != "null")
            {
                this.WarehouseDropDownList.SelectedValue = _stcOpnameHd.WrhsCode;
                if (_stcOpnameHd.WrhsFgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
                {
                    this.SubledDropDownList.Enabled = false;
                    this.SubledDropDownList.SelectedValue = "null";
                }
                else
                {
                    this.SubledDropDownList.Enabled = true;
                    if (_stcOpnameHd.WrhsFgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                    {
                        this.ShowCust();
                    }
                    else if (_stcOpnameHd.WrhsFgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                    {
                        this.ShowSupp();
                    }
                    this.SubledDropDownList.SelectedValue = _stcOpnameHd.WrhsSubLed;
                }
                this.OperatorTextBox.Text = _stcOpnameHd.Operator;
                this.WarehouseDropDownList.Enabled = false;
            }
            else
            {
                this.WarehouseDropDownList.Enabled = true;
            }

        }

        protected void WarehouseDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.SubledDropDownList.Enabled = false;
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

        protected void CurrencyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _currCodeHome = _currencyBL.GetCurrDefault();
            this.ForexRateTextBox.Text = _currencyRate.GetSingleLatestCurrRate(this.CurrencyDropDownList.SelectedValue).ToString("#,###.##");

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrencyDropDownList.SelectedValue);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            if (this.CurrencyDropDownList.SelectedValue == _currCodeHome)
            {
                this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#cccccc");
                this.ForexRateTextBox.Text = "1";

            }
            else
            {
                this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#ffffff");

            }
        }
    }
}