using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DataMapping;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.Report
{
    public partial class CashflowActualCash : ReportBase
    {
        private ReportBL _report = new ReportBL();
        private PermissionBL _permBL = new PermissionBL();

        private int _isSearch = 0;

        private string _reportPath0 = "Report/CashFlowStandard.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDGLSummary, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer1.Attributes.Add("style", "margin-bottom: 30px;");


            if (!Page.IsPostBack == true)
            {
                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = "Actual Cashflow";

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.MenuPanel.Visible = true;
                this.ReportViewer1.Visible = false;

                this.SetAttribut();
                this.ClearData();

            }
        }

        public void ClearData()
        {
            this.YearTextBox.Text = DateTime.Now.Year.ToString();
            this.YearEndTextBox.Text = DateTime.Now.Year.ToString();
            this.PeriodTextBox.Text = "";
            this.PeriodEndTextBox.Text = "";
        }

        public void SetAttribut()
        {
            this.YearTextBox.Attributes.Add("OnKeyDown", "return Numeric();");
            this.YearEndTextBox.Attributes.Add("OnKeyDown", "return Numeric();");
            this.PeriodTextBox.Attributes.Add("OnKeyDown", "return Numeric();");
            this.PeriodEndTextBox.Attributes.Add("OnKeyDown", "return Numeric();");
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {

            string _period, _periodEnd;

            if (this.PeriodTextBox.Text.Length < 2)
            {
                _period = "0" + PeriodTextBox.Text;

            }
            else
            {
                _period = PeriodTextBox.Text;
            }

            if (this.PeriodEndTextBox.Text.Length < 2)
            {
                _periodEnd = "0" + PeriodEndTextBox.Text;

            }
            else
            {
                _periodEnd = PeriodEndTextBox.Text;
            }


            this.MenuPanel.Visible = false;
            this.ReportViewer1.Visible = true;

            ReportDataSource _reportDataSource1 = null;

            _reportDataSource1 = this._report.CashFlowActualCashRange(this.YearTextBox.Text, _period.Trim(), this.YearEndTextBox.Text, _periodEnd.Trim());

            this.ReportViewer1.LocalReport.EnableExternalImages = true;
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

            this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;

            this.ReportViewer1.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[6];
            _reportParam[0] = new ReportParameter("StartYear", this.YearTextBox.Text, true);
            _reportParam[1] = new ReportParameter("EndYear", this.YearEndTextBox.Text, true);
            _reportParam[2] = new ReportParameter("StartMonth", _period.Trim(), true);
            _reportParam[3] = new ReportParameter("EndMonth", _periodEnd.Trim(), true);
            _reportParam[4] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            _reportParam[5] = new ReportParameter("Image", ApplicationConfig.HomeWebAppURL + "images/" + new UserBL().CompanyLogo(HttpContext.Current.User.Identity.Name), false);

            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();

        }

    }
}