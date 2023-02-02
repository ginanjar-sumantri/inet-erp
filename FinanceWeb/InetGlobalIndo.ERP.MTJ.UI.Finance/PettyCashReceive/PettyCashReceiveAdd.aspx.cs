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

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PettyCashReceive
{
    public partial class PettyCashReceiveAdd : PettyCashReceiveBase
    {
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

            this.RateTextbox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.RateTextbox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
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

        protected void ClearData()
        {
            this.petty_tr.Visible = false;
            this.payment_tr.Visible = false;

            this.ClearLabel();
            this.DateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.PettyDDL.SelectedValue = "null";
            this.CurrTextBox.Text = "";
            this.RateTextbox.Text = "0";
            this.PayToText.Text = "";
            this.RemarkTextBox.Text = "";
            this.EnableRate();
        }

        private void EnableRate()
        {
            this.RateTextbox.Attributes.Remove("ReadOnly");
            this.RateTextbox.Attributes.Add("style", "background-color:#FFFFFF");
        }

        private void DisableRate()
        {
            this.RateTextbox.Attributes.Add("ReadOnly", "True");
            this.RateTextbox.Attributes.Add("style", "background-color:#CCCCCC");
            this.RateTextbox.Text = "1";
        }

        protected void PettyDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.PettyDDL.SelectedValue != "null")
            {
                string _currCodeHome = _currencyBL.GetCurrDefault();

                MsPetty _pet = _pettyBL.GetSingle(this.PettyDDL.SelectedValue);
                MsAccount _acc = _accountBL.GetSingleAccount(_pet.Account);

                this.CurrTextBox.Text = _acc.CurrCode;

                this.SetCurrRate();
            }
            else
            {
                this.CurrTextBox.Text = "";
                this.RateTextbox.Text = "0";
                this.RateTextbox.Attributes.Remove("ReadOnly");
                this.RateTextbox.Attributes.Add("style", "background-color:#FFFFFF");
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

                this.SetCurrRate();
            }
            else
            {
                this.CurrTextBox.Text = "";
                this.RateTextbox.Text = "0";
                this.RateTextbox.Attributes.Remove("ReadOnly");
                this.RateTextbox.Attributes.Add("style", "background-color:#FFFFFF");
            }
        }

        private void SetCurrRate()
        {
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrTextBox.Text);
            this.RateTextbox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrTextBox.Text).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
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
                this.RateTextbox.Text = "0";
                this.RateTextbox.Attributes.Remove("ReadOnly");
                this.RateTextbox.Attributes.Add("style", "background-color:#FFFFFF");
            }
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            FINPettyReceiveHdPetty _finPettyReceiveHdPetty = null;
            FINPettyReceiveHdPayType _finPettyReceiveHdPayType = null;

            FINPettyReceiveHd _finPettyReceiveHd = new FINPettyReceiveHd();
            _finPettyReceiveHd.Status = PettyCashDataMapper.GetStatus(TransStatus.OnHold);
            _finPettyReceiveHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finPettyReceiveHd.CurrCode = this.CurrTextBox.Text;
            _finPettyReceiveHd.ForexRate = Convert.ToDecimal(this.RateTextbox.Text);
            _finPettyReceiveHd.PayTo = this.PayToText.Text;
            _finPettyReceiveHd.Remark = this.RemarkTextBox.Text;
            _finPettyReceiveHd.FgType = Convert.ToByte(this.TypeDDL.SelectedValue);

            _finPettyReceiveHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finPettyReceiveHd.DatePrep = DateTime.Now;

            if (TypeDDL.SelectedValue == PettyCashDataMapper.GetType(PettyCashReceiveType.Petty).ToString())
            {
                _finPettyReceiveHdPetty = new FINPettyReceiveHdPetty();
                _finPettyReceiveHdPetty.PettyCode = this.PettyDDL.SelectedValue;
            }
            else if (TypeDDL.SelectedValue == PettyCashDataMapper.GetType(PettyCashReceiveType.Payment).ToString())
            {
                _finPettyReceiveHdPayType = new FINPettyReceiveHdPayType();
                _finPettyReceiveHdPayType.PayCode = this.PaymentDropDownList.SelectedValue;
            }

            string _result = this._pettyBL.AddFINPettyReceiveHd(_finPettyReceiveHd, _finPettyReceiveHdPetty, _finPettyReceiveHdPayType);

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