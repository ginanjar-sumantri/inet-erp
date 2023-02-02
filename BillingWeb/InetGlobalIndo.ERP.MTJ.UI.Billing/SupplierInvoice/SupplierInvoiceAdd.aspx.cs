using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.SupplierInvoice
{
    public partial class SupplierInvoiceAdd : SupplierInvoiceBase
    {
        private SupplierInvoiceBL _supplierInvoiceBL = new SupplierInvoiceBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private TermBL _termBL = new TermBL();
        private SupplierBL _supplierBL = new SupplierBL();
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

                this.ClearLabel();
                this.SetAttribute();

                this.ShowSupplier();
                this.ShowTerm();
                this.ShowCurrency();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.ForexRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.ForexRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + this._imgPPNDate + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscPercentTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.StampFeeTextBox.ClientID + "," + this.OtherFeeTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.DiscPercentTextBox.Attributes.Add("OnBlur", "CalculateDiscountPercent(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + this._imgPPNDate + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscPercentTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.StampFeeTextBox.ClientID + "," + this.OtherFeeTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.DiscForexTextBox.Attributes.Add("OnBlur", "CalculateDiscountForex(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + this._imgPPNDate + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscPercentTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.StampFeeTextBox.ClientID + "," + this.OtherFeeTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.StampFeeTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + this._imgPPNDate + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscPercentTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.StampFeeTextBox.ClientID + "," + this.OtherFeeTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.OtherFeeTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + this._imgPPNDate + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscPercentTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.StampFeeTextBox.ClientID + "," + this.OtherFeeTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");

            this.AmountBaseTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.SetAttributeRate();
        }

        protected void ShowSupplier()
        {
            this.SupplierDropDownList.Items.Clear();
            this.SupplierDropDownList.DataTextField = "SuppName";
            this.SupplierDropDownList.DataValueField = "SuppCode";
            this.SupplierDropDownList.DataSource = this._supplierBL.GetListDDLSuppForReport();
            this.SupplierDropDownList.DataBind();
            this.SupplierDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowTerm()
        {
            this.TermDropDownList.Items.Clear();
            this.TermDropDownList.DataTextField = "TermName";
            this.TermDropDownList.DataValueField = "TermCode";
            this.TermDropDownList.DataSource = this._termBL.GetListTermForDDL();
            this.TermDropDownList.DataBind();
            this.TermDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowCurrency()
        {
            this.CurrCodeDropDownList.Items.Clear();
            this.CurrCodeDropDownList.DataTextField = "CurrCode";
            this.CurrCodeDropDownList.DataValueField = "CurrCode";
            this.CurrCodeDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrCodeDropDownList.DataBind();
            this.CurrCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearData()
        {
            this.ClearLabel();
            DateTime _now = DateTime.Now;

            this.TransactionNoTextBox.Text = "";
            this.DateTextBox.Text = DateFormMapper.GetValue(_now);
            this.SupplierDropDownList.SelectedValue = "null";
            this.TermDropDownList.SelectedValue = "null";
            this.CurrCodeDropDownList.SelectedValue = "null";
            this.ForexRateTextBox.Text = "0";
            this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
            this.ForexRateTextBox.Attributes.Add("Style", "Background-Color:#CCCCCC");

            this.PPNPercentTextBox.Text = "0";

            this.PPNNoTextBox.Text = "";
            this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNNoTextBox.Attributes.Add("Style", "Background-Color:#CCCCCC");

            this.PPNDateTextBox.Text = "";

            this.PPNForexTextBox.Text = "0";

            this.PPNRateTextBox.Text = "0";
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("Style", "Background-Color:#CCCCCC");

            this.CurrTextBox.Text = "";
            this.AmountBaseTextBox.Text = "0";
            this.DiscForexTextBox.Text = "0";
            this.DiscPercentTextBox.Text = "0";
            this.PPNForexTextBox.Text = "0";
            this.OtherFeeTextBox.Text = "0";
            this.StampFeeTextBox.Text = "0";
            this.TotalForexTextBox.Text = "0";
        }

        private void ClearDataNumeric()
        {
            this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNNoTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");

            this.ForexRateTextBox.Text = "0";
            this.DecimalPlaceHiddenField.Value = "";
            this.PPNPercentTextBox.Text = "0";
            this.PPNNoTextBox.Text = "";
            this.PPNDateTextBox.Text = "";
            this.PPNRateTextBox.Text = "0";
            this.CurrTextBox.Text = "";
            this.AmountBaseTextBox.Text = "0";
            this.DiscForexTextBox.Text = "0";
            this.DiscPercentTextBox.Text = "0";
            this.PPNForexTextBox.Text = "0";
            this.OtherFeeTextBox.Text = "0";
            this.StampFeeTextBox.Text = "0";
            this.TotalForexTextBox.Text = "0";
        }

        private void DisableRate()
        {
            this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
            this.ForexRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.ForexRateTextBox.Text = "1";
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.PPNRateTextBox.Text = "1";
        }

        private void EnableRate()
        {
            this.ForexRateTextBox.Attributes.Remove("ReadOnly");
            this.ForexRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
            this.PPNRateTextBox.Attributes.Remove("ReadOnly");
            this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
        }

        private void SetCurrRate()
        {
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrCodeDropDownList.SelectedValue);
            this.ForexRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNRateTextBox.Text = this.ForexRateTextBox.Text;
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

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            Billing_SupplierInvoiceHd _supplierInvoiceHd = new Billing_SupplierInvoiceHd();

            _supplierInvoiceHd.SupplierInvoiceHdCode = Guid.NewGuid();
            _supplierInvoiceHd.TransNmbr = this.TransactionNoTextBox.Text;
            _supplierInvoiceHd.Status = SupplierInvoiceDataMapper.GetStatus(TransStatus.OnHold);
            _supplierInvoiceHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _supplierInvoiceHd.SuppCode = this.SupplierDropDownList.SelectedValue;
            _supplierInvoiceHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _supplierInvoiceHd.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _supplierInvoiceHd.Term = this.TermDropDownList.SelectedValue;
            _supplierInvoiceHd.PPNNo = this.PPNNoTextBox.Text;

            if (this.PPNDateTextBox.Text != "")
            {
                _supplierInvoiceHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            else
            {
                _supplierInvoiceHd.PPNDate = null;
            }

            _supplierInvoiceHd.PPNRate = Convert.ToDecimal(this.PPNRateTextBox.Text);
            _supplierInvoiceHd.BaseForex = Convert.ToDecimal(this.AmountBaseTextBox.Text);
            _supplierInvoiceHd.PPN = Convert.ToDecimal(this.PPNPercentTextBox.Text);
            _supplierInvoiceHd.DiscPercent = Convert.ToDecimal(this.DiscPercentTextBox.Text);
            _supplierInvoiceHd.DiscForex = Convert.ToDecimal(this.DiscForexTextBox.Text);
            _supplierInvoiceHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _supplierInvoiceHd.OtherFee = Convert.ToDecimal(this.OtherFeeTextBox.Text);
            _supplierInvoiceHd.StampFee = Convert.ToDecimal(this.StampFeeTextBox.Text);
            _supplierInvoiceHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            _supplierInvoiceHd.Remark = this.RemarkTextBox.Text;

            _supplierInvoiceHd.InsertBy = HttpContext.Current.User.Identity.Name;
            _supplierInvoiceHd.InsertDate = DateTime.Now;
            _supplierInvoiceHd.EditBy = HttpContext.Current.User.Identity.Name;
            _supplierInvoiceHd.EditDate = DateTime.Now;

            bool _result = this._supplierInvoiceBL.AddSupplierInvoiceHd(_supplierInvoiceHd);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_supplierInvoiceHd.SupplierInvoiceHdCode.ToString(), ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
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