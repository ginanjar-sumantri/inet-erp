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
    public partial class SupplierGroupAccountEdit : SupplierGroupBase
    {
        private SupplierBL _supplier = new SupplierBL();
        private CurrencyBL _currency = new CurrencyBL();
        private AccountBL _account = new AccountBL();
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
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
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

        public void ShowData()
        {
            MsSuppGroupAcc _msSuppGroupAcc = this._supplier.GetSingleSuppGroupAcc(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._currCode), ApplicationConfig.EncryptionKey));
            this.CurrencyTextBox.Text = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._currCode), ApplicationConfig.EncryptionKey);

            this.ShowAccountAP(this.CurrencyTextBox.Text);
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

            if (_msSuppGroupAcc.AccAP != null || _msSuppGroupAcc.AccAP != "")
            {
                this.AccountAPTextBox.Text = _msSuppGroupAcc.AccAP;
                this.AccountAPDropDownList.SelectedValue = _msSuppGroupAcc.AccAP;
            }
            else
            {
                this.AccountAPTextBox.Text = "";
                this.AccountAPDropDownList.SelectedValue = "null";
            }

            if (_msSuppGroupAcc.AccAPPending != null || _msSuppGroupAcc.AccAPPending != "")
            {
                this.AccountAPTransitTextBox.Text = _msSuppGroupAcc.AccAPPending;
                this.AccountAPTransitDropDownList.SelectedValue = _msSuppGroupAcc.AccAPPending;
            }
            else
            {
                this.AccountAPTransitTextBox.Text = "";
                this.AccountAPTransitDropDownList.SelectedValue = "null";
            }

            if (_msSuppGroupAcc.AccDebitAP != null && _msSuppGroupAcc.AccDebitAP != "")
            {
                this.AccountDebitAPTextBox.Text = _msSuppGroupAcc.AccDebitAP;
                this.AccountDebitAPDropDownList.SelectedValue = _msSuppGroupAcc.AccDebitAP;
            }
            else
            {
                this.AccountDebitAPTextBox.Text = "";
                this.AccountDebitAPDropDownList.SelectedValue = "null";
            }

            if (_msSuppGroupAcc.AccDP != null && _msSuppGroupAcc.AccDP != "")
            {
                this.AccountDPTextBox.Text = _msSuppGroupAcc.AccDP;
                this.AccountDPDropDownList.SelectedValue = _msSuppGroupAcc.AccDP;
            }
            else
            {
                this.AccountDPTextBox.Text = "";
                this.AccountDPDropDownList.SelectedValue = "null";
            }

            if (_msSuppGroupAcc.AccVariantPO != null && _msSuppGroupAcc.AccVariantPO != "")
            {
                this.AccountVariantPOTextBox.Text = _msSuppGroupAcc.AccVariantPO;
                this.AccountVariantPODropDownList.SelectedValue = _msSuppGroupAcc.AccVariantPO;
            }
            else
            {
                this.AccountVariantPOTextBox.Text = "";
                this.AccountVariantPODropDownList.SelectedValue = "null";
            }

            if (_msSuppGroupAcc.AccPPn != null && _msSuppGroupAcc.AccPPn != "")
            {
                this.AccountPPnTextBox.Text = _msSuppGroupAcc.AccPPn;
                this.AccountPPnDropDownList.SelectedValue = _msSuppGroupAcc.AccPPn;
            }
            else
            {
                this.AccountPPnTextBox.Text = "";
                this.AccountPPnDropDownList.SelectedValue = "null";
            }

            if (_msSuppGroupAcc.AccPPh != null && _msSuppGroupAcc.AccPPh != "")
            {
                this.AccountPPhTextBox.Text = _msSuppGroupAcc.AccPPh;
                this.AccountPPhDropDownList.SelectedValue = _msSuppGroupAcc.AccPPh;
            }
            else
            {
                this.AccountPPhTextBox.Text = "";
                this.AccountPPhDropDownList.SelectedValue = "null";
            }

            if (_msSuppGroupAcc.AccOther != null && _msSuppGroupAcc.AccOther != "")
            {
                this.AccountOtherPurchaseTextBox.Text = _msSuppGroupAcc.AccOther;
                this.AccountOtherPurchaseDropDownList.SelectedValue = _msSuppGroupAcc.AccOther;
            }
            else
            {
                this.AccountOtherPurchaseTextBox.Text = "";
                this.AccountOtherPurchaseDropDownList.SelectedValue = "null";
            }

            if (_msSuppGroupAcc.AccDisc != null && _msSuppGroupAcc.AccDisc != "")
            {
                this.AccountDiscPurchaseTextBox.Text = _msSuppGroupAcc.AccDisc;
                this.AccountDiscPurchaseDropDownList.SelectedValue = _msSuppGroupAcc.AccDisc;
            }
            else
            {
                this.AccountDiscPurchaseTextBox.Text = "";
                this.AccountDiscPurchaseDropDownList.SelectedValue = "null";
            }

            if (_msSuppGroupAcc.AccDuty != null && _msSuppGroupAcc.AccDuty != "")
            {
                this.AccountDutyTransitTextBox.Text = _msSuppGroupAcc.AccDuty;
                this.AccountDutyTransitDropDownList.SelectedValue = _msSuppGroupAcc.AccDuty;
            }
            else
            {
                this.AccountDutyTransitTextBox.Text = "";
                this.AccountDutyTransitDropDownList.SelectedValue = "null";
            }

            if (_msSuppGroupAcc.AccHandling != null && _msSuppGroupAcc.AccHandling != "")
            {
                this.AccountHandlingTransitTextBox.Text = _msSuppGroupAcc.AccHandling;
                this.AccountHandlingTransitDropDownList.SelectedValue = _msSuppGroupAcc.AccHandling;
            }
            else
            {
                this.AccountHandlingTransitTextBox.Text = "";
                this.AccountHandlingTransitDropDownList.SelectedValue = "null";
            }
            this.FgActiveCheckBox.Checked = (_msSuppGroupAcc.FgActive == 'Y') ? true : false;
            this.RemarkTextBox.Text = _msSuppGroupAcc.Remark;
        }

        //private void ShowCurrency()
        //{
        //    this.CurrencyDropDownList.DataTextField = "CurrCode";
        //    this.CurrencyDropDownList.DataValueField = "CurrCode";
        //    this.CurrencyDropDownList.DataSource = this._currency.GetListAll();
        //    this.CurrencyDropDownList.DataBind();
        //    this.CurrencyDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        public void ShowAccountAP(string _prmCurrCode)
        {
            this.AccountAPDropDownList.Items.Clear();
            this.AccountAPDropDownList.DataTextField = "AccountName";
            this.AccountAPDropDownList.DataValueField = "Account";
            this.AccountAPDropDownList.DataSource = this._account.GetListForDDL(_prmCurrCode);
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

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsSuppGroupAcc _msSuppGroupAcc = this._supplier.GetSingleSuppGroupAcc(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), this.CurrencyTextBox.Text);

            _msSuppGroupAcc.AccAP = this.AccountAPTextBox.Text;
            _msSuppGroupAcc.AccAPPending = this.AccountAPTransitTextBox.Text;
            _msSuppGroupAcc.AccDebitAP = this.AccountDebitAPTextBox.Text;
            _msSuppGroupAcc.AccDP = this.AccountDPTextBox.Text;
            _msSuppGroupAcc.AccVariantPO = this.AccountVariantPOTextBox.Text;
            _msSuppGroupAcc.AccPPn = this.AccountPPnTextBox.Text;
            _msSuppGroupAcc.AccPPh = this.AccountPPhTextBox.Text;
            _msSuppGroupAcc.AccOther = this.AccountOtherPurchaseTextBox.Text;
            _msSuppGroupAcc.AccDisc = this.AccountDiscPurchaseTextBox.Text;
            _msSuppGroupAcc.AccDuty = this.AccountDutyTransitTextBox.Text;
            _msSuppGroupAcc.AccHandling = this.AccountHandlingTransitTextBox.Text;

            _msSuppGroupAcc.FgActive = (this.FgActiveCheckBox.Checked == true) ? 'Y' : 'N';
            _msSuppGroupAcc.Remark = this.RemarkTextBox.Text;
            _msSuppGroupAcc.ModifiedBy = HttpContext.Current.User.Identity.Name;
            _msSuppGroupAcc.ModifiedDate = DateTime.Now;

            bool _result = this._supplier.EditSuppGroupAcc(_msSuppGroupAcc);

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
            this.ClearLabel();
            this.ShowData();
        }
    }
}