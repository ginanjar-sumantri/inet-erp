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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.DPCustomerReceipt
{
    public partial class DPCustReceiptAdd : DPCustomerReceiptBase
    {
        private CustomerBL _custBL = new CustomerBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private FINDPCustomerBL _finDPCustBL = new FINDPCustomerBL();
        private FINDPCustomerListBL _finDPCustListBL = new FINDPCustomerListBL();
        private SalesOrderBL _salesOrderBL = new SalesOrderBL();
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
                this.PPNDateLiteral.Text = "<input id='button2' type='button' Style='visibility:visible' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowCust();

                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.PPNTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2"/*this.ppn_date_start.ClientID*/ + "," + this.BaseForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "  );");
            this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + " );");
        }

        protected void SetAttribute()
        {


            this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrCodeTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrTextBox.Attributes.Add("ReadOnly", "True");
            this.BaseForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.SONoTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");

            this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");

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

        public void ShowDPList()
        {
            this.DPListNoDropDownList.Items.Clear();
            this.DPListNoDropDownList.DataTextField = "FileNmbr";
            this.DPListNoDropDownList.DataValueField = "TransNmbr";
            this.DPListNoDropDownList.DataSource = this._finDPCustListBL.GetListDPCustListForDDL(this.CustDropDownList.SelectedValue);
            this.DPListNoDropDownList.DataBind();
            this.DPListNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ClearLabel();

            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            this.CustDropDownList.SelectedValue = "null";
            this.AttnTextBox.Text = "";
            this.DPListNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.DPListNoDropDownList.SelectedValue = "null";
            this.SONoTextBox.Text = "";
            this.CurrCodeTextBox.Text = "";
            this.CurrRateTextBox.Text = "";
            this.PPNNoTextBox.Text = "";
            this.PPNDateTextBox.Text = "";
            this.PPNRateTextBox.Text = "";
            this.CurrTextBox.Text = "";
            this.BaseForexTextBox.Text = "0";
            this.TotalForexTextBox.Text = "0";
            this.PPNTextBox.Text = "0";
            this.PPNForexTextBox.Text = "0";
            this.RemarkTextBox.Text = "";
        }

        protected void CustDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CustDropDownList.SelectedValue != "null")
            {
                this.AttnTextBox.Text = _custBL.GetCustContact(this.CustDropDownList.SelectedValue);
                this.ShowDPList();
                this.SoNoHiddenField.Value = "";
                this.SONoTextBox.Text = "";
                this.CurrCodeTextBox.Text = "";
                this.CurrRateTextBox.Text = "";
                this.CurrTextBox.Text = "";
                this.PPNRateTextBox.Text = "";
                this.BaseForexTextBox.Text = "0";
                this.PPNForexTextBox.Text = "0";
                this.TotalForexTextBox.Text = "0";
            }
            else
            {
                this.AttnTextBox.Text = "";
                this.SoNoHiddenField.Value = "";
                this.SONoTextBox.Text = "";
                this.CurrCodeTextBox.Text = "";
                this.CurrRateTextBox.Text = "";
                this.CurrTextBox.Text = "";
                this.PPNRateTextBox.Text = "";
                this.BaseForexTextBox.Text = "";
                this.PPNForexTextBox.Text = "";
                this.TotalForexTextBox.Text = "";
                this.DPListNoDropDownList.Items.Clear();
                this.DPListNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.DPListNoDropDownList.SelectedValue = "null";
            }
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            FINDPCustHd _finDPCustHd = new FINDPCustHd();

            _finDPCustHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finDPCustHd.Status = DPCustomerDataMapper.GetStatus(TransStatus.OnHold);
            _finDPCustHd.CustCode = this.CustDropDownList.SelectedValue;
            _finDPCustHd.Attn = this.AttnTextBox.Text;
            _finDPCustHd.DPListNo = this.DPListNoDropDownList.SelectedValue;
            _finDPCustHd.SONo = this.SoNoHiddenField.Value;
            _finDPCustHd.CurrCode = this.CurrCodeTextBox.Text;
            _finDPCustHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finDPCustHd.Remark = this.RemarkTextBox.Text;
            _finDPCustHd.PPNNo = this.PPNNoTextBox.Text;
            if (this.PPNDateTextBox.Text != "")
            {
                _finDPCustHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            else
            {
                _finDPCustHd.PPNDate = null;
            }
            _finDPCustHd.PPNRate = (this.PPNRateTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNRateTextBox.Text);
            _finDPCustHd.BaseForex = (this.BaseForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.BaseForexTextBox.Text);
            _finDPCustHd.PPN = (this.PPNTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNTextBox.Text);
            _finDPCustHd.PPNForex = (this.PPNForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNForexTextBox.Text);
            _finDPCustHd.TotalForex = (this.TotalForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text);
            _finDPCustHd.Balance = 0;
            _finDPCustHd.BalancePPn = 0;
            _finDPCustHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finDPCustHd.DatePrep = DateTime.Now;

            string _result = this._finDPCustBL.AddFINDPCustHd(_finDPCustHd);

            if (_result != "")
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.ClearLabel();
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

        protected void DPListNoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _currCodeHome = _currencyBL.GetCurrDefault();

            if (DPListNoDropDownList.SelectedValue != "null")
            {
                FINDPCustList _finDPCustList = this._finDPCustListBL.GetSingleFINDPCustList(this.DPListNoDropDownList.SelectedValue);
                byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finDPCustList.CurrCode);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                this.SoNoHiddenField.Value = _finDPCustList.SONo;
                this.SONoTextBox.Text = _salesOrderBL.GetFileNmbrMKTSOHd(_finDPCustList.SONo);
                this.CurrCodeTextBox.Text = _finDPCustList.CurrCode;
                this.CurrTextBox.Text = _finDPCustList.CurrCode;
                this.CurrRateTextBox.Text = _finDPCustList.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.PPNRateTextBox.Text = _finDPCustList.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.PPNTextBox.Text = (_finDPCustList.PPn == 0) ? "0" : _finDPCustList.PPn.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.PPNForexTextBox.Text = (_finDPCustList.PPNForex == 0) ? "0" : _finDPCustList.PPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.BaseForexTextBox.Text = (_finDPCustList.BaseForex == 0) ? "0" : _finDPCustList.BaseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.TotalForexTextBox.Text = (_finDPCustList.TotalForex == 0) ? "0" : _finDPCustList.TotalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            else
            {
                this.SoNoHiddenField.Value = "";
                this.SONoTextBox.Text = "";
                this.CurrCodeTextBox.Text = "";
                this.CurrTextBox.Text = "";
                this.CurrRateTextBox.Text = "";
                this.PPNRateTextBox.Text = "";
                this.PPNTextBox.Text = "0";
                this.PPNForexTextBox.Text = "0";
                this.BaseForexTextBox.Text = "0";
                this.TotalForexTextBox.Text = "0";
            }
            if (this.CurrCodeTextBox.Text == _currCodeHome || this.CurrCodeTextBox.Text == "")
            {
                this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            }
            else
            {
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
            }
            if (this.PPNTextBox.Text == "0" || this.PPNTextBox.Text == "0.00" || this.PPNTextBox.Text == "")
            {
                this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNNoTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
                //this.ppn_date_start.Attributes.Add("Style", "visibility:hidden");
                this.PPNDateLiteral.Text = "<input id='button2' type='button' Style='visibility:hidden' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
            }
            else
            {
                this.PPNNoTextBox.Attributes.Remove("ReadOnly");
                this.PPNNoTextBox.Attributes.Add("Style", "background-color:#FFFFFF");
                //this.ppn_date_start.Attributes.Add("Style", "visibility:visible");
                this.PPNDateLiteral.Text = "<input id='button2' type='button' Style='visibility:visible' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
            }
        }
    }
}