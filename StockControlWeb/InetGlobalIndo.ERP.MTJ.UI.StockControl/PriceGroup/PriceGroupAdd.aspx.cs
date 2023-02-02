using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;


namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.PriceGroup
{
    public partial class PriceGroupAdd : PriceGroupBase
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
                this.ClearData();

            }
        }

        protected void SetAttribute()
        {
            this.AmountForexTextBox.Attributes.Add("OnBlur", "return ChangeFormat2(" + this.AmountForexTextBox.ClientID + ", " + this.DecimalPlaceHiddenField.ClientID + ");");
            this.YearTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot()");
            this.StartDateTextBox.Attributes.Add("ReadOnly","True");
            this.EndDateTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.SellingPriceOWTextBox.Attributes.Add("onkeyup", "harusAngka(this)");
            this.SellingPriceOWTextBox.Attributes.Add("onchange", "harusAngka(this)");
            this.SellingPriceBMTextBox.Attributes.Add("onkeyup", "harusAngka(this)");
            this.SellingPriceBMTextBox.Attributes.Add("onchange", "harusAngka(this)");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearData()
        {
            this.PriceGroupCodeTextBox.Text = "";
            this.YearTextBox.Text = "";
            this.CurrencyDropDownList.SelectedValue = "null";
            this.AmountForexTextBox.Text = "0";
            this.RemarkTextBox.Text = "";
            this.StartDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.EndDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
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

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            Master_PriceGroup _masterPriceGroup = new Master_PriceGroup();

            _masterPriceGroup.PriceGroupCode = this.PriceGroupCodeTextBox.Text;
            _masterPriceGroup.Year = Convert.ToInt32(this.YearTextBox.Text);
            _masterPriceGroup.CurrCode = this.CurrencyDropDownList.SelectedValue;
            _masterPriceGroup.AmountForex = Convert.ToDecimal(this.AmountForexTextBox.Text);
            _masterPriceGroup.PGDesc = this.RemarkTextBox.Text;
            _masterPriceGroup.FgActive = true;
            _masterPriceGroup.StartDate = DateFormMapper.GetValue(this.StartDateTextBox.Text);
            _masterPriceGroup.EndDate = DateFormMapper.GetValue(this.EndDateTextBox.Text);
            _masterPriceGroup.InsertBy = HttpContext.Current.User.Identity.Name;
            _masterPriceGroup.InsertDate = DateTime.Now;
            _masterPriceGroup.EditBy = HttpContext.Current.User.Identity.Name;
            _masterPriceGroup.EditDate = DateTime.Now;

            _masterPriceGroup.SellingCurrOW = this.SellingCurrOWDropDownList.SelectedValue;
            _masterPriceGroup.SelingPriceOW = Convert.ToDecimal( this.SellingPriceOWTextBox.Text );
            _masterPriceGroup.SellingCurrBM = this.SellingCurrBMDropDownList.SelectedValue;
            _masterPriceGroup.SelingPriceBM = Convert.ToDecimal(this.SellingPriceBMTextBox.Text);

            bool _result = this._priceGroupBL.Add(_masterPriceGroup);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }

        protected void CurrencyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CurrencyDropDownList.SelectedValue != "null")
            {
                this.DecimalPlaceHiddenField.Value = "";

                string _currCodeHome = _currencyBL.GetCurrDefault();

                byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrencyDropDownList.SelectedValue);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            }
        }
    }
}