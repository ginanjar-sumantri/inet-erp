using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.ReceiptTrade
{
    public partial class ReceiptTradeDbEdit : ReceiptTradeBase
    {
        private CurrencyBL _currBL = new CurrencyBL();
        private AccountBL _accountBL = new AccountBL();
        private PaymentTradeBL _paymentTradeBL = new PaymentTradeBL();
        private PaymentBL _paymentBL = new PaymentBL();
        private FINReceiptTradeBL _receiptBL = new FINReceiptTradeBL();
        private BankBL _bankBL = new BankBL();
        private CurrencyRateBL _currRateBL = new CurrencyRateBL();
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
                this.DueDateLiteral.Text = "<input id='button1' type='button' Style = 'visibility:visible' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.ShowPayType();

                this.ClearLabel();
                this.ShowData();
                this.SetAttribute();
            }
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void SetAttribute()
        {
            this.CurrRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.AmountForexTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");

            this.CurrRateTextBox.Attributes.Add("OnBlur", "CalculateAmountHome(" + this.CurrRateTextBox.ClientID + "," + this.AmountForexTextBox.ClientID + "," + this.BankExpensePercentTextBox.ClientID + "," + this.BankExpenseAmountTextBox.ClientID + "," + this.CustRevenuePercentTextBox.ClientID + "," + this.CustRevenueAmountTextBox.ClientID + "," + this.TotalCustPaidTextBox.ClientID + "," + this.TotalReceiptForexTextBox.ClientID + "," + this.TotalReceiptHomeTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");
            this.AmountForexTextBox.Attributes.Add("OnBlur", "CalculateAmountHome(" + this.CurrRateTextBox.ClientID + "," + this.AmountForexTextBox.ClientID + "," + this.BankExpensePercentTextBox.ClientID + "," + this.BankExpenseAmountTextBox.ClientID + "," + this.CustRevenuePercentTextBox.ClientID + "," + this.CustRevenueAmountTextBox.ClientID + "," + this.TotalCustPaidTextBox.ClientID + "," + this.TotalReceiptForexTextBox.ClientID + "," + this.TotalReceiptHomeTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");

            this.DueDateTextBox.Attributes.Add("ReadOnly", "True");
            this.CustRevenueAmountTextBox.Attributes.Add("ReadOnly", "True");
            this.CustRevenuePercentTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalCustPaidTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalReceiptForexTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalReceiptHomeTextBox.Attributes.Add("ReadOnly", "True");
            this.BankExpensePercentTextBox.Attributes.Add("ReadOnly", "True");
            this.BankExpenseAmountTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrCodeTextBox.Attributes.Add("ReadOnly", "True");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowPayType()
        {
            this.PayTypeDropDownList.Items.Clear();
            this.PayTypeDropDownList.DataTextField = "PayName";
            this.PayTypeDropDownList.DataValueField = "PayCode";
            this.PayTypeDropDownList.DataSource = this._receiptBL.GetListDDLByViewPayment();
            this.PayTypeDropDownList.DataBind();
            this.PayTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowBankPayment()
        {
            string _currCodeHeader = _receiptBL.GetCustCodeHeader(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_currCodeHeader != "")
            {
                this.BankGiroDropDownList.Items.Clear();
                this.BankGiroDropDownList.DataTextField = "BankName";
                this.BankGiroDropDownList.DataValueField = "BankCode";
                this.BankGiroDropDownList.DataSource = this._bankBL.GetList(); /*this._paymentBL.GetListDDLbyViewMsBankPayment(this.CurrCodeTextBox.Text);*/
                this.BankGiroDropDownList.DataBind();
                this.BankGiroDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
            else
            {
                this.BankGiroDropDownList.Items.Clear();
                this.BankGiroDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
        }

        protected void PayTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.PayTypeDropDownList.SelectedValue != "null")
            {
                MsPayType _msPayType = _paymentBL.GetSinglePaymentType(this.PayTypeDropDownList.SelectedValue);

                char _fgMode = this._paymentBL.GetFgMode(this.PayTypeDropDownList.SelectedValue);

                this.CurrCodeTextBox.Text = this._paymentBL.GetCurrCodeByCode(this.PayTypeDropDownList.SelectedValue);
                byte _decimalPlace = _currBL.GetDecimalPlace(this.CurrCodeTextBox.Text);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                byte _decimalPlace2 = _currBL.GetDecimalPlace(_currBL.GetCurrDefault());
                this.DecimalPlaceHiddenField2.Value = CurrencyDataMapper.GetDecimal(_decimalPlace2);

                string _currHome = _currBL.GetCurrDefault();
                if (_currHome == this.CurrCodeTextBox.Text)
                {
                    this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.CurrRateTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
                }
                else
                {
                    this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                    this.CurrRateTextBox.Attributes.Add("Style", "background-color:#FFFFFF");
                }
                decimal _tempCurrRate = this._currRateBL.GetSingleLatestCurrRate(this.CurrCodeTextBox.Text);
                if (_tempCurrRate == 0)
                {
                    this.CurrRateTextBox.Text = "1";
                }
                else
                {
                    this.CurrRateTextBox.Text = _tempCurrRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                }
                this.AmountForexTextBox.Text = "0";

                if (_fgMode == ReceiptDataMapper.GetModeReceiptType(ModeReceiptType.Giro))
                {
                    this.FgGiroHiddenField.Value = (YesNoDataMapper.GetYesNo(YesNo.Yes)).ToString();

                    this.DocNoRequiredFieldValidator.Enabled = true;

                    this.BankGiroDropDownList.Enabled = true;
                    this.BankGiroCustomValidator.Enabled = true;
                    this.ShowBankPayment();

                    //this.headline_date_start.Attributes.Add("Style", "visibility:visible");
                    this.DueDateLiteral.Text = "<input id='button1' Style = 'visibility:visible' type='button' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                    this.DueDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
                }
                else
                {
                    this.FgGiroHiddenField.Value = (YesNoDataMapper.GetYesNo(YesNo.No)).ToString();

                    this.DocNoRequiredFieldValidator.Enabled = false;

                    this.BankGiroDropDownList.Enabled = false;
                    this.BankGiroCustomValidator.Enabled = false;
                    this.BankGiroDropDownList.Items.Clear();
                    this.BankGiroDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

                    //this.headline_date_start.Attributes.Add("Style", "visibility:hidden");
                    this.DueDateLiteral.Text = "<input id='button1' Style = 'visibility:hidden' type='button' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                    this.DueDateTextBox.Text = "";

                    if (_msPayType.FgCustCharge == false)
                    {
                        this.CustRevenuePercentTextBox.Text = _msPayType.CustRevenue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                        this.CustRevenueAmountTextBox.Text = "0";
                    }
                    else
                    {
                        this.CustRevenueAmountTextBox.Text = _msPayType.CustRevenue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                        this.CustRevenuePercentTextBox.Text = "0";
                    }

                    if (_msPayType.FgBankCharge == false)
                    {
                        this.BankExpensePercentTextBox.Text = _msPayType.ExpenseGiro.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                        this.BankExpenseAmountTextBox.Text = "0";
                    }
                    else
                    {
                        this.BankExpenseAmountTextBox.Text = _msPayType.ExpenseGiro.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                        this.BankExpensePercentTextBox.Text = "0";
                    }
                    this.TotalCustPaidTextBox.Text = "0";
                    this.TotalReceiptForexTextBox.Text = "0";
                    this.TotalReceiptHomeTextBox.Text = "0";
                    //if (_fgMode == ReceiptDataMapper.GetModeReceiptType(ModeReceiptType.Bank))
                    //{
                    //    this.BankExpensePercentTextBox.Attributes.Remove("ReadOnly");
                    //    this.BankExpensePercentTextBox.Attributes.Add("style", "background-color:#ffffff");
                    //    this.BankExpensePercentTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.BankExpensePercentTextBox.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");
                    //}
                    //else
                    //{
                    //    this.BankExpensePercentTextBox.Text = "";
                    //    this.BankExpensePercentTextBox.Attributes.Add("ReadOnly", "true");
                    //    this.BankExpensePercentTextBox.Attributes.Add("style", "background-color:#cccccc");
                    //    this.BankExpensePercentTextBox.Attributes.Remove("OnBlur");
                    //}
                }
            }
            else
            {
                this.ShowData();
            }
        }

        public void ShowData()
        {
            FINReceiptTradeDb _finReceiptTradeDb = this._receiptBL.GetSingleFINReceiptTradeDb(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));
            MsPayType _msPayType = _paymentBL.GetSinglePaymentType(_finReceiptTradeDb.ReceiptType);

            char _fgMode = this._paymentBL.GetFgMode(_finReceiptTradeDb.ReceiptType);

            this.PayTypeDropDownList.SelectedValue = _finReceiptTradeDb.ReceiptType;
            this.FgGiroHiddenField.Value = _finReceiptTradeDb.FgGiro.ToString();
            this.DocumentNoTextBox.Text = _finReceiptTradeDb.DocumentNo;
            this.CurrCodeTextBox.Text = _finReceiptTradeDb.CurrCode;

            byte _decimalPlace = _currBL.GetDecimalPlace(_finReceiptTradeDb.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            byte _decimalPlace2 = _currBL.GetDecimalPlace(_currBL.GetCurrDefault());
            this.DecimalPlaceHiddenField2.Value = CurrencyDataMapper.GetDecimal(_decimalPlace2);

            string _currHome = _currBL.GetCurrDefault();
            if (_currHome == this.CurrCodeTextBox.Text)
            {
                this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrRateTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
            }
            else
            {
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("Style", "background-color:#FFFFFF");
            }
            this.CurrRateTextBox.Text = (_finReceiptTradeDb.ForexRate == 0) ? "0" : _finReceiptTradeDb.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountForexTextBox.Text = (_finReceiptTradeDb.AmountForex == 0) ? "0" : _finReceiptTradeDb.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _finReceiptTradeDb.Remark;

            if (_fgMode == ReceiptDataMapper.GetModeReceiptType(ModeReceiptType.Giro))
            {
                this.DocNoRequiredFieldValidator.Enabled = true;

                this.BankGiroDropDownList.Enabled = true;
                this.BankGiroCustomValidator.Enabled = true;
                this.ShowBankPayment();
                this.BankGiroDropDownList.SelectedValue = _finReceiptTradeDb.BankGiro;

                //this.headline_date_start.Attributes.Add("Style", "visibility:visible");
                this.DueDateLiteral.Text = "<input id='button1' Style = 'visibility:visible' type='button' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.DueDateTextBox.Text = DateFormMapper.GetValue(_finReceiptTradeDb.DueDate);

                //this.BankExpenseAmountTextBox.Text = "0";
                //this.BankExpensePercentTextBox.Text = "0";
                //this.CustRevenueAmountTextBox.Text = "0";
                //this.CustRevenuePercentTextBox.Text = "0";
                //this.TotalCustPaidTextBox.Text = "0";
                //this.TotalReceiptForexTextBox.Text = "0";
                //this.TotalReceiptHomeTextBox.Text = "0";
            }
            else
            {
                this.DocNoRequiredFieldValidator.Enabled = false;

                this.BankGiroDropDownList.Enabled = false;
                this.BankGiroCustomValidator.Enabled = false;
                this.BankGiroDropDownList.Items.Clear();
                this.BankGiroDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

                //this.headline_date_start.Attributes.Add("Style", "visibility:hidden");
                this.DueDateLiteral.Text = "<input id='button1' Style = 'visibility:hidden' type='button' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.DueDateTextBox.Text = "";
            }

            if (_finReceiptTradeDb.BankExpense != null)
            {
                if (_msPayType.FgBankCharge == true)
                {
                    this.BankExpenseAmountTextBox.Text = (_msPayType.ExpenseGiro == 0) ? "0" : _msPayType.ExpenseGiro.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                    this.BankExpensePercentTextBox.Text = "0";
                }
                else
                {
                    this.BankExpensePercentTextBox.Text = (_msPayType.ExpenseGiro == 0) ? "0" : _msPayType.ExpenseGiro.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                    if (_msPayType.FgCustCharge == true)
                    {
                        this.BankExpenseAmountTextBox.Text = ((_finReceiptTradeDb.AmountForex + _msPayType.CustRevenue) * _msPayType.ExpenseGiro / 100 == 0) ? "0" : ((_finReceiptTradeDb.AmountForex + _msPayType.CustRevenue) * _msPayType.ExpenseGiro / 100).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                    }
                    else
                    {
                        this.BankExpenseAmountTextBox.Text = ((_finReceiptTradeDb.AmountForex + (_finReceiptTradeDb.AmountForex * _msPayType.CustRevenue / 100)) * _msPayType.ExpenseGiro / 100 == 0) ? "0" : ((_finReceiptTradeDb.AmountForex + (_finReceiptTradeDb.AmountForex * _msPayType.CustRevenue / 100)) * _msPayType.ExpenseGiro / 100).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                    }
                }
            }

            if (_finReceiptTradeDb.CustRevenue != null)
            {
                if (_msPayType.FgCustCharge == true)
                {
                    this.CustRevenueAmountTextBox.Text = (_msPayType.CustRevenue == 0) ? "0" : _msPayType.CustRevenue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                    this.CustRevenuePercentTextBox.Text = "0";
                }
                else
                {
                    this.CustRevenuePercentTextBox.Text = (_msPayType.CustRevenue == 0) ? "0" : _msPayType.CustRevenue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                    this.CustRevenueAmountTextBox.Text = (_finReceiptTradeDb.AmountForex * _msPayType.CustRevenue / 100 == 0) ? "0" : (_finReceiptTradeDb.AmountForex * _msPayType.CustRevenue / 100).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                }
            }
            this.TotalCustPaidTextBox.Text = Convert.ToDecimal(_finReceiptTradeDb.AmountForex + _finReceiptTradeDb.CustRevenue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TotalReceiptForexTextBox.Text = (_finReceiptTradeDb.ReceiptForex == null || _finReceiptTradeDb.ReceiptForex == 0) ? "0" : Convert.ToDecimal(_finReceiptTradeDb.ReceiptForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TotalReceiptHomeTextBox.Text = (_finReceiptTradeDb.ReceiptHome == null || _finReceiptTradeDb.ReceiptHome == 0) ? "0" : Convert.ToDecimal(_finReceiptTradeDb.ReceiptHome).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace2));
            //}
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINReceiptTradeDb _finReceiptTradeDb = this._receiptBL.GetSingleFINReceiptTradeDb(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

            _finReceiptTradeDb.ReceiptType = this.PayTypeDropDownList.SelectedValue;
            _finReceiptTradeDb.FgGiro = Convert.ToChar(this.FgGiroHiddenField.Value);
            _finReceiptTradeDb.DocumentNo = this.DocumentNoTextBox.Text;
            _finReceiptTradeDb.CurrCode = this.CurrCodeTextBox.Text;
            _finReceiptTradeDb.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finReceiptTradeDb.AmountForex = Convert.ToDecimal(this.AmountForexTextBox.Text);
            decimal _amountHome = Convert.ToDecimal(this.AmountForexTextBox.Text) * Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finReceiptTradeDb.AmountHome = _amountHome;
            _finReceiptTradeDb.Remark = this.RemarkTextBox.Text;

            if (this.BankGiroDropDownList.SelectedValue != "null")
            {
                _finReceiptTradeDb.BankGiro = this.BankGiroDropDownList.SelectedValue;
            }
            else
            {
                _finReceiptTradeDb.BankGiro = "";
            }

            if (this.DueDateTextBox.Text.Trim() != "")
            {
                _finReceiptTradeDb.DueDate = DateFormMapper.GetValue(this.DueDateTextBox.Text);
            }
            else
            {
                _finReceiptTradeDb.DueDate = null;
            }

            if (this.BankExpenseAmountTextBox.Text != "")
            {
                _finReceiptTradeDb.BankExpense = Convert.ToDecimal(this.BankExpenseAmountTextBox.Text);
            }
            else
            {
                _finReceiptTradeDb.BankExpense = null;
            }

            if (this.CustRevenueAmountTextBox.Text != "")
            {
                _finReceiptTradeDb.CustRevenue = Convert.ToDecimal(this.CustRevenueAmountTextBox.Text);
            }
            else
            {
                _finReceiptTradeDb.CustRevenue = null;
            }
            _finReceiptTradeDb.ReceiptForex = Convert.ToDecimal(this.TotalReceiptForexTextBox.Text);
            _finReceiptTradeDb.ReceiptHome = Convert.ToDecimal(this.TotalReceiptHomeTextBox.Text);

            bool _result = this._receiptBL.EditFINReceiptTradeDb(_finReceiptTradeDb);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}