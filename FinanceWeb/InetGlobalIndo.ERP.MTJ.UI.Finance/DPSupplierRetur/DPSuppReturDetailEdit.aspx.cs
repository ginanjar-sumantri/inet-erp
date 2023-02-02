using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.DPSupplierRetur
{
    public partial class DPSuppReturDetailEdit : DPSupplierReturBase
    {
        private FINDPSuppReturBL _finDPSuppReturBL = new FINDPSuppReturBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private FINDPSuppPayBL _finDPSuppBL = new FINDPSuppPayBL();
        private PermissionBL _permBL = new PermissionBL();

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

                this.ClearLabel();
                this.SetAttribute();

                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.BaseForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.BaseForexTextBox.ClientID + "," + this.PPNTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        public void SetAttribute()
        {
            this.CurrCodeTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.SetAttributeRate();
        }

        public void ShowData()
        {
            FINDPSuppReturDt _finDPSuppReturDt = this._finDPSuppReturBL.GetSingleFINDPSuppReturDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

            this.DPNoTextBox.Text = this._finDPSuppBL.GetSingleFINDPSuppHd(_finDPSuppReturDt.DPNo).FileNmbr;
            this.CurrCodeTextBox.Text = _finDPSuppReturDt.CurrCode;

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrCodeTextBox.Text);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            string _defaultCurrency = this._currencyBL.GetCurrDefault();
            if (_finDPSuppReturDt.CurrCode.Trim().ToLower() == _defaultCurrency.Trim().ToLower())
            {
                this.CurrRateTextBox.Text = "1";
                this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#cccccc");
            }
            else
            {
                this.CurrRateTextBox.Text = (_finDPSuppReturDt.ForexRate == 0) ? "0" : _finDPSuppReturDt.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#ffffff");
            }

            this.BaseForexTextBox.Text = (_finDPSuppReturDt.BaseForex == 0) ? "0" : _finDPSuppReturDt.BaseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNTextBox.Text = (_finDPSuppReturDt.PPN == 0) ? "0" : _finDPSuppReturDt.PPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNForexTextBox.Text = (_finDPSuppReturDt.PPNForex == 0) ? "0" : _finDPSuppReturDt.PPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TotalForexTextBox.Text = (_finDPSuppReturDt.TotalForex == 0) ? "0" : _finDPSuppReturDt.TotalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _finDPSuppReturDt.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINDPSuppReturDt _finDPSuppReturDt = this._finDPSuppReturBL.GetSingleFINDPSuppReturDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

            _finDPSuppReturDt.CurrCode = this.CurrCodeTextBox.Text;
            _finDPSuppReturDt.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finDPSuppReturDt.BaseForex = Convert.ToDecimal(this.BaseForexTextBox.Text);
            _finDPSuppReturDt.PPN = Convert.ToDecimal(this.PPNTextBox.Text);
            _finDPSuppReturDt.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _finDPSuppReturDt.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            _finDPSuppReturDt.Remark = this.RemarkTextBox.Text;

            bool _result = this._finDPSuppReturBL.EditFINDPSuppReturDt(_finDPSuppReturDt);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }
    }
}
