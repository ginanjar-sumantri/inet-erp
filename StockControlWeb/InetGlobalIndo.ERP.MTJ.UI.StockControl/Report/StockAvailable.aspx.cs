using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Report
{
    public partial class StockAvailable : ReportBase
    {
        private ReportStockControlBL _report = new ReportStockControlBL();
        private PermissionBL _permBL = new PermissionBL();
        private ProductBL _productBL = new ProductBL();
        private WarehouseBL _wrhsBL = new WarehouseBL();

        private string _reportPath0 = "Report/StockAvailablePerProduct.rdlc";
        private string _reportPath1 = "Report/StockAvailablePerProductGroup.rdlc";
        private string _reportPath2 = "Report/StockAvailablePerProductSubGroup.rdlc";
        private string _reportPath3 = "Report/StockAvailablePerProductType.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDStockAvailable, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

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

                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = "Stock Available Report";

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
            this.StartDateTextBox.Attributes.Add("ReadOnly", "True");
        }

        public void ClearData()
        {
            this.StartDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.FgReportDropDownList.SelectedValue = "null";
            this.ProductSubGrpCheckBoxList.ClearSelection();
            this.ProductTypeCheckBoxList.ClearSelection();
            this.MinRadioButtonList.SelectedValue = "0";
            this.MaxRadioButtonList.SelectedValue = "0";
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

            string _productType = "";
            string _productSubGrpCode = "";

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

            ReportDataSource _reportDataSource1 = this._report.StockAvailable(_startDateTime, _productSubGrpCode, _productType, Convert.ToInt32(this.FgReportDropDownList.SelectedValue), Convert.ToInt32(this.MinRadioButtonList.SelectedValue), Convert.ToInt32(MaxRadioButtonList.SelectedValue));

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
            _reportParam[0] = new ReportParameter("End", _startDateTime.ToString(), true);
            _reportParam[1] = new ReportParameter("Str1", _productType, true);
            _reportParam[2] = new ReportParameter("Str2", _productSubGrpCode, false);
            _reportParam[3] = new ReportParameter("Str3", "", true);
            _reportParam[4] = new ReportParameter("FgMin", this.MinRadioButtonList.SelectedValue, true);
            _reportParam[5] = new ReportParameter("FgMax", this.MaxRadioButtonList.SelectedValue, true);
            _reportParam[6] = new ReportParameter("FgReport", this.FgReportDropDownList.SelectedValue, true);
            _reportParam[7] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);

            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}
