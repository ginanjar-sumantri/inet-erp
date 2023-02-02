using System;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.Currency
{
    public partial class CurrencyEdit : CurrencyBase
    {
        private CurrencyBL _currencyBL = new CurrencyBL();
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
                this.PageTitleLiteral.Text = _pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetAttribute();
                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.DecimalPlaceTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            //this.DecimalPlaceReportTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
        }

        private void ShowData()
        {
            MsCurrency _msCurrency = this._currencyBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.CurrCodeTextBox.Text = _msCurrency.CurrCode;
            this.CurrNameTextBox.Text = _msCurrency.CurrName;
            this.CurrDefaultCheckBox.Checked = HomeCurrency.IsHome(_msCurrency.FgHome);
            this.DecimalPlaceTextBox.Text = (_msCurrency.DecimalPlace == 0) ? "0" : _msCurrency.DecimalPlace.ToString();
            //this.DecimalPlaceReportTextBox.Text = (_msCurrency.DecimalPlaceReport == 0) ? "0" : _msCurrency.DecimalPlaceReport.ToString();
            this.ValueToleranceTextBox.Text = (_msCurrency.ValueTolerance == 0) ? "0" : _msCurrency.ValueTolerance.ToString("#,###.##");
        }

        private string IsValidDecimalPlace()
        {
            string _result = "";

            byte _decimalPlace = Convert.ToByte(this.DecimalPlaceTextBox.Text);
            //byte _decimalPlaceReport = Convert.ToByte(this.DecimalPlaceReportTextBox.Text);

            if (_decimalPlace < 0 || _decimalPlace > 8)
                return _result = "Decimal Place input range 0-8";

            //if (_decimalPlaceReport < 0 || _decimalPlaceReport > 8)
            //    return _result = "Decimal Place Report input range 0-8";

            //if (_decimalPlaceReport > _decimalPlace)
            //    return _result = "Decimal Place Report must less than Decimal Place";

            return _result;
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string _warning = this.IsValidDecimalPlace();
            if (_warning == "")
            {
                MsCurrency _msCurrency = this._currencyBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                _msCurrency.CurrName = this.CurrNameTextBox.Text;
                _msCurrency.FgHome = HomeCurrency.IsHome(this.CurrDefaultCheckBox.Checked);
                _msCurrency.UserID = HttpContext.Current.User.Identity.Name;
                _msCurrency.UserDate = DateTime.Now;
                _msCurrency.DecimalPlace = Convert.ToByte(this.DecimalPlaceTextBox.Text);
                //_msCurrency.DecimalPlaceReport = 2; //Convert.ToByte(this.DecimalPlaceReportTextBox.Text);
                _msCurrency.ValueTolerance = Convert.ToDecimal(this.ValueToleranceTextBox.Text);

                if (this._currencyBL.Edit(_msCurrency) == true)
                {
                    Response.Redirect(this._homePage);
                }
                else
                {
                    this.WarningLabel.Text = "You Failed Edit Currency";
                }
            }
            else
            {
                this.WarningLabel.Text = _warning;
            }
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(this._homePage);
        }
    }
}
