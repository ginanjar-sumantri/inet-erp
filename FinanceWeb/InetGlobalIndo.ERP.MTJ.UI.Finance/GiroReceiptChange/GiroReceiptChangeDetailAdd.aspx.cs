using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.GiroReceiptChange
{
    public partial class GiroReceiptChangeDetailAdd : GiroReceiptChangeBase
    {
        private FINChangeGiroInBL _finChangeGiroInBL = new FINChangeGiroInBL();
        private FINGiroInBL _finGiroInBL = new FINGiroInBL();
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

            _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowOldGiro();

                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.DueDateTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrTextBox.Attributes.Add("ReadOnly", "True");
            this.RateTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountForexTextBox.Attributes.Add("ReadOnly", "True");
            this.BankGiroTextBox.Attributes.Add("ReadOnly", "True");
        }

        private void ClearData()
        {
            this.ClearLabel();

            this.DateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.DueDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.OldGiroDropDownList.SelectedValue = "null";
            this.BankGiroTextBox.Text = "";
            this.CurrTextBox.Text = "";
            this.RateTextBox.Text = "";
            this.AmountForexTextBox.Text = "";
        }

        private void ShowOldGiro()
        {
            FINChangeGiroInHd _finChangeGiroInHd = this._finChangeGiroInBL.GetSingleFINChangeGiroInHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            string supp = _finChangeGiroInHd.SuppCode;
            string cust = _finChangeGiroInHd.CustCode;

            this.OldGiroDropDownList.Items.Clear();
            this.OldGiroDropDownList.DataTextField = "GiroNo";
            this.OldGiroDropDownList.DataValueField = "GiroNo";
            if (supp != null)
            {
                this.OldGiroDropDownList.DataSource = this._finGiroInBL.GetListForDDL(supp);
            }
            else if (cust != null)
            {
                this.OldGiroDropDownList.DataSource = this._finGiroInBL.GetListForDDL(cust);
            }
            this.OldGiroDropDownList.DataBind();
            this.OldGiroDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINChangeGiroInDt _finChangeGiroInDt = new FINChangeGiroInDt();
            FINGiroIn _finGiroIn = this._finGiroInBL.GetSingleFINGiroIn(this.OldGiroDropDownList.SelectedValue);

            _finChangeGiroInDt.ReceiptDate = DateFormMapper.GetValue(this.DateTextBox.Text);
            _finChangeGiroInDt.DueDate = DateFormMapper.GetValue(this.DueDateTextBox.Text);
            _finChangeGiroInDt.OldGiro = this.OldGiroDropDownList.SelectedValue;
            _finChangeGiroInDt.TransNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            _finChangeGiroInDt.BankGiro = _finGiroIn.BankGiro;
            _finChangeGiroInDt.CurrCode = this.CurrTextBox.Text;
            _finChangeGiroInDt.ForexRate = Convert.ToDecimal(this.RateTextBox.Text);
            _finChangeGiroInDt.AmountForex = Convert.ToDecimal(this.AmountForexTextBox.Text);

            bool _result = this._finChangeGiroInBL.AddFINChangeGiroInDt(_finChangeGiroInDt);

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

        protected void OldGiroDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.OldGiroDropDownList.SelectedValue != "null")
            {
                FINGiroIn _finGiroIn = this._finGiroInBL.GetSingleFINGiroIn(this.OldGiroDropDownList.SelectedValue);

                string _currCode = _finGiroIn.CurrCode;
                byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);

                this.DateTextBox.Text = DateFormMapper.GetValue(_finGiroIn.ReceiptDate);
                this.DueDateTextBox.Text = DateFormMapper.GetValue(_finGiroIn.DueDate);
                this.BankGiroTextBox.Text = _bankBL.GetBankNameByCode(_finGiroIn.BankGiro);
                this.CurrTextBox.Text = _finGiroIn.CurrCode;
                this.RateTextBox.Text = _finGiroIn.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.AmountForexTextBox.Text = _finGiroIn.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            else
            {
                this.DateTextBox.Text = "";
                this.DueDateTextBox.Text = "";
                this.BankGiroTextBox.Text = "";
                this.CurrTextBox.Text = "";
                this.RateTextBox.Text = "";
                this.AmountForexTextBox.Text = "0";
            }
        }
    }
}