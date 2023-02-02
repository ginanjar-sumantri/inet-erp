using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.CustBillingAccountBase
{
    public partial class CustBillingAccountEdit : CustBillingAccountBase
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
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.ExpireDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.ExpiredDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.ActivateDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.ActivateDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";


                //this.ShowCustDropdownlist();
                this.ShowCurrDropdownlist();
                //this.ShowProductDrowDownList();
                this.ShowTypePaymentDrowDownList();

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
            }

            if (this.tempProductCode.Value != this.ProductPicker1.ProductCode)
            {
                this.tempProductCode.Value = this.ProductPicker1.ProductCode;

                if (this.ProductPicker1.ProductCode != null)
                {
                    this.DescTextBox.Text = this.ProductPicker1.ProductName;

                    Master_CustBillAccount _msCustBillAccount = this._custBillAccountBL.GetSingleCustBillAccount(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));
                    if (_msCustBillAccount.ProductCode != this.ProductPicker1.ProductCode)
                    {
                        this.DescTextBox.Text = this.ProductPicker1.ProductName;
                    }
                    else
                    {
                        this.DescTextBox.Text = _msCustBillAccount.CustBillDescription;
                    }
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
            //this.FgActiveCheckBox.Attributes.Add("OnClick", "Activate(" + this.FgActiveCheckBox.ClientID + "," + this.ActivateDateTextBox.ClientID + ");");

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

        protected void ShowCurrDropdownlist()
        {
            this.CurrDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrDropDownList.DataValueField = "CurrCode";
            this.CurrDropDownList.DataTextField = "CurrCode";
            this.CurrDropDownList.DataBind();
            this.CurrDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowBankDropdownlist()
        {
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

        protected void ShowData()
        {
            Master_CustBillAccount _msCustBillAccount = this._custBillAccountBL.GetSingleCustBillAccount(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_msCustBillAccount.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            this.CustBillAccountTextBox.Text = _msCustBillAccount.CustBillAccount;
            //this.CustDropDownList.SelectedValue = _msCustBillAccount.CustCode;

            this.CustTextBox.Text = this._custBL.GetNameByCode(_msCustBillAccount.CustCode) + " - " + _msCustBillAccount.CustCode;
            //this.ProductDropDownList.SelectedValue = _msCustBillAccount.ProductCode;
            this.ProductPicker1.ProductCode = _msCustBillAccount.ProductCode;
            this.ProductPicker1.ProductName = _prodBL.GetProductNameByCode(_msCustBillAccount.ProductCode);
            this.DescTextBox.Text = _msCustBillAccount.CustBillDescription;
            this.CurrDropDownList.SelectedValue = _msCustBillAccount.CurrCode;
            this.ShowAccount();
            this.AccountTextBox.Text = _msCustBillAccount.Account;
            this.AccountDropDownList.SelectedValue = _msCustBillAccount.Account;
            this.DisableAmount();
            this.AmountTextBox.Text = (_msCustBillAccount.AmountForex == 0) ? "0" : Convert.ToDecimal(_msCustBillAccount.AmountForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.BandwidthIixTextBox.Text = (_msCustBillAccount.BandWidthIIX == null) ? "" : _msCustBillAccount.BandWidthIIX.ToString();
            this.BandwidthIntTextBox.Text = (_msCustBillAccount.BandWidthINT == null) ? "" : _msCustBillAccount.BandWidthINT.ToString();
            this.ShowBankDropdownlist();
            this.BankAccountDropDownList.SelectedValue = _msCustBillAccount.BankAccountId;
            this.TypePaymentDropDownList.SelectedValue = _msCustBillAccount.TypePayment.ToString();
            this.ActivateDateTextBox.Text = (_msCustBillAccount.ActivateDate == null) ? "" : DateFormMapper.GetValue(_msCustBillAccount.ActivateDate);
            this.ExpiredDateTextBox.Text = (_msCustBillAccount.ExpiredDate == null) ? "" : DateFormMapper.GetValue(_msCustBillAccount.ExpiredDate);

            this.ContractNoTextBox.Text = _msCustBillAccount.ContractNo;
            this.BANoTextBox.Text = _msCustBillAccount.BANo;
            //this.RatioTextBox.Text = (_msCustBillAccount.Ratio == null) ? "" : _msCustBillAccount.Ratio.ToString();
            this.RatioIixTextBox.Text = (_msCustBillAccount.RatioIIX == null) ? "" : _msCustBillAccount.RatioIIX.ToString();
            this.RatioIntTextBox.Text = (_msCustBillAccount.RatioINT == null) ? "" : _msCustBillAccount.RatioINT.ToString();
            this.FgActiveCheckBox.Checked = (_msCustBillAccount.fgActive == null) ? false : Convert.ToBoolean(_msCustBillAccount.fgActive);

            this.PeriodTextBox.Text = (_msCustBillAccount.Period == null) ? "" : _msCustBillAccount.Period.ToString();
            this.YearTextBox.Text = (_msCustBillAccount.Year == null) ? "" : _msCustBillAccount.Year.ToString();
            this.IntervalTextBox.Text = (_msCustBillAccount.Interval == null) ? "" : _msCustBillAccount.Interval.ToString();
            this.SetAttributeRate();
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
        //    if (this.ProductPicker1.ProductCode != null)
        //    {
        //        string _description = this._prodBL.GetProductNameByCode(this.ProductPicker1.ProductCode );
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

            if (this.ExpiredDateTextBox.Text.Trim() == "" || DateFormMapper.GetValue(this.ExpiredDateTextBox.Text) > DateFormMapper.GetValue(this.ActivateDateTextBox.Text))
            {
                _result = true;
            }

            return _result;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.IsValidateDateActive() == true)
            {
                Master_CustBillAccount _msCustBillAccount = this._custBillAccountBL.GetSingleCustBillAccount(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

                //_msCustBillAccount.CustBillAccount = this.CustBillAccountTextBox.Text;
                //_msCustBillAccount.CustCode = this.CustDropDownList.SelectedValue;
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
                    _msCustBillAccount.ActivateDate = DateFormMapper.GetValue(this.ActivateDateTextBox.Text);
                }

                if (this.ExpiredDateTextBox.Text == "")
                {
                    _msCustBillAccount.ExpiredDate = null;
                }
                else
                {
                    _msCustBillAccount.ExpiredDate = DateFormMapper.GetValue(this.ExpiredDateTextBox.Text);
                }

                _msCustBillAccount.ContractNo = this.ContractNoTextBox.Text;
                _msCustBillAccount.BANo = this.BANoTextBox.Text;
                _msCustBillAccount.BANo = this.BANoTextBox.Text;
                _msCustBillAccount.RatioIIX = (this.RatioIixTextBox.Text == "") ? 0 : Convert.ToInt32(this.RatioIixTextBox.Text);
                _msCustBillAccount.RatioINT = (this.RatioIntTextBox.Text == "") ? 0 : Convert.ToInt32(this.RatioIntTextBox.Text);
                _msCustBillAccount.fgActive = this.FgActiveCheckBox.Checked;
                _msCustBillAccount.Period = (this.PeriodTextBox.Text == "") ? 0 : Convert.ToByte(this.PeriodTextBox.Text);
                _msCustBillAccount.Year = (this.YearTextBox.Text == "") ? 0 : Convert.ToInt32(this.YearTextBox.Text);
                _msCustBillAccount.Interval = (this.IntervalTextBox.Text == "") ? 0 : Convert.ToInt32(this.IntervalTextBox.Text);

                _msCustBillAccount.EditBy = HttpContext.Current.User.Identity.Name;
                _msCustBillAccount.EditDate = DateTime.Now;

                bool _result = this._custBillAccountBL.EditCustBillAccount(_msCustBillAccount);

                if (_result == true)
                {
                    Response.Redirect(this._homePage);
                }
                else
                {
                    this.WarningLabel.Text = "You Failed Edit Data";
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
            this.ShowData();
        }

    }
}

