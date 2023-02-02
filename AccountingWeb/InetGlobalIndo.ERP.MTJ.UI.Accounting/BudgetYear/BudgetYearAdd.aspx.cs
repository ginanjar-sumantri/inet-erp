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

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.BudgetYear
{
    public partial class BudgetYearAdd : GLBudgetYearBase
    {
        private GLBudgetBL _glBudgetBL = new GLBudgetBL();
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

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
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
            this.YearTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.YearTextBox.ClientID + "," + this.YearTextBox.ClientID + ",500" + ");");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ClearData()
        {
            this.YearTextBox.Text = "";
            this.RemarkTextBox.Text = "";
        }

        protected void CheckValidData()
        {
            this.ClearLabel();
            GLBudgetYearHd _glBudgetYearHd = this._glBudgetBL.GetSingleGLBudgetYearHd(Convert.ToInt32(this.YearTextBox.Text), 1);
            if (_glBudgetYearHd != null)
                this.WarningLabel.Text = "Data with Year : " + this.YearTextBox.Text + " Already Exist.";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                GLBudgetYearHd _glBudgetYearHd = new GLBudgetYearHd();

                _glBudgetYearHd.Year = Convert.ToInt32(this.YearTextBox.Text);
                _glBudgetYearHd.Revisi = 1;
                _glBudgetYearHd.Status = GLBudgetDataMapper.GetStatus(GLBudgetStatus.OnHold) == 0 ? '0' : Convert.ToChar(GLBudgetDataMapper.GetStatus(GLBudgetStatus.OnHold));
                _glBudgetYearHd.Remark = this.RemarkTextBox.Text;

                _glBudgetYearHd.InsertBy = HttpContext.Current.User.Identity.Name;
                _glBudgetYearHd.InsertDate = DateTime.Now;
                _glBudgetYearHd.EditBy = HttpContext.Current.User.Identity.Name;
                _glBudgetYearHd.EditDate = DateTime.Now;
                _glBudgetYearHd.FgActive = 'Y';

                string _result = this._glBudgetBL.AddGLBudgetYearHd(_glBudgetYearHd);

                if (_result != "")
                {
                    Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
                }
                else
                {
                    this.WarningLabel.Text = "You Failed Add Data";
                }
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