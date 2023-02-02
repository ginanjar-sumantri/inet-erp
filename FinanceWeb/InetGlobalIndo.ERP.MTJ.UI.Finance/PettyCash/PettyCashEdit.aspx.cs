using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PettyCash
{
    public partial class PettyCashEdit : PettyCashBase
    {
        private AccountBL _accountBL = new AccountBL();
        private PettyBL _pettyBL = new PettyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
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
                this.TransDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.TransDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.ShowPetty();

                this.SetAttribute();
                this.ClearLabel();
                this.ShowData();
            }
        }

        private void SetAttributeRate()
        {
            this.RateTextbox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.RateTextbox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribute()
        {
            this.TransDateTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.SetAttributeRate();
        }

        public void ShowData()
        {
            FINPettyHd _finPettyHd = this._pettyBL.GetSingleHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finPettyHd.CurrCode);

            this.TransactionNumberTextBox.Text = _finPettyHd.TransNmbr;
            this.TransDateTextBox.Text = DateFormMapper.GetValue(_finPettyHd.TransDate);
            this.FileNmbrTextBox.Text = _finPettyHd.FileNmbr;
            this.PettyDDL.SelectedValue = _finPettyHd.Petty;
            this.CurrTextBox.Text = _finPettyHd.CurrCode;
            string _currHome = _currencyBL.GetCurrDefault();
            if (_currHome == this.CurrTextBox.Text)
            {
                //this.RateTextbox.Attributes.Add("ReadOnly", "True");
                //this.RateTextbox.Attributes.Add("Style", "background-color:#CCCCCC");
                this.DisableRate();
            }
            else
            {
                //this.RateTextbox.Attributes.Remove("ReadOnly");
                //this.RateTextbox.Attributes.Add("Style", "background-color:#FFFFFF");
                this.EnableRate();
            }
            this.RateTextbox.Text = _finPettyHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PayToText.Text = _finPettyHd.PayTo;
            this.RemarkTextBox.Text = _finPettyHd.Remark;

            this.SetAttributeRate();
        }

        public void ShowPetty()
        {
            this.PettyDDL.DataTextField = "PettyName";
            this.PettyDDL.DataValueField = "PettyCode";
            this.PettyDDL.DataSource = this._pettyBL.GetList();
            this.PettyDDL.DataBind();
            this.PettyDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINPettyHd _finPettyHd = this._pettyBL.GetSingleHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finPettyHd.TransDate = new DateTime(DateFormMapper.GetValue(this.TransDateTextBox.Text).Year, DateFormMapper.GetValue(this.TransDateTextBox.Text).Month, DateFormMapper.GetValue(this.TransDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finPettyHd.Petty = this.PettyDDL.SelectedValue;
            _finPettyHd.CurrCode = this.CurrTextBox.Text;
            _finPettyHd.ForexRate = Convert.ToDecimal(this.RateTextbox.Text);
            _finPettyHd.PayTo = this.PayToText.Text;
            _finPettyHd.Remark = this.RemarkTextBox.Text;

            //_finPettyHd.UserPrep = HttpContext.Current.User.Identity.Name;
            //_finPettyHd.DatePrep = DateTime.Now;

            bool _result = this._pettyBL.EditCashHd(_finPettyHd);

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
            FINPettyHd _finPettyHd = this._pettyBL.GetSingleHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finPettyHd.TransDate = new DateTime(DateFormMapper.GetValue(this.TransDateTextBox.Text).Year, DateFormMapper.GetValue(this.TransDateTextBox.Text).Month, DateFormMapper.GetValue(this.TransDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finPettyHd.Petty = this.PettyDDL.SelectedValue;
            _finPettyHd.CurrCode = this.CurrTextBox.Text;
            _finPettyHd.ForexRate = Convert.ToDecimal(this.RateTextbox.Text);
            _finPettyHd.PayTo = this.PayToText.Text;
            _finPettyHd.Remark = this.RemarkTextBox.Text;

            //_finPettyHd.UserPrep = HttpContext.Current.User.Identity.Name;
            //_finPettyHd.DatePrep = DateTime.Now;

            bool _result = this._pettyBL.EditCashHd(_finPettyHd);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void PettyDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _currCodeHome = _currencyBL.GetCurrDefault();
            MsPetty _pet = _pettyBL.GetSingle(this.PettyDDL.SelectedValue);
            MsAccount _acc = _accountBL.GetSingleAccount(_pet.Account);
            CurrTextBox.Text = _acc.CurrCode;
            //if (this.CurrTextBox.Text == _currCodeHome)
            //{
            //    this.RateTextbox.Attributes.Add("ReadOnly", "True");
            //    this.RateTextbox.Attributes.Add("style", "background-color:#cccccc");
            //    this.RateTextbox.Text = "1";
            //}
            //else
            //{
            //    this.RateTextbox.Attributes.Remove("ReadOnly");
            //    this.RateTextbox.Attributes.Add("style", "background-color:#ffffff");
            //    this.RateTextbox.Text = _currencyRateBL.GetSingleLatestCurrRate(this.CurrTextBox.Text).ToString("###,###.##");
            //}

            this.SetCurrRate();
        }

        private void DisableRate()
        {
            this.RateTextbox.Attributes.Add("ReadOnly", "True");
            this.RateTextbox.Attributes.Add("style", "background-color:#CCCCCC");
            this.RateTextbox.Text = "1";
            this.RateTextbox.Attributes.Add("ReadOnly", "True");
            this.RateTextbox.Attributes.Add("style", "background-color:#CCCCCC");
            this.RateTextbox.Text = "1";
        }

        private void EnableRate()
        {
            this.RateTextbox.Attributes.Remove("ReadOnly");
            this.RateTextbox.Attributes.Add("style", "background-color:#FFFFFF");
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
    }
}