using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.GiroPaymentChange
{
    public partial class GiroPaymentChangePaymentView : GiroPaymentChangeBase
    {
        private PaymentBL _paymentBL = new PaymentBL();
        private FINChangeGiroOutBL _finChangeGiroOutBL = new FINChangeGiroOutBL();
        private BankBL _bankBL = new BankBL();
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

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                FINChangeGiroOutHd _finChangeGiroOutHd = this._finChangeGiroOutBL.GetSingleFINChangeGiroOutHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
                if (_finChangeGiroOutHd.Status != GiroPaymentChangeDataMapper.GetStatus(TransStatus.Posted))
                {
                    this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                }
                else
                {
                    this.EditButton.Visible = false;
                }

                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.SetButtonPermission();

                this.ClearLabel();
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

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            FINChangeGiroOutPay _finChangeOutPay = this._finChangeGiroOutBL.GetSingleFINChangeGiroOutPay(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItem), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finChangeOutPay.CurrCode);

            this.PayTypeTextBox.Text = this._paymentBL.GetPaymentName(_finChangeOutPay.PayType);
            this.DocumentNoTextBox.Text = _finChangeOutPay.DocumentNo;
            this.CurrCodeTextBox.Text = _finChangeOutPay.CurrCode;
            this.CurrRateTextBox.Text = (_finChangeOutPay.ForexRate == 0) ? "0" : _finChangeOutPay.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountForexTextBox.Text = (_finChangeOutPay.AmountForex == 0) ? "0" : _finChangeOutPay.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _finChangeOutPay.Remark;
            this.BankPaymentTextBox.Text = this._bankBL.GetBankPaymentNameByCode(_finChangeOutPay.BankPayment);

            if (_finChangeOutPay.DueDate != null)
            {
                this.DateTextBox.Text = DateFormMapper.GetValue(_finChangeOutPay.DueDate);
            }

            if (_finChangeOutPay.BankExpense != null)
            {
                decimal _tempBankExpense = Convert.ToInt32(_finChangeOutPay.BankExpense);
                this.BankExpenseTextBox.Text = (_tempBankExpense == 0) ? "0" : _tempBankExpense.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage2 + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeItem + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeItem)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

    }
}