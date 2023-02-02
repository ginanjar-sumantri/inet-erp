using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DataMapping;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.PriceGroup
{
    public partial class PriceGroupEdit : PriceGroupBase
    {
        private PriceGroupBL _priceGroupBL = new PriceGroupBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRate = new CurrencyRateBL();

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
                this.StartDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.StartDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.EndDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.EndDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ClearLabel();
                this.ShowCurrency();
                this.SetAttribute();
                this.ShowData();
            }
        }

        protected void SetAttribute()
        {
            this.AmountForexTextBox.Attributes.Add("OnBlur", "return ChangeFormat2(" + this.AmountForexTextBox.ClientID + ", " + this.DecimalPlaceHiddenField.ClientID + ");");
            this.YearTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.StartDateTextBox.Attributes.Add("ReadOnly", "True");
            this.EndDateTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowCurrency()
        {
            this.CurrencyDropDownList.DataTextField = "CurrCode";
            this.CurrencyDropDownList.DataValueField = "CurrCode";
            this.CurrencyDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrencyDropDownList.DataBind();
            this.CurrencyDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

            this.SellingCurrBMDropDownList.DataSource = _currencyBL.GetListAll();
            this.SellingCurrBMDropDownList.DataValueField = "CurrCode";
            this.SellingCurrBMDropDownList.DataValueField = "CurrCode";
            this.SellingCurrBMDropDownList.DataBind();
            this.SellingCurrBMDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

            this.SellingCurrOWDropDownList.DataSource = _currencyBL.GetListAll();
            this.SellingCurrOWDropDownList.DataValueField = "CurrCode";
            this.SellingCurrOWDropDownList.DataValueField = "CurrCode";
            this.SellingCurrOWDropDownList.DataBind();
            this.SellingCurrOWDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowData()
        {
            this.DecimalPlaceHiddenField.Value = "";

            Master_PriceGroup _msPriceGroup = this._priceGroupBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._yearKey), ApplicationConfig.EncryptionKey)));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_msPriceGroup.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            this.PriceGroupCodeTextBox.Text = _msPriceGroup.PriceGroupCode;
            this.YearTextBox.Text = _msPriceGroup.Year.ToString();
            this.CurrencyDropDownList.SelectedValue = _msPriceGroup.CurrCode;
            this.AmountForexTextBox.Text = (_msPriceGroup.AmountForex == 0 ? "0" : _msPriceGroup.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));
            this.RemarkTextBox.Text = _msPriceGroup.PGDesc;
            this.IsActiveCheckBox.Checked = _msPriceGroup.FgActive;
            this.StartDateTextBox.Text = DateFormMapper.GetValue(_msPriceGroup.StartDate);
            this.EndDateTextBox.Text = DateFormMapper.GetValue(_msPriceGroup.EndDate);

            this.SellingCurrOWDropDownList.SelectedValue = _msPriceGroup.SellingCurrOW;
            this.SellingPriceOWTextBox.Text = Convert.ToDecimal(_msPriceGroup.SelingPriceOW).ToString("#,##0.00");
            this.SellingCurrBMDropDownList.SelectedValue = _msPriceGroup.SellingCurrBM;
            this.SellingPriceBMTextBox.Text = Convert.ToDecimal(_msPriceGroup.SelingPriceBM).ToString("#,##0.00");
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            Master_PriceGroup _masterPriceGroup = this._priceGroupBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._yearKey), ApplicationConfig.EncryptionKey)));

            _masterPriceGroup.CurrCode = this.CurrencyDropDownList.SelectedValue;
            _masterPriceGroup.AmountForex = Convert.ToDecimal(this.AmountForexTextBox.Text);
            _masterPriceGroup.StartDate = DateFormMapper.GetValue(this.StartDateTextBox.Text);
            _masterPriceGroup.EndDate = DateFormMapper.GetValue(this.EndDateTextBox.Text);
            _masterPriceGroup.PGDesc = this.RemarkTextBox.Text;

            _masterPriceGroup.SellingCurrOW = this.SellingCurrOWDropDownList.SelectedValue;
            _masterPriceGroup.SelingPriceOW = Convert.ToDecimal(this.SellingPriceOWTextBox.Text);
            _masterPriceGroup.SellingCurrBM = this.SellingCurrBMDropDownList.SelectedValue;
            _masterPriceGroup.SelingPriceBM = Convert.ToDecimal(this.SellingPriceBMTextBox.Text);

            if (this.IsActiveCheckBox.Checked == true)
            {
                _masterPriceGroup.FgActive = true;
            }
            else
            {
                _masterPriceGroup.FgActive = false;
            }
            _masterPriceGroup.EditBy = HttpContext.Current.User.Identity.Name;
            _masterPriceGroup.EditDate = DateTime.Now;

            bool _result = this._priceGroupBL.Edit(_masterPriceGroup);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        protected void CurrencyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CurrencyDropDownList.SelectedValue != "null")
            {
                this.DecimalPlaceHiddenField.Value = "";

                byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrencyDropDownList.SelectedValue);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            }
            else
            {
                this.DecimalPlaceHiddenField.Value = "";
            }
        }
    }
}