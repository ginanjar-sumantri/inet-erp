using System;
using System.Web;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.Report
{
    public partial class CashFlowForecast : ReportBase
    {
        private ReportFinanceBL _reportBL = new ReportFinanceBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _reportPath0 = "Report/CashFlowForecastReport.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDCashFlowForecast, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer.Attributes.Add("style", "margin-bottom: 30px;");

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = "Cash Flow Forecast Report";

                this.ShowReport();
            }
        }


        private void ShowReport()
        {
            this.ReportViewer.Visible = true;

            ReportDataSource _reportDataSource1 = this._reportBL.CashFlowForecast(DateTime.Now);

            this.ReportViewer.LocalReport.DataSources.Clear();
            this.ReportViewer.LocalReport.DataSources.Add(_reportDataSource1);

            this.ReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;

            this.ReportViewer.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[2];
            _reportParam[0] = new ReportParameter("Date", DateTime.Now.ToString(), true);
            _reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);

            this.ReportViewer.LocalReport.SetParameters(_reportParam);
            this.ReportViewer.LocalReport.Refresh();
        }
    }
}