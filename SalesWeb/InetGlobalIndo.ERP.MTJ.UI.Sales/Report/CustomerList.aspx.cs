using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.Report
{
    public partial class CustomerList : ReportBase
    {
        private ReportSalesBL _reportBL = new ReportSalesBL();
        private CustomerBL _custBL = new CustomerBL();
        private CityBL _cityBL = new CityBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _reportPath0 = "Report/CustomerList.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDCustomerList, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer.Attributes.Add("style", "margin-bottom: 30px;");

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = "Customer List Report";

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.MenuPanel.Visible = true;
                this.ReportViewer.Visible = false;

                this.ShowCustGroup();
                this.ShowCustType();
                this.ShowCity();

                this.ClearData();
            }
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ShowCustGroup()
        {
            this.CustGroupCheckBoxList.ClearSelection();
            this.CustGroupCheckBoxList.Items.Clear();
            this.CustGroupCheckBoxList.DataSource = this._custBL.GetListCustGroupForDDL();
            this.CustGroupCheckBoxList.DataValueField = "CustGroupCode";
            this.CustGroupCheckBoxList.DataTextField = "CustGroupName";
            this.CustGroupCheckBoxList.DataBind();
        }

        protected void ShowCustType()
        {
            this.CustTypeCheckBoxList.ClearSelection();
            this.CustTypeCheckBoxList.Items.Clear();
            this.CustTypeCheckBoxList.DataSource = this._custBL.GetListCustTypeForDDL();
            this.CustTypeCheckBoxList.DataValueField = "CustTypeCode";
            this.CustTypeCheckBoxList.DataTextField = "CustTypeName";
            this.CustTypeCheckBoxList.DataBind();
        }

        protected void ShowCity()
        {
            this.CityCheckBoxList.ClearSelection();
            this.CityCheckBoxList.Items.Clear();
            this.CityCheckBoxList.DataSource = this._cityBL.GetListCityForDDL();
            this.CityCheckBoxList.DataValueField = "CityCode";
            this.CityCheckBoxList.DataTextField = "CityName";
            this.CityCheckBoxList.DataBind();
        }

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ClearLabel();

            this.CityCheckBoxList.ClearSelection();
            this.CustGroupCheckBoxList.ClearSelection();
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = false;
            this.ReportViewer.Visible = true;

            string _custGroupCode = "";
            string _custTypeCode = "";
            string _cityCode = "";

            var _hasilCustGroup = this._custBL.GetListCustGroupForDDL();
            var _hasilCustType = this._custBL.GetListCustTypeForDDL();
            var _hasilCity = this._cityBL.GetListCityForDDL();

            for (var i = 0; i < _hasilCustGroup.Count(); i++)
            {
                if (this.CustGroupCheckBoxList.Items[i].Selected == true)
                {
                    if (_custGroupCode == "")
                    {
                        _custGroupCode += this.CustGroupCheckBoxList.Items[i].Value;
                    }
                    else
                    {
                        _custGroupCode += "," + this.CustGroupCheckBoxList.Items[i].Value;
                    }
                }
            }

            for (var i = 0; i < _hasilCustType.Count(); i++)
            {
                if (this.CustTypeCheckBoxList.Items[i].Selected == true)
                {
                    if (_custTypeCode == "")
                    {
                        _custTypeCode += this.CustTypeCheckBoxList.Items[i].Value;
                    }
                    else
                    {
                        _custTypeCode += "," + this.CustTypeCheckBoxList.Items[i].Value;
                    }
                }
            }

            for (var i = 0; i < _hasilCity.Count(); i++)
            {
                if (this.CityCheckBoxList.Items[i].Selected == true)
                {
                    if (_cityCode == "")
                    {
                        _cityCode += this.CityCheckBoxList.Items[i].Value;
                    }
                    else
                    {
                        _cityCode += "," + this.CityCheckBoxList.Items[i].Value;
                    }
                }
            }

            ReportDataSource _reportDataSource1 = this._reportBL.CustomerList(_custGroupCode, _custTypeCode, _cityCode, Convert.ToInt32(this.CustGroupTypeDropDownList.SelectedValue));

            this.ReportViewer.LocalReport.DataSources.Clear();
            this.ReportViewer.LocalReport.DataSources.Add(_reportDataSource1);

            this.ReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;

            this.ReportViewer.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[6];
            _reportParam[0] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            _reportParam[1] = new ReportParameter("StrGroup", _custGroupCode, true);
            _reportParam[2] = new ReportParameter("StrCity", _cityCode, true);
            _reportParam[3] = new ReportParameter("Str1", "0", true);
            _reportParam[4] = new ReportParameter("FgExport", this.CustGroupTypeDropDownList.SelectedValue, true);
            _reportParam[5] = new ReportParameter("StrType", _custTypeCode, true);

            this.ReportViewer.LocalReport.SetParameters(_reportParam);
            this.ReportViewer.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}