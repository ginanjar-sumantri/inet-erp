using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.Report
{
    public partial class AccountTransactionSummary : ReportBase
    {
        private ReportBL _report = new ReportBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _reportPath1 = "Report/BSAndPL.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDBSAndPL, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer1.Attributes.Add("style", "margin-bottom: 30px;");

            if (!Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = "Account Transaction Summary Report";

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.MenuPanel.Visible = true;
                this.ReportViewer1.Visible = false;

                this.YearTextBox.Attributes.Add("OnKeyDown", "return  NumericWithEnter();");
                this.PeriodTextBox.Attributes.Add("OnKeyDown", "return  NumericWithEnter();");

                this.ClearLabel();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearData()
        {
            this.YearTextBox.Text = DateTime.Now.Year.ToString();
            this.PeriodTextBox.Text = DateTime.Now.Month.ToString();
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = false;
            this.ReportViewer1.Visible = true;

            ReportDataSource _reportDataSource1 = this._report.BSAndPL(Convert.ToInt32(this.YearTextBox.Text), Convert.ToInt32(this.PeriodTextBox.Text));

            this.ReportViewer1.LocalReport.EnableExternalImages = true;
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
            this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
            this.ReportViewer1.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[5];
            _reportParam[0] = new ReportParameter("Year", this.YearTextBox.Text, true);
            _reportParam[1] = new ReportParameter("Period", this.PeriodTextBox.Text, true);
            _reportParam[2] = new ReportParameter("Type", "0", false);
            _reportParam[3] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            _reportParam[4] = new ReportParameter("Image", ApplicationConfig.HomeWebAppURL + "images/" + new UserBL().CompanyLogo(HttpContext.Current.User.Identity.Name), false);

            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}