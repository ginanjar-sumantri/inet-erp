using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.DPSupplierPayment
{
    public partial class DPSuppPaymentEdit : DPSupplierPaymentBase
    {
        private SupplierBL _suppBL = new SupplierBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private FINDPSuppPayBL _finDPSuppBL = new FINDPSuppPayBL();
        private PurchaseOrderBL _poBL = new PurchaseOrderBL();
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
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.PPNDateLiteral.Text = "<input id='button2' type='button' Style='visibility:visible' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.ShowCurrency();

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2"/*this.ppn_date_start.ClientID*/ + "," + this.BaseForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.BaseForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
            this.SetAttributeRate();
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
            FINDPSuppHd _finDPSuppHd = this._finDPSuppBL.GetSingleFINDPSuppHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            string _currCodeHome = _currencyBL.GetCurrDefault();
            string _currCode = _finDPSuppHd.CurrCode;
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);

            this.TransNoTextBox.Text = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            this.FileNmbrTextBox.Text = _finDPSuppHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_finDPSuppHd.TransDate);
            //this.StatusLabel.Text = DPCustomerDataMapper.GetStatusText(_finDPSuppHd.Status);
            this.SuppTextBox.Text = _suppBL.GetSuppNameByCode(_finDPSuppHd.SuppCode);
            this.AttnTextBox.Text = _finDPSuppHd.Attn;
            this.SuppInvoiceTextBox.Text = _finDPSuppHd.SuppInvoice;
            this.PONoTextBox.Text = _poBL.GetFileNmbrPRCPOHd(_finDPSuppHd.PONo, 0);
            this.CurrCodeDropDownList.SelectedValue = _finDPSuppHd.CurrCode;
            this.CurrTextBox.Text = _finDPSuppHd.CurrCode;

            this.SetCurrRate();

            this.CurrRateTextBox.Text = (_finDPSuppHd.ForexRate == 0) ? "0" : _finDPSuppHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _finDPSuppHd.Remark;
            this.PPNNoTextBox.Text = _finDPSuppHd.PPNNo;
            this.PPNDateTextBox.Text = (_finDPSuppHd.PPNDate == null) ? "" : DateFormMapper.GetValue(_finDPSuppHd.PPNDate);
            this.PPNRateTextBox.Text = (_finDPSuppHd.PPNRate == null) ? "0" : Convert.ToDecimal(_finDPSuppHd.PPNRate).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.BaseForexTextBox.Text = (_finDPSuppHd.BaseForex == 0) ? "0" : _finDPSuppHd.BaseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNTextBox.Text = (_finDPSuppHd.PPN == 0) ? "0" : _finDPSuppHd.PPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNForexTextBox.Text = (_finDPSuppHd.PPNForex == 0) ? "0" : _finDPSuppHd.PPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TotalForexTextBox.Text = (_finDPSuppHd.TotalForex == 0) ? "0" : _finDPSuppHd.TotalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            if (_finDPSuppHd.PPN != 0)
            {
                this.PPNNoTextBox.Attributes.Remove("ReadOnly");
                this.PPNNoTextBox.Attributes.Add("Style", "background-color: #FFFFFF");
                //this.ppn_date_start.Attributes.Add("Style", "visibility: visible");
                this.PPNDateLiteral.Text = "<input id='button2' type='button' Style='visibility:visible' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINDPSuppHd _finDPSuppHd = this._finDPSuppBL.GetSingleFINDPSuppHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finDPSuppHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finDPSuppHd.Attn = this.AttnTextBox.Text;
            _finDPSuppHd.SuppInvoice = this.SuppInvoiceTextBox.Text;
            _finDPSuppHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _finDPSuppHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finDPSuppHd.Remark = this.RemarkTextBox.Text;
            _finDPSuppHd.PPNNo = this.PPNNoTextBox.Text;
            if (this.PPNDateTextBox.Text != "")
            {
                _finDPSuppHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            else
            {
                _finDPSuppHd.PPNDate = null;
            }
            _finDPSuppHd.PPNRate = Convert.ToDecimal(this.PPNRateTextBox.Text);
            _finDPSuppHd.BaseForex = Convert.ToDecimal(this.BaseForexTextBox.Text);
            _finDPSuppHd.PPN = Convert.ToDecimal(this.PPNTextBox.Text);
            _finDPSuppHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _finDPSuppHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            _finDPSuppHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finDPSuppHd.DatePrep = DateTime.Now;

            bool _result = this._finDPSuppBL.EditFINDPSuppHd(_finDPSuppHd);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
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
            this.ShowData();
        }

        protected void CurrCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearDataNumeric();

            if (CurrCodeDropDownList.SelectedValue != "null")
            {
                this.SetCurrRate();
            }
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            FINDPSuppHd _finDPSuppHd = this._finDPSuppBL.GetSingleFINDPSuppHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finDPSuppHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finDPSuppHd.Attn = this.AttnTextBox.Text;
            _finDPSuppHd.SuppInvoice = this.SuppInvoiceTextBox.Text;
            _finDPSuppHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _finDPSuppHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finDPSuppHd.Remark = this.RemarkTextBox.Text;
            _finDPSuppHd.PPNNo = this.PPNNoTextBox.Text;
            if (this.PPNDateTextBox.Text != "")
            {
                _finDPSuppHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            else
            {
                _finDPSuppHd.PPNDate = null;
            }
            _finDPSuppHd.PPNRate = Convert.ToDecimal(this.PPNRateTextBox.Text);
            _finDPSuppHd.BaseForex = Convert.ToDecimal(this.BaseForexTextBox.Text);
            _finDPSuppHd.PPN = Convert.ToDecimal(this.PPNTextBox.Text);
            _finDPSuppHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _finDPSuppHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            _finDPSuppHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finDPSuppHd.DatePrep = DateTime.Now;

            bool _result = this._finDPSuppBL.EditFINDPSuppHd(_finDPSuppHd);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        private void ClearDataNumeric()
        {
            this.CurrRateTextBox.Text = "0";
            this.DecimalPlaceHiddenField.Value = "";
            this.PPNTextBox.Text = "0";
            this.PPNRateTextBox.Text = "0";
            this.CurrTextBox.Text = "";
            this.BaseForexTextBox.Text = "0";
            this.PPNForexTextBox.Text = "0";
            this.TotalForexTextBox.Text = "0";
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

        private void SetCurrRate()
        {
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrCodeDropDownList.SelectedValue);
            this.CurrRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNRateTextBox.Text = this.CurrRateTextBox.Text;
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
    }
}