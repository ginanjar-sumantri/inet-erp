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
    public partial class PriceList : ReportBase
    {
        private ReportStockControlBL _reportStokControlBL = new ReportStockControlBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _reportPath0 = "Report/ReportPriceListGrpByProd.rdlc";
        private string _reportPath1 = "Report/PriceListGrpByPG.rdlc";
        private string _reportPath2 = "Report/ReportPriceListGrpByProdSubGrp.rdlc";

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
                this.HeaderReportList1.ReportGroup = "STCPriceList";
                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = "Price List Report";

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

            string _startCode = (this.StartCodeTextBox.Text == "") ? "000" : this.StartCodeTextBox.Text;
            string _endCode = (this.EndCodeTextBox.Text == "") ? "zzz" : this.EndCodeTextBox.Text;
            string _startPriceGroup = (this.StartPriceGroupTextBox.Text == "") ? "000" : this.StartPriceGroupTextBox.Text;
            string _endPriceGroup = (this.EndPriceGroupTextBox.Text == "") ? "zzz" : this.EndPriceGroupTextBox.Text;
            string _startPrice = (this.StartPriceTextBox.Text == "") ? "0" : this.StartPriceTextBox.Text;
            string _endPrice = (this.EndPriceTextBox.Text == "") ? "9999" : this.EndPriceTextBox.Text;

            ReportDataSource _reportDataSource1 = this._reportStokControlBL.PriceList(_startCode, _endCode, _startPriceGroup, _endPriceGroup, _startPrice, _endPrice);

            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

            if (this.HeaderReportList1.SelectedValue == "Report/ReportPriceListGrpByProd.rdlc")
            {
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
            }
            else if (this.HeaderReportList1.SelectedValue == "Report/PriceListGrpByPG.rdlc")
            {
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
            }
            else if (this.HeaderReportList1.SelectedValue == "Report/ReportPriceListGrpByProdSubGrp.rdlc")
            {
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath2;
            }

            this.ReportViewer1.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[7];
            _reportParam[0] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            _reportParam[1] = new ReportParameter("StartCode", _startCode, true);
            _reportParam[2] = new ReportParameter("EndCode", _endCode, true);
            _reportParam[3] = new ReportParameter("StartPG", _startPriceGroup, true);
            _reportParam[4] = new ReportParameter("EndPG", _endPriceGroup, true);
            _reportParam[5] = new ReportParameter("StartPrice", _startPrice, true);
            _reportParam[6] = new ReportParameter("EndPrice", _endPrice, true);

            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
 
        }
    }
}