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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockBeginning
{
    public partial class StockBeginningAdd : StockBeginningBase
    {
        private StockBeginningBL _stockBeginBL = new StockBeginningBL();
        private StockTypeBL _stockTypeBL = new StockTypeBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private CustomerBL _custBL = new CustomerBL();
        private SupplierBL _suppBL = new SupplierBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRate = new CurrencyRateBL();

        private string _homePage = "StockBeginning.aspx";
        private string _detailPage = "StockBeginningDetail.aspx";

        private string _codeKey = "code";

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
                this.PageTitleLiteral.Text = "Stock - Beginning";

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                //this.ShowStockType();
                this.ShowWarehouse();

                this.SetAttribute();
                this.ShowCurrency();
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

        public void ShowCurrency()
        {
            this.CurrencyDropDownList.DataTextField = "CurrCode";
            this.CurrencyDropDownList.DataValueField = "CurrCode";
            this.CurrencyDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrencyDropDownList.DataBind();
            this.CurrencyDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
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

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            this.RemarkTextBox.Text = "";
            this.OperatorTextBox.Text = "";
            this.WarehouseDropDownList.SelectedValue = "null";
            this.SubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.SubledDropDownList.SelectedValue = "null";
            this.CurrencyDropDownList.SelectedValue = "null";
            this.ForexRateTextBox.Text = "0";
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            STCAdjustHd _stcAdjustHd = new STCAdjustHd();

            _stcAdjustHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcAdjustHd.Status = StockBeginningDataMapper.GetStatus(TransStatus.OnHold);
            _stcAdjustHd.Remark = this.RemarkTextBox.Text;
            _stcAdjustHd.Operator = this.OperatorTextBox.Text;
            _stcAdjustHd.WrhsCode = this.WarehouseDropDownList.SelectedValue;
            _stcAdjustHd.CurrCode = this.CurrencyDropDownList.SelectedValue;
            _stcAdjustHd.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _stcAdjustHd.WrhsFgSubLed = _warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue);
            if (this.SubledDropDownList.SelectedValue != "null")
            {
                _stcAdjustHd.WrhsSubled = this.SubledDropDownList.SelectedValue;
            }
            else
            {
                _stcAdjustHd.WrhsSubled = "";
            }
            _stcAdjustHd.StockType = "";
            _stcAdjustHd.FgProcess = YesNoDataMapper.GetYesNo(YesNo.No);
            _stcAdjustHd.AdjustType = "Saldo";
            _stcAdjustHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcAdjustHd.DatePrep = DateTime.Now;

            string _result = this._stockBeginBL.AddSTCAdjustHd(_stcAdjustHd);

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
            this.ClearLabel();
            this.ClearData();
        }

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