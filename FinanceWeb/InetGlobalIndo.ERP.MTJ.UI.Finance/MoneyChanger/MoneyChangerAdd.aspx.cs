using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
    public partial class MoneyChangerAdd : MoneyChangerBase
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

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ClearData();
                this.SetAttribute();
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

            //this.AmountExchangeTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.AmountExchangeTextBox.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");
            //this.CurrRateExchangeTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateExchangeTextBox.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");

            //this.AmountTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            //this.RateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.RateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.RateTextBox.Attributes.Add("OnBlur", "return Count(" + this.RateTextBox.ClientID + "," + this.CurrRateExchangeTextBox.ClientID + "," + this.AmountExchangeTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");
            this.CurrRateExchangeTextBox.Attributes.Add("OnBlur", "return Count(" + this.RateTextBox.ClientID + "," + this.CurrRateExchangeTextBox.ClientID + "," + this.AmountExchangeTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");
            this.AmountTextBox.Attributes.Add("OnBlur", "return Count(" + this.RateTextBox.ClientID + "," + this.CurrRateExchangeTextBox.ClientID + "," + this.AmountExchangeTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");
            //this.AmountExchangeTextBox.Attributes.Add("OnBlur", "return Count(" + this.RateTextBox.ClientID + "," + this.CurrRateExchangeTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.AmountExchangeTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");
              
        }

        public void ShowPetty()
        {
            this.PettyDDL.Items.Clear();
            this.PettyDDL.DataTextField = "PettyName";
            this.PettyDDL.DataValueField = "PettyCode";
            this.PettyDDL.DataSource = this._pettyBL.GetList();
            this.PettyDDL.DataBind();
            this.PettyDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowPayment()
        {
            this.PaymentDropDownList.Items.Clear();
            this.PaymentDropDownList.DataTextField = "PayName";
            this.PaymentDropDownList.DataValueField = "PayCode";
            this.PaymentDropDownList.DataSource = this._paymentBL.GetListDDLPayment();
            this.PaymentDropDownList.DataBind();
            this.PaymentDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowPettyExchange()
        {
            this.PettyExchangeDDL.Items.Clear();
            this.PettyExchangeDDL.DataTextField = "PettyName";
            this.PettyExchangeDDL.DataValueField = "PettyCode";
            this.PettyExchangeDDL.DataSource = this._pettyBL.GetListPettyWithoutCurrCode(this.CurrTextBox.Text);
            this.PettyExchangeDDL.DataBind();
            this.PettyExchangeDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowPaymentExchange()
        {
            this.PaymentExchangeDropDownList.Items.Clear();
            this.PaymentExchangeDropDownList.DataTextField = "PayName";
            this.PaymentExchangeDropDownList.DataValueField = "PayCode";
            this.PaymentExchangeDropDownList.DataSource = this._paymentBL.GetListDDLPaymentWithoutCurrCode(this.CurrTextBox.Text);
            this.PaymentExchangeDropDownList.DataBind();
            this.PaymentExchangeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearData()
        {
            this.petty_tr.Visible = false;
            this.payment_tr.Visible = false;
            this.petty_exchange_tr.Visible = false;
            this.payment_exchange_tr.Visible = false;

            this.ClearLabel();
            this.DateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.PettyDDL.SelectedValue = "null";
            this.CurrTextBox.Text = "";
            this.CurrRateExchangeTextBox.Text = "0";
            this.RateTextBox.Text = "0";
            this.PettyExchangeDDL.SelectedValue = "null";
            this.CurrExchangeTextBox.Text = "";
            this.RemarkTextBox.Text = "";
            this.AmountExchangeTextBox.Text = "0";
            this.AmountTextBox.Text = "0";
            this.TypeDDL.SelectedValue = "null";
            this.TypeExchangeDDL.SelectedValue = "null";
            this.EnableRate();
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

        protected void PettyDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.PettyDDL.SelectedValue != "null")
            {
                string _currCodeHome = _currencyBL.GetCurrDefault();

                MsPetty _pet = _pettyBL.GetSingle(this.PettyDDL.SelectedValue);
                MsAccount _acc = _accountBL.GetSingleAccount(_pet.Account);

                this.CurrTextBox.Text = _acc.CurrCode;

                byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrTextBox.Text);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                this.SetCurrRate();
            }
            else
            {
                this.CurrTextBox.Text = "";
                this.RateTextBox.Text = "0";
                this.RateTextBox.Attributes.Remove("ReadOnly");
                this.RateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
            }
        }

        protected void PaymentDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.PaymentDropDownList.SelectedValue != "null")
            {
                string _currCodeHome = _currencyBL.GetCurrDefault();

                MsPayType _msPayType = _paymentBL.GetSinglePaymentType(this.PaymentDropDownList.SelectedValue);
                MsAccount _msAccount = _accountBL.GetSingleAccount(_msPayType.Account);

                this.CurrTextBox.Text = _msAccount.CurrCode;

                byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrTextBox.Text);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                this.SetCurrRate();
            }
            else
            {
                this.CurrTextBox.Text = "";
                this.RateTextBox.Text = "0";
                this.RateTextBox.Attributes.Remove("ReadOnly");
                this.RateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
            }
        }

        protected void PettyExchangeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.PettyExchangeDDL.SelectedValue != "null")
            {
                string _currCodeHome = _currencyBL.GetCurrDefault();

                MsPetty _pet = _pettyBL.GetSingle(this.PettyExchangeDDL.SelectedValue);
                MsAccount _acc = _accountBL.GetSingleAccount(_pet.Account);

                this.CurrExchangeTextBox.Text = _acc.CurrCode;

                this.SetCurrRateExchange();
            }
            else
            {
                this.CurrExchangeTextBox.Text = "";
                this.CurrRateExchangeTextBox.Text = "0";
                this.CurrRateExchangeTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateExchangeTextBox.Attributes.Add("style", "background-color:#FFFFFF");
            }
        }

        protected void PaymentExchangeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.PaymentExchangeDropDownList.SelectedValue != "null")
            {
                string _currCodeHome = _currencyBL.GetCurrDefault();

                MsPayType _msPayType = _paymentBL.GetSinglePaymentType(this.PaymentExchangeDropDownList.SelectedValue);
                MsAccount _msAccount = _accountBL.GetSingleAccount(_msPayType.Account);

                this.CurrExchangeTextBox.Text = _msAccount.CurrCode;
               
                this.SetCurrRateExchange();
            }
            else
            {
                this.CurrExchangeTextBox.Text = "";
                this.CurrRateExchangeTextBox.Text = "0";
                this.CurrRateExchangeTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateExchangeTextBox.Attributes.Add("style", "background-color:#FFFFFF");
            }
        }

        private void SetCurrRateExchange()
        {
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrExchangeTextBox.Text);
            this.CurrRateExchangeTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrExchangeTextBox.Text).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DecimalPlaceHiddenField2.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            if (this.CurrExchangeTextBox.Text.Trim().ToLower() == _currencyBL.GetCurrDefault().Trim().ToLower())
            {
                this.DisableRateExchange();
            }
            else
            {
                this.EnableRateExchange();
            }
        }

        private void SetCurrRate()
        {
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrTextBox.Text);
            this.RateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrTextBox.Text).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            if (this.CurrTextBox.Text.Trim().ToLower() == _currencyBL.GetCurrDefault().Trim().ToLower())
            {
                this.DisableRate();
            }
            else
            {
                this.EnableRate();
            }
        }

        protected void TypeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.TypeDDL.SelectedValue == PettyCashDataMapper.GetType(PettyCashReceiveType.Petty).ToString())
            {
                this.petty_tr.Visible = true;
                this.payment_tr.Visible = false;
                this.ShowPetty();
            }
            else if (this.TypeDDL.SelectedValue == PettyCashDataMapper.GetType(PettyCashReceiveType.Payment).ToString())
            {
                this.petty_tr.Visible = false;
                this.payment_tr.Visible = true;
                this.ShowPayment();
            }
            else
            {
                this.petty_tr.Visible = false;
                this.payment_tr.Visible = false;

                this.CurrTextBox.Text = "";
                this.RateTextBox.Text = "0";
                this.RateTextBox.Attributes.Remove("ReadOnly");
                this.RateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
            }
        }

        protected void TypeExchangeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.TypeExchangeDDL.SelectedValue == PettyCashDataMapper.GetType(PettyCashReceiveType.Petty).ToString())
            {
                this.petty_exchange_tr.Visible = true;
                this.payment_exchange_tr.Visible = false;
                this.ShowPettyExchange();
            }
            else if (this.TypeExchangeDDL.SelectedValue == PettyCashDataMapper.GetType(PettyCashReceiveType.Payment).ToString())
            {
                this.petty_exchange_tr.Visible = false;
                this.payment_exchange_tr.Visible = true;
                this.ShowPaymentExchange();
            }
            else
            {
                this.petty_exchange_tr.Visible = false;
                this.payment_exchange_tr.Visible = false;

                this.CurrExchangeTextBox.Text = "";
                this.CurrRateExchangeTextBox.Text = "0";
                this.CurrRateExchangeTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateExchangeTextBox.Attributes.Add("style", "background-color:#FFFFFF");
            }
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            Finance_MoneyChangerPetty _finMoneyChangerPetty = null;
            Finance_MoneyChangerPayment _finMoneyChangerPayment = null;
            Finance_MoneyChangerPettyExchange _finMoneyChangerPettyExchange = null;
            Finance_MoneyChangerPaymentExchange _finMoneyChangerPaymentExchange = null;

            Finance_MoneyChanger _finMoneyChanger = new Finance_MoneyChanger();

            _finMoneyChanger.MoneyChangerCode = Guid.NewGuid();
            _finMoneyChanger.Status = MoneyChangerDataMapper.GetStatusByte(TransStatus.OnHold);
            _finMoneyChanger.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finMoneyChanger.CurrCode = this.CurrTextBox.Text;
            _finMoneyChanger.ForexRate = (this.RateTextBox.Text == "") ? 0 : Convert.ToDecimal(this.RateTextBox.Text);
            _finMoneyChanger.CurrExchange = this.CurrExchangeTextBox.Text;
            _finMoneyChanger.ForexRateExchange = (this.CurrRateExchangeTextBox.Text == "") ? 0 : Convert.ToDecimal(this.CurrRateExchangeTextBox.Text);
            _finMoneyChanger.Remark = this.RemarkTextBox.Text;
            _finMoneyChanger.FgType = Convert.ToByte(this.TypeDDL.SelectedValue);
            _finMoneyChanger.FgTypeExchange = Convert.ToByte(this.TypeExchangeDDL.SelectedValue);
            _finMoneyChanger.AmountExchange = (this.AmountExchangeTextBox.Text == "") ? 0 : Convert.ToDecimal(this.AmountExchangeTextBox.Text);
            _finMoneyChanger.Amount = (this.AmountTextBox.Text == "") ? 0 : Convert.ToDecimal(this.AmountTextBox.Text);

            _finMoneyChanger.InsertBy = HttpContext.Current.User.Identity.Name;
            _finMoneyChanger.InsertDate = DateTime.Now;
            _finMoneyChanger.EditBy = HttpContext.Current.User.Identity.Name;
            _finMoneyChanger.EditDate = DateTime.Now;

            if (TypeDDL.SelectedValue == MoneyChangerDataMapper.GetType(MoneyChangerType.Petty).ToString())
            {
                _finMoneyChangerPetty = new Finance_MoneyChangerPetty();
                _finMoneyChangerPetty.PettyCode = this.PettyDDL.SelectedValue;
            }
            else if (TypeDDL.SelectedValue == MoneyChangerDataMapper.GetType(MoneyChangerType.Payment).ToString())
            {
                _finMoneyChangerPayment = new Finance_MoneyChangerPayment();
                _finMoneyChangerPayment.PayCode = this.PaymentDropDownList.SelectedValue;
            }
            if (TypeExchangeDDL.SelectedValue == MoneyChangerDataMapper.GetType(MoneyChangerType.Petty).ToString())
            {
                _finMoneyChangerPettyExchange = new Finance_MoneyChangerPettyExchange();
                _finMoneyChangerPettyExchange.PettyCodeExchange = this.PettyExchangeDDL.SelectedValue;
            }
            else if (TypeExchangeDDL.SelectedValue == MoneyChangerDataMapper.GetType(MoneyChangerType.Payment).ToString())
            {
                _finMoneyChangerPaymentExchange = new Finance_MoneyChangerPaymentExchange();
                _finMoneyChangerPaymentExchange.PayCodeExchange = this.PaymentExchangeDropDownList.SelectedValue;
            }

            string _result = this._moneyChangerBL.AddMoneyChanger(_finMoneyChanger, _finMoneyChangerPetty, _finMoneyChangerPayment, _finMoneyChangerPettyExchange, _finMoneyChangerPaymentExchange);

            if (_result != "")
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}