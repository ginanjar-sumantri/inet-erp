using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.BudgetEntry
{
    public partial class GLBudgetEdit : GLBudgetBase
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

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.StartDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.StartDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.EndDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.EndDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.ShowOrgUnitDropdownlist();

                this.SetAttribute();
                this.ClearLabel();
                this.ShowData();
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

        private void ShowOrgUnitDropdownlist()
        {
            this.OrgUnitDropDownList.DataSource = _orgUnit.GetListOrganizationUnitForDDL();
            this.OrgUnitDropDownList.DataValueField = "OrgUnit";
            this.OrgUnitDropDownList.DataTextField = "Description";
            this.OrgUnitDropDownList.DataBind();
            this.OrgUnitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowData()
        {
            GLBudget _glBudget = this._glBudgetBL.GetSingleHd(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            this.StartDateTextBox.Text = DateFormMapper.GetValue(_glBudget.StartDate);
            this.EndDateTextBox.Text = DateFormMapper.GetValue(_glBudget.EndDate);
            this.OrgUnitDropDownList.SelectedValue = _glBudget.OrgUnit;
            this.RemarkTextBox.Text = _glBudget.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            GLBudget _glBudget = this._glBudgetBL.GetSingleHd(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            _glBudget.StartDate = new DateTime(DateFormMapper.GetValue(this.StartDateTextBox.Text).Year, DateFormMapper.GetValue(this.StartDateTextBox.Text).Month, DateFormMapper.GetValue(this.StartDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _glBudget.EndDate = new DateTime(DateFormMapper.GetValue(this.EndDateTextBox.Text).Year, DateFormMapper.GetValue(this.EndDateTextBox.Text).Month, DateFormMapper.GetValue(this.EndDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _glBudget.OrgUnit = this.OrgUnitDropDownList.SelectedValue;
            _glBudget.Remark = this.RemarkTextBox.Text;
            _glBudget.EditBy = HttpContext.Current.User.Identity.Name;
            _glBudget.EditDate = DateTime.Now;

            bool _result = this._glBudgetBL.EditGLBudget(_glBudget);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            GLBudget _glBudget = this._glBudgetBL.GetSingleHd(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            _glBudget.StartDate = new DateTime(DateFormMapper.GetValue(this.StartDateTextBox.Text).Year, DateFormMapper.GetValue(this.StartDateTextBox.Text).Month, DateFormMapper.GetValue(this.StartDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _glBudget.EndDate = new DateTime(DateFormMapper.GetValue(this.EndDateTextBox.Text).Year, DateFormMapper.GetValue(this.EndDateTextBox.Text).Month, DateFormMapper.GetValue(this.EndDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _glBudget.OrgUnit = this.OrgUnitDropDownList.SelectedValue;
            _glBudget.Remark = this.RemarkTextBox.Text;
            _glBudget.EditBy = HttpContext.Current.User.Identity.Name;
            _glBudget.EditDate = DateTime.Now;

            bool _result = this._glBudgetBL.EditGLBudget(_glBudget);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }
    }
}