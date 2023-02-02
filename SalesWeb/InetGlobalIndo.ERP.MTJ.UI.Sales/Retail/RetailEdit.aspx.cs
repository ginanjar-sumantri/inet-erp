using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.Retail
{
    public partial class RetailEdit : RetailBase
    {
        private CustomerBL _custBL = new CustomerBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRate = new CurrencyRateBL();
        private EmployeeBL _employeeBL = new EmployeeBL();
        private RetailBL _retailBL = new RetailBL();
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

                this.ShowEmployee();
                this.ShowCurrency();

                this.SetAttribute();

                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrTextBox.Attributes.Add("ReadOnly", "True");
            this.BaseForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");

            this.ForexRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.DiscTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.DiscForexTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.PPNPercentTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");

            this.ForexRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.ForexRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.PPNPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.BaseForexTextBox.ClientID + ", " + this.DiscForexTextBox.ClientID + ", " + this.PPNPercentTextBox.ClientID + ", " + this.PPNForexTextBox.ClientID + ", " + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.AddFeeTextBox.ClientID + ");");
            this.AddFeeTextBox.Attributes.Add("OnBlur", "Calculate(" + this.BaseForexTextBox.ClientID + ", " + this.DiscForexTextBox.ClientID + ", " + this.PPNPercentTextBox.ClientID + ", " + this.PPNForexTextBox.ClientID + ", " + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.AddFeeTextBox.ClientID + ");");

            this.DiscTextBox.Attributes.Add("OnBlur", "CalculateDisc(" + this.BaseForexTextBox.ClientID + ", " + this.DiscTextBox.ClientID + ", " + this.DiscForexTextBox.ClientID + ", " + this.PPNPercentTextBox.ClientID + ", " + this.PPNForexTextBox.ClientID + ", " + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.AddFeeTextBox.ClientID + ");");
            this.DiscForexTextBox.Attributes.Add("OnBlur", "CalculateDiscForex(" + this.BaseForexTextBox.ClientID + ", " + this.DiscTextBox.ClientID + ", " + this.DiscForexTextBox.ClientID + ", " + this.PPNPercentTextBox.ClientID + ", " + this.PPNForexTextBox.ClientID + ", " + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.AddFeeTextBox.ClientID + ");");

            this.PayTypeDropDownList.Attributes.Add("OnChange", "return a(" + this.PayTypeDropDownList.ClientID + ", " + this.CardNoTextBox.ClientID + ", " + this.CardNameTextBox.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowEmployee()
        {
            this.EmpNumbDropDownList.Items.Clear();
            this.EmpNumbDropDownList.DataTextField = "EmpName";
            this.EmpNumbDropDownList.DataValueField = "EmpNumb";
            this.EmpNumbDropDownList.DataSource = this._employeeBL.GetListEmpForDDL();
            this.EmpNumbDropDownList.DataBind();
            this.EmpNumbDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
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
            SAL_TrRetail _sal_TrRetail = this._retailBL.GetSingleSAL_TrRetail(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = _currencyBL.GetDecimalPlace(_sal_TrRetail.CurrCode);

            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.TransNoTextBox.Text = _sal_TrRetail.TransNmbr;
            this.FileNmbrTextBox.Text = _sal_TrRetail.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_sal_TrRetail.TransDate);
            this.CustNameTextBox.Text = _sal_TrRetail.CustName;
            this.EmpNumbDropDownList.SelectedValue = _sal_TrRetail.EmpNumb;
            this.CurrCodeDropDownList.SelectedValue = _sal_TrRetail.CurrCode;
            this.CurrTextBox.Text = _sal_TrRetail.CurrCode;
            this.PayTypeDropDownList.SelectedValue = _sal_TrRetail.PaymentType;
            this.CardNoTextBox.Text = _sal_TrRetail.PaymentCode;
            this.CardNameTextBox.Text = _sal_TrRetail.BankName;

            string _currCodeHome = _currencyBL.GetCurrDefault();

            if (this.CurrCodeDropDownList.SelectedValue == _currCodeHome)
            {
                this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                this.ForexRateTextBox.Text = "1";
            }
            else
            {
                this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
            }

            this.ForexRateTextBox.Text = (_sal_TrRetail.ForexRate == 0) ? "0" : _sal_TrRetail.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TotalForexTextBox.Text = (_sal_TrRetail.TotalAmount == 0) ? "0" : _sal_TrRetail.TotalAmount.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _sal_TrRetail.Remark;
            this.AddFeeTextBox.Text = (_sal_TrRetail.AdditionalFee == 0) ? "0" : _sal_TrRetail.AdditionalFee.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            if (_sal_TrRetail.BaseForex != null)
            {
                this.BaseForexTextBox.Text = (_sal_TrRetail.BaseForex == 0) ? "0" : _sal_TrRetail.BaseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            else
            {
                this.BaseForexTextBox.Text = "0";
            }
            if (_sal_TrRetail.DiscPercent != null)
            {
                this.DiscTextBox.Text = (_sal_TrRetail.DiscPercent == 0) ? "0" : _sal_TrRetail.DiscPercent.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            else
            {
                this.DiscTextBox.Text = "0";
            }
            if (_sal_TrRetail.DiscAmount != null)
            {
                this.DiscForexTextBox.Text = (_sal_TrRetail.DiscAmount == 0) ? "0" : _sal_TrRetail.DiscAmount.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            else
            {
                this.DiscForexTextBox.Text = "0";
            }
            if (_sal_TrRetail.PPNPercent != null)
            {
                this.PPNPercentTextBox.Text = (_sal_TrRetail.PPNPercent == 0) ? "0" : _sal_TrRetail.PPNPercent.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            else
            {
                this.PPNPercentTextBox.Text = "0";
            }
            if (_sal_TrRetail.PPNAmount != null)
            {
                this.PPNForexTextBox.Text = (_sal_TrRetail.PPNAmount == 0) ? "0" : _sal_TrRetail.PPNAmount.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            else
            {
                this.PPNForexTextBox.Text = "0";
            }
        }

        protected void CurrCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrCodeDropDownList.SelectedValue != "null")
            {
                string _currCode = this.CurrCodeDropDownList.SelectedValue;
                string _currCodeHome = _currencyBL.GetCurrDefault();
                byte _decimalPlace = _currencyBL.GetDecimalPlace(this.CurrCodeDropDownList.SelectedValue);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                this.ForexRateTextBox.Text = _currencyRate.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                if (this.CurrCodeDropDownList.SelectedValue == _currCodeHome)
                {
                    this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.ForexRateTextBox.Text = "1";
                }
                else
                {
                    this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                }
                this.CurrTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrTextBox.Attributes.Add("style", "background-color:#cccccc");
                this.CurrTextBox.Text = CurrCodeDropDownList.SelectedValue;
                this.ForexRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.ForexRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            }
            else
            {
                this.ForexRateTextBox.Text = "0";
                this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#ffffff");
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            SAL_TrRetail _sal_TrRetail = this._retailBL.GetSingleSAL_TrRetail(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _sal_TrRetail.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _sal_TrRetail.CustName = this.CustNameTextBox.Text;
            _sal_TrRetail.EmpNumb = this.EmpNumbDropDownList.SelectedValue;
            _sal_TrRetail.PaymentType = this.PayTypeDropDownList.SelectedValue;
            _sal_TrRetail.BankName = this.CardNameTextBox.Text;
            _sal_TrRetail.PaymentCode = this.CardNoTextBox.Text;
            _sal_TrRetail.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _sal_TrRetail.ForexRate = (this.ForexRateTextBox.Text == "") ? 0 : Convert.ToDecimal(this.ForexRateTextBox.Text);
            _sal_TrRetail.BaseForex = (this.BaseForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.BaseForexTextBox.Text);
            _sal_TrRetail.DiscPercent = (this.DiscTextBox.Text == "") ? Convert.ToByte(0) : Convert.ToByte(this.DiscTextBox.Text);
            _sal_TrRetail.DiscAmount = (this.DiscForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.DiscForexTextBox.Text);
            _sal_TrRetail.PPNPercent = (this.PPNPercentTextBox.Text == "") ? Convert.ToByte(0) : Convert.ToByte(this.PPNPercentTextBox.Text);
            _sal_TrRetail.PPNAmount = (this.PPNForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNForexTextBox.Text);
            _sal_TrRetail.TotalAmount = (this.TotalForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text);
            _sal_TrRetail.AdditionalFee = (this.AddFeeTextBox.Text == "") ? 0 : Convert.ToDecimal(this.AddFeeTextBox.Text);
            _sal_TrRetail.Remark = this.RemarkTextBox.Text;

            bool _result = this._retailBL.EditSAL_TrRetail(_sal_TrRetail);

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

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.Page.IsValid == true)
            {
                SAL_TrRetail _sal_TrRetail = this._retailBL.GetSingleSAL_TrRetail(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                _sal_TrRetail.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                _sal_TrRetail.CustName = this.CustNameTextBox.Text;
                _sal_TrRetail.EmpNumb = this.EmpNumbDropDownList.SelectedValue;
                _sal_TrRetail.PaymentType = this.PayTypeDropDownList.SelectedValue;
                _sal_TrRetail.BankName = this.CardNameTextBox.Text;
                _sal_TrRetail.PaymentCode = this.CardNoTextBox.Text;
                _sal_TrRetail.CurrCode = this.CurrCodeDropDownList.SelectedValue;
                _sal_TrRetail.ForexRate = (this.ForexRateTextBox.Text == "") ? 0 : Convert.ToDecimal(this.ForexRateTextBox.Text);
                _sal_TrRetail.BaseForex = (this.BaseForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.BaseForexTextBox.Text);
                _sal_TrRetail.DiscPercent = (this.DiscTextBox.Text == "") ? Convert.ToByte(0) : Convert.ToByte(this.DiscTextBox.Text);
                _sal_TrRetail.DiscAmount = (this.DiscForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.DiscForexTextBox.Text);
                _sal_TrRetail.PPNPercent = (this.PPNPercentTextBox.Text == "") ? Convert.ToByte(0) : Convert.ToByte(this.PPNPercentTextBox.Text);
                _sal_TrRetail.PPNAmount = (this.PPNForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNForexTextBox.Text);
                _sal_TrRetail.TotalAmount = (this.TotalForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text);
                _sal_TrRetail.AdditionalFee = (this.AddFeeTextBox.Text == "") ? 0 : Convert.ToDecimal(this.AddFeeTextBox.Text);
                _sal_TrRetail.Remark = this.RemarkTextBox.Text;

                bool _result = this._retailBL.EditSAL_TrRetail(_sal_TrRetail);

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
}