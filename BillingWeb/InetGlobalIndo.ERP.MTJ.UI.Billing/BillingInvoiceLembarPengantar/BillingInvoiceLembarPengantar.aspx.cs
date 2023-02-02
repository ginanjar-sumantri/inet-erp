using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.BillingInvoiceLembarPengantar
{
    public partial class BillingInvoiceLembarPengantar : BillingInvoiceLembarPengantarBase
    {
        private CustomerBL _customerBL = new CustomerBL();
        private ReportBillingBL _reportBillingBL = new ReportBillingBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _reportPath1 = "BillingInvoiceLembarPengantar/BillingInvoiceLembarPengantar.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/preview.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetButtonPermission();
                this.ClearLabel();
                this.SetAttribute();

                this.ShowCustomerGroup();
                this.ShowCustomerType();

                this.ClearData();
            }
        }

        private void SetButtonPermission()
        {
            this._prmPrintPreview = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.PrintPreview);

            if (this._prmPrintPreview == PermissionLevel.NoAccess)
            {
                this.PreviewButton.Visible = false;
            }

        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.PeriodTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.PeriodTextBox.Attributes.Add("OnBlur", " ValidatePeriod(" + this.PeriodTextBox.ClientID + ");");

            this.YearTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.YearTextBox.Attributes.Add("OnBlur", " ValidateYear(" + this.YearTextBox.ClientID + ");");

        }

        protected void ShowCustomerGroup()
        {
            this.CustomerGroupDropDownList.Items.Clear();
            this.CustomerGroupDropDownList.DataSource = this._customerBL.GetListCustGroupForDDL();
            this.CustomerGroupDropDownList.DataValueField = "CustGroupCode";
            this.CustomerGroupDropDownList.DataTextField = "CustGroupName";
            this.CustomerGroupDropDownList.DataBind();
            this.CustomerGroupDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowCustomerType()
        {
            this.CustomerTypeDropDownList.Items.Clear();
            this.CustomerTypeDropDownList.DataSource = this._customerBL.GetListCustTypeForDDL();
            this.CustomerTypeDropDownList.DataValueField = "CustTypeCode";
            this.CustomerTypeDropDownList.DataTextField = "CustTypeName";
            this.CustomerTypeDropDownList.DataBind();
            this.CustomerTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearData()
        {
            DateTime _now = DateTime.Now;

            this.PeriodTextBox.Text = _now.Month.ToString();
            this.YearTextBox.Text = _now.Year.ToString();
            this.CustomerGroupDropDownList.SelectedValue = "null";
            this.CustomerTypeDropDownList.SelectedValue = "null";

            this.CustomerDropDownList.Items.Clear();
            this.CustomerDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowCustomer()
        {
            string _tempCustomerGroup = (this.CustomerGroupDropDownList.SelectedValue == "null") ? "" : this.CustomerGroupDropDownList.SelectedValue;
            string _tempCustomerType = (this.CustomerTypeDropDownList.SelectedValue == "null") ? "" : this.CustomerTypeDropDownList.SelectedValue;

            this.CustomerDropDownList.Items.Clear();
            this.CustomerDropDownList.DataSource = this._customerBL.GetListDDLCustomer(_tempCustomerGroup, _tempCustomerType);
            this.CustomerDropDownList.DataValueField = "CustCode";
            this.CustomerDropDownList.DataTextField = "CustName";
            this.CustomerDropDownList.DataBind();
            this.CustomerDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void CustomerGroupDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowCustomer();
        }

        protected void CustomerTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowCustomer();
        }

        protected void PreviewButton_Click(object sender, ImageClickEventArgs e)
        {
            string _tempCustomerGroup = (this.CustomerGroupDropDownList.SelectedValue == "null") ? "" : this.CustomerGroupDropDownList.SelectedValue;
            string _tempCustomerType = (this.CustomerTypeDropDownList.SelectedValue == "null") ? "" : this.CustomerTypeDropDownList.SelectedValue;
            string _tempCustomer = (this.CustomerDropDownList.SelectedValue == "null") ? "" : this.CustomerDropDownList.SelectedValue;

            this.Panel1.Visible = false;
            this.Panel2.Visible = true;

            ReportDataSource _reportDataSource1 = this._reportBillingBL.BillingInvoiceLembarPengantar(Convert.ToInt32(this.PeriodTextBox.Text), Convert.ToInt32(this.YearTextBox.Text), _tempCustomerGroup, _tempCustomerType, _tempCustomer);

            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
            this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
            this.ReportViewer1.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[5];
            _reportParam[0] = new ReportParameter("Period", this.PeriodTextBox.Text, true);
            _reportParam[1] = new ReportParameter("Year", this.YearTextBox.Text, true);
            _reportParam[2] = new ReportParameter("CustomerGroup", _tempCustomerGroup, true);
            _reportParam[3] = new ReportParameter("CustomerType", _tempCustomerType, true);
            _reportParam[4] = new ReportParameter("CustomerCode", _tempCustomer, true);
            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }
    }
}
