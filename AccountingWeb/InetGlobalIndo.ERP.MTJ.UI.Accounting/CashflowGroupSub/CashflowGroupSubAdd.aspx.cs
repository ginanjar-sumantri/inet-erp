using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.CashflowGroupSub
{
    public partial class CashflowGroupSubAdd : CashflowGroupSubBase
    {
        private CashflowgroupBL _cashFlowGroupBL = new CashflowgroupBL();
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
                this.PageTitleLiteral.Text = this._transPageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ClearLabel();
                this.ClearData();
                this.ShowCFGroup();
                this.SetAttribute();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.CFGroupCodeTextBox.Attributes.Add("ReadOnly", "True");
            this.CodeTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
        
        }

        protected void ClearData()
        {
            this.CodeTextBox.Text = "";
            this.NameTextBox.Text = "";
        }

        private void ShowCFGroup()
        {
            this.CfGroupDropDownList.Items.Clear();
            this.CfGroupDropDownList.DataSource = this._cashFlowGroupBL.GetListForDDL(this.CashflowTypeDDL.SelectedValue);
            this.CfGroupDropDownList.DataValueField = "CashFlowGroupCode";
            this.CfGroupDropDownList.DataTextField = "CashFlowGroupName";
            this.CfGroupDropDownList.DataBind();
            this.CfGroupDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }


        protected void SaveButton_Click(object sender, EventArgs e)
        {
            FINMsCashFlowGroupSub _finMsCfGroupSub = new FINMsCashFlowGroupSub();

            _finMsCfGroupSub.CashFlowGroupSubCode = CFGroupCodeTextBox.Text.Trim() + this.CodeTextBox.Text.Trim();
            _finMsCfGroupSub.CashFlowGroupSubName = this.NameTextBox.Text;
            _finMsCfGroupSub.CashFlowType = this.CashflowTypeDDL.SelectedValue;
            _finMsCfGroupSub.CashFlowGroupCode = this.CFGroupCodeTextBox.Text;
            _finMsCfGroupSub.Operator = Convert.ToChar(this.OperatorDropDownList.SelectedValue);
            _finMsCfGroupSub.TypeCode = this.SumTypeDropDownList.SelectedValue;

            _finMsCfGroupSub.InsertBy = HttpContext.Current.User.Identity.Name;
            _finMsCfGroupSub.InsertDate = DateTime.Now;
            _finMsCfGroupSub.EditBy = HttpContext.Current.User.Identity.Name;
            _finMsCfGroupSub.EditDate = DateTime.Now;

            bool _result = this._cashFlowGroupBL.AddCFGroupSub(_finMsCfGroupSub);

            if (_result == true)
            {
                Response.Redirect(this._transHomePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(_transHomePage);
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }

        protected void CashflowTypeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowCFGroup();
        }

        protected void CfGroupDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CFGroupCodeTextBox.Text = this.CfGroupDropDownList.SelectedValue;    
        }
}
}