using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.Report
{
    public partial class IncomeGraph : ReportBase
    {
        private ReportFinanceBL _reportBL = new ReportFinanceBL();
        private CustomerBL _custBL = new CustomerBL();
        private CurrencyBL _currBL = new CurrencyBL();
        private PermissionBL _permBL = new PermissionBL();


        private int?[] _navMark = { null, null, null, null };

        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);

        private string _currPageKey = "CurrentPage";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDARListing, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer.Attributes.Add("style", "margin-bottom: 30px;");


            if (!this.Page.IsPostBack == true)
            {
                //this.StartDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.StartDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                //this.EndDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.EndDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = "Income Graph Per Year";

                this.ViewState[this._currPageKey] = 0;

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetAttribute();

                this.MenuPanel.Visible = true;
                this.ReportViewer.Visible = false;

                this.ShowCurrency();
                this.ShowCustomerGroup();
                this.ShowCustomerType();

                this.ClearData();

                this.ShowCustomer(0);
            }
        }

        private void SetAttribute()
        {
            //this.StartDateTextBox.Attributes.Add("ReadOnly", "True");
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowCurrency()
        {
            this.CurrCodeCheckBoxList.DataTextField = "CurrCode";
            this.CurrCodeCheckBoxList.DataValueField = "CurrCode";
            this.CurrCodeCheckBoxList.DataSource = this._currBL.GetListAll();
            this.CurrCodeCheckBoxList.DataBind();
        }

        private void ShowCustomer(Int32 _prmCurrentPage)
        {
            string _tempCustomerGroup = (this.CustomerGroupDropDownList.SelectedValue == "null") ? "" : this.CustomerGroupDropDownList.SelectedValue;
            string _tempCustomerType = (this.CustomerTypeDropDownList.SelectedValue == "null") ? "" : this.CustomerTypeDropDownList.SelectedValue;
        }

        protected void ShowCustomerGroup()
        {
            this.CustomerGroupDropDownList.Items.Clear();
            this.CustomerGroupDropDownList.DataSource = this._custBL.GetListCustGroupForDDL();
            this.CustomerGroupDropDownList.DataValueField = "CustGroupCode";
            this.CustomerGroupDropDownList.DataTextField = "CustGroupName";
            this.CustomerGroupDropDownList.DataBind();
            this.CustomerGroupDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowCustomerType()
        {
            this.CustomerTypeDropDownList.Items.Clear();
            this.CustomerTypeDropDownList.DataSource = this._custBL.GetListCustTypeForDDL();
            this.CustomerTypeDropDownList.DataValueField = "CustTypeCode";
            this.CustomerTypeDropDownList.DataTextField = "CustTypeName";
            this.CustomerTypeDropDownList.DataBind();
            this.CustomerTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void CustomerGroupDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowCustomer(0);
        }

        protected void CustomerTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowCustomer(0);
        }

        public void ClearData()
        {
            DateTime now = DateTime.Now;
            this.ClearLabel();

            this.YearTextBox.Text = "";
            this.HeaderReportList1.ReportGroup = "InComeGraphPerYear";
            this.CustomerGroupDropDownList.SelectedValue = "null";
            this.CustomerTypeDropDownList.SelectedValue = "null";
            this.CurrCodeCheckBoxList.ClearSelection();
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = false;
            this.ReportViewer.Visible = true;

            string _tempCustGroup = (this.CustomerGroupDropDownList.SelectedValue == "null") ? "" : this.CustomerGroupDropDownList.SelectedValue;
            string _tempCustType = (this.CustomerTypeDropDownList.SelectedValue == "null") ? "" : this.CustomerTypeDropDownList.SelectedValue;

            string _currCode = "";

            var hasilCurr = this._currBL.GetListAll();

            for (var i = 0; i < hasilCurr.Count(); i++)
            {
                if (this.CurrCodeCheckBoxList.Items[i].Selected == true)
                {
                    if (_currCode == "")
                    {
                        _currCode += this.CurrCodeCheckBoxList.Items[i].Value;
                    }
                    else
                    {
                        _currCode += "," + this.CurrCodeCheckBoxList.Items[i].Value;
                    }
                }
            }


            ReportDataSource _reportDataSource1 = this._reportBL.IncomeGraph(Convert.ToInt32(this.YearTextBox.Text), _tempCustGroup, _tempCustType, _currCode);

            this.ReportViewer.LocalReport.DataSources.Clear();
            this.ReportViewer.LocalReport.DataSources.Add(_reportDataSource1);

            this.ReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + this.HeaderReportList1.SelectedValue;


            this.ReportViewer.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[5];
            _reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            _reportParam[0] = new ReportParameter("Year", this.YearTextBox.Text, true);
            _reportParam[2] = new ReportParameter("CustGroup", _tempCustGroup, true);
            _reportParam[3] = new ReportParameter("CustType", _tempCustType, true);
            _reportParam[4] = new ReportParameter("Currency", _currCode, true);

            this.ReportViewer.LocalReport.SetParameters(_reportParam);
            this.ReportViewer.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();

            this.ShowCustomer(0);
        }

    }
}