using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.CustomerGroup
{
    public partial class CustomerGroupAccountAdd : CustomerGroupBase
    {
        private CustomerBL _customerBL = new CustomerBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private AccountBL _accountBL = new AccountBL();
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

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowCurrency();

                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribute()
        {
            this.AccountARDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountARDropDownList.ClientID + "," + this.AccountARTextBox.ClientID + ");");
            this.AccountARTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountARDropDownList.ClientID + "," + this.AccountARTextBox.ClientID + ");");

            this.AccountDiscDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountDiscDropDownList.ClientID + "," + this.AccountDiscTextBox.ClientID + ");");
            this.AccountDiscTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountDiscDropDownList.ClientID + "," + this.AccountDiscTextBox.ClientID + ");");

            this.AccountCreditARDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountCreditARDropDownList.ClientID + "," + this.AccountCreditARTextBox.ClientID + ");");
            this.AccountCreditARTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountCreditARDropDownList.ClientID + "," + this.AccountCreditARTextBox.ClientID + ");");

            this.AccountDPDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountDPDropDownList.ClientID + "," + this.AccountDPTextBox.ClientID + ");");
            this.AccountDPTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountDPDropDownList.ClientID + "," + this.AccountDPTextBox.ClientID + ");");

            this.AccountPPNDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountPPNDropDownList.ClientID + "," + this.AccountPPNTextBox.ClientID + ");");
            this.AccountPPNTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountPPNDropDownList.ClientID + "," + this.AccountPPNTextBox.ClientID + ");");

            this.AccountOtherDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountOtherDropDownList.ClientID + "," + this.AccountOtherTextBox.ClientID + ");");
            this.AccountOtherTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountOtherTextBox.ClientID + "," + this.AccountOtherTextBox.ClientID + ");");
        }

        public void ClearData()
        {
            this.ClearLabel();

            this.CustGroupCodeTextBox.Text = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            this.AccountARTextBox.Text = "";
            this.AccountDiscTextBox.Text = "";
            this.AccountCreditARTextBox.Text = "";
            this.AccountDPTextBox.Text = "";
            this.AccountPPNTextBox.Text = "";
            this.AccountOtherTextBox.Text = "";

            this.CurrencyDropDownList.SelectedValue = "null";

            this.AccountARDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.AccountARDropDownList.SelectedValue = "null";
            this.AccountDiscDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.AccountDiscDropDownList.SelectedValue = "null";
            this.AccountCreditARDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.AccountCreditARDropDownList.SelectedValue = "null";
            this.AccountDPDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.AccountDPDropDownList.SelectedValue = "null";
            this.AccountPPNDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.AccountPPNDropDownList.SelectedValue = "null";
            this.AccountOtherDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.AccountOtherDropDownList.SelectedValue = "null";
        }

        public void ShowCurrency()
        {
            this.CurrencyDropDownList.Items.Clear();
            this.CurrencyDropDownList.DataTextField = "CurrCode";
            this.CurrencyDropDownList.DataValueField = "CurrCode";
            this.CurrencyDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrencyDropDownList.DataBind();
            this.CurrencyDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void CurrencyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CurrencyDropDownList.SelectedValue != "null")
            {
                this.ShowAccountAR();
                this.ShowAccountDisc();
                this.ShowAccountCreditAR();
                this.ShowAccountDP();
                this.ShowAccountPPn();
                this.ShowAccountOther();
            }
            else
            {
                this.ShowAccountAR();
                this.ShowAccountDisc();
                this.ShowAccountCreditAR();
                this.ShowAccountDP();
                this.ShowAccountPPn();
                this.ShowAccountOther();

                this.ClearData();
            }
        }

        public void ShowAccountAR()
        {
            this.AccountARDropDownList.Items.Clear();
            this.AccountARDropDownList.DataTextField = "AccountName";
            this.AccountARDropDownList.DataValueField = "Account";
            this.AccountARDropDownList.DataSource = this._accountBL.GetListForDDL(this.CurrencyDropDownList.SelectedValue);
            this.AccountARDropDownList.DataBind();
            this.AccountARDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowAccountDisc()
        {
            this.AccountDiscDropDownList.Items.Clear();
            this.AccountDiscDropDownList.DataTextField = "AccountName";
            this.AccountDiscDropDownList.DataValueField = "Account";
            this.AccountDiscDropDownList.DataSource = this.AccountARDropDownList.DataSource;
            this.AccountDiscDropDownList.DataBind();
            this.AccountDiscDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowAccountCreditAR()
        {
            this.AccountCreditARDropDownList.Items.Clear();
            this.AccountCreditARDropDownList.DataTextField = "AccountName";
            this.AccountCreditARDropDownList.DataValueField = "Account";
            this.AccountCreditARDropDownList.DataSource = this.AccountARDropDownList.DataSource;
            this.AccountCreditARDropDownList.DataBind();
            this.AccountCreditARDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowAccountDP()
        {
            this.AccountDPDropDownList.Items.Clear();
            this.AccountDPDropDownList.DataTextField = "AccountName";
            this.AccountDPDropDownList.DataValueField = "Account";
            this.AccountDPDropDownList.DataSource = this.AccountARDropDownList.DataSource;
            this.AccountDPDropDownList.DataBind();
            this.AccountDPDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowAccountPPn()
        {
            this.AccountPPNDropDownList.Items.Clear();
            this.AccountPPNDropDownList.DataTextField = "AccountName";
            this.AccountPPNDropDownList.DataValueField = "Account";
            this.AccountPPNDropDownList.DataSource = this.AccountARDropDownList.DataSource;
            this.AccountPPNDropDownList.DataBind();
            this.AccountPPNDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowAccountOther()
        {
            this.AccountOtherDropDownList.Items.Clear();
            this.AccountOtherDropDownList.DataTextField = "AccountName";
            this.AccountOtherDropDownList.DataValueField = "Account";
            this.AccountOtherDropDownList.DataSource = this.AccountARDropDownList.DataSource;
            this.AccountOtherDropDownList.DataBind();
            this.AccountOtherDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsCustGroupAcc _msCustGroupAcc = new MsCustGroupAcc();

            _msCustGroupAcc.CustGroup = this.CustGroupCodeTextBox.Text;
            _msCustGroupAcc.CurrCode = this.CurrencyDropDownList.SelectedValue;
            _msCustGroupAcc.AccAR = this.AccountARTextBox.Text;
            _msCustGroupAcc.AccDisc = this.AccountDiscTextBox.Text;
            _msCustGroupAcc.AccCreditAR = this.AccountCreditARTextBox.Text;
            _msCustGroupAcc.AccDP = this.AccountDPTextBox.Text;
            _msCustGroupAcc.AccPPn = this.AccountPPNTextBox.Text;
            _msCustGroupAcc.AccOther = this.AccountOtherTextBox.Text;
            _msCustGroupAcc.UserId = HttpContext.Current.User.Identity.Name;
            _msCustGroupAcc.UserDate = DateTime.Now;

            bool _result = this._customerBL.AddCustGroupAcc(_msCustGroupAcc);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}