using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.SupplierDifferenceRate
{
    public partial class SuppDiffRateDetailView : SupplierDifferenceRateBase
    {
        private FINAPRateBL _finAPRateBL = new FINAPRateBL();
        private SupplierBL _suppBL = new SupplierBL();
        private PaymentTradeBL _paymentTradeBL = new PaymentTradeBL();
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

                char _status = this._finAPRateBL.GetSingleFINAPRateHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Status;

                if (_status != APRateDataMapper.GetStatus(TransStatus.Posted))
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
            FINAPRateDt _finAPRateDt = this._finAPRateBL.GetSingleFINAPRateDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._itemKey), ApplicationConfig.EncryptionKey));
            FINAPRateHd _finAPRateHd = this._finAPRateBL.GetSingleFINAPRateHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            string _invNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._itemKey), ApplicationConfig.EncryptionKey);
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finAPRateHd.CurrCode);
            string _currHome = this._currencyBL.GetCurrDefault();
            byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(_currHome);

            this.InvoiceNoTextBox.Text = this._paymentTradeBL.GetFileNmbrFINAPPostingByInvoiceNo(_invNmbr);
            this.SuppNameTextBox.Text = _suppBL.GetSuppNameByCode(_finAPRateDt.SuppCode);
            this.ForexRateTextBox.Text = (_finAPRateDt.ForexRate == 0) ? "0" : _finAPRateDt.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountForexTextBox.Text = (_finAPRateDt.AmountForex == 0) ? "0" : _finAPRateDt.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountHomeTextBox.Text = (_finAPRateDt.AmountHome == 0) ? "0" : _finAPRateDt.AmountHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
            this.NewAmountHomeTextBox.Text = (_finAPRateDt.NewAmountHome == 0) ? "0" : _finAPRateDt.NewAmountHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
            decimal _adjust = _finAPRateDt.NewAmountHome - _finAPRateDt.AmountHome;
            this.AdjustTextBox.Text = (_adjust == 0) ? "0" : _adjust.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.IsApplyToPPNCheckBox.Checked = _finAPRateDt.IsApplyToPPN;
            this.PPNForexTextBox.Text = (_finAPRateDt.PPNForex == 0) ? "0" : _finAPRateDt.PPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNRateTextBox.Text = (_finAPRateDt.PPNRate == 0) ? "0" : _finAPRateDt.PPNRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNHomeTextBox.Text = (_finAPRateDt.PPNHome == 0) ? "0" : _finAPRateDt.PPNHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
            this.NewPPNHomeTextBox.Text = (_finAPRateDt.NewPPNHome == 0) ? "0" : _finAPRateDt.NewPPNHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
            decimal _adjustPPN = (_finAPRateDt.NewPPNHome - _finAPRateDt.PPNHome);
            this.PPNAdjustTextBox.Text = (_adjustPPN == 0) ? "0" : _adjustPPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
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