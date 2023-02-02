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
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PaymentNonPurchase
{
    public partial class PaymentNonPurchaseEdit : PaymentNonPurchaseBase
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
                this.SetAttribute();
                this.ShowData();
            }
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
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

        protected void CurrCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrCodeDropDownList.SelectedValue != "null")
            {
                byte _decimalPlace = _currencyBL.GetDecimalPlace(this.CurrCodeDropDownList.SelectedValue);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                decimal _currRate = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue);
                if (_currRate != 0)
                {
                    this.CurrRateTextBox.Text = _currRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                    this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                    this.CurrRateTextBox.Attributes.Add("style", "background-color:#ffffff");
                }
                else
                {
                    this.CurrRateTextBox.Text = "1";
                    this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.CurrRateTextBox.Attributes.Add("style", "background-color:#cccccc");
                }
            }
            else
            {
                this.CurrRateTextBox.Text = "0";
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#ffffff");
            }
        }

        public void ShowData()
        {
            FINPayNonTradeHd _finPayNonTradeHd = this._paymentNonPurchaseBL.GetSingleFINPayNonTradeHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            FINPayNonTradeCr _finPayNonTradeCr = this._paymentNonPurchaseBL.GetSingleFINPayNonTradeCr(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), "1");
            FINPayNonTradeDb _finPayNonTradeDb = this._paymentNonPurchaseBL.GetSingleFINPayNonTradeDb(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), "1");

            this.ShowSupplier();
            byte _decimalPlace = _currencyBL.GetDecimalPlace(_finPayNonTradeHd.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.PaymentNoTextBox.Text = _finPayNonTradeHd.TransNmbr;
            this.FileNmbrTextBox.Text = _finPayNonTradeHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_finPayNonTradeHd.TransDate);
            if (_finPayNonTradeCr == null && _finPayNonTradeDb == null)
            {
                this.SuppNameDropDownList.Enabled = true;
                this.CurrCodeDropDownList.Enabled = true;
            }
            else
            {
                this.SuppNameDropDownList.Enabled = false;
                this.CurrCodeDropDownList.Enabled = false;
            }
            this.SuppNameDropDownList.SelectedValue = _finPayNonTradeHd.SuppCode;
            if (SuppNameDropDownList.SelectedValue != "null")
            {
                this.ShowCurrency();
            }
            this.CurrCodeDropDownList.SelectedValue = _finPayNonTradeHd.CurrCode;
            string _currHome = _currencyBL.GetCurrDefault();
            if (_currHome == this.CurrCodeDropDownList.SelectedValue)
            {
                this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrRateTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
            }
            else
            {
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("Style", "background-color:#FFFFFF");
            }
            this.CurrRateTextBox.Text = _finPayNonTradeHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _finPayNonTradeHd.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINPayNonTradeHd _finPayNonTradeHd = this._paymentNonPurchaseBL.GetSingleFINPayNonTradeHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finPayNonTradeHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finPayNonTradeHd.SuppCode = this.SuppNameDropDownList.SelectedValue;
            _finPayNonTradeHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _finPayNonTradeHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finPayNonTradeHd.Remark = this.RemarkTextBox.Text;

            _finPayNonTradeHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finPayNonTradeHd.DatePrep = DateTime.Now;

            bool _result = this._paymentNonPurchaseBL.EditFINPayNonTradeHd(_finPayNonTradeHd);

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
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            FINPayNonTradeHd _finPayNonTradeHd = this._paymentNonPurchaseBL.GetSingleFINPayNonTradeHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finPayNonTradeHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finPayNonTradeHd.SuppCode = this.SuppNameDropDownList.SelectedValue;
            _finPayNonTradeHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _finPayNonTradeHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finPayNonTradeHd.Remark = this.RemarkTextBox.Text;

            _finPayNonTradeHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finPayNonTradeHd.DatePrep = DateTime.Now;

            bool _result = this._paymentNonPurchaseBL.EditFINPayNonTradeHd(_finPayNonTradeHd);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void SuppNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SuppNameDropDownList.SelectedValue != "null")
            {
                this.ShowCurrency();

                string _currCode = this._supplierBL.GetCurr(this.SuppNameDropDownList.SelectedValue);
                byte _decimalPlace = _currencyBL.GetDecimalPlace(_currCode);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                this.CurrRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                if (_currCode != "")
                {
                    this.CurrCodeDropDownList.SelectedValue = _currCode;
                }

                if (this.CurrCodeDropDownList.SelectedValue == _currencyBL.GetCurrDefault())
                {
                    this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.CurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.CurrRateTextBox.Text = "1";
                }
                else
                {
                    this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                    this.CurrRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                }
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
    }
}