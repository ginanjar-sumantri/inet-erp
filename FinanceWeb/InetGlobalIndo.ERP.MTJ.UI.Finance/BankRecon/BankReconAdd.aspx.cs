using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.BankRecon
{
    public partial class BankReconAdd : BankReconBase
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

                this.ShowPayType();
                this.SetAttribute();
                this.ClearLabel();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowPayType()
        {
            this.PayTypeDDL.DataTextField = "PayName";
            this.PayTypeDDL.DataValueField = "PayCode";
            this.PayTypeDDL.DataSource = this._payTypeBL.GetListDDLBank();
            this.PayTypeDDL.DataBind();
            this.PayTypeDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.SumValueForexTextBox.Attributes.Add("ReadOnly", "True");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.WarningLabel.Text = "";
            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            this.PayTypeDDL.SelectedValue = "null";
            this.SumValueForexTextBox.Text = "0";
            this.RemarkTextBox.Text = "";
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            string _result = "";
            Finance_BankRecon _bankRecon = new Finance_BankRecon();

            _bankRecon.BankReconCode = Guid.NewGuid();
            _bankRecon.Status = BankReconDataMapper.GetStatusByte(TransStatus.OnHold);
            _bankRecon.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _bankRecon.PayCode = this.PayTypeDDL.SelectedValue;
            _bankRecon.AccPay = _payTypeBL.GetAccountByCode(_bankRecon.PayCode);
            _bankRecon.FgPay = _payTypeBL.GetFgMode(_bankRecon.PayCode);
            _bankRecon.SumValueForex = Convert.ToDecimal(this.SumValueForexTextBox.Text);
            _bankRecon.DiffValueForex = 0;
            _bankRecon.BankValueForex = _bankRecon.SumValueForex;
            _bankRecon.Remark = this.RemarkTextBox.Text;

            _bankRecon.InsertBy = HttpContext.Current.User.Identity.Name;
            _bankRecon.InsertDate = DateTime.Now;
            _bankRecon.EditBy = HttpContext.Current.User.Identity.Name;
            _bankRecon.EditDate = DateTime.Now;

            if (_payTypeBL.IsExist(_bankRecon.PayCode) == true)
            {
                if (_accountBL.IsExist(_bankRecon.AccPay) == true)
                {
                    _result = this._bankReconBL.AddFinance_BankRecon(_bankRecon);
                }
                else
                {
                    _result = ", Account " + _bankRecon.AccPay + " not define at master chart of account";
                }
            }
            else
            {
                _result = ", Payment Type " + _bankRecon.PayCode + " does not exist";
            }

            if (_result != "")
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Add Data " + _result;
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }

        protected void PayTypeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateSumValue();
        }

        protected void DateTextBox_TextChanged(object sender, EventArgs e)
        {
            this.UpdateSumValue();
        }

        protected void UpdateSumValue()
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