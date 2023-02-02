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
    public partial class RekapTicketingByDate : ReportBase
    {
        private ReportTourBL _reportBL = new ReportTourBL();
        private PermissionBL _permBL = new PermissionBL();

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private string _reportPath0 = "Report/RptRekapTicketByDate.rdlc";
        private string _reportPath1 = "Report/RptRekapVoucherHotelByDate.rdlc";

        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);

        private string _currPageKey = "CurrentPage";

        //private string _awal = "ctl00_DefaultBodyContentPlaceHolder_SupplierCheckBoxList_";
        //private string _akhir = "";
        //private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        //private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDRptRekapTicketByDate, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

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
                this.PageTitleLiteral.Text = "Report Rekap Ticketing By Date";

                this.ViewState[this._currPageKey] = 0;

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";


                this.MenuPanel.Visible = true;
                this.ReportViewer1.Visible = false;

            }
        }

        public void ClearData()
        {
            this.BeginDateTextBox.Text = "";
            this.EndDateTextBox.Text = "";
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = false;
            this.ReportViewer1.Visible = true;

            DateTime _beginDateTime = DateFormMapper.GetValue(this.BeginDateTextBox.Text);
            DateTime _endDateTime = DateFormMapper.GetValue(this.EndDateTextBox.Text);

            if (this.RekapTypeDDL.SelectedValue == "ticketing")
            {

                ReportDataSource _reportDataSource1 = this._reportBL.RekapTicketingByDate(_beginDateTime, _endDateTime);

                //this.ReportViewer1.LocalReport.EnableExternalImages = true;
                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
                this.ReportViewer1.DataBind();
            }
            else
            {
                ReportDataSource _reportDataSource1 = this._reportBL.RekapVoucherHotelByDate(_beginDateTime, _endDateTime);

                //this.ReportViewer1.LocalReport.EnableExternalImages = true;
                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
                this.ReportViewer1.DataBind();
            }

            ReportParameter[] _reportParam = new ReportParameter[2];
            _reportParam[0] = new ReportParameter("BeginDate", _beginDateTime.ToString(), true);
            _reportParam[1] = new ReportParameter("EndDate", _endDateTime.ToString(), true);
            //_reportParam[2] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            //_reportParam[7] = new ReportParameter("Image", ApplicationConfig.HomeWebAppURL + "images/" + new UserBL().CompanyLogo(HttpContext.Current.User.Identity.Name), false);

            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }


    }
}