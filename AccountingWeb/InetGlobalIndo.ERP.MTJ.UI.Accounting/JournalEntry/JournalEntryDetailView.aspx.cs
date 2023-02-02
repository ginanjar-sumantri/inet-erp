using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.JournalEntry
{
    public partial class JournalEntryDetailView : JournalEntryBase
    {
        private JournalEntryBL _journalEntryBL = new JournalEntryBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRate = new CurrencyRateBL();
        private AccountBL _account = new AccountBL();
        private SubledBL _subled = new SubledBL();
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.SetButtonPermission();

                this.ShowDefaultCurrency();
                this.ShowData();
            }
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        public void ShowDefaultCurrency()
        {
            string _defaultCurrency = this._currencyBL.GetCurrDefault();
            this.DefaultLiteral.Text = _defaultCurrency;
        }

        public void ShowData()
        {
            GLJournalDt _glJournalDt = this._journalEntryBL.GetSingleGLJournalDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeReference), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeTransClass), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_glJournalDt.CurrCode);

            this.AccountTextBox.Text = _glJournalDt.Account;
            this.AccountNameTextBox.Text = _account.GetAccountNameByCode(_glJournalDt.Account);
            this.SubLedgerTextBox.Text = _journalEntryBL.GetSubledNameBySubledCode(_glJournalDt.SubLed);
            this.FgSubledHiddenField.Value = _glJournalDt.FgSubLed.ToString();
            this.RemarkTextBox.Text = _glJournalDt.Remark;
            this.CurrencyTextBox.Text = _glJournalDt.CurrCode;
            //this.DebitCurrLiteral.Text = this.CurrencyTextBox.Text;
            //this.CreditCurrLiteral.Text = this.CurrencyTextBox.Text;
            this.ForexRateTextBox.Text = (_glJournalDt.ForexRate == 0 ? "0" : _glJournalDt.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));
            this.DebitForexTextBox.Text = (_glJournalDt.DebitForex == 0 ? "0" : _glJournalDt.DebitForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));
            this.DebitHomeTextBox.Text = (_glJournalDt.DebitHome == 0 ? "0" : _glJournalDt.DebitHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));
            this.CreditForexTextBox.Text = (_glJournalDt.CreditForex == 0 ? "0" : _glJournalDt.CreditForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));
            this.CreditHomeTextBox.Text = (_glJournalDt.CreditHome == 0 ? "0" : _glJournalDt.CreditHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeReference + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeReference)) + "&" + this._codeTransClass + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeTransClass)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeReference)) + "&" + this._codeTransClass + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeTransClass)));
        }
    }
}