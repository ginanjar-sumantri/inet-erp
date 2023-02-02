using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PaymentType
{
    public partial class PaymentTypeView : PaymentTypeBase
    {
        private PaymentBL _paymentBL = new PaymentBL();
        private AccountBL _accountBL = new AccountBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private BankBL _bankBL = new BankBL();
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

                this.ClearLabel();
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

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            MsPayType _msPayType = this._paymentBL.GetSinglePaymentType(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlaceBankCharge = _currencyBL.GetDecimalPlace(_accountBL.GetCurrByAccCode(_msPayType.AccBankCharge));
            byte _decimalPlaceCustCharge = _currencyBL.GetDecimalPlace(_accountBL.GetCurrByAccCode(_msPayType.AccCustCharge));

            this.PaymentCodeTextBox.Text = _msPayType.PayCode;
            this.PaymentNameTextBox.Text = _msPayType.PayName;
            this.AccountTextBox.Text = _msPayType.Account;
            this.AccountNameTextBox.Text = _accountBL.GetAccountNameByCode(_msPayType.Account);
            this.ModeDropDownList.SelectedValue = _msPayType.FgMode.ToString();
            this.TypeDropDownList.SelectedValue = _msPayType.FgType.ToString();
            if (_msPayType.Bank != "" || _msPayType.Bank != null)
            {
                this.BankTextBox.Text = _bankBL.GetBankNameByCode(_msPayType.Bank);
            }
            this.NoRekeningTextBox.Text = _msPayType.NoRekening;
            this.NoRekeningOwnerTextBox.Text = _msPayType.NoRekeningOwner;
            this.AccBankChargeTextBox.Text = _msPayType.AccBankCharge;
            this.AccCustChargeTextBox.Text = _msPayType.AccCustCharge;
            if (_msPayType.AccBankCharge != "" || _msPayType.AccBankCharge != null)
            {
                this.AccBankChargeNameTextBox.Text = _accountBL.GetAccountNameByCode(_msPayType.AccBankCharge);
            }
            if (_msPayType.AccCustCharge != "" || _msPayType.AccCustCharge != null)
            {
                this.AccCustChargeNameTextBox.Text = _accountBL.GetAccountNameByCode(_msPayType.AccCustCharge);
            }
            this.ExpenseGiroTextBox.Text = _msPayType.ExpenseGiro.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceBankCharge));
            this.CustChargeRevenueTextBox.Text = _msPayType.CustRevenue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceCustCharge));
            this.FgBankChargeRadioButtonList.SelectedValue = PaymentDataMapper.IsCheckedBool(_msPayType.FgBankCharge);
            this.CustChargeRevenueRadioButtonList.SelectedValue = PaymentDataMapper.IsCheckedBool(_msPayType.FgCustCharge);
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }
    }
}