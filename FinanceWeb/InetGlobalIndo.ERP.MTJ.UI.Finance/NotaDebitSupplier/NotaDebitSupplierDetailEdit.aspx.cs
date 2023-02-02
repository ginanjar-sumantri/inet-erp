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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.NotaDebitSupplier
{
    public partial class NotaDebitSupplierDetailEdit : NotaDebitSupplierBase
    {
        private NotaDebitSupplierBL _notaDebitSupplierBL = new NotaDebitSupplierBL();
        private AccountBL _accountBL = new AccountBL();
        private UnitBL _unitBL = new UnitBL();
        private SubledBL _subledBL = new SubledBL();
        private EmployeeBL _empBL = new EmployeeBL();
        private User_EmployeeBL _userEmpBL = new User_EmployeeBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();

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

                this.ShowAccount();
                this.ShowUnit();

                this.ShowData();
                this.SetAttribute();
                this.ClearLabel();
            }
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribute()
        {
            this.AmountTextBox.Attributes.Add("ReadOnly", "True");
            this.QtyTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.PriceTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.PriceTextBox.Attributes.Add("OnBlur", "Count(" + this.QtyTextBox.ClientID + "," + this.PriceTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.QtyTextBox.Attributes.Add("OnBlur", "Count(" + this.QtyTextBox.ClientID + "," + this.PriceTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowData()
        {
            FINDNSuppDt _finDNSuppDt = this._notaDebitSupplierBL.GetSingleFINDNSuppDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey)));
            FINDNSuppHd _finDNSuppHd = _notaDebitSupplierBL.GetSingleFINDNSuppHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finDNSuppHd.CurrCode);

            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.AccountTextBox.Text = _finDNSuppDt.Account;
            this.AccountDropDownList.SelectedValue = _finDNSuppDt.Account;
            this.FgSubledHiddenField.Value = _finDNSuppDt.FgSubLed.ToString();
            this.SubledDropDownList.SelectedValue = _finDNSuppDt.Subled;
            this.RemarkTextBox.Text = _finDNSuppDt.Remark;
            this.PriceTextBox.Text = _finDNSuppDt.PriceForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountTextBox.Text = _finDNSuppDt.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.QtyTextBox.Text = _finDNSuppDt.Qty.ToString("#,###.##");
            this.UnitDropDownList.SelectedValue = _finDNSuppDt.Unit;

            this.GetSubled();
        }

        public void ShowAccount()
        {
            string _currHeader = this._notaDebitSupplierBL.GetSingleFINDNSuppHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).CurrCode;

            this.AccountDropDownList.Items.Clear();
            this.AccountDropDownList.DataTextField = "AccountName";
            this.AccountDropDownList.DataValueField = "Account";
            this.AccountDropDownList.DataSource = this._accountBL.GetListAccountByTransType(_currHeader, _userEmpBL.GetEmployeeIDByUserName(HttpContext.Current.User.Identity.Name), AppModule.GetValue(TransactionType.NotaDebitSupplier));
            this.AccountDropDownList.DataBind();
            this.AccountDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowUnit()
        {
            this.UnitDropDownList.Items.Clear();
            this.UnitDropDownList.DataTextField = "UnitName";
            this.UnitDropDownList.DataValueField = "UnitCode";
            this.UnitDropDownList.DataSource = this._unitBL.GetListForDDL();
            this.UnitDropDownList.DataBind();
            this.UnitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void GetSubled()
        {
            char _tempSubled = this._accountBL.GetAccountSubLed(this.AccountDropDownList.SelectedValue);
            this.FgSubledHiddenField.Value = _tempSubled.ToString();

            this.SubledDropDownList.Items.Clear();
            this.SubledDropDownList.DataTextField = "SubLed_Name";
            this.SubledDropDownList.DataValueField = "SubLed_No";
            this.SubledDropDownList.DataSource = this._subledBL.GetListSubled(Convert.ToChar(this.FgSubledHiddenField.Value));
            this.SubledDropDownList.DataBind();
            this.SubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void AccountDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.AccountDropDownList.SelectedValue != "null")
            {
                this.AccountTextBox.Text = this.AccountDropDownList.SelectedValue;
            }
            else
            {
                this.AccountTextBox.Text = "";
            }

            this.GetSubled();
        }

        protected void AccountTextBox_TextChanged(object sender, EventArgs e)
        {
            bool _exist = this._accountBL.IsExistAccountByTransType(AppModule.GetValue(TransactionType.NotaDebitSupplier), this.AccountTextBox.Text);
            if (_exist == true)
            {
                this.AccountDropDownList.SelectedValue = this.AccountTextBox.Text;
            }
            else
            {
                this.AccountTextBox.Text = "";
                this.AccountDropDownList.SelectedValue = "null";
            }

            this.GetSubled();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINDNSuppDt _finDNSuppDt = this._notaDebitSupplierBL.GetSingleFINDNSuppDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey)));

            if (this.SubledDropDownList.SelectedValue != "null")
            {
                _finDNSuppDt.FgSubLed = Convert.ToChar(this.FgSubledHiddenField.Value);
                _finDNSuppDt.Subled = this.SubledDropDownList.SelectedValue;
            }
            else
            {
                _finDNSuppDt.FgSubLed = 'N';
                _finDNSuppDt.Subled = null;
            }
            _finDNSuppDt.Account = this.AccountTextBox.Text;
            _finDNSuppDt.Remark = this.RemarkTextBox.Text;
            _finDNSuppDt.PriceForex = Convert.ToDecimal(this.PriceTextBox.Text);
            _finDNSuppDt.AmountForex = Convert.ToDecimal(this.AmountTextBox.Text);
            _finDNSuppDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _finDNSuppDt.Unit = this.UnitDropDownList.SelectedValue;

            bool _result = this._notaDebitSupplierBL.EditFINDNSuppDt(_finDNSuppDt);

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
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }
    }
}