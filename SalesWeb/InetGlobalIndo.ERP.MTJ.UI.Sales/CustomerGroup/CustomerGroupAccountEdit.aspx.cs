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
    public partial class CustomerGroupAccountEdit : CustomerGroupBase
    {
        private CustomerBL _customerBL = new CustomerBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private AccountBL _accountBL = new AccountBL();
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

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

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
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

        public void ShowData()
        {
            MsCustGroupAcc _msCustGroupAcc = this._customerBL.GetSingleCustGroupAcc(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._currCodeKey), ApplicationConfig.EncryptionKey));

            this.CustGroupCodeTextBox.Text = _msCustGroupAcc.CustGroup;
            if (_msCustGroupAcc.CurrCode != null && _msCustGroupAcc.CurrCode != "")
            {
                this.CurrencyTextBox.Text = _msCustGroupAcc.CurrCode;
                this.ShowAccountAR();
                this.ShowAccountDisc();
                this.ShowAccountCreditAR();
                this.ShowAccountDP();
                this.ShowAccountPPn();
                this.ShowAccountOther();
            }

            if (_msCustGroupAcc.AccAR != null && _msCustGroupAcc.AccAR != "")
            {
                this.AccountARTextBox.Text = _msCustGroupAcc.AccAR;
                this.AccountARDropDownList.SelectedValue = _msCustGroupAcc.AccAR;
            }
            else
            {
                this.AccountARTextBox.Text = "";
                this.AccountARDropDownList.SelectedValue = "null";
            }

            if (_msCustGroupAcc.AccDisc != null && _msCustGroupAcc.AccDisc != "")
            {
                this.AccountDiscTextBox.Text = _msCustGroupAcc.AccDisc;
                this.AccountDiscDropDownList.SelectedValue = _msCustGroupAcc.AccDisc;
            }
            else
            {
                this.AccountDiscTextBox.Text = "";
                this.AccountDiscDropDownList.SelectedValue = "null";
            }

            if (_msCustGroupAcc.AccCreditAR != null && _msCustGroupAcc.AccCreditAR != "")
            {
                this.AccountCreditARTextBox.Text = _msCustGroupAcc.AccCreditAR;
                this.AccountCreditARDropDownList.SelectedValue = _msCustGroupAcc.AccCreditAR;
            }
            else
            {
                this.AccountCreditARTextBox.Text = "";
                this.AccountCreditARDropDownList.SelectedValue = "null";
            }

            if (_msCustGroupAcc.AccDP != null && _msCustGroupAcc.AccDP != "")
            {
                this.AccountDPTextBox.Text = _msCustGroupAcc.AccDP;
                this.AccountDPDropDownList.SelectedValue = _msCustGroupAcc.AccDP;
            }
            else
            {
                this.AccountDPTextBox.Text = "";
                this.AccountDPDropDownList.SelectedValue = "null";
            }

            if (_msCustGroupAcc.AccPPn != null && _msCustGroupAcc.AccPPn != "")
            {
                this.AccountPPNTextBox.Text = _msCustGroupAcc.AccPPn;
                this.AccountPPNDropDownList.SelectedValue = _msCustGroupAcc.AccPPn;
            }
            else
            {
                this.AccountPPNTextBox.Text = "";
                this.AccountPPNDropDownList.SelectedValue = "null";
            }

            if (_msCustGroupAcc.AccOther != null && _msCustGroupAcc.AccOther != "")
            {
                this.AccountOtherTextBox.Text = _msCustGroupAcc.AccOther;
                this.AccountOtherDropDownList.SelectedValue = _msCustGroupAcc.AccOther;
            }
            else
            {
                this.AccountOtherTextBox.Text = "";
                this.AccountOtherDropDownList.SelectedValue = "null";
            }
        }

        public void ShowAccountAR()
        {
            this.AccountARDropDownList.Items.Clear();
            this.AccountARDropDownList.DataTextField = "AccountName";
            this.AccountARDropDownList.DataValueField = "Account";
            this.AccountARDropDownList.DataSource = this._accountBL.GetListForDDL(this.CurrencyTextBox.Text);
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
            MsCustGroupAcc _msCustGroupAcc = this._customerBL.GetSingleCustGroupAcc(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._currCodeKey), ApplicationConfig.EncryptionKey));

            _msCustGroupAcc.AccAR = this.AccountARTextBox.Text;
            _msCustGroupAcc.AccDisc = this.AccountDiscTextBox.Text;
            _msCustGroupAcc.AccCreditAR = this.AccountCreditARTextBox.Text;
            _msCustGroupAcc.AccDP = this.AccountDPTextBox.Text;
            _msCustGroupAcc.AccPPn = this.AccountPPNTextBox.Text;
            _msCustGroupAcc.AccOther = this.AccountOtherTextBox.Text;
            _msCustGroupAcc.UserId = HttpContext.Current.User.Identity.Name;
            _msCustGroupAcc.UserDate = DateTime.Now;

            bool _result = this._customerBL.EditCustGroupAcc(_msCustGroupAcc);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }
    }
}