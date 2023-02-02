using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.GiroReceiptChange
{
    public partial class GiroReceiptChangeDetailView : GiroReceiptChangeBase
    {
        private FINChangeGiroInBL _finChangeGiroInBL = new FINChangeGiroInBL();
        private FINGiroInBL _finGiroInBL = new FINGiroInBL();
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

                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowData();
            }
        }

        private void ShowData()
        {
            FINChangeGiroInDt _finChangeGiroInDt = this._finChangeGiroInBL.GetSingleFINChangeGiroInDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._giroKey), ApplicationConfig.EncryptionKey));

            string _currCode = _finChangeGiroInDt.CurrCode;
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);

            this.DateTextBox.Text = DateFormMapper.GetValue(_finChangeGiroInDt.ReceiptDate);
            this.DueDateTextBox.Text = DateFormMapper.GetValue(_finChangeGiroInDt.DueDate);
            this.OldGiroTextBox.Text = _finChangeGiroInDt.OldGiro;
            this.BankGiroTextBox.Text = _bankBL.GetBankNameByCode(_finChangeGiroInDt.BankGiro);
            this.CurrTextBox.Text = _finChangeGiroInDt.CurrCode;
            this.RateTextBox.Text = _finChangeGiroInDt.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountForexTextBox.Text = _finChangeGiroInDt.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}