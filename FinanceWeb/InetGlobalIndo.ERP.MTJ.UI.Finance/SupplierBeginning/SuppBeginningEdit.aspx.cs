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

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.SupplierBeginning
{
    public partial class SuppBeginningEdit : SupplierBeginningBase
    {
        private SupplierBL _suppBL = new SupplierBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private FINBeginSIBL _finBeginSIBL = new FINBeginSIBL();
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
                this.PPNDateLiteral.Text = "<input id='button3' type='button' Style ='visibility:visible' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowSupp();

                this.SetAttribute();
                this.ClearLabel();
                this.ShowData();
            }
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.PPNTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button3" + "," + this.BaseForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.BaseForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button3" + "," + this.BaseForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        private void SetAttribute()
        {
            this.InvoiceNoTextBox.Attributes.Add("ReadOnly", "True");

            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            //this.TermTextBox.Attributes.Add("ReadOnly", "True");
            this.DueDateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");

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

        public void ShowSupp()
        {
            this.SuppDropDownList.Items.Clear();
            this.SuppDropDownList.DataTextField = "SuppName";
            this.SuppDropDownList.DataValueField = "SuppCode";
            this.SuppDropDownList.DataSource = this._suppBL.GetListDDLSupp();
            this.SuppDropDownList.DataBind();
            this.SuppDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowData()
        {
            FINBeginSI _FINBeginSI = this._finBeginSIBL.GetSingleFINBeginSI(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_FINBeginSI.CurrCode);

            this.InvoiceNoTextBox.Text = _FINBeginSI.InvoiceNo;
            this.DateTextBox.Text = DateFormMapper.GetValue(_FINBeginSI.TransDate);
            //this.StatusLabel.Text = SuppBeginningDataMapper.GetStatusText(_FINBeginSI.Status);
            //this.StatusHiddenField.Value = _FINBeginSI.Status.ToString();
            this.SuppPONoTextBox.Text = _FINBeginSI.SuppPONo;
            this.SuppDropDownList.SelectedValue = _FINBeginSI.SuppCode;
            this.ShowCurrency();
            this.DueDateTextBox.Text = DateFormMapper.GetValue(_FINBeginSI.DueDate);
            this.CurrCodeDropDownList.SelectedValue = _FINBeginSI.CurrCode;
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            if (_FINBeginSI.CurrCode == _currencyBL.GetCurrDefault())
            {
                this.DisableRate();
            }
            else
            {
                this.EnableRate();
            }
            this.CurrRateTextBox.Text = _FINBeginSI.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNTextBox.Text = (_FINBeginSI.PPN == 0) ? "0" : _FINBeginSI.PPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            if (_FINBeginSI.PPN == 0)
            {
                //this.PPNDateButton.Attributes.Add("Style", "visibility:hidden");
                this.PPNDateLiteral.Text = "<input id='button3' type='button' Style ='visibility:hidden' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.PPNNoTextBox.Text = "";
                this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNNoTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
                this.PPNDateTextBox.Text = "";
                this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNDateTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
                this.PPNRateTextBox.Text = "";
            }
            else
            {
                //this.PPNDateButton.Attributes.Add("Style", "visibility:visible");
                this.PPNDateLiteral.Text = "<input id='button3' type='button' Style ='visibility:visible' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.PPNNoTextBox.Text = _FINBeginSI.PPNNo;
                this.PPNNoTextBox.Attributes.Remove("ReadOnly");
                this.PPNNoTextBox.Attributes.Add("Style", "background-color:#FFFFFF");
                this.PPNDateTextBox.Text = (_FINBeginSI.PPNDate == null) ? "" : DateFormMapper.GetValue(_FINBeginSI.PPNDate);
                this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNDateTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
                this.PPNRateTextBox.Text = (_FINBeginSI.PPNRate == null) ? "0" : Convert.ToDecimal(_FINBeginSI.PPNRate).ToString("#,###.##");
            }

            this.CurrTextBox.Text = _FINBeginSI.CurrCode;
            this.BaseForexTextBox.Text = (_FINBeginSI.BaseForex == 0) ? "0" : _FINBeginSI.BaseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNForexTextBox.Text = (_FINBeginSI.PPNForex == 0) ? "0" : _FINBeginSI.PPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TotalForexTextBox.Text = (_FINBeginSI.TotalForex == 0) ? "0" : _FINBeginSI.TotalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _FINBeginSI.Remark;

            this.SetAttributeRate();
        }

        protected void SuppDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearDataNumeric();

            if (this.SuppDropDownList.SelectedValue != "null")
            {
                this.ShowCurrency();
                this.ClearLabel();

                string _currCode = this._suppBL.GetCurr(this.SuppDropDownList.SelectedValue);
                //this.BillToDropDownList.SelectedValue = _supp.GetBillTo(this.SuppDropDownList.SelectedValue);

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
            this.ClearDataNumeric();

            if (CurrCodeDropDownList.SelectedValue != "null")
            {
                this.SetCurrRate();
            }

            this.SetAttributeRate();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINBeginSI _finBeginSI = this._finBeginSIBL.GetSingleFINBeginSI(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finBeginSI.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finBeginSI.SuppPONo = this.SuppPONoTextBox.Text;
            _finBeginSI.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _finBeginSI.SuppCode = this.SuppDropDownList.SelectedValue;
            _finBeginSI.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finBeginSI.Remark = this.RemarkTextBox.Text;
            _finBeginSI.PPNNo = this.PPNNoTextBox.Text;
            TimeSpan _term = DateFormMapper.GetValue(this.DueDateTextBox.Text) - DateFormMapper.GetValue(this.DateTextBox.Text);
            _finBeginSI.Term = _term.Days;
            if (this.PPNDateTextBox.Text == "")
            {
                _finBeginSI.PPNDate = null;
            }
            else
            {
                _finBeginSI.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            _finBeginSI.PPNRate = (this.PPNRateTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNRateTextBox.Text);
            _finBeginSI.BaseForex = Convert.ToDecimal(this.BaseForexTextBox.Text);
            _finBeginSI.PPN = (this.PPNTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNTextBox.Text);
            _finBeginSI.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _finBeginSI.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);

            _finBeginSI.DueDate = DateFormMapper.GetValue(this.DueDateTextBox.Text);

            _finBeginSI.UserPrep = HttpContext.Current.User.Identity.Name;
            _finBeginSI.DatePrep = DateTime.Now;

            bool _result = this._finBeginSIBL.EditFINBeginSI(_finBeginSI);

            if (_result == true)
            {
                //Response.Redirect(this._homePage);
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
    }
}