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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PaymentTrade
{
    public partial class PaymentTradeCrEdit : PaymentTradeBase
    {
        private AccountBL _accountBL = new AccountBL();
        private PaymentBL _paymentBL = new PaymentBL();
        private PaymentTradeBL _paymentTradeBL = new PaymentTradeBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private CurrencyBL _currBL = new CurrencyBL();
        private PermissionBL _permBL = new PermissionBL();
        private FINDPSuppPayBL _dpSuppBL = new FINDPSuppPayBL();

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
                this.DueDateLiteral.Text = "<input id='button1' Style='visibility:visible' type='button' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

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

        private void SetAttribute()
        {
            this.CurrCodeTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountTotalTextBox.Attributes.Add("ReadOnly", "True");
            this.DueDateTextBox.Attributes.Add("ReadOnly", "True");

            this.CurrRateTextBox.Attributes.Add("OnBlur", "CalculateAmountTotal(" + this.CurrRateTextBox.ClientID + "," + this.AmountForexTextBox.ClientID + "," + this.AmountTotalTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");
            this.AmountForexTextBox.Attributes.Add("OnBlur", "CalculateAmountTotal(" + this.CurrRateTextBox.ClientID + "," + this.AmountForexTextBox.ClientID + "," + this.AmountTotalTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");
            this.BankExpenseTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.BankExpenseTextBox.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowPayType()
        {
            this.PayTypeDropDownList.Items.Clear();
            this.PayTypeDropDownList.DataTextField = "PayName";
            this.PayTypeDropDownList.DataValueField = "PayCode";
            this.PayTypeDropDownList.DataSource = this._paymentBL.GetListDDLByViewPayment();
            this.PayTypeDropDownList.DataBind();
            this.PayTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowBankPayment()
        {
            this.BankPaymentDropDownList.Items.Clear();
            this.BankPaymentDropDownList.DataTextField = "PayName";
            this.BankPaymentDropDownList.DataValueField = "PayCode";
            this.BankPaymentDropDownList.DataSource = this._paymentBL.GetListDDLbyViewMsBankPayment(this.CurrCodeTextBox.Text);
            this.BankPaymentDropDownList.DataBind();
            this.BankPaymentDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowDPSupp()
        {
            String _suppCode = _paymentTradeBL.GetSingleFINPayTradeHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).SuppCode;

            this.DPSuppDropDownList.Items.Clear();
            this.DPSuppDropDownList.DataTextField = "FileNmbr";
            this.DPSuppDropDownList.DataValueField = "TransNmbr";
            this.DPSuppDropDownList.DataSource = this._dpSuppBL.GetListDPForDDL(_suppCode, this.CurrCodeTextBox.Text);
            this.DPSuppDropDownList.DataBind();
            this.DPSuppDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowData()
        {
            FINPayTradeCr _finPayTradeCr = this._paymentTradeBL.GetSingleFINPayTradeCr(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = _currBL.GetDecimalPlace(_finPayTradeCr.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            byte _decimalPlace2 = _currBL.GetDecimalPlace(_currBL.GetCurrDefault());
            this.DecimalPlaceHiddenField2.Value = CurrencyDataMapper.GetDecimal(_decimalPlace2);

            char _fgMode = this._paymentBL.GetFgMode(_finPayTradeCr.PayType);
            this.PayTypeDropDownList.SelectedValue = _finPayTradeCr.PayType;
            this.CurrCodeTextBox.Text = _finPayTradeCr.CurrCode;
            string _currHome = _currBL.GetCurrDefault();
            this.SetCurrRate();
            this.CurrRateTextBox.Text = (_finPayTradeCr.ForexRate == 0) ? "0" : _finPayTradeCr.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountForexTextBox.Text = (_finPayTradeCr.AmountForex == 0) ? "0" : _finPayTradeCr.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempAmountTotal = Convert.ToDecimal((_finPayTradeCr.AmountHome == null) ? 0 : _finPayTradeCr.AmountHome);
            this.AmountTotalTextBox.Text = (_tempAmountTotal == 0) ? "0" : _tempAmountTotal.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace2));

            this.RemarkTextBox.Text = _finPayTradeCr.Remark;

            if (_fgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Giro))
            {
                this.DocNoRequiredFieldValidator.Enabled = true;

                this.BankPaymentDropDownList.Enabled = true;
                this.BankPaymentCustomValidator.Enabled = true;
                this.ShowBankPayment();
                this.BankPaymentDropDownList.SelectedValue = _finPayTradeCr.BankPayment;

                //this.headline_date_start.Attributes.Add("Style", "visibility:visible");
                this.DueDateLiteral.Text = "<input id='button1' Style='visibility:visible' type='button' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.DueDateTextBox.Text = DateFormMapper.GetValue(_finPayTradeCr.DueDate);

                this.BankExpenseTextBox.Text = "";
                this.BankExpenseTextBox.Attributes.Add("ReadOnly", "true");
                this.BankExpenseTextBox.Attributes.Add("style", "background-color:#cccccc");
                this.BankExpenseTextBox.Attributes.Remove("OnBlur");
            }
            else
            {
                this.DocNoRequiredFieldValidator.Enabled = false;

                this.BankPaymentDropDownList.Enabled = false;
                this.BankPaymentCustomValidator.Enabled = false;
                this.BankPaymentDropDownList.Items.Clear();
                this.BankPaymentDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

                //this.headline_date_start.Attributes.Add("Style", "visibility:hidden");
                this.DueDateLiteral.Text = "<input id='button1' Style='visibility:hidden' type='button' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.DueDateTextBox.Text = "";

                if (_fgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Bank))
                {
                    decimal _tempBankExpense = Convert.ToDecimal(_finPayTradeCr.BankExpense);
                    this.BankExpenseTextBox.Text = (_tempBankExpense == 0) ? "0" : _tempBankExpense.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace2)); ;

                    this.BankExpenseTextBox.Attributes.Remove("ReadOnly");
                    this.BankExpenseTextBox.Attributes.Add("style", "background-color:#ffffff");
                    this.BankExpenseTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.BankExpenseTextBox.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");
                }
                else
                {
                    this.BankExpenseTextBox.Text = "";
                    this.BankExpenseTextBox.Attributes.Add("ReadOnly", "true");
                    this.BankExpenseTextBox.Attributes.Add("style", "background-color:#cccccc");
                    this.BankExpenseTextBox.Attributes.Remove("OnBlur");
                }
                if (_fgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.DP))
                {
                    ShowDPSupp();
                    DPTr.Visible = true;
                    DocTr.Visible = false;
                    DPSuppDropDownList.SelectedValue = _finPayTradeCr.DocumentNo;
                    DocumentNoTextBox.Text = "";
                }
                else
                {
                    DPTr.Visible = false;
                    DocTr.Visible = true;
                    DPSuppDropDownList.SelectedValue = "null";
                    this.DocumentNoTextBox.Text = _finPayTradeCr.DocumentNo;
                }
            }
        }

        private void EnableRate()
        {
            this.CurrRateTextBox.Attributes.Remove("ReadOnly");
            this.CurrRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
        }

        private void DisableRate()
        {
            this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.CurrRateTextBox.Text = "1";
        }

        private void SetCurrRate()
        {
            byte _decimalPlace = this._currBL.GetDecimalPlace(this.CurrCodeTextBox.Text);
            this.CurrRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeTextBox.Text).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            if (this.CurrCodeTextBox.Text.Trim().ToLower() == _currBL.GetCurrDefault().Trim().ToLower())
            {
                this.DisableRate();
            }
            else
            {
                this.EnableRate();
            }
        }

        protected void PayTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.PayTypeDropDownList.SelectedValue != "null")
            {
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
                decimal _tempCurrRate = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeTextBox.Text);
                if (_tempCurrRate == 0)
                {
                    this.CurrRateTextBox.Text = "1";
                }
                else
                {
                    this.CurrRateTextBox.Text = _tempCurrRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                }

                this.AmountForexTextBox.Text = "0";
                this.AmountTotalTextBox.Text = "0";

                if (_fgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Giro))
                {
                    this.FgGiroHiddenField.Value = (YesNoDataMapper.GetYesNo(YesNo.Yes)).ToString();

                    this.DocNoRequiredFieldValidator.Enabled = true;

                    this.BankPaymentDropDownList.Enabled = true;
                    this.BankPaymentCustomValidator.Enabled = true;
                    this.ShowBankPayment();

                    //this.headline_date_start.Attributes.Add("Style", "visibility:visible");
                    this.DueDateLiteral.Text = "<input id='button1' Style='visibility:visible' type='button' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                    this.DueDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);

                    this.BankExpenseTextBox.Text = "";
                    this.BankExpenseTextBox.Attributes.Add("ReadOnly", "true");
                    this.BankExpenseTextBox.Attributes.Add("style", "background-color:#cccccc");
                    this.BankExpenseTextBox.Attributes.Remove("OnBlur");
                }
                else
                {
                    this.FgGiroHiddenField.Value = (YesNoDataMapper.GetYesNo(YesNo.No)).ToString();

                    this.DocNoRequiredFieldValidator.Enabled = false;

                    this.BankPaymentDropDownList.Enabled = false;
                    this.BankPaymentCustomValidator.Enabled = false;
                    this.BankPaymentDropDownList.Items.Clear();
                    this.BankPaymentDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

                    //this.headline_date_start.Attributes.Add("Style", "visibility:hidden");
                    this.DueDateLiteral.Text = "<input id='button1' Style='visibility:hidden' type='button' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                    this.DueDateTextBox.Text = "";

                    if (_fgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Bank))
                    {
                        this.BankExpenseTextBox.Attributes.Remove("ReadOnly");
                        this.BankExpenseTextBox.Attributes.Add("style", "background-color:#ffffff");
                        this.BankExpenseTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.BankExpenseTextBox.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");
                    }
                    else
                    {
                        this.BankExpenseTextBox.Text = "";
                        this.BankExpenseTextBox.Attributes.Add("ReadOnly", "true");
                        this.BankExpenseTextBox.Attributes.Add("style", "background-color:#cccccc");
                        this.BankExpenseTextBox.Attributes.Remove("OnBlur");
                    }
                    if (_fgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.DP))
                    {
                        ShowDPSupp();
                        DPTr.Visible = true;
                        DocTr.Visible = false;
                        DPSuppDropDownList.SelectedValue = "null";
                        DocumentNoTextBox.Text = "";
                    }
                    else
                    {
                        DPTr.Visible = false;
                        DocTr.Visible = true;
                        DPSuppDropDownList.SelectedValue = "null";
                        DocumentNoTextBox.Text = "";
                    }
                }
            }
            else
            {
                this.ShowData();
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINPayTradeCr _finPayTradeCr = this._paymentTradeBL.GetSingleFINPayTradeCr(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

            _finPayTradeCr.PayType = this.PayTypeDropDownList.SelectedValue;
            char _fgMode = _paymentBL.GetFgMode(this.PayTypeDropDownList.SelectedValue);
            if (_fgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Giro))
            {
                _finPayTradeCr.FgGiro = YesNoDataMapper.GetYesNo(YesNo.Yes);
            }
            else
            {
                _finPayTradeCr.FgGiro = YesNoDataMapper.GetYesNo(YesNo.No);
            }
            if (_fgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.DP))
            {
                _finPayTradeCr.FgDP = YesNoDataMapper.GetYesNo(YesNo.Yes);
                _finPayTradeCr.DocumentNo = this.DPSuppDropDownList.SelectedValue;
            }
            else
            {
                _finPayTradeCr.FgDP = YesNoDataMapper.GetYesNo(YesNo.No);
                _finPayTradeCr.DocumentNo = this.DocumentNoTextBox.Text;
            }

            _finPayTradeCr.CurrCode = this.CurrCodeTextBox.Text;
            _finPayTradeCr.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finPayTradeCr.AmountForex = Convert.ToDecimal(this.AmountForexTextBox.Text);
            decimal _amountHome = Convert.ToDecimal((this.AmountTotalTextBox.Text == null) ? "0" : this.AmountTotalTextBox.Text);
            _finPayTradeCr.AmountHome = _amountHome;
            _finPayTradeCr.Remark = this.RemarkTextBox.Text;

            if (this.BankPaymentDropDownList.SelectedValue != "null")
            {
                _finPayTradeCr.BankPayment = this.BankPaymentDropDownList.SelectedValue;
            }
            else
            {
                _finPayTradeCr.BankPayment = "";
            }

            if (this.DueDateTextBox.Text.Trim() != "")
            {
                _finPayTradeCr.DueDate = DateFormMapper.GetValue(this.DueDateTextBox.Text);
            }
            else
            {
                _finPayTradeCr.DueDate = null;
            }

            if (this.BankExpenseTextBox.Text != "")
            {
                _finPayTradeCr.BankExpense = Convert.ToDecimal(this.BankExpenseTextBox.Text);
            }
            else
            {
                _finPayTradeCr.BankExpense = null;
            }

            bool _result = this._paymentTradeBL.EditFINPayTradeCr(_finPayTradeCr);

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

        protected void DPSuppDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DPSuppDropDownList.SelectedValue != "null")
            {
                Decimal _baseForex = _dpSuppBL.GetSingleFINDPSuppHd(this.DPSuppDropDownList.SelectedValue).BaseForex;
                Decimal _balance = Convert.ToDecimal(_dpSuppBL.GetSingleFINDPSuppHd(this.DPSuppDropDownList.SelectedValue).Balance);
                AmountForexTextBox.Text = (_baseForex - _balance).ToString("#,##0.##");
                AmountTotalTextBox.Text = ((_baseForex - _balance) * Convert.ToDecimal(this.CurrRateTextBox.Text)).ToString("#,##0.##");
            }
        }
    }
}