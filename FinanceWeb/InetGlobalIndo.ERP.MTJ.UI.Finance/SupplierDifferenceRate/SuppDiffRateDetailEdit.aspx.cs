using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.SupplierDifferenceRate
{
    public partial class SuppDiffRateDetailEdit : SupplierDifferenceRateBase
    {
        private FINAPRateBL _finAPRateBL = new FINAPRateBL();
        private PaymentTradeBL _payTradeBL = new PaymentTradeBL();
        private SupplierBL _suppBL = new SupplierBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();

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
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetAttribute();
                this.ShowData();
            }
        }

        private void SetAttribute()
        {
            this.SuppNameTextBox.Attributes.Add("ReadOnly", "True");
            this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountForexTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountHomeTextBox.Attributes.Add("ReadOnly", "True");
            this.NewAmountHomeTextBox.Attributes.Add("ReadOnly", "True");
            this.AdjustTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNHomeTextBox.Attributes.Add("ReadOnly", "True");
            this.NewPPNHomeTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNAdjustTextBox.Attributes.Add("ReadOnly", "True");
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowData()
        {
            FINAPRateDt _finAPRateDt = this._finAPRateBL.GetSingleFINAPRateDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._itemKey), ApplicationConfig.EncryptionKey));
            FINAPRateHd _finAPRateHd = this._finAPRateBL.GetSingleFINAPRateHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            string _invNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._itemKey), ApplicationConfig.EncryptionKey);
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finAPRateHd.CurrCode);
            string _currHome = this._currencyBL.GetCurrDefault();
            byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(_currHome);

            this.InvoiceNoTextBox.Text = this._payTradeBL.GetFileNmbrFINAPPostingByInvoiceNo(_invNmbr);
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

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINAPRateDt _finAPRateDt = this._finAPRateBL.GetSingleFINAPRateDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._itemKey), ApplicationConfig.EncryptionKey));
            FINAPPosting _finAPPosting = _payTradeBL.GetSingleFINAPPosting(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._itemKey), ApplicationConfig.EncryptionKey));

            _finAPRateDt.IsApplyToPPN = this.IsApplyToPPNCheckBox.Checked;
            _finAPRateDt.PPNRate = Convert.ToDecimal(this.PPNRateTextBox.Text);
            _finAPRateDt.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _finAPRateDt.PPNHome = Convert.ToDecimal(this.PPNHomeTextBox.Text);
            _finAPRateDt.NewPPNHome = Convert.ToDecimal(this.NewPPNHomeTextBox.Text);
            _finAPRateDt.FgPPNValue = _finAPPosting.FgValue;

            bool _result = this._finAPRateBL.EditFINAPRateDt(_finAPRateDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }

        protected void IsApplyToPPNCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.IsApplyToPPNCheckBox.Checked == true)
            {
                FINAPPosting _finAPPosting = this._payTradeBL.GetSingleFINAPPosting(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._itemKey), ApplicationConfig.EncryptionKey));

                string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
                string _currCode = this._finAPRateBL.GetSingleFINAPRateHd(_transNo).CurrCode;
                byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);
                string _currHome = this._currencyBL.GetCurrDefault();
                byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(_currHome);

                this.PPNRateTextBox.Text = (_finAPPosting.PPNRate == 0) ? "0" : Convert.ToDecimal(_finAPPosting.PPNRate).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                decimal _ppnForex = Convert.ToDecimal(_finAPPosting.AmountPPN) - Convert.ToDecimal(_finAPPosting.BalancePPN);
                this.PPNForexTextBox.Text = (_ppnForex == 0) ? "0" : _ppnForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                decimal _ppnHome = Convert.ToDecimal(this.PPNRateTextBox.Text) * Convert.ToDecimal(this.PPNForexTextBox.Text);
                this.PPNHomeTextBox.Text = (_ppnHome == 0) ? "0" : _ppnHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
                decimal _newPPN = Convert.ToDecimal(this.PPNForexTextBox.Text) * Convert.ToDecimal(this._finAPRateBL.GetNewRateHeader(_transNo));
                this.NewPPNHomeTextBox.Text = (_newPPN == 0) ? "0" : _newPPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
                decimal _adjust = _newPPN - _ppnHome;
                this.PPNAdjustTextBox.Text = (_adjust == 0) ? "0" : _adjust.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
            }
            else
            {
                this.PPNRateTextBox.Text = "0";
                this.PPNForexTextBox.Text = "0";
                this.PPNHomeTextBox.Text = "0";
                this.NewPPNHomeTextBox.Text = "0";
                this.PPNAdjustTextBox.Text = "0";
            }
        }
    }
}