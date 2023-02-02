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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.DirectSales
{
    public partial class DirectSalesEdit : DirectSalesBase
    {
        private CustomerBL _custBL = new CustomerBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private TermBL _termBL = new TermBL();
        private DirectSalesBL _directSalesBL = new DirectSalesBL();
        private PermissionBL _permBL = new PermissionBL();
        private PaymentBL _paymentBL = new PaymentBL();

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
                this.DateTextBox.Attributes.Add("ReadOnly", "True");

                String spawnJS = "<script language='JavaScript'>\n";

                ////////////////////ONCHANGE DISCOUNT AMOUNT
                spawnJS += "function setDiscAmount() {\n";
                spawnJS += "document.getElementById('" + this.DiscAmountTextBox.ClientID + "').value = Math.round(";
                spawnJS += "document.getElementById('" + this.DiscPercentTextBox.ClientID + "').value * ";
                spawnJS += "document.getElementById('" + this.BaseForexHiddenField.ClientID + "').value / 100 ) ;";
                spawnJS += "}\n";

                spawnJS += "</script>\n";
                this.javascriptReceiver.Text = spawnJS;

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.ClearLabel();
                this.ShowData();
                this.SetAttribute();
            }
            this.showBoughtItem();
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void showBoughtItem()
        {
            if (this.DiscAmountTextBox.Text != "0" && this.BaseForexTextBox.Text != "0")
            {
                this.DiscPercentTextBox.Text = Math.Round(Convert.ToDecimal(this.DiscAmountTextBox.Text) / Convert.ToDecimal(this.BaseForexTextBox.Text) * 100, 2).ToString();
                this.PPNAmountTextBox.Text = ((Convert.ToDecimal(this.BaseForexTextBox.Text) - (Convert.ToDecimal(this.DiscAmountTextBox.Text))) * Convert.ToDecimal(this.PPNPercentTextBox.Text) / 100).ToString("#,##0.00");
                this.TotalAmountTextBox.Text = ((Convert.ToDecimal(this.BaseForexTextBox.Text) - (Convert.ToDecimal(this.DiscAmountTextBox.Text))) + Convert.ToDecimal(this.PPNAmountTextBox.Text) + Convert.ToDecimal(this.StampFeeTextBox.Text) + Convert.ToDecimal(this.OtherFeeTextBox.Text)).ToString("#,##0.00");
            }
        }

        private void ShowPayType()
        {
            SALTrDirectSalesHd _directSalesHd = this._directSalesBL.GetSingleDirectSalesHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.PayTypeDropDownList.Items.Clear();
            this.PayTypeDropDownList.DataTextField = "PayName";
            this.PayTypeDropDownList.DataValueField = "PayCode";
            this.PayTypeDropDownList.DataSource = this._paymentBL.GetListDDLDPCustomer(_directSalesHd.CurrCode);
            this.PayTypeDropDownList.DataBind();
            this.PayTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNAmountTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalAmountTextBox.Attributes.Add("ReadOnly", "True");

            this.PPNPercentTextBox.Attributes.Add("onkeyup", "numericInput(this)");
            this.PPNPercentTextBox.Attributes.Add("onchange", "numericInput(this);document.forms[0].submit()");
            this.PPNAmountTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");

            this.StampFeeTextBox.Attributes.Add("onkeyup", "numericInput(this)");
            this.StampFeeTextBox.Attributes.Add("onchange", "numericInput(this);document.forms[0].submit()");

            this.OtherFeeTextBox.Attributes.Add("onkeyup", "numericInput(this)");
            this.OtherFeeTextBox.Attributes.Add("onchange", "numericInput(this);document.forms[0].submit()");

            this.DiscPercentTextBox.Attributes.Add("onkeyup", "numericInput(this)");
            this.DiscPercentTextBox.Attributes.Add("onkeydown", "if(event.keyCode==13){return setDiscAmount();}");
            this.DiscPercentTextBox.Attributes.Add("onchange", "numericInput(this);setDiscAmount();document.forms[0].submit()");
            this.DiscAmountTextBox.Attributes.Add("onkeyup", "numericInput(this)");
            this.DiscAmountTextBox.Attributes.Add("onchange", "numericInput(this);document.forms[0].submit()");

            this.ForexRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
        }

        public void ShowData()
        {
            decimal _discAmount = 0;
            decimal _ppnAmount = 0;

            SALTrDirectSalesHd _directSalesHd = this._directSalesBL.GetSingleDirectSalesHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            if (_directSalesHd.DiscAmount != 0 && _directSalesHd.BaseForex != 0)
            {
                _discAmount = Math.Round((_directSalesHd.DiscAmount / _directSalesHd.BaseForex) * 100, 2);
                _ppnAmount = Math.Round((_directSalesHd.PPNAmount / (_directSalesHd.BaseForex - _directSalesHd.DiscAmount)) * 100, 2);
            }
             
            this.TransNoTextBox.Text = _directSalesHd.TransNmbr;
            this.FileNmbrTextBox.Text = _directSalesHd.FileNo;
            this.DateTextBox.Text = DateFormMapper.GetValue(_directSalesHd.Date);
            this.CustTextBox.Text = _directSalesHd.CustCode;
            this.StatusLabel.Text = DirectSalesDataMapper.GetStatusText(_directSalesHd.Status);
            this.RemarkTextBox.Text = _directSalesHd.Remark;
            this.CurrCodeTextBox.Text = _directSalesHd.CurrCode;
            this.ForexRateTextBox.Text = _directSalesHd.ForexRate.ToString("#,##0");
            this.BaseForexTextBox.Text = _directSalesHd.BaseForex.ToString("#,##0.00");
            this.DiscAmountTextBox.Text = _directSalesHd.DiscAmount.ToString("#,##0.00");
            this.BaseForexHiddenField.Value = _directSalesHd.BaseForex.ToString("0.00");
            this.DiscPercentTextBox.Text = Convert.ToDecimal(_directSalesHd.DiscPercent).ToString("#,##0.00");
            this.PPNPercentTextBox.Text = Convert.ToDecimal(_directSalesHd.PPNPercent).ToString("#,##0.00");
            this.PPNAmountTextBox.Text = _directSalesHd.PPNAmount.ToString("#,##0.00");
            this.StampFeeTextBox.Text = Convert.ToDecimal(_directSalesHd.StampFee).ToString("#,##0.00");
            this.OtherFeeTextBox.Text = Convert.ToDecimal(_directSalesHd.OtherFee).ToString("#,##0.00");
            this.ShowPayType();
            this.PayTypeDropDownList.SelectedValue = _directSalesHd.PayCode;
            this.TotalAmountTextBox.Text = _directSalesHd.TotalAmount.ToString("#,##0,00");
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            SALTrDirectSalesHd _directSalesHd = this._directSalesBL.GetSingleDirectSalesHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _directSalesHd.Remark = this.RemarkTextBox.Text;
            _directSalesHd.PayCode = this.PayTypeDropDownList.SelectedValue;
            _directSalesHd.Date = DateFormMapper.GetValue(this.DateTextBox.Text);
            _directSalesHd.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _directSalesHd.DiscPercent = Convert.ToDecimal(this.DiscPercentTextBox.Text);
            _directSalesHd.DiscAmount = Convert.ToDecimal(this.DiscAmountTextBox.Text);
            _directSalesHd.PPNPercent = Convert.ToDecimal(this.PPNPercentTextBox.Text);
            _directSalesHd.PPNAmount = Convert.ToDecimal(this.PPNAmountTextBox.Text);
            _directSalesHd.StampFee = Convert.ToDecimal(this.StampFeeTextBox.Text);
            _directSalesHd.OtherFee = Convert.ToDecimal(this.OtherFeeTextBox.Text);
            _directSalesHd.TotalAmount = Convert.ToDecimal(this.TotalAmountTextBox.Text);
            _directSalesHd.EditBy = HttpContext.Current.User.Identity.Name;
            _directSalesHd.EditDate = DateTime.Now;

            bool _result = this._directSalesBL.EditDirectSalesHd(_directSalesHd);

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
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.Page.IsValid == true)
            {
                SALTrDirectSalesHd _directSalesHd = this._directSalesBL.GetSingleDirectSalesHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                _directSalesHd.Remark = this.RemarkTextBox.Text;
                _directSalesHd.PayCode = this.PayTypeDropDownList.SelectedValue;
                _directSalesHd.Date = DateFormMapper.GetValue(this.DateTextBox.Text);
                _directSalesHd.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
                _directSalesHd.DiscPercent = Convert.ToDecimal(this.DiscPercentTextBox.Text);
                _directSalesHd.DiscAmount = Convert.ToDecimal(this.DiscAmountTextBox.Text);
                _directSalesHd.PPNPercent = Convert.ToDecimal(this.PPNPercentTextBox.Text);
                _directSalesHd.PPNAmount = Convert.ToDecimal(this.PPNAmountTextBox.Text);
                _directSalesHd.StampFee = Convert.ToDecimal(this.StampFeeTextBox.Text);
                _directSalesHd.OtherFee = Convert.ToDecimal(this.OtherFeeTextBox.Text);
                _directSalesHd.TotalAmount = Convert.ToDecimal(this.TotalAmountTextBox.Text);
                _directSalesHd.EditBy = HttpContext.Current.User.Identity.Name;
                _directSalesHd.EditDate = DateTime.Now;

                bool _result = this._directSalesBL.EditDirectSalesHd(_directSalesHd);

                if (_result == true)
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "You Failed Edit Data";
                }
            }
        }
    }
}