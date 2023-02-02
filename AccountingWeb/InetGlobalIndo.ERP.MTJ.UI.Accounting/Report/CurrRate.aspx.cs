using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.Report
{
    public partial class CurrRate : ReportBase
    {
        private ReportBL _reportBL = new ReportBL();
        private CurrencyBL _currBL = new CurrencyBL();
        private CityBL _cityBL = new CityBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _reportPath0 = "Report/CurrRatePerDate.rdlc";
        private string _reportPath1 = "Report/CurrRatePerCurr.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDCurrRate, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer.Attributes.Add("style", "margin-bottom: 30px;");

            if (!this.Page.IsPostBack == true)
            {
                this.StartDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.StartDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.EndDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.EndDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = "Currency Rate Report";

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.MenuPanel.Visible = true;
                this.ReportViewer.Visible = false;

                this.ShowCurr();

                this.ClearData();
                this.SetAttribute();
            }
        }

        protected void SetAttribute()
        {
            this.StartDateTextBox.Attributes.Add("ReadOnly", "True");
            this.EndDateTextBox.Attributes.Add("ReadOnly", "True");
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ShowCurr()
        {
            this.CurrCheckBoxList.ClearSelection();
            this.CurrCheckBoxList.Items.Clear();
            this.CurrCheckBoxList.DataSource = this._currBL.GetListAll();
            this.CurrCheckBoxList.DataValueField = "CurrCode";
            this.CurrCheckBoxList.DataTextField = "CurrName";
            this.CurrCheckBoxList.DataBind();
        }

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ClearLabel();

            this.StartDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.EndDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.CurrCheckBoxList.ClearSelection();
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = false;
            this.ReportViewer.Visible = true;

            string _currCode = "";

            var _hasilCurr = this._currBL.GetListAll();

            for (var i = 0; i < _hasilCurr.Count(); i++)
            {
                if (this.CurrCheckBoxList.Items[i].Selected == true)
                {
                    if (_currCode == "")
                    {
                        _currCode += this.CurrCheckBoxList.Items[i].Value;
                    }
                    else
                    {
                        _currCode += "," + this.CurrCheckBoxList.Items[i].Value;
                    }
                }
            }

            DateTime _startDateTime = new DateTime(DateFormMapper.GetValue(this.StartDateTextBox.Text).Year, DateFormMapper.GetValue(this.StartDateTextBox.Text).Month, DateFormMapper.GetValue(this.StartDateTextBox.Text).Day, 0, 0, 0);
            DateTime _endDateTime = new DateTime(DateFormMapper.GetValue(this.EndDateTextBox.Text).Year, DateFormMapper.GetValue(this.EndDateTextBox.Text).Month, DateFormMapper.GetValue(this.EndDateTextBox.Text).Day, 23, 59, 59);

            ReportDataSource _reportDataSource1 = this._reportBL.CurrRate(_startDateTime,_endDateTime, _currCode, Convert.ToInt32(this.FgTypeDropDownList.SelectedValue));

            this.ReportViewer.LocalReport.DataSources.Clear();
            this.ReportViewer.LocalReport.DataSources.Add(_reportDataSource1);

            if (this.FgTypeDropDownList.SelectedValue == "0")
            {
                this.ReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
            }
            else if (this.FgTypeDropDownList.SelectedValue == "1")
            {
                this.ReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
            }

            this.ReportViewer.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[5];
            _reportParam[0] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            _reportParam[1] = new ReportParameter("Start", _startDateTime.ToString(), true);
            _reportParam[2] = new ReportParameter("End", _endDateTime.ToString(), true);
            _reportParam[3] = new ReportParameter("Str1", _currCode, true);
            _reportParam[4] = new ReportParameter("FgReport", this.FgTypeDropDownList.SelectedValue, true);

            this.ReportViewer.LocalReport.SetParameters(_reportParam);
            this.ReportViewer.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}