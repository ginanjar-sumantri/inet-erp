using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.RequestSalesRetur
{
    public partial class RequestSalesReturEdit : RequestSalesReturBase
    {
        private CustomerBL _custBL = new CustomerBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private RequestSalesReturBL _requestSalesReturBL = new RequestSalesReturBL();
        private BillOfLadingBL _billOfLadingBL = new BillOfLadingBL();
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
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.CalendarScriptLiteral.Text = "<input type='button' id='date_start' value='...' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' />";

                this.ShowCurrency();

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
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");

            this.CurrRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
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

        public void ShowData()
        {
            MKTReqReturHd _mktReqReturHd = this._requestSalesReturBL.GetSingleMKTReqReturHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = _currencyBL.GetDecimalPlace(_mktReqReturHd.CurrCode);

            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.TransNoTextBox.Text = _mktReqReturHd.TransNmbr;
            this.FileNoTextBox.Text = _mktReqReturHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_mktReqReturHd.TransDate);
            this.CustTextBox.Text = _custBL.GetNameByCode(_mktReqReturHd.CustCode);
            this.UseReferenceTextBox.Text = ((_mktReqReturHd.UseReferense.ToString() == "Y") ? "Yes" : "No");
            if (_mktReqReturHd.UseReferense.ToString() == "Y")
            {
                this.ReferenceTable.Visible = true;
                //this.TransTypeTextBox.Text = _mktReqReturHd.TransType;
                this.SuratJalanTextBox.Text = _mktReqReturHd.SJNo;// _billOfLadingBL.GetFileNmbrFromSTCSJHd(_mktReqReturHd.SJNo);
            }
            else
            {
                this.ReferenceTable.Visible = false;
                //this.TransTypeTextBox.Text = "";
                this.SuratJalanTextBox.Text = "";
            }

            this.CurrCodeDropDownList.SelectedValue = _mktReqReturHd.CurrCode;
            this.DeliveryBackTextBox.Text = ((_mktReqReturHd.FgDeliveryBack.ToString() == "Y") ? "YES" : "NO");
            //this.ProductScrapTextBox.Text = ((_mktReqReturHd.ProductScrap.ToString() == "Y") ? "YES" : "NO");
            if (this.CurrCodeDropDownList.SelectedValue == _currencyBL.GetCurrDefault())
            {
                this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            }
            else
            {
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
            }
            this.CurrRateTextBox.Text = (_mktReqReturHd.ForexRate == 0) ? "0" : Convert.ToDecimal(_mktReqReturHd.ForexRate).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TotalForexTextBox.Text = (_mktReqReturHd.TotalForex == 0) ? "0" : Convert.ToDecimal(_mktReqReturHd.TotalForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AttnTextBox.Text = _mktReqReturHd.Attn;
            this.RemarkTextBox.Text = _mktReqReturHd.Remark;

        }

        protected void CurrCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrCodeDropDownList.SelectedValue != "null")
            {
                string _currCodeHome = _currencyBL.GetCurrDefault();
                byte _decimalPlace = _currencyBL.GetDecimalPlace(this.CurrCodeDropDownList.SelectedValue);
                this.CurrRateTextBox.Text = _currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                if (this.CurrCodeDropDownList.SelectedValue == _currCodeHome)
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
                this.CurrRateTextBox.Text = "0";
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#ffffff");
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MKTReqReturHd _mktReqReturHd = this._requestSalesReturBL.GetSingleMKTReqReturHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _mktReqReturHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _mktReqReturHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _mktReqReturHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _mktReqReturHd.Remark = this.RemarkTextBox.Text;
            _mktReqReturHd.Attn = this.AttnTextBox.Text;
            _mktReqReturHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            _mktReqReturHd.EditBy = HttpContext.Current.User.Identity.Name;
            _mktReqReturHd.EditDate = DateTime.Now;

            bool _result = this._requestSalesReturBL.EditMKTReqReturHd(_mktReqReturHd);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBox.Text, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
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
            MKTReqReturHd _mktReqReturHd = this._requestSalesReturBL.GetSingleMKTReqReturHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _mktReqReturHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _mktReqReturHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _mktReqReturHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _mktReqReturHd.Remark = this.RemarkTextBox.Text;
            _mktReqReturHd.Attn = this.AttnTextBox.Text;
            _mktReqReturHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            _mktReqReturHd.EditBy = HttpContext.Current.User.Identity.Name;
            _mktReqReturHd.EditDate = DateTime.Now;

            bool _result = this._requestSalesReturBL.EditMKTReqReturHd(_mktReqReturHd);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }
    }
}