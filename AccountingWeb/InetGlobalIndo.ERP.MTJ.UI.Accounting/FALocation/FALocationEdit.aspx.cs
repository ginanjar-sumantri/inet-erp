﻿using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FALocation
{
    public partial class FALocationEdit : FALocationBase
    {
        private FixedAssetsBL _FALocation = new FixedAssetsBL();
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

                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ShowData()
        {
            MsFALocation _msFALocation = this._FALocation.GetSingleFALocation(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._FALocationCodeKey), ApplicationConfig.EncryptionKey));

            this.FALocationCodeTextBox.Text = _msFALocation.FALocationCode;
            this.FALocationNameTextBox.Text = _msFALocation.FALocationName;
            this.FGActiveCheckBox.Checked = Convert.ToBoolean(_msFALocation.FgActive);
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsFALocation _msFALocation = this._FALocation.GetSingleFALocation(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._FALocationCodeKey), ApplicationConfig.EncryptionKey));

            _msFALocation.FALocationCode = this.FALocationCodeTextBox.Text;
            _msFALocation.FALocationName = this.FALocationNameTextBox.Text;
            _msFALocation.FgActive = this.FGActiveCheckBox.Checked;
            _msFALocation.UserID = HttpContext.Current.User.Identity.Name;
            _msFALocation.UserDate = DateTime.Now;

            if (this._FALocation.EditFALocation(_msFALocation) == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit FA Location";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}
