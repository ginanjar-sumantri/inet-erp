using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;



namespace InetGlobalIndo.ERP.MTJ.UI.Billing.CustBillingAccountBase
{
    public partial class CustBillingAccountAdd : CustBillingAccountBase
    {
        private CustBillAccountBL _custBillAccountBL = new CustBillAccountBL();
        private CustomerBL _custBL = new CustomerBL();
        private ProductBL _prodBL = new ProductBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private PaymentBL _paymentBL = new PaymentBL();
        private AccountBL _accountBL = new AccountBL();
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

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";


                this.ShowCustDropdownlist();
                this.ShowCurrDropdownlist();
                //this.ShowProductDrowDownList();
                this.ShowTypePaymentDrowDownList();

                this.ClearLabel();
                this.SetAttribute();
                this.ClearData();




            }

            if (this.tempProductCode.Value != this.ProductPicker1.ProductCode)
            {
                this.tempProductCode.Value = this.ProductPicker1.ProductCode;

                if (this.ProductPicker1.ProductCode != null)
                {
                    string _description = this._prodBL.GetProductNameByCode(this.ProductPicker1.ProductCode);
                    this.DescTextBox.Text = _description;
                }
                else
                {
                    this.DescTextBox.Text = "";
                }
            }

        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.AmountTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        protected void SetAttribute()
        {
            this.AccountDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountDropDownList.ClientID + "," + this.AccountTextBox.ClientID + ");");
            this.AccountTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountDropDownList.ClientID + "," + this.AccountTextBox.ClientID + ");");

            this.ActivateDateTextBox.Attributes.Add("ReadOnly", "True");
            this.ExpiredDateTextBox.Attributes.Add("ReadOnly", "True");
            this.BandwidthIixTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.BandwidthIntTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.RatioIixTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.RatioIntTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.FgActiveCheckBox.Attributes.Add("OnClick", "Activate(" + this.FgActiveCheckBox.ClientID + "," + this.ActivateDateTextBox.ClientID + ");");

            this.PeriodTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.PeriodTextBox.Attributes.Add("OnBlur", " ValidatePeriod(" + this.PeriodTextBox.ClientID + ");");

            this.YearTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.YearTextBox.Attributes.Add("OnBlur", " ValidateYear(" + this.YearTextBox.ClientID + ");");

            this.IntervalTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.IntervalTextBox.Attributes.Add("OnBlur", " ValidateInterval(" + this.IntervalTextBox.ClientID + ");");

            this.SetAttributeRate();
        }

        private void ShowTypePaymentDrowDownList()
        {
            this.TypePaymentDropDownList.Items.Clear();
            this.TypePaymentDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.TypePaymentDropDownList.Items.Insert(1, new ListItem("Pra Bayar", "0"));
            this.TypePaymentDropDownList.Items.Insert(2, new ListItem("Pasca Bayar", "1"));
        }

        protected void ShowCustDropdownlist()
        {
            this.CustDropDownList.Items.Clear();
            this.CustDropDownList.DataSource = this._custBL.GetListCustForDDLForReport();
            this.CustDropDownList.DataValueField = "CustCode";
            this.CustDropDownList.DataTextField = "CustName";
            this.CustDropDownList.DataBind();
            this.CustDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        //private void ShowProductDrowDownList()
        //{
        //    this.ProductDropDownList.Items.Clear();
        //    this.ProductDropDownList.DataSource = this._prodBL.GetListForDDLProductNonStock();
        //    this.ProductDropDownList.DataValueField = "ProductCode";
        //    this.ProductDropDownList.DataTextField = "ProductName";
        //    this.ProductDropDownList.DataBind();
        //    this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        protected void ShowCurrDropdownlist()
        {
            this.CurrDropDownList.Items.Clear();
            this.CurrDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrDropDownList.DataValueField = "CurrCode";
            this.CurrDropDownList.DataTextField = "CurrCode";
            this.CurrDropDownList.DataBind();
            this.CurrDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowBankDropdownlist()
        {
            this.BankAccountDropDownList.Items.Clear();
            this.BankAccountDropDownList.DataSource = this._paymentBL.GetListDDLCustBillAcc(this.CurrDropDownList.SelectedValue);
            this.BankAccountDropDownList.DataValueField = "PayCode";
            this.BankAccountDropDownList.DataTextField = "PayName";
            this.BankAccountDropDownList.DataBind();
            this.BankAccountDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowAccount()
        {
            this.AccountDropDownList.DataTextField = "AccountName";
            this.AccountDropDownList.DataValueField = "Account";
            this.AccountDropDownList.DataSource = _accountBL.GetListForDDL(SubledDataMapper.GetSubled(SubledStatus.Customer), this.CurrDropDownList.SelectedValue);
            this.AccountDropDownList.DataBind();
            this.AccountDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearData()
        {
            this.CustDropDownList.SelectedValue = "null";
            //this.ProductDropDownList.SelectedValue = "null";
            this.ProductPicker1.ProductCode = "";
            this.ProductPicker1.ProductName = "";
            this.DescTextBox.Text = "";
            this.CurrDropDownList.SelectedValue = "null";

            this.AccountTextBox.Text = "";
            this.AccountDropDownList.Items.Clear();
            this.AccountDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.AccountDropDownList.SelectedValue = "null";

            this.EnableAmount();
            this.AmountTextBox.Text = "0";
            this.FgActiveCheckBox.Checked = false;
            this.BandwidthIixTextBox.Text = "";
            this.BandwidthIntTextBox.Text = "";

            this.BankAccountDropDownList.Items.Clear();
            this.BankAccountDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.BankAccountDropDownList.SelectedValue = "null";

            this.TypePaymentDropDownList.SelectedValue = "null";

            this.ActivateDateTextBox.Text = "";
            this.ExpiredDateTextBox.Text = "";


            this.ContractNoTextBox.Text = "";
            this.BANoTextBox.Text = "";
            this.RatioIixTextBox.Text = "";
            this.RatioIntTextBox.Text = "";

            this.PeriodTextBox.Text = "";
            this.YearTextBox.Text = "";
            this.IntervalTextBox.Text = "";
        }

        private void DisableAmount()
        {
            this.AmountTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountTextBox.Attributes.Add("style", "background-color:#CCCCCC");
        }

        private void EnableAmount()
        {
            this.AmountTextBox.Attributes.Remove("ReadOnly");
            this.AmountTextBox.Attributes.Add("style", "background-color:#FFFFFF");
        }

        protected void CurrDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.EnableAmount();
            this.AmountTextBox.Text = "0";

            if (this.CurrDropDownList.SelectedValue != "null")
            {
                byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrDropDownList.SelectedValue);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                this.AccountTextBox.Text = "";

                this.ShowAccount();
                this.ShowBankDropdownlist();

                decimal _amount = this._prodBL.GetProductSalesPrice(this.CurrDropDownList.SelectedValue, this.ProductPicker1.ProductCode);
                this.AmountTextBox.Text = _amount.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                if (_amount > 0)
                {
                    this.DisableAmount();
                }
            }
            else
            {
                this.DecimalPlaceHiddenField.Value = "";

                this.AccountTextBox.Text = "";

                this.AccountDropDownList.Items.Clear();
                this.AccountDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.AccountDropDownList.SelectedValue = "null";

                this.BankAccountDropDownList.Items.Clear();
                this.BankAccountDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.BankAccountDropDownList.SelectedValue = "null";
            }

            this.SetAttributeRate();
        }

        //protected void ProductDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.ProductPicker1.ProductCode  != null)
        //    {
        //        string _description = this._prodBL.GetProductNameByCode(this.ProductPicker1.ProductCode);
        //        this.DescTextBox.Text = _description;
        //    }
        //    else
        //    {
        //        this.DescTextBox.Text = "";
        //    }
        //}

        private bool IsValidateDateActive()
        {
            bool _result = false;

            if (this.ExpiredDateTextBox.Text.Trim() == "" || Convert.ToDateTime(this.ExpiredDateTextBox.Text) > Convert.ToDateTime(this.ActivateDateTextBox.Text))
            {
                _result = true;
            }

            return _result;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {


            if (this.IsValidateDateActive() == true)
            {
                Master_CustBillAccount _msCustBillAccount = new Master_CustBillAccount();

                _msCustBillAccount.CustBillCode = Guid.NewGuid();
                //_msCustBillAccount.CustBillAccount = this.CustBillAccountTextBox.Text;
                _msCustBillAccount.CustCode = this.CustDropDownList.SelectedValue;
                _msCustBillAccount.ProductCode = this.ProductPicker1.ProductCode;
                _msCustBillAccount.CustBillDescription = this.DescTextBox.Text;
                _msCustBillAccount.CurrCode = this.CurrDropDownList.SelectedValue;
                _msCustBillAccount.Account = this.AccountDropDownList.SelectedValue;
                _msCustBillAccount.AmountForex = (this.AmountTextBox.Text == "") ? 0 : Convert.ToDecimal(this.AmountTextBox.Text);
                _msCustBillAccount.BandWidthIIX = (this.BandwidthIixTextBox.Text == "") ? 0 : Convert.ToInt32(this.BandwidthIixTextBox.Text);
                _msCustBillAccount.BandWidthINT = (this.BandwidthIntTextBox.Text == "") ? 0 : Convert.ToInt32(this.BandwidthIntTextBox.Text);

                if (this.BankAccountDropDownList.SelectedValue != "null")
                {
                    _msCustBillAccount.BankAccountId = this.BankAccountDropDownList.SelectedValue;
                }
                else
                {
                    _msCustBillAccount.BankAccountId = null;
                }

                _msCustBillAccount.TypePayment = Convert.ToByte(this.TypePaymentDropDownList.SelectedValue);

                if (this.ActivateDateTextBox.Text == "")
                {
                    _msCustBillAccount.ActivateDate = null;
                }
                else
                {
                    _msCustBillAccount.ActivateDate = Convert.ToDateTime(this.ActivateDateTextBox.Text);
                }

                if (this.ExpiredDateTextBox.Text == "")
                {
                    _msCustBillAccount.ExpiredDate = null;
                }
                else
                {
                    _msCustBillAccount.ExpiredDate = Convert.ToDateTime(this.ExpiredDateTextBox.Text);
                }
                _msCustBillAccount.ContractNo = this.ContractNoTextBox.Text;
                _msCustBillAccount.BANo = this.BANoTextBox.Text;
                _msCustBillAccount.RatioIIX = (this.RatioIixTextBox.Text == "") ? 0 : Convert.ToInt32(this.RatioIixTextBox.Text);
                _msCustBillAccount.RatioINT = (this.RatioIntTextBox.Text == "") ? 0 : Convert.ToInt32(this.RatioIntTextBox.Text);
                _msCustBillAccount.fgActive = this.FgActiveCheckBox.Checked;
                _msCustBillAccount.Period = (this.PeriodTextBox.Text == "") ? 0 : Convert.ToByte(this.PeriodTextBox.Text);
                _msCustBillAccount.Year = (this.YearTextBox.Text == "") ? 0 : Convert.ToInt32(this.YearTextBox.Text);
                _msCustBillAccount.Interval = (this.IntervalTextBox.Text == "") ? 0 : Convert.ToInt32(this.IntervalTextBox.Text);

                _msCustBillAccount.InsertBy = HttpContext.Current.User.Identity.Name;
                _msCustBillAccount.InsertDate = DateTime.Now;
                _msCustBillAccount.EditBy = HttpContext.Current.User.Identity.Name;
                _msCustBillAccount.EditDate = DateTime.Now;

                string _result = this._custBillAccountBL.AddCustBillAccount(_msCustBillAccount);

                if (_result != "")
                {
                    Response.Redirect(this._homePage);
                }
                else
                {
                    this.WarningLabel.Text = "You Failed Add Data";
                }
            }
            else
            {
                this.WarningLabel.Text = "Activate Date is greater than Expired Date";
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
























































































    }
}

