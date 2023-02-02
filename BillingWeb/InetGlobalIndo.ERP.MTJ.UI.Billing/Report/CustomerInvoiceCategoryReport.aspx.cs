using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.Report
{
    public partial class CustomerInvoiceCategoryReport : ReportBillingBase
    {
        private ReportBillingBL _reportBillingBL = new ReportBillingBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _reportPath0 = "Report/CustomerInvoiceCategoryReport.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDCustomerInvoiceCategory, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer.Attributes.Add("style", "margin-bottom: 30px;");

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = "Customer Invoice Category Report";

                this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/preview.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetButtonPermission();

                this.ClearLabel();
                this.SetAttribute();

                this.ClearData();
            }
        }

        private void SetButtonPermission()
        {
            this._permView = this._permBL.PermissionValidation1(this._menuIDCustomerInvoiceCategory, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.PreviewButton.Visible = false;
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.PeriodTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.PeriodTextBox.Attributes.Add("OnBlur", " ValidatePeriod(" + this.PeriodTextBox.ClientID + ");");

            this.YearTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.YearTextBox.Attributes.Add("OnBlur", " ValidateYear(" + this.YearTextBox.ClientID + ");");

            this.PeriodEndTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.PeriodEndTextBox.Attributes.Add("OnBlur", " ValidatePeriod(" + this.PeriodTextBox.ClientID + ");");

            this.YearEndTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.YearEndTextBox.Attributes.Add("OnBlur", " ValidateYear(" + this.YearTextBox.ClientID + ");");
        }

        protected void ClearData()
        {
            DateTime _now = DateTime.Now;

            this.PeriodTextBox.Text = _now.Month.ToString();
            this.YearTextBox.Text = _now.Year.ToString();

            this.PeriodEndTextBox.Text = _now.Month.ToString();
            this.YearEndTextBox.Text = _now.Year.ToString();
        }

        protected void PreviewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.Panel1.Visible = false;
            this.Panel2.Visible = true;

            ReportDataSource _reportDataSource1 = this._reportBillingBL.CustomerInvoiceCategory(this.PeriodTextBox.Text, this.YearTextBox.Text, this.PeriodEndTextBox.Text, this.YearEndTextBox.Text);

            this.ReportViewer.LocalReport.DataSources.Clear();
            this.ReportViewer.LocalReport.DataSources.Add(_reportDataSource1);
            this.ReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;

            this.ReportViewer.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[3];
            _reportParam[0] = new ReportParameter("StartYear", (this.YearTextBox.Text == "") ? null : this.YearTextBox.Text, true);
            _reportParam[1] = new ReportParameter("StartPeriod", (this.PeriodTextBox.Text == "") ? null : this.PeriodTextBox.Text, true);
            _reportParam[0] = new ReportParameter("EndYear", (this.YearEndTextBox.Text == "") ? null : this.YearEndTextBox.Text, true);
            _reportParam[1] = new ReportParameter("EndPeriod", (this.PeriodEndTextBox.Text == "") ? null : this.PeriodEndTextBox.Text, true);
            _reportParam[2] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            this.ReportViewer.LocalReport.SetParameters(_reportParam);
            this.ReportViewer.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }
    }
}
