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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.BillingInvoice
{
    public partial class BillingInvoiceEdit : BillingInvoiceBase
    {
        private BillingInvoiceBL _billingInvoiceBL = new BillingInvoiceBL();
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

            _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

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
            this.PPNPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + this._imgPPNDate + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscPercentTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.StampFeeTextBox.ClientID + "," + this.OtherFeeTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.DiscPercentTextBox.Attributes.Add("OnBlur", "CalculateDiscountPercent(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + this._imgPPNDate + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscPercentTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.StampFeeTextBox.ClientID + "," + this.OtherFeeTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.DiscForexTextBox.Attributes.Add("OnBlur", "CalculateDiscountForex(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + this._imgPPNDate + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscPercentTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.StampFeeTextBox.ClientID + "," + this.OtherFeeTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.StampFeeTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + this._imgPPNDate + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscPercentTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.StampFeeTextBox.ClientID + "," + this.OtherFeeTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.OtherFeeTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + this._imgPPNDate + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscPercentTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.StampFeeTextBox.ClientID + "," + this.OtherFeeTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        protected void SetAttribute()
        {
            this.PeriodTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.PeriodTextBox.Attributes.Add("OnBlur", " ValidatePeriod(" + this.PeriodTextBox.ClientID + ");");

            this.YearTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.YearTextBox.Attributes.Add("OnBlur", " ValidateYear(" + this.YearTextBox.ClientID + ");");

            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.DueDateTextBox.Attributes.Add("ReadOnly", "True");
            //this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
            //this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");

            this.AmountBaseTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
            //this.ViewDetailButton.Attributes.Add("OnClick", "return AskYouFirstToSave(" + this.CheckHidden.ClientID + ");");

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

        private void ClearDataNumeric()
        {
            this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNNoTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");

            this.ForexRateTextBox.Text = "0";
            this.DecimalPlaceHiddenField.Value = "";
            this.PPNPercentTextBox.Text = "0";
            this.PPNNoTextBox.Text = "";
            this.PPNDateTextBox.Text = "";
            this.PPNRateTextBox.Text = "0";
            this.CurrTextBox.Text = "";
            this.DiscForexTextBox.Text = "0";
            this.PPNForexTextBox.Text = "0";
            this.OtherFeeTextBox.Text = "0";
            this.StampFeeTextBox.Text = "0";
            this.TotalForexTextBox.Text = "0";
        }

        protected void ShowData()
        {
            Billing_InvoiceHd _billingInvoiceHd = this._billingInvoiceBL.GetSingleBillingInvoiceHd(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_billingInvoiceHd.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            this.TransactionNoTextBox.Text = _billingInvoiceHd.TransNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_billingInvoiceHd.TransDate);
            this.PeriodTextBox.Text = _billingInvoiceHd.Period.ToString();
            this.YearTextBox.Text = _billingInvoiceHd.Year.ToString();
            this.CustomerDropDownList.SelectedValue = _billingInvoiceHd.CustCode;
            this.DueDateTextBox.Text = DateFormMapper.GetValue(_billingInvoiceHd.DueDate);
            this.AttnTextBox.Text = _billingInvoiceHd.Attn;
            this.TermDropDownList.SelectedValue = _billingInvoiceHd.Term;

            string _currCodeHome = _currencyBL.GetCurrDefault();
            if (_billingInvoiceHd.CurrCode.Trim().ToLower() == _currCodeHome.Trim().ToLower())
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

            this.CurrCodeTextbox.Text = _billingInvoiceHd.CurrCode;
            this.ForexRateTextBox.Text = (_billingInvoiceHd.ForexRate == 0) ? "0" : _billingInvoiceHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempPPNPercent = Convert.ToDecimal((_billingInvoiceHd.PPN == null) ? 0 : _billingInvoiceHd.PPN);
            this.PPNPercentTextBox.Text = (_tempPPNPercent == 0) ? "0" : _tempPPNPercent.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            if (_tempPPNPercent > 0)
            {
                this.PPNNoTextBox.Attributes.Remove("ReadOnly");
                this.PPNNoTextBox.Attributes.Add("style", "background-color:#FFFFFF");

                this.PPNNoTextBox.Text = _billingInvoiceHd.PPNNo;
                this.PPNDateTextBox.Text = (_billingInvoiceHd.PPNDate == null) ? "" : DateFormMapper.GetValue(_billingInvoiceHd.PPNDate);
            }
            else
            {
                this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNNoTextBox.Attributes.Add("style", "background-color:#CCCCCC");

                this.PPNNoTextBox.Text = "";
                this.PPNDateTextBox.Text = "";
            }

            decimal _tempPPNRate = Convert.ToDecimal((_billingInvoiceHd.PPNRate == null) ? 0 : _billingInvoiceHd.PPNRate);
            this.PPNRateTextBox.Text = (_tempPPNRate == 0) ? "0" : _tempPPNRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            this.CurrTextBox.Text = _billingInvoiceHd.CurrCode;

            decimal _tempBaseForex = Convert.ToDecimal((_billingInvoiceHd.BaseForex == null) ? 0 : _billingInvoiceHd.BaseForex);
            this.AmountBaseTextBox.Text = (_tempBaseForex == 0) ? "0" : _tempBaseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempDiscPercent = Convert.ToDecimal((_billingInvoiceHd.DiscPercent == null) ? 0 : _billingInvoiceHd.DiscPercent);
            this.DiscPercentTextBox.Text = (_tempDiscPercent == 0) ? "0" : _tempDiscPercent.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            if (_billingInvoiceHd.DiscPercent != 0)
            {
                this.DiscForexTextBox.Attributes.Add("ReadOnly", "True");
                this.DiscForexTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            }
            else
            {
                this.DiscForexTextBox.Attributes.Remove("ReadOnly");
                this.DiscForexTextBox.Attributes.Add("style", "background-color:#FFFFFF");
            }
            decimal _tempDiscForex = Convert.ToDecimal((_billingInvoiceHd.DiscForex == null) ? 0 : _billingInvoiceHd.DiscForex);
            this.DiscForexTextBox.Text = (_tempDiscForex == 0) ? "0" : _tempDiscForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempPPNForex = Convert.ToDecimal((_billingInvoiceHd.PPNForex == null) ? 0 : _billingInvoiceHd.PPNForex);
            this.PPNForexTextBox.Text = (_tempPPNForex == 0) ? "0" : _tempPPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempStampFee = Convert.ToDecimal((_billingInvoiceHd.StampFee == null) ? 0 : _billingInvoiceHd.StampFee);
            this.StampFeeTextBox.Text = (_tempStampFee == 0) ? "0" : _tempStampFee.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempOtherFee = Convert.ToDecimal((_billingInvoiceHd.OtherFee == null) ? 0 : _billingInvoiceHd.OtherFee);
            this.OtherFeeTextBox.Text = (_tempOtherFee == 0) ? "0" : _tempOtherFee.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempTotalForex = Convert.ToDecimal((_billingInvoiceHd.TotalForex == null) ? 0 : _billingInvoiceHd.TotalForex);
            this.TotalForexTextBox.Text = (_tempTotalForex == 0) ? "0" : _tempTotalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            this.RemarkTextBox.Text = _billingInvoiceHd.Remark;

            this.SetAttributeRate();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            Billing_InvoiceHd _billingInvoiceHd = this._billingInvoiceBL.GetSingleBillingInvoiceHd(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            _billingInvoiceHd.Period = Convert.ToInt32(this.PeriodTextBox.Text);
            _billingInvoiceHd.Year = Convert.ToInt32(this.YearTextBox.Text);
            _billingInvoiceHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _billingInvoiceHd.Attn = this.AttnTextBox.Text;
            _billingInvoiceHd.DueDate = DateFormMapper.GetValue(this.DueDateTextBox.Text);
            _billingInvoiceHd.CustCode = this.CustomerDropDownList.SelectedValue;
            _billingInvoiceHd.CurrCode = this.CurrCodeTextbox.Text;
            _billingInvoiceHd.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _billingInvoiceHd.Term = this.TermDropDownList.SelectedValue;
            _billingInvoiceHd.PPNNo = this.PPNNoTextBox.Text;

            if (this.PPNDateTextBox.Text != "")
            {
                _billingInvoiceHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            else
            {
                _billingInvoiceHd.PPNDate = null;
            }

            _billingInvoiceHd.PPNRate = Convert.ToDecimal(this.PPNRateTextBox.Text);
            _billingInvoiceHd.BaseForex = Convert.ToDecimal(this.AmountBaseTextBox.Text);
            _billingInvoiceHd.PPN = Convert.ToDecimal(this.PPNPercentTextBox.Text);
            _billingInvoiceHd.DiscPercent = Convert.ToDecimal(this.DiscPercentTextBox.Text);
            _billingInvoiceHd.DiscForex = Convert.ToDecimal(this.DiscForexTextBox.Text);
            _billingInvoiceHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _billingInvoiceHd.StampFee = Convert.ToDecimal(this.StampFeeTextBox.Text);
            _billingInvoiceHd.OtherFee = Convert.ToDecimal(this.OtherFeeTextBox.Text);
            _billingInvoiceHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            _billingInvoiceHd.Remark = this.RemarkTextBox.Text;

            _billingInvoiceHd.EditBy = HttpContext.Current.User.Identity.Name;
            _billingInvoiceHd.EditDate = DateTime.Now;

            bool _result = this._billingInvoiceBL.EditBillingInvoiceHd(_billingInvoiceHd);

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
                Billing_InvoiceHd _billingInvoiceHd = this._billingInvoiceBL.GetSingleBillingInvoiceHd(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

                _billingInvoiceHd.Period = Convert.ToInt32(this.PeriodTextBox.Text);
                _billingInvoiceHd.Year = Convert.ToInt32(this.YearTextBox.Text);
                _billingInvoiceHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                _billingInvoiceHd.Attn = this.AttnTextBox.Text;
                _billingInvoiceHd.DueDate = DateFormMapper.GetValue(this.DueDateTextBox.Text);
                _billingInvoiceHd.CustCode = this.CustomerDropDownList.SelectedValue;
                _billingInvoiceHd.CurrCode = this.CurrCodeTextbox.Text;
                _billingInvoiceHd.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
                _billingInvoiceHd.Term = this.TermDropDownList.SelectedValue;
                _billingInvoiceHd.PPNNo = this.PPNNoTextBox.Text;

                if (this.PPNDateTextBox.Text != "")
                {
                    _billingInvoiceHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
                }
                else
                {
                    _billingInvoiceHd.PPNDate = null;
                }

                _billingInvoiceHd.PPNRate = Convert.ToDecimal(this.PPNRateTextBox.Text);
                _billingInvoiceHd.BaseForex = Convert.ToDecimal(this.AmountBaseTextBox.Text);
                _billingInvoiceHd.PPN = Convert.ToDecimal(this.PPNPercentTextBox.Text);
                _billingInvoiceHd.DiscPercent = Convert.ToDecimal(this.DiscPercentTextBox.Text);
                _billingInvoiceHd.DiscForex = Convert.ToDecimal(this.DiscForexTextBox.Text);
                _billingInvoiceHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
                _billingInvoiceHd.StampFee = Convert.ToDecimal(this.StampFeeTextBox.Text);
                _billingInvoiceHd.OtherFee = Convert.ToDecimal(this.OtherFeeTextBox.Text);
                _billingInvoiceHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
                _billingInvoiceHd.Remark = this.RemarkTextBox.Text;

                _billingInvoiceHd.EditBy = HttpContext.Current.User.Identity.Name;
                _billingInvoiceHd.EditDate = DateTime.Now;

                bool _result = this._billingInvoiceBL.EditBillingInvoiceHd(_billingInvoiceHd);

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