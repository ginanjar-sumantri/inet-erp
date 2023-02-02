using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Collections.Generic;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.Report
{
    public partial class CustomerInvoiceSummary : ReportBillingBase
    {
        private ReportBillingBL _report = new ReportBillingBL();
        private CustomerBL _customerBL = new CustomerBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _reportPath0 = "Report/CustomerInvoiceSummary.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDCustomerInvoiceSummary, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer1.Attributes.Add("style", "margin-bottom: 30px;");

            if (!Page.IsPostBack == true)
            {
                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = "Customer Invoice Summary Report";

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.MenuPanel.Visible = true;
                this.ReportViewer1.Visible = false;

                this.ShowCustType();
                this.ShowCustGroup();

                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void ShowCustType()
        {
            this.CustTypeCheckBoxList.Items.Clear();
            this.CustTypeCheckBoxList.DataSource = this._customerBL.GetListCustTypeForDDL();
            this.CustTypeCheckBoxList.DataValueField = "CustTypeCode";
            this.CustTypeCheckBoxList.DataTextField = "CustTypeName";
            this.CustTypeCheckBoxList.DataBind();
        }

        protected void ShowCustGroup()
        {
            this.CustGroupCheckBoxList.Items.Clear();
            this.CustGroupCheckBoxList.DataSource = this._customerBL.GetListCustGroupForDDL();
            this.CustGroupCheckBoxList.DataValueField = "CustGroupCode";
            this.CustGroupCheckBoxList.DataTextField = "CustGroupName";
            this.CustGroupCheckBoxList.DataBind();
        }

        public void ClearData()
        {
            this.StartDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.EndDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.CustTypeCheckBoxList.ClearSelection();
            this.CustGroupCheckBoxList.ClearSelection();
        }

        public void SetAttribute()
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

            string _custTypeCode = "";
            string _custGroupCode = "";

            var _hasilCustType = this._customerBL.GetListCustTypeForDDL();
            var _hasilCustGroup = this._customerBL.GetListCustGroupForDDL();

            for (var i = 0; i < _hasilCustType.Count(); i++)
            {
                if (this.CustTypeCheckBoxList.Items[i].Selected == true)
                {
                    if (_custTypeCode == "")
                    {
                        _custTypeCode += this.CustTypeCheckBoxList.Items[i].Value;
                    }
                    else
                    {
                        _custTypeCode += "," + this.CustTypeCheckBoxList.Items[i].Value;
                    }
                }
            }

            for (var i = 0; i < _hasilCustGroup.Count(); i++)
            {
                if (this.CustGroupCheckBoxList.Items[i].Selected == true)
                {
                    if (_custGroupCode == "")
                    {
                        _custGroupCode += this.CustGroupCheckBoxList.Items[i].Value;
                    }
                    else
                    {
                        _custGroupCode += "," + this.CustGroupCheckBoxList.Items[i].Value;
                    }
                }
            }

            ReportDataSource _reportDataSource1 = this._report.CustomerInvoiceSummary(_startDateTime, _endDateTime, _custTypeCode, _custGroupCode);

            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
            this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
            this.ReportViewer1.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[6];
            _reportParam[0] = new ReportParameter("Start", _startDateTime.ToString(), true);
            _reportParam[1] = new ReportParameter("End", _endDateTime.ToString(), true);
            _reportParam[2] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            _reportParam[3] = new ReportParameter("Str1", _custTypeCode, true);
            _reportParam[4] = new ReportParameter("Str2", _custGroupCode, true);
            _reportParam[5] = new ReportParameter("Str3", "", true);

            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowCustType();
            this.ShowCustGroup();

            this.ClearData();
        }
    }
}