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

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.DPCustomerReceipt
{
    public partial class DPCustReceiptDetailAdd : DPCustomerReceiptBase
    {
        private FINDPCustomerBL _finDPCustBL = new FINDPCustomerBL();
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

            _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.DueDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowReceiptType();
                this.ShowBankGiro();

                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void SetAttribute()
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _currCode = this._finDPCustBL.GetCurrCodeHeader(_transNo);
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            this.DueDateTextBox.Attributes.Add("ReadOnly", "True");

            this.AmountForexTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.BankExpenseTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");

            this.AmountForexTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + AmountForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.BankExpenseTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + BankExpenseTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            this.ClearLabel();

            this.ReceiptTypeDropDownList.SelectedValue = "null";
            this.DocNoTextBox.Text = "";
            this.AmountForexTextBox.Text = "";
            this.DueDateTextBox.Text = "";
            this.BankGiroDropDownList.SelectedValue = "null";
            this.RemarkTextBox.Text = "";
            this.BankExpenseTextBox.Text = "";
            this.ImageSpan.Visible = false;
            this.FgGiroHiddenField.Value = "";
        }

        private void ShowReceiptType()
        {
            string _currCodeHeader = _finDPCustBL.GetCurrCodeHeader(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_currCodeHeader != "")
            {
                this.ReceiptTypeDropDownList.Items.Clear();
                this.ReceiptTypeDropDownList.DataTextField = "PayName";
                this.ReceiptTypeDropDownList.DataValueField = "PayCode";
                this.ReceiptTypeDropDownList.DataSource = this._payBL.GetListDDLDPCustomer(_currCodeHeader);
                this.ReceiptTypeDropDownList.DataBind();
                this.ReceiptTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
            else
            {
                this.ReceiptTypeDropDownList.Items.Clear();
                this.ReceiptTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
        }

        private void ShowBankGiro()
        {
            this.BankGiroDropDownList.Items.Clear();
            this.BankGiroDropDownList.DataTextField = "BankName";
            this.BankGiroDropDownList.DataValueField = "BankCode";
            this.BankGiroDropDownList.DataSource = this._bankBL.GetList();
            this.BankGiroDropDownList.DataBind();
            this.BankGiroDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINDPCustDt _finDPCustDt = new FINDPCustDt();

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            int _maxItemNo = this._finDPCustBL.GetMaxNoItemFINDPCustDt(_transNo);

            _finDPCustDt.TransNmbr = _transNo;
            _finDPCustDt.ItemNo = _maxItemNo + 1;
            _finDPCustDt.ReceiptType = this.ReceiptTypeDropDownList.SelectedValue;
            _finDPCustDt.DocumentNo = this.DocNoTextBox.Text;
            _finDPCustDt.AmountForex = Convert.ToDecimal(this.AmountForexTextBox.Text);
            _finDPCustDt.Remark = this.RemarkTextBox.Text;
            _finDPCustDt.FgGiro = Convert.ToChar(this.FgGiroHiddenField.Value);

            _finDPCustDt.BankExpense = Convert.ToDecimal((this.BankExpenseTextBox.Text == "") ? "0" : this.BankExpenseTextBox.Text.Trim());
            _finDPCustDt.BankGiro = this.BankGiroDropDownList.SelectedValue;
            if (this.DueDateTextBox.Text != "")
            {
                _finDPCustDt.DueDate = DateFormMapper.GetValue(this.DueDateTextBox.Text);
            }

            bool _result = this._finDPCustBL.AddFINDPCustDt(_finDPCustDt);

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

        protected void ReceiptTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            char _fgMode = this._payBL.GetFgMode(this.ReceiptTypeDropDownList.SelectedValue);

            if (_fgMode == PaymentDataMapper.GetModeReceiptType(ModeReceiptType.Giro))
            {
                this.FgGiroHiddenField.Value = (GiroReceiptDataMapper.GetYesNo(YesNo.Yes)).ToString();

                this.DocumentNoRequiredFieldValidator.Enabled = true;

                this.BankGiroDropDownList.Enabled = true;

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

                this.BankGiroDropDownList.SelectedValue = "null";
                this.BankGiroDropDownList.Enabled = false;

                this.DueDateTextBox.Text = "";
                this.ImageSpan.Visible = false;

                if (_fgMode == PaymentDataMapper.GetModeReceiptType(ModeReceiptType.Bank))
                {
                    this.BankExpenseTextBox.Attributes.Remove("ReadOnly");
                    this.BankExpenseTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                    this.BankExpenseTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + BankExpenseTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
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
    }
}