using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.SupplierNote
{
    public partial class SupplierNoteEdit : SupplierNoteBase
    {
        private SupplierBL _suppBL = new SupplierBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private TermBL _termBL = new TermBL();
        private SupplierNoteBL _supplierNoteBL = new SupplierNoteBL();
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
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.PPNDateLiteral.Text = "<input id='button2' Style = 'visibility:visible' type='button' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
            }
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.PPNPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2" /*this.ppn_date_start.ClientID*/ + "," + this.PPhPercentTextBox.ClientID + "," + this.PPhForexTextBox.ClientID + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.OtherForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPhPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2" /*this.ppn_date_start.ClientID*/ + "," + this.PPhPercentTextBox.ClientID + "," + this.PPhForexTextBox.ClientID + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.OtherForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.DiscForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2" /*this.ppn_date_start.ClientID*/ + "," + this.PPhPercentTextBox.ClientID + "," + this.PPhForexTextBox.ClientID + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.OtherForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.OtherForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2" /*this.ppn_date_start.ClientID*/+ "," + this.PPhPercentTextBox.ClientID + "," + this.PPhForexTextBox.ClientID + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.OtherForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        private void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountBaseTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPhForexTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.SetAttributeRate();
        }

        public void ShowData()
        {
            FINSuppInvHd _finSuppInvHd = this._supplierNoteBL.GetSingleFINSuppInvHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finSuppInvHd.CurrCode);

            this.TransNoTextBox.Text = _finSuppInvHd.TransNmbr;
            this.FileNmbrTextBox.Text = _finSuppInvHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_finSuppInvHd.TransDate);
            this.SuppTextBox.Text = _suppBL.GetSuppNameByCode(_finSuppInvHd.SuppCode);
            this.CurrCodeTextBox.Text = _finSuppInvHd.CurrCode;
            if (_finSuppInvHd.CurrCode.Trim().ToLower() == this._currencyBL.GetCurrDefault().Trim().ToLower())
            {
                this.DisableRate();
            }
            else
            {
                this.EnableRate();
            }
            this.CurrRateTextBox.Text = _finSuppInvHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.TermTextBox.Text = _termBL.GetTermNameByCode(_finSuppInvHd.Term);
            this.CurrTextBox.Text = _finSuppInvHd.CurrCode;
            this.RemarkTextBox.Text = _finSuppInvHd.Remark;
            this.SuppInvTextBox.Text = _finSuppInvHd.SuppInvoice;
            this.PPNPercentTextBox.Text = (_finSuppInvHd.PPN == 0) ? "0" : _finSuppInvHd.PPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            if (_finSuppInvHd.PPN == 0)
            {
                this.PPNDateLiteral.Text = "<input id='button2' Style = 'visibility:hidden' type='button' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                //this.ppn_date_start.Attributes.Add("Style", "visibility:hidden");
                this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNNoTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                this.PPNDateTextBox.Text = "";
                this.PPNNoTextBox.Text = "";
                this.PPNRateTextBox.Text = "";
            }
            else
            {
                this.PPNDateLiteral.Text = "<input id='button2' Style = 'visibility:visible' type='button' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                //this.ppn_date_start.Attributes.Add("Style", "visibility:visible");
                this.PPNNoTextBox.Attributes.Remove("ReadOnly");
                this.PPNNoTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                this.PPNDateTextBox.Text = (_finSuppInvHd.PPNDate == null) ? "" : DateFormMapper.GetValue(_finSuppInvHd.PPNDate);
                this.PPNNoTextBox.Text = _finSuppInvHd.PPNNo;
                this.PPNRateTextBox.Text = (_finSuppInvHd.PPNRate == null || _finSuppInvHd.PPNRate == 0) ? "0" : Convert.ToDecimal(_finSuppInvHd.PPNRate).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.PPhPercentTextBox.Text = (_finSuppInvHd.PPh == 0) ? "0" : _finSuppInvHd.PPh.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPhForexTextBox.Text = (_finSuppInvHd.PPhForex == 0) ? "0" : _finSuppInvHd.PPhForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountBaseTextBox.Text = (_finSuppInvHd.BaseForex == 0) ? "0" : _finSuppInvHd.BaseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DiscForexTextBox.Text = (_finSuppInvHd.DiscForex == 0) ? "0" : _finSuppInvHd.DiscForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNForexTextBox.Text = (_finSuppInvHd.PPNForex == 0) ? "0" : _finSuppInvHd.PPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.OtherForexTextBox.Text = (_finSuppInvHd.OtherForex == 0) ? "0" : _finSuppInvHd.OtherForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TotalForexTextBox.Text = (_finSuppInvHd.TotalForex == 0) ? "0" : _finSuppInvHd.TotalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            this.SetAttributeRate();
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

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINSuppInvHd _finSuppInvHd = this._supplierNoteBL.GetSingleFINSuppInvHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finSuppInvHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finSuppInvHd.ForexRate = (this.CurrRateTextBox.Text == "") ? 0 : Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finSuppInvHd.PPN = (this.PPNPercentTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNPercentTextBox.Text);
            if (this.PPNDateTextBox.Text == "")
            {
                _finSuppInvHd.PPNDate = null;
            }
            else
            {
                _finSuppInvHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            _finSuppInvHd.PPNForex = (this.PPNForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNForexTextBox.Text);
            _finSuppInvHd.PPNNo = this.PPNNoTextBox.Text;
            _finSuppInvHd.PPNRate = (this.PPNRateTextBox.Text == "") ? 1 : Convert.ToDecimal(this.PPNRateTextBox.Text);
            _finSuppInvHd.PPh = (this.PPhPercentTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPhPercentTextBox.Text);
            _finSuppInvHd.PPhForex = (this.PPhForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPhForexTextBox.Text);
            _finSuppInvHd.SuppInvoice = this.SuppInvTextBox.Text;
            _finSuppInvHd.Remark = this.RemarkTextBox.Text;
            _finSuppInvHd.BaseForex = (this.AmountBaseTextBox.Text == "") ? 0 : Convert.ToDecimal(this.AmountBaseTextBox.Text);
            _finSuppInvHd.DiscForex = (this.DiscForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.DiscForexTextBox.Text);
            _finSuppInvHd.OtherForex = (this.OtherForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.OtherForexTextBox.Text);
            _finSuppInvHd.TotalForex = (this.TotalForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text);
            _finSuppInvHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finSuppInvHd.DatePrep = DateTime.Now;

            bool _result = this._supplierNoteBL.EditFINSuppInvHd(_finSuppInvHd);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBox.Text, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
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
            FINSuppInvHd _finSuppInvHd = this._supplierNoteBL.GetSingleFINSuppInvHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finSuppInvHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finSuppInvHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finSuppInvHd.PPN = Convert.ToDecimal(this.PPNPercentTextBox.Text);
            if (this.PPNDateTextBox.Text == "")
            {
                _finSuppInvHd.PPNDate = null;
            }
            else
            {
                _finSuppInvHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            _finSuppInvHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _finSuppInvHd.PPNNo = this.PPNNoTextBox.Text;
            _finSuppInvHd.PPNRate = (this.PPNRateTextBox.Text == "") ? 1 : Convert.ToDecimal(this.PPNRateTextBox.Text);
            _finSuppInvHd.PPh = (this.PPhPercentTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPhPercentTextBox.Text);
            _finSuppInvHd.PPhForex = (this.PPhForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPhForexTextBox.Text);
            _finSuppInvHd.SuppInvoice = this.SuppInvTextBox.Text;
            _finSuppInvHd.Remark = this.RemarkTextBox.Text;
            _finSuppInvHd.BaseForex = (this.AmountBaseTextBox.Text == "") ? 0 : Convert.ToDecimal(this.AmountBaseTextBox.Text);
            _finSuppInvHd.DiscForex = (this.DiscForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.DiscForexTextBox.Text);
            _finSuppInvHd.OtherForex = (this.OtherForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.OtherForexTextBox.Text);
            _finSuppInvHd.TotalForex = (this.TotalForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text);
            _finSuppInvHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finSuppInvHd.DatePrep = DateTime.Now;

            bool _result = this._supplierNoteBL.EditFINSuppInvHd(_finSuppInvHd);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }
    }
}