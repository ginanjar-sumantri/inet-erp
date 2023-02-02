using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FASubGroup
{
    public partial class FAGroupSubAccEdit : FASubGroupBase
    {
        private FixedAssetsBL _fixedAssetBL = new FixedAssetsBL();
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

                this.ShowCurrency();

                this.SetAttribute();
                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.AccountAssetDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountAssetDropDownList.ClientID + "," + this.AccAssetsTextBox.ClientID + ");");
            this.AccAssetsTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountAssetDropDownList.ClientID + "," + this.AccAssetsTextBox.ClientID + ");");

            this.AccountDPDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountDPDropDownList.ClientID + "," + this.AccDepreciationTextBox.ClientID + ");");
            this.AccDepreciationTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountDPDropDownList.ClientID + "," + this.AccDepreciationTextBox.ClientID + ");");

            this.AccountAkumDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountAkumDropDownList.ClientID + "," + this.AccAkumTextBox.ClientID + ");");
            this.AccAkumTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountAkumDropDownList.ClientID + "," + this.AccAkumTextBox.ClientID + ");");

            this.AccountSalesDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountSalesDropDownList.ClientID + "," + this.AccSalesTextBox.ClientID + ");");
            this.AccSalesTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountSalesDropDownList.ClientID + "," + this.AccSalesTextBox.ClientID + ");");

            this.AccountTenancyDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountTenancyDropDownList.ClientID + "," + this.AccTenancyTextBox.ClientID + ");");
            this.AccTenancyTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountTenancyDropDownList.ClientID + "," + this.AccTenancyTextBox.ClientID + ");");
        }

        public void ShowData()
        {
            MsFAGroupSubAcc _msFAGroupSubAcc = this._fixedAssetBL.GetSingleFAGroupSubAcc(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._currCodeKey), ApplicationConfig.EncryptionKey));

            if (_msFAGroupSubAcc.CurrCode != null && _msFAGroupSubAcc.CurrCode != "")
            {
                this.CurrencyDropDownList.SelectedValue = _msFAGroupSubAcc.CurrCode;

                if (_msFAGroupSubAcc.AccAkumDepr != "null")
                {
                    this.AccAkumTextBox.Text = _msFAGroupSubAcc.AccAkumDepr;
                }
                else
                {
                    this.AccAkumTextBox.Text = "";
                }
                if (_msFAGroupSubAcc.AccFA != "null")
                {
                    this.AccAssetsTextBox.Text = _msFAGroupSubAcc.AccFA;
                }
                else
                {
                    this.AccAssetsTextBox.Text = "";
                }
                if (_msFAGroupSubAcc.AccDepr != "null")
                {
                    this.AccDepreciationTextBox.Text = _msFAGroupSubAcc.AccDepr;
                }
                else
                {
                    this.AccDepreciationTextBox.Text = "";
                }
                if (_msFAGroupSubAcc.AccSales != "null")
                {
                    this.AccSalesTextBox.Text = _msFAGroupSubAcc.AccSales;
                }
                else
                {
                    this.AccSalesTextBox.Text = "";
                }
                if (_msFAGroupSubAcc.AccTenancy != "null")
                {
                    this.AccTenancyTextBox.Text = _msFAGroupSubAcc.AccTenancy;
                }
                else
                {
                    this.AccTenancyTextBox.Text = "";
                }

                this.ShowAccountAkum();
                this.ShowAccountAsset();
                this.ShowAccountDP();
                this.ShowAccountSales();
                this.ShowAccountTanancy();
            }

            if (_msFAGroupSubAcc.AccAkumDepr != null && _msFAGroupSubAcc.AccAkumDepr != "")
            {
                this.AccountAkumDropDownList.SelectedValue = _msFAGroupSubAcc.AccAkumDepr;
            }
            else
            {
                this.AccountAkumDropDownList.SelectedValue = "null";
            }

            if (_msFAGroupSubAcc.AccDepr != null && _msFAGroupSubAcc.AccDepr != "")
            {
                this.AccountDPDropDownList.SelectedValue = _msFAGroupSubAcc.AccDepr;
            }
            else
            {
                this.AccountDPDropDownList.SelectedValue = "null";
            }

            if (_msFAGroupSubAcc.AccFA != null && _msFAGroupSubAcc.AccFA != "")
            {
                this.AccountAssetDropDownList.SelectedValue = _msFAGroupSubAcc.AccFA;
            }
            else
            {
                this.AccountAssetDropDownList.SelectedValue = "null";
            }

            if (_msFAGroupSubAcc.AccSales != null && _msFAGroupSubAcc.AccSales != "")
            {
                this.AccountSalesDropDownList.SelectedValue = _msFAGroupSubAcc.AccSales;
            }
            else
            {
                this.AccountSalesDropDownList.SelectedValue = "null";
            }

            if (_msFAGroupSubAcc.AccTenancy != null && _msFAGroupSubAcc.AccTenancy != "")
            {
                this.AccountTenancyDropDownList.SelectedValue = _msFAGroupSubAcc.AccTenancy;
            }
            else
            {
                this.AccountTenancyDropDownList.SelectedValue = "null";
            }
        }

        public void ShowCurrency()
        {
            this.CurrencyDropDownList.DataTextField = "CurrCode";
            this.CurrencyDropDownList.DataValueField = "CurrCode";
            this.CurrencyDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrencyDropDownList.DataBind();
        }

        public void ShowAccountAkum()
        {
            this.AccountAkumDropDownList.DataTextField = "AccountName";
            this.AccountAkumDropDownList.DataValueField = "Account";
            this.AccountAkumDropDownList.DataSource = this._accountBL.GetListForDDL(this.CurrencyDropDownList.SelectedValue);
            this.AccountAkumDropDownList.DataBind();
            this.AccountAkumDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowAccountAsset()
        {
            this.AccountAssetDropDownList.DataTextField = "AccountName";
            this.AccountAssetDropDownList.DataValueField = "Account";
            this.AccountAssetDropDownList.DataSource = this._accountBL.GetListForDDL(this.CurrencyDropDownList.SelectedValue);
            this.AccountAssetDropDownList.DataBind();
            this.AccountAssetDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowAccountDP()
        {
            this.AccountDPDropDownList.DataTextField = "AccountName";
            this.AccountDPDropDownList.DataValueField = "Account";
            this.AccountDPDropDownList.DataSource = this._accountBL.GetListForDDL(this.CurrencyDropDownList.SelectedValue);
            this.AccountDPDropDownList.DataBind();
            this.AccountDPDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowAccountSales()
        {
            this.AccountSalesDropDownList.DataTextField = "AccountName";
            this.AccountSalesDropDownList.DataValueField = "Account";
            this.AccountSalesDropDownList.DataSource = this._accountBL.GetListForDDL(this.CurrencyDropDownList.SelectedValue);
            this.AccountSalesDropDownList.DataBind();
            this.AccountSalesDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowAccountTanancy()
        {
            this.AccountTenancyDropDownList.DataTextField = "AccountName";
            this.AccountTenancyDropDownList.DataValueField = "Account";
            this.AccountTenancyDropDownList.DataSource = this._accountBL.GetListForDDL(this.CurrencyDropDownList.SelectedValue);
            this.AccountTenancyDropDownList.DataBind();
            this.AccountTenancyDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsFAGroupSubAcc _msFAGroupSubAcc = this._fixedAssetBL.GetSingleFAGroupSubAcc(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._currCodeKey), ApplicationConfig.EncryptionKey));

            _msFAGroupSubAcc.AccFA = this.AccountAssetDropDownList.SelectedValue;
            _msFAGroupSubAcc.AccAkumDepr = this.AccountAkumDropDownList.SelectedValue;
            _msFAGroupSubAcc.AccDepr = this.AccountDPDropDownList.SelectedValue;
            _msFAGroupSubAcc.AccSales = this.AccountSalesDropDownList.SelectedValue;
            _msFAGroupSubAcc.AccTenancy = this.AccountTenancyDropDownList.SelectedValue;

            _msFAGroupSubAcc.UserId = HttpContext.Current.User.Identity.Name;
            _msFAGroupSubAcc.UserDate = DateTime.Now;

            bool _result = this._fixedAssetBL.EditFAGroupSubAcc(_msFAGroupSubAcc);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
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