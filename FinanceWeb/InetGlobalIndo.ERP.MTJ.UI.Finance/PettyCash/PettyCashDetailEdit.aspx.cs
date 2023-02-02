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
    public partial class PettyCashDetailEdit : PettyCashBase
    {
        private PettyBL _pettyBL = new PettyBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private AccountBL _accountBL = new AccountBL();
        private SubledBL _subledBL = new SubledBL();
        private User_EmployeeBL _userEmpBL = new User_EmployeeBL();
        private EmployeeBL _empBL = new EmployeeBL();
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

                this.ClearLabel();
                this.SetAttribute();
                this.ShowAccount();
                this.ShowDataEdit();
            }
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.AmountTextbox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.AmountTextbox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            //this.AmountTextbox.Attributes.Add("OnBlur", "AmountForex_OnBlur(" + this.AmountTextbox.ClientID + ");");
        }

        private void SetAttribute()
        {
            this.AccountTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.AmountTextbox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
            this.SetAttributeRate();
        }

        public void ShowDataEdit()
        {
            FINPettyDt _finPettyDt = this._pettyBL.GetSingleDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._numberKey), ApplicationConfig.EncryptionKey));
            FINPettyHd _finPettyHd = this._pettyBL.GetSingleHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            string _curren = this._accountBL.GetSingleAccount(_finPettyDt.Account).CurrCode;
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_curren);

            this.SubLedgerDropDownList.Items.Clear();
            this.SubLedgerDropDownList.DataTextField = "SubLed_Name";
            this.SubLedgerDropDownList.DataValueField = "SubLed_No";
            this.SubLedgerDropDownList.DataSource = this._subledBL.GetListSubled(Convert.ToChar(_accountBL.GetAccountSubLed(_finPettyDt.Account)));
            this.SubLedgerDropDownList.DataBind();
            this.SubLedgerDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

            this.AccountTextBox.Text = _finPettyDt.Account;
            this.AccountDropDownList.SelectedValue = _finPettyDt.Account;
            this.FgSubledHiddenField.Value = _finPettyDt.FgSubLed.ToString();
            this.SubLedgerDropDownList.SelectedValue = _finPettyDt.SubLed;
            this.RemarkTextBox.Text = _finPettyDt.Remark;
            this.AmountTextbox.Text = (_finPettyDt.AmountForex == 0) ? "0" : _finPettyDt.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

        }

        public void ClearData()
        {
            this.SubLedgerDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.AccountDropDownList.SelectedValue = "null";
            this.SubLedgerDropDownList.SelectedValue = "null";
            this.RemarkTextBox.Text = "";
            this.AccountTextBox.Text = "";
            this.AmountTextbox.Text = "0";
        }

        public void ShowAccount()
        {
            string _curr = this._pettyBL.GetCurrHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
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

            this.SubLedgerDropDownList.Items.Clear();
            this.SubLedgerDropDownList.DataTextField = "SubLed_Name";
            this.SubLedgerDropDownList.DataValueField = "SubLed_No";
            this.SubLedgerDropDownList.DataSource = this._subledBL.GetListSubled(Convert.ToChar(this.FgSubledHiddenField.Value));
            this.SubLedgerDropDownList.DataBind();
            this.SubLedgerDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }


        protected void AccountDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.AccountTextBox.Text = this.AccountDropDownList.SelectedValue;

            this.GetSubled();
        }

        protected void AccountTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.AccountDropDownList.SelectedValue = this.AccountTextBox.Text;

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
            //string _transNumber = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            //int _maxItemNo = this._pettyBL.GetMaxNoItem(_transNumber);

            FINPettyDt _finPettyDt = this._pettyBL.GetSingleDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._numberKey), ApplicationConfig.EncryptionKey));

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
            //_finPettyDt.CurrCode = this.CurrencyDropDownList.SelectedValue;
            _finPettyDt.AmountForex = Convert.ToDecimal(this.AmountTextbox.Text);

            bool _result = this._pettyBL.EditCashDt(_finPettyDt);

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
            this.ShowDataEdit();
        }

        private void SetCurrRate()
        {
            string _curren;
            _curren = this._accountBL.GetSingleAccount(this.AccountTextBox.Text.Trim()).CurrCode;
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_curren);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

        }
    }
}