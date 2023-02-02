using System;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.CurrencyRate
{
    public partial class CurrencyRateAdd : CurrencyRateBase
    {
        private CurrencyRateBL _currRateBL = new CurrencyRateBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private PermissionBL _permBL = new PermissionBL();

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
                this.CurrDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.CurrDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowCurrCode();

                this.ClearData();
                this.SetAttribute();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.CurrDateTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenPlace.ClientID + ");");
        }

        private void ClearData()
        {
            this.ClearLabel();

            DateTime now = DateTime.Now;
            this.CurrDateTextBox.Text = DateFormMapper.GetValue(now);
            this.CurrRateTextBox.Text = "0";
            this.CurrCodeDropDownList.SelectedValue = "null";
            this.DecimalPlaceHiddenPlace.Value = "";
        }

        private void ShowCurrCode()
        {
            this.CurrCodeDropDownList.DataTextField = "CurrCode";
            this.CurrCodeDropDownList.DataValueField = "CurrCode";
            this.CurrCodeDropDownList.DataSource = _currencyBL.GetList();
            this.CurrCodeDropDownList.DataBind();
            this.CurrCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            MsCurrRate _msCurrRate = new MsCurrRate();

            _msCurrRate.CurrDate = DateFormMapper.GetValue(this.CurrDateTextBox.Text);
            _msCurrRate.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _msCurrRate.CurrRate = Convert.ToDecimal(this.CurrRateTextBox.Text);

            bool _result = this._currRateBL.Add(_msCurrRate);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            this.ClearData();
        }

        protected void CurrCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CurrCodeDropDownList.SelectedValue != "null")
            {
                byte _decimalPlace = _currencyBL.GetDecimalPlace(this.CurrCodeDropDownList.SelectedValue);
                this.DecimalPlaceHiddenPlace.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            }
        }
    }
}