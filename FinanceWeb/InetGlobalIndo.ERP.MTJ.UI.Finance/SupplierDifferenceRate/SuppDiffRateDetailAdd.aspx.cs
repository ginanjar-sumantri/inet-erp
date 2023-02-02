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
    public partial class SuppDiffRateDetailAdd : SupplierDifferenceRateBase
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
                this.ShowInvoiceNo();
                this.ClearData();
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

        private void ClearData()
        {
            this.ClearLabel();
            this.InvoiceNoDropDownList.SelectedValue = "null";
            this.SuppNameTextBox.Text = "";
            this.ForexRateTextBox.Text = "0";
            this.AmountForexTextBox.Text = "0";
            this.AmountHomeTextBox.Text = "0";
            this.NewAmountHomeTextBox.Text = "0";
            this.IsApplyToPPNCheckBox.Checked = false;
            this.PPNAdjustTextBox.Text = "0";
            this.PPNForexTextBox.Text = "0";
            this.PPNHomeTextBox.Text = "0";
            this.PPNRateTextBox.Text = "0";
            this.NewPPNHomeTextBox.Text = "0";
        }

        private void ShowInvoiceNo()
        {
            string _currCodeHeader = _finAPRateBL.GetCurrCodeHeader(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            decimal _newForexRateHeader = _finAPRateBL.GetNewForexRateHeader(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_currCodeHeader != "")
            {
                this.InvoiceNoDropDownList.Items.Clear();
                this.InvoiceNoDropDownList.DataTextField = "FileNmbr";
                this.InvoiceNoDropDownList.DataValueField = "InvoiceNo";
                this.InvoiceNoDropDownList.DataSource = this._payTradeBL.GetListInvoiceNoForDDLAPRate(_currCodeHeader, _newForexRateHeader);
                this.InvoiceNoDropDownList.DataBind();
                this.InvoiceNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
            else
            {
                this.InvoiceNoDropDownList.Items.Clear();
                this.InvoiceNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINAPRateDt _finAPRateDt = new FINAPRateDt();
            FINAPPosting _finAPPosting = _payTradeBL.GetSingleFINAPPosting(this.InvoiceNoDropDownList.SelectedValue);

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            _finAPRateDt.TransNmbr = _transNo;
            _finAPRateDt.InvoiceNo = this.InvoiceNoDropDownList.SelectedValue;
            _finAPRateDt.SuppCode = _finAPPosting.SuppCode;
            _finAPRateDt.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _finAPRateDt.AmountForex = Convert.ToDecimal(this.AmountForexTextBox.Text);
            _finAPRateDt.AmountHome = Convert.ToDecimal(this.AmountHomeTextBox.Text);
            _finAPRateDt.NewAmountHome = Convert.ToDecimal(this.NewAmountHomeTextBox.Text);
            _finAPRateDt.FgValue = _finAPPosting.FgValue;
            _finAPRateDt.IsApplyToPPN = this.IsApplyToPPNCheckBox.Checked;
            _finAPRateDt.PPNRate = Convert.ToDecimal(this.PPNRateTextBox.Text);
            _finAPRateDt.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _finAPRateDt.PPNHome = Convert.ToDecimal(this.PPNHomeTextBox.Text);
            _finAPRateDt.NewPPNHome = Convert.ToDecimal(this.NewPPNHomeTextBox.Text);
            _finAPRateDt.FgPPNValue = _finAPPosting.FgValue;

            bool _result = this._finAPRateBL.AddFINAPRateDt(_finAPRateDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        protected void InvoiceNoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.InvoiceNoDropDownList.SelectedValue != "null")
            {
                FINAPPosting _finAPPosting = this._payTradeBL.GetSingleFINAPPosting(this.InvoiceNoDropDownList.SelectedValue);

                string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
                string _currCode = this._finAPRateBL.GetSingleFINAPRateHd(_transNo).CurrCode;
                byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);
                string _currHome = this._currencyBL.GetCurrDefault();
                byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(_currHome);

                this.SuppNameTextBox.Text = _suppBL.GetSuppNameByCode(_finAPPosting.SuppCode);
                this.ForexRateTextBox.Text = (_finAPPosting.ForexRate == 0) ? "0" : _finAPPosting.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                decimal _amountForex = _finAPPosting.Amount - _finAPPosting.Balance;
                this.AmountForexTextBox.Text = (_amountForex == 0) ? "0" : _amountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                decimal _amountHome = Convert.ToDecimal(this.ForexRateTextBox.Text) * Convert.ToDecimal(this.AmountForexTextBox.Text);
                this.AmountHomeTextBox.Text = (_amountHome == 0) ? "0" : _amountHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
                decimal _newAmount = Convert.ToDecimal(this.AmountForexTextBox.Text) * Convert.ToDecimal(this._finAPRateBL.GetNewRateHeader(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));
                this.NewAmountHomeTextBox.Text = (_newAmount == 0) ? "0" : _newAmount.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
                decimal _adjust = _newAmount - _amountHome;
                this.AdjustTextBox.Text = (_adjust == 0) ? "0" : _adjust.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
                if (this.IsApplyToPPNCheckBox.Checked == true)
                {
                    this.PPNRateTextBox.Text = (_finAPPosting.PPNRate == 0) ? "0" : Convert.ToDecimal(_finAPPosting.PPNRate).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                    decimal _ppnForex = Convert.ToDecimal(_finAPPosting.AmountPPN) - Convert.ToDecimal(_finAPPosting.BalancePPN);
                    this.PPNForexTextBox.Text = (_ppnForex == 0) ? "0" : _ppnForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                    decimal _ppnHome = Convert.ToDecimal(this.PPNRateTextBox.Text) * Convert.ToDecimal(this.PPNForexTextBox.Text);
                    this.PPNHomeTextBox.Text = (_ppnHome == 0) ? "0" : _ppnHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
                    decimal _newPPN = Convert.ToDecimal(this.PPNForexTextBox.Text) * Convert.ToDecimal(this._finAPRateBL.GetNewRateHeader(_transNo));
                    this.NewPPNHomeTextBox.Text = (_newPPN == 0) ? "0" : _newPPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
                    decimal _adjustPPN = _newPPN - _ppnHome;
                    this.PPNAdjustTextBox.Text = (_adjustPPN == 0) ? "0" : _adjustPPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
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
            else
            {
                this.SuppNameTextBox.Text = "";
                this.ForexRateTextBox.Text = "0";
                this.AmountForexTextBox.Text = "0";
                this.AmountHomeTextBox.Text = "0";
                this.NewAmountHomeTextBox.Text = "0";
                this.AdjustTextBox.Text = "0";
            }
        }

        protected void IsApplyToPPNCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.InvoiceNoDropDownList.SelectedValue != "null")
            {
                if (this.IsApplyToPPNCheckBox.Checked == true)
                {
                    FINAPPosting _finAPPosting = this._payTradeBL.GetSingleFINAPPosting(this.InvoiceNoDropDownList.SelectedValue);

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
}