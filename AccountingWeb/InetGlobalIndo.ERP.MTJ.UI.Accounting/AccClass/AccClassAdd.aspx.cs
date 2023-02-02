using System;
using System.Web;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.AccClass
{
    public partial class AccClassAdd : AccClassBase
    {
        private AccClassBL _accClassBL = new AccClassBL();
        private AccSubGroupBL _accSubGroupBL = new AccSubGroupBL();
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

                this.ShowAccSubGroupDropDownList();

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
            this.AccountSubGroupCodeTextBox.Attributes.Add("ReadOnly", "True");
            this.AccClassCodeTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.AccSubGroupDDL.Attributes.Add("OnChange", "Selected(" + this.AccSubGroupDDL.ClientID + "," + this.AccountSubGroupCodeTextBox.ClientID + ");");
        }

        protected void ClearData()
        {
            this.ClearLabel();

            this.AccountSubGroupCodeTextBox.Text = "";
            this.AccClassCodeTextBox.Text = "";
            this.AccClassNameTextBox.Text = "";
            this.AccSubGroupDDL.SelectedValue = "null";
        }

        private void ShowAccSubGroupDropDownList()
        {
            this.AccSubGroupDDL.Items.Clear();
            this.AccSubGroupDDL.DataSource = this._accSubGroupBL.GetListForDDL();
            this.AccSubGroupDDL.DataValueField = "AccSubGroupCode";
            this.AccSubGroupDDL.DataTextField = "AccsubGroupName";
            this.AccSubGroupDDL.DataBind();
            this.AccSubGroupDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            MsAccClass _msAccCLass = new MsAccClass();

            _msAccCLass.AccClassCode = this.AccountSubGroupCodeTextBox.Text.ToString() + this.AccClassCodeTextBox.Text.ToString();
            _msAccCLass.AccClassName = this.AccClassNameTextBox.Text;
            _msAccCLass.AccSubGroup = this.AccSubGroupDDL.SelectedValue;
            _msAccCLass.UserID = HttpContext.Current.User.Identity.Name;
            _msAccCLass.UserDate = DateTime.Now;

            bool _result = this._accClassBL.AddAccClass(_msAccCLass);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            this.ClearData();
        }
    }
}