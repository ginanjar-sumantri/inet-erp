using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.GiroReceiptChange
{
    public partial class GiroReceiptChangePayView : GiroReceiptChangeBase
    {
        private FINChangeGiroInBL _finChangeGiroInBL = new FINChangeGiroInBL();
        private PaymentBL _payBL = new PaymentBL();
        private BankBL _bankBL = new BankBL();
        private CurrencyRateBL _currRateBL = new CurrencyRateBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();

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

            _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                FINChangeGiroInHd _finChangeGiroInHd = this._finChangeGiroInBL.GetSingleFINChangeGiroInHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                if (_finChangeGiroInHd.Status != GiroReceiptChangeDataMapper.GetStatus(TransStatus.Posted))
                {
                    this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                }
                else
                {
                    this.EditButton.Visible = false;
                }
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.SetButtonPermission();

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

        private void ShowData()
        {
            FINChangeGiroInPay _finChangeGiroInPay = this._finChangeGiroInBL.GetSingleFINChangeGiroInPay(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._itemKey), ApplicationConfig.EncryptionKey)));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrTextBox.Text);
            byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(this._currencyBL.GetCurrDefault());

            this.ReceiptTypeTextBox.Text = this._payBL.GetPaymentName(_finChangeGiroInPay.ReceiptType);
            this.DocNoTextBox.Text = _finChangeGiroInPay.DocumentNo;
            this.CurrTextBox.Text = _finChangeGiroInPay.CurrCode;
            this.RateTextBox.Text = _finChangeGiroInPay.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountForexTextBox.Text = _finChangeGiroInPay.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DueDateTextBox.Text = DateFormMapper.GetValue(_finChangeGiroInPay.DueDate);
            this.BankGiroTextBox.Text = this._bankBL.GetBankNameByCode(_finChangeGiroInPay.BankGiro);
            this.RemarkTextBox.Text = _finChangeGiroInPay.Remark;
            this.BankExpenseTextBox.Text = Convert.ToDecimal(_finChangeGiroInPay.BankExpense).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPayPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._itemKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._itemKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}