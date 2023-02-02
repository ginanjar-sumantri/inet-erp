using System;
using System.Web;
using System.Web.UI;
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
    public partial class SupplierGroupAccountView : SupplierGroupBase
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

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

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
            MsSuppGroupAcc _msSuppGroupAcc = this._supplierBL.GetSingleSuppGroupAcc(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._currCode), ApplicationConfig.EncryptionKey));
            this.CurrencyTextBox.Text = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._currCode), ApplicationConfig.EncryptionKey);

            if (_msSuppGroupAcc.AccAP != null || _msSuppGroupAcc.AccAP != "")
            {
                this.AccountAPTextBox.Text = _msSuppGroupAcc.AccAP;
                this.AccountAPNameTextBox.Text = _accountBL.GetAccountNameByCode(_msSuppGroupAcc.AccAP, this.CurrencyTextBox.Text);
            }
            else
            {
                this.AccountAPTextBox.Text = "";
                this.AccountAPNameTextBox.Text = "";
            }

            if (_msSuppGroupAcc.AccAPPending != null || _msSuppGroupAcc.AccAPPending != "")
            {
                this.AccountAPTransitTextBox.Text = _msSuppGroupAcc.AccAPPending;
                this.AccountAPTransitNameTextBox.Text = _accountBL.GetAccountNameByCode(_msSuppGroupAcc.AccAPPending);
            }
            else
            {
                this.AccountAPTransitTextBox.Text = "";
                this.AccountAPTransitNameTextBox.Text = "";
            }

            if (_msSuppGroupAcc.AccDebitAP != null && _msSuppGroupAcc.AccDebitAP != "")
            {
                this.AccountDebitAPTextBox.Text = _msSuppGroupAcc.AccDebitAP;
                this.AccountDebitAPNameTextBox.Text = _accountBL.GetAccountNameByCode(_msSuppGroupAcc.AccDebitAP);
            }
            else
            {
                this.AccountDebitAPTextBox.Text = "";
                this.AccountDebitAPNameTextBox.Text = "";
            }

            if (_msSuppGroupAcc.AccDP != null && _msSuppGroupAcc.AccDP != "")
            {
                this.AccountDPTextBox.Text = _msSuppGroupAcc.AccDP;
                this.AccountDPNameTextBox.Text = _accountBL.GetAccountNameByCode(_msSuppGroupAcc.AccDP);
            }
            else
            {
                this.AccountDPTextBox.Text = "";
                this.AccountDPNameTextBox.Text = "";
            }

            if (_msSuppGroupAcc.AccVariantPO != null && _msSuppGroupAcc.AccVariantPO != "")
            {
                this.AccountVariantPOTextBox.Text = _msSuppGroupAcc.AccVariantPO;
                this.AccountVariantPONameTextBox.Text = _accountBL.GetAccountNameByCode(_msSuppGroupAcc.AccVariantPO);
            }
            else
            {
                this.AccountVariantPOTextBox.Text = "";
                this.AccountVariantPONameTextBox.Text = "";
            }

            if (_msSuppGroupAcc.AccPPn != null && _msSuppGroupAcc.AccPPn != "")
            {
                this.AccountPPnTextBox.Text = _msSuppGroupAcc.AccPPn;
                this.AccountPPnNameTextBox.Text = _accountBL.GetAccountNameByCode(_msSuppGroupAcc.AccPPn);
            }
            else
            {
                this.AccountPPnTextBox.Text = "";
                this.AccountPPnNameTextBox.Text = "";
            }

            if (_msSuppGroupAcc.AccPPh != null && _msSuppGroupAcc.AccPPh != "")
            {
                this.AccountPPhTextBox.Text = _msSuppGroupAcc.AccPPh;
                this.AccountPPhNameTextBox.Text = _accountBL.GetAccountNameByCode(_msSuppGroupAcc.AccPPh);
            }
            else
            {
                this.AccountPPhTextBox.Text = "";
                this.AccountPPhNameTextBox.Text = "";
            }

            if (_msSuppGroupAcc.AccOther != null && _msSuppGroupAcc.AccOther != "")
            {
                this.AccountOtherPurchaseTextBox.Text = _msSuppGroupAcc.AccOther;
                this.AccountOtherPurchaseNameTextBox.Text = _accountBL.GetAccountNameByCode(_msSuppGroupAcc.AccOther);
            }
            else
            {
                this.AccountOtherPurchaseTextBox.Text = "";
                this.AccountOtherPurchaseNameTextBox.Text = "";
            }

            if (_msSuppGroupAcc.AccDisc != null && _msSuppGroupAcc.AccDisc != "")
            {
                this.AccountDiscPurchaseTextBox.Text = _msSuppGroupAcc.AccDisc;
                this.AccountDiscPurchaseNameTextBox.Text = _accountBL.GetAccountNameByCode(_msSuppGroupAcc.AccDisc);
            }
            else
            {
                this.AccountDiscPurchaseTextBox.Text = "";
                this.AccountDiscPurchaseNameTextBox.Text = "";
            }

            if (_msSuppGroupAcc.AccDuty != null && _msSuppGroupAcc.AccDuty != "")
            {
                this.AccountDutyTransitTextBox.Text = _msSuppGroupAcc.AccDuty;
                this.AccountDutyTransitNameTextBox.Text = _accountBL.GetAccountNameByCode(_msSuppGroupAcc.AccDuty);
            }
            else
            {
                this.AccountDutyTransitTextBox.Text = "";
                this.AccountDutyTransitNameTextBox.Text = "";
            }

            if (_msSuppGroupAcc.AccHandling != null && _msSuppGroupAcc.AccHandling != "")
            {
                this.AccountHandlingTransitTextBox.Text = _msSuppGroupAcc.AccHandling;
                this.AccountHandlingTransitNameTextBox.Text = _accountBL.GetAccountNameByCode(_msSuppGroupAcc.AccHandling);
            }
            else
            {
                this.AccountHandlingTransitTextBox.Text = "";
                this.AccountHandlingTransitNameTextBox.Text = "";
            }
            this.FgActiveCheckBox.Checked = (_msSuppGroupAcc.FgActive == 'Y') ? true : false;
            this.RemarkTextBox.Text = _msSuppGroupAcc.Remark;
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._currCode + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._currCode)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}