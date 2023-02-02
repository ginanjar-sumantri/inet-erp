using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.SupplierInvoice
{
    public partial class SupplierInvoiceEdit : SupplierInvoiceBase
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
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.ClearLabel();
                this.SetAttribute();

                this.ShowSupplier();
                this.ShowTerm();
                //this.ShowCurrency();
                this.ShowData();
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
            //this.ViewDetailButton.Attributes.Add("OnClick", "return AskYouFirstToSave(" + this.CheckHidden.ClientID + ");");

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
        /*
        protected void ShowCurrency()
        {
            this.CurrCodeDropDownList.Items.Clear();
            this.CurrCodeDropDownList.DataTextField = "CurrCode";
            this.CurrCodeDropDownList.DataValueField = "CurrCode";
            this.CurrCodeDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrCodeDropDownList.DataBind();
            this.CurrCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }
        
        protected void CurrCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrCodeDropDownList.SelectedValue != "null")
            {
                //string _currCode = this._cust.GetCurr(CustDropDownList.SelectedValue);
                string _currCodeHome = _currencyBL.GetCurrDefault();
                decimal _tempForexRate = _currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue);
                this.ForexRateTextBox.Text = this.PPNRateTextBox.Text = (_tempForexRate == 0) ? "0" : _tempForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                if (this.CurrCodeDropDownList.SelectedValue == _currCodeHome)
                {
                    this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.ForexRateTextBox.Attributes.Remove("OnBlur");
                    this.ForexRateTextBox.Text = "1";

                    this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.PPNRateTextBox.Attributes.Remove("OnBlur");
                    this.PPNRateTextBox.Text = "1";
                }
                else
                {
                    this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                    this.ForexRateTextBox.Attributes.Add("OnBlur", "ChangeFormat(" + this.ForexRateTextBox.ClientID + ");");

                    this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                    this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                    this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat(" + this.PPNRateTextBox.ClientID + ");");
                }

                this.CurrTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrTextBox.Attributes.Add("style", "background-color:#cccccc");
                this.CurrTextBox.Text = CurrCodeDropDownList.SelectedValue;
            }
            else
            {
                this.ForexRateTextBox.Text = "0";
                this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                this.ForexRateTextBox.Attributes.Add("Style", "Background-Color:#CCCCCC");

                this.PPNRateTextBox.Text = "0";
                this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNRateTextBox.Attributes.Add("Style", "Background-Color:#CCCCCC");
            }
        }
        */
        protected void ShowData()
        {
            Billing_SupplierInvoiceHd _supplierInvoiceHd = this._supplierInvoiceBL.GetSingleSupplierInvoiceHd(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_supplierInvoiceHd.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            this.TransactionNoTextBox.Text = _supplierInvoiceHd.TransNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_supplierInvoiceHd.TransDate);
            this.SupplierDropDownList.SelectedValue = _supplierInvoiceHd.SuppCode;
            this.TermDropDownList.SelectedValue = _supplierInvoiceHd.Term;

            string _currCodeHome = _currencyBL.GetCurrDefault();
            if (_supplierInvoiceHd.CurrCode.Trim().ToLower() == _currCodeHome.Trim().ToLower())
            {
                this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                this.ForexRateTextBox.Attributes.Remove("OnBlur");
                this.ForexRateTextBox.Text = "1";

                this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                this.PPNRateTextBox.Attributes.Remove("OnBlur");
                this.PPNRateTextBox.Text = "1";
            }
            else
            {
                this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                this.ForexRateTextBox.Attributes.Add("OnBlur", "ChangeFormat(" + this.ForexRateTextBox.ClientID + ");");

                this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                this.PPNRateTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + this.PPNRateTextBox.ClientID + "," + this._imgPPNDate + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + ");");
            }

            this.CurrCodeTextbox.Text = _supplierInvoiceHd.CurrCode;
            this.ForexRateTextBox.Text = (_supplierInvoiceHd.ForexRate == 0) ? "0" : _supplierInvoiceHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempPPNPercent = Convert.ToDecimal((_supplierInvoiceHd.PPN == null) ? 0 : _supplierInvoiceHd.PPN);
            this.PPNPercentTextBox.Text = (_tempPPNPercent == 0) ? "0" : _tempPPNPercent.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            if (_tempPPNPercent > 0)
            {
                this.PPNNoTextBox.Attributes.Remove("ReadOnly");
                this.PPNNoTextBox.Attributes.Add("style", "background-color:#FFFFFF");

                this.PPNNoTextBox.Text = _supplierInvoiceHd.PPNNo;
                this.PPNDateTextBox.Text = DateFormMapper.GetValue(_supplierInvoiceHd.PPNDate);
            }
            else
            {
                this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNNoTextBox.Attributes.Add("style", "background-color:#CCCCCC");

                this.PPNNoTextBox.Text = "";
                this.PPNDateTextBox.Text = "";
            }

            decimal _tempPPNRate = Convert.ToDecimal((_supplierInvoiceHd.PPNRate == null) ? 0 : _supplierInvoiceHd.PPNRate);
            this.PPNRateTextBox.Text = (_tempPPNRate == 0) ? "0" : _tempPPNRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            this.CurrTextBox.Text = _supplierInvoiceHd.CurrCode;

            decimal _tempBaseForex = Convert.ToDecimal((_supplierInvoiceHd.BaseForex == null) ? 0 : _supplierInvoiceHd.BaseForex);
            this.AmountBaseTextBox.Text = (_tempBaseForex == 0) ? "0" : _tempBaseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempDiscPercent = Convert.ToDecimal((_supplierInvoiceHd.DiscPercent == null) ? 0 : _supplierInvoiceHd.DiscPercent);
            this.DiscPercentTextBox.Text = (_tempDiscPercent == 0) ? "0" : _tempDiscPercent.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            if (_supplierInvoiceHd.DiscPercent != 0)
            {
                this.DiscForexTextBox.Attributes.Add("ReadOnly", "True");
                this.DiscForexTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            }
            else
            {
                this.DiscForexTextBox.Attributes.Remove("ReadOnly");
                this.DiscForexTextBox.Attributes.Add("style", "background-color:#FFFFFF");
            }
            decimal _tempDiscForex = Convert.ToDecimal((_supplierInvoiceHd.DiscForex == null) ? 0 : _supplierInvoiceHd.DiscForex);
            this.DiscForexTextBox.Text = (_tempDiscForex == 0) ? "0" : _tempDiscForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempPPNForex = Convert.ToDecimal((_supplierInvoiceHd.PPNForex == null) ? 0 : _supplierInvoiceHd.PPNForex);
            this.PPNForexTextBox.Text = (_tempPPNForex == 0) ? "0" : _tempPPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempOtherFee = Convert.ToDecimal((_supplierInvoiceHd.OtherFee == null) ? 0 : _supplierInvoiceHd.OtherFee);
            this.OtherFeeTextBox.Text = (_tempOtherFee == 0) ? "0" : _tempOtherFee.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempStampFee = Convert.ToDecimal((_supplierInvoiceHd.StampFee == null) ? 0 : _supplierInvoiceHd.StampFee);
            this.StampFeeTextBox.Text = (_tempStampFee == 0) ? "0" : _tempStampFee.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempTotalForex = Convert.ToDecimal((_supplierInvoiceHd.TotalForex == null) ? 0 : _supplierInvoiceHd.TotalForex);
            this.TotalForexTextBox.Text = (_tempTotalForex == 0) ? "0" : _tempTotalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            this.RemarkTextBox.Text = _supplierInvoiceHd.Remark;

            this.SetAttributeRate();

        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            Billing_SupplierInvoiceHd _supplierInvoiceHd = this._supplierInvoiceBL.GetSingleSupplierInvoiceHd(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            _supplierInvoiceHd.TransNmbr = this.TransactionNoTextBox.Text;
            //_supplierInvoiceHd.Status = SupplierInvoiceDataMapper.GetStatus(TransStatus.Hold);
            _supplierInvoiceHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _supplierInvoiceHd.SuppCode = this.SupplierDropDownList.SelectedValue;
            _supplierInvoiceHd.CurrCode = this.CurrCodeTextbox.Text;
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

            _supplierInvoiceHd.EditBy = HttpContext.Current.User.Identity.Name;
            _supplierInvoiceHd.EditDate = DateTime.Now;

            bool _result = this._supplierInvoiceBL.EditSupplierInvoiceHd(_supplierInvoiceHd);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Edit Data";
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

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.Page.IsValid == true)
            {
                Billing_SupplierInvoiceHd _supplierInvoiceHd = this._supplierInvoiceBL.GetSingleSupplierInvoiceHd(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

                _supplierInvoiceHd.TransNmbr = this.TransactionNoTextBox.Text;
                //_supplierInvoiceHd.Status = SupplierInvoiceDataMapper.GetStatus(TransStatus.Hold);
                _supplierInvoiceHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                _supplierInvoiceHd.SuppCode = this.SupplierDropDownList.SelectedValue;
                _supplierInvoiceHd.CurrCode = this.CurrCodeTextbox.Text;
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

                _supplierInvoiceHd.EditBy = HttpContext.Current.User.Identity.Name;
                _supplierInvoiceHd.EditDate = DateTime.Now;

                bool _result = this._supplierInvoiceBL.EditSupplierInvoiceHd(_supplierInvoiceHd);

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