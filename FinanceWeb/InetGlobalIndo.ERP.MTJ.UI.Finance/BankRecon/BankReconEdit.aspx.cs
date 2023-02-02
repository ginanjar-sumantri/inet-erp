using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.BankRecon
{
    public partial class BankReconEdit : BankReconBase
    {
        private BankReconBL _bankReconBL = new BankReconBL();
        private PaymentBL _payTypeBL = new PaymentBL();
        private AccountBL _accountBL = new AccountBL();
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
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.ShowPayType();
                this.SetAttribute();
                this.ClearLabel();
                this.ShowData();
            }
        }

        private void ShowPayType()
        {
            this.PayTypeDDL.DataTextField = "PayName";
            this.PayTypeDDL.DataValueField = "PayCode";
            this.PayTypeDDL.DataSource = this._payTypeBL.GetListDDLBank();
            this.PayTypeDDL.DataBind();
            this.PayTypeDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.SumValueForexTextBox.Attributes.Add("ReadOnly", "True");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowData()
        {
            Finance_BankRecon _bankRecon = this._bankReconBL.GetSingleFinance_BankRecon(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            this.TransNoTextBox.Text = _bankRecon.TransNmbr;
            this.FileNmbrTextBox.Text = _bankRecon.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_bankRecon.TransDate);
            this.PayTypeDDL.SelectedValue = _bankRecon.PayCode;
            string _account = _payTypeBL.GetAccountByCode(_bankRecon.PayCode);
            string _currCode = _accountBL.GetCurrByAccCode(_account);
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);
            this.SumValueForexTextBox.Text = _bankRecon.SumValueForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _bankRecon.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            Finance_BankRecon _bankRecon = this._bankReconBL.GetSingleFinance_BankRecon(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            _bankRecon.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _bankRecon.PayCode = this.PayTypeDDL.SelectedValue;
            _bankRecon.AccPay = _payTypeBL.GetAccountByCode(_bankRecon.PayCode);
            _bankRecon.FgPay = _payTypeBL.GetFgMode(_bankRecon.PayCode);
            _bankRecon.SumValueForex = Convert.ToDecimal(this.SumValueForexTextBox.Text);
            _bankRecon.BankValueForex = _bankRecon.SumValueForex + _bankRecon.DiffValueForex;
            _bankRecon.Remark = this.RemarkTextBox.Text;

            _bankRecon.EditBy = HttpContext.Current.User.Identity.Name;
            _bankRecon.EditDate = DateTime.Now;

            string _result = this._bankReconBL.EditFinance_BankRecon(_bankRecon);

            if (_result == "")
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = _result;
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Finance_BankRecon _bankRecon = this._bankReconBL.GetSingleFinance_BankRecon(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            _bankRecon.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _bankRecon.PayCode = this.PayTypeDDL.SelectedValue;
            _bankRecon.AccPay = _payTypeBL.GetAccountByCode(_bankRecon.PayCode);
            _bankRecon.FgPay = _payTypeBL.GetFgMode(_bankRecon.PayCode);
            _bankRecon.SumValueForex = Convert.ToDecimal(this.SumValueForexTextBox.Text);
            _bankRecon.BankValueForex = _bankRecon.SumValueForex + _bankRecon.DiffValueForex;
            _bankRecon.Remark = this.RemarkTextBox.Text;

            _bankRecon.EditBy = HttpContext.Current.User.Identity.Name;
            _bankRecon.EditDate = DateTime.Now;

            string _result = this._bankReconBL.EditFinance_BankRecon(_bankRecon);

            if (_result == "")
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = _result;
            }
        }

        protected void PayTypeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _account = _payTypeBL.GetAccountByCode(this.PayTypeDDL.SelectedValue);
            string _currCode = _accountBL.GetCurrByAccCode(_account);
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);

            if (this.PayTypeDDL.SelectedValue != "null")
            {
                decimal _value = Convert.ToDecimal(_bankReconBL.GetAmountBankReconByAccount(DateFormMapper.GetValue(this.DateTextBox.Text), _account));
                this.SumValueForexTextBox.Text = (_value == 0) ? "0" : _value.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)); //_bankReconBL.GetAmountBankReconByAccount(DateFormMapper.GetValue(this.DateTextBox.Text), _account).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            else
            {
                this.SumValueForexTextBox.Text = "0";// 0.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
        }
    }
}