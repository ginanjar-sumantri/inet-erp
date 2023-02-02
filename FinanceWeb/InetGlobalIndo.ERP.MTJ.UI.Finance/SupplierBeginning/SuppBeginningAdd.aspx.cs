using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.SupplierBeginning
{
    public partial class SuppBeginningAdd : SupplierBeginningBase
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

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.DueDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.PPNDateLiteral.Text = "<input id='button3' type='button' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ShowSupp();
                this.ShowCurrency();

                this.ClearLabel();
                this.ClearData();
                this.SetAttribute();
            }
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.CurrRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.PPNRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");

            this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.PPNTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button3" + "," + this.BaseForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.BaseForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button3" + "," + this.BaseForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }


        private void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.DueDateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.SetAttributeRate();
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

        public void ShowSupp()
        {
            this.SuppDropDownList.Items.Clear();
            this.SuppDropDownList.DataTextField = "SuppName";
            this.SuppDropDownList.DataValueField = "SuppCode";
            this.SuppDropDownList.DataSource = this._suppBL.GetListDDLSupp();
            this.SuppDropDownList.DataBind();
            this.SuppDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
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

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ClearLabel();
            this.InvoiceNoTextBox.Text = "";
            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            this.SuppDropDownList.SelectedValue = "null";
            this.SuppPONoTextBox.Text = "";
            this.DueDateTextBox.Text = DateFormMapper.GetValue(now);
            //this.BillToDropDownList.SelectedValue = "null";
            this.CurrCodeDropDownList.SelectedValue = "null";

            this.EnableRate();

            this.CurrRateTextBox.Text = "";
            this.CurrRateTextBox.Attributes.Remove("ReadOnly");
            this.PPNTextBox.Text = "0";
            this.PPNNoTextBox.Text = "";
            this.PPNDateTextBox.Text = "";
            this.PPNRateTextBox.Text = "";
            this.CurrTextBox.Text = "";
            this.BaseForexTextBox.Text = "0";
            this.PPNForexTextBox.Text = "0";
            this.TotalForexTextBox.Text = "0";
            this.RemarkTextBox.Text = "";
        }

        protected void SuppDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SuppDropDownList.SelectedValue != "null")
            {
                //this.ShowCurrency();
                //this.ClearLabel();
                string _currCode = this._suppBL.GetCurr(this.SuppDropDownList.SelectedValue);
                //this.BillToDropDownList.SelectedValue = _supp.GetBillTo(this.SuppDropDownList.SelectedValue);

                if (_currCode != "")
                {
                    this.CurrCodeDropDownList.SelectedValue = _currCode;
                }

                //if (this.CurrCodeDropDownList.SelectedValue == _currencyBL.GetCurrDefault())
                //{
                //    this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                //    this.CurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                //    this.CurrRateTextBox.Text = "1";
                //    this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                //    this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                //    this.PPNRateTextBox.Text = "1";
                //}
                //else
                //{
                //    this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                //    this.CurrRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                //    this.CurrRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
                //    this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                //    this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                //    this.PPNRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
                //    this.CurrRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString("#,###.##");
                //    this.PPNRateTextBox.Text = this.CurrRateTextBox.Text;
                //}
                //this.CurrTextBox.Text = this.CurrCodeDropDownList.SelectedValue;

                this.SetCurrRate();
            }
            else
            {
                this.CurrCodeDropDownList.SelectedValue = "null";
                this.CurrRateTextBox.Text = "";
                this.PPNRateTextBox.Text = "";
                this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                this.PPNRateTextBox.Attributes.Add("style", "background-color:#ffffff");
                this.CurrTextBox.Text = "";
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#ffffff");
            }
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            FINBeginSI _finBeginSI = new FINBeginSI();

            _finBeginSI.InvoiceNo = this.InvoiceNoTextBox.Text;
            _finBeginSI.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finBeginSI.Status = SuppBeginningDataMapper.GetStatus(TransStatus.OnHold);
            _finBeginSI.SuppPONo = this.SuppPONoTextBox.Text;
            _finBeginSI.SuppCode = this.SuppDropDownList.SelectedValue;
            //_finBeginSI.BillTo = this.BillToDropDownList.SelectedValue;
            _finBeginSI.DueDate = DateFormMapper.GetValue(this.DueDateTextBox.Text);
            TimeSpan _term = DateFormMapper.GetValue(this.DueDateTextBox.Text) - DateFormMapper.GetValue(this.DateTextBox.Text);
            _finBeginSI.Term = _term.Days;
            _finBeginSI.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _finBeginSI.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finBeginSI.PPNNo = this.PPNNoTextBox.Text;
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
            _finBeginSI.Remark = this.RemarkTextBox.Text;
            _finBeginSI.UserPrep = HttpContext.Current.User.Identity.Name;
            _finBeginSI.DatePrep = DateTime.Now;

            bool _result = this._finBeginSIBL.AddFINBeginSI(_finBeginSI);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.InvoiceNoTextBox.Text, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        protected void CurrCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearDataNumeric();

            if (CurrCodeDropDownList.SelectedValue != "null")
            {
                //string _currCode = this._suppBL.GetCurr(SuppDropDownList.SelectedValue);
                //string _currCodeHome = _currencyBL.GetCurrDefault();
                //this.CurrRateTextBox.Text = _currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString("#,###.##");
                //this.PPNRateTextBox.Text = _currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString("#,###.##");

                //if (this.CurrCodeDropDownList.SelectedValue == _currCodeHome)
                //{
                //    this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                //    this.CurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                //    this.CurrRateTextBox.Text = "1";
                //    this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                //    this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                //    this.PPNRateTextBox.Text = "1";
                //}
                //else
                //{
                //    this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                //    this.CurrRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                //    this.CurrRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
                //    this.CurrRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString("#,###.##");
                //    this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                //    this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                //    this.PPNRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
                //}
                //this.CurrTextBox.Attributes.Add("ReadOnly", "True");
                //this.CurrTextBox.Attributes.Add("style", "background-color:#cccccc");
                //this.CurrTextBox.Text = CurrCodeDropDownList.SelectedValue;
                this.SetCurrRate();
            }

            this.SetAttributeRate();
            //else
            //{
            //    this.CurrRateTextBox.Text = "";
            //    this.PPNRateTextBox.Text = "";
            //    this.CurrRateTextBox.Attributes.Remove("ReadOnly");
            //    this.CurrRateTextBox.Attributes.Add("style", "background-color:#ffffff");
            //}
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