using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FATenancy
{
    public partial class FATenancyAdd : FATenancyBase
    {
        private FATenancyBL _faTenancyBL = new FATenancyBL();
        private CustomerBL _customerBL = new CustomerBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private TermBL _termBL = new TermBL();
        private PermissionBL _permBL = new PermissionBL();

        //string _imgPPNDate = "ppn_date_start";

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
                this.PPNDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowCustomer();
                this.ShowCurrency();
                this.ShowTerm();

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
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrencyForexTextBox.Attributes.Add("ReadOnly", "True");
            this.BaseForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");

            this.CurrencyRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrencyRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.PPNTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2"/*this.ppn_date_start.ClientID*/ + "," + this.BaseForexTextBox.ClientID + "," + this.DiscountTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.DiscountTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2"/*this.ppn_date_start.ClientID*/ + "," + this.BaseForexTextBox.ClientID + "," + this.DiscountTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.WarningLabel.Text = "";
            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            this.CustCodeDropDownList.SelectedValue = "null";
            this.CurrencyDropDownList.SelectedValue = "null";
            this.CurrencyRateTextBox.Text = "";
            this.TermDropDownList.SelectedValue = "null";
            this.AttnTextBox.Text = "";
            this.CurrencyRateTextBox.Text = "0";
            this.PPNTextBox.Text = "0";
            this.PPNNoTextBox.Text = "";
            this.PPNDateTextBox.Text = "";
            this.PPNRateTextBox.Text = "0";
            this.BaseForexTextBox.Text = "0";
            this.DiscountTextBox.Text = "0";
            this.PPNForexTextBox.Text = "0";
            this.TotalForexTextBox.Text = "0";
            this.RemarkTextBox.Text = "";
            this.DecimalPlaceHiddenField.Value = "";
        }

        public void ShowCustomer()
        {
            this.CustCodeDropDownList.DataTextField = "CustName";
            this.CustCodeDropDownList.DataValueField = "CustCode";
            this.CustCodeDropDownList.DataSource = this._customerBL.GetListCustomerForDDL();
            this.CustCodeDropDownList.DataBind();
            this.CustCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowCurrency()
        {
            this.CurrencyDropDownList.DataTextField = "CurrCode";
            this.CurrencyDropDownList.DataValueField = "CurrCode";
            this.CurrencyDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrencyDropDownList.DataBind();
            this.CurrencyDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowTerm()
        {
            this.TermDropDownList.DataTextField = "TermName";
            this.TermDropDownList.DataValueField = "TermCode";
            this.TermDropDownList.DataSource = this._termBL.GetListTermForDDL();
            this.TermDropDownList.DataBind();
            this.TermDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void CurrencyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrencyDropDownList.SelectedValue != "null")
            {
                string _currCodeHome = _currencyBL.GetCurrDefault();
                byte _decimalPlace = _currencyBL.GetDecimalPlace(this.CurrencyDropDownList.SelectedValue);
                decimal _tempForexRate = _currencyRateBL.GetSingleLatestCurrRate(this.CurrencyDropDownList.SelectedValue);

                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                this.CurrencyRateTextBox.Text = this.PPNRateTextBox.Text = (_tempForexRate == 0) ? "0" : _tempForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                if (this.CurrencyDropDownList.SelectedValue == _currCodeHome)
                {
                    this.CurrencyRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.CurrencyRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.CurrencyRateTextBox.Attributes.Remove("OnBlur");
                    this.CurrencyRateTextBox.Text = "1";

                    this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.PPNRateTextBox.Attributes.Remove("OnBlur");
                    this.PPNRateTextBox.Text = "1";
                }
                else
                {
                    this.CurrencyRateTextBox.Attributes.Remove("ReadOnly");
                    this.CurrencyRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                    this.CurrencyRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrencyRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

                    this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                    this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                    this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
                }

                this.CurrencyForexTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrencyForexTextBox.Attributes.Add("style", "background-color:#cccccc");
                this.CurrencyForexTextBox.Text = CurrencyDropDownList.SelectedValue;
            }
            else
            {
                this.CurrencyRateTextBox.Text = "0";
                this.CurrencyRateTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrencyRateTextBox.Attributes.Add("Style", "Background-Color:#CCCCCC");

                this.PPNRateTextBox.Text = "0";
                this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNRateTextBox.Attributes.Add("Style", "Background-Color:#CCCCCC");
            }
        }

        protected void CustCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _custContact = _customerBL.GetCustContact(this.CustCodeDropDownList.SelectedValue);
            this.AttnTextBox.Text = _custContact;

            if (this.CustCodeDropDownList.SelectedValue != "null")
            {
                this.ChangeCurr();
            }
            else
            {
                this.CurrencyDropDownList.SelectedValue = "null";
                this.CurrencyRateTextBox.Text = "0";
                this.PPNRateTextBox.Text = "0";
                this.CurrencyRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrencyRateTextBox.Attributes.Add("style", "background-color:#ffffff");
            }
        }

        public void ChangeCurr()
        {
            string _currCode = this._customerBL.GetCurr(CustCodeDropDownList.SelectedValue);
            byte _decimalPlace = _currencyBL.GetDecimalPlace(_currCode);

            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.CurrencyDropDownList.SelectedValue = _currCode;
            this.CurrencyForexTextBox.Text = _currCode;

            decimal _currRate = this._currencyRateBL.GetSingleLatestCurrRate(_currCode);
            if (_currRate != 0)
            {
                this.CurrencyRateTextBox.Text = _currRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.PPNForexTextBox.Text = _currRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.CurrencyRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrencyRateTextBox.Attributes.Add("style", "background-color:#ffffff");
            }
            else
            {
                this.CurrencyRateTextBox.Text = "1";
                this.PPNRateTextBox.Text = "1";
                this.CurrencyRateTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrencyRateTextBox.Attributes.Add("style", "background-color:#cccccc");
            }
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            GLFATenancyHd _glFATenancyHd = new GLFATenancyHd();

            _glFATenancyHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _glFATenancyHd.Status = FixedAssetStatus.GetStatus(TransStatus.OnHold);
            _glFATenancyHd.CustCode = this.CustCodeDropDownList.SelectedValue;
            _glFATenancyHd.CurrCode = this.CurrencyDropDownList.SelectedValue;
            _glFATenancyHd.ForexRate = Convert.ToDecimal(this.CurrencyRateTextBox.Text);
            _glFATenancyHd.Term = this.TermDropDownList.SelectedValue;
            _glFATenancyHd.Attn = this.AttnTextBox.Text;
            _glFATenancyHd.PPN = Convert.ToDecimal(this.PPNTextBox.Text);
            _glFATenancyHd.PPNNo = this.PPNNoTextBox.Text;
            if (this.PPNDateTextBox.Text != "")
            {
                _glFATenancyHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            else
            {
                _glFATenancyHd.PPNDate = null;
            }
            _glFATenancyHd.PPNRate = (this.PPNRateTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNRateTextBox.Text);
            _glFATenancyHd.BaseForex = Convert.ToDecimal(this.BaseForexTextBox.Text);
            _glFATenancyHd.DiscForex = Convert.ToDecimal(this.DiscountTextBox.Text);
            _glFATenancyHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _glFATenancyHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            _glFATenancyHd.Remark = this.DiscountTextBox.Text;

            _glFATenancyHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _glFATenancyHd.DatePrep = DateTime.Now;

            string _result = this._faTenancyBL.AddGLFATenancyHd(_glFATenancyHd);

            if (_result != "")
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
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
    }
}