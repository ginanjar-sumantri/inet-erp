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

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.NotaCreditCustomer
{
    public partial class NotaCreditCustomerDetailAdd : NotaCreditCustomerBase
    {
        private NotaCreditCustomerBL _notaCreditCustomerBL = new NotaCreditCustomerBL();
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

                this.ShowAccount();
                this.ShowUnit();
                this.SetCurrRate();
                this.ClearLabel();
                this.SetAttribute();
                this.ClearData();
            }
        }

        private void SetAttributeRate()
        {
            this.PriceTextBox.Attributes.Add("OnBlur", "Count(" + this.QtyTextBox.ClientID + "," + this.PriceTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + " );");
            this.QtyTextBox.Attributes.Add("OnBlur", "Count(" + this.QtyTextBox.ClientID + "," + this.PriceTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "  );");
        }

        protected void SetAttribute()
        {
            this.AmountTextBox.Attributes.Add("ReadOnly", "True");
            this.QtyTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.PriceTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.SetAttributeRate();
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearData()
        {
            this.ClearLabel();
            this.AccountTextBox.Text = "";
            this.AccountDropDownList.SelectedValue = "null";
            this.FgSubledHiddenField.Value = "";
            this.SubledDropDownList.Items.Clear();
            this.SubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.RemarkTextBox.Text = "";
            this.PriceTextBox.Text = "0";
            this.AmountTextBox.Text = "0";
            this.QtyTextBox.Text = "1";
            this.UnitDropDownList.SelectedValue = "null";
        }

        public void ShowAccount()
        {
            string _currHeader = this._notaCreditCustomerBL.GetSingleFINCNCustHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).CurrCode;

            this.AccountDropDownList.Items.Clear();
            this.AccountDropDownList.DataTextField = "AccountName";
            this.AccountDropDownList.DataValueField = "Account";
            this.AccountDropDownList.DataSource = this._accountBL.GetListAccountByTransType(_currHeader, _userEmpBL.GetEmployeeIDByUserName(HttpContext.Current.User.Identity.Name), AppModule.GetValue(TransactionType.NotaCreditCustomer));
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
            bool _exist = this._accountBL.IsExistAccountByTransType(AppModule.GetValue(TransactionType.NotaCreditCustomer), this.AccountTextBox.Text);
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
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            int _maxItemNo = this._notaCreditCustomerBL.GetMaxNoItemFINCNCustDt(_transNo);

            FINCNCustDt _finCNCustDt = new FINCNCustDt();
            _finCNCustDt.TransNmbr = _transNo;
            _finCNCustDt.ItemNo = _maxItemNo + 1;
            if (this.SubledDropDownList.SelectedValue != "null")
            {
                _finCNCustDt.FgSubLed = Convert.ToChar(this.FgSubledHiddenField.Value);
                _finCNCustDt.SubLed = this.SubledDropDownList.SelectedValue;
            }
            else
            {
                _finCNCustDt.FgSubLed = 'N';
                _finCNCustDt.SubLed = null;
            }
            _finCNCustDt.Account = this.AccountTextBox.Text;
            _finCNCustDt.Remark = this.RemarkTextBox.Text;
            _finCNCustDt.PriceForex = Convert.ToDecimal(this.PriceTextBox.Text);
            _finCNCustDt.AmountForex = Convert.ToDecimal(this.AmountTextBox.Text);
            _finCNCustDt.Qty = Convert.ToInt32(this.QtyTextBox.Text);
            _finCNCustDt.Unit = this.UnitDropDownList.SelectedValue;

            bool _result = this._notaCreditCustomerBL.AddFINCNCustDt(_finCNCustDt);

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

        private void SetCurrRate()
        {
            string _currHeader = this._notaCreditCustomerBL.GetSingleFINCNCustHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).CurrCode;

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currHeader);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
        }
    }
}