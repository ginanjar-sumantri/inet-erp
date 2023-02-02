using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.Report
{
    public partial class GLSubledger : ReportBase
    {
        private ReportBL _report = new ReportBL();
        private SubledBL _subled = new SubledBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _reportPath1 = "Report/GLSubLedger.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDGLSubledger, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

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
                this.PageTitleLiteral.Text = "GL Subledger Report";

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.MenuPanel.Visible = true;
                this.ReportViewer1.Visible = false;
                SetAttribut();
                this.ShowFGSubledDDL();
                this.ClearData();
            }
        }

        public void ShowFGSubledDDL()
        {
            var hasil = this._subled.GetList();

            this.FGSubledDDL.DataSource = hasil;
            this.FGSubledDDL.DataValueField = "SubledCode";
            this.FGSubledDDL.DataTextField = "SubledName";
            this.FGSubledDDL.DataBind();
            this.FGSubledDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ClearData()
        {
            this.YearTextBox.Text = DateTime.Now.Year.ToString();
            this.YearEndTextBox.Text = DateTime.Now.Year.ToString();
            this.PeriodTextBox.Text = "";
            this.PeriodEndTextBox.Text = "";
            this.FGSubledDDL.SelectedValue = "null";
            this.OrderByDDL.SelectedValue = "0";
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
            this.MenuPanel.Visible = false;
            this.ReportViewer1.Visible = true;

            ReportDataSource _reportDataSource1 = this._report.GLSubled(Convert.ToInt32(this.YearTextBox.Text), Convert.ToInt32(this.PeriodTextBox.Text), Convert.ToInt32(this.YearEndTextBox.Text), Convert.ToInt32(this.PeriodEndTextBox.Text), this.FGSubledDDL.SelectedValue, Convert.ToInt32(this.OrderByDDL.SelectedValue));

            this.ReportViewer1.LocalReport.EnableExternalImages = true;
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
            this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
            this.ReportViewer1.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[10];
            _reportParam[0] = new ReportParameter("StartYear", this.YearTextBox.Text, true);
            _reportParam[1] = new ReportParameter("StartPeriod", this.PeriodTextBox.Text, true);
            _reportParam[2] = new ReportParameter("EndYear", this.YearEndTextBox.Text, true);
            _reportParam[3] = new ReportParameter("EndPeriod", this.PeriodEndTextBox.Text, true);
            _reportParam[4] = new ReportParameter("Str1", "", false);
            _reportParam[5] = new ReportParameter("FgSubLed", this.FGSubledDDL.SelectedValue, true);
            _reportParam[6] = new ReportParameter("SubLed", "", false);
            _reportParam[7] = new ReportParameter("OrderBy", this.OrderByDDL.SelectedValue, true);
            _reportParam[8] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            _reportParam[9] = new ReportParameter("Image", ApplicationConfig.HomeWebAppURL + "images/" + new UserBL().CompanyLogo(HttpContext.Current.User.Identity.Name), false);

            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}