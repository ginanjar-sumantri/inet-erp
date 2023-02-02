using System;
using System.Web;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FACondition
{
    public partial class FAConditionAdd : FAConditionBase
    {
        private FixedAssetsBL _faStatus = new FixedAssetsBL();
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

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ClearLabel();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ClearData()
        {
            this.FAStatusCodeTextBox.Text = "";
            this.FAStatusNameTextBox.Text = "";
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            MsFAStatus _msFaStatus = new MsFAStatus();
            
            _msFaStatus.FAStatusCode = this.FAStatusCodeTextBox.Text;
            _msFaStatus.FAStatusName = this.FAStatusNameTextBox.Text;

            _msFaStatus.UserId = HttpContext.Current.User.Identity.Name;
            _msFaStatus.UserDate = DateTime.Now;

            bool _result = this._faStatus.AddFAStatus(_msFaStatus);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Fixed Assets Status";
            }
        }


        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }

    }
}