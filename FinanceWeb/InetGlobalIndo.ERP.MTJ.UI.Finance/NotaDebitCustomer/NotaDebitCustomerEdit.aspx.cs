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
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.NotaDebitCustomer
{
    public partial class NotaDebitCustomerEdit : NotaDebitCustomerBase
    {
        private CustomerBL _custBL = new CustomerBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private TermBL _termBL = new TermBL();
        private NotaDebitCustomerBL _notaDebitCustomerBL = new NotaDebitCustomerBL();
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
                this.PPNDateLiteral.Text = "<input id='button2' type='button' style='visibility:visible' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.ShowCust();
                this.ShowTerm();
                this.ShowBillTo();
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
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountBaseTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");

            this.CurrRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.PPNRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");

            this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.PPNPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2"/*this.ppn_date_start.ClientID*/ + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.DiscForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2"/*this.ppn_date_start.ClientID*/+ "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

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

        public void ShowTerm()
        {
            this.TermDropDownList.Items.Clear();
            this.TermDropDownList.DataTextField = "TermName";
            this.TermDropDownList.DataValueField = "TermCode";
            this.TermDropDownList.DataSource = this._termBL.GetListTermForDDL();
            this.TermDropDownList.DataBind();
            this.TermDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowBillTo()
        {
            this.BillToDropDownList.Items.Clear();
            this.BillToDropDownList.DataTextField = "CustName";
            this.BillToDropDownList.DataValueField = "CustCode";
            this.BillToDropDownList.DataSource = this._custBL.GetListCustomerForDDL();
            this.BillToDropDownList.DataBind();
            this.BillToDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
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
            FINDNCustHd _finDNCustHd = this._notaDebitCustomerBL.GetSingleFINDNCustHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = _currencyBL.GetDecimalPlace(_finDNCustHd.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.TransNoTextBox.Text = _finDNCustHd.TransNmbr;
            this.FileNmbrTextBox.Text = _finDNCustHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_finDNCustHd.TransDate);
            this.CustDropDownList.SelectedValue = _finDNCustHd.CustCode;
            this.CurrCodeDropDownList.SelectedValue = _finDNCustHd.CurrCode;
            string _currHome = _currencyBL.GetCurrDefault();
            if (_currHome == this.CurrCodeDropDownList.SelectedValue)
            {
                this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrRateTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
                this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNRateTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
            }
            else
            {
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("Style", "background-color:#FFFFFF");
                this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                this.PPNRateTextBox.Attributes.Add("Style", "background-color:#FFFFFF");
            }
            this.CurrRateTextBox.Text = _finDNCustHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            decimal _totalForexValue = Convert.ToDecimal((_finDNCustHd.TotalForex == null) ? 0 : _finDNCustHd.TotalForex);
            this.TotalForexTextBox.Text = (_totalForexValue == 0) ? "0" : _totalForexValue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TermDropDownList.SelectedValue = _finDNCustHd.Term;
            this.CurrTextBox.Text = _finDNCustHd.CurrCode;
            this.AttnTextBox.Text = _finDNCustHd.Attn;
            this.RemarkTextBox.Text = _finDNCustHd.Remark;
            this.BillToDropDownList.SelectedValue = _finDNCustHd.BillTo;

            if (_finDNCustHd.PPN != null && _finDNCustHd.PPN != 0)
            {
                //this.ppn_date_start.Attributes.Add("Style", "visibility:visible");
                this.PPNDateLiteral.Text = "<input id='button2' type='button' style='visibility:visible' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.PPNNoTextBox.Text = _finDNCustHd.PPNNo;
                decimal _ppnValue = Convert.ToDecimal((_finDNCustHd.PPN == null) ? 0 : _finDNCustHd.PPN);
                this.PPNPercentTextBox.Text = (_ppnValue == 0) ? "0" : _ppnValue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                if (_finDNCustHd.PPNRate != null)
                {
                    decimal _ppnRateValue = Convert.ToDecimal((_finDNCustHd.PPNRate == null) ? 0 : _finDNCustHd.PPNRate);
                    this.PPNRateTextBox.Text = (_ppnRateValue == 0) ? "0" : _ppnRateValue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                }
            }
            else
            {
                this.PPNNoTextBox.Text = "";
                //this.ppn_date_start.Attributes.Add("Style", "visibility:hidden");
                this.PPNDateLiteral.Text = "<input id='button2' type='button' style='visibility:hidden' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.PPNPercentTextBox.Text = "0";
                this.PPNRateTextBox.Text = "";
            }
            if (_finDNCustHd.PPNForex != null)
            {
                decimal _ppnForexValue = Convert.ToDecimal((_finDNCustHd.PPNForex == null) ? 0 : _finDNCustHd.PPNForex);
                this.PPNForexTextBox.Text = (_ppnForexValue == 0) ? "0" : _ppnForexValue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            if (_finDNCustHd.PPNDate != null)
            {
                this.PPNDateTextBox.Text = DateFormMapper.GetValue((DateTime)_finDNCustHd.PPNDate);
            }
            if (_finDNCustHd.BaseForex != null)
            {
                decimal _baseForex = Convert.ToDecimal((_finDNCustHd.BaseForex == null) ? 0 : _finDNCustHd.BaseForex);
                this.AmountBaseTextBox.Text = (_baseForex == 0) ? "0" : _baseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            if (_finDNCustHd.DiscForex != null)
            {
                decimal _discForex = Convert.ToDecimal((_finDNCustHd.DiscForex == null) ? 0 : _finDNCustHd.DiscForex);
                this.DiscForexTextBox.Text = (_discForex == 0) ? "0" : _discForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            this.CustPONoTextBox.Text = _finDNCustHd.CustPONo;
        }

        protected void CustDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CustDropDownList.SelectedValue != "null")
            {
                this.AttnTextBox.Text = _custBL.GetCustContact(this.CustDropDownList.SelectedValue);
                string _currCode = this._custBL.GetCurr(CustDropDownList.SelectedValue);
                string _termBLCode = this._custBL.GetTerm(CustDropDownList.SelectedValue);
                string _billTo = this._custBL.GetBillTo(CustDropDownList.SelectedValue);
                byte _decimalPlace = _currencyBL.GetDecimalPlace(_currCode);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                if (_currCode != "")
                {
                    this.CurrCodeDropDownList.SelectedValue = _currCode;
                }
                this.CurrRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.PPNRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                if (_termBLCode != "")
                {
                    this.TermDropDownList.SelectedValue = _termBLCode;
                }
                if (_billTo != "")
                {
                    this.BillToDropDownList.SelectedValue = _billTo;
                }
                else
                {
                    this.BillToDropDownList.SelectedValue = "null";
                }

                if (this.CurrCodeDropDownList.SelectedValue == _currencyBL.GetCurrDefault())
                {
                    this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.CurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.CurrRateTextBox.Text = "1";
                    this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.PPNRateTextBox.Text = "1";
                }
                else
                {
                    this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                    this.CurrRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                    this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                    this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                }
                this.CurrTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrTextBox.Attributes.Add("style", "background-color:#cccccc");
                this.CurrTextBox.Text = CurrCodeDropDownList.SelectedValue;
                this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
                this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            }
            else
            {
                this.AttnTextBox.Text = "";
                this.CurrTextBox.Text = "";
                this.CurrCodeDropDownList.SelectedValue = "null";
                this.TermDropDownList.SelectedValue = "null";
                this.BillToDropDownList.SelectedValue = "null";
                this.CurrRateTextBox.Text = "0";
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#ffffff");
                this.PPNRateTextBox.Text = "0";
                this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
            }
        }

        protected void CurrCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrCodeDropDownList.SelectedValue != "null")
            {
                string _currCode = this._custBL.GetCurr(CustDropDownList.SelectedValue);
                string _currCodeHome = _currencyBL.GetCurrDefault();
                byte _decimalPlace = _currencyBL.GetDecimalPlace(this.CurrCodeDropDownList.SelectedValue);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                this.CurrRateTextBox.Text = _currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.PPNRateTextBox.Text = _currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                if (this.CurrCodeDropDownList.SelectedValue == _currCodeHome)
                {
                    this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.CurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.CurrRateTextBox.Text = "1";
                    this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.PPNRateTextBox.Text = "1";
                }
                else
                {
                    this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                    this.CurrRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                    this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                    this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                }
                this.CurrTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrTextBox.Attributes.Add("style", "background-color:#cccccc");
                this.CurrTextBox.Text = CurrCodeDropDownList.SelectedValue;
                this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
                this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            }
            else
            {
                this.CurrRateTextBox.Text = "0";
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#ffffff");
                this.PPNRateTextBox.Text = "0";
                this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINDNCustHd _finDNCustHd = this._notaDebitCustomerBL.GetSingleFINDNCustHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finDNCustHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finDNCustHd.CustCode = this.CustDropDownList.SelectedValue;
            _finDNCustHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _finDNCustHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finDNCustHd.PPN = (this.PPNPercentTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNPercentTextBox.Text);
            if (this.PPNDateTextBox.Text != "")
            {
                _finDNCustHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            else
            {
                _finDNCustHd.PPNDate = null;
            }
            _finDNCustHd.PPNForex = (this.PPNForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNForexTextBox.Text);
            _finDNCustHd.PPNNo = this.PPNNoTextBox.Text;
            _finDNCustHd.PPNRate = (this.PPNRateTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNRateTextBox.Text);
            _finDNCustHd.BillTo = this.BillToDropDownList.SelectedValue;
            _finDNCustHd.CustPONo = this.CustPONoTextBox.Text;
            _finDNCustHd.Remark = this.RemarkTextBox.Text;
            _finDNCustHd.Attn = this.AttnTextBox.Text;
            _finDNCustHd.BaseForex = Convert.ToDecimal(this.AmountBaseTextBox.Text);
            _finDNCustHd.DiscForex = (this.DiscForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.DiscForexTextBox.Text);
            _finDNCustHd.TotalForex = (this.TotalForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text);
            _finDNCustHd.Term = this.TermDropDownList.SelectedValue;
            _finDNCustHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finDNCustHd.DatePrep = DateTime.Now;

            bool _result = this._notaDebitCustomerBL.EditFINDNCustHd(_finDNCustHd);

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
            FINDNCustHd _finDNCustHd = this._notaDebitCustomerBL.GetSingleFINDNCustHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finDNCustHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finDNCustHd.CustCode = this.CustDropDownList.SelectedValue;
            _finDNCustHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _finDNCustHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finDNCustHd.PPN = (this.PPNPercentTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNPercentTextBox.Text);
            if (this.PPNDateTextBox.Text != "")
            {
                _finDNCustHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            else
            {
                _finDNCustHd.PPNDate = null;
            }
            _finDNCustHd.PPNForex = (this.PPNForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNForexTextBox.Text);
            _finDNCustHd.PPNNo = this.PPNNoTextBox.Text;
            _finDNCustHd.PPNRate = (this.PPNRateTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNRateTextBox.Text);
            _finDNCustHd.BillTo = this.BillToDropDownList.SelectedValue;
            _finDNCustHd.CustPONo = this.CustPONoTextBox.Text;
            _finDNCustHd.Remark = this.RemarkTextBox.Text;
            _finDNCustHd.Attn = this.AttnTextBox.Text;
            _finDNCustHd.BaseForex = Convert.ToDecimal(this.AmountBaseTextBox.Text);
            _finDNCustHd.DiscForex = (this.DiscForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.DiscForexTextBox.Text);
            _finDNCustHd.TotalForex = (this.TotalForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text);
            _finDNCustHd.Term = this.TermDropDownList.SelectedValue;
            _finDNCustHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finDNCustHd.DatePrep = DateTime.Now;

            bool _result = this._notaDebitCustomerBL.EditFINDNCustHd(_finDNCustHd);

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