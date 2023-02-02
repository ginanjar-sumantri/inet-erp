﻿using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FASubGroup
{
    public partial class FAGroupSubAccView : FASubGroupBase
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
            MsFAGroupSubAcc _msFAGroupSubAcc = this._fixedAssetBL.GetSingleFAGroupSubAcc(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._currCodeKey), ApplicationConfig.EncryptionKey));

            if (_msFAGroupSubAcc.CurrCode != null && _msFAGroupSubAcc.CurrCode != "")
            {
                this.CurrencyTextBox.Text = _msFAGroupSubAcc.CurrCode;

                if (_msFAGroupSubAcc.AccAkumDepr != "null")
                {
                    this.AccAkumTextBox.Text = _msFAGroupSubAcc.AccAkumDepr;
                }
                if (_msFAGroupSubAcc.AccFA != "null")
                {
                    this.AccAssetsTextBox.Text = _msFAGroupSubAcc.AccFA;
                }
                if (_msFAGroupSubAcc.AccDepr != "null")
                {
                    this.AccDepreciationTextBox.Text = _msFAGroupSubAcc.AccDepr;
                }
                if (_msFAGroupSubAcc.AccSales != "null")
                {
                    this.AccSalesTextBox.Text = _msFAGroupSubAcc.AccSales;
                }
                if (_msFAGroupSubAcc.AccTenancy != "null")
                {
                    this.AccTenancyTextBox.Text = _msFAGroupSubAcc.AccTenancy;
                }
            }

            if (_msFAGroupSubAcc.AccAkumDepr != null && _msFAGroupSubAcc.AccAkumDepr != "")
            {
                this.AccountAkumNameTextBox.Text = _accountBL.GetAccountNameByCode(_msFAGroupSubAcc.AccAkumDepr);
            }
            else
            {
                this.AccountAkumNameTextBox.Text = "";
            }

            if (_msFAGroupSubAcc.AccDepr != null && _msFAGroupSubAcc.AccDepr != "")
            {
                this.AccountDPNameTextBox.Text = _accountBL.GetAccountNameByCode(_msFAGroupSubAcc.AccDepr);
            }
            else
            {
                this.AccountDPNameTextBox.Text = "";
            }

            if (_msFAGroupSubAcc.AccFA != null && _msFAGroupSubAcc.AccFA != "")
            {
                this.AccountAssetNameTextBox.Text = _accountBL.GetAccountNameByCode(_msFAGroupSubAcc.AccFA);
            }
            else
            {
                this.AccountAssetNameTextBox.Text = "";
            }

            if (_msFAGroupSubAcc.AccSales != null && _msFAGroupSubAcc.AccSales != "")
            {
                this.AccountSalesNameTextBox.Text = _accountBL.GetAccountNameByCode(_msFAGroupSubAcc.AccSales);
            }
            else
            {
                this.AccountSalesNameTextBox.Text = "";
            }

            if (_msFAGroupSubAcc.AccTenancy != null && _msFAGroupSubAcc.AccTenancy != "")
            {
                this.AccountTenancyNameTextBox.Text = _accountBL.GetAccountNameByCode(_msFAGroupSubAcc.AccTenancy);
            }
            else
            {
                this.AccountTenancyNameTextBox.Text = "";
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