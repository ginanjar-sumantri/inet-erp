using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using Microsoft.Reporting.WebForms;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using System.Web.UI.WebControls;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.Report
{
    public partial class CustomerBillingAccountReport : ReportBillingBase
    {
        private ReportBillingBL _reportBillingBL = new ReportBillingBL();
        private RadiusBL _radiusBL = new RadiusBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _reportPath0 = "Report/RadiusReportPerPeriod.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDRadiusReport, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer.Attributes.Add("style", "margin-bottom: 30px;");

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = "Radius Report Per Period";

                this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/preview.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetButtonPermission();

                this.ShowCustomerRadius();
                this.ClearLabel();
                this.SetAttribute();

                this.ClearData();
            }
        }

        private void SetButtonPermission()
        {
            this._permView = this._permBL.PermissionValidation1(this._menuIDRadiusReport, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.PreviewButton.Visible = false;
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ShowCustomerRadius()
        {
            this.RadiusDropDownList.Items.Clear();
            this.RadiusDropDownList.DataTextField = "RadiusName";
            this.RadiusDropDownList.DataValueField = "RadiusCode";
            this.RadiusDropDownList.DataSource = this._radiusBL.GetListRadiusForDDL();
            this.RadiusDropDownList.DataBind();
            this.RadiusDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SetAttribute()
        {
            this.PeriodTextBox.Attributes.Add("OnKeyUp", "formatangka(" + this.PeriodTextBox.ClientID + ");");
            this.PeriodTextBox.Attributes.Add("OnBlur", " ValidatePeriod(" + this.PeriodTextBox.ClientID + ");");

            this.YearTextBox.Attributes.Add("OnKeyUp", "formatangka(" + this.YearTextBox.ClientID + ");");
            this.YearTextBox.Attributes.Add("OnBlur", " ValidateYear(" + this.YearTextBox.ClientID + ");");
        }

        protected void ClearData()
        {
            DateTime _now = DateTime.Now;

            this.PeriodTextBox.Text = _now.Month.ToString();
            this.YearTextBox.Text = _now.Year.ToString();
            this.RadiusDropDownList.SelectedValue = "null";
        }

        protected void PreviewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.Panel1.Visible = false;
            this.Panel2.Visible = true;

            ReportDataSource _reportDataSource1 = this._reportBillingBL.RadiusReportPerPeriod(this.PeriodTextBox.Text, this.YearTextBox.Text, this.RadiusDropDownList.SelectedValue);

            this.ReportViewer.LocalReport.DataSources.Clear();
            this.ReportViewer.LocalReport.DataSources.Add(_reportDataSource1);
            this.ReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
            this.ReportViewer.DataBind();

            String _custCode = new RadiusBL().GetSingleRadius(this.RadiusDropDownList.SelectedValue).CustCode;
            String _customerName = new CustomerBL().GetNameByCode(_custCode);
            
            ReportParameter[] _reportParam = new ReportParameter[2];
            _reportParam[0] = new ReportParameter("YearPeriod", ((this.YearTextBox.Text + this.PeriodTextBox.Text) == "" ? null : (this.YearTextBox.Text + this.PeriodTextBox.Text)), true);
            _reportParam[1] = new ReportParameter("CustomerRadius", _customerName, false);
            
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
