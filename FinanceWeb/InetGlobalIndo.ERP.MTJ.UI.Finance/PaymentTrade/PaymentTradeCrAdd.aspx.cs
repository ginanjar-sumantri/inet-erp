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
    public partial class PaymentTradeCrAdd : PaymentTradeBase
    {
        private AccountBL _accountBL = new AccountBL();
        private PaymentTradeBL _paymentTradeBL = new PaymentTradeBL();
        private PaymentBL _paymentBL = new PaymentBL();
        private CurrencyRateBL _currRateBL = new CurrencyRateBL();
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

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
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
                this.DueDateLiteral.Text = "<input id='button1' style ='visibility:visible' type='button' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.ShowPayType();

                this.ClearData();
                this.SetAttribute();
            }
        }

        private void SetAttribute()
        {
            this.CurrCodeTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountTotalTextBox.Attributes.Add("ReadOnly", "True");
            this.DueDateTextBox.Attributes.Add("ReadOnly", "True");
            this.BankExpenseTextBox.Attributes.Add("ReadOnly", "True");

            this.CurrRateTextBox.Attributes.Add("OnBlur", "CalculateAmountTotal(" + this.CurrRateTextBox.ClientID + "," + this.AmountForexTextBox.ClientID + "," + this.AmountTotalTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");
            this.AmountForexTextBox.Attributes.Add("OnBlur", "CalculateAmountTotal(" + this.CurrRateTextBox.ClientID + "," + this.AmountForexTextBox.ClientID + "," + this.AmountTotalTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");
            this.BankExpenseTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.BankExpenseTextBox.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");

            //this.headline_date_start.Attributes.Add("Style", "visibility:hidden");
            this.DueDateLiteral.Text = "<input id='button1' style ='visibility:hidden' type='button' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
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

        protected void PayTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.PayTypeDropDownList.SelectedValue != "null")
            {
                this.CurrCodeTextBox.Text = _paymentBL.GetCurrCodeByCode(this.PayTypeDropDownList.SelectedValue);

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
                this.AmountTotalTextBox.Text = "0";

                char _fgMode = _paymentBL.GetFgMode(this.PayTypeDropDownList.SelectedValue);
                if (_fgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Giro))
                {
                    DateTime now = DateTime.Now.Date;
                    this.ShowBankPayment();
                    this.BankPaymentDropDownList.Enabled = true;
                    this.BankPaymentCustomValidator.Enabled = true;
                    this.DocNoRequiredFieldValidator.Enabled = true;
                    //this.headline_date_start.Attributes.Add("Style", "visibility:visible");
                    this.DueDateLiteral.Text = "<input id='button1' style ='visibility:visible' type='button' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                    this.DueDateTextBox.Text = DateFormMapper.GetValue(now);
                    this.BankExpenseTextBox.Text = "";
                    this.BankExpenseTextBox.Attributes.Add("ReadOnly", "True");
                    this.BankExpenseTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                }
                else
                {
                    this.DocNoRequiredFieldValidator.Enabled = false;
                    this.BankPaymentDropDownList.SelectedValue = "null";
                    this.BankPaymentDropDownList.Enabled = false;
                    this.BankPaymentCustomValidator.Enabled = false;
                    this.DueDateTextBox.Text = "";
                    //this.headline_date_start.Attributes.Add("Style", "visibility:hidden");
                    this.DueDateLiteral.Text = "<input id='button1' style ='visibility:hidden' type='button' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                    this.BankExpenseTextBox.Text = "";
                    this.BankExpenseTextBox.Attributes.Remove("ReadOnly");
                    this.BankExpenseTextBox.Attributes.Add("style", "background-color:#FFFFFF");

                    if (_fgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Bank))
                    {
                        this.BankExpenseTextBox.Text = "";
                        this.BankExpenseTextBox.Attributes.Remove("ReadOnly");
                        this.BankExpenseTextBox.Attributes.Add("Style", "background-color:#FFFFFF");
                    }
                    else
                    {
                        this.BankExpenseTextBox.Text = "";
                        this.BankExpenseTextBox.Attributes.Add("ReadOnly", "True");
                        this.BankExpenseTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
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
                this.ClearData();
            }
        }

        public void ClearData()
        {
            DateTime now = DateTime.Now.Date;

            this.ClearLabel();

            this.PayTypeDropDownList.SelectedValue = "null";
            this.DecimalPlaceHiddenField.Value = "";
            this.DecimalPlaceHiddenField2.Value = "";
            this.DocTr.Visible = true;
            this.DPTr.Visible = false;
            this.DPSuppDropDownList.SelectedValue = "null";
            this.DocumentNoTextBox.Text = "";
            this.CurrCodeTextBox.Text = "";
            this.CurrRateTextBox.Text = "0";
            this.AmountForexTextBox.Text = "0";
            this.AmountTotalTextBox.Text = "0";
            this.RemarkTextBox.Text = "";
            this.BankPaymentDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.BankPaymentDropDownList.SelectedValue = "null";
            this.DueDateTextBox.Text = "";
            this.BankExpenseTextBox.Text = "0";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            int _maxItemNo = this._paymentTradeBL.GetMaxNoItemFINPayTradeCr(_transNo);

            FINPayTradeCr _finPayTradeCr = new FINPayTradeCr();

            _finPayTradeCr.TransNmbr = _transNo;
            _finPayTradeCr.ItemNo = _maxItemNo + 1;
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
                _finPayTradeCr.DocumentNo = DPSuppDropDownList.SelectedValue;
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
            _finPayTradeCr.BankPayment = this.BankPaymentDropDownList.SelectedValue;
            if (this.DueDateTextBox.Text != "")
            {
                _finPayTradeCr.DueDate = DateFormMapper.GetValue(this.DueDateTextBox.Text);
            }
            if (this.BankExpenseTextBox.Text != "")
            {
                decimal _bankExpense = Convert.ToDecimal((this.BankExpenseTextBox.Text == null) ? "0" : this.BankExpenseTextBox.Text);
                _finPayTradeCr.BankExpense = _bankExpense;
            }

            bool _result = this._paymentTradeBL.AddFINPayTradeCr(_finPayTradeCr);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
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