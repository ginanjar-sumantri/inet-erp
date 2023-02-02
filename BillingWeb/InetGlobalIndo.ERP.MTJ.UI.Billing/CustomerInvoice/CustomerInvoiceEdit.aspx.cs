using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.CustomerInvoice
{
    public partial class CustomerInvoiceEdit : CustomerInvoiceBase
    {
        private CustomerInvoiceBL _customerInvoiceBL = new CustomerInvoiceBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private TermBL _termBL = new TermBL();
        private CustomerBL _customerBL = new CustomerBL();
        private PermissionBL _permBL = new PermissionBL();

        string _imgPPNDate = "ppn_date_start";

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
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.ClearLabel();
                this.SetAttribute();

                this.ShowCustomer();
                this.ShowTerm();
                //this.ShowType();
                //this.ShowCurrency();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.ForexRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.ForexRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + this._imgPPNDate + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscPercentTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.StampFeeTextBox.ClientID + "," + this.OtherFeeTextBox.ClientID + "," + this.CommissionExpenseTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.DiscPercentTextBox.Attributes.Add("OnBlur", "CalculateDiscountPercent(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + this._imgPPNDate + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscPercentTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.StampFeeTextBox.ClientID + "," + this.OtherFeeTextBox.ClientID + "," + this.CommissionExpenseTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.DiscForexTextBox.Attributes.Add("OnBlur", "CalculateDiscountForex(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + this._imgPPNDate + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscPercentTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.StampFeeTextBox.ClientID + "," + this.OtherFeeTextBox.ClientID + "," + this.CommissionExpenseTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.StampFeeTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + this._imgPPNDate + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscPercentTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.StampFeeTextBox.ClientID + "," + this.OtherFeeTextBox.ClientID + "," + this.CommissionExpenseTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.OtherFeeTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + this._imgPPNDate + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscPercentTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.StampFeeTextBox.ClientID + "," + this.OtherFeeTextBox.ClientID + "," + this.CommissionExpenseTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.CommissionExpenseTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + this._imgPPNDate + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscPercentTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.StampFeeTextBox.ClientID + "," + this.OtherFeeTextBox.ClientID + "," + this.CommissionExpenseTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        protected void SetAttribute()
        {
            this.PeriodTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.PeriodTextBox.Attributes.Add("OnBlur", " ValidatePeriod(" + this.PeriodTextBox.ClientID + ");");

            this.YearTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.YearTextBox.Attributes.Add("OnBlur", " ValidateYear(" + this.YearTextBox.ClientID + ");");

            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.DueDateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");

            this.CurrTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountBaseTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.SetAttributeRate();
        }

        protected void ShowCustomer()
        {
            this.CustomerDropDownList.Items.Clear();
            this.CustomerDropDownList.DataTextField = "CustName";
            this.CustomerDropDownList.DataValueField = "CustCode";
            this.CustomerDropDownList.DataSource = this._customerBL.GetListCustForDDLForReport();
            this.CustomerDropDownList.DataBind();
            this.CustomerDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowTerm()
        {
            this.TermDropDownList.Items.Clear();
            this.TermDropDownList.DataTextField = "TermName";
            this.TermDropDownList.DataValueField = "TermCode";
            this.TermDropDownList.DataSource = this._termBL.GetListTermForDDL();
            this.TermDropDownList.DataBind();
            this.TermDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        //private void ShowType()
        //{
        //    this.TypeDropDownList.Items.Clear();
        //    this.TypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //    this.TypeDropDownList.Items.Insert(1, new ListItem("Other", "0"));
        //    this.TypeDropDownList.Items.Insert(2, new ListItem("Postpone", "1"));
        //    this.TypeDropDownList.Items.Insert(3, new ListItem("Register", "2"));
        //    this.TypeDropDownList.Items.Insert(4, new ListItem("Security Deposit", "3"));
        //}

        /*
        protected void ShowCurrency()
        {
            this.CurrCodeDropDownList.Items.Clear();
            this.CurrCodeDropDownList.DataTextField = "CurrCode";
            this.CurrCodeDropDownList.DataValueField = "CurrCode";
            this.CurrCodeDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrCodeDropDownList.DataBind();
            this.CurrCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void CurrCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrCodeDropDownList.SelectedValue != "null")
            {
                //string _currCode = this._cust.GetCurr(CustDropDownList.SelectedValue);
                string _currCodeHome = _currencyBL.GetCurrDefault();
                decimal _tempForexRate = _currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue);
                this.ForexRateTextBox.Text = this.PPNRateTextBox.Text = (_tempForexRate == 0) ? "0" : _tempForexRate.ToString("#,###.##");

                if (this.CurrCodeDropDownList.SelectedValue == _currCodeHome)
                {
                    this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.ForexRateTextBox.Attributes.Remove("OnBlur");
                    this.ForexRateTextBox.Text = "1";

                    this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.PPNRateTextBox.Attributes.Remove("OnBlur");
                    this.PPNRateTextBox.Text = "1";
                }
                else
                {
                    this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                    this.ForexRateTextBox.Attributes.Add("OnBlur", "ChangeFormat(" + this.ForexRateTextBox.ClientID + ");");

                    this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                    this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                    this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat(" + this.PPNRateTextBox.ClientID + ");");
                }

                this.CurrTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrTextBox.Attributes.Add("style", "background-color:#cccccc");
                this.CurrTextBox.Text = CurrCodeDropDownList.SelectedValue;
            }
            else
            {
                this.ForexRateTextBox.Text = "0";
                this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                this.ForexRateTextBox.Attributes.Add("Style", "Background-Color:#CCCCCC");

                this.PPNRateTextBox.Text = "0";
                this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNRateTextBox.Attributes.Add("Style", "Background-Color:#CCCCCC");
            }
        }
        */
        protected void ShowData()
        {
            Billing_CustomerInvoiceHd _customerInvoiceHd = this._customerInvoiceBL.GetSingleCustomerInvoiceHd(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_customerInvoiceHd.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            this.TransactionNoTextBox.Text = _customerInvoiceHd.TransNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_customerInvoiceHd.TransDate);
            //this.PeriodDDL.SelectedValue = _billingInvServiceHd.Period.ToString();
            //this.YearDDL.SelectedValue = _billingInvServiceHd.Year.ToString();
            this.PeriodTextBox.Text = _customerInvoiceHd.Period.ToString();
            this.YearTextBox.Text = _customerInvoiceHd.Year.ToString();
            this.CustomerDropDownList.SelectedValue = _customerInvoiceHd.CustCode;
            this.DueDateTextBox.Text = DateFormMapper.GetValue(_customerInvoiceHd.DueDate);
            this.AttnTextBox.Text = _customerInvoiceHd.Attn;
            this.TermDropDownList.SelectedValue = _customerInvoiceHd.Term;
            this.TypeTextbox.Text = CustomerInvoiceDataMapper.GetTypeText(CustomerInvoiceDataMapper.GetType(_customerInvoiceHd.Type));

            string _currCodeHome = _currencyBL.GetCurrDefault();
            if (_customerInvoiceHd.CurrCode.Trim().ToLower() == _currCodeHome.Trim().ToLower())
            {
                this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                this.ForexRateTextBox.Attributes.Remove("OnBlur");
                this.ForexRateTextBox.Text = "1";

                this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                this.PPNRateTextBox.Attributes.Remove("OnBlur");
                this.PPNRateTextBox.Text = "1";
            }
            else
            {
                this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                this.ForexRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.ForexRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

                this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                this.PPNRateTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + this.PPNRateTextBox.ClientID + "," + this._imgPPNDate + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            }

            this.CurrCodeTextbox.Text = _customerInvoiceHd.CurrCode;
            this.ForexRateTextBox.Text = (_customerInvoiceHd.ForexRate == 0) ? "0" : _customerInvoiceHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempPPNPercent = Convert.ToDecimal((_customerInvoiceHd.PPN == null) ? 0 : _customerInvoiceHd.PPN);
            this.PPNPercentTextBox.Text = (_tempPPNPercent == 0) ? "0" : _tempPPNPercent.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            if (_tempPPNPercent > 0)
            {
                this.PPNNoTextBox.Attributes.Remove("ReadOnly");
                this.PPNNoTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                this.ppn_date_start.Attributes.Add("Style", "visibility:visible; width:20px;");
                this.PPNNoTextBox.Text = _customerInvoiceHd.PPNNo;
                this.PPNDateTextBox.Text = DateFormMapper.GetValue(_customerInvoiceHd.PPNDate);
            }
            else
            {
                this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNNoTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                this.ppn_date_start.Attributes.Add("Style", "visibility:hidden");
                this.PPNNoTextBox.Text = "";
                this.PPNDateTextBox.Text = "";
            }

            decimal _tempPPNRate = Convert.ToDecimal((_customerInvoiceHd.PPNRate == null) ? 0 : _customerInvoiceHd.PPNRate);
            this.PPNRateTextBox.Text = (_tempPPNRate == 0) ? "0" : _tempPPNRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            this.CurrTextBox.Text = _customerInvoiceHd.CurrCode;

            decimal _tempBaseForex = Convert.ToDecimal((_customerInvoiceHd.BaseForex == null) ? 0 : _customerInvoiceHd.BaseForex);
            this.AmountBaseTextBox.Text = (_tempBaseForex == 0) ? "0" : _tempBaseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempDiscPercent = Convert.ToDecimal((_customerInvoiceHd.DiscPercent == null) ? 0 : _customerInvoiceHd.DiscPercent);
            this.DiscPercentTextBox.Text = (_tempDiscPercent == 0) ? "0" : _tempDiscPercent.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            if (_customerInvoiceHd.DiscPercent != 0)
            {
                this.DiscForexTextBox.Attributes.Add("ReadOnly", "True");
                this.DiscForexTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            }
            else
            {
                this.DiscForexTextBox.Attributes.Remove("ReadOnly");
                this.DiscForexTextBox.Attributes.Add("style", "background-color:#FFFFFF");
            }
            decimal _tempDiscForex = Convert.ToDecimal((_customerInvoiceHd.DiscForex == null) ? 0 : _customerInvoiceHd.DiscForex);
            this.DiscForexTextBox.Text = (_tempDiscForex == 0) ? "0" : _tempDiscForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempPPNForex = Convert.ToDecimal((_customerInvoiceHd.PPNForex == null) ? 0 : _customerInvoiceHd.PPNForex);
            this.PPNForexTextBox.Text = (_tempPPNForex == 0) ? "0" : _tempPPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempOtherFee = Convert.ToDecimal((_customerInvoiceHd.OtherFee == null) ? 0 : _customerInvoiceHd.OtherFee);
            this.OtherFeeTextBox.Text = (_tempOtherFee == 0) ? "0" : _tempOtherFee.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempStampFee = Convert.ToDecimal((_customerInvoiceHd.StampFee == null) ? 0 : _customerInvoiceHd.StampFee);
            this.StampFeeTextBox.Text = (_tempStampFee == 0) ? "0" : _tempStampFee.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempCommissionExpense = Convert.ToDecimal((_customerInvoiceHd.CommissionExpense == null) ? 0 : _customerInvoiceHd.CommissionExpense);
            this.CommissionExpenseTextBox.Text = (_tempCommissionExpense == 0) ? "0" : _tempCommissionExpense.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempTotalForex = Convert.ToDecimal((_customerInvoiceHd.TotalForex == null) ? 0 : _customerInvoiceHd.TotalForex);
            this.TotalForexTextBox.Text = (_tempTotalForex == 0) ? "0" : _tempTotalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            this.RemarkTextBox.Text = _customerInvoiceHd.Remark;

            this.SetAttributeRate();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            Billing_CustomerInvoiceHd _customerInvoiceHd = this._customerInvoiceBL.GetSingleCustomerInvoiceHd(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            _customerInvoiceHd.Period = Convert.ToInt32(this.PeriodTextBox.Text);
            _customerInvoiceHd.Year = Convert.ToInt32(this.YearTextBox.Text);
            //_customerInvoiceHd.Status = BillingInvoiceDataMapper.GetStatus(TransStatus.Hold);
            _customerInvoiceHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _customerInvoiceHd.Attn = this.AttnTextBox.Text;
            _customerInvoiceHd.DueDate = DateFormMapper.GetValue(this.DueDateTextBox.Text);
            _customerInvoiceHd.CustCode = this.CustomerDropDownList.SelectedValue;
            _customerInvoiceHd.CurrCode = this.CurrCodeTextbox.Text;
            _customerInvoiceHd.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _customerInvoiceHd.Term = this.TermDropDownList.SelectedValue;
            _customerInvoiceHd.PPNNo = this.PPNNoTextBox.Text;

            if (this.PPNDateTextBox.Text != "")
            {
                _customerInvoiceHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            else
            {
                _customerInvoiceHd.PPNDate = null;
            }

            _customerInvoiceHd.PPNRate = Convert.ToDecimal(this.PPNRateTextBox.Text);
            _customerInvoiceHd.BaseForex = Convert.ToDecimal(this.AmountBaseTextBox.Text);
            _customerInvoiceHd.PPN = Convert.ToDecimal(this.PPNPercentTextBox.Text);
            _customerInvoiceHd.DiscPercent = Convert.ToDecimal(this.DiscPercentTextBox.Text);
            _customerInvoiceHd.DiscForex = Convert.ToDecimal(this.DiscForexTextBox.Text);
            _customerInvoiceHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _customerInvoiceHd.OtherFee = Convert.ToDecimal(this.OtherFeeTextBox.Text);
            _customerInvoiceHd.StampFee = Convert.ToDecimal(this.StampFeeTextBox.Text);
            _customerInvoiceHd.CommissionExpense = Convert.ToDecimal(this.CommissionExpenseTextBox.Text);
            _customerInvoiceHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            _customerInvoiceHd.Remark = this.RemarkTextBox.Text;

            _customerInvoiceHd.EditBy = HttpContext.Current.User.Identity.Name;
            _customerInvoiceHd.EditDate = DateTime.Now;

            bool _result = this._customerInvoiceBL.EditCustomerInvoiceHd(_customerInvoiceHd);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
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
            if (this.Page.IsValid == true)
            {
                Billing_CustomerInvoiceHd _customerInvoiceHd = this._customerInvoiceBL.GetSingleCustomerInvoiceHd(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

                _customerInvoiceHd.Period = Convert.ToInt32(this.PeriodTextBox.Text);
                _customerInvoiceHd.Year = Convert.ToInt32(this.YearTextBox.Text);
                //_customerInvoiceHd.Status = BillingInvoiceDataMapper.GetStatus(TransStatus.Hold);
                _customerInvoiceHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                _customerInvoiceHd.Attn = this.AttnTextBox.Text;
                _customerInvoiceHd.DueDate = DateFormMapper.GetValue(this.DueDateTextBox.Text);
                _customerInvoiceHd.CustCode = this.CustomerDropDownList.SelectedValue;
                _customerInvoiceHd.CurrCode = this.CurrCodeTextbox.Text;
                _customerInvoiceHd.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
                _customerInvoiceHd.Term = this.TermDropDownList.SelectedValue;
                _customerInvoiceHd.PPNNo = this.PPNNoTextBox.Text;

                if (this.PPNDateTextBox.Text != "")
                {
                    _customerInvoiceHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
                }
                else
                {
                    _customerInvoiceHd.PPNDate = null;
                }

                _customerInvoiceHd.PPNRate = Convert.ToDecimal(this.PPNRateTextBox.Text);
                _customerInvoiceHd.BaseForex = Convert.ToDecimal(this.AmountBaseTextBox.Text);
                _customerInvoiceHd.PPN = Convert.ToDecimal(this.PPNPercentTextBox.Text);
                _customerInvoiceHd.DiscPercent = Convert.ToDecimal(this.DiscPercentTextBox.Text);
                _customerInvoiceHd.DiscForex = Convert.ToDecimal(this.DiscForexTextBox.Text);
                _customerInvoiceHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
                _customerInvoiceHd.OtherFee = Convert.ToDecimal(this.OtherFeeTextBox.Text);
                _customerInvoiceHd.StampFee = Convert.ToDecimal(this.StampFeeTextBox.Text);
                _customerInvoiceHd.CommissionExpense = Convert.ToDecimal(this.CommissionExpenseTextBox.Text);
                _customerInvoiceHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
                _customerInvoiceHd.Remark = this.RemarkTextBox.Text;

                _customerInvoiceHd.EditBy = HttpContext.Current.User.Identity.Name;
                _customerInvoiceHd.EditDate = DateTime.Now;

                bool _result = this._customerInvoiceBL.EditCustomerInvoiceHd(_customerInvoiceHd);

                if (_result == true)
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "You Failed Edit Data";
                }
            }
        }
    }
}