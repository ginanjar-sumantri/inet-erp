using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.CustomControl;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.MoneyChanger
{
    public partial class MoneyChangerView : MoneyChangerBase
    {
        private MoneyChangerBL _moneyChangerBL = new MoneyChangerBL();
        private PettyBL _pettyBL = new PettyBL();
        private ReportFinanceBL _reportFinanceBL = new ReportFinanceBL();
        private PermissionBL _permBL = new PermissionBL();
        private PaymentBL _paymentBL = new PaymentBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private UnpostingActivitiesBL _unpostingActivitiesBL = new UnpostingActivitiesBL();

        private string _imgGetApproval = "get_approval.jpg";
        private string _imgApprove = "approve.jpg";
        private string _imgPosting = "posting.jpg";
        private string _imgUnposting = "unposting.jpg";

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

                this.ClearLabel();
                this.ShowData();

                this.SetButtonPermission();
            }
        }

        public void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        public void ShowActionButton()
        {
            if (this.StatusHiddenField.Value.Trim().ToLower() == MoneyChangerDataMapper.GetStatusByte(TransStatus.OnHold).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgGetApproval;

                this._permGetApproval = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval);

                if (this._permGetApproval == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == MoneyChangerDataMapper.GetStatusByte(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgApprove;

                this._permApprove = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Approve);

                if (this._permApprove == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == MoneyChangerDataMapper.GetStatusByte(TransStatus.Approved).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;

                this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                if (this._permPosting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == MoneyChangerDataMapper.GetStatusByte(TransStatus.Posted).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgUnposting;

                this._permUnposting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Unposting);

                if (this._permUnposting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
        }

        protected void ShowData()
        {
            _moneyChangerBL = new MoneyChangerBL();
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

            this.RateTextBox.Text = _finMoneyChanger.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            this.CurrExchangeTextBox.Text = _finMoneyChanger.CurrExchange;
            byte _decimalPlaceExchange = this._currencyBL.GetDecimalPlace(this.CurrExchangeTextBox.Text);
            this.DecimalPlaceHiddenField2.Value = CurrencyDataMapper.GetDecimal(_decimalPlaceExchange);

            this.CurrRateExchangeTextBox.Text = _finMoneyChanger.ForexRateExchange.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceExchange));

            this.RemarkTextBox.Text = _finMoneyChanger.Remark;
            this.AmountExchangeTextBox.Text = _finMoneyChanger.AmountExchange.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceExchange));
            this.AmountTextBox.Text = _finMoneyChanger.Amount.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            this.StatusLabel.Text = MoneyChangerDataMapper.GetStatusText(_finMoneyChanger.Status);
            this.StatusHiddenField.Value = _finMoneyChanger.Status.ToString();

            if (_finMoneyChanger.Status != MoneyChangerDataMapper.GetStatusByte(TransStatus.Posted))
            {
                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.EditButton.Visible = true;
            }
            else
            {
                this.EditButton.Visible = false;
            }
            this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";

            this.ShowActionButton();
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ActionButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();

            if (this.StatusHiddenField.Value.Trim().ToLower() == MoneyChangerDataMapper.GetStatusByte(TransStatus.OnHold).ToString().ToLower())
            {
                string _result = this._moneyChangerBL.GetApproval(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), HttpContext.Current.User.Identity.Name);

                if (_result == "")
                {
                    this.WarningLabel.Text = "Get Approval Success";
                }
                else
                {
                    this.WarningLabel.Text = _result;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == MoneyChangerDataMapper.GetStatusByte(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                string _result = this._moneyChangerBL.Approve(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == MoneyChangerDataMapper.GetStatusByte(TransStatus.Approved).ToString().ToLower())
            {
                string _result = this._moneyChangerBL.Posting(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == MoneyChangerDataMapper.GetStatusByte(TransStatus.Posted).ToString().ToLower())
            {
                this.ReasonPanel.Visible = true;
                //string _result = this._moneyChangerBL.UnPosting(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), HttpContext.Current.User.Identity.Name);

                //this.WarningLabel.Text = _result;
            }
            this.ShowData();
        }

        protected void YesButton_OnClick(object sender, EventArgs e)
        {
            DateTime _transdate = DateFormMapper.GetValue(this.DateTextBox.Text);
            int _year = _transdate.Year;
            int _period = _transdate.Month;

            string _result = this._moneyChangerBL.UnPosting(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), HttpContext.Current.User.Identity.Name);

            //this.Label.Text = _result;

            if (_result == "Unposting Success")
            {
                bool _result1 = _unpostingActivitiesBL.InsertUnpostingActivities(AppModule.GetValue(TransactionType.MoneyChanger), this.TransNoTextBox.Text, this.FileNoTextBox.Text, HttpContext.Current.User.Identity.Name, this.ReasonTextBox.Text, Convert.ToByte(ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting)));
                if (_result1 == true)
                this.WarningLabel.Text = _result;
                this.ReasonTextBox.Text = "";
                this.ReasonPanel.Visible = false;
            }
            else
            {
                this.ReasonTextBox.Text = "";
                this.ReasonPanel.Visible = false;
                this.WarningLabel.Text = _result;
            }

            this.ShowData();
        }

        protected void NoButton_OnClick(object sender, EventArgs e)
        {
            this.ReasonPanel.Visible = false;
        }
    }
}