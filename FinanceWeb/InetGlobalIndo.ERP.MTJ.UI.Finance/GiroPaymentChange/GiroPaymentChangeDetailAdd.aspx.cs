using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.GiroPaymentChange
{
    public partial class GiroPaymentChangeDetailAdd : GiroPaymentChangeBase
    {
        private FINGiroOutBL _finGiroOutBL = new FINGiroOutBL();
        private FINChangeGiroOutBL _finChangeGiroOutBL = new FINChangeGiroOutBL();
        private BankBL _bankBL = new BankBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();

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

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowOldGiro();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void OldGiroDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            FINGiroOut _finGiroOut = this._finGiroOutBL.GetSingleFINGiroOut(this.OldGiroDropDownList.SelectedValue);

            if (_finGiroOut != null)
            {
                string _currCode = _finGiroOut.CurrCode;
                byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);

                this.DateTextBox.Text = DateFormMapper.GetValue(_finGiroOut.PaymentDate);
                this.DueDateTextBox.Text = DateFormMapper.GetValue(_finGiroOut.DueDate);
                this.BankPaymentTextBox.Text = _bankBL.GetBankPaymentNameByCode(_finGiroOut.BankPayment);
                this.CurrTextBox.Text = _finGiroOut.CurrCode;
                this.RateTextBox.Text = (_finGiroOut.ForexRate == 0) ? "0" : _finGiroOut.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.AmountForexTextBox.Text = (_finGiroOut.AmountForex == 0) ? "0" : _finGiroOut.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            else
            {
                this.ClearData();
            }
        }

        public void ClearData()
        {
            this.ClearLabel();
            //this.TransNoTextBox.Text = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            this.DateTextBox.Text = "";
            this.DueDateTextBox.Text = "";
            this.OldGiroDropDownList.SelectedValue = "null";
            this.BankPaymentTextBox.Text = "";
            this.CurrTextBox.Text = "";
            this.RateTextBox.Text = "";
            this.AmountForexTextBox.Text = "";
        }

        public void ShowOldGiro()
        {
            FINChangeGiroOutHd _finChangeGiroOutHd = this._finChangeGiroOutBL.GetSingleFINChangeGiroOutHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            string _supp = _finChangeGiroOutHd.SuppCode;
            string _cust = _finChangeGiroOutHd.CustCode;

            this.OldGiroDropDownList.Items.Clear();
            this.OldGiroDropDownList.DataTextField = "GiroNo";
            this.OldGiroDropDownList.DataValueField = "GiroNo";
            if (_supp != null && _supp.Trim() != "")
            {
                this.OldGiroDropDownList.DataSource = this._finGiroOutBL.GetListForDDL(_supp);
            }
            else if (_cust != null && _cust.Trim() != "")
            {
                this.OldGiroDropDownList.DataSource = this._finGiroOutBL.GetListForDDL(_cust);
            }
            this.OldGiroDropDownList.DataBind();
            this.OldGiroDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINChangeGiroOutDt _finChangeGiroOutDt = new FINChangeGiroOutDt();
            FINGiroOut _finGiroOut = this._finGiroOutBL.GetSingleFINGiroOut(this.OldGiroDropDownList.SelectedValue);

            _finChangeGiroOutDt.PaymentDate = DateFormMapper.GetValue(this.DateTextBox.Text);
            _finChangeGiroOutDt.DueDate = DateFormMapper.GetValue(this.DueDateTextBox.Text);
            _finChangeGiroOutDt.OldGiro = this.OldGiroDropDownList.SelectedValue;
            _finChangeGiroOutDt.TransNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            _finChangeGiroOutDt.BankPayment = _finGiroOut.BankPayment;
            _finChangeGiroOutDt.CurrCode = this.CurrTextBox.Text;
            _finChangeGiroOutDt.ForexRate = Convert.ToDecimal(this.RateTextBox.Text);
            _finChangeGiroOutDt.AmountForex = Convert.ToDecimal(this.AmountForexTextBox.Text);

            bool _result = this._finChangeGiroOutBL.AddFINChangeGiroOutDt(_finChangeGiroOutDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }
        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}