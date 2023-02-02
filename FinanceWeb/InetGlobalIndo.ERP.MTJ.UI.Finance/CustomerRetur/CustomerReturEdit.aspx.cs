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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.CustomerRetur
{
    public partial class CustomerReturEdit : CustomerReturBase
    {
        private CustomerBL _custBL = new CustomerBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private TermBL _termBL = new TermBL();
        private CustomerReturBL _customerReturBL = new CustomerReturBL();
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
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.PPNDateLiteral.Text = "<input id='button2' Style = 'visibility:visible' type='button' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.ClearLabel();
                this.SetAttribute();
                this.ShowCust();
                this.ShowTerm();
                this.ShowBillTo();
                this.ShowCurrency();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void SetAttributeRate()
        {
            this.CurrRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.PPNRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            
            this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.PPNPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2" /*this.ppn_date_start.ClientID*/ + "," + this.AmountBaseTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + ","+ this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            //this.DiscForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2" /*this.ppn_date_start.ClientID*/ + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.OtherForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            //this.OtherForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2" /*this.ppn_date_start.ClientID*/ + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.OtherForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            //this.PPNPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + this.PPNDateLiteral.ClientID + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.OtherForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            //this.DiscForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + this.PPNDateLiteral.ClientID + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.OtherForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            //this.OtherForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + this.PPNDateLiteral.ClientID + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.OtherForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        public void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountBaseTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");

            this.PPNPercentTextBox.Text = "0";
            this.AmountBaseTextBox.Text = "0";
            //this.DiscForexTextBox.Text = "0";
            this.PPNForexTextBox.Text = "0";
            this.TotalForexTextBox.Text = "0";

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.SetAttributeRate();
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
            FINCustReturHd _finCustReturHd = this._customerReturBL.GetSingleFINCustReturHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finCustReturHd.CurrCode);

            this.TransNoTextBox.Text = _finCustReturHd.TransNmbr;
            this.FileNmbrTextBox.Text = _finCustReturHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_finCustReturHd.TransDate);
            this.CustDropDownList.SelectedValue = _finCustReturHd.CustCode;
            this.CurrCodeDropDownList.SelectedValue = _finCustReturHd.CurrCode;
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            //this.OtherForexTextBox.Text = (_finCustReturHd.OtherForex == 0) ? "0" : _finCustReturHd.OtherForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TotalForexTextBox.Text = (((_finCustReturHd.TotalForex == null) ? 0 : Convert.ToDecimal(_finCustReturHd.TotalForex)) == 0) ? "0" : Convert.ToDecimal(_finCustReturHd.TotalForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TermDropDownList.SelectedValue = _finCustReturHd.Term;
            this.CurrTextBox.Text = _finCustReturHd.CurrCode;
            if (_finCustReturHd.CurrCode == _currencyBL.GetCurrDefault())
            {
                this.DisableRate();
            }
            else
            {
                this.EnableRate();
                this.CurrRateTextBox.Text = (_finCustReturHd.ForexRate == 0) ? "0" : _finCustReturHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.PPNRateTextBox.Text = (_finCustReturHd.PPNRate == null || _finCustReturHd.PPNRate == 0) ? "0" : Convert.ToDecimal(_finCustReturHd.PPNRate).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            this.AttnTextBox.Text = _finCustReturHd.Attn;
            this.RemarkTextBox.Text = _finCustReturHd.Remark;
            this.BillToDropDownList.SelectedValue = _finCustReturHd.BillTo;
            this.PPNPercentTextBox.Text = (_finCustReturHd.PPN == 0) ? "0" : Convert.ToDecimal(_finCustReturHd.PPN).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            if (_finCustReturHd.PPN != 0)
            {
                this.PPNDateLiteral.Text = "<input id='button2' Style = 'visibility:visible' type='button' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PPNNoTextBox.Text = _finCustReturHd.PPNNo;
                this.PPNNoTextBox.Attributes.Remove("ReadOnly");
                this.PPNNoTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                this.PPNDateTextBox.Text = (_finCustReturHd.PPNDate == null) ? "" : DateFormMapper.GetValue((DateTime)_finCustReturHd.PPNDate);
               // this.PPNDateLiteral.Visible = true;
            }
            else
            {
                this.PPNDateLiteral.Text = "<input id='button2' Style = 'visibility:hidden' type='button' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PPNNoTextBox.Text = "";
                this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNNoTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                this.PPNDateTextBox.Text = "";
                //this.PPNDateLiteral.Visible = false;
            }
            this.PPNForexTextBox.Text = (_finCustReturHd.PPNForex == 0) ? "0" : Convert.ToDecimal(_finCustReturHd.PPNForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountBaseTextBox.Text = (_finCustReturHd.BaseForex == null || _finCustReturHd.BaseForex == 0) ? "0" : Convert.ToDecimal(_finCustReturHd.BaseForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            //this.DiscForexTextBox.Text = (_finCustReturHd.DiscForex == null || _finCustReturHd.DiscForex == 0) ? "0" : Convert.ToDecimal(_finCustReturHd.DiscForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            
        }

        protected void CustDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearDataNumeric();

            if (this.CustDropDownList.SelectedValue != "null")
            {
                this.AttnTextBox.Text = _custBL.GetCustContact(this.CustDropDownList.SelectedValue);
                string _currCode = this._custBL.GetCurr(CustDropDownList.SelectedValue);
                string _termCode = this._custBL.GetTerm(CustDropDownList.SelectedValue);
                string _billTo = this._custBL.GetBillTo(CustDropDownList.SelectedValue);

                if (_termCode != "")
                {
                    this.TermDropDownList.SelectedValue = _termCode;
                }
                if (_billTo != "")
                {
                    this.BillToDropDownList.SelectedValue = _billTo;
                }
                else
                {
                    this.BillToDropDownList.SelectedValue = "null";
                }

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

        }

        protected void CurrCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrCodeDropDownList.SelectedValue != "null")
            {
                this.SetCurrRate();

            }

            this.SetAttributeRate();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINCustReturHd _FINCustReturHd = this._customerReturBL.GetSingleFINCustReturHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _FINCustReturHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _FINCustReturHd.CustCode = this.CustDropDownList.SelectedValue;
            _FINCustReturHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _FINCustReturHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _FINCustReturHd.PPN = Convert.ToDecimal(this.PPNPercentTextBox.Text);
            if (this.PPNDateTextBox.Text != "")
            {
                _FINCustReturHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            else
            {
                _FINCustReturHd.PPNDate = null;
            }
            _FINCustReturHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _FINCustReturHd.PPNNo = this.PPNNoTextBox.Text;
            _FINCustReturHd.PPNRate = Convert.ToDecimal(this.PPNRateTextBox.Text);
            _FINCustReturHd.BillTo = this.BillToDropDownList.SelectedValue;
            _FINCustReturHd.Remark = this.RemarkTextBox.Text;
            _FINCustReturHd.Attn = this.AttnTextBox.Text;
            _FINCustReturHd.BaseForex = (this.AmountBaseTextBox.Text == "") ? 0 : Convert.ToDecimal(this.AmountBaseTextBox.Text);
            //_FINCustReturHd.DiscForex = (this.DiscForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.DiscForexTextBox.Text);
            _FINCustReturHd.TotalForex = (this.TotalForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text);
            //_FINCustReturHd.OtherForex = (this.OtherForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.OtherForexTextBox.Text);
            _FINCustReturHd.Term = this.TermDropDownList.SelectedValue;
            _FINCustReturHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _FINCustReturHd.DatePrep = DateTime.Now;

            bool _result = this._customerReturBL.EditFINCustReturHd(_FINCustReturHd);

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
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            FINCustReturHd _FINCustReturHd = this._customerReturBL.GetSingleFINCustReturHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _FINCustReturHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            //_FINCustReturHd.Status = CustomerNoteDataMapper.GetStatus(CustomerNoteStatus.OnHold);
            _FINCustReturHd.CustCode = this.CustDropDownList.SelectedValue;
            _FINCustReturHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _FINCustReturHd.ForexRate = (this.CurrRateTextBox.Text == "") ? 0 : Convert.ToDecimal(this.CurrRateTextBox.Text);
            _FINCustReturHd.PPN = (this.PPNPercentTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNPercentTextBox.Text);

            if (this.PPNDateTextBox.Text != "")
            {
                _FINCustReturHd.PPNDate = Convert.ToDateTime(this.PPNDateTextBox.Text);
            }
            else
            {
                _FINCustReturHd.PPNDate = null;
            }
            _FINCustReturHd.PPNForex = (this.PPNForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNForexTextBox.Text);
            _FINCustReturHd.PPNNo = this.PPNNoTextBox.Text;
            _FINCustReturHd.PPNRate = (this.PPNRateTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNRateTextBox.Text);
            _FINCustReturHd.BillTo = this.BillToDropDownList.SelectedValue;
            _FINCustReturHd.Remark = this.RemarkTextBox.Text;
            _FINCustReturHd.Attn = this.AttnTextBox.Text;
            _FINCustReturHd.BaseForex = (this.AmountBaseTextBox.Text == "") ? 0 : Convert.ToDecimal(this.AmountBaseTextBox.Text);
            //_FINCustReturHd.DiscForex = (this.DiscForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.DiscForexTextBox.Text);
            _FINCustReturHd.TotalForex = (this.TotalForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text);
            //_FINCustReturHd.OtherForex = (this.OtherForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.OtherForexTextBox.Text);
            _FINCustReturHd.Term = this.TermDropDownList.SelectedValue;
            _FINCustReturHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _FINCustReturHd.DatePrep = DateTime.Now;

            bool _result = this._customerReturBL.EditFINCustReturHd(_FINCustReturHd);

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

        private void ClearDataNumeric()
        {
            this.CurrRateTextBox.Text = "0";
            this.DecimalPlaceHiddenField.Value = "";
            this.PPNPercentTextBox.Text = "0";
            this.PPNRateTextBox.Text = "0";
            this.CurrTextBox.Text = "";
            this.AmountBaseTextBox.Text = "0";
            this.PPNForexTextBox.Text = "0";
            this.TotalForexTextBox.Text = "0";
        }

        private void DisableRate()
        {
            this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.CurrRateTextBox.Text = "1";
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.PPNRateTextBox.Text = "1";
        }

        private void EnableRate()
        {
            this.CurrRateTextBox.Attributes.Remove("ReadOnly");
            this.CurrRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
            this.PPNRateTextBox.Attributes.Remove("ReadOnly");
            this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
        }

        private void SetCurrRate()
        {
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrCodeDropDownList.SelectedValue);
            this.CurrRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNRateTextBox.Text = this.CurrRateTextBox.Text;
            this.CurrTextBox.Text = this.CurrCodeDropDownList.SelectedValue;
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