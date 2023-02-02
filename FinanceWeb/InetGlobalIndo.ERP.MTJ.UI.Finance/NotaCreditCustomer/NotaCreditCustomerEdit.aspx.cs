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
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.NotaCreditCustomer
{
    public partial class NotaCreditCustomerEdit : NotaCreditCustomerBase
    {
        private CustomerBL _custBL = new CustomerBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private TermBL _termBL = new TermBL();
        private NotaCreditCustomerBL _notaCreditCustomerBL = new NotaCreditCustomerBL();
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
                
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.ShowCust();
                this.ShowTerm();
                this.ShowCurrency();
                //this.ShowInvoiceNo();

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
        }

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
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

        //public void ShowInvoiceNo()
        //{
        //    this.InvoiceDDL.Items.Clear();
        //    this.InvoiceDDL.DataTextField = "FileNmbr";
        //    this.InvoiceDDL.DataValueField = "InvoiceNo";
        //    this.InvoiceDDL.DataSource = this._notaCreditCustomerBL.GetListInvoiceNoFINARPostingForDDL(this.CustDropDownList.SelectedValue);
        //    this.InvoiceDDL.DataBind();
        //    this.InvoiceDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
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
            FINCNCustHd _finCNCustHd = this._notaCreditCustomerBL.GetSingleFINCNCustHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finCNCustHd.CurrCode);

            this.TransNoTextBox.Text = _finCNCustHd.TransNmbr;
            this.FileNmbrTextBox.Text = _finCNCustHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_finCNCustHd.TransDate);
            //this.StatusLabel.Text = NotaDebitCustDataMapper.GetStatusText(_finCNCustHd.Status);
            this.CustDropDownList.SelectedValue = _finCNCustHd.CustCode;
            this.CNCustNoTextBox.Text = _finCNCustHd.CNCustNo;
            this.CurrCodeDropDownList.SelectedValue = _finCNCustHd.CurrCode;
            if (this.CurrCodeDropDownList.SelectedValue == _currencyBL.GetCurrDefault())
            {
                this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            }

            this.CurrRateTextBox.Text = _finCNCustHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            decimal _totalForexValue = Convert.ToDecimal((_finCNCustHd.TotalForex == null) ? 0 : _finCNCustHd.TotalForex);
            this.TotalForexTextBox.Text = (_totalForexValue == 0) ? "0" : _totalForexValue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TermDropDownList.SelectedValue = _finCNCustHd.Term;
            this.CurrTextBox.Text = _finCNCustHd.CurrCode;
            //this.StatusLabel.Text = NotaDebitCustDataMapper.GetStatusText(_finCNCustHd.Status);
            this.AttnTextBox.Text = _finCNCustHd.Attn;
            //if (this.CustDropDownList.SelectedValue != "null")
            //{
            //    this.ShowInvoiceNo();
            //    this.InvoiceDDL.SelectedValue = _finCNCustHd.InvoiceNo;
            //}
            this.RemarkTextBox.Text = _finCNCustHd.Remark;
        }

        protected void CustDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CustDropDownList.SelectedValue != "null")
            {
                this.AttnTextBox.Text = _custBL.GetCustContact(this.CustDropDownList.SelectedValue);
                string _currCode = this._custBL.GetCurr(CustDropDownList.SelectedValue);
                string _termCode = this._custBL.GetTerm(CustDropDownList.SelectedValue);

                if (_currCode != "")
                {
                    this.CurrCodeDropDownList.SelectedValue = _currCode;
                }
                if (_termCode != "")
                {
                    this.TermDropDownList.SelectedValue = _termCode;
                }

                this.SetCurrRate();

                this.CurrTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrTextBox.Attributes.Add("style", "background-color:#cccccc");
                this.CurrTextBox.Text = CurrCodeDropDownList.SelectedValue;
            }
            else
            {
                this.AttnTextBox.Text = "";
                this.CurrTextBox.Text = "";
                this.CurrCodeDropDownList.SelectedValue = "null";
                this.TermDropDownList.SelectedValue = "null";
                this.CurrRateTextBox.Text = "0";
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#ffffff");
            }

            //this.ShowInvoiceNo();
        }

        protected void CurrCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrCodeDropDownList.SelectedValue != "null")
            {
                this.SetCurrRate();

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
            FINCNCustHd _finCNCustHd = this._notaCreditCustomerBL.GetSingleFINCNCustHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finCNCustHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finCNCustHd.Status = _finCNCustHd.Status;
            _finCNCustHd.CustCode = this.CustDropDownList.SelectedValue;
            _finCNCustHd.CNCustNo = this.CNCustNoTextBox.Text;
            _finCNCustHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _finCNCustHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finCNCustHd.Remark = this.RemarkTextBox.Text;
            _finCNCustHd.Attn = this.AttnTextBox.Text;
            //_finCNCustHd.InvoiceNo = this.InvoiceDDL.SelectedValue;
            _finCNCustHd.TotalForex = (this.TotalForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text);
            _finCNCustHd.Term = this.TermDropDownList.SelectedValue;
            _finCNCustHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finCNCustHd.DatePrep = DateTime.Now;

            bool _result = this._notaCreditCustomerBL.EditFINCNCustHd(_finCNCustHd);

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
            FINCNCustHd _finCNCustHd = this._notaCreditCustomerBL.GetSingleFINCNCustHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finCNCustHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finCNCustHd.Status = _finCNCustHd.Status;
            _finCNCustHd.CustCode = this.CustDropDownList.SelectedValue;
            _finCNCustHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _finCNCustHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finCNCustHd.CNCustNo = this.CNCustNoTextBox.Text;
            _finCNCustHd.Remark = this.RemarkTextBox.Text;
            _finCNCustHd.Attn = this.AttnTextBox.Text;
            //_finCNCustHd.InvoiceNo = this.InvoiceDDL.SelectedValue;
            _finCNCustHd.TotalForex = (this.TotalForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text);
            _finCNCustHd.Term = this.TermDropDownList.SelectedValue;
            _finCNCustHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finCNCustHd.DatePrep = DateTime.Now;

            bool _result = this._notaCreditCustomerBL.EditFINCNCustHd(_finCNCustHd);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        private void DisableRate()
        {

            this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.CurrRateTextBox.Text = "1";
        }

        private void EnableRate()
        {
            this.CurrRateTextBox.Attributes.Remove("ReadOnly");
            this.CurrRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
        }

        private void SetCurrRate()
        {
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrCodeDropDownList.SelectedValue);
            this.CurrRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
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