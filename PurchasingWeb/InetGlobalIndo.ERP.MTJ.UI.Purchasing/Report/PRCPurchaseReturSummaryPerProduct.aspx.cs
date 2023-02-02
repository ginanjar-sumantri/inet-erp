using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.Report
{
    public partial class PRCPurchaseReturSummaryPerProduct : ReportBase
    {
        private ReportPurchaseBL _report = new ReportPurchaseBL();
        private PermissionBL _permBL = new PermissionBL();
        private ProductBL _productBL = new ProductBL();
        private SupplierBL _suppBL = new SupplierBL();

        private string _reportPath0 = "Report/PRCPurchaseReturSummaryPerProductProd.rdlc";
        private string _reportPath1 = "Report/PRCPurchaseReturSummaryPerProductProdGroup.rdlc";
        private string _reportPath2 = "Report/PRCPurchaseReturSummaryPerProductProdSubGroup.rdlc";
        private string _reportPath3 = "Report/PRCPurchaseReturSummaryPerProductProdType.rdlc";

        private int?[] _navMark = { null, null, null, null };
        //private bool _flag = true;
        //private bool _nextFlag = false;
        //private bool _lastFlag = false;

        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);

        private string _currPageKey = "CurrentPage";

        //private string _awal = "ctl00_DefaultBodyContentPlaceHolder_SuppCodeCheckBoxList_";
        //private string _akhir = "";
        //private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        //private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDPurchaseReturSummaryPerProduct, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

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
                this.PageTitleLiteral.Text = "Purchase Retur Summary Per Product Report";

                this.ViewState[this._currPageKey] = 0;

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.MenuPanel.Visible = true;
                this.ReportViewer1.Visible = false;

                this.ShowProductSubGrp();
                this.ShowProductType();

                this.SetAttribute();
                this.ClearData();
            }
        }

        public void SetAttribute()
        {
            this.StartPeriodTextBox.Attributes.Add("OnBlur", " ValidatePeriod(" + this.StartPeriodTextBox.ClientID + ");");
            this.StartYearTextBox.Attributes.Add("OnBlur", " ValidateYear(" + this.StartYearTextBox.ClientID + ");");

            this.EndPeriodTextBox.Attributes.Add("OnBlur", " ValidatePeriod(" + this.EndPeriodTextBox.ClientID + ");");
            this.EndYearTextBox.Attributes.Add("OnBlur", " ValidateYear(" + this.EndYearTextBox.ClientID + ");");
        }

        public void ClearData()
        {
            this.StartPeriodTextBox.Text = "";
            this.StartYearTextBox.Text = "";
            this.EndPeriodTextBox.Text = "";
            this.EndYearTextBox.Text = "";
            this.FgReportDropDownList.SelectedValue = "null";
            this.WrhsCheckBox.Checked = false;
            this.ProductTypeCheckBoxList.ClearSelection();
            this.ProductSubGrpCheckBoxList.ClearSelection();
        }

        private void ShowProductSubGrp()
        {
            this.ProductSubGrpCheckBoxList.ClearSelection();
            this.ProductSubGrpCheckBoxList.Items.Clear();
            this.ProductSubGrpCheckBoxList.DataTextField = "ProductSubGrpName";
            this.ProductSubGrpCheckBoxList.DataValueField = "ProductSubGrpCode";
            this.ProductSubGrpCheckBoxList.DataSource = this._productBL.GetListProductSubGroupForDDL();
            this.ProductSubGrpCheckBoxList.DataBind();
        }

        private void ShowProductType()
        {
            this.ProductTypeCheckBoxList.ClearSelection();
            this.ProductTypeCheckBoxList.Items.Clear();
            this.ProductTypeCheckBoxList.DataTextField = "ProductTypeName";
            this.ProductTypeCheckBoxList.DataValueField = "ProductTypeCode";
            this.ProductTypeCheckBoxList.DataSource = this._productBL.GetListProductTypeForDDL();
            this.ProductTypeCheckBoxList.DataBind();
        }

        private bool ValidateDiffDate()
        {
            bool _result = false;

            DateTime _start = Convert.ToDateTime(this.StartYearTextBox.Text + "-" + this.StartPeriodTextBox.Text + "-01");
            DateTime _end = Convert.ToDateTime(this.EndYearTextBox.Text + "-" + this.EndPeriodTextBox.Text + "-01");

            for (int i = 0; i < 11; i++)
            {
                _start = _start.AddMonths(1);

                if (_start == _end)
                {
                    _result = true;
                    break;
                }
            }

            return _result;
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ValidateDiffDate() == true)
            {
                this.MenuPanel.Visible = false;
                this.ReportViewer1.Visible = true;

                string _start = this.StartYearTextBox.Text.PadLeft(4, '0') + this.StartPeriodTextBox.Text.PadLeft(2, '0');
                string _end = this.EndYearTextBox.Text.PadLeft(4, '0') + this.EndPeriodTextBox.Text.PadLeft(2, '0');

                string _productSubGrpCode = "";
                string _productType = "";

                var _hasilProductSubGrp = this._productBL.GetListProductSubGroupForDDL();

                for (var i = 0; i < _hasilProductSubGrp.Count(); i++)
                {
                    if (this.ProductSubGrpCheckBoxList.Items[i].Selected == true)
                    {
                        if (_productSubGrpCode == "")
                        {
                            _productSubGrpCode += this.ProductSubGrpCheckBoxList.Items[i].Value;
                        }
                        else
                        {
                            _productSubGrpCode += "," + this.ProductSubGrpCheckBoxList.Items[i].Value;
                        }
                    }
                }

                var _hasilProductType = this._productBL.GetListProductTypeForDDL();

                for (var i = 0; i < _hasilProductType.Count(); i++)
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

                ReportDataSource _reportDataSource1 = this._report.PurchaseReturSummaryPerProduct(_start, _end, _productType, _productSubGrpCode, Convert.ToInt32(this.FgReportDropDownList.SelectedValue));

                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

                if (this.FgReportDropDownList.SelectedValue == "0")
                {
                    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
                }
                else if (this.FgReportDropDownList.SelectedValue == "1")
                {
                    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
                }
                else if (this.FgReportDropDownList.SelectedValue == "2")
                {
                    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath2;
                }
                else if (this.FgReportDropDownList.SelectedValue == "3")
                {
                    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath3;
                }

                this.ReportViewer1.DataBind();

                ReportParameter[] _reportParam = new ReportParameter[8];
                _reportParam[0] = new ReportParameter("Start", _start, true);
                _reportParam[1] = new ReportParameter("End", _end, true);
                _reportParam[2] = new ReportParameter("Str1", _productType, true);
                _reportParam[3] = new ReportParameter("Str2", _productSubGrpCode, false);
                _reportParam[4] = new ReportParameter("Str3", Convert.ToInt16(this.WrhsCheckBox.Checked).ToString(), true);
                _reportParam[5] = new ReportParameter("FgReport", this.FgReportDropDownList.SelectedValue, true);
                _reportParam[6] = new ReportParameter("FgAmount", "0", true);
                _reportParam[7] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);

                this.ReportViewer1.LocalReport.SetParameters(_reportParam);
                this.ReportViewer1.LocalReport.Refresh();
            }
            else
            {
                this.WarningLabel.Text = "Deifference Between Start and End Period Must Not Greater Than 12 Months";
            }
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}
