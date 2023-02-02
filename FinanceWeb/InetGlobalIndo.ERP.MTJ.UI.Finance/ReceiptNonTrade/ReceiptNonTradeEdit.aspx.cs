using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.ReceiptNonTrade
{
    public partial class ReceiptNonTradeEdit : ReceiptNonTradeBase
    {
        private CustomerBL _custBL = new CustomerBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private PaymentNonTradeBL _paymentNonTradeBL = new PaymentNonTradeBL();
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
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.ClearLabel();
                this.ShowData();
                this.SetAttribute();
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

        public void ShowCust()
        {
            this.CustDropDownList.Items.Clear();
            this.CustDropDownList.DataTextField = "CustName";
            this.CustDropDownList.DataValueField = "CustCode";
            this.CustDropDownList.DataSource = this._custBL.GetListCustomerForDDL();
            this.CustDropDownList.DataBind();
            this.CustDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
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

        protected void CustDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CustDropDownList.SelectedValue != "null")
            {
                this.ShowCurrency();

                string _currCode = this._custBL.GetCurr(CustDropDownList.SelectedValue);
                this.CurrRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString("#,###.##");

                if (_currCode != "")
                {
                    this.CurrCodeDropDownList.SelectedValue = _currCode;
                }

                this.SetCurrRate();
            }
            else
            {
                this.CurrCodeDropDownList.SelectedValue = "null";
                this.CurrRateTextBox.Text = "0";
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#ffffff");
            }
        }

        private void DisableRate()
        {
            this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.CurrRateTextBox.Text = "1";
        }

        private void EnableRate()
        {
            this.CurrRateTextBox.Attributes.Remove("ReadOnly");
            this.CurrRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
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
            this.SetAttribute();
        }

        public void ShowData()
        {
            FINReceiptNonTradeHd _finReceiptNonTradeHd = this._paymentNonTradeBL.GetSingleFINReceiptNonTradeHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            FINReceiptNonTradeCr _finReceiptNonTradeCr = this._paymentNonTradeBL.GetSingleFINReceiptNonTradeCr(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), "1");
            FINReceiptNonTradeDb _finReceiptNonTradeDb = this._paymentNonTradeBL.GetSingleFINReceiptNonTradeDb(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), "1");

            this.ShowCust();
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finReceiptNonTradeHd.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.PaymentNoTextBox.Text = _finReceiptNonTradeHd.TransNmbr;
            this.FileNmbrTextBox.Text = _finReceiptNonTradeHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_finReceiptNonTradeHd.TransDate);
            if (_finReceiptNonTradeCr == null && _finReceiptNonTradeDb == null)
            {
                this.CustDropDownList.Enabled = true;
                this.CurrCodeDropDownList.Enabled = true;
            }
            else
            {
                this.CustDropDownList.Enabled = false;
                this.CurrCodeDropDownList.Enabled = false;
            }
            this.CustDropDownList.SelectedValue = _finReceiptNonTradeHd.CustCode;
            if (this.CustDropDownList.SelectedValue != "null")
            {
                this.ShowCurrency();
            }
            this.CurrCodeDropDownList.SelectedValue = _finReceiptNonTradeHd.CurrCode;
            string _currHome = _currencyBL.GetCurrDefault();
            if (_currHome == this.CurrCodeDropDownList.SelectedValue)
            {
                this.DisableRate();
            }
            else
            {
                this.EnableRate();
            }
            this.CurrRateTextBox.Text = _finReceiptNonTradeHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _finReceiptNonTradeHd.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINReceiptNonTradeHd _finReceiptNonTradeHd = this._paymentNonTradeBL.GetSingleFINReceiptNonTradeHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finReceiptNonTradeHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finReceiptNonTradeHd.CustCode = this.CustDropDownList.SelectedValue;
            _finReceiptNonTradeHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _finReceiptNonTradeHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finReceiptNonTradeHd.Remark = this.RemarkTextBox.Text;

            _finReceiptNonTradeHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finReceiptNonTradeHd.DatePrep = DateTime.Now;

            bool _result = this._paymentNonTradeBL.EditFINReceiptNonTradeHd(_finReceiptNonTradeHd);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
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

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            FINReceiptNonTradeHd _finReceiptNonTradeHd = this._paymentNonTradeBL.GetSingleFINReceiptNonTradeHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finReceiptNonTradeHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finReceiptNonTradeHd.CustCode = this.CustDropDownList.SelectedValue;
            _finReceiptNonTradeHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _finReceiptNonTradeHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finReceiptNonTradeHd.Remark = this.RemarkTextBox.Text;

            _finReceiptNonTradeHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finReceiptNonTradeHd.DatePrep = DateTime.Now;

            bool _result = this._paymentNonTradeBL.EditFINReceiptNonTradeHd(_finReceiptNonTradeHd);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }
    }
}