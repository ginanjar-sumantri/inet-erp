using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.Report
{
    public partial class SalesInvoiceTopSalesPerProduct : ReportBase
    {
        private ReportFinanceBL _reportBL = new ReportFinanceBL();
        private ProductBL _productBL = new ProductBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _reportPath0 = "Report/SalesInvoiceTopSalesPerProduct.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDSalesInvoiceTopSalesPerProduct, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

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

                this.PageTitleLiteral.Text = "Sales Invoice Top Sales Per Product Report";

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.MenuPanel.Visible = true;
                this.ReportViewer.Visible = false;

                this.ShowProductSubGroup();

                this.SetAttribute();
                this.ClearData();
            }
        }

        private void SetAttribute()
        {
            this.StartDateTextBox.Attributes.Add("ReadOnly", "True");
            this.EndDateTextBox.Attributes.Add("ReadOnly", "True");

            this.RecordTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ShowProductSubGroup()
        {
            this.ProductSubGroupCheckBoxList.Items.Clear();
            this.ProductSubGroupCheckBoxList.DataSource = this._productBL.GetListProductSubGroupForDDL();
            this.ProductSubGroupCheckBoxList.DataValueField = "ProductSubGrpCode";
            this.ProductSubGroupCheckBoxList.DataTextField = "ProductSubGrpName";
            this.ProductSubGroupCheckBoxList.DataBind();
        }

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ClearLabel();
            this.StartDateTextBox.Text = DateFormMapper.GetValue(now);
            this.EndDateTextBox.Text = DateFormMapper.GetValue(now);
            this.FgReportDropDownList.SelectedValue = "null";
            this.RecordTextBox.Text = "15";
            this.ProductSubGroupCheckBoxList.ClearSelection();
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = false;
            this.ReportViewer.Visible = true;

            string _productSubGroupCode = "";

            var _hasilProductSubGroup = this._productBL.GetListProductSubGroupForDDL();

            for (var i = 0; i < _hasilProductSubGroup.Count(); i++)
            {
                if (this.ProductSubGroupCheckBoxList.Items[i].Selected == true)
                {
                    if (_productSubGroupCode == "")
                    {
                        _productSubGroupCode += this.ProductSubGroupCheckBoxList.Items[i].Value;
                    }
                    else
                    {
                        _productSubGroupCode += "," + this.ProductSubGroupCheckBoxList.Items[i].Value;
                    }
                }
            }

            DateTime _startDateTime = new DateTime(DateFormMapper.GetValue(this.StartDateTextBox.Text).Year, DateFormMapper.GetValue(this.StartDateTextBox.Text).Month, DateFormMapper.GetValue(this.StartDateTextBox.Text).Day, 0, 0, 0);
            DateTime _endDateTime = new DateTime(DateFormMapper.GetValue(this.EndDateTextBox.Text).Year, DateFormMapper.GetValue(this.EndDateTextBox.Text).Month, DateFormMapper.GetValue(this.EndDateTextBox.Text).Day, 23, 59, 59);

            ReportDataSource _reportDataSource1 = this._reportBL.SalesInvoiceTopSalesPerProduct(_startDateTime, _endDateTime, _productSubGroupCode, this.RecordTextBox.Text, Convert.ToInt32(this.FgReportDropDownList.SelectedValue));

            this.ReportViewer.LocalReport.DataSources.Clear();
            this.ReportViewer.LocalReport.DataSources.Add(_reportDataSource1);

            this.ReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;

            this.ReportViewer.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[6];
            _reportParam[0] = new ReportParameter("Start", _startDateTime.ToString(), true);
            _reportParam[1] = new ReportParameter("End", _endDateTime.ToString(), true);
            _reportParam[2] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            _reportParam[3] = new ReportParameter("iTop", this.RecordTextBox.Text, true);
            _reportParam[4] = new ReportParameter("Order", this.FgReportDropDownList.SelectedValue, true);
            _reportParam[5] = new ReportParameter("Str1", _productSubGroupCode, true);

            this.ReportViewer.LocalReport.SetParameters(_reportParam);
            this.ReportViewer.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}