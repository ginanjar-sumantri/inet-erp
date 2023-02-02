using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.BudgetEntry
{
    public partial class GLBudgetDetailEdit : GLBudgetBase
    {
        private GLBudgetBL _glBudgetBL = new GLBudgetBL();
        private AccountBL _accountBL = new AccountBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();

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
                this.ShowData();
                this.SetAttribute();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        private void SetAttributeRate()
        {
            this.AmountBudgetRateTextBox.Attributes.Add("OnBlur", "Count(" + this.AmountBudgetRateTextBox.ClientID + "," + this.AmountBudgetForexTextBox.ClientID + "," + this.AmountBudgetHomeTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHomeHiddenField.ClientID + " );");
            this.AmountBudgetForexTextBox.Attributes.Add("OnBlur", "Count(" + this.AmountBudgetRateTextBox.ClientID + "," + this.AmountBudgetForexTextBox.ClientID + "," + this.AmountBudgetHomeTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHomeHiddenField.ClientID + " );");
        }

        public void SetAttribute()
        {
            this.AmountBudgetForexTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.AmountBudgetRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.AmountActualTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountBudgetHomeTextBox.Attributes.Add("ReadOnly", "True");

            this.SetAttributeRate();
        }

        private void ShowData()
        {
            Guid _budgetCode = new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            Guid _budgetDetailCode = new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeDt), ApplicationConfig.EncryptionKey));

            GLBudgetAcc _glBudgetAcc = this._glBudgetBL.GetSingleGLBudgetAcc(_budgetCode, _budgetDetailCode);

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_accountBL.GetCurrByAccCode(_glBudgetAcc.Account));
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(_currencyBL.GetCurrDefault());
            this.DecimalPlaceHomeHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlaceHome);

            this.AccountTextBox.Text = _glBudgetAcc.Account;
            this.AccountNameTextBox.Text = _accountBL.GetAccountNameByCode(_glBudgetAcc.Account);
            if (_accountBL.GetCurrByAccCode(_glBudgetAcc.Account) == _currencyBL.GetCurrDefault())
            {
                this.AmountBudgetRateTextBox.Attributes.Add("ReadOnly", "True");
                this.AmountBudgetRateTextBox.Attributes.Add("Style", "background-color: #CCCCCC");
            }
            else
            {
                this.AmountBudgetRateTextBox.Attributes.Remove("ReadOnly");
                this.AmountBudgetRateTextBox.Attributes.Add("Style", "background-color: #FFFFFF");
            }
            this.AmountBudgetRateTextBox.Text = (_glBudgetAcc.AmountBudgetRate == 0) ? "0" : _glBudgetAcc.AmountBudgetRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountBudgetForexTextBox.Text = (_glBudgetAcc.AmountBudgetForex == 0) ? "0" : Convert.ToDecimal(_glBudgetAcc.AmountBudgetForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountBudgetHomeTextBox.Text = (_glBudgetAcc.AmountBudgetHome == 0) ? "0" : Convert.ToDecimal(_glBudgetAcc.AmountBudgetHome).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
            this.AmountActualTextBox.Text = (_glBudgetAcc.AmountActual == 0) ? "0" : Convert.ToDecimal(_glBudgetAcc.AmountActual).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            Guid _budgetCode = new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            Guid _budgetDetailCode = new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeDt), ApplicationConfig.EncryptionKey));

            GLBudgetAcc _glBudgetAcc = this._glBudgetBL.GetSingleGLBudgetAcc(_budgetCode, _budgetDetailCode);

            _glBudgetAcc.AmountBudgetRate = Convert.ToDecimal(this.AmountBudgetRateTextBox.Text);
            _glBudgetAcc.AmountBudgetForex = Convert.ToDecimal(this.AmountBudgetForexTextBox.Text);
            _glBudgetAcc.AmountBudgetHome = Convert.ToDecimal(this.AmountBudgetHomeTextBox.Text);
            _glBudgetAcc.AmountActual = Convert.ToDecimal(this.AmountActualTextBox.Text);
            _glBudgetAcc.EditBy = HttpContext.Current.User.Identity.Name;
            _glBudgetAcc.EditDate = DateTime.Now;

            bool _result = this._glBudgetBL.EditGLBudgetAcc(_glBudgetAcc);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}