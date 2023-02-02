using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.Report
{
    public partial class SalesProfitSumPerProductGrp : ReportBase
    {
        private ReportFinanceBL _reportBL = new ReportFinanceBL();
        private PermissionBL _permBL = new PermissionBL();
        private ProductBL _productBL = new ProductBL();

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);

        private string _currPageKey = "CurrentPage";

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_PayTypeCheckBoxList_";
        private string _akhir = "";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private int?[] _navMark2 = { null, null, null, null };
        private bool _flag2 = true;
        private bool _nextFlag2 = false;
        private bool _lastFlag2 = false;

        private int _maxrow2 = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength2 = Convert.ToInt32(ApplicationConfig.DataPagerRange);

        private string _currPageKey2 = "CurrentPage";

        private string _awal2 = "ctl00_DefaultBodyContentPlaceHolder_SuppCodeCheckBoxList_";
        private string _akhir2 = "";
        private string _cbox2 = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox2";
        private string _tempHidden2 = "ctl00$DefaultBodyContentPlaceHolder$TempHidden2";

        private string _reportPath0 = "Report/SalesProfitSumPerProductGrp.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDSalesProfitSummaryPerProductGroup, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer1.Attributes.Add("style", "margin-bottom: 30px;");

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = "Sales Profit Summary Per Product Group";

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.MenuPanel.Visible = true;
                this.ReportViewer1.Visible = false;

                this.ShowProductGroup();

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

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearData()
        {
            this.ClearLabel();
            this.StartPeriodTextBox.Text = "";
            this.StartYearTextBox.Text = "";

            this.EndPeriodTextBox.Text = "";
            this.EndYearTextBox.Text = "";

            this.DivideByDropDownList.SelectedValue = "0";
        }

        protected void ShowProductGroup()
        {
            this.ProdGroupCheckBoxList.ClearSelection();
            this.ProdGroupCheckBoxList.Items.Clear();
            this.ProdGroupCheckBoxList.DataSource = this._productBL.GetListForDDL();
            this.ProdGroupCheckBoxList.DataValueField = "ProductGrpCode";
            this.ProdGroupCheckBoxList.DataTextField = "ProductGrpName";
            this.ProdGroupCheckBoxList.DataBind();
        }

        private bool ValidateDiffDate()
        {
            bool _result = false;

            DateTime _start = Convert.ToDateTime(this.StartYearTextBox.Text + "-" + this.StartPeriodTextBox.Text + "-01");
            DateTime _end = Convert.ToDateTime(this.EndYearTextBox.Text + "-" + this.EndPeriodTextBox.Text + "-01");

            for (int i = 0; i <= 11; i++)
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

                string _productGroupCode = "";

                var _hasilProductGroup = this._productBL.GetListForDDL();

                for (var i = 0; i < _hasilProductGroup.Count; i++)
                {
                    if (this.ProdGroupCheckBoxList.Items[i].Selected == true)
                    {
                        if (_productGroupCode == "")
                        {
                            _productGroupCode += this.ProdGroupCheckBoxList.Items[i].Value;
                        }
                        else
                        {
                            _productGroupCode += "," + this.ProdGroupCheckBoxList.Items[i].Value;
                        }
                    }
                }

                string _kosong = "";

                ReportDataSource _reportDataSource1 = this._reportBL.SalesProfitSumPerProductGroup(_start, _end, _productGroupCode, _kosong, this.DivideByDropDownList.SelectedValue);

                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
                this.ReportViewer1.DataBind();

                ReportParameter[] _reportParam = new ReportParameter[6];
                _reportParam[0] = new ReportParameter("Start", _start, true);
                _reportParam[1] = new ReportParameter("End", _end, true);
                _reportParam[2] = new ReportParameter("Str1", _productGroupCode, true);
                _reportParam[3] = new ReportParameter("Str2", "", true);
                _reportParam[4] = new ReportParameter("Str3", this.DivideByDropDownList.SelectedValue, true);
                _reportParam[5] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);

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