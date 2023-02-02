using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.SupplierGroup
{
    public partial class SupplierGroupAccountAdd : SupplierGroupBase
    {
        private SupplierBL _supplierBL = new SupplierBL();
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
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowCurrency();

                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void SetAttribute()
        {
            this.AccountAPDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountAPDropDownList.ClientID + "," + this.AccountAPTextBox.ClientID + ");");
            this.AccountAPTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountAPDropDownList.ClientID + "," + this.AccountAPTextBox.ClientID + ");");

            this.AccountAPTransitDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountAPTransitDropDownList.ClientID + "," + this.AccountAPTransitTextBox.ClientID + ");");
            this.AccountAPTransitTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountAPTransitDropDownList.ClientID + "," + this.AccountAPTransitTextBox.ClientID + ");");

            this.AccountDebitAPDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountDebitAPDropDownList.ClientID + "," + this.AccountDebitAPTextBox.ClientID + ");");
            this.AccountDebitAPTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountDebitAPDropDownList.ClientID + "," + this.AccountDebitAPTextBox.ClientID + ");");

            this.AccountDPDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountDPDropDownList.ClientID + "," + this.AccountDPTextBox.ClientID + ");");
            this.AccountDPTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountDPDropDownList.ClientID + "," + this.AccountDPTextBox.ClientID + ");");

            this.AccountVariantPODropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountVariantPODropDownList.ClientID + "," + this.AccountVariantPOTextBox.ClientID + ");");
            this.AccountVariantPOTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountVariantPODropDownList.ClientID + "," + this.AccountVariantPOTextBox.ClientID + ");");

            this.AccountPPnDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountPPnDropDownList.ClientID + "," + this.AccountPPnTextBox.ClientID + ");");
            this.AccountPPnTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountPPnDropDownList.ClientID + "," + this.AccountPPnTextBox.ClientID + ");");

            this.AccountPPhDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountPPhDropDownList.ClientID + "," + this.AccountPPhTextBox.ClientID + ");");
            this.AccountPPhTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountPPhDropDownList.ClientID + "," + this.AccountPPhTextBox.ClientID + ");");

            this.AccountOtherPurchaseDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountOtherPurchaseDropDownList.ClientID + "," + this.AccountOtherPurchaseTextBox.ClientID + ");");
            this.AccountOtherPurchaseTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountOtherPurchaseDropDownList.ClientID + "," + this.AccountOtherPurchaseTextBox.ClientID + ");");

            this.AccountDiscPurchaseDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountDiscPurchaseDropDownList.ClientID + "," + this.AccountDiscPurchaseTextBox.ClientID + ");");
            this.AccountDiscPurchaseTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountDiscPurchaseDropDownList.ClientID + "," + this.AccountDiscPurchaseTextBox.ClientID + ");");

            this.AccountDutyTransitDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountDutyTransitDropDownList.ClientID + "," + this.AccountDutyTransitTextBox.ClientID + ");");
            this.AccountDutyTransitTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountDutyTransitDropDownList.ClientID + "," + this.AccountDutyTransitTextBox.ClientID + ");");

            this.AccountHandlingTransitDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountHandlingTransitDropDownList.ClientID + "," + this.AccountHandlingTransitTextBox.ClientID + ");");
            this.AccountHandlingTransitTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountHandlingTransitDropDownList.ClientID + "," + this.AccountHandlingTransitTextBox.ClientID + ");");

        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearDropDown()
        {
            this.AccountAPDropDownList.Items.Clear();
            this.AccountAPTransitDropDownList.Items.Clear();
            this.AccountDebitAPDropDownList.Items.Clear();
            this.AccountDPDropDownList.Items.Clear();
            this.AccountVariantPODropDownList.Items.Clear();
            this.AccountPPnDropDownList.Items.Clear();
            this.AccountPPhDropDownList.Items.Clear();
            this.AccountOtherPurchaseDropDownList.Items.Clear();
            this.AccountDiscPurchaseDropDownList.Items.Clear();
            this.AccountDutyTransitDropDownList.Items.Clear();
            this.AccountHandlingTransitDropDownList.Items.Clear();

            this.AccountAPDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.AccountAPTransitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.AccountDebitAPDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.AccountDPDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.AccountVariantPODropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.AccountPPnDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.AccountPPhDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.AccountOtherPurchaseDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.AccountDiscPurchaseDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.AccountDutyTransitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.AccountHandlingTransitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

            this.AccountAPDropDownList.SelectedValue = "null";
            this.AccountAPTransitDropDownList.SelectedValue = "null";
            this.AccountDebitAPDropDownList.SelectedValue = "null";
            this.AccountDPDropDownList.SelectedValue = "null";
            this.AccountVariantPODropDownList.SelectedValue = "null";
            this.AccountPPnDropDownList.SelectedValue = "null";
            this.AccountPPhDropDownList.SelectedValue = "null";
            this.AccountOtherPurchaseDropDownList.SelectedValue = "null";
            this.AccountDiscPurchaseDropDownList.SelectedValue = "null";
            this.AccountDutyTransitDropDownList.SelectedValue = "null";
            this.AccountHandlingTransitDropDownList.SelectedValue = "null";
        }

        public void ClearData()
        {
            this.ClearLabel();

            this.AccountAPTextBox.Text = "";
            this.AccountAPTransitTextBox.Text = "";
            this.AccountDebitAPTextBox.Text = "";
            this.AccountDPTextBox.Text = "";
            this.AccountVariantPOTextBox.Text = "";
            this.AccountPPnTextBox.Text = "";
            this.AccountPPhTextBox.Text = "";
            this.AccountOtherPurchaseTextBox.Text = "";
            this.AccountDiscPurchaseTextBox.Text = "";
            this.AccountDutyTransitTextBox.Text = "";
            this.AccountHandlingTransitTextBox.Text = "";
            this.FgActiveCheckBox.Checked = false;
            this.RemarkTextBox.Text = "";

            this.ClearDropDown();
        }

        private void ShowCurrency()
        {
            this.CurrencyDropDownList.DataTextField = "CurrCode";
            this.CurrencyDropDownList.DataValueField = "CurrCode";
            this.CurrencyDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrencyDropDownList.DataBind();
            this.CurrencyDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowAccountAP(string _prmCurrCode)
        {
            this.AccountAPDropDownList.Items.Clear();
            this.AccountAPDropDownList.DataTextField = "AccountName";
            this.AccountAPDropDownList.DataValueField = "Account";
            this.AccountAPDropDownList.DataSource = this._accountBL.GetListForDDL(_prmCurrCode);
            this.AccountAPDropDownList.DataBind();
            this.AccountAPDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowAccountAPTransit()
        {
            this.AccountAPTransitDropDownList.Items.Clear();
            this.AccountAPTransitDropDownList.DataTextField = "AccountName";
            this.AccountAPTransitDropDownList.DataValueField = "Account";
            this.AccountAPTransitDropDownList.DataSource = this.AccountAPDropDownList.DataSource;
            this.AccountAPTransitDropDownList.DataBind();
            this.AccountAPTransitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowAccountDebitAP()
        {
            this.AccountDebitAPDropDownList.Items.Clear();
            this.AccountDebitAPDropDownList.DataTextField = "AccountName";
            this.AccountDebitAPDropDownList.DataValueField = "Account";
            this.AccountDebitAPDropDownList.DataSource = this.AccountAPDropDownList.DataSource;
            this.AccountDebitAPDropDownList.DataBind();
            this.AccountDebitAPDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowAccountDP()
        {
            this.AccountDPDropDownList.Items.Clear();
            this.AccountDPDropDownList.DataTextField = "AccountName";
            this.AccountDPDropDownList.DataValueField = "Account";
            this.AccountDPDropDownList.DataSource = this.AccountAPDropDownList.DataSource;
            this.AccountDPDropDownList.DataBind();
            this.AccountDPDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowAccountVariantPO()
        {
            this.AccountVariantPODropDownList.Items.Clear();
            this.AccountVariantPODropDownList.DataTextField = "AccountName";
            this.AccountVariantPODropDownList.DataValueField = "Account";
            this.AccountVariantPODropDownList.DataSource = this.AccountAPDropDownList.DataSource;
            this.AccountVariantPODropDownList.DataBind();
            this.AccountVariantPODropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowAccountPPn()
        {
            this.AccountPPnDropDownList.Items.Clear();
            this.AccountPPnDropDownList.DataTextField = "AccountName";
            this.AccountPPnDropDownList.DataValueField = "Account";
            this.AccountPPnDropDownList.DataSource = this.AccountAPDropDownList.DataSource;
            this.AccountPPnDropDownList.DataBind();
            this.AccountPPnDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowAccountPPh()
        {
            this.AccountPPhDropDownList.Items.Clear();
            this.AccountPPhDropDownList.DataTextField = "AccountName";
            this.AccountPPhDropDownList.DataValueField = "Account";
            this.AccountPPhDropDownList.DataSource = this.AccountAPDropDownList.DataSource;
            this.AccountPPhDropDownList.DataBind();
            this.AccountPPhDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowAccountOtherPurchase()
        {
            this.AccountOtherPurchaseDropDownList.Items.Clear();
            this.AccountOtherPurchaseDropDownList.DataTextField = "AccountName";
            this.AccountOtherPurchaseDropDownList.DataValueField = "Account";
            this.AccountOtherPurchaseDropDownList.DataSource = this.AccountAPDropDownList.DataSource;
            this.AccountOtherPurchaseDropDownList.DataBind();
            this.AccountOtherPurchaseDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowAccountDiscPurchase()
        {
            this.AccountDiscPurchaseDropDownList.Items.Clear();
            this.AccountDiscPurchaseDropDownList.DataTextField = "AccountName";
            this.AccountDiscPurchaseDropDownList.DataValueField = "Account";
            this.AccountDiscPurchaseDropDownList.DataSource = this.AccountAPDropDownList.DataSource;
            this.AccountDiscPurchaseDropDownList.DataBind();
            this.AccountDiscPurchaseDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowAccountDutyTransit()
        {
            this.AccountDutyTransitDropDownList.Items.Clear();
            this.AccountDutyTransitDropDownList.DataTextField = "AccountName";
            this.AccountDutyTransitDropDownList.DataValueField = "Account";
            this.AccountDutyTransitDropDownList.DataSource = this.AccountAPDropDownList.DataSource;
            this.AccountDutyTransitDropDownList.DataBind();
            this.AccountDutyTransitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowAccountHandlingTransit()
        {
            this.AccountHandlingTransitDropDownList.Items.Clear();
            this.AccountHandlingTransitDropDownList.DataTextField = "AccountName";
            this.AccountHandlingTransitDropDownList.DataValueField = "Account";
            this.AccountHandlingTransitDropDownList.DataSource = this.AccountAPDropDownList.DataSource;
            this.AccountHandlingTransitDropDownList.DataBind();
            this.AccountHandlingTransitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void CurrencyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CurrencyDropDownList.SelectedValue != "null")
            {
                this.ShowAccountAP(this.CurrencyDropDownList.SelectedValue);
                this.ShowAccountAPTransit();
                this.ShowAccountDebitAP();
                this.ShowAccountDP();
                this.ShowAccountVariantPO();
                this.ShowAccountPPn();
                this.ShowAccountPPh();
                this.ShowAccountOtherPurchase();
                this.ShowAccountDiscPurchase();
                this.ShowAccountDutyTransit();
                this.ShowAccountHandlingTransit();
            }
            else
            {
                this.ClearDropDown();
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsSuppGroupAcc _msSuppGroupAcc = new MsSuppGroupAcc();

            _msSuppGroupAcc.SuppGroup = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            _msSuppGroupAcc.CurrCode = this.CurrencyDropDownList.SelectedValue;
            _msSuppGroupAcc.AccAP = this.AccountAPTextBox.Text;
            _msSuppGroupAcc.AccAPPending = this.AccountAPTransitTextBox.Text;
            _msSuppGroupAcc.AccDebitAP = this.AccountDebitAPTextBox.Text;
            _msSuppGroupAcc.AccDP = this.AccountDPTextBox.Text;
            _msSuppGroupAcc.AccVariantPO = this.AccountVariantPOTextBox.Text;
            _msSuppGroupAcc.AccPPn = this.AccountPPnTextBox.Text;
            _msSuppGroupAcc.AccPPh = this.AccountPPhTextBox.Text;
            _msSuppGroupAcc.AccOther = this.AccountOtherPurchaseTextBox.Text;
            _msSuppGroupAcc.AccDisc = this.AccountDiscPurchaseTextBox.Text;
            _msSuppGroupAcc.AccHandling = this.AccountHandlingTransitTextBox.Text;
            _msSuppGroupAcc.AccDuty = this.AccountDutyTransitTextBox.Text;
            _msSuppGroupAcc.FgActive = (this.FgActiveCheckBox.Checked == true) ? 'Y' : 'N';
            _msSuppGroupAcc.Remark = this.RemarkTextBox.Text;
            _msSuppGroupAcc.CreatedBy = HttpContext.Current.User.Identity.Name;
            _msSuppGroupAcc.CreatedDate = DateTime.Now;

            bool _result = this._supplierBL.AddSuppGroupAcc(_msSuppGroupAcc);

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
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

    }
}