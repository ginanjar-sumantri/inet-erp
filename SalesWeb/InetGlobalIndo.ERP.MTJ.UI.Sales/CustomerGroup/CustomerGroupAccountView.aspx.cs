using System;
using System.Web;
using System.Web.UI;
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
    public partial class CustomerGroupAccountView : CustomerGroupBase
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

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.SetButtonPermission();

                this.ShowData();
            }
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        public void ShowData()
        {
            MsCustGroupAcc _msCustGroupAcc = this._customerBL.GetSingleCustGroupAcc(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._currCodeKey), ApplicationConfig.EncryptionKey));

            this.CustGroupCodeTextBox.Text = _msCustGroupAcc.CustGroup;
            if (_msCustGroupAcc.CurrCode != null && _msCustGroupAcc.CurrCode != "")
            {
                this.CurrencyTextBox.Text = _msCustGroupAcc.CurrCode;
            }

            if (_msCustGroupAcc.AccAR != null && _msCustGroupAcc.AccAR != "")
            {
                this.AccountARTextBox.Text = _msCustGroupAcc.AccAR;
                this.AccountARNameTextBox.Text = _accountBL.GetAccountNameByCode(_msCustGroupAcc.AccAR);
            }
            else
            {
                this.AccountARTextBox.Text = "";
                this.AccountARNameTextBox.Text = "";
            }

            if (_msCustGroupAcc.AccDisc != null && _msCustGroupAcc.AccDisc != "")
            {
                this.AccountDiscTextBox.Text = _msCustGroupAcc.AccDisc;
                this.AccountDiscNameTextBox.Text = _accountBL.GetAccountNameByCode(_msCustGroupAcc.AccDisc);
            }
            else
            {
                this.AccountDiscTextBox.Text = "";
                this.AccountDiscNameTextBox.Text = "";
            }

            if (_msCustGroupAcc.AccCreditAR != null && _msCustGroupAcc.AccCreditAR != "")
            {
                this.AccountCreditARTextBox.Text = _msCustGroupAcc.AccCreditAR;
                this.AccountCreditARNameTextBox.Text = _accountBL.GetAccountNameByCode(_msCustGroupAcc.AccCreditAR);
            }
            else
            {
                this.AccountCreditARTextBox.Text = "";
                this.AccountCreditARNameTextBox.Text = "";
            }

            if (_msCustGroupAcc.AccDP != null && _msCustGroupAcc.AccDP != "")
            {
                this.AccountDPTextBox.Text = _msCustGroupAcc.AccDP;
                this.AccountDPNameTextBox.Text = _accountBL.GetAccountNameByCode(_msCustGroupAcc.AccDP);
            }
            else
            {
                this.AccountDPTextBox.Text = "";
                this.AccountDPNameTextBox.Text = "";
            }

            if (_msCustGroupAcc.AccPPn != null && _msCustGroupAcc.AccPPn != "")
            {
                this.AccountPPNTextBox.Text = _msCustGroupAcc.AccPPn;
                this.AccountPPNNameTextBox.Text = _accountBL.GetAccountNameByCode(_msCustGroupAcc.AccPPn);
            }
            else
            {
                this.AccountPPNTextBox.Text = "";
                this.AccountPPNNameTextBox.Text = "";
            }

            if (_msCustGroupAcc.AccOther != null && _msCustGroupAcc.AccOther != "")
            {
                this.AccountOtherTextBox.Text = _msCustGroupAcc.AccOther;
                this.AccountOtherNameTextBox.Text = _accountBL.GetAccountNameByCode(_msCustGroupAcc.AccOther);
            }
            else
            {
                this.AccountOtherTextBox.Text = "";
                this.AccountOtherNameTextBox.Text = "";
            }
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + _currCodeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._currCodeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}