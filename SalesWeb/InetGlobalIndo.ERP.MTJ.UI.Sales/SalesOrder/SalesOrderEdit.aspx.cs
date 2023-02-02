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

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.SalesOrder
{
    public partial class SalesOrderEdit : SalesOrderBase
    {
        private CustomerBL _custBL = new CustomerBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private TermBL _termBL = new TermBL();
        private SalesOrderBL _salesOrderBL = new SalesOrderBL();
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
                this.CalendarScriptLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.CustPODateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.DeliveryDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.DeliveryDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.DueDateLiteral.Text = "<input id='button3' type='button' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.DateLiteral.Text = "<input id='button4' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

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

        private void SetAttribute()
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

        public void ShowDeliveryTo()
        {
            this.DeliveryToDropDownList.Items.Clear();
            this.DeliveryToDropDownList.DataTextField = "DeliveryName";
            this.DeliveryToDropDownList.DataValueField = "DeliveryCode";
            this.DeliveryToDropDownList.DataSource = this._custBL.GetListCustAddressByCustCode(this.CustDropDownList.SelectedValue);
            this.DeliveryToDropDownList.DataBind();
            this.DeliveryToDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
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
            MKTSOHd _salesOrderHd = this._salesOrderBL.GetSingleMKTSOHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisiKey), ApplicationConfig.EncryptionKey)));

            byte _decimalPlace = _currencyBL.GetDecimalPlace(_salesOrderHd.CurrCode);

            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.TransNoTextBox.Text = _salesOrderHd.TransNmbr;
            this.FileNmbrTextBox.Text = _salesOrderHd.FileNmbr;
            this.RevisiTextBox.Text = _salesOrderHd.Revisi.ToString();
            this.DateTextBox.Text = DateFormMapper.GetValue(_salesOrderHd.TransDate);
            this.CustDropDownList.SelectedValue = _salesOrderHd.CustCode;
            this.AttnTextBox.Text = _salesOrderHd.Attn;
            this.BillToDropDownList.SelectedValue = _salesOrderHd.BillTo;
            this.TermDropDownList.SelectedValue = _salesOrderHd.Term;
            this.DueDateTextBox.Text = DateFormMapper.GetValue(_salesOrderHd.DueDate);
            this.CustPONoTextBox.Text = _salesOrderHd.CustPONo;
            this.CustPODateTextBox.Text = DateFormMapper.GetValue(_salesOrderHd.CustPODate);
            if (this.CustDropDownList.SelectedValue != "null")
            {
                this.ShowDeliveryTo();
            }
            this.DeliveryToDropDownList.SelectedValue = _salesOrderHd.DeliveryTo;
            this.DeliveryDateTextBox.Text = DateFormMapper.GetValue(_salesOrderHd.DeliveryDate);
            this.CurrCodeDropDownList.SelectedValue = _salesOrderHd.CurrCode;
            this.CurrTextBox.Text = _salesOrderHd.CurrCode;

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

            this.ForexRateTextBox.Text = _salesOrderHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TotalForexTextBox.Text = _salesOrderHd.TotalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            //this.FgDPCheckBox.Checked = SalesOrderDataMapper.GetStatusFlag(_salesOrderHd.FgDP);
            decimal _dpPercentValue = Convert.ToDecimal((_salesOrderHd.DP == null) ? 0 : _salesOrderHd.DP);
            this.DPPercentTextBox.Text = (_dpPercentValue == 0) ? "0" : _dpPercentValue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            decimal _dpForexValue = Convert.ToDecimal((_salesOrderHd.DPForex == null) ? 0 : _salesOrderHd.DPForex);
            this.DPForexTextBox.Text = (_dpForexValue == 0) ? "0" : _dpForexValue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _salesOrderHd.Remark;

            if (_salesOrderHd.BaseForex != null)
            {
                this.BaseForexTextBox.Text = (_salesOrderHd.BaseForex == 0) ? "0" : _salesOrderHd.BaseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            else
            {
                this.BaseForexTextBox.Text = "0";
            }
            if (_salesOrderHd.Disc != null)
            {
                this.DiscTextBox.Text = (_salesOrderHd.Disc == 0) ? "0" : _salesOrderHd.Disc.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            else
            {
                this.DiscTextBox.Text = "0";
            }
            if (_salesOrderHd.DiscForex != null)
            {
                this.DiscForexTextBox.Text = (_salesOrderHd.DiscForex == 0) ? "0" : _salesOrderHd.DiscForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            else
            {
                this.DiscForexTextBox.Text = "0";
            }
            if (_salesOrderHd.PPN != null)
            {
                this.PPNPercentTextBox.Text = (_salesOrderHd.PPN == 0) ? "0" : _salesOrderHd.PPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            else
            {
                this.PPNPercentTextBox.Text = "0";
            }
            if (_salesOrderHd.PPNForex != null)
            {
                this.PPNForexTextBox.Text = (_salesOrderHd.PPNForex == 0) ? "0" : _salesOrderHd.PPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            else
            {
                this.PPNForexTextBox.Text = "0";
            }
        }

        protected void CustDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CustDropDownList.SelectedValue != "null")
            {
                this.AttnTextBox.Text = _custBL.GetCustContact(this.CustDropDownList.SelectedValue);
                string _currCode = this._custBL.GetCurr(CustDropDownList.SelectedValue);
                string _termCode = this._custBL.GetTerm(CustDropDownList.SelectedValue);
                string _billTo = this._custBL.GetBillTo(CustDropDownList.SelectedValue);
                byte _decimalPlace = _currencyBL.GetDecimalPlace(_currCode);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                if (_currCode != "")
                {
                    this.CurrCodeDropDownList.SelectedValue = _currCode;
                }
                this.ForexRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                if (_termCode != "")
                {
                    this.TermDropDownList.SelectedValue = _termCode;
                }
                if (_billTo != "")
                {
                    this.BillToDropDownList.SelectedValue = _billTo;
                }
                if (this.CurrCodeDropDownList.SelectedValue == _currencyBL.GetCurrDefault())
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

                this.ShowDeliveryTo();
            }
            else
            {
                this.AttnTextBox.Text = "";
                this.CurrTextBox.Text = "";
                this.CurrCodeDropDownList.SelectedValue = "null";
                this.TermDropDownList.SelectedValue = "null";
                this.ForexRateTextBox.Text = "0";
                this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#ffffff");
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
                this.ForexRateTextBox.Text = _currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

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
            MKTSOHd _salesOrderHd = this._salesOrderBL.GetSingleMKTSOHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisiKey), ApplicationConfig.EncryptionKey)));

            _salesOrderHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
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
            _salesOrderHd.ForexRate = (this.ForexRateTextBox.Text == "") ? 0 : Convert.ToDecimal(this.ForexRateTextBox.Text);
            _salesOrderHd.BaseForex = (this.BaseForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.BaseForexTextBox.Text);
            _salesOrderHd.Disc = (this.DiscTextBox.Text == "") ? 0 : Convert.ToDecimal(this.DiscTextBox.Text);
            _salesOrderHd.DiscForex = (this.DiscForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.DiscForexTextBox.Text);
            _salesOrderHd.PPN = (this.PPNPercentTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNPercentTextBox.Text);
            _salesOrderHd.PPNForex = (this.PPNForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNForexTextBox.Text);
            _salesOrderHd.TotalForex = (this.TotalForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text);
            if (Convert.ToDecimal(this.DPPercentTextBox.Text) != 0)
            {
                _salesOrderHd.FgDP = YesNoDataMapper.GetYesNo(YesNo.Yes);
            }
            else
            {
                _salesOrderHd.FgDP = YesNoDataMapper.GetYesNo(YesNo.No);
            }
            _salesOrderHd.DP = (this.DPPercentTextBox.Text == "") ? 0 : Convert.ToDecimal(this.DPPercentTextBox.Text);
            _salesOrderHd.DPForex = (this.DPForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.DPForexTextBox.Text);
            _salesOrderHd.Remark = this.RemarkTextBox.Text;
            _salesOrderHd.FgActive = YesNoDataMapper.GetYesNo(YesNo.No);
            _salesOrderHd.SOType = AppModule.GetValue(TransactionType.SalesOrder);
            _salesOrderHd.DoneInvoice = YesNoDataMapper.GetYesNo(YesNo.No);
            _salesOrderHd.EditBy  = HttpContext.Current.User.Identity.Name;
            _salesOrderHd.EditDate = DateTime.Now;

            bool _result = this._salesOrderBL.EditMKTSOHd(_salesOrderHd);

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
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeRevisiKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeRevisiKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.Page.IsValid == true)
            {
                MKTSOHd _salesOrderHd = this._salesOrderBL.GetSingleMKTSOHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisiKey), ApplicationConfig.EncryptionKey)));

                _salesOrderHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
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
                _salesOrderHd.ForexRate = (this.ForexRateTextBox.Text == "") ? 0 : Convert.ToDecimal(this.ForexRateTextBox.Text);
                _salesOrderHd.BaseForex = (this.BaseForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.BaseForexTextBox.Text);
                _salesOrderHd.Disc = (this.DiscTextBox.Text == "") ? 0 : Convert.ToDecimal(this.DiscTextBox.Text);
                _salesOrderHd.DiscForex = (this.DiscForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.DiscForexTextBox.Text);
                _salesOrderHd.PPN = (this.PPNPercentTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNPercentTextBox.Text);
                _salesOrderHd.PPNForex = (this.PPNForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNForexTextBox.Text);
                _salesOrderHd.TotalForex = (this.TotalForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text);
                if (Convert.ToDecimal(this.DPPercentTextBox.Text) != 0)
                {
                    _salesOrderHd.FgDP = YesNoDataMapper.GetYesNo(YesNo.Yes);
                }
                else
                {
                    _salesOrderHd.FgDP = YesNoDataMapper.GetYesNo(YesNo.No);
                }
                _salesOrderHd.DP = (this.DPPercentTextBox.Text == "") ? 0 : Convert.ToDecimal(this.DPPercentTextBox.Text);
                _salesOrderHd.DPForex = (this.DPForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.DPForexTextBox.Text);
                _salesOrderHd.Remark = this.RemarkTextBox.Text;
                _salesOrderHd.FgActive = YesNoDataMapper.GetYesNo(YesNo.No);
                _salesOrderHd.SOType = AppModule.GetValue(TransactionType.SalesOrder);
                _salesOrderHd.DoneInvoice = YesNoDataMapper.GetYesNo(YesNo.No);
                _salesOrderHd.EditBy = HttpContext.Current.User.Identity.Name;
                _salesOrderHd.EditDate = DateTime.Now;

                bool _result = this._salesOrderBL.EditMKTSOHd(_salesOrderHd);

                if (_result == true)
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeRevisiKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeRevisiKey)));
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