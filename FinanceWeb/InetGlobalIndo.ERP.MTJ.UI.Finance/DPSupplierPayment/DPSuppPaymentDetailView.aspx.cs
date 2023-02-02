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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.DPSupplierPayment
{
    public partial class DPSuppPaymentDetailView : DPSupplierPaymentBase
    {
        private FINDPSuppPayBL _finDPSuppBL = new FINDPSuppPayBL();
        private PaymentBL _payBL = new PaymentBL();
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

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
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
            FINDPSuppDt _finDPSuppDt = this._finDPSuppBL.GetSingleFINDPSuppDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._itemKey), ApplicationConfig.EncryptionKey)));

            string _currCode = _finDPSuppBL.GetCurrCodeHeader(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);

            string _currCodeHome = this._currencyBL.GetCurrDefault();
            byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(_currCodeHome);

            //this.TransNoTextBox.Text = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            this.PaymentTypeTextBox.Text = _payBL.GetPaymentName(_finDPSuppDt.PayType);
            this.DocNoTextBox.Text = _finDPSuppDt.DocumentNo;
            this.AmountForexTextBox.Text = _finDPSuppDt.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            if (_finDPSuppDt.BankPayment != "null")
            {
                this.BankPaymentTextBox.Text = _finDPSuppDt.BankPayment;
            }
            else
            {
                this.BankPaymentTextBox.Text = "";
            }
            this.DueDateTextBox.Text = DateFormMapper.GetValue(_finDPSuppDt.DueDate);

            decimal _bankExpense = Convert.ToDecimal((_finDPSuppDt.BankExpense == null) ? 0 : _finDPSuppDt.BankExpense);
            this.BankExpenseTextBox.Text = (_bankExpense == 0 ? "" : _bankExpense.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome)));
            this.RemarkTextBox.Text = _finDPSuppDt.Remark;

            char _status = this._finDPSuppBL.GetStatusFINDPSuppHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_status == DPSuppPayDataMapper.GetStatus(TransStatus.Posted))
            {
                this.EditButton.Visible = false;
            }
            else
            {
                this.EditButton.Visible = true;
            }
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._itemKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._itemKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}