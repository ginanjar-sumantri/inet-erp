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
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockBeginning
{
    public partial class StockBeginningEdit : StockBeginningBase
    {
        private StockBeginningBL _stockBeginBL = new StockBeginningBL();
        private StockTypeBL _stockTypeBL = new StockTypeBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private CustomerBL _custBL = new CustomerBL();
        private SupplierBL _suppBL = new SupplierBL();
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

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                //this.ShowStockType();
                this.ShowWarehouse();
                this.ShowCurrency();
                this.SetAttribute();
                this.ClearLabel();
                this.ShowData();
            }
        }

        private void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
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

        public void ShowCurrency()
        {
            this.CurrencyDropDownList.DataTextField = "CurrCode";
            this.CurrencyDropDownList.DataValueField = "CurrCode";
            this.CurrencyDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrencyDropDownList.DataBind();
            this.CurrencyDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
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

        public void ShowData()
        {
            STCAdjustHd _stcAdjustHd = this._stockBeginBL.GetSingleSTCAdjustHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_stcAdjustHd.CurrCode);
            string _currCodeHome = _currencyBL.GetCurrDefault();

            this.TransNoTextBox.Text = _stcAdjustHd.TransNmbr;
            this.FileNoTextBox.Text = _stcAdjustHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_stcAdjustHd.TransDate);
            this.RemarkTextBox.Text = _stcAdjustHd.Remark;
            this.OperatorTextBox.Text = _stcAdjustHd.Operator;
            this.CurrencyDropDownList.SelectedValue = _stcAdjustHd.CurrCode;

            this.WarehouseDropDownList.SelectedValue = _stcAdjustHd.WrhsCode;
            if (_stcAdjustHd.WrhsFgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            {
                this.SubledDropDownList.Enabled = true;
                this.ShowCust();
                this.SubledDropDownList.SelectedValue = _stcAdjustHd.WrhsSubled;
            }
            else if (_stcAdjustHd.WrhsFgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
            {
                this.SubledDropDownList.Enabled = true;
                this.ShowSupp();
                this.SubledDropDownList.SelectedValue = _stcAdjustHd.WrhsSubled;
            }
            else
            {
                this.SubledDropDownList.Enabled = false;
                this.SubledDropDownList.SelectedValue = "null";
            }

            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            if (_stcAdjustHd.CurrCode == _currCodeHome)
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
            this.ForexRateTextBox.Text = Convert.ToDecimal(_stcAdjustHd.ForexRate).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCAdjustHd _stcAdjustHd = this._stockBeginBL.GetSingleSTCAdjustHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcAdjustHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcAdjustHd.Remark = this.RemarkTextBox.Text;
            _stcAdjustHd.Operator = this.OperatorTextBox.Text;
            _stcAdjustHd.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _stcAdjustHd.CurrCode = this.CurrencyDropDownList.SelectedValue;

            if (this.WarehouseDropDownList.SelectedValue != "null")
            {
                _stcAdjustHd.WrhsCode = this.WarehouseDropDownList.SelectedValue;
            }
            else
            {
                _stcAdjustHd.WrhsCode = "";
            }
            if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                _stcAdjustHd.WrhsFgSubLed = Convert.ToChar(WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled));
            }
            else if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            {
                _stcAdjustHd.WrhsFgSubLed = Convert.ToChar(WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer));
            }
            else if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            {
                _stcAdjustHd.WrhsFgSubLed = Convert.ToChar(WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer));
            }
            _stcAdjustHd.WrhsSubled = this.SubledDropDownList.SelectedValue;
            _stcAdjustHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcAdjustHd.DatePrep = DateTime.Now;

            bool _result = this._stockBeginBL.EditSTCAdjustHd(_stcAdjustHd);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
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

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            STCAdjustHd _stcAdjustHd = this._stockBeginBL.GetSingleSTCAdjustHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcAdjustHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcAdjustHd.Remark = this.RemarkTextBox.Text;
            _stcAdjustHd.Operator = this.OperatorTextBox.Text;
            _stcAdjustHd.WrhsCode = this.WarehouseDropDownList.SelectedValue;
            _stcAdjustHd.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _stcAdjustHd.CurrCode = this.CurrencyDropDownList.SelectedValue;

            if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                _stcAdjustHd.WrhsFgSubLed = Convert.ToChar(WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled));
            }
            else if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            {
                _stcAdjustHd.WrhsFgSubLed = Convert.ToChar(WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer));
            }
            else if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            {
                _stcAdjustHd.WrhsFgSubLed = Convert.ToChar(WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer));
            }
            if (this.WarehouseDropDownList.SelectedValue != "null")
            {
                _stcAdjustHd.WrhsCode = this.WarehouseDropDownList.SelectedValue;
            }
            else
            {
                _stcAdjustHd.WrhsCode = "";
            }
            _stcAdjustHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _stcAdjustHd.DatePrep = DateTime.Now;

            bool _result = this._stockBeginBL.EditSTCAdjustHd(_stcAdjustHd);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
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