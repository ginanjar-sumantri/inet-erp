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

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.DPCustomerRetur
{
    public partial class DPCustomerReturPayView : DPCustomerReturBase
    {
        private PaymentBL _paymentBL = new PaymentBL();
        private FINDPCustomerReturBL _finDPCustomerReturBL = new FINDPCustomerReturBL();
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

            _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                FINDPCustReturHd _finDPCustReturHd = this._finDPCustomerReturBL.GetSingleFINDPCustReturHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
                if (_finDPCustReturHd.Status != DPCustomerReturDataMapper.GetStatus(TransStatus.Posted))
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

        public void ShowData()
        {
            FINDPCustReturPay _finDPCustReturPay = this._finDPCustomerReturBL.GetSingleFINDPCustReturPay(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItem), ApplicationConfig.EncryptionKey)));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finDPCustReturPay.CurrCode);
            byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(this._currencyBL.GetCurrDefault());

            this.PayTypeTextBox.Text = this._paymentBL.GetPaymentName(_finDPCustReturPay.PayType);
            this.DocumentNoTextBox.Text = _finDPCustReturPay.DocumentNo;
            this.CurrCodeTextBox.Text = _finDPCustReturPay.CurrCode;
            this.CurrRateTextBox.Text = (_finDPCustReturPay.ForexRate == 0) ? "0" : _finDPCustReturPay.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountForexTextBox.Text = (_finDPCustReturPay.AmountForex == 0) ? "0" : _finDPCustReturPay.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _finDPCustReturPay.Remark;
            this.BankPaymentTextBox.Text = this._bankBL.GetBankPaymentNameByCode(_finDPCustReturPay.BankPayment);

            if (_finDPCustReturPay.DueDate != null)
            {
                this.DateTextBox.Text = DateFormMapper.GetValue(_finDPCustReturPay.DueDate);
            }

            if (_finDPCustReturPay.BankExpense != null)
            {
                decimal _tempBankExpense = Convert.ToDecimal(_finDPCustReturPay.BankExpense);
                this.BankExpenseTextBox.Text = (_tempBankExpense == 0) ? "0" : _tempBankExpense.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
            }
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage2 + "?" + this._codeItem + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeItem)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeItem + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeItem)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}