using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Report
{
    public partial class StockActivitiesMonthly : ReportBase
    {
        private ReportStockControlBL _report = new ReportStockControlBL();
        private PermissionBL _permBL = new PermissionBL();
        private ProductBL _productBL = new ProductBL();
        private BillOfLadingBL _bolBL = new BillOfLadingBL();

        String _bolReferenceType = "";

        private string _reportPath0 = "Report/StockActivitiesMonthlyPerProduct.rdlc";
        private string _reportPath1 = "Report/StockActivitiesMonthlyPerProductGroup.rdlc";
        private string _reportPath2 = "Report/StockActivitiesMonthlyPerProductSubGroup.rdlc";
        private string _reportPath3 = "Report/StockActivitiesMonthlyPerProductType.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDStockActivitiesMonthly, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

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
                this.PageTitleLiteral.Text = "Stock Activities Monthly Report";

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                _bolReferenceType = _bolBL.GetSingleBOLReferenceType();

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
            //this.FgReportDropDownList.SelectedValue = "null";
            this.WrhsCheckBox.Checked = false;
            this.ProductTypeCheckBoxList.ClearSelection();
            this.ProductSubGrpCheckBoxList.ClearSelection();
            this.FgDivideDropDownList.SelectedValue = "1";
            this.FgDivideDropDownList.Enabled = false;
            this.FgQtyRadioButtonList.SelectedValue = "Y";
            this.HeaderReportList1.ReportGroup = "StockActivityMonth";
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

                if (this.ProductFromTextBox.Text == "")
                    this.ProductFromTextBox.Text = "000";
                if (this.ProductToTextBox.Text == "")
                    this.ProductToTextBox.Text = "zzz";

                ReportDataSource _reportDataSource1 = this._report.StockActivitiesMonthly(_start, _end, _productType, _productSubGrpCode, Convert.ToInt32(this.HeaderReportList1.SelectedIndex), Convert.ToInt32(!this.WrhsCheckBox.Checked), Convert.ToChar(this.FgQtyRadioButtonList.SelectedValue), Convert.ToDecimal(this.FgDivideDropDownList.SelectedValue), this.FilterDropDownList.SelectedValue, this.ProductFromTextBox.Text, this.ProductToTextBox.Text);
                
                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + this.HeaderReportList1.SelectedValue;

                //if (this.FgReportDropDownList.SelectedValue == "0")
                //{
                //    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
                //}
                //else if (this.FgReportDropDownList.SelectedValue == "1")
                //{
                //    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
                //}
                //else if (this.FgReportDropDownList.SelectedValue == "2")
                //{
                //    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath2;
                //}
                //else if (this.FgReportDropDownList.SelectedValue == "3")
                //{
                //    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath3;
                //}

                this.ReportViewer1.DataBind();

                ReportParameter[] _reportParam = new ReportParameter[13];
                _reportParam[0] = new ReportParameter("Start", _start, true);
                _reportParam[1] = new ReportParameter("End", _end, true);
                _reportParam[2] = new ReportParameter("Str1", _productType, true);
                _reportParam[3] = new ReportParameter("Str2", _productSubGrpCode, false);
                _reportParam[4] = new ReportParameter("Str3", "", true);
                _reportParam[5] = new ReportParameter("FgReport", this.HeaderReportList1.SelectedIndex, true);
                _reportParam[6] = new ReportParameter("FgDetail", Convert.ToInt16(!this.WrhsCheckBox.Checked).ToString(), true);
                _reportParam[7] = new ReportParameter("FgQty", this.FgQtyRadioButtonList.SelectedValue, true);
                _reportParam[8] = new ReportParameter("FgDivide", this.FgDivideDropDownList.SelectedValue, true);
                _reportParam[9] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
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
            this.ClearData();
        }

        protected void FgQtyRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.FgQtyRadioButtonList.SelectedValue == "Y")
            {
                this.FgDivideDropDownList.Enabled = false;
            }
            else if (this.FgQtyRadioButtonList.SelectedValue == "N")
            {
                this.FgDivideDropDownList.Enabled = true;
            }
        }
        protected void FilterDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearData();

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
