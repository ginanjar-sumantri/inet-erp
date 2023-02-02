using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using System.Web.UI.WebControls;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.Report
{
    public partial class CashflowProjection : ReportBase
    {
        private ReportBL _report = new ReportBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _reportPath0 = "Report/CashflowProjection.rdlc";

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_TransClassCheckBoxList_";
        private string _akhir = "";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDCashflowProjection, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer.Attributes.Add("style", "margin-bottom: 30px;");

            if (!this.Page.IsPostBack == true)
            {
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = "Cashflow Projection Report";

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.RangeTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.RangeTextBox.ClientID + "," + this.RangeTextBox.ClientID + ",500" + ");");
            
                this.DateTextBox.Attributes.Add("ReadOnly", "True");
                
                this.MenuPanel.Visible = true;
                this.ReportViewer.Visible = false;

                this.ClearData();
            }
        }

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            this.RangeTypeDDL.SelectedValue = "Month";
            this.RangeTextBox.Text = "1";
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = false;
            this.ReportViewer.Visible = true;

            DateTime _dateTime = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, 0, 0, 0);

            ReportDataSource _reportDataSource1 = this._report.CashflowProjection(_dateTime, this.RangeTypeDDL.SelectedValue.Trim(), Convert.ToInt32(this.RangeTextBox.Text));

            this.ReportViewer.LocalReport.DataSources.Clear();
            this.ReportViewer.LocalReport.DataSources.Add(_reportDataSource1);
            this.ReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
            this.ReportViewer.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[3];
            _reportParam[0] = new ReportParameter("Date", _dateTime.ToString(), true);
            _reportParam[1] = new ReportParameter("RangeType", this.RangeTypeDDL.SelectedValue, true);
            _reportParam[2] = new ReportParameter("RangeX", this.RangeTextBox.Text.Trim(), true);
            
            this.ReportViewer.LocalReport.SetParameters(_reportParam);
            this.ReportViewer.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}