using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using Microsoft.Reporting.WebForms;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.Report
{
    public partial class CustomerBillingAccountReport : ReportBillingBase
    {
        private ReportBillingBL _reportBillingBL = new ReportBillingBL();
        private CustomerBL _custBL = new CustomerBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _reportPath0 = "Report/CustomerBillingAccountReport.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDCustomerBillingAccount, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer.Attributes.Add("style", "margin-bottom: 30px;");

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = "Customer Expired Billing Account Report";

                this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/preview.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetButtonPermission();

                this.ShowCustGroup();
                this.ShowCustType();
                this.ClearLabel();
                this.SetAttribute();

                this.ClearData();
            }
        }

        private void SetButtonPermission()
        {
            this._permView = this._permBL.PermissionValidation1(this._menuIDCustomerBillingAccount, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.PreviewButton.Visible = false;
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ShowCustGroup()
        {
            this.CustGroupCheckBoxList.ClearSelection();
            this.CustGroupCheckBoxList.Items.Clear();
            this.CustGroupCheckBoxList.DataSource = this._custBL.GetListCustGroupForDDL();
            this.CustGroupCheckBoxList.DataValueField = "CustGroupCode";
            this.CustGroupCheckBoxList.DataTextField = "CustGroupName";
            this.CustGroupCheckBoxList.DataBind();
        }

        protected void ShowCustType()
        {
            this.CustTypeCheckBoxList.ClearSelection();
            this.CustTypeCheckBoxList.Items.Clear();
            this.CustTypeCheckBoxList.DataSource = this._custBL.GetListCustTypeForDDL();
            this.CustTypeCheckBoxList.DataValueField = "CustTypeCode";
            this.CustTypeCheckBoxList.DataTextField = "CustTypeName";
            this.CustTypeCheckBoxList.DataBind();
        }

        protected void SetAttribute()
        {
            this.PeriodTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.PeriodTextBox.Attributes.Add("OnBlur", " ValidatePeriod(" + this.PeriodTextBox.ClientID + ");");

            this.YearTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.YearTextBox.Attributes.Add("OnBlur", " ValidateYear(" + this.YearTextBox.ClientID + ");");
        }

        protected void ClearData()
        {
            DateTime _now = DateTime.Now;

            this.PeriodTextBox.Text = _now.Month.ToString();
            this.YearTextBox.Text = _now.Year.ToString();
            this.CustGroupCheckBoxList.ClearSelection();
            this.CustTypeCheckBoxList.ClearSelection();
        }

        protected void PreviewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.Panel1.Visible = false;
            this.Panel2.Visible = true;

            string _custGroupCode = "";
            string _custTypeCode = "";

            var _hasilCustGroup = this._custBL.GetListCustGroupForDDL();
            var _hasilCustType = this._custBL.GetListCustTypeForDDL();
            
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

            ReportDataSource _reportDataSource1 = this._reportBillingBL.CustomerBillingAccount(this.PeriodTextBox.Text, this.YearTextBox.Text, _custGroupCode, _custTypeCode);

            this.ReportViewer.LocalReport.DataSources.Clear();
            this.ReportViewer.LocalReport.DataSources.Add(_reportDataSource1);
            this.ReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;

            this.ReportViewer.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[5];
            _reportParam[0] = new ReportParameter("Period", (this.PeriodTextBox.Text == "") ? null : this.PeriodTextBox.Text, true);
            _reportParam[1] = new ReportParameter("Year", (this.YearTextBox.Text == "") ? null : this.YearTextBox.Text, true);
            _reportParam[2] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            _reportParam[3] = new ReportParameter("CustGroup", _custGroupCode, false);
            _reportParam[4] = new ReportParameter("CustType", _custTypeCode, false);
            
            this.ReportViewer.LocalReport.SetParameters(_reportParam);
            this.ReportViewer.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }
    }
}
