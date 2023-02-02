using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.DPCustomerList
{
    public partial class DPCustomerListAdd : DPCustomerListBase
    {
        private FINDPCustomerListBL _finDPCustListBL = new FINDPCustomerListBL();
        private CustomerBL _custBL = new CustomerBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private SalesOrderBL _soBL = new SalesOrderBL();
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
                this.DPCustListDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DPCustListDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowCustomerDDL();
                this.ShowCurrencyDDL();

                this.SetAttribute();
                this.ClearData();
            }
        }

        private void SetAttributeRate()
        {
            this.ForexRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.ForexRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.BaseForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNTextBox.ClientID + "," + this.BaseForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNTextBox.ClientID + "," + this.BaseForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        protected void SetAttribute()
        {
            this.DPCustListDateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.SetAttributeRate();
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            this.ClearLabel();
            //this.DPCustListCodeTextBox.Text = "";
            this.DPCustListDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.CustCodeDropDownList.SelectedValue = "null";
            this.CurrCodeDropDownList.SelectedValue = "null";
            this.ForexRateTextBox.Text = "0";
            this.AttnTextBox.Text = "";
            //this.SONoTextBox.Text = "";
            this.SONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.SONoDropDownList.SelectedValue = "null";
            this.BaseForexTextBox.Text = "0";
            this.PPNTextBox.Text = "0";
            this.PPNForexTextBox.Text = "0";
            this.TotalForexTextBox.Text = "0";
            this.RemarkTextBox.Text = "";
        }

        private void ShowCustomerDDL()
        {
            this.CustCodeDropDownList.Items.Clear();
            this.CustCodeDropDownList.DataTextField = "CustName";
            this.CustCodeDropDownList.DataValueField = "CustCode";
            this.CustCodeDropDownList.DataSource = this._custBL.GetListCustomerForDDL();
            this.CustCodeDropDownList.DataBind();
            this.CustCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowCurrencyDDL()
        {
            this.CurrCodeDropDownList.Items.Clear();
            this.CurrCodeDropDownList.DataTextField = "CurrCode";
            this.CurrCodeDropDownList.DataValueField = "CurrCode";
            this.CurrCodeDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrCodeDropDownList.DataBind();
            this.CurrCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowSODLL()
        {
            this.SONoDropDownList.Items.Clear();
            this.SONoDropDownList.DataTextField = "FileNmbr";
            this.SONoDropDownList.DataValueField = "TransNmbr";
            this.SONoDropDownList.DataSource = this._soBL.GetListMKTSOForDDLByCustCode(this.CustCodeDropDownList.SelectedValue);
            this.SONoDropDownList.DataBind();
            this.SONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ClearDataNumeric()
        {
            this.ForexRateTextBox.Text = "0";
            this.DecimalPlaceHiddenField.Value = "";
            this.BaseForexTextBox.Text = "0";
            this.PPNTextBox.Text = "0";
            this.PPNForexTextBox.Text = "0";
            this.TotalForexTextBox.Text = "0";
        }

        private void DisableRate()
        {
            this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
            this.ForexRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.ForexRateTextBox.Text = "1";
        }

        private void EnableRate()
        {
            this.ForexRateTextBox.Attributes.Remove("ReadOnly");
            this.ForexRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
        }

        private void SetCurrRate()
        {
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrCodeDropDownList.SelectedValue);
            this.ForexRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
  
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

        protected void CustCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearDataNumeric();

            if (this.CustCodeDropDownList.SelectedValue != "null")
            {
                MsCustomer _msCust = _custBL.GetSingleCust(this.CustCodeDropDownList.SelectedValue);

                this.CurrCodeDropDownList.SelectedValue = _msCust.CurrCode;
                this.AttnTextBox.Text = _msCust.ContactName;

                this.ShowSODLL();
                this.SetCurrRate();
            }
            else
            {
                this.SONoDropDownList.Items.Clear();
                this.SONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

                this.ClearDataNumeric();
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

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            FINDPCustList _finDPCustList = new FINDPCustList();

            _finDPCustList.TransDate = new DateTime(DateFormMapper.GetValue(this.DPCustListDateTextBox.Text).Year, DateFormMapper.GetValue(this.DPCustListDateTextBox.Text).Month, DateFormMapper.GetValue(this.DPCustListDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finDPCustList.Status = DPCustListDataMapper.GetStatus(TransStatus.OnHold);
            _finDPCustList.CustCode = this.CustCodeDropDownList.SelectedValue;
            _finDPCustList.CustName = this._custBL.GetNameByCode(this.CustCodeDropDownList.SelectedValue);
            _finDPCustList.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _finDPCustList.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _finDPCustList.Attn = this.AttnTextBox.Text;
            //_finDPCustList.SONo = this.SONoDropDownList.SelectedValue;
            _finDPCustList.SONo = this.SONoDropDownList.SelectedValue;
            _finDPCustList.BaseForex = Convert.ToDecimal(this.BaseForexTextBox.Text);
            _finDPCustList.PPn = Convert.ToDecimal(this.PPNTextBox.Text);
            _finDPCustList.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _finDPCustList.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            _finDPCustList.Remark = this.RemarkTextBox.Text;
            _finDPCustList.UserPrep = HttpContext.Current.User.Identity.Name;
            _finDPCustList.DatePrep = DateTime.Now;
            _finDPCustList.DoneDPReceipt = YesNoDataMapper.GetYesNo(YesNo.No);

            string _result = this._finDPCustListBL.AddFINDPCustList(_finDPCustList);

            if (_result != "")
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
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
            this.SONoDropDownList.Items.Clear();
            this.SONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

            this.ClearData();
        }

        protected void SONoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SONoDropDownList.SelectedValue != "null")
            {
                MKTSOHd _soHd = _soBL.GetSingleMKTSOHd(this.SONoDropDownList.SelectedValue, _soBL.GetLastRevisiMKTSOHd(this.SONoDropDownList.SelectedValue));

                this.CurrCodeDropDownList.SelectedValue = _soHd.CurrCode;
                this.CurrCodeDropDownList.Enabled = false;

                byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrCodeDropDownList.SelectedValue);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                this.ForexRateTextBox.Text = _soHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                this.ForexRateTextBox.Attributes.Add("Style", "background-color: #CCCCCC");

                decimal _dpForex = Convert.ToDecimal((_soHd.DPForex == null) ? 0 : _soHd.DPForex);
                this.TotalForexTextBox.Text = (_dpForex == 0) ? "0" : _dpForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.PPNTextBox.Text = (_soHd.PPN == 0) ? "0" : _soHd.PPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                decimal _ppnForex = Math.Round(_dpForex / (100+_soHd.PPN) * _soHd.PPN, _decimalPlace);
                decimal _baseForex = Math.Round(_dpForex / ((100 + _soHd.PPN) / 100), _decimalPlace);

                this.PPNForexTextBox.Text = (_ppnForex == 0) ? "0" : _ppnForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.BaseForexTextBox.Text = (_baseForex == 0) ? "0" : _baseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            else
            {
                this.CurrCodeDropDownList.SelectedValue = "null";
                this.CurrCodeDropDownList.Enabled = true;
                this.ForexRateTextBox.Text = "";
                this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                this.ForexRateTextBox.Attributes.Add("Style", "background-color: #FFFFFF");

                this.PPNTextBox.Text = "0";
                this.TotalForexTextBox.Text = "0";

                this.PPNForexTextBox.Text = "0";
                this.BaseForexTextBox.Text = "0";
            }
        }
    }
}