using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.DPCustomerList
{
    public partial class DPCustomerListEdit : DPCustomerListBase
    {
        private FINDPCustomerListBL _finDPCustListBL = new FINDPCustomerListBL();
        private CustomerBL _custBL = new CustomerBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
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
                this.DPCustListDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DPCustListDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                
                this.ShowCurrencyDDL();
                this.ShowData();

                this.ClearLabel();
                this.SetAttribute();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
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
            this.SONoTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
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

        public void ShowData()
        {
            FINDPCustList _finDPCustomerList = this._finDPCustListBL.GetSingleFINDPCustList(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finDPCustomerList.CurrCode);

            this.DPCustListCodeTextBox.Text = _finDPCustomerList.TransNmbr;
            this.FileNmbrTextBox.Text = _finDPCustomerList.FileNmbr;
            this.DPCustListDateTextBox.Text = DateFormMapper.GetValue(_finDPCustomerList.TransDate);
            //this.StatusLabel.Text = DPCustListDataMapper.GetStatusText(_finDPCustomerList.Status);
            this.CustTextBox.Text = _custBL.GetNameByCode(_finDPCustomerList.CustCode);
            this.CurrCodeDropDownList.SelectedValue = _finDPCustomerList.CurrCode;
            if (_finDPCustomerList.CurrCode.Trim().ToLower() == _currencyBL.GetCurrDefault().Trim().ToLower())
            {
                this.DisableRate();
            }
            else
            {
                this.EnableRate();
            }
            this.AttnTextBox.Text = _finDPCustomerList.Attn;
            this.SONoTextBox.Text = this._salesOrderBL.GetFileNmbrMKTSOHd(_finDPCustomerList.SONo);
            this.BaseForexTextBox.Text = _finDPCustomerList.BaseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNTextBox.Text = (_finDPCustomerList.PPn == 0) ? "0" : _finDPCustomerList.PPn.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNForexTextBox.Text = (_finDPCustomerList.PPNForex == 0) ? "0" : _finDPCustomerList.PPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TotalForexTextBox.Text = _finDPCustomerList.TotalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _finDPCustomerList.Remark;

            this.SetAttributeRate();
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

        private void ClearDataNumeric()
        {
            this.ForexRateTextBox.Text = "0";
            this.DecimalPlaceHiddenField.Value = "";
            this.BaseForexTextBox.Text = "0";
            this.PPNTextBox.Text = "0";
            this.PPNForexTextBox.Text = "0";
            this.TotalForexTextBox.Text = "0";
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

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINDPCustList _finDPCustomerList = _finDPCustListBL.GetSingleFINDPCustList(this.DPCustListCodeTextBox.Text);

            _finDPCustomerList.TransDate = new DateTime(DateFormMapper.GetValue(this.DPCustListDateTextBox.Text).Year, DateFormMapper.GetValue(this.DPCustListDateTextBox.Text).Month, DateFormMapper.GetValue(this.DPCustListDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finDPCustomerList.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _finDPCustomerList.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            //_finDPCustomerList.SONo = this.SONoDropDownList.SelectedValue;
            //_finDPCustomerList.SONo = this.SONoTextBox.Text;
            _finDPCustomerList.BaseForex = Convert.ToDecimal(this.BaseForexTextBox.Text);
            _finDPCustomerList.PPn = Convert.ToDecimal(this.PPNTextBox.Text);
            _finDPCustomerList.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _finDPCustomerList.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            _finDPCustomerList.Remark = this.RemarkTextBox.Text;
            _finDPCustomerList.UserPrep = HttpContext.Current.User.Identity.Name;
            _finDPCustomerList.DatePrep = DateTime.Now;

            bool _result = this._finDPCustListBL.EditFINDPCustList(_finDPCustomerList);

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
    }
}