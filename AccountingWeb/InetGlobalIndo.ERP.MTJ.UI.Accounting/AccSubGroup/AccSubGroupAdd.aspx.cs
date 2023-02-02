using System;
using System.Web;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.AccSubGroup
{
    public partial class AccSubGroupAdd : AccSubGroupBase
    {
        private AccSubGroupBL _accSubGroupBL = new AccSubGroupBL();
        private AccGroupBL _accGroupBL = new AccGroupBL();
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

                this.ShowDropdownlist();

                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.AccGroupCodeTextBox.Attributes.Add("ReadOnly", "True");
            this.AccSubGroupCodeTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.AccGroupDDL.Attributes.Add("OnChange", "SelectedAccGroup(" + this.AccGroupDDL.ClientID + "," + this.AccGroupCodeTextBox.ClientID + ");");
        }

        protected void ClearData()
        {
            this.ClearLabel();

            this.AccSubGroupCodeTextBox.Text = "";
            this.AccSubGroupNameTextBox.Text = "";
            this.AccGroupDDL.SelectedValue = "null";
        }

        private void ShowDropdownlist()
        {
            var hasil = this._accGroupBL.GetListAccGroupForDDL();

            this.AccGroupDDL.DataSource = hasil;
            this.AccGroupDDL.DataValueField = "AccGroupCode";
            this.AccGroupDDL.DataTextField = "AccGroupName";
            this.AccGroupDDL.DataBind();
            this.AccGroupDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            MsAccSubGroup _aSubGroup = new MsAccSubGroup();

            _aSubGroup.AccSubGroupCode = this.AccGroupCodeTextBox.Text.ToString() + this.AccSubGroupCodeTextBox.Text.ToString();
            _aSubGroup.AccSubGroupName = this.AccSubGroupNameTextBox.Text;
            _aSubGroup.AccGroup = this.AccGroupDDL.SelectedValue;
            _aSubGroup.UserID = HttpContext.Current.User.Identity.Name;
            _aSubGroup.UserDate = DateTime.Now;

            bool _result = this._accSubGroupBL.Add(_aSubGroup);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            this.ClearData();
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(_homePage);
        }
    }
}