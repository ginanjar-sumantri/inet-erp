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
    public partial class DailyTransactionJournal : ReportBase
    {
        private ReportBL _report = new ReportBL();
        private PermissionBL _permBL = new PermissionBL();
        private JournalEntryBL _journalEntryBL = new JournalEntryBL();

        private string _reportPath0 = "Report/DailyTransactionJournal.rdlc";

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_TransClassCheckBoxList_";
        private string _akhir = "";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDDailyTransactionJournal, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

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

                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = "Daily Transaction Journal Report";

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.StartDateTextBox.Attributes.Add("ReadOnly", "True");
                this.EndDateTextBox.Attributes.Add("ReadOnly", "True");

                this.MenuPanel.Visible = true;
                this.ReportViewer.Visible = false;

                this.ClearData();
                this.ShowTransClass();
                this.SetCheckBox();
            }
        }

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.StartDateTextBox.Text = DateFormMapper.GetValue(now);
            this.EndDateTextBox.Text = DateFormMapper.GetValue(now);
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = false;
            this.ReportViewer.Visible = true;

            DateTime _startDateTime = new DateTime(DateFormMapper.GetValue(this.StartDateTextBox.Text).Year, DateFormMapper.GetValue(this.StartDateTextBox.Text).Month, DateFormMapper.GetValue(this.StartDateTextBox.Text).Day, 0, 0, 0);
            DateTime _endDateTime = new DateTime(DateFormMapper.GetValue(this.EndDateTextBox.Text).Year, DateFormMapper.GetValue(this.EndDateTextBox.Text).Month, DateFormMapper.GetValue(this.EndDateTextBox.Text).Day, 23, 59, 59);
            
            string _transClass = "";

            if (this.AllCheckBox.Checked == true)
            {
                _transClass = this.TempHidden.Value;
            }
            else
            {
                _transClass = this.CheckHidden.Value;
            }


            ReportDataSource _reportDataSource1 = this._report.TransactionJournalDaily(_startDateTime, _endDateTime, _transClass, this.FileNmbrTextBox.Text);

            this.ReportViewer.LocalReport.DataSources.Clear();
            this.ReportViewer.LocalReport.DataSources.Add(_reportDataSource1);
            this.ReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
            this.ReportViewer.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[6];
            _reportParam[0] = new ReportParameter("Start", _startDateTime.ToString(), true);
            _reportParam[1] = new ReportParameter("End", _endDateTime.ToString(), true);
            _reportParam[2] = new ReportParameter("PrmTransClass", _transClass, true);
            _reportParam[3] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            _reportParam[4] = new ReportParameter("Str1", "00", true);
            _reportParam[5] = new ReportParameter("Str2", this.FileNmbrTextBox.Text, true);

            this.ReportViewer.LocalReport.SetParameters(_reportParam);
            this.ReportViewer.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        public void ShowTransClass()
        {
            var hasil = this._journalEntryBL.GetTranstypeCodeAndNameListForReport();

            this.TransClassCheckBoxList.DataSource = hasil;
            this.TransClassCheckBoxList.DataValueField = "TransClass";
            this.TransClassCheckBoxList.DataTextField = "TransTypeName";
            this.TransClassCheckBoxList.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "', 'true');");

            int i = 0;
            foreach (ListItem _item in this.TransClassCheckBoxList.Items)
            {
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _awal + i.ToString() + ", '" + _item.Value + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "', 'true');");
                i++;
            }
        }

        protected void SetCheckBox()
        {
            int i = 0;
            foreach (ListItem _item in this.TransClassCheckBoxList.Items)
            {
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + "ctl00_DefaultBodyContentPlaceHolder_TransClassCheckBoxList_" + i.ToString() + ", '" + _item.Value + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "', 'true');");

                _item.Selected = this.CheckHidden.Value.Contains(_item.Value);

                i++;

                //bound all data (on this page) to temphidden
                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _item.Value;
                }
                else
                {
                    this.TempHidden.Value += "," + _item.Value;
                }
            }

            this.SetCheckAllCheckBox();
        }

        protected void SetCheckAllCheckBox()
        {
            this.AllCheckBox.Checked = true;

            foreach (ListItem _item in this.TransClassCheckBoxList.Items)
            {
                if (_item.Selected == false)
                {
                    this.AllCheckBox.Checked = false;
                }
            }
        }
    }
}