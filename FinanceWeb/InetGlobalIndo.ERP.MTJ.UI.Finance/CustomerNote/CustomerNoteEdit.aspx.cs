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

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.CustomerNote
{
    public partial class CustomerNoteEdit : CustomerNoteBase
    {
        private CustomerBL _custBL = new CustomerBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private TermBL _termBL = new TermBL();
        private CustomerNoteBL _customerNoteBL = new CustomerNoteBL();
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

            this.PPNPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2" /*this.ppn_date_start.ClientID*/ + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.OtherForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.DiscForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2" /*this.ppn_date_start.ClientID*/ + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.OtherForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.OtherForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2" /*this.ppn_date_start.ClientID*/ + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.OtherForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

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
            this.DiscForexTextBox.Text = "0";
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
            FINCustInvHd _finCustInvHd = this._customerNoteBL.GetSingleFINCustInvHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finCustInvHd.CurrCode);

            this.TransNoTextBox.Text = _finCustInvHd.TransNmbr;
            this.FileNmbrTextBox.Text = _finCustInvHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_finCustInvHd.TransDate);
            this.CustDropDownList.SelectedValue = _finCustInvHd.CustCode;
            this.CurrCodeDropDownList.SelectedValue = _finCustInvHd.CurrCode;
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.OtherForexTextBox.Text = (_finCustInvHd.OtherForex == 0) ? "0" : _finCustInvHd.OtherForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TotalForexTextBox.Text = (((_finCustInvHd.TotalForex == null) ? 0 : Convert.ToDecimal(_finCustInvHd.TotalForex)) == 0) ? "0" : Convert.ToDecimal(_finCustInvHd.TotalForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TermDropDownList.SelectedValue = _finCustInvHd.Term;
            this.CurrTextBox.Text = _finCustInvHd.CurrCode;
            if (_finCustInvHd.CurrCode == _currencyBL.GetCurrDefault())
            {
                this.DisableRate();
            }
            else
            {
                this.EnableRate();
                this.CurrRateTextBox.Text = (_finCustInvHd.ForexRate == 0) ? "0" : _finCustInvHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.PPNRateTextBox.Text = (_finCustInvHd.PPNRate == null || _finCustInvHd.PPNRate == 0) ? "0" : Convert.ToDecimal(_finCustInvHd.PPNRate).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            this.AttnTextBox.Text = _finCustInvHd.Attn;
            this.RemarkTextBox.Text = _finCustInvHd.Remark;
            this.BillToDropDownList.SelectedValue = _finCustInvHd.BillTo;
            this.PPNPercentTextBox.Text = (_finCustInvHd.PPN == 0) ? "0" : Convert.ToDecimal(_finCustInvHd.PPN).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            if (_finCustInvHd.PPN != 0)
            {
                this.PPNDateLiteral.Text = "<input id='button2' Style = 'visibility:visible' type='button' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PPNNoTextBox.Text = _finCustInvHd.PPNNo;
                this.PPNNoTextBox.Attributes.Remove("ReadOnly");
                this.PPNNoTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                this.PPNDateTextBox.Text = (_finCustInvHd.PPNDate == null) ? "" : DateFormMapper.GetValue((DateTime)_finCustInvHd.PPNDate);
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
            this.PPNForexTextBox.Text = (_finCustInvHd.PPNForex == 0) ? "0" : Convert.ToDecimal(_finCustInvHd.PPNForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountBaseTextBox.Text = (_finCustInvHd.BaseForex == null || _finCustInvHd.BaseForex == 0) ? "0" : Convert.ToDecimal(_finCustInvHd.BaseForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DiscForexTextBox.Text = (_finCustInvHd.DiscForex == null || _finCustInvHd.DiscForex == 0) ? "0" : Convert.ToDecimal(_finCustInvHd.DiscForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            
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
            FINCustInvHd _finCustInvHd = this._customerNoteBL.GetSingleFINCustInvHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finCustInvHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finCustInvHd.CustCode = this.CustDropDownList.SelectedValue;
            _finCustInvHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _finCustInvHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finCustInvHd.PPN = Convert.ToDecimal(this.PPNPercentTextBox.Text);
            if (this.PPNDateTextBox.Text != "")
            {
                _finCustInvHd.PPNDate = Convert.ToDateTime(this.PPNDateTextBox.Text);
            }
            else
            {
                _finCustInvHd.PPNDate = null;
            }
            _finCustInvHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _finCustInvHd.PPNNo = this.PPNNoTextBox.Text;
            _finCustInvHd.PPNRate = Convert.ToDecimal(this.PPNRateTextBox.Text);
            _finCustInvHd.BillTo = this.BillToDropDownList.SelectedValue;
            _finCustInvHd.Remark = this.RemarkTextBox.Text;
            _finCustInvHd.Attn = this.AttnTextBox.Text;
            _finCustInvHd.BaseForex = (this.AmountBaseTextBox.Text == "") ? 0 : Convert.ToDecimal(this.AmountBaseTextBox.Text);
            _finCustInvHd.DiscForex = (this.DiscForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.DiscForexTextBox.Text);
            _finCustInvHd.TotalForex = (this.TotalForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text);
            _finCustInvHd.OtherForex = (this.OtherForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.OtherForexTextBox.Text);
            _finCustInvHd.Term = this.TermDropDownList.SelectedValue;
            _finCustInvHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finCustInvHd.DatePrep = DateTime.Now;

            bool _result = this._customerNoteBL.EditFINCustInvHd(_finCustInvHd);

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
            FINCustInvHd _finCustInvHd = this._customerNoteBL.GetSingleFINCustInvHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finCustInvHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            //_finCustInvHd.Status = CustomerNoteDataMapper.GetStatus(CustomerNoteStatus.OnHold);
            _finCustInvHd.CustCode = this.CustDropDownList.SelectedValue;
            _finCustInvHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _finCustInvHd.ForexRate = (this.CurrRateTextBox.Text == "") ? 0 : Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finCustInvHd.PPN = (this.PPNPercentTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNPercentTextBox.Text);

            if (this.PPNDateTextBox.Text != "")
            {
                _finCustInvHd.PPNDate = Convert.ToDateTime(this.PPNDateTextBox.Text);
            }
            else
            {
                _finCustInvHd.PPNDate = null;
            }
            _finCustInvHd.PPNForex = (this.PPNForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNForexTextBox.Text);
            _finCustInvHd.PPNNo = this.PPNNoTextBox.Text;
            _finCustInvHd.PPNRate = (this.PPNRateTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNRateTextBox.Text);
            _finCustInvHd.BillTo = this.BillToDropDownList.SelectedValue;
            _finCustInvHd.Remark = this.RemarkTextBox.Text;
            _finCustInvHd.Attn = this.AttnTextBox.Text;
            _finCustInvHd.BaseForex = (this.AmountBaseTextBox.Text == "") ? 0 : Convert.ToDecimal(this.AmountBaseTextBox.Text);
            _finCustInvHd.DiscForex = (this.DiscForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.DiscForexTextBox.Text);
            _finCustInvHd.TotalForex = (this.TotalForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text);
            _finCustInvHd.OtherForex = (this.OtherForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.OtherForexTextBox.Text);
            _finCustInvHd.Term = this.TermDropDownList.SelectedValue;
            _finCustInvHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finCustInvHd.DatePrep = DateTime.Now;

            bool _result = this._customerNoteBL.EditFINCustInvHd(_finCustInvHd);

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