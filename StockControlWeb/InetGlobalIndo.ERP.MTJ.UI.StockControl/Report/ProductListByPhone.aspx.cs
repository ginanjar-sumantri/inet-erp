using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Report
{
    public partial class ProductListByPhone : ReportBase
    {
        private ReportStockControlBL _reportStokControlBL = new ReportStockControlBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _reportPath = "Report/ProductListByPhoneType.rdlc";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDNCPSoldListByCategory, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer1.Attributes.Add("style", "margin-bottom: 30px;");

            if (!Page.IsPostBack == true)
            {
                //this.HeaderReportList1.ReportGroup = "STCSellingPrice";
                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = "Product List By Phone Type Report";

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.MenuPanel.Visible = true;
                this.ReportViewer1.Visible = false;
            }
        }

      
        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = false;
            this.ReportViewer1.Visible = true;

            string _startProductName = (this.PhoneTypeFromTextBox.Text == "") ? "000" : this.PhoneTypeFromTextBox.Text;
            string _endProductName = (this.PhoneTypeToTextBox.Text == "") ? "zzz" : this.PhoneTypeToTextBox.Text;
            //string _startCurr = (this.StartCurrTextBox.Text == "") ? "000" : this.StartCurrTextBox.Text;
            //string _endCurr = (this.EndCurrTextBox.Text == "") ? "zzz" : this.EndCurrTextBox.Text;

            ReportDataSource _reportDataSource1 = this._reportStokControlBL.ProductListByPhone(_startProductName, _endProductName);

            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
           
            this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath;
           
            this.ReportViewer1.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[3];
            _reportParam[0] = new ReportParameter("Start", _startProductName, true);
            _reportParam[1] = new ReportParameter("End", _endProductName, true);
            _reportParam[2] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);

            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ProductListByPhone.aspx");
        }
    }
}