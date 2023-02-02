using System;
using System.Web;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.AccGroup
{
    public partial class AccGroupAdd : AccGroupBase
    {
        private AccGroupBL _accGroupBL = new AccGroupBL();
        private AccTypeBL _accTypeBL = new AccTypeBL();
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

                this.ShowAccTypeDropdownlist();

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
            this.AccTypeCodeTextBox.Attributes.Add("ReadOnly", "True");
            this.CodeTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.AccTypeDDL.Attributes.Add("OnChange", "SelectedAccType(" + this.AccTypeDDL.ClientID + "," + this.AccTypeCodeTextBox.ClientID + ");");
        }

        protected void ClearData()
        {
            this.ClearLabel();

            this.CodeTextBox.Text = "";
            this.NameTextBox.Text = "";
            this.AccTypeCodeTextBox.Text = "";
            this.AccTypeDDL.SelectedValue = "null";
        }

        private void ShowAccTypeDropdownlist()
        {
            this.AccTypeDDL.Items.Clear();
            this.AccTypeDDL.DataSource = this._accTypeBL.GetList();
            this.AccTypeDDL.DataValueField = "AccTypeCode";
            this.AccTypeDDL.DataTextField = "AccTypeName";
            this.AccTypeDDL.DataBind();
            this.AccTypeDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            MsAccGroup _aGroup = new MsAccGroup();

            _aGroup.AccGroupCode = this.AccTypeCodeTextBox.Text.ToString() + this.CodeTextBox.Text.ToString();
            _aGroup.AccGroupName = this.NameTextBox.Text;
            _aGroup.AccType = Convert.ToChar(this.AccTypeDDL.SelectedValue);
            _aGroup.UserID = HttpContext.Current.User.Identity.Name;
            _aGroup.UserDate = DateTime.Now;

            bool _result = this._accGroupBL.AddAccGroup(_aGroup);

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