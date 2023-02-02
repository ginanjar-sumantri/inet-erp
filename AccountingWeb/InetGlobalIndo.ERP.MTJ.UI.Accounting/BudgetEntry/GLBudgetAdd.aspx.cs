using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.BudgetEntry
{
    public partial class GLBudgetAdd : GLBudgetBase
    {
        private GLBudgetBL _glBudgetBL = new GLBudgetBL();
        private PermissionBL _permBL = new PermissionBL();
        private OrganizationStructureBL _orgUnit = new OrganizationStructureBL();

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
                this.StartDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.StartDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.EndDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.EndDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowOrgUnitDropdownlist();

                this.SetAttribute();
                this.ClearLabel();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ClearData()
        {
            this.StartDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.EndDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.RemarkTextBox.Text = "";
            this.OrgUnitDropDownList.SelectedValue = "null";
        }

        private void ShowOrgUnitDropdownlist()
        {
            this.OrgUnitDropDownList.DataSource = _orgUnit.GetListOrganizationUnitForDDL();
            this.OrgUnitDropDownList.DataValueField = "OrgUnit";
            this.OrgUnitDropDownList.DataTextField = "Description";
            this.OrgUnitDropDownList.DataBind();
            this.OrgUnitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            GLBudget _glBudget = new GLBudget();

            _glBudget.BudgetCode = Guid.NewGuid();
            _glBudget.StartDate = new DateTime(DateFormMapper.GetValue(this.StartDateTextBox.Text).Year, DateFormMapper.GetValue(this.StartDateTextBox.Text).Month, DateFormMapper.GetValue(this.StartDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _glBudget.EndDate = new DateTime(DateFormMapper.GetValue(this.EndDateTextBox.Text).Year, DateFormMapper.GetValue(this.EndDateTextBox.Text).Month, DateFormMapper.GetValue(this.EndDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _glBudget.OrgUnit = this.OrgUnitDropDownList.SelectedValue;
            _glBudget.Remark = this.RemarkTextBox.Text;
            _glBudget.Status = GLBudgetDataMapper.GetStatus(GLBudgetStatus.OnHold);

            _glBudget.InsertBy = HttpContext.Current.User.Identity.Name;
            _glBudget.InsertDate = DateTime.Now;
            _glBudget.EditBy = HttpContext.Current.User.Identity.Name;
            _glBudget.EditDate = DateTime.Now;

            string _result = this._glBudgetBL.AddGLBudget(_glBudget);

            if (_result != "")
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }
    }
}