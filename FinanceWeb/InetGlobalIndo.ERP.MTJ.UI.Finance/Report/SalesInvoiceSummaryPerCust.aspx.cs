using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Collections.Generic;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.Report
{
    public partial class SalesInvoiceSummaryPerCust : ReportBase
    {
        private ReportFinanceBL _report = new ReportFinanceBL();
        private PermissionBL _permBL = new PermissionBL();
        private CustomerBL _custBL = new CustomerBL();

        private string _reportPath0 = "Report/SalesInvoiceSummaryPerCust.rdlc";

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);

        private string _currPageKey = "CurrentPage";

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_CustCodeCheckBoxList_";
        private string _akhir = "";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDSalesInvoiceSummaryPerCustomer, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer1.Attributes.Add("style", "margin-bottom: 30px;");

            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");

            if (!Page.IsPostBack == true)
            {
                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = "Sales Invoice Summary Per Customer Report";

                this.ViewState[this._currPageKey] = 0;

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.GoImageButton1.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/go.jpg";

                this.MenuPanel.Visible = true;
                this.ReportViewer1.Visible = false;

                this.ShowCustGroup();
                this.ShowCustType();

                this.SetAttribute();
                this.ClearData();

                this.ShowCustomer(0);
                this.SetCheckBox();
            }

            if (this.HeaderReportList1.SelectedIndex == "3")
            {
                this.StartPeriodTextBox.Visible = false;
                this.StartPeriodRequiredFieldValidator.Visible = false;
                this.EndPeriodTextBox.Visible = false;
                this.EndPeriodRequiredFieldValidator.Visible = false;
            }
            else 
            {
                this.StartPeriodTextBox.Visible = true;
                this.StartPeriodRequiredFieldValidator.Visible = true;
                this.EndPeriodTextBox.Visible = true;
                this.EndPeriodRequiredFieldValidator.Visible = true;
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
            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";

            this.StartPeriodTextBox.Text = "";
            this.StartYearTextBox.Text = "";
            this.EndPeriodTextBox.Text = "";
            this.EndYearTextBox.Text = "";
            this.CustCodeCheckBoxList.ClearSelection();
            this.GroupByDropDownList.SelectedValue = "0";
            this.CustGroupDropDownList.SelectedValue = "null";
            this.CustTypeDropDownList.SelectedValue = "null";
            this.DivideByDropDownList.SelectedValue = "3";
            this.AllCheckBox.Checked = false;
            this.HeaderReportList1.ReportGroup = "SalesInvSummPerCust";
        }

        private void ShowCustomer(Int32 _prmCurrentPage)
        {
            string _tempCustGroup = (this.CustGroupDropDownList.SelectedValue == "null") ? "" : this.CustGroupDropDownList.SelectedValue;
            string _tempCustType = (this.CustTypeDropDownList.SelectedValue == "null") ? "" : this.CustTypeDropDownList.SelectedValue;

            this.CustCodeCheckBoxList.ClearSelection();
            this.CustCodeCheckBoxList.Items.Clear();
            this.CustCodeCheckBoxList.DataTextField = "CustName";
            this.CustCodeCheckBoxList.DataValueField = "CustCode";
            this.CustCodeCheckBoxList.DataSource = this._custBL.GetListDDLCustomer(_tempCustGroup, _tempCustType, this.CategoryDropDownList1.SelectedValue, this.KeywordTextBox1.Text, _prmCurrentPage, _maxrow);
            this.CustCodeCheckBoxList.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "', 'true');");

            int i = 0;
            foreach (ListItem _item in this.CustCodeCheckBoxList.Items)
            {
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _awal + i.ToString() + ", '" + _item.Value + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "', 'true');");
                i++;
            }

            List<MsCustomer> _listMsCustomer = this._custBL.GetListDDLCustomer(_tempCustGroup, _tempCustType);
            this.AllHidden.Value = "";

            foreach (MsCustomer _item in _listMsCustomer)
            {
                if (this.AllHidden.Value == "")
                {
                    this.AllHidden.Value = _item.CustCode;
                }
                else
                {
                    this.AllHidden.Value += "," + _item.CustCode;
                }
            }

            this.ShowPage(_prmCurrentPage);
        }

        protected void ShowCustGroup()
        {
            this.CustGroupDropDownList.Items.Clear();
            this.CustGroupDropDownList.DataSource = this._custBL.GetListCustGroupForDDL();
            this.CustGroupDropDownList.DataValueField = "CustGroupCode";
            this.CustGroupDropDownList.DataTextField = "CustGroupName";
            this.CustGroupDropDownList.DataBind();
            this.CustGroupDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowCustType()
        {
            this.CustTypeDropDownList.Items.Clear();
            this.CustTypeDropDownList.DataSource = this._custBL.GetListCustTypeForDDL();
            this.CustTypeDropDownList.DataValueField = "CustTypeCode";
            this.CustTypeDropDownList.DataTextField = "CustTypeName";
            this.CustTypeDropDownList.DataBind();
            this.CustTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void CustGroupDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";
            this.ShowCustomer(0);
            this.SetCheckBox();
        }

        protected void CustTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";
            this.ShowCustomer(0);
            this.SetCheckBox();
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

        private bool ValidateDiffYear()
        {
            bool _result = false;

            DateTime _start = Convert.ToDateTime(this.StartYearTextBox.Text);
            DateTime _end = Convert.ToDateTime(this.EndYearTextBox.Text);

            for (int i = 0; i <= 11; i++)
            {
                _start = _start.AddYears(1);

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
            bool _validateDiff = false;
            if (this.HeaderReportList1.SelectedIndex == "3")
            {
                if (this.ValidateDiffYear() == true)
                    _validateDiff = true;
            }
            else 
            {
                if (this.ValidateDiffDate() == true)
                _validateDiff = true;
            }
            
            if (_validateDiff == true)
            {
                this.MenuPanel.Visible = false;
                this.ReportViewer1.Visible = true;

                string _start = this.StartYearTextBox.Text.PadLeft(4, '0') + this.StartPeriodTextBox.Text.PadLeft(2, '0');
                string _end = this.EndYearTextBox.Text.PadLeft(4, '0') + this.EndPeriodTextBox.Text.PadLeft(2, '0');

                string _custCode = "";

                if (this.GrabAllCheckBox.Checked == true)
                {
                    _custCode = this.AllHidden.Value;
                }
                else
                {
                    if (this.AllCheckBox.Checked == true)
                    {
                        _custCode = this.TempHidden.Value;
                    }
                    else
                    {
                        _custCode = this.CheckHidden.Value;
                    }
                }

                if (this.SelectionDropDownList.SelectedValue == "0")
                {
                    if (this.CustCodeFromTextBox.Text == "")
                        this.CustCodeFromTextBox.Text = "000";
                    if (this.CustCodeToTextBox.Text == "")
                        this.CustCodeFromTextBox.Text = "zzz";                
                }

                if (this.HeaderReportList1.SelectedValue == "Report/SalesInvoiceSummaryPerCust.rdlc")////Sales Invoice Summary Per Customer
                {

                    ReportDataSource _reportDataSource1 = this._report.SalesInvoiceSummaryPerCustomer(_start, _end, _custCode, this.DivideByDropDownList.SelectedValue, Convert.ToInt32(this.GroupByDropDownList.SelectedValue), Convert.ToInt32(this.SelectionDropDownList.SelectedValue), this.CustCodeFromTextBox.Text, this.CustCodeToTextBox.Text);

                    this.ReportViewer1.LocalReport.DataSources.Clear();
                    this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
                    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + this.HeaderReportList1.SelectedValue;
                    this.ReportViewer1.DataBind();

                    ReportParameter[] _reportParam = new ReportParameter[10];
                    _reportParam[0] = new ReportParameter("Start", _start, true);
                    _reportParam[1] = new ReportParameter("End", _end, true);
                    _reportParam[2] = new ReportParameter("Str1", _custCode, true);
                    _reportParam[3] = new ReportParameter("Str2", "", false);
                    _reportParam[4] = new ReportParameter("Str3", this.DivideByDropDownList.SelectedValue, true);
                    _reportParam[5] = new ReportParameter("FgCurr", this.GroupByDropDownList.SelectedValue, true);
                    _reportParam[6] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                    _reportParam[7] = new ReportParameter("FgFilter", this.SelectionDropDownList.SelectedValue, true);
                    _reportParam[8] = new ReportParameter("FromCustomer", this.CustCodeFromTextBox.Text, false);
                    _reportParam[9] = new ReportParameter("ToCustomer", this.CustCodeToTextBox.Text, true);

                    this.ReportViewer1.LocalReport.SetParameters(_reportParam);
                    this.ReportViewer1.LocalReport.Refresh();
                }
                else if (this.HeaderReportList1.SelectedValue == "Report/SalesInvoiceSummaryPerCustomerProductProdAmount.rdlc" || this.HeaderReportList1.SelectedValue == "Report/SalesInvoiceSummaryPerCustomerProductProdQty.rdlc")//// per Month
                {
                    ReportDataSource _reportDataSource1 = this._report.SalesInvoiceSummaryPerCustomerByMonth(_start, _end, _custCode, this.DivideByDropDownList.SelectedValue, Convert.ToInt32(this.GroupByDropDownList.SelectedValue), Convert.ToInt32(this.SelectionDropDownList.SelectedValue), this.CustCodeFromTextBox.Text, this.CustCodeToTextBox.Text, "000", "zzz");

                    this.ReportViewer1.LocalReport.DataSources.Clear();
                    this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
                    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + this.HeaderReportList1.SelectedValue;
                    this.ReportViewer1.DataBind();

                    ReportParameter[] _reportParam = new ReportParameter[13];
                    _reportParam[0] = new ReportParameter("Start", _start, true);
                    _reportParam[1] = new ReportParameter("End", _end, true);
                    _reportParam[2] = new ReportParameter("Str1", _custCode, true);
                    _reportParam[3] = new ReportParameter("Str2", "", false);
                    _reportParam[4] = new ReportParameter("Str3", this.DivideByDropDownList.SelectedValue, true);
                    _reportParam[5] = new ReportParameter("FgCurr", this.GroupByDropDownList.SelectedValue, true);
                    _reportParam[6] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                    _reportParam[7] = new ReportParameter("FgFilter", this.SelectionDropDownList.SelectedValue, true);
                    _reportParam[8] = new ReportParameter("FromCustomer", this.CustCodeFromTextBox.Text, false);
                    _reportParam[9] = new ReportParameter("ToCustomer", this.CustCodeToTextBox.Text, true);
                    _reportParam[10] = new ReportParameter("FromProduct", "000", false);
                    _reportParam[11] = new ReportParameter("ToProduct", "zzz", true);
                    _reportParam[12] = new ReportParameter("FgReport", "4", true);

                    this.ReportViewer1.LocalReport.SetParameters(_reportParam);
                    this.ReportViewer1.LocalReport.Refresh();
                }
                else if (this.HeaderReportList1.SelectedValue == "Report/SalesInvoiceSummaryPerCustomerProductProdQtyYear.rdlc")////per Year
                {
                    ReportDataSource _reportDataSource1 = this._report.SalesInvoiceSummaryPerCustomerByYear(this.StartYearTextBox.Text , this.EndYearTextBox.Text, _custCode, this.DivideByDropDownList.SelectedValue, Convert.ToInt32(this.GroupByDropDownList.SelectedValue), Convert.ToInt32(this.SelectionDropDownList.SelectedValue), this.CustCodeFromTextBox.Text, this.CustCodeToTextBox.Text);

                    this.ReportViewer1.LocalReport.DataSources.Clear();
                    this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
                    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + this.HeaderReportList1.SelectedValue;
                    this.ReportViewer1.DataBind();

                    ReportParameter[] _reportParam = new ReportParameter[11];
                    _reportParam[0] = new ReportParameter("Start", this.StartYearTextBox.Text, true);
                    _reportParam[1] = new ReportParameter("End", this.EndYearTextBox.Text, true);
                    _reportParam[2] = new ReportParameter("Str1", _custCode, true);
                    _reportParam[3] = new ReportParameter("Str2", "", false);
                    _reportParam[4] = new ReportParameter("Str3", this.DivideByDropDownList.SelectedValue, true);
                    _reportParam[5] = new ReportParameter("FgCurr", this.GroupByDropDownList.SelectedValue, true);
                    _reportParam[6] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                    _reportParam[7] = new ReportParameter("FgFilter", this.SelectionDropDownList.SelectedValue, true);
                    _reportParam[8] = new ReportParameter("FromCustomer", this.CustCodeFromTextBox.Text, false);
                    _reportParam[9] = new ReportParameter("ToCustomer", this.CustCodeToTextBox.Text, true);
                    _reportParam[10] = new ReportParameter("FgReport", "0", true);

                    this.ReportViewer1.LocalReport.SetParameters(_reportParam);
                    this.ReportViewer1.LocalReport.Refresh();
                }
            }
            else
            {
                this.WarningLabel.Text = "Deifference Between Start and End Period Must Not Greater Than 12 Months";
            }
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ViewState[this._currPageKey] = "0";

            this.ClearData();

            this.ShowCustomer(0);
            this.SetCheckBox();
        }

        private double RowCountCustomer()
        {
            double _result1 = 0;
            string _tempCustGroup = (this.CustGroupDropDownList.SelectedValue == "null") ? "" : this.CustGroupDropDownList.SelectedValue;
            string _tempCustType = (this.CustTypeDropDownList.SelectedValue == "null") ? "" : this.CustTypeDropDownList.SelectedValue;

            _result1 = this._custBL.RowsCountCustReport(_tempCustGroup, _tempCustType, this.CategoryDropDownList1.SelectedValue, this.KeywordTextBox1.Text);
            _result1 = System.Math.Ceiling(_result1 / (double)_maxrow);

            return _result1;
        }

        private void ShowPage(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCountCustomer();

            if (_prmCurrentPage - _maxlength > 0)
            {
                min = _prmCurrentPage - _maxlength;
            }
            else
            {
                min = 0;
            }

            if (_prmCurrentPage + _maxlength < q)
            {
                max = _prmCurrentPage + _maxlength + 1;
            }
            else
            {
                max = Convert.ToDecimal(q);
            }

            if (_prmCurrentPage > 0)
                _addElement += 2;

            if (_prmCurrentPage < q - 1)
                _addElement += 2;

            i = Convert.ToInt32(min);
            _pageNumber = new Int32[Convert.ToInt32(max - min) + _addElement];
            if (_pageNumber.Length != 0)
            {
                //NB: Prev Or First
                if (_prmCurrentPage > 0)
                {
                    this._navMark[0] = 0;

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[0]);
                    _pageNumberElement++;

                    this._navMark[1] = Convert.ToInt32(Math.Abs(_prmCurrentPage - 1));

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[1]);
                    _pageNumberElement++;
                }

                for (; i < max; i++)
                {
                    _pageNumber[_pageNumberElement] = i;
                    _pageNumberElement++;
                }

                if (_prmCurrentPage < q - 1)
                {
                    this._navMark[2] = Convert.ToInt32(_prmCurrentPage + 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[2]);
                    _pageNumberElement++;

                    if (_pageNumber[_pageNumberElement - 2] > _pageNumber[_pageNumberElement - 1])
                    {
                        this._flag = true;
                    }

                    this._navMark[3] = Convert.ToInt32(q - 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[3]);
                    _pageNumberElement++;
                }

                int?[] _navMarkBackup = new int?[4];
                this._navMark.CopyTo(_navMarkBackup, 0);
                this.DataPagerTopRepeater.DataSource = from _query in _pageNumber
                                                       select _query;
                this.DataPagerTopRepeater.DataBind();

                _flag = true;
                _nextFlag = false;
                _lastFlag = false;
                _navMark = _navMarkBackup;
            }
            else
            {
                this.DataPagerTopRepeater.DataSource = from _query in _pageNumber
                                                       select _query;
                this.DataPagerTopRepeater.DataBind();
            }
        }

        protected void DataPagerTopRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this.TempHidden.Value = "";

                this.ShowCustomer(Convert.ToInt32(e.CommandArgument));
                this.SetCheckBox();
            }
        }

        protected void DataPagerTopRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                int _pageNumber = (int)e.Item.DataItem;

                if (Convert.ToInt32(this.ViewState[this._currPageKey]) == _pageNumber)
                {
                    TextBox _pageNumberTextbox = (TextBox)e.Item.FindControl("PageNumberTextBox");

                    _pageNumberTextbox.Text = (_pageNumber + 1).ToString();
                    _pageNumberTextbox.MaxLength = 9;
                    _pageNumberTextbox.Visible = true;

                    _pageNumberTextbox.Attributes.Add("style", "text-align: center;");
                    _pageNumberTextbox.Attributes.Add("OnKeyDown", "return NumericWithEnter();");
                }
                else
                {
                    LinkButton _pageNumberLinkButton = (LinkButton)e.Item.FindControl("PageNumberLinkButton");
                    _pageNumberLinkButton.CommandName = "DataPager";
                    _pageNumberLinkButton.CommandArgument = _pageNumber.ToString();

                    if (_pageNumber == this._navMark[0])
                    {
                        _pageNumberLinkButton.Text = "First";
                        this._navMark[0] = null;
                    }
                    else if (_pageNumber == this._navMark[1])
                    {
                        _pageNumberLinkButton.Text = "Prev";
                        this._navMark[1] = null;
                    }
                    else if (_pageNumber == this._navMark[2] && this._flag == false)
                    {
                        _pageNumberLinkButton.Text = "Next";
                        this._navMark[2] = null;
                        this._nextFlag = true;
                        this._flag = true;
                    }
                    else if (_pageNumber == this._navMark[3] && this._flag == true && this._nextFlag == true)
                    {
                        _pageNumberLinkButton.Text = "Last";
                        this._navMark[3] = null;
                        this._lastFlag = true;
                    }
                    else
                    {
                        if (this._lastFlag == false)
                        {
                            _pageNumberLinkButton.Text = (_pageNumber + 1).ToString();
                        }

                        if (_pageNumber == this._navMark[2] && this._flag == true)
                            this._flag = false;
                    }
                }
            }
        }

        protected void DataPagerButton_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerTopRepeater.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCountCustomer())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCountCustomer().ToString();
                        _reqPage = Convert.ToInt32(this.RowCountCustomer()) - 1;
                        break;
                    }
                    else if (_reqPage < 0)
                    {
                        ((TextBox)_item.Controls[3]).Text = "1";
                        _reqPage = 0;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            this.ViewState[this._currPageKey] = _reqPage;

            this.ShowCustomer(_reqPage);
            this.SetCheckBox();
        }

        protected void SetCheckBox()
        {
            int i = 0;
            foreach (ListItem _item in this.CustCodeCheckBoxList.Items)
            {
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _awal + i.ToString() + ", '" + _item.Value + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "', 'true');");

                _item.Selected = this.CheckHidden.Value.Contains(_item.Value);

                i++;

                //bound all data (on this page) to temphidden
                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _item.Value;
                }
                else
                {
                    this.TempHidden.Value += "," + _item.Value;
                }
            }

            this.SetCheckAllCheckBox();
        }

        protected void SetCheckAllCheckBox()
        {
            this.AllCheckBox.Checked = true;

            foreach (ListItem _item in this.CustCodeCheckBoxList.Items)
            {
                if (_item.Selected == false)
                {
                    this.AllCheckBox.Checked = false;
                }
            }

            if (this.CustCodeCheckBoxList.Items.Count < 1)
            {
                this.AllCheckBox.Checked = false;
            }
        }

        protected void SelectionDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearData();

            if (this.SelectionDropDownList.SelectedValue == "0")
            {
                this.RangePanel.Visible = true;
                this.SelectionPanel.Visible = false;
                this.ViewState[this._currPageKey] = 0;

                this.CheckHidden.Value = "";
                this.TempHidden.Value = "";
                //this.ShowCustGroup();
                this.ShowCustomer(0);
                this.SetCheckBox();
            }
            else
            {
                this.RangePanel.Visible = false;
                this.SelectionPanel.Visible = true;
                this.ViewState[this._currPageKey] = 0;

                this.CheckHidden.Value = "";
                this.TempHidden.Value = "";
                //this.ShowCustType();
                //this.ShowCustGroup();
                this.ShowCustomer(0);
                this.SetCheckBox();
            }
        }

        protected void GoImageButton1_Click(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.ClearData();

            this.ShowCustomer(0);
        }
}
}
