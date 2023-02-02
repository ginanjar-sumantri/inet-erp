using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.Report
{
    public partial class PLMonthly : ReportBase
    {
        private ReportBL _report = new ReportBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _reportPath = "";
        private string _reportPath0 = "Report/PLMonthly.rdlc";
        private string _reportPath1 = "Report/PLMonthlyWithout0.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDPLMonthly, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer.Attributes.Add("style", "margin-bottom: 30px;");

            if (!this.Page.IsPostBack == true)
            {
                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = "PL Monthly Report";

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.YearTextBox.Attributes.Add("OnKeyDown", "return NumericWithEnter();");
                this.PeriodTextBox.Attributes.Add("OnKeyDown", "return NumericWithEnter();");

                this.MenuPanel.Visible = true;
                this.ReportViewer.Visible = false;

                this.ClearData();
            }
        }

        public void ClearData()
        {
            DateTime _now = DateTime.Now;

            this.YearTextBox.Text = _now.Year.ToString();
            this.PeriodTextBox.Text = "";
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = false;
            this.ReportViewer.Visible = true;
            this._reportPath = _reportPath0;

            //if (this.FgModeDropDownList.SelectedValue == "0")
            //{
            //    _reportPath = _reportPath0;
            //}
            //else
            //{
            //    _reportPath = _reportPath1;
            //}

            ReportDataSource _reportDataSource1 = this._report.PLMonthly(Convert.ToInt32(this.YearTextBox.Text), Convert.ToInt32(this.PeriodTextBox.Text), 0, Convert.ToInt32(this.FgModeDropDownList.SelectedIndex));

            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DataSources.Clear();
            this.ReportViewer.LocalReport.DataSources.Add(_reportDataSource1);
            this.ReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath;
            this.ReportViewer.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[6];
            _reportParam[0] = new ReportParameter("Year", this.YearTextBox.Text, true);
            _reportParam[1] = new ReportParameter("Period", this.PeriodTextBox.Text, true);
            _reportParam[2] = new ReportParameter("Mode", this.FgModeDropDownList.SelectedValue, false);
            _reportParam[3] = new ReportParameter("GroupBy", "0", false);
            _reportParam[4] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            _reportParam[5] = new ReportParameter("Image", ApplicationConfig.HomeWebAppURL + "images/" + new UserBL().CompanyLogo(HttpContext.Current.User.Identity.Name), false);

            this.ReportViewer.LocalReport.SetParameters(_reportParam);
            this.ReportViewer.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}