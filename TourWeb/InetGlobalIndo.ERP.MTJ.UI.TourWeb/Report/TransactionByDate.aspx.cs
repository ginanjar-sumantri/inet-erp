using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Tour;
using InetGlobalIndo.ERP.MTJ.Common;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.Tour.Report
{
    public partial class TransactionByDate : ReportBase
    {
        private ReportTourBL _reportBL = new ReportTourBL();
        private PermissionBL _permBL = new PermissionBL();

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private string _reportPath0 = "Report/RptRekapTicketByDate.rdlc";
        private string _reportPath1 = "Report/RptRekapVoucherHotelByDate.rdlc";
        private string _reportPath2 = "Report/RptTicketingPerTransaction(AR).rdlc";
        private string _reportPath2b = "Report/RptTicketingPerTransaction(Cash).rdlc";
        private string _reportPath3 = "Report/RptVoucherHotelPerTransaction(AR).rdlc";
        private string _reportPath3b = "Report/RptVoucherHotelPerTransaction(Cash).rdlc";


        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);

        private string _currPageKey = "CurrentPage";

        //private string _awal = "ctl00_DefaultBodyContentPlaceHolder_SupplierCheckBoxList_";
        //private string _akhir = "";
        //private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        //private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDTransactionByDate, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer1.Attributes.Add("style", "margin-bottom: 30px;");

            if (!Page.IsPostBack == true)
            {
                this.BeginDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.BeginDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.EndDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.EndDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = "Transaction By Date";

                this.ViewState[this._currPageKey] = 0;

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";


                this.MenuPanel.Visible = true;
                this.ReportViewer1.Visible = false;
                this.ClearData();

            }
        }

        public void ClearData()
        {
            this.BeginDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.EndDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = false;
            this.ReportViewer1.Visible = true;

            DateTime _beginDateTime = DateFormMapper.GetValue(this.BeginDateTextBox.Text);
            DateTime _endDateTime = DateFormMapper.GetValue(this.EndDateTextBox.Text);
            ReportParameter[] _reportParam = new ReportParameter[0];

            this.ReportViewer1.Reset();
            //this.ReportViewer1.ServerReport.Refresh();

            LocalReport report = new LocalReport();
            report = this.ReportViewer1.LocalReport;

            if (this.RekapTypeDDL.SelectedValue == "RptRpTicketing")
            {

                ReportDataSource _reportDataSource1 = this._reportBL.RekapTicketingByDate(_beginDateTime, _endDateTime);

                //report.EnableExternalImages = true;
                report.DataSources.Clear();
                report.DataSources.Add(_reportDataSource1);

                report.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
                this.ReportViewer1.DataBind();

                _reportParam = new ReportParameter[2];
                _reportParam[0] = new ReportParameter("BeginDate", _beginDateTime.ToString(), true);
                _reportParam[1] = new ReportParameter("EndDate", _endDateTime.ToString(), true);
            }
            else if (this.RekapTypeDDL.SelectedValue == "RptRpHotel")
            {
                ReportDataSource _reportDataSource1 = this._reportBL.RekapVoucherHotelByDate(_beginDateTime, _endDateTime);

                //report.EnableExternalImages = true;
                report.DataSources.Clear();
                report.DataSources.Add(_reportDataSource1);

                report.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
                this.ReportViewer1.DataBind();

                _reportParam = new ReportParameter[2];
                _reportParam[0] = new ReportParameter("BeginDate", _beginDateTime.ToString(), true);
                _reportParam[1] = new ReportParameter("EndDate", _endDateTime.ToString(), true);
            }
            else if (this.RekapTypeDDL.SelectedValue == "RptTrTicketingAR")
            {
                ReportDataSource _reportDataSource1 = this._reportBL.RekapTicketingByDate(_beginDateTime, _endDateTime);

                //report.EnableExternalImages = true;
                report.DataSources.Clear();
                report.DataSources.Add(_reportDataSource1);

                report.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath2;
                this.ReportViewer1.DataBind();

                _reportParam = new ReportParameter[4];
                _reportParam[0] = new ReportParameter("BeginDate", _beginDateTime.ToString(), true);
                _reportParam[1] = new ReportParameter("EndDate", _endDateTime.ToString(), true);
                _reportParam[2] = new ReportParameter("FgReport", "1", true);
                _reportParam[3] = new ReportParameter("CompanyName", "", true);

            }
            else if (this.RekapTypeDDL.SelectedValue == "RptTrTicketingCash")
            {
                ReportDataSource _reportDataSource1 = this._reportBL.RekapTicketingByDate(_beginDateTime, _endDateTime);

                //report.EnableExternalImages = true;
                report.DataSources.Clear();
                report.DataSources.Add(_reportDataSource1);

                report.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath2b;
                this.ReportViewer1.DataBind();

                _reportParam = new ReportParameter[4];
                _reportParam[0] = new ReportParameter("BeginDate", _beginDateTime.ToString(), true);
                _reportParam[1] = new ReportParameter("EndDate", _endDateTime.ToString(), true);
                _reportParam[2] = new ReportParameter("FgReport", "0", true);
                _reportParam[3] = new ReportParameter("CompanyName", "", true);
            }
            else if (this.RekapTypeDDL.SelectedValue == "RptTrHotelAR")
            {
                ReportDataSource _reportDataSource1 = this._reportBL.RekapVoucherHotelByDate(_beginDateTime, _endDateTime);

                //report.EnableExternalImages = true;
                report.DataSources.Clear();
                report.DataSources.Add(_reportDataSource1);

                report.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath3;
                this.ReportViewer1.DataBind();

                _reportParam = new ReportParameter[3];
                _reportParam[0] = new ReportParameter("BeginDate", _beginDateTime.ToString(), true);
                _reportParam[1] = new ReportParameter("EndDate", _endDateTime.ToString(), true);
                _reportParam[2] = new ReportParameter("FgReport", "1", true);
            }
            else if (this.RekapTypeDDL.SelectedValue == "RptTrHotelCash")
            {
                ReportDataSource _reportDataSource1 = this._reportBL.RekapVoucherHotelByDate(_beginDateTime, _endDateTime);

                //report.EnableExternalImages = true;
                report.DataSources.Clear();
                report.DataSources.Add(_reportDataSource1);

                report.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath3b;
                this.ReportViewer1.DataBind();

                _reportParam = new ReportParameter[3];
                _reportParam[0] = new ReportParameter("BeginDate", _beginDateTime.ToString(), true);
                _reportParam[1] = new ReportParameter("EndDate", _endDateTime.ToString(), true);
                _reportParam[2] = new ReportParameter("FgReport", "0", true);
            }
            //ReportParameter[] _reportParam = new ReportParameter[2];
            //_reportParam[0] = new ReportParameter("BeginDate", _beginDateTime.ToString(), true);
            //_reportParam[1] = new ReportParameter("EndDate", _endDateTime.ToString(), true);
            //_reportParam[2] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            //_reportParam[7] = new ReportParameter("Image", ApplicationConfig.HomeWebAppURL + "images/" + new UserBL().CompanyLogo(HttpContext.Current.User.Identity.Name), false);

            report.SetParameters(_reportParam);
            report.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }


    }
}