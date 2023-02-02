using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.Report
{
    public partial class DifferenceRateSupplierPerDate : ReportBase
    {
        private ReportFinanceBL _report = new ReportFinanceBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();

        private string _reportPath0 = "Report/DifferenceRateSupplierPerDate.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDDifferenceRateSupplierPerDate, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

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

                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = "Difference Rate Supplier Per Date";

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.MenuPanel.Visible = true;
                this.ReportViewer1.Visible = false;

                this.ShowCurrency();

                this.SetAttribute();
                this.ClearData();
            }
        }

        public void SetAttribute()
        {
            this.StartDateTextBox.Attributes.Add("ReadOnly", "True");
            this.EndDateTextBox.Attributes.Add("ReadOnly", "True");
        }

        public void ClearData()
        {
            this.StartDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.EndDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.CurrCodeCheckBoxList.ClearSelection();
        }

        private void ShowCurrency()
        {
            this.CurrCodeCheckBoxList.DataTextField = "CurrCode";
            this.CurrCodeCheckBoxList.DataValueField = "CurrCode";
            this.CurrCodeCheckBoxList.DataSource = this._currencyBL.GetListAll();
            this.CurrCodeCheckBoxList.DataBind();
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = false;
            this.ReportViewer1.Visible = true;

            string _currCode = "";

            var _hasilCurr = this._currencyBL.GetListAll();

            for (var i = 0; i < _hasilCurr.Count(); i++)
            {
                if (this.CurrCodeCheckBoxList.Items[i].Selected == true)
                {
                    if (_currCode == "")
                    {
                        _currCode += this.CurrCodeCheckBoxList.Items[i].Value;
                    }
                    else
                    {
                        _currCode += "," + this.CurrCodeCheckBoxList.Items[i].Value;
                    }
                }
            }

            DateTime _startDateTime = new DateTime(DateFormMapper.GetValue(this.StartDateTextBox.Text).Year, DateFormMapper.GetValue(this.StartDateTextBox.Text).Month, DateFormMapper.GetValue(this.StartDateTextBox.Text).Day, 0, 0, 0);
            DateTime _endDateTime = new DateTime(DateFormMapper.GetValue(this.EndDateTextBox.Text).Year, DateFormMapper.GetValue(this.EndDateTextBox.Text).Month, DateFormMapper.GetValue(this.EndDateTextBox.Text).Day, 23, 59, 59);

            ReportDataSource _reportDataSource1 = this._report.DifferenceRateSupplierPerDate(_startDateTime, _endDateTime, _currCode, "", "");

            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
            this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
            this.ReportViewer1.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[6];
            _reportParam[0] = new ReportParameter("Start", _startDateTime.ToString(), true);
            _reportParam[1] = new ReportParameter("End", _endDateTime.ToString(), true);
            _reportParam[2] = new ReportParameter("Str1", _currCode, true);
            _reportParam[3] = new ReportParameter("Str2", "", false);
            _reportParam[4] = new ReportParameter("Str3", "", false);
            _reportParam[5] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);

            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}
