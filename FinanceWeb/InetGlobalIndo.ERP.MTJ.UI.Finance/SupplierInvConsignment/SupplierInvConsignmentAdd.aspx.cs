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
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.UI.Finance.NotaCreditCustomer;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.SupplierInvConsignment
{
    public partial class SupplierInvConsignmentAdd : SupplierInvConsignmentBase
    {
        private SupplierBL _suppBL = new SupplierBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private TermBL _termBL = new TermBL();
        //private NotaCreditSupplierBL _notaCreditSupplierBL = new NotaCreditSupplierBL();
        private SupplierInvConsignmentBL _finSupInvConsignBL = new SupplierInvConsignmentBL();
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
                this.PPNDateLiteral.Text = "<input id='button2' type='button' Style = 'visibility:visible' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowSupp();
                this.ShowTerm();
                this.ShowCurrency();

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
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountBaseTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");

            this.CurrRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.PPNRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");

            this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.DiscForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2"/*this.ppn_date_start.ClientID */ + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2"/*this.ppn_date_start.ClientID */ + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
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

        public void ShowTerm()
        {
            this.TermDropDownList.Items.Clear();
            this.TermDropDownList.DataTextField = "TermName";
            this.TermDropDownList.DataValueField = "TermCode";
            this.TermDropDownList.DataSource = this._termBL.GetListTermForDDL();
            this.TermDropDownList.DataBind();
            this.TermDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
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
            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            this.SuppDropDownList.SelectedValue = "null";
            this.CurrCodeDropDownList.SelectedValue = "null";
            this.CurrRateTextBox.Text = "0";
            this.CurrRateTextBox.Attributes.Remove("ReadOnly");
            this.TotalForexTextBox.Text = "0";
            this.TermDropDownList.SelectedValue = "null";
            this.CurrTextBox.Text = "";
            //this.AttnTextBox.Text = "";
            this.RemarkTextBox.Text = "";
            this.PPNRateTextBox.Text = "0";
            this.PPNPercentTextBox.Text = "0";
            this.PPNNoTextBox.Text = "";
            this.PPNForexTextBox.Text = "0";
            this.PPNDateTextBox.Text = "";
            this.AmountBaseTextBox.Text = "0";
            this.DiscForexTextBox.Text = "0";
            this.DecimalPlaceHiddenField.Value = "";
        }

        protected void SuppDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SuppDropDownList.SelectedValue != "null")
            {
               // this.AttnTextBox.Text = _suppBL.GetSuppContact(this.SuppDropDownList.SelectedValue);
                string _currCode = this._suppBL.GetCurr(SuppDropDownList.SelectedValue);
                string _termCode = this._suppBL.GetTerm(SuppDropDownList.SelectedValue);
                byte _decimalPlace = _currencyBL.GetDecimalPlace(_currCode);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                if (_currCode != "")
                {
                    this.CurrCodeDropDownList.SelectedValue = _currCode;
                }
                this.CurrRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.PPNRateTextBox.Text = _currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                if (_termCode != "")
                {
                    this.TermDropDownList.SelectedValue = _termCode;
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
                //this.ppn_date_start.Attributes.Add("Style", "visibility:hidden");
                this.PPNDateLiteral.Text = "<input id='button2' type='button' Style = 'visibility:hidden' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PPNNoTextBox.Text = "";
                this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNNoTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                this.PPNDateTextBox.Text = "";
                this.PPNPercentTextBox.Text = "0";
                this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
                this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            }
            else
            {
                //this.AttnTextBox.Text = "";
                this.CurrTextBox.Text = "";
                this.CurrCodeDropDownList.SelectedValue = "null";
                this.TermDropDownList.SelectedValue = "null";
                this.CurrRateTextBox.Text = "0";
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#ffffff");
                this.PPNRateTextBox.Text = "0";
                this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                this.PPNNoTextBox.Text = "";
                this.PPNDateTextBox.Text = "";
                this.PPNPercentTextBox.Text = "0";
                //this.ppn_date_start.Attributes.Add("Style", "visibility:hidden");
                this.PPNDateLiteral.Text = "<input id='button2' type='button' Style = 'visibility:hidden' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

            }
        }

        protected void CurrCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrCodeDropDownList.SelectedValue != "null")
            {
                string _currCode = this._suppBL.GetCurr(SuppDropDownList.SelectedValue);
                string _currCodeHome = _currencyBL.GetCurrDefault();
                byte _decimalPlace = _currencyBL.GetDecimalPlace(_currCode);
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
                this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
                this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            }
            else
            {
                this.CurrRateTextBox.Text = "0";
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#ffffff");
                this.PPNRateTextBox.Text = "0";
                this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                this.PPNRateTextBox.Attributes.Add("style", "background-color:#ffffff");
            }
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            FINSuppInvConsignmentHd _suppInvConsignmentHd = new FINSuppInvConsignmentHd();

            _suppInvConsignmentHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _suppInvConsignmentHd.Status = SupplierInvoiceDataMapper.GetStatus(TransStatus.OnHold);
            _suppInvConsignmentHd.SuppCode = this.SuppDropDownList.SelectedValue;
            _suppInvConsignmentHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _suppInvConsignmentHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _suppInvConsignmentHd.PPN = Convert.ToDecimal(this.PPNPercentTextBox.Text);
            if (this.PPNDateTextBox.Text == "")
            {
                _suppInvConsignmentHd.PPNDate = null;
            }
            else
            {
                _suppInvConsignmentHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            _suppInvConsignmentHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _suppInvConsignmentHd.PPNNo = this.PPNNoTextBox.Text;
            _suppInvConsignmentHd.PPNRate = Convert.ToDecimal(this.PPNRateTextBox.Text);
            _suppInvConsignmentHd.SuppInvoice = this.SuppInvNoTextBox.Text;
            //_suppInvConsignmentHd.SuppPONo = this.SuppPONoTextBox.Text;
            _suppInvConsignmentHd.Remark = this.RemarkTextBox.Text;
            //_suppInvConsignmentHd.Attn = this.AttnTextBox.Text;
            _suppInvConsignmentHd.BaseForex = Convert.ToDecimal(this.AmountBaseTextBox.Text);
            _suppInvConsignmentHd.DiscForex = (this.DiscForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.DiscForexTextBox.Text);
            _suppInvConsignmentHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            _suppInvConsignmentHd.Term = this.TermDropDownList.SelectedValue;
            _suppInvConsignmentHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _suppInvConsignmentHd.DatePrep = DateTime.Now;

            string _result = this._finSupInvConsignBL.AddSuppInvConsignHd(_suppInvConsignmentHd);

            if (_result != "")
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
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