using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.SalesOrder
{
    public partial class SalesOrderAdd : SalesOrderBase
    {
        private CustomerBL _cust = new CustomerBL();
        private CurrencyBL _currency = new CurrencyBL();
        private CurrencyRateBL _currencyRate = new CurrencyRateBL();
        private TermBL _term = new TermBL();
        private SalesOrderBL _salesOrder = new SalesOrderBL();
        private PermissionBL _permBL = new PermissionBL();

        string _imgPPNDate = "ppn_date_start";

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
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.CalendarScriptLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.CustPODateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.DeliveryDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.DeliveryDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.DueDateLiteral.Text = "<input id='button3' type='button' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.DateLiteral.Text = "<input id='button4' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.ShowCust();
                this.ShowTerm();
                this.ShowBillTo();
                this.ShowCurrency();

                this.ShowAttribute();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.DueDateTextBox.Attributes.Add("ReadOnly", "True");
            this.CustPODateTextBox.Attributes.Add("ReadOnly", "True");
            this.DeliveryDateTextBox.Attributes.Add("ReadOnly", "True");
            this.DPForexTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrTextBox.Attributes.Add("ReadOnly", "True");
            this.BaseForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");

            this.ForexRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.DPPercentTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.DiscTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.DiscForexTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.PPNPercentTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");

            this.ForexRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.ForexRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.DPPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.DPPercentTextBox.ClientID + ", " + this.DPForexTextBox.ClientID + ", " + this.BaseForexTextBox.ClientID + ", " + this.DiscForexTextBox.ClientID + ", " + this.PPNPercentTextBox.ClientID + ", " + this.PPNForexTextBox.ClientID + ", " + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.DPPercentTextBox.ClientID + ", " + this.DPForexTextBox.ClientID + ", " + this.BaseForexTextBox.ClientID + ", " + this.DiscForexTextBox.ClientID + ", " + this.PPNPercentTextBox.ClientID + ", " + this.PPNForexTextBox.ClientID + ", " + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.DiscTextBox.Attributes.Add("OnBlur", "CalculateDisc(" + this.DPPercentTextBox.ClientID + ", " + this.DPForexTextBox.ClientID + ", " + this.BaseForexTextBox.ClientID + ", " + this.DiscTextBox.ClientID + ", " + this.DiscForexTextBox.ClientID + ", " + this.PPNPercentTextBox.ClientID + ", " + this.PPNForexTextBox.ClientID + ", " + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.DiscForexTextBox.Attributes.Add("OnBlur", "CalculateDiscForex(" + this.DPPercentTextBox.ClientID + ", " + this.DPForexTextBox.ClientID + ", " + this.BaseForexTextBox.ClientID + ", " + this.DiscTextBox.ClientID + ", " + this.DiscForexTextBox.ClientID + ", " + this.PPNPercentTextBox.ClientID + ", " + this.PPNForexTextBox.ClientID + ", " + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowCust()
        {
            this.CustDropDownList.Items.Clear();
            this.CustDropDownList.DataTextField = "CustName";
            this.CustDropDownList.DataValueField = "CustCode";
            this.CustDropDownList.DataSource = this._cust.GetListCustomerForDDL();
            this.CustDropDownList.DataBind();
            this.CustDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowTerm()
        {
            this.TermDropDownList.Items.Clear();
            this.TermDropDownList.DataTextField = "TermName";
            this.TermDropDownList.DataValueField = "TermCode";
            this.TermDropDownList.DataSource = this._term.GetListTermForDDL();
            this.TermDropDownList.DataBind();
            this.TermDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowBillTo()
        {
            this.BillToDropDownList.Items.Clear();
            this.BillToDropDownList.DataTextField = "CustName";
            this.BillToDropDownList.DataValueField = "CustCode";
            this.BillToDropDownList.DataSource = this._cust.GetListCustomerForDDL();
            this.BillToDropDownList.DataBind();
            this.BillToDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowDeliveryTo()
        {
            this.DeliveryToDropDownList.Items.Clear();
            this.DeliveryToDropDownList.DataTextField = "DeliveryName";
            this.DeliveryToDropDownList.DataValueField = "DeliveryCode";
            this.DeliveryToDropDownList.DataSource = this._cust.GetListCustAddressByCustCode(this.CustDropDownList.SelectedValue);
            this.DeliveryToDropDownList.DataBind();
            this.DeliveryToDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowCurrency()
        {
            this.CurrCodeDropDownList.Items.Clear();
            this.CurrCodeDropDownList.DataTextField = "CurrCode";
            this.CurrCodeDropDownList.DataValueField = "CurrCode";
            this.CurrCodeDropDownList.DataSource = this._currency.GetListAll();
            this.CurrCodeDropDownList.DataBind();
            this.CurrCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ClearLabel();

            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            this.CustDropDownList.SelectedValue = "null";
            this.BillToDropDownList.SelectedValue = "null";
            this.TermDropDownList.SelectedValue = "null";
            this.DeliveryToDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.DeliveryToDropDownList.SelectedValue = "null";
            this.CurrCodeDropDownList.SelectedValue = "null";

            this.DueDateTextBox.Text = DateFormMapper.GetValue(now);
            this.CustPODateTextBox.Text = DateFormMapper.GetValue(now);
            this.DeliveryDateTextBox.Text = DateFormMapper.GetValue(now);

            this.AttnTextBox.Text = "";
            this.CustPONoTextBox.Text = "";
            this.ForexRateTextBox.Text = "0";
            this.ForexRateTextBox.Attributes.Remove("ReadOnly");
            this.DPPercentTextBox.Text = "0";
            this.DPForexTextBox.Text = "0";
            this.CurrTextBox.Text = "";
            this.BaseForexTextBox.Text = "0";
            this.DiscTextBox.Text = "0";
            this.DiscForexTextBox.Text = "0";
            this.PPNPercentTextBox.Text = "0";
            this.PPNForexTextBox.Text = "0";
            this.TotalForexTextBox.Text = "0";
            this.RemarkTextBox.Text = "";
            this.DecimalPlaceHiddenField.Value = "";
        }

        protected void CustDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CustDropDownList.SelectedValue != "null")
            {
                this.AttnTextBox.Text = _cust.GetCustContact(this.CustDropDownList.SelectedValue);
                string _currCode = this._cust.GetCurr(this.CustDropDownList.SelectedValue);
                string _termCode = this._cust.GetTerm(this.CustDropDownList.SelectedValue);
                string _billTo = this._cust.GetBillTo(this.CustDropDownList.SelectedValue);
                byte _decimalPlace = _currency.GetDecimalPlace(_currCode);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                if (_currCode != "")
                {
                    this.CurrCodeDropDownList.SelectedValue = _currCode;
                }
                this.ForexRateTextBox.Text = this._currencyRate.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                if (_termCode != "")
                {
                    this.TermDropDownList.SelectedValue = _termCode;
                }
                if (_billTo != "")
                {
                    this.BillToDropDownList.SelectedValue = this.CustDropDownList.SelectedValue;
                }
                else
                {
                    this.BillToDropDownList.SelectedValue = "null";
                }

                if (this.CurrCodeDropDownList.SelectedValue == _currency.GetCurrDefault())
                {
                    this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.ForexRateTextBox.Text = "1";
                }
                else
                {
                    this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                    this.ForexRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
                }
                this.CurrTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrTextBox.Attributes.Add("style", "background-color:#cccccc");
                this.CurrTextBox.Text = CurrCodeDropDownList.SelectedValue;
                this.ForexRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.ForexRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

                this.ShowDeliveryTo();
            }
            else
            {
                this.AttnTextBox.Text = "";
                this.CurrTextBox.Text = "";
                this.CurrCodeDropDownList.SelectedValue = "null";
                this.TermDropDownList.SelectedValue = "null";
                this.BillToDropDownList.SelectedValue = "null";

                this.DeliveryToDropDownList.Items.Clear();
                this.DeliveryToDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

                this.ForexRateTextBox.Text = "0";
                this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#ffffff");
            }
        }

        protected void CurrCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrCodeDropDownList.SelectedValue != "null")
            {
                string _currCode = this._cust.GetCurr(CustDropDownList.SelectedValue);
                string _currCodeHome = _currency.GetCurrDefault();
                byte _decimalPlace = _currency.GetDecimalPlace(this.CurrCodeDropDownList.SelectedValue);
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

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            MKTSOHd _salesOrderHd = new MKTSOHd();

            _salesOrderHd.Revisi = 0;
            _salesOrderHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _salesOrderHd.Status = SalesOrderDataMapper.GetStatus(TransStatus.OnHold);
            _salesOrderHd.CustCode = this.CustDropDownList.SelectedValue;
            _salesOrderHd.Attn = this.AttnTextBox.Text;
            _salesOrderHd.BillTo = this.BillToDropDownList.SelectedValue;
            _salesOrderHd.Term = this.TermDropDownList.SelectedValue;
            _salesOrderHd.DueDate = DateFormMapper.GetValue(this.DueDateTextBox.Text);
            _salesOrderHd.CustPONo = this.CustPONoTextBox.Text;
            _salesOrderHd.CustPODate = DateFormMapper.GetValue(this.CustPODateTextBox.Text);
            _salesOrderHd.DeliveryTo = this.DeliveryToDropDownList.SelectedValue;
            _salesOrderHd.DeliveryDate = DateFormMapper.GetValue(this.DeliveryDateTextBox.Text);
            _salesOrderHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _salesOrderHd.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _salesOrderHd.BaseForex = Convert.ToDecimal(this.BaseForexTextBox.Text);
            _salesOrderHd.Disc = Convert.ToDecimal(this.DiscTextBox.Text);
            _salesOrderHd.DiscForex = (this.DiscForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.DiscForexTextBox.Text);
            _salesOrderHd.PPN = Convert.ToDecimal(this.PPNPercentTextBox.Text);
            _salesOrderHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _salesOrderHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            if (Convert.ToDecimal(this.DPPercentTextBox.Text) != 0)
            {
                _salesOrderHd.FgDP = YesNoDataMapper.GetYesNo(YesNo.Yes);
            }
            else
            {
                _salesOrderHd.FgDP = YesNoDataMapper.GetYesNo(YesNo.No);
            }
            _salesOrderHd.DP = Convert.ToDecimal(this.DPPercentTextBox.Text);
            _salesOrderHd.DPForex = Convert.ToDecimal(this.DPForexTextBox.Text);
            _salesOrderHd.Remark = this.RemarkTextBox.Text;
            _salesOrderHd.FgActive = YesNoDataMapper.GetYesNo(YesNo.No);
            _salesOrderHd.SOType = AppModule.GetValue(TransactionType.SalesOrder);
            _salesOrderHd.DoneInvoice = YesNoDataMapper.GetYesNo(YesNo.No);
            _salesOrderHd.CreatedBy = HttpContext.Current.User.Identity.Name;
            _salesOrderHd.CreatedDate = DateTime.Now;
            _salesOrderHd.EditBy = HttpContext.Current.User.Identity.Name;
            _salesOrderHd.EditDate = DateTime.Now;

            string _result = this._salesOrder.AddMKTSOHd(_salesOrderHd);

            if (_result != "")
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)) + "&" + this._codeRevisiKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt("0", ApplicationConfig.EncryptionKey)));
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
    }
}