using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.MoneyChanger
{
    public partial class MoneyChangerEdit : MoneyChangerBase
    {
        private MoneyChangerBL _moneyChangerBL = new MoneyChangerBL();
        private AccountBL _accountBL = new AccountBL();
        private PettyBL _pettyBL = new PettyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private PermissionBL _permBL = new PermissionBL();
        private PaymentBL _paymentBL = new PaymentBL();

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
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrExchangeTextBox.Attributes.Add("ReadOnly", "True");

            this.AmountExchangeTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.AmountExchangeTextBox.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");
            this.CurrRateExchangeTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateExchangeTextBox.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");

            this.AmountTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.RateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.RateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void EnableRateExchange()
        {
            this.CurrRateExchangeTextBox.Attributes.Remove("ReadOnly");
            this.CurrRateExchangeTextBox.Attributes.Add("style", "background-color:#FFFFFF");
        }

        private void DisableRateExchange()
        {
            this.CurrRateExchangeTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateExchangeTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.CurrRateExchangeTextBox.Text = "1";
        }

        private void EnableRate()
        {
            this.RateTextBox.Attributes.Remove("ReadOnly");
            this.RateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
        }

        private void DisableRate()
        {
            this.RateTextBox.Attributes.Add("ReadOnly", "True");
            this.RateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.RateTextBox.Text = "1";
        }

        protected void ShowData()
        {
            Finance_MoneyChanger _finMoneyChanger = this._moneyChangerBL.GetSingleMoneyChanger(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            this.TransNoTextBox.Text = _finMoneyChanger.TransNmbr;
            this.FileNoTextBox.Text = _finMoneyChanger.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_finMoneyChanger.TransDate);
            this.TypeTextBox.Text = MoneyChangerDataMapper.GetTypeText(MoneyChangerDataMapper.GetType(_finMoneyChanger.FgType));
            this.TypeExchangeTextBox.Text = MoneyChangerDataMapper.GetTypeText(MoneyChangerDataMapper.GetType(_finMoneyChanger.FgTypeExchange));

            if (_finMoneyChanger.FgType == MoneyChangerDataMapper.GetType(MoneyChangerType.Petty))
            {
                this.petty_tr.Visible = true;
                this.payment_tr.Visible = false;
                this.PettyTextBox.Text = _pettyBL.GetPettyNameByCode(this._moneyChangerBL.GetPettyCodeMoneyChanger(_finMoneyChanger.MoneyChangerCode));
            }
            else if (_finMoneyChanger.FgType == MoneyChangerDataMapper.GetType(MoneyChangerType.Payment))
            {
                this.petty_tr.Visible = false;
                this.payment_tr.Visible = true;
                this.PaymentTextBox.Text = _paymentBL.GetPaymentName(this._moneyChangerBL.GetPayCodeMoneyChanger(_finMoneyChanger.MoneyChangerCode));
            }

            if (_finMoneyChanger.FgTypeExchange == MoneyChangerDataMapper.GetType(MoneyChangerType.Petty))
            {
                this.petty_exchange_tr.Visible = true;
                this.payment_exchange_tr.Visible = false;
                this.PettyExchangeTextBox.Text = _pettyBL.GetPettyNameByCode(this._moneyChangerBL.GetPettyCodeExchangeMoneyChanger(_finMoneyChanger.MoneyChangerCode));
            }
            else if (_finMoneyChanger.FgTypeExchange == MoneyChangerDataMapper.GetType(MoneyChangerType.Payment))
            {
                this.petty_exchange_tr.Visible = false;
                this.payment_exchange_tr.Visible = true;
                this.PaymentExchangeTextBox.Text = _paymentBL.GetPaymentName(this._moneyChangerBL.GetPayCodeExchangeMoneyChanger(_finMoneyChanger.MoneyChangerCode));
            }

            this.CurrTextBox.Text = _finMoneyChanger.CurrCode;
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrTextBox.Text);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            if (_finMoneyChanger.CurrCode == _currencyBL.GetCurrDefault())
            {
                this.DisableRate();
            }
            else
            {
                this.EnableRate();
                this.RateTextBox.Text = _finMoneyChanger.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }

            this.CurrExchangeTextBox.Text = _finMoneyChanger.CurrExchange;
            byte _decimalPlaceExchange = this._currencyBL.GetDecimalPlace(this.CurrExchangeTextBox.Text);
            this.DecimalPlaceHiddenField2.Value = CurrencyDataMapper.GetDecimal(_decimalPlaceExchange);

            if (_finMoneyChanger.CurrExchange == _currencyBL.GetCurrDefault())
            {
                this.DisableRateExchange();
            }
            else
            {
                this.EnableRateExchange();
                this.CurrRateExchangeTextBox.Text = _finMoneyChanger.ForexRateExchange.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceExchange));
            }

            this.RemarkTextBox.Text = _finMoneyChanger.Remark;
            this.AmountExchangeTextBox.Text = _finMoneyChanger.AmountExchange.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceExchange));
            this.AmountTextBox.Text = _finMoneyChanger.Amount.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            Finance_MoneyChanger _finMoneyChanger = this._moneyChangerBL.GetSingleMoneyChanger(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            _finMoneyChanger.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finMoneyChanger.ForexRate = (this.RateTextBox.Text == "") ? 0 : Convert.ToDecimal(this.RateTextBox.Text);
            _finMoneyChanger.ForexRateExchange = (this.CurrRateExchangeTextBox.Text == "") ? 0 : Convert.ToDecimal(this.CurrRateExchangeTextBox.Text);
            _finMoneyChanger.AmountExchange = (this.AmountExchangeTextBox.Text == "") ? 0 : Convert.ToDecimal(this.AmountExchangeTextBox.Text);
            _finMoneyChanger.Amount = (this.AmountTextBox.Text == "") ? 0 : Convert.ToDecimal(this.AmountTextBox.Text);
            _finMoneyChanger.Remark = this.RemarkTextBox.Text;

            _finMoneyChanger.EditBy = HttpContext.Current.User.Identity.Name;
            _finMoneyChanger.EditDate = DateTime.Now;

            bool _result = this._moneyChangerBL.EditMoneyChanger(_finMoneyChanger);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }
    }
}