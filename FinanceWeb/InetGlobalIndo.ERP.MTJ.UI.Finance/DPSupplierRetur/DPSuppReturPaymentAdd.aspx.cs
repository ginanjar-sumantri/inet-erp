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
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.DPSupplierRetur
{
    public partial class DPSuppReturPaymentAdd : DPSupplierReturBase
    {
        private PaymentBL _paymentBL = new PaymentBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private BankBL _bankBL = new BankBL();
        private FINDPSuppReturBL _finDPSuppReturBL = new FINDPSuppReturBL();
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

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetCurrRate();
                this.SetAttribute();
                this.ShowReceiptType();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.CurrRateTextBox.Attributes.Add("OnBlur", "Calculate(" + this.CurrRateTextBox.ClientID + "," + this.AmountForexTextBox.ClientID + "," + this.AmountHomeTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHiddenFieldHome.ClientID + " );");
            this.AmountForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.CurrRateTextBox.ClientID + "," + this.AmountForexTextBox.ClientID + "," + this.AmountHomeTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHiddenFieldHome.ClientID + ");");
        }

        public void SetAttribute()
        {
            this.CurrCodeTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountHomeTextBox.Attributes.Add("ReadOnly", "True");
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.SetAttributeRate();
        }

        public void ClearData()
        {
            this.ClearLabel();
            this.ReceiptTypeDropDownList.SelectedValue = "null";
            this.FgGiroHiddenField.Value = "";
            this.DocumentNoTextBox.Text = "";
            this.DocNoRequiredFieldValidator.Enabled = false;
            this.CurrCodeTextBox.Text = "";
            this.CurrRateTextBox.Text = "";
            this.AmountForexTextBox.Text = "0";
            this.AmountHomeTextBox.Text = "0";
            this.RemarkTextBox.Text = "";
            this.BankGiroDropDownList.Items.Clear();
            this.BankGiroDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.BankGiroDropDownList.SelectedValue = "null";
            this.BankGiroDropDownList.Enabled = false;
            this.BankGiroCustomValidator.Enabled = false;
            this.DateTextBox.Text = "";
            this.ImageSpan.Visible = false;
            this.BankExpenseTextBox.Text = "";
            this.BankExpenseTextBox.Attributes.Add("ReadOnly", "true");
            this.BankExpenseTextBox.Attributes.Add("style", "background-color:#cccccc");
            this.BankExpenseTextBox.Attributes.Remove("OnBlur");
        }

        public void ShowReceiptType()
        {
            this.ReceiptTypeDropDownList.Items.Clear();
            this.ReceiptTypeDropDownList.DataTextField = "PayName";
            this.ReceiptTypeDropDownList.DataValueField = "PayCode";
            this.ReceiptTypeDropDownList.DataSource = this._paymentBL.GetListDDLGiroReceiptChangePay();
            this.ReceiptTypeDropDownList.DataBind();
            this.ReceiptTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowBankGiro()
        {
            this.BankGiroDropDownList.Items.Clear();
            this.BankGiroDropDownList.DataTextField = "BankName";
            this.BankGiroDropDownList.DataValueField = "BankCode";
            this.BankGiroDropDownList.DataSource = this._bankBL.GetList();
            this.BankGiroDropDownList.DataBind();
            this.BankGiroDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ReceiptTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ReceiptTypeDropDownList.SelectedValue != "null")
            {
                char _fgMode = this._paymentBL.GetFgMode(this.ReceiptTypeDropDownList.SelectedValue);

                this.CurrCodeTextBox.Text = this._paymentBL.GetCurrCodeByCode(this.ReceiptTypeDropDownList.SelectedValue);

                decimal _tempCurrRate = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeTextBox.Text);

                byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrCodeTextBox.Text);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                string _defaultCurrency = this._currencyBL.GetCurrDefault();
                if (this.CurrCodeTextBox.Text.Trim().ToLower() == _defaultCurrency.Trim().ToLower())
                {
                    this.CurrRateTextBox.Text = "1";
                    this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.CurrRateTextBox.Attributes.Add("style", "background-color:#cccccc");
                }
                else
                {
                    this.CurrRateTextBox.Text = _tempCurrRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                    this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                    this.CurrRateTextBox.Attributes.Add("style", "background-color:#ffffff");
                }

                if (_fgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Giro))
                {
                    this.FgGiroHiddenField.Value = (YesNoDataMapper.GetYesNo(YesNo.Yes)).ToString();

                    this.DocNoRequiredFieldValidator.Enabled = true;

                    this.BankGiroDropDownList.Enabled = true;
                    this.BankGiroCustomValidator.Enabled = true;
                    this.ShowBankGiro();

                    this.ImageSpan.Visible = true;
                    this.DateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);

                    this.BankExpenseTextBox.Text = "";
                    this.BankExpenseTextBox.Attributes.Add("ReadOnly", "true");
                    this.BankExpenseTextBox.Attributes.Add("style", "background-color:#cccccc");
                    this.BankExpenseTextBox.Attributes.Remove("OnBlur");

                }
                else
                {
                    this.FgGiroHiddenField.Value = (YesNoDataMapper.GetYesNo(YesNo.No)).ToString();

                    this.DocNoRequiredFieldValidator.Enabled = false;

                    this.BankGiroDropDownList.Enabled = false;
                    this.BankGiroCustomValidator.Enabled = false;
                    this.BankGiroDropDownList.Items.Clear();
                    this.BankGiroDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

                    this.ImageSpan.Visible = false;
                    this.DateTextBox.Text = "";

                    if (_fgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Bank))
                    {
                        this.BankExpenseTextBox.Attributes.Remove("ReadOnly");
                        this.BankExpenseTextBox.Attributes.Add("style", "background-color:#ffffff");
                        this.BankExpenseTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.BankExpenseTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + " );");
                    }
                    else
                    {
                        this.BankExpenseTextBox.Text = "";
                        this.BankExpenseTextBox.Attributes.Add("ReadOnly", "true");
                        this.BankExpenseTextBox.Attributes.Add("style", "background-color:#cccccc");
                        this.BankExpenseTextBox.Attributes.Remove("OnBlur");
                    }
                }
            }
            else
            {
                this.ClearData();
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            int _maxItemNo = this._finDPSuppReturBL.GetMaxNoItemFINDPSuppReturPay(_transNo);

            FINDPSuppReturPay _finDPSuppReturPay = new FINDPSuppReturPay();
            _finDPSuppReturPay.TransNmbr = _transNo;
            _finDPSuppReturPay.ItemNo = _maxItemNo + 1;
            _finDPSuppReturPay.ReceiptType = this.ReceiptTypeDropDownList.SelectedValue;
            _finDPSuppReturPay.FgGiro = Convert.ToChar(this.FgGiroHiddenField.Value);
            _finDPSuppReturPay.DocumentNo = this.DocumentNoTextBox.Text;
            _finDPSuppReturPay.CurrCode = this.CurrCodeTextBox.Text;
            _finDPSuppReturPay.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finDPSuppReturPay.AmountForex = Convert.ToDecimal(this.AmountForexTextBox.Text);
            _finDPSuppReturPay.AmountHome = Convert.ToDecimal(this.AmountHomeTextBox.Text);
            _finDPSuppReturPay.Remark = this.RemarkTextBox.Text;

            if (this.BankGiroDropDownList.SelectedValue != "null")
            {
                _finDPSuppReturPay.BankGiro = this.BankGiroDropDownList.SelectedValue;
            }
            else
            {
                _finDPSuppReturPay.BankGiro = "";
            }

            if (this.DateTextBox.Text.Trim() != "")
            {
                _finDPSuppReturPay.DueDate = DateFormMapper.GetValue(this.DateTextBox.Text);
            }
            else
            {
                _finDPSuppReturPay.DueDate = null;
            }

            if (this.BankExpenseTextBox.Text != "")
            {
                _finDPSuppReturPay.BankExpense = Convert.ToDecimal(this.BankExpenseTextBox.Text);
            }
            else
            {
                _finDPSuppReturPay.BankExpense = null;
            }

            bool _result = this._finDPSuppReturBL.AddFINDPSuppReturPay(_finDPSuppReturPay);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
            }

        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        private void SetCurrRate()
        {
            this.DecimalPlaceHiddenFieldHome.Value = this._currencyBL.GetCurrDefault();
        }
    }
}