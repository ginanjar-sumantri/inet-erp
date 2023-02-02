using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FASales
{
    public partial class FASalesEdit : FASalesBase
    {
        private FASalesBL _faSalesBL = new FASalesBL();
        private TermBL _termBL = new TermBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CustomerBL _customerBL = new CustomerBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
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
                this.TransDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.TransDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.PPNDateLiteral.Text = "<input id='button2' type='button' style='visibility:visible' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = _pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.ShowTermDropdownlist();
                this.ShowCurrencyDropdownlist();
                this.ShowCustomerDropdownlist();

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
            this.TransDateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountBaseTextBox.Attributes.Add("ReadOnly", "True");

            this.PPNPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2"/*this.ppn_date_start.ClientID*/ + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.DiscForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2"/*this.ppn_date_start.ClientID*/ + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ShowTermDropdownlist()
        {
            this.TermDropDownList.DataSource = this._termBL.GetListTermForDDL();
            this.TermDropDownList.DataValueField = "TermCode";
            this.TermDropDownList.DataTextField = "TermName";
            this.TermDropDownList.DataBind();
            this.TermDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowCurrencyDropdownlist()
        {
            this.CurrDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrDropDownList.DataValueField = "CurrCode";
            this.CurrDropDownList.DataTextField = "CurrCode";
            this.CurrDropDownList.DataBind();
            this.CurrDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowCustomerDropdownlist()
        {
            this.CustDropDownList.DataSource = this._customerBL.GetListCustomerForDDL();
            this.CustDropDownList.DataValueField = "CustCode";
            this.CustDropDownList.DataTextField = "CustName";
            this.CustDropDownList.DataBind();
            this.CustDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void CustDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CustDropDownList.SelectedValue != "null")
            {
                string _custContact = _customerBL.GetCustContact(this.CustDropDownList.SelectedValue);
                string _currCust = _customerBL.GetCurr(this.CustDropDownList.SelectedValue);
                string _currCodeHome = _currencyBL.GetCurrDefault();
                byte _decimalPlace = _currencyBL.GetDecimalPlace(_currCust);

                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                this.ForexRateTextBox.Text = _currencyRateBL.GetSingleLatestCurrRate(this.CurrDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.PPNRateTextBox.Text = _currencyRateBL.GetSingleLatestCurrRate(this.CurrDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                this.AttnTextBox.Text = _custContact;
                if (_currCust != "")
                {
                    this.CurrDropDownList.SelectedValue = _currCust;
                }

                if (this.CurrDropDownList.SelectedValue == _currCodeHome)
                {
                    this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.ForexRateTextBox.Text = "1";
                    this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.PPNRateTextBox.Text = "1";
                }
                else
                {
                    this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                    this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                    this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                }
                this.CurrTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrTextBox.Attributes.Add("style", "background-color:#cccccc");
                this.CurrTextBox.Text = CurrDropDownList.SelectedValue;
            }
            else
            {
                this.AttnTextBox.Text = "";
                this.ForexRateTextBox.Text = "0";
                this.PPNRateTextBox.Text = "0";
                this.CurrDropDownList.SelectedValue = "null";
                this.CurrTextBox.Text = "";
            }
        }

        public void ShowData()
        {
            GLFASalesHd _glFASalesHd = this._faSalesBL.GetSingleFASalesHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = _currencyBL.GetDecimalPlace(_glFASalesHd.CurrCode);

            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.TransNoTextBox.Text = _glFASalesHd.TransNmbr;
            this.FileNmbrTextBox.Text = _glFASalesHd.FileNmbr;
            this.TransDateTextBox.Text = DateFormMapper.GetValue(_glFASalesHd.TransDate);
            this.CustDropDownList.SelectedValue = _glFASalesHd.CustCode;
            this.AttnTextBox.Text = _glFASalesHd.Attn;
            this.CurrTextBox.Text = _glFASalesHd.CurrCode;
            this.AmountBaseTextBox.Text = (_glFASalesHd.BaseForex == 0) ? "0" : _glFASalesHd.BaseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.CurrDropDownList.SelectedValue = _glFASalesHd.CurrCode;
            this.ForexRateTextBox.Text = (_glFASalesHd.Forexrate == 0) ? "0" : _glFASalesHd.Forexrate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            //Ini untuk tampilin PPN-Discount
            if (Convert.ToInt32(_glFASalesHd.PPN) == 0)
            {
                this.PPNPercentTextBox.Text = "0";
                this.PPNDateLiteral.Text = "<input id='button2' type='button' style='visibility:hidden' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNNoTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
            }
            else
            {
                this.PPNPercentTextBox.Text = (_glFASalesHd.PPN == 0) ? "0" : _glFASalesHd.PPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                if (_glFASalesHd.PPNDate != null)
                {
                    this.PPNDateTextBox.Text = DateFormMapper.GetValue(_glFASalesHd.PPNDate);
                    this.PPNDateLiteral.Text = "<input id='button2' type='button' style='visibility:visible' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                }
                this.PPNNoTextBox.Attributes.Remove("ReadOnly");
                this.PPNNoTextBox.Attributes.Add("Style", "background-color:#FFFFFF");
                this.PPNNoTextBox.Text = _glFASalesHd.PPNNo;
            }
            //Ini untuk tampilin Discount-FOREX
            if (Convert.ToDouble(_glFASalesHd.DiscForex) == 0)
            {
                this.DiscForexTextBox.Text = "0";
            }
            else
            {
                this.DiscForexTextBox.Text = (_glFASalesHd.DiscForex == 0) ? "0" : _glFASalesHd.DiscForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            this.PPNForexTextBox.Text = (_glFASalesHd.PPNForex == 0) ? "0" : _glFASalesHd.PPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            if (_glFASalesHd.PPNRate != null)
            {
                decimal _ppnRateValue = Convert.ToDecimal((_glFASalesHd.PPNRate == null) ? 0 : _glFASalesHd.PPNRate);
                this.PPNRateTextBox.Text = (_ppnRateValue == 0) ? "0" : _ppnRateValue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            this.RemarkTextBox.Text = _glFASalesHd.Remark;
            this.TermDropDownList.SelectedValue = _glFASalesHd.Term;
            this.TotalForexTextBox.Text = (_glFASalesHd.TotalForex == 0) ? "0" : _glFASalesHd.TotalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            GLFASalesHd _glFASalesHd = this._faSalesBL.GetSingleFASalesHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _glFASalesHd.TransDate = new DateTime(DateFormMapper.GetValue(this.TransDateTextBox.Text).Year, DateFormMapper.GetValue(this.TransDateTextBox.Text).Month, DateFormMapper.GetValue(this.TransDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _glFASalesHd.CustCode = this.CustDropDownList.SelectedValue;
            _glFASalesHd.Attn = this.AttnTextBox.Text;
            _glFASalesHd.Term = this.TermDropDownList.SelectedValue;
            _glFASalesHd.Remark = this.RemarkTextBox.Text;
            _glFASalesHd.DiscForex = Convert.ToDecimal(this.DiscForexTextBox.Text);
            _glFASalesHd.PPN = Convert.ToDecimal(this.PPNPercentTextBox.Text);
            _glFASalesHd.PPNNo = this.PPNNoTextBox.Text;
            _glFASalesHd.PPNForex = (this.PPNForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNForexTextBox.Text);
            _glFASalesHd.TotalForex = (this.TotalForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text);

            if (this.PPNDateTextBox.Text != "")
            {
                _glFASalesHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            _glFASalesHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _glFASalesHd.DatePrep = DateTime.Now;

            bool _result = this._faSalesBL.EditFASalesHd(_glFASalesHd);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
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
            GLFASalesHd _glFASalesHd = this._faSalesBL.GetSingleFASalesHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _glFASalesHd.TransDate = new DateTime(DateFormMapper.GetValue(this.TransDateTextBox.Text).Year, DateFormMapper.GetValue(this.TransDateTextBox.Text).Month, DateFormMapper.GetValue(this.TransDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _glFASalesHd.CustCode = this.CustDropDownList.SelectedValue;
            _glFASalesHd.Attn = this.AttnTextBox.Text;
            _glFASalesHd.Term = this.TermDropDownList.SelectedValue;
            _glFASalesHd.Remark = this.RemarkTextBox.Text;
            _glFASalesHd.DiscForex = Convert.ToDecimal(this.DiscForexTextBox.Text);
            _glFASalesHd.PPN = Convert.ToDecimal(this.PPNPercentTextBox.Text);
            _glFASalesHd.PPNNo = this.PPNNoTextBox.Text;
            _glFASalesHd.PPNForex = (this.PPNForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNForexTextBox.Text);
            _glFASalesHd.TotalForex = (this.TotalForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text);

            if (this.PPNDateTextBox.Text != "")
            {
                _glFASalesHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            _glFASalesHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _glFASalesHd.DatePrep = DateTime.Now;

            bool _result = this._faSalesBL.EditFASalesHd(_glFASalesHd);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }
    }
}