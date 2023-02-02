using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Report
{
    public partial class StockActivitiesListing : ReportBase
    {
        private ReportStockControlBL _report = new ReportStockControlBL();
        private PermissionBL _permBL = new PermissionBL();
        private ProductBL _productBL = new ProductBL();
        private WarehouseBL _wrhsBL = new WarehouseBL();
        private BillOfLadingBL _bolBL = new BillOfLadingBL();

        String _bolReferenceType = "";

        private string _reportPath0 = "Report/StockActivitiesListingPerProduct.rdlc";
        private string _reportPath1 = "Report/StockActivitiesListingPerProductGroup.rdlc";
        private string _reportPath2 = "Report/StockActivitiesListingPerProductSubGroup.rdlc";
        private string _reportPath3 = "Report/StockActivitiesListingPerProductType.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDStockActivitiesListing, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer1.Attributes.Add("style", "margin-bottom: 30px;");

            if (!Page.IsPostBack == true)
            {
                this.StartDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.StartDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.EndDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.EndDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = "Stock Activities Listing Report";

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
            this.StartDateTextBox.Attributes.Add("ReadOnly", "True");
            this.EndDateTextBox.Attributes.Add("ReadOnly", "True");
        }

        public void ClearData()
        {
            this.StartDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.EndDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            //this.FgReportDropDownList.SelectedValue = "null";
            //this.FgDivideDropDownList.SelectedValue = "1";
            this.FgDivideDropDownList.Enabled = false;
            this.FgQtyRadioButtonList.SelectedValue = "Y";
            this.WrhsCheckBox.Checked = false;
            this.ProductSubGrpCheckBoxList.ClearSelection();
            this.ProductTypeCheckBoxList.ClearSelection();
            this.HeaderReportList1.ReportGroup = "StockActivityList";
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

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = false;
            this.ReportViewer1.Visible = true;

            DateTime _startDateTime = new DateTime(DateFormMapper.GetValue(this.StartDateTextBox.Text).Year, DateFormMapper.GetValue(this.StartDateTextBox.Text).Month, DateFormMapper.GetValue(this.StartDateTextBox.Text).Day, 0, 0, 0);
            DateTime _endDateTime = new DateTime(DateFormMapper.GetValue(this.EndDateTextBox.Text).Year, DateFormMapper.GetValue(this.EndDateTextBox.Text).Month, DateFormMapper.GetValue(this.EndDateTextBox.Text).Day, 23, 59, 59);

            string _productType = "";
            string _productSubGrpCode = "";

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

            if (this.ProductFromTextBox.Text == "")
                this.ProductFromTextBox.Text = "000";
            if (this.ProductToTextBox.Text == "")
                this.ProductToTextBox.Text = "zzz";

            ReportDataSource _reportDataSource1 = this._report.StockActivitiesListing(_startDateTime, _endDateTime, _productType, _productSubGrpCode, Convert.ToInt32(this.HeaderReportList1.SelectedIndex), Convert.ToInt32(!this.WrhsCheckBox.Checked), Convert.ToChar(this.FgQtyRadioButtonList.SelectedValue), Convert.ToDecimal(this.FgDivideDropDownList.SelectedValue), this.FilterDropDownList.SelectedValue, this.ProductFromTextBox.Text, this.ProductToTextBox.Text);

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
            _reportParam[0] = new ReportParameter("Start", _startDateTime.ToString(), true);
            _reportParam[1] = new ReportParameter("End", _endDateTime.ToString(), true);
            _reportParam[2] = new ReportParameter("Str1", _productType, true);
            _reportParam[3] = new ReportParameter("Str2", _productSubGrpCode, false);
            _reportParam[4] = new ReportParameter("Str3", "", true);
            _reportParam[5] = new ReportParameter("FgReport", this.HeaderReportList1.SelectedIndex, true);
            _reportParam[6] = new ReportParameter("FgDetail", Convert.ToInt32(!this.WrhsCheckBox.Checked).ToString(), true);
            _reportParam[7] = new ReportParameter("FgQty", this.FgQtyRadioButtonList.SelectedValue, true);
            _reportParam[8] = new ReportParameter("FgDivide", this.FgDivideDropDownList.SelectedValue, true);
            _reportParam[9] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            _reportParam[10] = new ReportParameter("FgFilter", this.FilterDropDownList.SelectedValue, true);
            _reportParam[11] = new ReportParameter("FromProduct", this.ProductFromTextBox.Text, true);
            _reportParam[12] = new ReportParameter("ToProduct", this.ProductToTextBox.Text, true);

            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
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
