using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PettyCash
{
    public partial class PettyCashDetailAdd : PettyCashBase
    {
        private PettyBL _pettyBL = new PettyBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private AccountBL _accountBL = new AccountBL();
        private SubledBL _subledBL = new SubledBL();
        private EmployeeBL _empBL = new EmployeeBL();
        private User_EmployeeBL _userEmpBL = new User_EmployeeBL();
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
            FINPettyHd _finPettyHd = this._pettyBL.GetSingleHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            this._codeTransaction = _finPettyHd.TransNmbr;

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowAccount();

                this.SetAttribute();
                this.ClearData();
            }
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribute()
        {
            this.AccountTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.AmountTextbox.Attributes.Add("OnBlur", "AmountForex_OnBlur(" + this.AmountTextbox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        public void ClearData()
        {
            this.ClearLabel();
            this.SubLedgerDropDownList.Items.Clear();
            this.SubLedgerDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.SubLedgerDropDownList.SelectedValue = "null";

            this.AccountDropDownList.SelectedValue = "null";
            this.AccountTextBox.Text = "";

            this.FgSubledHiddenField.Value = "";
            this.AmountTextbox.Text = "0";
            this.DecimalPlaceHiddenField.Value = "";
            this.RemarkTextBox.Text = "";

            FINPettyHd _finPettyHd = _pettyBL.GetSingleHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finPettyHd.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
        }

        public void ShowAccount()
        {
            string _curr = this._pettyBL.GetCurrHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            this.AccountDropDownList.Items.Clear();
            this.AccountDropDownList.DataTextField = "AccountName";
            this.AccountDropDownList.DataValueField = "Account";
            this.AccountDropDownList.DataSource = this._accountBL.GetListAccountByTransType(_curr, _userEmpBL.GetEmployeeIDByUserName(HttpContext.Current.User.Identity.Name), AppModule.GetValue(TransactionType.PettyCash));
            this.AccountDropDownList.DataBind();
            this.AccountDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void GetSubled()
        {
            char _tempSubled = this._accountBL.GetAccountSubLed(this.AccountDropDownList.SelectedValue);
            this.FgSubledHiddenField.Value = _tempSubled.ToString();

            this.SubLedgerDropDownList.DataTextField = "SubLed_Name";
            this.SubLedgerDropDownList.DataValueField = "SubLed_No";
            this.SubLedgerDropDownList.DataSource = this._subledBL.GetListSubled(Convert.ToChar(this.FgSubledHiddenField.Value));
            this.SubLedgerDropDownList.DataBind();
            this.SubLedgerDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowData()
        {
            this.GetSubled();

            string _currCodeHome = _currencyBL.GetCurrDefault();
            MsAccount _acc = _accountBL.GetSingleAccount(this.AccountDropDownList.SelectedValue);
        }

        private void SetCurrRate()
        {
            string _curren;
            _curren = this._accountBL.GetCurrByAccCode(this.AccountTextBox.Text);
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_curren);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
        }

        protected void AccountDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.AccountTextBox.Text = this.AccountDropDownList.SelectedValue;
            this.ShowData();
            this.GetSubled();
        }

        protected void AccountTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.AccountDropDownList.SelectedValue = this.AccountTextBox.Text;
                this.ShowData();
                this.SetCurrRate();
            }
            catch (Exception ex)
            {
                this.AccountTextBox.Text = "";
                this.AccountDropDownList.SelectedValue = "null";
            }
            this.GetSubled();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNumber = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            int _maxItemNo = this._pettyBL.GetMaxNoItem(_transNumber);

            FINPettyDt _finPettyDt = new FINPettyDt();

            _finPettyDt.TransNmbr = _transNumber;
            _finPettyDt.ItemNo = _maxItemNo + 1;
            _finPettyDt.Account = this.AccountDropDownList.SelectedValue;

            if (this.SubLedgerDropDownList.SelectedValue != "null")
            {
                _finPettyDt.FgSubLed = Convert.ToChar(this.FgSubledHiddenField.Value);
                _finPettyDt.SubLed = this.SubLedgerDropDownList.SelectedValue;
            }
            else
            {
                _finPettyDt.FgSubLed = 'N';
                _finPettyDt.SubLed = null;
            }

            _finPettyDt.Remark = this.RemarkTextBox.Text;
            _finPettyDt.AmountForex = Convert.ToDecimal(this.AmountTextbox.Text);
            //_glJournalDt.DebitForex = Convert.ToDecimal(this.DebitForexTextBox.Text);
            //_glJournalDt.DebitHome = Convert.ToDecimal(this.DebitHomeTextBox.Text);
            //_glJournalDt.CreditForex = Convert.ToDecimal(this.CreditForexTextBox.Text);
            //_glJournalDt.CreditHome = Convert.ToDecimal(this.CreditHomeTextBox.Text);

            bool _result = this._pettyBL.AddCashDt(_finPettyDt);

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
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_codeTransaction, ApplicationConfig.EncryptionKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }

}