using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.GiroPaymentChange
{
    public partial class GiroPaymentChangeDetailView : GiroPaymentChangeBase
    {
        private FINGiroOutBL _finGiroOutBL = new FINGiroOutBL();
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

                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            FINChangeGiroOutDt _finChangeGiroOutDt = this._finChangeGiroOutBL.GetSingleFINChangeGiroOutDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItem), ApplicationConfig.EncryptionKey));

            string _currCode = _finChangeGiroOutDt.CurrCode;
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);

            //this.TransNoTextBox.Text = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            this.OldGiroTextBox.Text = _finChangeGiroOutDt.OldGiro;
            this.DateTextBox.Text = DateFormMapper.GetValue(_finChangeGiroOutDt.PaymentDate);
            this.DueDateTextBox.Text = DateFormMapper.GetValue(_finChangeGiroOutDt.DueDate);
            this.BankPaymentTextBox.Text = this._bankBL.GetBankPaymentNameByCode(_finChangeGiroOutDt.BankPayment);
            this.CurrTextBox.Text = _finChangeGiroOutDt.CurrCode;
            this.RateTextBox.Text = (_finChangeGiroOutDt.ForexRate == 0) ? "0" : _finChangeGiroOutDt.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountForexTextBox.Text = (_finChangeGiroOutDt.AmountForex == 0) ? "0" : _finChangeGiroOutDt.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}