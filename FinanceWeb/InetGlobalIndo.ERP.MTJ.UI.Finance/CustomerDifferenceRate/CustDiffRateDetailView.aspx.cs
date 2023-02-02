using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.CustomerDifferenceRate
{
    public partial class CustDiffRateDetailView : CustomerDifferenceRateBase
    {
        private FINARRateBL _finARRateBL = new FINARRateBL();
        private CustomerBL _custBL = new CustomerBL();
        private CurrencyRateBL _currRateBL = new CurrencyRateBL();
        private FINReceiptTradeBL _finReceiptTradeBL = new FINReceiptTradeBL();
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

                FINARRateHd _finARRateHd = this._finARRateBL.GetSingleFINARRateHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                if (_finARRateHd.Status != ARRateDataMapper.GetStatus(TransStatus.Posted))
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
            FINARRateDt _finARRateDt = this._finARRateBL.GetSingleFINARRateDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._invoiceKey), ApplicationConfig.EncryptionKey));
            FINARPosting _finARPosting = this._finReceiptTradeBL.GetSingleFINARPosting(_finARRateDt.InvoiceNo);

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _currCode = this._finARRateBL.GetSingleFINARRateHd(_transNo).CurrCode;
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);
            string _currHome = this._currencyBL.GetCurrDefault();
            byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(_currHome);

            this.InvoiceNoTextBox.Text = _finARPosting.FileNmbr;
            this.CustTextBox.Text = _custBL.GetNameByCode(_finARRateDt.CustCode);
            this.AmountForexTextBox.Text = (_finARRateDt.AmountForex == 0) ? "0" : _finARRateDt.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.ForexRateTextBox.Text = (_finARRateDt.ForexRate == 0) ? "0" : _finARRateDt.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountHomeTextBox.Text = (_finARRateDt.AmountHome == 0) ? "0" : _finARRateDt.AmountHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
            this.NewAmountHomeTextBox.Text = (_finARRateDt.NewAmountHome == 0) ? "0" : _finARRateDt.NewAmountHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
            decimal _adjust = (_finARRateDt.NewAmountHome - _finARRateDt.AmountHome);
            this.AdjustTextBox.Text = (_adjust == 0) ? "0" : _adjust.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
            this.IsApplyToPPNCheckBox.Checked = _finARRateDt.IsApplyToPPN;
            this.PPNForexTextBox.Text = (_finARRateDt.PPNForex == 0) ? "0" : _finARRateDt.PPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNRateTextBox.Text = (_finARRateDt.PPNRate == 0) ? "0" : _finARRateDt.PPNRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNHomeTextBox.Text = (_finARRateDt.PPNHome == 0) ? "0" : _finARRateDt.PPNHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
            this.NewPPNHomeTextBox.Text = (_finARRateDt.NewPPNHome == 0) ? "0" : _finARRateDt.NewPPNHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
            decimal _adjustPPN = (_finARRateDt.NewPPNHome - _finARRateDt.PPNHome);
            this.PPNAdjustTextBox.Text = (_adjustPPN == 0) ? "0" : _adjustPPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._invoiceKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._invoiceKey)));
        }
    }
}