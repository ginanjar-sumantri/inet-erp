using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
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
    public partial class PettyCashReceiveEdit : PettyCashReceiveBase
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
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

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

        protected void ShowData()
        {
            FINPettyReceiveHd _finPettyReceiveHd = this._pettyBL.GetSingleFINPettyReceiveHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransactionNumberTextBox.Text = _finPettyReceiveHd.TransNmbr;
            this.FileNmbrTextBox.Text = _finPettyReceiveHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_finPettyReceiveHd.TransDate);
            this.TypeDDL.SelectedValue = _finPettyReceiveHd.FgType.ToString();

            if (_finPettyReceiveHd.FgType == PettyCashDataMapper.GetType(PettyCashReceiveType.Petty))
            {
                this.ShowPetty();
                this.petty_tr.Visible = true;
                this.payment_tr.Visible = false;
                this.PettyDDL.SelectedValue = this._pettyBL.GetPettyCodeFINPettyReceive(_finPettyReceiveHd.TransNmbr);
            }
            else if (_finPettyReceiveHd.FgType == PettyCashDataMapper.GetType(PettyCashReceiveType.Payment))
            {
                this.ShowPayment();
                this.petty_tr.Visible = false;
                this.payment_tr.Visible = true;
                this.PaymentDropDownList.SelectedValue = this._pettyBL.GetPayCodeFINPettyReceive(_finPettyReceiveHd.TransNmbr);
            }

            this.CurrTextBox.Text = _finPettyReceiveHd.CurrCode;
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrTextBox.Text);
            this.RateTextbox.Text = _finPettyReceiveHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.PayToText.Text = _finPettyReceiveHd.PayTo;
            this.RemarkTextBox.Text = _finPettyReceiveHd.Remark;

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
                this.RateTextbox.Attributes.Add("ReadOnly", "True");
                this.RateTextbox.Attributes.Add("style", "background-color:#cccccc");
                this.RateTextbox.Text = "";
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

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            String _pettyCode = "";
            String _payCode = "";
            FINPettyReceiveHd _finPettyReceiveHd = this._pettyBL.GetSingleFINPettyReceiveHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

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
                _pettyCode = this.PettyDDL.SelectedValue;
            }
            else if (TypeDDL.SelectedValue == PettyCashDataMapper.GetType(PettyCashReceiveType.Payment).ToString())
            {
                _payCode = this.PaymentDropDownList.SelectedValue;
            }

            bool _result = this._pettyBL.EditFINPettyReceiveHd(_finPettyReceiveHd, _pettyCode, _payCode);

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

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            String _pettyCode = "";
            String _payCode = "";
            FINPettyReceiveHd _finPettyReceiveHd = this._pettyBL.GetSingleFINPettyReceiveHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

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
                _pettyCode = this.PettyDDL.SelectedValue;
            }
            else if (TypeDDL.SelectedValue == PettyCashDataMapper.GetType(PettyCashReceiveType.Payment).ToString())
            {
                _payCode = this.PaymentDropDownList.SelectedValue;
            }

            bool _result = this._pettyBL.EditFINPettyReceiveHd(_finPettyReceiveHd, _pettyCode, _payCode);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }
    }
}