using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using System.Web.UI.WebControls;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.CashflowGroup
{
    public partial class CashflowGroupAdd : CashflowGroupBase
    {
        private CashflowgroupBL _cashflowGroupBL = new CashflowgroupBL();
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
                //this.SetAttribute(); 
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribute()
        {
            this.CodeTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
        }

        protected void ClearData()
        {
            this.ClearLabel();

            this.CodeTextBox.Text = "";
            this.NameTextBox.Text = "";
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            this.ClearData();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINMsCashFlowGroup _finMsCashflowGroup = new FINMsCashFlowGroup();

            _finMsCashflowGroup.CashFlowGroupCode = Convert.ToString(this.CodeTextBox.Text);
            _finMsCashflowGroup.CashFlowGroupName = this.NameTextBox.Text;
            _finMsCashflowGroup.CashFlowType = this.CashflowTypeDDL.SelectedValue;

            bool _result = this._cashflowGroupBL.Add(_finMsCashflowGroup);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Cashflow Group";
            }
        }

    }
}