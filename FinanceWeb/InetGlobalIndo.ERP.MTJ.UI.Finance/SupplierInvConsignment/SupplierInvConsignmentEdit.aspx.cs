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
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.UI.Finance.NotaCreditCustomer;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.SupplierInvConsignment
{
    public partial class SupplierInvConsignmentEdit : SupplierInvConsignmentBase
    {
        private SupplierBL _suppBL = new SupplierBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private TermBL _termBL = new TermBL();
        //private NotaCreditSupplierBL _notaCreditSupplierBL = new NotaCreditSupplierBL();
        private SupplierInvConsignmentBL _suppInvConsBL = new SupplierInvConsignmentBL();
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
                this.PPNDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                //this.ShowSupp();
                this.ShowTerm();
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

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountBaseTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
            //this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            //this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");

            this.CurrRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.PPNRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");

            this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.DiscForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2"/*this.ppn_date_start.ClientID */+ "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2"/*this.ppn_date_start.ClientID */ + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        //public void ShowSupp()
        //{
        //    this.SuppDropDownList.Items.Clear();
        //    this.SuppDropDownList.DataTextField = "SuppName";
        //    this.SuppDropDownList.DataValueField = "SuppCode";
        //    this.SuppDropDownList.DataSource = this._suppBL.GetListDDLSupp();
        //    this.SuppDropDownList.DataBind();
        //    this.SuppDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

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

        public void ShowData()
        {
            FINSuppInvConsignmentHd _finSuppInvConsignHd = this._suppInvConsBL.GetSingleSuppInvConsignHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = _currencyBL.GetDecimalPlace(_finSuppInvConsignHd.CurrCode);

            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.TransNoTextBox.Text = _finSuppInvConsignHd.TransNmbr;
            this.FileNmbrTextBox.Text = _finSuppInvConsignHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_finSuppInvConsignHd.TransDate);
            //this.StatusLabel.Text = NotaDebitSuppDataMapper.GetStatusText(_finCNSuppHd.Status);
            //this.SuppDropDownList.SelectedValue = _finCNSuppHd.SuppCode;
            this.SupplierTextBox.Text = _finSuppInvConsignHd.SuppCode;
            this.CurrCodeDropDownList.SelectedValue = _finSuppInvConsignHd.CurrCode;
            string _currHome = _currencyBL.GetCurrDefault();
            if (_currHome == this.CurrCodeDropDownList.SelectedValue)
            {
                this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrRateTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
                this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNRateTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
            }
            else
            {
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("Style", "background-color:#FFFFFF");
                this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                this.PPNRateTextBox.Attributes.Add("Style", "background-color:#FFFFFF");
            }
            this.CurrRateTextBox.Text = (_finSuppInvConsignHd.ForexRate == 0) ? "0" : _finSuppInvConsignHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            if (_finSuppInvConsignHd.TotalForex != null)
            {
                decimal _totalForex = Convert.ToDecimal(_finSuppInvConsignHd.TotalForex);
                this.TotalForexTextBox.Text = (_totalForex == 0) ? "0" : _totalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            this.TermDropDownList.SelectedValue = _finSuppInvConsignHd.Term;
            this.CurrTextBox.Text = _finSuppInvConsignHd.CurrCode;
            //this.StatusLabel.Text = NotaDebitSuppDataMapper.GetStatusText(_finCNSuppHd.Status);
            //this.AttnTextBox.Text = _finCNSuppHd.Attn;
            this.RemarkTextBox.Text = _finSuppInvConsignHd.Remark;
            if (_finSuppInvConsignHd.PPN != null || _finSuppInvConsignHd.PPN != 0)
            {
                this.PPNPercentTextBox.Text = (_finSuppInvConsignHd.PPN == null || _finSuppInvConsignHd.PPN == 0) ? "0" : Convert.ToDecimal(_finSuppInvConsignHd.PPN).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                if (_finSuppInvConsignHd.PPNRate != null)
                {
                    decimal _ppnRate = Convert.ToDecimal(_finSuppInvConsignHd.PPNRate);
                    this.PPNRateTextBox.Text = (_ppnRate == 0) ? "0" : _ppnRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                }
            }
            else
            {
                this.PPNPercentTextBox.Text = "0";
                this.PPNRateTextBox.Text = "";
            }
            this.PPNNoTextBox.Text = _finSuppInvConsignHd.PPNNo;
            if (_finSuppInvConsignHd.PPNForex != null)
            {
                decimal _ppnForex = Convert.ToDecimal(_finSuppInvConsignHd.PPNForex);
                this.PPNForexTextBox.Text = (_ppnForex == 0) ? "0" : _ppnForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            if (_finSuppInvConsignHd.PPNDate != null)
            {
                this.PPNDateTextBox.Text = DateFormMapper.GetValue(_finSuppInvConsignHd.PPNDate);
            }
            if (_finSuppInvConsignHd.BaseForex != null)
            {
                decimal _amountBase = Convert.ToDecimal(_finSuppInvConsignHd.BaseForex);
                this.AmountBaseTextBox.Text = (_amountBase == 0) ? "0" : _amountBase.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            if (_finSuppInvConsignHd.DiscForex != null)
            {
                decimal _discForex = Convert.ToDecimal(_finSuppInvConsignHd.DiscForex);
                this.DiscForexTextBox.Text = (_discForex == 0) ? "0" : _discForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            this.SuppInvNoTextBox.Text = _finSuppInvConsignHd.SuppInvoice;
            //this.SuppPONoTextBox.Text = _finCNSuppHd.SuppPONo;
        }

        //protected void SuppDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.SuppDropDownList.SelectedValue != "null")
        //    {
        //        this.AttnTextBox.Text = _suppBL.GetSuppContact(this.SuppDropDownList.SelectedValue);
        //        string _currCode = this._suppBL.GetCurr(SuppDropDownList.SelectedValue);
        //        string _termCode = this._suppBL.GetTerm(SuppDropDownList.SelectedValue);
        //        byte _decimalPlace = _currencyBL.GetDecimalPlace(_currCode);

        //        this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
        //        this.CurrRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
        //        this.PPNRateTextBox.Text = this.CurrRateTextBox.Text;

        //        if (_currCode != "")
        //        {
        //            this.CurrCodeDropDownList.SelectedValue = _currCode;
        //        }
        //        if (_termCode != "")
        //        {
        //            this.TermDropDownList.SelectedValue = _termCode;
        //        }
        //        if (this.CurrCodeDropDownList.SelectedValue == _currencyBL.GetCurrDefault())
        //        {
        //            this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
        //            this.CurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
        //            this.CurrRateTextBox.Text = "1";
        //            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
        //            this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
        //            this.PPNRateTextBox.Text = "1";
        //        }
        //        else
        //        {
        //            this.CurrRateTextBox.Attributes.Remove("ReadOnly");
        //            this.CurrRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
        //            this.PPNRateTextBox.Attributes.Remove("ReadOnly");
        //            this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
        //        }
        //        this.CurrTextBox.Attributes.Add("ReadOnly", "True");
        //        this.CurrTextBox.Attributes.Add("style", "background-color:#cccccc");
        //        this.CurrTextBox.Text = CurrCodeDropDownList.SelectedValue;
        //    }
        //    else
        //    {
        //        this.AttnTextBox.Text = "";
        //        this.CurrTextBox.Text = "";
        //        this.CurrCodeDropDownList.SelectedValue = "null";
        //        this.TermDropDownList.SelectedValue = "null";
        //        this.CurrRateTextBox.Text = "0";
        //        this.CurrRateTextBox.Attributes.Remove("ReadOnly");
        //        this.CurrRateTextBox.Attributes.Add("style", "background-color:#ffffff");
        //    }
        //}

        protected void CurrCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CurrCodeDropDownList.SelectedValue != "null")
            {
                string _currCode = this._suppBL.GetCurr(this.SupplierTextBox.Text);
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
            }
            else
            {
                this.CurrRateTextBox.Text = "0";
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#ffffff");
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINSuppInvConsignmentHd _finSuppInvConsignHd = this._suppInvConsBL.GetSingleSuppInvConsignHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finSuppInvConsignHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finSuppInvConsignHd.Status = _finSuppInvConsignHd.Status;
            _finSuppInvConsignHd.SuppCode = this.SupplierTextBox.Text;
            _finSuppInvConsignHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _finSuppInvConsignHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finSuppInvConsignHd.PPN = Convert.ToDecimal(this.PPNPercentTextBox.Text);
            if (this.PPNDateTextBox.Text != "")
            {
                _finSuppInvConsignHd.PPNDate = Convert.ToDateTime(this.PPNDateTextBox.Text);
            }
            else
            {
                _finSuppInvConsignHd.PPNDate = null;
            }
            _finSuppInvConsignHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _finSuppInvConsignHd.PPNNo = this.PPNNoTextBox.Text;
            _finSuppInvConsignHd.PPNRate = Convert.ToDecimal(this.PPNRateTextBox.Text);
            _finSuppInvConsignHd.SuppInvoice = this.SuppInvNoTextBox.Text;
            //_finSuppInvConsignHd.SuppPONo = this.SuppPONoTextBox.Text;
            _finSuppInvConsignHd.Remark = this.RemarkTextBox.Text;
            //_finSuppInvConsignHd.Attn = this.AttnTextBox.Text;
            _finSuppInvConsignHd.BaseForex = Convert.ToDecimal(this.AmountBaseTextBox.Text);
            _finSuppInvConsignHd.DiscForex = (this.DiscForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.DiscForexTextBox.Text);
            _finSuppInvConsignHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            _finSuppInvConsignHd.Term = this.TermDropDownList.SelectedValue;
            _finSuppInvConsignHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finSuppInvConsignHd.DatePrep = DateTime.Now;

            bool _result = this._suppInvConsBL.EditSuppInvConsignHd(_finSuppInvConsignHd);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
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

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            FINSuppInvConsignmentHd _finSuppInvConsignHd = this._suppInvConsBL.GetSingleSuppInvConsignHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finSuppInvConsignHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finSuppInvConsignHd.Status = _finSuppInvConsignHd.Status;
            _finSuppInvConsignHd.SuppCode = this.SupplierTextBox.Text;
            _finSuppInvConsignHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _finSuppInvConsignHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finSuppInvConsignHd.PPN = Convert.ToDecimal(this.PPNPercentTextBox.Text);
            if (this.PPNDateTextBox.Text != "")
            {
                _finSuppInvConsignHd.PPNDate = Convert.ToDateTime(this.PPNDateTextBox.Text);
            }
            else
            {
                _finSuppInvConsignHd.PPNDate = null;
            }
            _finSuppInvConsignHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _finSuppInvConsignHd.PPNNo = this.PPNNoTextBox.Text;
            _finSuppInvConsignHd.PPNRate = Convert.ToDecimal(this.PPNRateTextBox.Text);
            _finSuppInvConsignHd.SuppInvoice = this.SuppInvNoTextBox.Text;
            //_finSuppInvConsignHd.SuppPONo = this.SuppPONoTextBox.Text;
            _finSuppInvConsignHd.Remark = this.RemarkTextBox.Text;
            //_finSuppInvConsignHd.Attn = this.AttnTextBox.Text;
            _finSuppInvConsignHd.BaseForex = Convert.ToDecimal(this.AmountBaseTextBox.Text);
            _finSuppInvConsignHd.DiscForex = (this.DiscForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.DiscForexTextBox.Text);
            _finSuppInvConsignHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            _finSuppInvConsignHd.Term = this.TermDropDownList.SelectedValue;
            _finSuppInvConsignHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finSuppInvConsignHd.DatePrep = DateTime.Now;

            bool _result = this._suppInvConsBL.EditSuppInvConsignHd(_finSuppInvConsignHd);

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