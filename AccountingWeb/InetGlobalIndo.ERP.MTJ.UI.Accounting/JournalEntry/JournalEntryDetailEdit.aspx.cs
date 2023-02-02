using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.JournalEntry
{
    public partial class JournalEntryDetailEdit : JournalEntryBase
    {
        private JournalEntryBL _journalEntryBL = new JournalEntryBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRate = new CurrencyRateBL();
        private AccountBL _account = new AccountBL();
        private SubledBL _subled = new SubledBL();

        private User_EmployeeBL _userEmpBL = new User_EmployeeBL();
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
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.SaveAndNewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_new.jpg";

                this.ShowAccount();
                this.ShowCurrency();
                this.ShowDefaultCurrency();

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.ForexRateTextBox.Attributes.Add("OnBlur", "Clear(" + this.ForexRateTextBox.ClientID + ", " + this.DebitForexTextBox.ClientID + ", " + this.DebitHomeTextBox.ClientID + ", " + this.CreditForexTextBox.ClientID + ", " + this.CreditHomeTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + " ); ");
            this.DebitForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.ForexRateTextBox.ClientID + ", " + this.DebitForexTextBox.ClientID + ", " + this.DebitHomeTextBox.ClientID + ", " + this.CreditForexTextBox.ClientID + ", " + this.CreditHomeTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "); ");
            this.CreditForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.ForexRateTextBox.ClientID + ", " + this.DebitForexTextBox.ClientID + ", " + this.DebitHomeTextBox.ClientID + ", " + this.CreditForexTextBox.ClientID + ", " + this.CreditHomeTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "); ");

        }

        protected void SetAttribute()
        {
            this.DebitHomeTextBox.Attributes.Add("ReadOnly", "True");
            this.CreditHomeTextBox.Attributes.Add("ReadOnly", "True");

            this.ForexRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.DebitForexTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.CreditForexTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.SetAttributeRate();
        }

        public void ShowDefaultCurrency()
        {
            string _defaultCurrency = this._currencyBL.GetCurrDefault();
            this.DefaultLiteral.Text = _defaultCurrency;
        }

        public void ShowAccount()
        {
            this.AccountDropDownList.DataTextField = "AccountName";
            this.AccountDropDownList.DataValueField = "Account";
            this.AccountDropDownList.DataSource = this._account.GetListAccountByTransType(_userEmpBL.GetEmployeeIDByUserName(HttpContext.Current.User.Identity.Name), AppModule.GetValue(TransactionType.JournalEntry));
            this.AccountDropDownList.DataBind();
            this.AccountDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowCurrency()
        {
            this.CurrencyDropDownList.DataTextField = "CurrCode";
            this.CurrencyDropDownList.DataValueField = "CurrCode";
            this.CurrencyDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrencyDropDownList.DataBind();
            this.CurrencyDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void GetSubled()
        {
            char _tempSubled = this._account.GetAccountSubLed(this.AccountDropDownList.SelectedValue);
            this.FgSubledHiddenField.Value = _tempSubled.ToString();

            this.SubLedgerDropDownList.DataTextField = "SubLed_Name";
            this.SubLedgerDropDownList.DataValueField = "SubLed_No";
            this.SubLedgerDropDownList.DataSource = this._subled.GetListSubled(Convert.ToChar(this.FgSubledHiddenField.Value));
            this.SubLedgerDropDownList.DataBind();
            this.SubLedgerDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void AccountTextBox_TextChanged(object sender, EventArgs e)
        {
            bool _exist = this._account.IsExistAccount(AppModule.GetValue(TransactionType.JournalEntry), this.AccountTextBox.Text);
            if (_exist == true)
            {
                this.AccountDropDownList.SelectedValue = this.AccountTextBox.Text;
            }
            else
            {
                this.AccountTextBox.Text = "";
                this.AccountDropDownList.SelectedValue = "null";
            }

            this.GetSubled();
        }

        protected void AccountDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.AccountDropDownList.SelectedValue != "null")
            {
                this.AccountTextBox.Text = this.AccountDropDownList.SelectedValue;

                this.CurrencyDropDownList.SelectedValue = this._account.GetSingleAccount(AccountDropDownList.SelectedValue).CurrCode;
                string _currCodeHome = this._currencyBL.GetCurrDefault();
                this.ForexRateTextBox.Text = this._currencyRate.GetSingleLatestCurrRate(this.CurrencyDropDownList.SelectedValue).ToString("#,###.##");

                byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrencyDropDownList.SelectedValue);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                if (this.CurrencyDropDownList.SelectedValue == _currCodeHome)
                {
                    this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#cccccc");
                    this.ForexRateTextBox.Text = "1";
                    this.CreditHomeTextBox.Text = "0";
                    this.CreditForexTextBox.Text = "0";
                    this.DebitForexTextBox.Text = "0";
                    this.DebitHomeTextBox.Text = "0";
                }
                else
                {
                    this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#ffffff");
                    this.CreditHomeTextBox.Text = "0";
                    this.CreditForexTextBox.Text = "0";
                    this.DebitForexTextBox.Text = "0";
                    this.DebitHomeTextBox.Text = "0";
                }
            }
            else
            {
                this.AccountTextBox.Text = "";

                this.CurrencyDropDownList.SelectedValue = "null";

                this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#ffffff");
                this.CreditHomeTextBox.Text = "0";
                this.CreditForexTextBox.Text = "0";
                this.DebitForexTextBox.Text = "0";
                this.DebitHomeTextBox.Text = "0";
            }

            this.GetSubled();
        }

        protected void CurrencyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DecimalPlaceHiddenField.Value = "";

            string _currCodeHome = _currencyBL.GetCurrDefault();
            this.ForexRateTextBox.Text = _currencyRate.GetSingleLatestCurrRate(this.CurrencyDropDownList.SelectedValue).ToString("#,###.##");

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrencyDropDownList.SelectedValue);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            if (this.CurrencyDropDownList.SelectedValue == _currCodeHome)
            {
                this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#cccccc");
                this.CreditHomeTextBox.Text = "0";
                this.CreditForexTextBox.Text = "0";
                this.DebitForexTextBox.Text = "0";
                this.DebitHomeTextBox.Text = "0";
            }
            else
            {
                this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#ffffff");
                this.CreditHomeTextBox.Text = "0";
                this.CreditForexTextBox.Text = "0";
                this.DebitForexTextBox.Text = "0";
                this.DebitHomeTextBox.Text = "0";
            }
        }

        public void ShowData()
        {
            this.DecimalPlaceHiddenField.Value = "";

            GLJournalDt _glJournalDt = this._journalEntryBL.GetSingleGLJournalDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeReference), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeTransClass), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_glJournalDt.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            this.AccountTextBox.Text = _glJournalDt.Account;
            this.AccountDropDownList.SelectedValue = _glJournalDt.Account;
            this.GetSubled();
            this.SubLedgerDropDownList.SelectedValue = _glJournalDt.SubLed;
            this.FgSubledHiddenField.Value = _glJournalDt.FgSubLed.ToString();
            this.RemarkTextBox.Text = _glJournalDt.Remark;
            this.CurrencyDropDownList.SelectedValue = _glJournalDt.CurrCode;
            //this.DebitCurrLiteral.Text = this.CurrencyDropDownList.SelectedValue;
            //this.CreditCurrLiteral.Text = this.CurrencyDropDownList.SelectedValue;
            this.ForexRateTextBox.Text = (_glJournalDt.ForexRate == 0 ? "0" : _glJournalDt.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));
            this.DebitForexTextBox.Text = (_glJournalDt.DebitForex == 0 ? "0" : _glJournalDt.DebitForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));
            this.DebitHomeTextBox.Text = (_glJournalDt.DebitHome == 0 ? "0" : _glJournalDt.DebitHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));
            this.CreditForexTextBox.Text = (_glJournalDt.CreditForex == 0 ? "0" : _glJournalDt.CreditForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));
            this.CreditHomeTextBox.Text = (_glJournalDt.CreditHome == 0 ? "0" : _glJournalDt.CreditHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            GLJournalDt _glJournalDt = this._journalEntryBL.GetSingleGLJournalDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeReference), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeTransClass), ApplicationConfig.EncryptionKey));

            _glJournalDt.Account = this.AccountDropDownList.SelectedValue;
            if (this.SubLedgerDropDownList.SelectedValue != "null")
            {
                _glJournalDt.FgSubLed = Convert.ToChar(this.FgSubledHiddenField.Value);
                _glJournalDt.SubLed = this.SubLedgerDropDownList.SelectedValue;
            }
            else
            {
                _glJournalDt.FgSubLed = 'N';
                _glJournalDt.SubLed = null;
            }
            _glJournalDt.Remark = this.RemarkTextBox.Text;
            _glJournalDt.CurrCode = this.CurrencyDropDownList.SelectedValue;
            _glJournalDt.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _glJournalDt.DebitForex = Convert.ToDecimal(this.DebitForexTextBox.Text);
            _glJournalDt.DebitHome = Convert.ToDecimal(this.DebitHomeTextBox.Text);
            _glJournalDt.CreditForex = Convert.ToDecimal(this.CreditForexTextBox.Text);
            _glJournalDt.CreditHome = Convert.ToDecimal(this.CreditHomeTextBox.Text);

            bool _result = this._journalEntryBL.EditGLJournalDt(_glJournalDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeReference)) + "&" + this._codeTransClass + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeTransClass)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void SaveAndNewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            GLJournalDt _glJournalDt = this._journalEntryBL.GetSingleGLJournalDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeReference), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeTransClass), ApplicationConfig.EncryptionKey));

            _glJournalDt.Account = this.AccountDropDownList.SelectedValue;
            if (this.SubLedgerDropDownList.SelectedValue != "null")
            {
                _glJournalDt.FgSubLed = Convert.ToChar(this.FgSubledHiddenField.Value);
                _glJournalDt.SubLed = this.SubLedgerDropDownList.SelectedValue;
            }
            else
            {
                _glJournalDt.FgSubLed = 'N';
                _glJournalDt.SubLed = null;
            }
            _glJournalDt.Remark = this.RemarkTextBox.Text;
            _glJournalDt.CurrCode = this.CurrencyDropDownList.SelectedValue;
            _glJournalDt.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _glJournalDt.DebitForex = Convert.ToDecimal(this.DebitForexTextBox.Text);
            _glJournalDt.DebitHome = Convert.ToDecimal(this.DebitHomeTextBox.Text);
            _glJournalDt.CreditForex = Convert.ToDecimal(this.CreditForexTextBox.Text);
            _glJournalDt.CreditHome = Convert.ToDecimal(this.CreditHomeTextBox.Text);

            bool _result = this._journalEntryBL.EditGLJournalDt(_glJournalDt);

            if (_result == true)
            {
                Response.Redirect(this._addDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeReference)) + "&" + this._codeTransClass + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeTransClass)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeReference)) + "&" + this._codeTransClass + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeTransClass)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}