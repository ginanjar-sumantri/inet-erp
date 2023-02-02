using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.Report
{
    public partial class TransactionJournal : ReportBase
    {
        private ReportBL _report = new ReportBL();
        private TransTypeBL _transType = new TransTypeBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _reportPath0 = "Report/TransactionJournal.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDTransactionJournal, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

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
                this.PageTitleLiteral.Text = "Transaction Journal";

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.YearTextBox.Attributes.Add("OnKeyDown", "return NumericWithEnter();");
                this.PeriodTextBox.Attributes.Add("OnKeyDown", "return NumericWithEnter();");

                this.MenuPanel.Visible = true;
                this.ReportViewer.Visible = false;

                this.ClearData();
                this.ShowTransTypeDDL();
            }
        }

        public void ClearData()
        {
            this.FgTypeRadioBtnList.SelectedValue = "TransNmbr";
            this.TransIdLiteral.Text = "Trans No.";
            this.TransIdTextbox.Text = "";
            DateTime _now = DateTime.Now;

            this.YearTextBox.Text = _now.Year.ToString();
            this.PeriodTextBox.Text = "";
        }

        public void ShowTransTypeDDL()
        {
            this.TransTypeDDL.DataSource = this._transType.GetList();
            this.TransTypeDDL.DataValueField = "TransTypeCode";
            this.TransTypeDDL.DataTextField = "TransTypeName";
            this.TransTypeDDL.DataBind();
            this.TransTypeDDL.Items.Insert(0, new ListItem("[Choose One]", "00"));
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = false;
            this.ReportViewer.Visible = true;

            ReportDataSource _reportDataSource1 = this._report.TransactionJournal(this.TransTypeDDL.SelectedValue, Convert.ToInt32(this.YearTextBox.Text), Convert.ToInt32(this.PeriodTextBox.Text), this.FgTypeRadioBtnList.SelectedValue, this.TransIdTextbox.Text);

            this.ReportViewer.LocalReport.DataSources.Clear();
            this.ReportViewer.LocalReport.DataSources.Add(_reportDataSource1);
            this.ReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
            this.ReportViewer.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[6];
            _reportParam[0] = new ReportParameter("TransClass", this.TransTypeDDL.SelectedValue, true);
            _reportParam[1] = new ReportParameter("Year", this.YearTextBox.Text, true);
            _reportParam[2] = new ReportParameter("Period", this.PeriodTextBox.Text, true);
            _reportParam[3] = new ReportParameter("FgType", this.FgTypeRadioBtnList.SelectedValue, true);
            _reportParam[4] = new ReportParameter("TransId", this.TransIdTextbox.Text, true);
            _reportParam[5] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);

            this.ReportViewer.LocalReport.SetParameters(_reportParam);
            this.ReportViewer.LocalReport.Refresh();

        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        protected void FgTypeRadioBtnList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.FgTypeRadioBtnList.SelectedValue == "TransNmbr")
            {
                this.TransIdLiteral.Text = "Trans No.";
            }
            else
            {
                this.TransIdLiteral.Text = "File No.";
            }
        }
    }
}