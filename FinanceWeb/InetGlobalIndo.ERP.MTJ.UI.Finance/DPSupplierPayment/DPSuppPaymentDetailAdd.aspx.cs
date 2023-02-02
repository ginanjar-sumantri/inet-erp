using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.DPSupplierPayment
{
    public partial class DPSuppPaymentDetailAdd : DPSupplierPaymentBase
    {
        private FINDPSuppPayBL _finDPSuppBL = new FINDPSuppPayBL();
        private PaymentBL _payBL = new PaymentBL();
        private BankBL _bankBL = new BankBL();
        private CurrencyRateBL _currRateBL = new CurrencyRateBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();

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
                this.DueDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
               
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowPaymentType();
                this.ShowBankPayment();
                this.SetCurrRate();
                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.AmountForexTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + AmountForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.BankExpenseTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + BankExpenseTextBox.ClientID + "," + this.DecimalPlaceHiddenFieldHome.ClientID + ");");

        }

        protected void SetAttribute()
        {
            this.DueDateTextBox.Attributes.Add("ReadOnly", "True");
            this.TransNoTextBox.Attributes.Add("ReadOnly", "True");

            this.AmountForexTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.BankExpenseTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.SetAttributeRate();
        }

        private void ClearData()
        {
            this.ClearLabel();
            //this.TransNoTextBox.Text = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            this.PaymentTypeDropDownList.SelectedValue = "null";
            this.DocNoTextBox.Text = "";
            this.AmountForexTextBox.Text = "";
            this.DueDateTextBox.Text = "";
            this.BankPaymentDropDownList.SelectedValue = "null";
            this.RemarkTextBox.Text = "";
            this.BankExpenseTextBox.Text = "";
            this.ImageSpan.Visible = false;
            this.FgGiroHiddenField.Value = "";

            this.ClearDataNumeric();
        }

        private void ShowPaymentType()
        {
            string _currCodeHeader = _finDPSuppBL.GetCurrCodeHeader(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_currCodeHeader != "")
            {
                this.PaymentTypeDropDownList.Items.Clear();
                this.PaymentTypeDropDownList.DataTextField = "PayName";
                this.PaymentTypeDropDownList.DataValueField = "PayCode";
                this.PaymentTypeDropDownList.DataSource = this._payBL.GetListDDLDPSuppPay(_currCodeHeader);
                this.PaymentTypeDropDownList.DataBind();
                this.PaymentTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
            else
            {
                this.PaymentTypeDropDownList.Items.Clear();
                this.PaymentTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
        }

        private void ShowBankPayment()
        {
            string _currCodeHeader = _finDPSuppBL.GetCurrCodeHeader(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_currCodeHeader != "")
            {
                this.BankPaymentDropDownList.Items.Clear();
                this.BankPaymentDropDownList.DataTextField = "PayName";
                this.BankPaymentDropDownList.DataValueField = "PayCode";
                this.BankPaymentDropDownList.DataSource = this._payBL.GetListDDLbyViewMsBankPayment(_currCodeHeader);
                this.BankPaymentDropDownList.DataBind();
                this.BankPaymentDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
            else
            {
                this.BankPaymentDropDownList.Items.Clear();
                this.BankPaymentDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINDPSuppDt _finDPSuppDt = new FINDPSuppDt();

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            int _maxItemNo = this._finDPSuppBL.GetMaxNoItemFINDPSuppDt(_transNo);

            _finDPSuppDt.TransNmbr = _transNo;
            _finDPSuppDt.ItemNo = _maxItemNo + 1;
            _finDPSuppDt.PayType = this.PaymentTypeDropDownList.SelectedValue;
            _finDPSuppDt.DocumentNo = this.DocNoTextBox.Text;
            _finDPSuppDt.AmountForex = Convert.ToDecimal(this.AmountForexTextBox.Text);
            _finDPSuppDt.Remark = this.RemarkTextBox.Text;
            _finDPSuppDt.FgGiro = Convert.ToChar(this.FgGiroHiddenField.Value);
            if (this.BankExpenseTextBox.Text != "")
            {
                _finDPSuppDt.BankExpense = Convert.ToDecimal(this.BankExpenseTextBox.Text);
            }
            _finDPSuppDt.BankPayment = this.BankPaymentDropDownList.SelectedValue;
            if (this.DueDateTextBox.Text != "")
            {
                _finDPSuppDt.DueDate = DateFormMapper.GetValue(this.DueDateTextBox.Text);
            }

            bool _result = this._finDPSuppBL.AddFINDPSuppDt(_finDPSuppDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
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

        protected void PaymentTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            char _fgMode = this._payBL.GetFgMode(this.PaymentTypeDropDownList.SelectedValue);

            if (_fgMode == PaymentDataMapper.GetModeReceiptType(ModeReceiptType.Giro))
            {
                this.FgGiroHiddenField.Value = (GiroReceiptDataMapper.GetYesNo(YesNo.Yes)).ToString();

                this.DocumentNoRequiredFieldValidator.Enabled = true;

                this.BankPaymentDropDownList.Enabled = true;
                this.BankPaymentCustomValidator.Enabled = true;

                this.DueDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
                this.ImageSpan.Visible = true;

                this.BankExpenseTextBox.Text = "";
                this.BankExpenseTextBox.Attributes.Add("ReadOnly", "true");
                this.BankExpenseTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                this.BankExpenseTextBox.Attributes.Remove("OnBlur");
            }
            else
            {
                this.FgGiroHiddenField.Value = (GiroReceiptDataMapper.GetYesNo(YesNo.No)).ToString();

                this.DocumentNoRequiredFieldValidator.Enabled = false;

                this.BankPaymentDropDownList.SelectedValue = "null";
                this.BankPaymentDropDownList.Enabled = false;
                this.BankPaymentCustomValidator.Enabled = false;

                this.DueDateTextBox.Text = "";
                this.ImageSpan.Visible = false;

                if (_fgMode == PaymentDataMapper.GetModeReceiptType(ModeReceiptType.Bank))
                {
                    this.BankExpenseTextBox.Attributes.Remove("ReadOnly");
                    this.BankExpenseTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                    this.BankExpenseTextBox.Attributes.Add("OnBlur", "ChangeFormat(" + this.BankExpenseTextBox.ClientID + ");");
                }
                else
                {
                    this.BankExpenseTextBox.Text = "";
                    this.BankExpenseTextBox.Attributes.Add("ReadOnly", "true");
                    this.BankExpenseTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.BankExpenseTextBox.Attributes.Remove("OnBlur");
                }
            }
        }


        private void ClearDataNumeric()
        {
            this.AmountForexTextBox.Text = "0";
            this.BankExpenseTextBox.Text = "0";
        }

        private void SetCurrRate()
        {
            string _currCode = _finDPSuppBL.GetCurrCodeHeader(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            string _currCodeHome = this._currencyBL.GetCurrDefault();
            byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(_currCodeHome);
            this.DecimalPlaceHiddenFieldHome.Value = CurrencyDataMapper.GetDecimal(_decimalPlaceHome);
        }
    }
}