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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.CustomerBeginning
{
    public partial class CustBeginningEdit : CustomerBeginningBase
    {
        private CustomerBL _custBL = new CustomerBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private FINBeginCIBL _finBeginCIBL = new FINBeginCIBL();
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
                this.DueDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.PPNDateLiteral.Text = "<input id='button3' Style ='visibility:visible' type='button' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowCust();
                this.ShowBillTo();
                this.ShowCurrency();

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.PPNTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button3" /*this.PPNDateButton.ClientID*/ + "," + this.BaseForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.BaseForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button3" /*this.PPNDateButton.ClientID*/ + "," + this.BaseForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        protected void SetAttribute()
        {
            this.InvoiceNoTextBox.Attributes.Add("ReadOnly", "True");
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.DueDateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            //this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
            //this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
            //this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            //this.SetAttributeRate();
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
            FINBeginCI _FINBeginCI = this._finBeginCIBL.GetSingleFINBeginCI(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_FINBeginCI.CurrCode);

            this.InvoiceNoTextBox.Text = _FINBeginCI.InvoiceNo;
            this.DateTextBox.Text = DateFormMapper.GetValue(_FINBeginCI.TransDate);
            this.CustPONoTextBox.Text = _FINBeginCI.CustPONo;
            this.CustDropDownList.SelectedValue = _FINBeginCI.CustCode;
            this.BillToDropDownList.SelectedValue = _FINBeginCI.BillTo;
            this.DueDateTextBox.Text = DateFormMapper.GetValue(_FINBeginCI.DueDate);
            this.CurrCodeDropDownList.SelectedValue = _FINBeginCI.CurrCode;
            this.CurrRateTextBox.Text = _FINBeginCI.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.PPNRateTextBox.Text = (_FINBeginCI.PPNRate == null || _FINBeginCI.PPNRate == 0) ? "0" : Convert.ToDecimal(_FINBeginCI.PPNRate).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            if (_FINBeginCI.CurrCode.Trim().ToLower() == this._currencyBL.GetCurrDefault().Trim().ToLower())
            {
                this.DisableRate();
            }
            else
            {
                this.EnableRate();
            }

            this.BaseForexTextBox.Text = (_FINBeginCI.BaseForex == 0) ? "0" : _FINBeginCI.BaseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            if (_FINBeginCI.PPN == 0)
            {
                this.PPNDateLiteral.Text = "<input id='button3' Style ='visibility:hidden' type='button' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                //this.PPNDateButton.Attributes.Add("Style", "visibility:hidden");
                this.PPNNoTextBox.Text = "";
                this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNNoTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
                this.PPNDateTextBox.Text = "";
                this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNDateTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
                this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNRateTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
            }
            else
            {
                this.PPNDateLiteral.Text = "<input id='button3' Style ='visibility:visible' type='button' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                //this.PPNDateButton.Attributes.Add("Style", "visibility:visible");
                this.PPNNoTextBox.Text = _FINBeginCI.PPNNo;
                this.PPNNoTextBox.Attributes.Remove("ReadOnly");
                this.PPNNoTextBox.Attributes.Add("Style", "background-color:#FFFFFF");
                this.PPNDateTextBox.Text = (_FINBeginCI.PPNDate == null) ? "" : DateFormMapper.GetValue(_FINBeginCI.PPNDate);
                this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNDateTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
                this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                this.PPNRateTextBox.Attributes.Add("Style", "background-color:#FFFFFF");
            }
            this.PPNTextBox.Text = (_FINBeginCI.PPN == 0) ? "0" : _FINBeginCI.PPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNForexTextBox.Text = (_FINBeginCI.PPNForex == 0) ? "0" : _FINBeginCI.PPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TotalForexTextBox.Text = (_FINBeginCI.TotalForex == 0) ? "0" : _FINBeginCI.TotalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.CurrTextBox.Text = _FINBeginCI.CurrCode;
            this.RemarkTextBox.Text = _FINBeginCI.Remark;

            this.SetAttributeRate();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINBeginCI _finBeginCI = this._finBeginCIBL.GetSingleFINBeginCI(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finBeginCI.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finBeginCI.BillTo = this.BillToDropDownList.SelectedValue;
            _finBeginCI.CustPONo = this.CustPONoTextBox.Text;
            _finBeginCI.CustCode = this.CustDropDownList.SelectedValue;
            _finBeginCI.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _finBeginCI.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finBeginCI.Remark = this.RemarkTextBox.Text;
            _finBeginCI.PPNNo = this.PPNNoTextBox.Text;
            TimeSpan _tes = DateFormMapper.GetValue(this.DueDateTextBox.Text) - DateFormMapper.GetValue(this.DateTextBox.Text);
            _finBeginCI.Term = _tes.Days;
            if (this.PPNDateTextBox.Text == "")
            {
                _finBeginCI.PPNDate = null;
            }
            else
            {
                _finBeginCI.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            _finBeginCI.PPNRate = (this.PPNRateTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNRateTextBox.Text);
            _finBeginCI.BaseForex = Convert.ToDecimal(this.BaseForexTextBox.Text);
            _finBeginCI.PPN = (this.PPNTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNTextBox.Text);
            _finBeginCI.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _finBeginCI.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            _finBeginCI.DueDate = DateFormMapper.GetValue(this.DueDateTextBox.Text);
            _finBeginCI.UserPrep = HttpContext.Current.User.Identity.Name;
            _finBeginCI.DatePrep = DateTime.Now;

            bool _result = this._finBeginCIBL.EditFINBeginCI(_finBeginCI);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
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

        private void ClearDataNumeric()
        {
            this.CurrRateTextBox.Text = "0";
            this.DecimalPlaceHiddenField.Value = "";
            this.PPNTextBox.Text = "0";
            this.PPNRateTextBox.Text = "0";
            this.CurrTextBox.Text = "";
            this.BaseForexTextBox.Text = "0";
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

        protected void CurrCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearDataNumeric();

            if (CurrCodeDropDownList.SelectedValue != "null")
            {
                this.SetCurrRate();
            }

            this.SetAttributeRate();
        }

        protected void CustDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _billTo = "";
            string _currCode = "";

            this.ClearDataNumeric();

            if (this.CustDropDownList.SelectedValue != "null")
            {
                _billTo = _custBL.GetBillTo(this.CustDropDownList.SelectedValue);
                _currCode = this._custBL.GetCurr(CustDropDownList.SelectedValue);

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
    }
}