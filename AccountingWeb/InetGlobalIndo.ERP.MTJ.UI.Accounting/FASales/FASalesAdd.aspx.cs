using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FASales
{
    public partial class FASalesAdd : FASalesBase
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

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.PPNDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.TransDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.TransDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                this.PageTitleLiteral.Text = _pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowTermDropdownlist();
                this.ShowCurrencyDropdownlist();
                this.ShowCustomerDropdownlist();

                this.ClearLabel();
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
            this.TransDateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountBaseTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");

            this.ForexRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.PPNRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.PPNPercentTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.DiscForexTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");

            this.ForexRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.ForexRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.PPNPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2"/*this.ppn_date_start.ClientID*/ + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.DiscForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2"/*this.ppn_date_start.ClientID*/ + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearData()
        {
            this.TransDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.PPNPercentTextBox.Text = "0";
            this.AmountBaseTextBox.Text = "0";
            this.DiscForexTextBox.Text = "0";
            this.PPNForexTextBox.Text = "0";
            this.TotalForexTextBox.Text = "0";
            this.TermDropDownList.SelectedValue = "null";
            this.CurrDropDownList.SelectedValue = "null";
            this.CustDropDownList.SelectedValue = "null";
            this.AttnTextBox.Text = "";
            this.CurrTextBox.Text = "";
            this.PPNDateTextBox.Text = "";
            this.ForexRateTextBox.Text = "0";
            this.PPNNoTextBox.Text = "";
            this.PPNRateTextBox.Text = "";
            this.RemarkTextBox.Text = "";
            this.DecimalPlaceHiddenField.Value = "";
        }

        protected void CurrDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CurrDropDownList.SelectedValue != "null")
            {
                string _currCodeHome = _currencyBL.GetCurrDefault();
                byte _decimalPlace = _currencyBL.GetDecimalPlace(this.CurrDropDownList.SelectedValue);

                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                this.ForexRateTextBox.Text = _currencyRateBL.GetSingleLatestCurrRate(this.CurrDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.PPNRateTextBox.Text = _currencyRateBL.GetSingleLatestCurrRate(this.CurrDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

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
                this.ForexRateTextBox.Text = "0";
                this.PPNRateTextBox.Text = "0";
                this.CurrDropDownList.SelectedValue = "null";
                this.CurrTextBox.Text = "";
            }
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

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            GLFASalesHd _glFASalesHd = new GLFASalesHd();

            _glFASalesHd.TransDate = new DateTime(DateFormMapper.GetValue(this.TransDateTextBox.Text).Year, DateFormMapper.GetValue(this.TransDateTextBox.Text).Month, DateFormMapper.GetValue(this.TransDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _glFASalesHd.Status = FixedAssetStatus.GetStatus(TransStatus.OnHold);
            _glFASalesHd.CustCode = this.CustDropDownList.SelectedValue;
            _glFASalesHd.Attn = this.AttnTextBox.Text;
            _glFASalesHd.BaseForex = Convert.ToDecimal(this.AmountBaseTextBox.Text);
            _glFASalesHd.CurrCode = this.CurrDropDownList.SelectedValue;
            _glFASalesHd.Forexrate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _glFASalesHd.PPN = Convert.ToDecimal(this.PPNPercentTextBox.Text);
            _glFASalesHd.DiscForex = Convert.ToDecimal(this.DiscForexTextBox.Text);
            if (this.PPNDateTextBox.Text != "")
            {
                _glFASalesHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            _glFASalesHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _glFASalesHd.PPNNo = this.PPNNoTextBox.Text;
            if (this.PPNRateTextBox.Text != "")
            {
                _glFASalesHd.PPNRate = Convert.ToDecimal(this.PPNRateTextBox.Text);
            }
            _glFASalesHd.Remark = this.RemarkTextBox.Text;
            _glFASalesHd.Term = this.TermDropDownList.SelectedValue;
            _glFASalesHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);

            _glFASalesHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _glFASalesHd.DatePrep = DateTime.Now;

            string _result = this._faSalesBL.AddFASalesHd(_glFASalesHd);

            if (_result != "")
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }
    }
}