using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using VTSWeb.SystemConfig;
using VTSWeb.Common;
using VTSWeb.Database;
using VTSWeb.BusinessRule;
using VTSWeb.DataMapping;


namespace VTSWeb.UI
{
    public partial class ReportSummaryCustomerLogVisitByCustomer : ReportBase
    {
        private ReportClearanceBL _reportClearanceBL = new ReportClearanceBL();
        private MsCustomerBL _customerBL = new MsCustomerBL();

        private int?[] _navMark = { null, null, null, null };
        //private bool _flag = true;
        //private bool _nextFlag = false;
        //private bool _lastFlag = false;

        private string _currPageKey = "CurrentPage";

        private String _reportPath0 = "Report/SumCustomerLogVisitByCustomer.rdlc";

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        //private int _no = 0;
        //private int _nomor = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

            //HttpCookie cookie = Request.Cookies[ApplicationConfig.CookiesPreferences];
            //if (cookie == null)
            //{
            //    Response.Redirect("..\\Login.aspx");
            //}

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteralSummaryCustomerLogVisitByDate;
                this.ViewState[this._currPageKey] = 0;
                this.ViewButton.ImageUrl = "../images/view2.jpg";

                ShowData(0);
                //this.PanelSearch.Visible = true;

            }
        }

        protected void ShowData(Int32 _prmCurrentPage)
        {
            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._page = _prmCurrentPage;

            DateTime _now = DateTime.Now;

            this.StartDateTextBox.Text = DateFormMapping.GetValue(_now);
            this.EndDateTextBox.Text = DateFormMapping.GetValue(_now);

        }


        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            //this.PanelSearch.Visible = false;
            this.ReportViewer1.Visible = true;
            ReportDataSource _reportDataSource1 = this._reportClearanceBL.ReportSummaryCustomerLogVisitByCustomer(Convert.ToDateTime(this.StartDateTextBox.Text), Convert.ToDateTime(this.EndDateTextBox.Text), this.StartNameTextBox.Text, this.EndNameTextBox.Text);
            //ReportDataSource _reportDataSource1 = this._reportDowntimeBL.ReportDowntimePerformanceDetailbyCircuit(this.CustomerDDL.SelectedValue, ((this.CircuitSelectRBL.SelectedValue == "All") ? "" : this.CircuitSelectDDL.SelectedValue), Convert.ToDateTime(this.DateFrom1TextBox.Text), Convert.ToDateTime(this.DateFrom2TextBox.Text));
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

            this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;


            ReportParameter[] _reportParam = new ReportParameter[6];
            _reportParam[0] = new ReportParameter("StartDate", this.StartDateTextBox.Text, true);
            _reportParam[1] = new ReportParameter("EndDate", this.EndDateTextBox.Text, true);
            _reportParam[2] = new ReportParameter("StartCustName", this.StartNameTextBox.Text, true);
            _reportParam[3] = new ReportParameter("EndCustName", this.EndNameTextBox.Text, true);
            _reportParam[4] = new ReportParameter("Param", "PT. Inet Global Indo", true);
            _reportParam[5] = new ReportParameter("ParamTitle", "Summary Customer Log Visit By Customer", true);

            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

        //protected void CircuitSelectRBL_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (CircuitSelectRBL.SelectedValue == "All")
        //    {
        //        this.CircuitSelectDDL.Visible = false;
        //    }
        //    else if (CircuitSelectRBL.SelectedValue == "Circuit")
        //    {
        //        this.CircuitSelectDDL.Visible = true;

        //    }
        //}
    }
}
