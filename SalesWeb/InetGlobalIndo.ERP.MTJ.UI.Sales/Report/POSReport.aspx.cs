using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.NCC;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;


namespace InetGlobalIndo.ERP.MTJ.UI.Sales.Report
{
    public partial class POSReport : ReportBase
    {
        private ReportSalesBL _reportBL = new ReportSalesBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _reportPath0 = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDPOSReport, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer1.Attributes.Add("style", "margin-bottom: 30px;");

            if (!Page.IsPostBack == true)
            {
                this.StartDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.StartDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.EndDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.EndDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.HeaderReportList.ReportGroup = "POS";
                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = "POS Report";

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
            this.StartDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.EndDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);

        }

        public void SetAttribut()
        {
            this.StartDateTextBox.Attributes.Add("ReadOnly", "True");
            this.EndDateTextBox.Attributes.Add("ReadOnly", "True");
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = false;
            this.ReportViewer1.Visible = true;

            DateTime _startDateTime = new DateTime(DateFormMapper.GetValue(this.StartDateTextBox.Text).Year, DateFormMapper.GetValue(this.StartDateTextBox.Text).Month, DateFormMapper.GetValue(this.StartDateTextBox.Text).Day, 0, 0, 0);
            DateTime _endDateTime = new DateTime(DateFormMapper.GetValue(this.EndDateTextBox.Text).Year, DateFormMapper.GetValue(this.EndDateTextBox.Text).Month, DateFormMapper.GetValue(this.EndDateTextBox.Text).Day, 23, 59, 59);

            ReportDataSource _reportDataSource1 = new ReportDataSource();
            String _orderBy = "TransDate";
            int _fgHeader = 1;
            if (this.HeaderReportList.SelectedValue == "Report/POSReportSummaryByNoNota.rdlc" || this.HeaderReportList.SelectedValue == "Report/POSReportDetailByNoNota.rdlc")
                _orderBy = "FileNmbr";
            if (this.HeaderReportList.SelectedValue == "Report/POSReportSummaryByCustomer.rdlc" || this.HeaderReportList.SelectedValue == "Report/POSReportDetailByCustomer.rdlc")
                _orderBy = "CustName";
            if (this.HeaderReportList.SelectedValue == "Report/POSReportSummaryBySales.rdlc" || this.HeaderReportList.SelectedValue == "Report/POSReportDetailBySales.rdlc")
                _orderBy = "EmpName";
            if (this.HeaderReportList.SelectedValue == "Report/POSReportDetailBySales.rdlc" || this.HeaderReportList.SelectedValue == "Report/POSReportDetailByCustomer.rdlc" || this.HeaderReportList.SelectedValue == "Report/POSReportDetailByNoNota.rdlc" || this.HeaderReportList.SelectedValue == "Report/POSReportDetailByTanggal.rdlc")
                _fgHeader = 0;

            _reportDataSource1 = this._reportBL.POSReport(_startDateTime, _endDateTime,this.NoNota.Text, this.CustomerCode.Text,_orderBy , _fgHeader );

            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

            if (this.HeaderReportList.SelectedValue != "")
                _reportPath0 = this.HeaderReportList.SelectedValue;

            this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;

            this.ReportViewer1.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[7];
            _reportParam[0] = new ReportParameter("Start", _startDateTime.ToString(), true);
            _reportParam[1] = new ReportParameter("End", _endDateTime.ToString(), true);
            _reportParam[2] = new ReportParameter("FileNo", this.NoNota.Text , true);
            _reportParam[3] = new ReportParameter("CustCode", this.CustomerCode.Text , true);
            _reportParam[4] = new ReportParameter("OrderBy", _orderBy , true);
            _reportParam[5] = new ReportParameter("FgHeader", _fgHeader.ToString() , true);
            _reportParam[6] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);

            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();

        }

    }
}