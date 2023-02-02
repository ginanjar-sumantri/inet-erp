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

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PettyCash
{
    public partial class PettyCashAdd : PettyCashBase
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

                this.ShowPetty();
                this.SetAttribute();
                this.ClearData();

            }
        }

        private void SetAttributeRate()
        {
            this.RateTextbox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.RateTextbox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        private void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            SetAttributeRate();
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowPetty()
        {
            this.PettyDDL.DataTextField = "PettyName";
            this.PettyDDL.DataValueField = "PettyCode";
            this.PettyDDL.DataSource = this._pettyBL.GetList();
            this.PettyDDL.DataBind();
            this.PettyDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ClearLabel();
            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            this.PettyDDL.SelectedValue = "null";
            this.PayToText.Text = "";
            this.RateTextbox.Text = "";
            this.CurrTextBox.Text = "";
            this.RemarkTextBox.Text = "";

            this.EnableRate();
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            FINPettyHd _finPettyHd = new FINPettyHd();
            _finPettyHd.Status =  PettyCashDataMapper.GetStatus(TransStatus.OnHold);
            _finPettyHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finPettyHd.Petty = this.PettyDDL.SelectedValue;
            _finPettyHd.CurrCode = this.CurrTextBox.Text;
            _finPettyHd.ForexRate = Convert.ToDecimal(this.RateTextbox.Text);
            _finPettyHd.PayTo = this.PayToText.Text;
            _finPettyHd.Remark = this.RemarkTextBox.Text;

            _finPettyHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finPettyHd.DatePrep = DateTime.Now;

            string _result = this._pettyBL.AddFINPettyCashHd(_finPettyHd);

            if (_result != "")
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_finPettyHd.TransNmbr, ApplicationConfig.EncryptionKey)));
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