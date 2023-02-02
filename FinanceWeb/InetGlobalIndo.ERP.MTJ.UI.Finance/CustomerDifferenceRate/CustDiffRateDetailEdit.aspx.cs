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
    public partial class CustDiffRateDetailEdit : CustomerDifferenceRateBase
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

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
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

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
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

        private void ShowData()
        {
            FINARRateDt _finARRateDt = this._finARRateBL.GetSingleFINARRateDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._invoiceKey), ApplicationConfig.EncryptionKey));
            FINARPosting _finARPosting = this._finReceiptTradeBL.GetSingleFINARPosting(_finARRateDt.InvoiceNo);

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _currCode = this._finARRateBL.GetSingleFINARRateHd(_transNo).CurrCode;
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);
            string _currHome = this._currencyBL.GetCurrDefault();
            byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(_currHome);

            //this.TransNoTextBox.Text = _finARRateDt.TransNmbr;
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

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINARRateDt _finARRateDt = this._finARRateBL.GetSingleFINARRateDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._invoiceKey), ApplicationConfig.EncryptionKey));
            FINARPosting _finARPosting = this._finReceiptTradeBL.GetSingleFINARPosting(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._invoiceKey), ApplicationConfig.EncryptionKey));

            _finARRateDt.IsApplyToPPN = this.IsApplyToPPNCheckBox.Checked;
            _finARRateDt.PPNRate = Convert.ToDecimal(this.PPNRateTextBox.Text);
            _finARRateDt.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _finARRateDt.PPNHome = Convert.ToDecimal(this.PPNHomeTextBox.Text);
            _finARRateDt.NewPPNHome = Convert.ToDecimal(this.NewPPNHomeTextBox.Text);
            _finARRateDt.FgPPNValue = _finARPosting.FgValue;

            bool _result = this._finARRateBL.EditFINARRateDt(_finARRateDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        protected void IsApplyToPPNCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.IsApplyToPPNCheckBox.Checked == true)
            {
                FINARPosting _finARPosting = this._finReceiptTradeBL.GetSingleFINARPosting(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._invoiceKey), ApplicationConfig.EncryptionKey));

                string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
                string _currCode = this._finARRateBL.GetSingleFINARRateHd(_transNo).CurrCode;
                byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);
                string _currHome = this._currencyBL.GetCurrDefault();
                byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(_currHome);

                this.PPNRateTextBox.Text = (_finARPosting.PPNRate == 0) ? "0" : Convert.ToDecimal(_finARPosting.PPNRate).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                decimal _ppnForex = _finARPosting.AmountPPN - Convert.ToDecimal(_finARPosting.BalancePPN);
                this.PPNForexTextBox.Text = (_ppnForex == 0) ? "0" : _ppnForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                decimal _ppnHome = Convert.ToDecimal(this.PPNRateTextBox.Text) * Convert.ToDecimal(this.PPNForexTextBox.Text);
                this.PPNHomeTextBox.Text = (_ppnHome == 0) ? "0" : _ppnHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
                decimal _newPPN = Convert.ToDecimal(this.PPNForexTextBox.Text) * Convert.ToDecimal(this._finARRateBL.GetNewRateHeader(_transNo));
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