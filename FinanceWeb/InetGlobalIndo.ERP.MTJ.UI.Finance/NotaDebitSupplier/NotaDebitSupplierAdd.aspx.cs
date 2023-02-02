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
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.NotaDebitSupplier
{
    public partial class NotaDebitSupplierAdd : NotaDebitSupplierBase
    {
        private SupplierBL _suppBL = new SupplierBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private TermBL _termBL = new TermBL();
        private NotaDebitSupplierBL _notaDebitSupplierBL = new NotaDebitSupplierBL();
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

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowSupp();
                this.ShowTerm();
                this.ShowCurrency();
                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");

            this.CurrRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowSupp()
        {
            this.SuppDropDownList.Items.Clear();
            this.SuppDropDownList.DataTextField = "SuppName";
            this.SuppDropDownList.DataValueField = "SuppCode";
            this.SuppDropDownList.DataSource = this._suppBL.GetListDDLSupp();
            this.SuppDropDownList.DataBind();
            this.SuppDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

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

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ClearLabel();
            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            this.SuppDropDownList.SelectedValue = "null";
            this.CurrCodeDropDownList.SelectedValue = "null";
            this.CurrRateTextBox.Text = "0";
            this.CurrRateTextBox.Attributes.Remove("ReadOnly");
            this.TotalForexTextBox.Text = "0";
            this.TermDropDownList.SelectedValue = "null";
            this.CurrTextBox.Text = "";
            this.AttnTextBox.Text = "";
            this.RemarkTextBox.Text = "";
            this.DecimalPlaceHiddenField.Value = "";
        }

        protected void SuppDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SuppDropDownList.SelectedValue != "null")
            {
                this.AttnTextBox.Text = _suppBL.GetSuppContact(this.SuppDropDownList.SelectedValue);
                string _currCode = this._suppBL.GetCurr(SuppDropDownList.SelectedValue);
                string _termCode = this._suppBL.GetTerm(SuppDropDownList.SelectedValue);
                byte _decimalPlace = _currencyBL.GetDecimalPlace(_currCode);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                if (_currCode != "")
                {
                    this.CurrCodeDropDownList.SelectedValue = _currCode;
                }
                this.CurrRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                if (_termCode != "")
                {
                    this.TermDropDownList.SelectedValue = _termCode;
                }
                if (this.CurrCodeDropDownList.SelectedValue == _currencyBL.GetCurrDefault())
                {
                    this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.CurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.CurrRateTextBox.Text = "1";
                }
                else
                {
                    this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                    this.CurrRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                }
                this.CurrTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrTextBox.Attributes.Add("style", "background-color:#cccccc");
                this.CurrTextBox.Text = CurrCodeDropDownList.SelectedValue;
                this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
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
        }

        protected void CurrCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrCodeDropDownList.SelectedValue != "null")
            {
                string _currCode = this._suppBL.GetCurr(SuppDropDownList.SelectedValue);
                string _currCodeHome = _currencyBL.GetCurrDefault();
                byte _decimalPlace = _currencyBL.GetDecimalPlace(this.CurrCodeDropDownList.SelectedValue);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                this.CurrRateTextBox.Text = _currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                if (this.CurrCodeDropDownList.SelectedValue == _currCodeHome)
                {
                    this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.CurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.CurrRateTextBox.Text = "1";
                }
                else
                {
                    this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                    this.CurrRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                }
                this.CurrTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrTextBox.Attributes.Add("style", "background-color:#cccccc");
                this.CurrTextBox.Text = CurrCodeDropDownList.SelectedValue;
                this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            }
            else
            {
                this.CurrRateTextBox.Text = "0";
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#ffffff");
            }
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            FINDNSuppHd _finDNSuppHd = new FINDNSuppHd();

            _finDNSuppHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finDNSuppHd.Status = NotaDebitSuppDataMapper.GetStatus(TransStatus.OnHold);
            _finDNSuppHd.SuppCode = this.SuppDropDownList.SelectedValue;
            _finDNSuppHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _finDNSuppHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finDNSuppHd.Remark = this.RemarkTextBox.Text;
            _finDNSuppHd.Attn = this.AttnTextBox.Text;
            _finDNSuppHd.BaseForex = 0;
            _finDNSuppHd.DiscForex = 0;
            _finDNSuppHd.TotalForex = 0;
            _finDNSuppHd.Term = this.TermDropDownList.SelectedValue;
            _finDNSuppHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finDNSuppHd.DatePrep = DateTime.Now;

            string _result = this._notaDebitSupplierBL.AddFINDNSuppHd(_finDNSuppHd);

            if (_result != "")
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
            }
            else
            {
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
    }
}