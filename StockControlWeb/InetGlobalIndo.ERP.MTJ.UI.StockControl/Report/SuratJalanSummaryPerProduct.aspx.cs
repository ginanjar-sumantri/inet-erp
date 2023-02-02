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
    public partial class SuratJalanSummaryPerProduct : ReportBase
    {
        private ReportStockControlBL _report = new ReportStockControlBL();
        private ProductBL _productBL = new ProductBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _reportPath0 = "Report/SuratJalanSumPerProductByProduct.rdlc";//All Product
        private string _reportPath1 = "Report/SuratJalanSumPerProductByProductGrp.rdlc";//Product Group
        private string _reportPath2 = "Report/SuratJalanSumPerProductByProductSubGrp.rdlc";//Product Sub Group
        private string _reportPath3 = "Report/SuratJalanSumPerProductByProductType.rdlc";//Product Type

        private string _currPageKey = "CurrentPage";

        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDSuratJalanSumProd, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

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
                this.PageTitleLiteral.Text = "Bill Of Lading Summary Per Product Report";

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.SearchImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/go.jpg";

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
            this.ProductTypeCheckBoxList.DataSource = this._productBL.GetListProductTypeForDDL(this.CategoryDropDownList.SelectedValue, this.KeywordTextBox.Text);
            this.ProductTypeCheckBoxList.DataValueField = "ProductTypeCode";
            this.ProductTypeCheckBoxList.DataTextField = "ProductTypeName";
            this.ProductTypeCheckBoxList.DataBind();
        }

        public void ClearData()
        {
            this.ProductGroupCheckBoxList.SelectedValue = "null";
            this.ProductTypeCheckBoxList.SelectedValue = "null";

            this.HeaderReportList1.ReportGroup = "SJSumPerProd";
        }

        private bool ValidateDiffDate()
        {
            bool _result = false;

            DateTime _start = Convert.ToDateTime(this.StartYearTextBox.Text + "-" + this.StartPeriodTextBox.Text + "-01");
            DateTime _end = Convert.ToDateTime(this.EndYearTextBox.Text + "-" + this.EndPeriodTextBox.Text + "-01");

            for (int i = 0; i <= 11; i++)
            {
                if (_start == _end)
                {
                    _result = true;
                    break;
                }
                _start = _start.AddMonths(1);
            }

            return _result;
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ValidateDiffDate() == true)
            {
                this.MenuPanel.Visible = false;
                this.ReportViewer1.Visible = true;

                string _productGroup = "";
                string _productType = "";
                string _fgReport = this.HeaderReportList1.SelectedIndex;

                string _start = this.StartYearTextBox.Text.PadLeft(4, '0') + this.StartPeriodTextBox.Text.PadLeft(2, '0');
                string _end = this.EndYearTextBox.Text.PadLeft(4, '0') + this.EndPeriodTextBox.Text.PadLeft(2, '0');

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

                if (this.ProductFromTextBox.Text == "")
                    this.ProductFromTextBox.Text = "000";

                if (this.ProductToTextBox.Text == "")
                    this.ProductToTextBox.Text = "zzz";

                ReportDataSource _reportDataSource1 = this._report.SuratJalanSummaryPerProduct(_start, _end, this.ProductGroupCheckBoxList.SelectedValue, this.ProductTypeCheckBoxList.SelectedValue, _fgReport, this.FilterDropDownList.SelectedValue, this.ProductFromTextBox.Text, this.ProductToTextBox.Text);

                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + this.HeaderReportList1.SelectedValue;

                //if (this.GroupByDDL.SelectedValue == "0")
                //{
                //    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
                //}
                //else if (this.GroupByDDL.SelectedValue == "1")
                //{
                //    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
                //}
                //else if (this.GroupByDDL.SelectedValue == "2")
                //{
                //    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath2;
                //}
                //else if (this.GroupByDDL.SelectedValue == "3")
                //{
                //    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath3;
                //}

                this.ReportViewer1.DataBind();

                ReportParameter[] _reportParam = new ReportParameter[13];
                _reportParam[0] = new ReportParameter("Start", _start, true);
                _reportParam[1] = new ReportParameter("End", _end, true);
                _reportParam[2] = new ReportParameter("Str1", this.ProductGroupCheckBoxList.SelectedValue, true);
                _reportParam[3] = new ReportParameter("Str2", this.ProductTypeCheckBoxList.SelectedValue, true);
                _reportParam[4] = new ReportParameter("Str3", "", false);
                _reportParam[5] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                _reportParam[6] = new ReportParameter("FgReport", _fgReport, true);
                _reportParam[7] = new ReportParameter("FgAmount", "0", true);
                _reportParam[8] = new ReportParameter("FgPriceType", "0", true);
                _reportParam[9] = new ReportParameter("FgWarehouse", Convert.ToInt32(!this.WarehouseCheckBox.Checked).ToString(), true);
                _reportParam[10] = new ReportParameter("FgFilter", this.FilterDropDownList.SelectedValue, true);
                _reportParam[11] = new ReportParameter("FromProduct", this.ProductFromTextBox.Text, true);
                _reportParam[12] = new ReportParameter("ToProduct", this.ProductToTextBox.Text, true);

                this.ReportViewer1.LocalReport.SetParameters(_reportParam);
                this.ReportViewer1.LocalReport.Refresh();
            }
            else
            {
                this.WarningLabel.Text = "Diffrence Between Start and End Period Must Not Greater Than 12 Months";
            }
        }
        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowProductGroup();
            this.ShowProductType();

            this.ClearData();
        }

        protected void SearchImageButton_Click(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.ClearLabel();

            this.ShowProductType();
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }
        protected void FilterDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            if (this.FilterDropDownList.SelectedValue == "0")
            {
                this.RangePanel.Visible = true;
                this.SelectionPanel.Visible = false;
            }
            else
            {
                this.RangePanel.Visible = false;
                this.SelectionPanel.Visible = true;
            }
        }
    }
}