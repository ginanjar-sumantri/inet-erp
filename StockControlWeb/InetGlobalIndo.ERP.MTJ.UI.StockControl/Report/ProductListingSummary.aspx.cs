using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Report
{
    public partial class ProductListing : ReportBase
    {
        private ReportStockControlBL _report = new ReportStockControlBL();
        private ProductBL _productBL = new ProductBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _reportPath0 = "Report/ProductListingSummaryPerAllProduct.rdlc";
        private string _reportPath1 = "Report/ProductListingSummaryPerProductGroup.rdlc";
        private string _reportPath2 = "Report/ProductListingSummaryPerProductSubGroup.rdlc";
        private string _reportPath3 = "Report/ProductListingSummaryPerProductType.rdlc";

        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDProductListing, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

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
                this.PageTitleLiteral.Text = "Product Listing Summary Report";

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.MenuPanel.Visible = true;
                this.ReportViewer1.Visible = false;

                this.ShowProductGroup();
                this.ShowProductType();

                this.ClearData();
            }
        }

        protected void ShowProductGroup()
        {
            this.ProductGroupCheckBoxList.Items.Clear();
            this.ProductGroupCheckBoxList.DataSource = this._productBL.GetListForDDL();
            this.ProductGroupCheckBoxList.DataValueField = "ProductGrpCode";
            this.ProductGroupCheckBoxList.DataTextField = "ProductGrpName";
            this.ProductGroupCheckBoxList.DataBind();
        }

        protected void ShowProductType()
        {
            this.ProductTypeCheckBoxList.Items.Clear();
            this.ProductTypeCheckBoxList.DataSource = this._productBL.GetListProductTypeForDDL();
            this.ProductTypeCheckBoxList.DataValueField = "ProductTypeCode";
            this.ProductTypeCheckBoxList.DataTextField = "ProductTypeName";
            this.ProductTypeCheckBoxList.DataBind();
        }

        public void ClearData()
        {
            this.FgActive.SelectedValue = "null";
            this.ProductGroupCheckBoxList.SelectedValue = "null";
            this.ProductTypeCheckBoxList.SelectedValue = "null";
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = false;
            this.ReportViewer1.Visible = true;

            string _productGroup = "";
            string _productType = "";
            string _fgReport = "";

            var hasilGroup = this._productBL.GetListForDDL();
            for (var i = 0; i < hasilGroup.Count(); i++)
            {
                if (this.ProductGroupCheckBoxList.Items[i].Selected == true)
                {
                    if (_productGroup == "")
                    {
                        _productGroup += this.ProductGroupCheckBoxList.Items[i].Value;
                    }
                    else
                    {
                        _productGroup += "," + this.ProductGroupCheckBoxList.Items[i].Value;
                    }
                }
            }

            var hasilType = this._productBL.GetListProductTypeForDDL();
            for (var i = 0; i < hasilType.Count(); i++)
            {
                if (this.ProductTypeCheckBoxList.Items[i].Selected == true)
                {
                    if (_productType == "")
                    {
                        _productType += this.ProductTypeCheckBoxList.Items[i].Value;
                    }
                    else
                    {
                        _productType += "," + this.ProductTypeCheckBoxList.Items[i].Value;
                    }
                }
            }

            if (this.GroupByDDL.SelectedValue == "0")
            {
                if (this.FgActive.SelectedValue == "0") _fgReport = "3";
                else if (this.FgActive.SelectedValue == "1") _fgReport = "7";
                else if (this.FgActive.SelectedValue == "2") _fgReport = "11";
            }
            else if (this.GroupByDDL.SelectedValue == "1")
            {
                if (this.FgActive.SelectedValue == "0") _fgReport = "1";
                else if (this.FgActive.SelectedValue == "1") _fgReport = "5";
                else if (this.FgActive.SelectedValue == "2") _fgReport = "9";
            }
            else if (this.GroupByDDL.SelectedValue == "2")
            {
                if (this.FgActive.SelectedValue == "0") _fgReport = "2";
                else if (this.FgActive.SelectedValue == "1") _fgReport = "6";
                else if (this.FgActive.SelectedValue == "2") _fgReport = "10";
            }
            else if (this.GroupByDDL.SelectedValue == "3")
            {
                if (this.FgActive.SelectedValue == "0") _fgReport = "0";
                else if (this.FgActive.SelectedValue == "1") _fgReport = "4";
                else if (this.FgActive.SelectedValue == "2") _fgReport = "8";
            }

            ReportDataSource _reportDataSource1 = this._report.ProductListing(_fgReport, this.ProductGroupCheckBoxList.SelectedValue, this.ProductTypeCheckBoxList.SelectedValue);

            this.ReportViewer1.LocalReport.EnableExternalImages = true;
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

            if (this.GroupByDDL.SelectedValue == "0")
            {
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
            }
            else if (this.GroupByDDL.SelectedValue == "1")
            {
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
            }
            else if (this.GroupByDDL.SelectedValue == "2")
            {
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath2;
            }
            else if (this.GroupByDDL.SelectedValue == "3")
            {
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath3;
            }

            this.ReportViewer1.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[6];
            _reportParam[0] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            _reportParam[1] = new ReportParameter("Str1", this.ProductTypeCheckBoxList.SelectedValue, true);
            _reportParam[2] = new ReportParameter("Str2", this.ProductGroupCheckBoxList.SelectedValue, true);
            _reportParam[3] = new ReportParameter("Str3", "", false);
            _reportParam[4] = new ReportParameter("FgReport", _fgReport, true);
            _reportParam[5] = new ReportParameter("Path", ApplicationConfig.ProductPhotoVirDirPath, false);

            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowProductGroup();
            this.ShowProductType();

            this.ClearData();
        }
    }
}