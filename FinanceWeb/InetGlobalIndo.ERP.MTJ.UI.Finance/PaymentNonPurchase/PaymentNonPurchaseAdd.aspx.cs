using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PaymentNonPurchase
{
    public partial class PaymentNonPurchaseAdd : PaymentNonPurchaseBase
    {
        private SupplierBL _supplierBL = new SupplierBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private PaymentNonPurchaseBL _paymentNonPurchaseBL = new PaymentNonPurchaseBL();
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
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowSupplier();
                this.ShowCurrency();

                this.SetAttribute();
                this.ClearData();
            }
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");

            this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowSupplier()
        {
            this.SuppNameDropDownList.Items.Clear();
            this.SuppNameDropDownList.DataTextField = "SuppName";
            this.SuppNameDropDownList.DataValueField = "SuppCode";
            this.SuppNameDropDownList.DataSource = this._supplierBL.GetListDDLSupp();
            this.SuppNameDropDownList.DataBind();
            this.SuppNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowCurrency()
        {
            this.CurrCodeDropDownList.Items.Clear();
            this.CurrCodeDropDownList.DataTextField = "CurrCode";
            this.CurrCodeDropDownList.DataValueField = "CurrCode";
            this.CurrCodeDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrCodeDropDownList.DataBind();
            this.CurrCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ClearLabel();

            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            this.SuppNameDropDownList.SelectedValue = "null";
            this.CurrCodeDropDownList.SelectedValue = "null";
            this.CurrRateTextBox.Text = "0";
            this.CurrRateTextBox.Attributes.Remove("ReadOnly");
            this.RemarkTextBox.Text = "";
        }

        protected void CurrCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrCodeDropDownList.SelectedValue != "null")
            {
                this.SetCurrRate();
            }
            else
            {
                this.CurrRateTextBox.Text = "0";
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#ffffff");
            }
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            FINPayNonTradeHd _finPayNonTradeHd = new FINPayNonTradeHd();

            _finPayNonTradeHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finPayNonTradeHd.Status = PaymentNonPurchaseDataMapper.GetStatus(TransStatus.OnHold);
            _finPayNonTradeHd.SuppCode = this.SuppNameDropDownList.SelectedValue;
            _finPayNonTradeHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _finPayNonTradeHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finPayNonTradeHd.Remark = this.RemarkTextBox.Text;

            _finPayNonTradeHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finPayNonTradeHd.DatePrep = DateTime.Now;

            string _result = this._paymentNonPurchaseBL.AddFINPayNonTradeHd(_finPayNonTradeHd);

            if (_result != "")
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        protected void SuppNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SuppNameDropDownList.SelectedValue != "null")
            {
                this.ShowCurrency();

                string _currCode = this._supplierBL.GetCurr(this.SuppNameDropDownList.SelectedValue);
                this.CurrRateTextBox.Text = Convert.ToString(this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue));

                if (_currCode != "")
                {
                    this.CurrCodeDropDownList.SelectedValue = _currCode;
                }
                else
                {
                    this.CurrCodeDropDownList.SelectedValue = "null";
                }

                this.SetCurrRate();
            }
            else
            {
                this.CurrCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.CurrCodeDropDownList.SelectedValue = "null";
                this.CurrRateTextBox.Text = "0";
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#ffffff");
            }
        }

        private void EnableRate()
        {
            this.CurrRateTextBox.Attributes.Remove("ReadOnly");
            this.CurrRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
        }

        private void DisableRate()
        {
            this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.CurrRateTextBox.Text = "1";
        }

        private void SetCurrRate()
        {
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrCodeDropDownList.SelectedValue);
            this.CurrRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            if (this.CurrCodeDropDownList.SelectedValue.Trim().ToLower() == _currencyBL.GetCurrDefault().Trim().ToLower())
            {
                this.DisableRate();
            }
            else
            {
                this.EnableRate();
            }
        }
    }
}