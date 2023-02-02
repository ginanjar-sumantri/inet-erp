using System;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.CurrencyRate
{
    public partial class CurrencyRateEdit : CurrencyRateBase
    {
        private CurrencyRateBL _currRate = new CurrencyRateBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currBL = new CurrencyBL();

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
                this.CurrDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.CurrDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ClearLabel();
                this.ShowData();
                this.SetAttribute();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.CurrCodeTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        private void ShowData()
        {
            MsCurrRate _msCurrRate = this._currRate.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeDate), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = _currBL.GetDecimalPlace(_msCurrRate.CurrCode);

            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.CurrDateTextBox.Text = DateFormMapper.GetValue(_msCurrRate.CurrDate);
            this.CurrCodeTextBox.Text = _msCurrRate.CurrCode;
            this.CurrRateTextBox.Text = (_msCurrRate.CurrRate == 0) ? "0" : _msCurrRate.CurrRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            MsCurrRate _msCurrRate = this._currRate.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeDate), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msCurrRate.CurrRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            
            bool _result = this._currRate.Edit(_msCurrRate);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click1(object sender, EventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}
