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
    public partial class BSheetDetail : ReportBase
    {
        private ReportBL _report = new ReportBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _reportPath = "";
        private string _reportPath1 = "Report/BSheetSummaryStandard.rdlc";
        private string _reportPath2 = "Report/BSheetSummaryWithout0T.rdlc";
        private string _reportPath3 = "Report/BSheetDetailStandard.rdlc";
        private string _reportPath4 = "Report/BSheetDetailWithout0T.rdlc";
        private string _reportPath5 = "Report/BSheetSummaryT.rdlc";
        private string _reportPath6 = "Report/BSheetSummaryWithout0Standard.rdlc";
        private string _reportPath7 = "Report/BSheetDetailT.rdlc";
        private string _reportPath8 = "Report/BSheetDetailWithout0Standard.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDBSheet, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer1.Attributes.Add("style", "margin-bottom: 30px;");

            if (!Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = "Balance Sheet Report";

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
            this.PeriodTextBox.Text = "";
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = false;
            this.ReportViewer1.Visible = true;

            if (this.FormDropDownList.SelectedValue == "0")  //TForm
            {
                if (this.TypeRadioButtonList.SelectedValue == "0")  //Summary
                {
                    if (this.FgModeDropDownList.SelectedValue == "0") // With 0
                    {
                        _reportPath = _reportPath5;
                    }
                    else  // Without 0
                    {
                        _reportPath = _reportPath2;
                    }
                }
                else //Detail
                {
                    if (this.FgModeDropDownList.SelectedValue == "0") // With 0
                    {
                        _reportPath = _reportPath7;
                    }
                    else  // Without 0
                    {
                        _reportPath = _reportPath4;
                    }
                }
            }
            else //Standard
            {
                if (this.TypeRadioButtonList.SelectedValue == "0")  //Summary
                {
                    if (this.FgModeDropDownList.SelectedValue == "0") // With 0
                    {
                        _reportPath = _reportPath1;
                    }
                    else  // Without 0
                    {
                        _reportPath = _reportPath6;
                    }
                }
                else //Detail
                {
                    if (this.FgModeDropDownList.SelectedValue == "0") // With 0
                    {
                        _reportPath = _reportPath3;
                    }
                    else  // Without 0
                    {
                        _reportPath = _reportPath8;
                    }
                }
            }

            ReportDataSource _reportDataSource1 = this._report.BSheet(Convert.ToInt32(this.YearTextBox.Text), Convert.ToInt32(this.PeriodTextBox.Text), Convert.ToInt32(this.FgModeDropDownList.SelectedValue), Convert.ToInt32(this.TypeRadioButtonList.SelectedValue));

            //this.ReportViewer1.LocalReport.EnableHyperlinks = true;
            this.ReportViewer1.LocalReport.EnableExternalImages = true;
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
            this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath;
            this.ReportViewer1.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[6];
            _reportParam[0] = new ReportParameter("Year", this.YearTextBox.Text, true);
            _reportParam[1] = new ReportParameter("Period", this.PeriodTextBox.Text, true);
            _reportParam[2] = new ReportParameter("Mode", FgModeDropDownList.SelectedValue, true);
            _reportParam[3] = new ReportParameter("Type", TypeRadioButtonList.SelectedValue, true);
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