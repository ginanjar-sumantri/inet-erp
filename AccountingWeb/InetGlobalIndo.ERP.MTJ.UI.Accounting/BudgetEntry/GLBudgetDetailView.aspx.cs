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
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.BudgetEntry
{
    public partial class GLBudgetDetailView : GLBudgetBase
    {
        private GLPeriodBL _periodBL = new GLPeriodBL();
        private GLYearBL _yearBL = new GLYearBL();
        private GLBudgetBL _glBudgetBL = new GLBudgetBL();
        private AccountBL _accountBL = new AccountBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();

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

                if (_glBudgetBL.GetSingleHd(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey))).Status.ToString().ToLower() == GLBudgetDataMapper.GetStatus(GLBudgetStatus.Approved).ToString().ToLower())
                {
                    this.EditButton.Visible = false;
                }
                else
                {
                    this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                }
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.SetButtonPermission();

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

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailEdit + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeDt + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeDt)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        private void ShowData()
        {
            Guid _budgetCode = new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            Guid _budgetDetailCode = new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeDt), ApplicationConfig.EncryptionKey));

            GLBudgetAcc _glBudgetAcc = this._glBudgetBL.GetSingleGLBudgetAcc(_budgetCode, _budgetDetailCode);

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_accountBL.GetCurrByAccCode(_glBudgetAcc.Account));
            byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(_currencyBL.GetCurrDefault());

            this.AccountTextBox.Text = _glBudgetAcc.Account;
            this.AccountNameTextBox.Text = _accountBL.GetAccountNameByCode(_glBudgetAcc.Account);
            this.AmountBudgetRateTextBox.Text = (_glBudgetAcc.AmountBudgetRate == 0) ? "0" : _glBudgetAcc.AmountBudgetRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountBudgetForexTextBox.Text = (_glBudgetAcc.AmountBudgetForex == 0) ? "0" : Convert.ToDecimal(_glBudgetAcc.AmountBudgetForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountBudgetHomeTextBox.Text = (_glBudgetAcc.AmountBudgetHome == 0) ? "0" : Convert.ToDecimal(_glBudgetAcc.AmountBudgetHome).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
            this.AmountActualTextBox.Text = (_glBudgetAcc.AmountActual == 0) ? "0" : Convert.ToDecimal(_glBudgetAcc.AmountActual).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
        }
    }
}