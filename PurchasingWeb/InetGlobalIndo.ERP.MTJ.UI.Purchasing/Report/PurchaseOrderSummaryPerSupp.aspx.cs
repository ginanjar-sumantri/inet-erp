using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.Report
{
    public partial class PurchaseOrderSummaryPerSupp : ReportBase
    {
        private ReportPurchaseBL _report = new ReportPurchaseBL();
        private PermissionBL _permBL = new PermissionBL();
        private SupplierBL _suppBL = new SupplierBL();

        private string _reportPath0 = "Report/PurchaseOrderSummaryPerSupp.rdlc";

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);

        private string _currPageKey = "CurrentPage";

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_SuppCodeCheckBoxList_";
        private string _akhir = "";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private ReportListBL _reporListBL = new ReportListBL();

        private void ShowData(string _ReportType)
        {
            this.ReportNameDDL.Items.Clear();
            this.ReportNameDDL.DataSource = _reporListBL.GetReportListDDL(_ReportType, new UserBL().GetCompanyId(HttpContext.Current.User.Identity.Name));
            this.ReportNameDDL.DataValueField = "ReportPath";
            this.ReportNameDDL.DataTextField = "ReportName";
            this.ReportNameDDL.DataBind();
            //this.ReportNameDDL.SelectedIndex = 0;
        }
        private void ShowData2(string _ReportType)
        {
            this.ReportNameDDL.Items.Clear();
            this.ReportNameDDL.DataSource = _reporListBL.GetReportListDDL(_ReportType, new UserBL().GetCompanyId(HttpContext.Current.User.Identity.Name));
            this.ReportNameDDL.DataValueField = "ReportPath";
            this.ReportNameDDL.DataTextField = "ReportName";
            this.ReportNameDDL.DataBind();
            //this.ReportNameDDL.SelectedIndex = 0;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDPurchaseOrderSummaryPerSupp, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

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
                this.PageTitleLiteral.Text = "Purchase Order Summary Per Supplier Report";

                this.ViewState[this._currPageKey] = 0;

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.MenuPanel.Visible = true;
                this.ReportViewer1.Visible = false;

                this.ShowSuppGroup();
                this.ShowSuppType();

                this.SetAttribute();
                this.ClearData();

                this.ShowSupplier(0);
                this.SetCheckBox();

                this.FilterReportRadioButtonList1.SelectedIndex = 0;
                
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
            this.SuppCodeCheckBoxList.ClearSelection();
            this.GroupByDropDownList.SelectedValue = "0";
            this.SuppGroupDropDownList.SelectedValue = "null";
            this.SuppTypeDropDownList.SelectedValue = "null";
            this.AllCheckBox.Checked = false;

            //this.HeaderReportList1.ReportGroup = "POSumPerSupp";
            this.ShowData("POSumPerSupp");
        }

        private void ShowSupplier(Int32 _prmCurrentPage)
        {
            string _tempSuppGroup = (this.SuppGroupDropDownList.SelectedValue == "null") ? "" : this.SuppGroupDropDownList.SelectedValue;
            string _tempSuppType = (this.SuppTypeDropDownList.SelectedValue == "null") ? "" : this.SuppTypeDropDownList.SelectedValue;

            this.SuppCodeCheckBoxList.ClearSelection();
            this.SuppCodeCheckBoxList.Items.Clear();
            this.SuppCodeCheckBoxList.DataTextField = "SuppName";
            this.SuppCodeCheckBoxList.DataValueField = "SuppCode";
            this.SuppCodeCheckBoxList.DataSource = this._suppBL.GetListDDLSuppForReport(_tempSuppGroup, _tempSuppType, _prmCurrentPage, _maxrow);
            this.SuppCodeCheckBoxList.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "', 'true');");

            int i = 0;
            foreach (ListItem _item in this.SuppCodeCheckBoxList.Items)
            {
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _awal + i.ToString() + ", '" + _item.Value + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "', 'true');");
                i++;
            }

            List<MsSupplier> _listMsSupplier = this._suppBL.GetListDDLSuppForReport(_tempSuppGroup, _tempSuppType);
            this.AllHidden.Value = "";

            foreach (MsSupplier _item in _listMsSupplier)
            {
                if (this.AllHidden.Value == "")
                {
                    this.AllHidden.Value = _item.SuppCode;
                }
                else
                {
                    this.AllHidden.Value += "," + _item.SuppCode;
                }
            }

            this.ShowPage(_prmCurrentPage);
        }

        protected void ShowSuppGroup()
        {
            this.SuppGroupDropDownList.Items.Clear();
            this.SuppGroupDropDownList.DataSource = this._suppBL.GetListSuppGroupForDDL();
            this.SuppGroupDropDownList.DataValueField = "SuppGroupCode";
            this.SuppGroupDropDownList.DataTextField = "SuppGroupName";
            this.SuppGroupDropDownList.DataBind();
            this.SuppGroupDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowSuppType()
        {
            this.SuppTypeDropDownList.Items.Clear();
            this.SuppTypeDropDownList.DataSource = this._suppBL.GetListSuppTypeForDDL();
            this.SuppTypeDropDownList.DataValueField = "SuppTypeCode";
            this.SuppTypeDropDownList.DataTextField = "SuppTypeName";
            this.SuppTypeDropDownList.DataBind();
            this.SuppTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SuppGroupDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";
            this.ShowSupplier(0);
            this.SetCheckBox();
        }

        protected void SuppTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";
            this.ShowSupplier(0);
            this.SetCheckBox();
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

        private bool ValidateDiffYear()
        {
            bool _result = false;

            DateTime _start = Convert.ToDateTime(this.StartYearTextBox.Text + "-01-01");
            DateTime _end = Convert.ToDateTime(this.EndYearTextBox.Text + "-01-01");

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
            bool _validate = true;
            string _warning = "";
            if (this.ReportType.Value == "POSumPerSupp")
            {
                _validate = ValidateDiffDate();
                _warning = "Difference Between Start and End Period Must Not Greater Than 12 Months";

            }
            if (this.ReportType.Value == "POSumPerSuppYear")
            {
                _validate = ValidateDiffYear();
                _warning = "Difference Between Start and End Period Must Not Greater Than 12 Years";

            }
            if (_validate == true)
            {
                this.MenuPanel.Visible = false;
                this.ReportViewer1.Visible = true;

                string _suppCode = "";

                if (this.GrabAllCheckBox.Checked == true)
                {
                    _suppCode = this.AllHidden.Value;
                }
                else
                {
                    if (this.AllCheckBox.Checked == true)
                    {
                        _suppCode = this.TempHidden.Value;
                    }
                    else
                    {
                        _suppCode = this.CheckHidden.Value;
                    }
                }

                if (this.SupplierFromTextBox.Text == "")
                    this.SupplierFromTextBox.Text = "000";
                if (this.SupplierToTextBox.Text == "")
                    this.SupplierToTextBox.Text = "zzz";

                if (this.FilterReportRadioButtonList1.SelectedValue == "0")
                {
                    string _start = this.StartYearTextBox.Text.PadLeft(4, '0') + this.StartPeriodTextBox.Text.PadLeft(2, '0');
                    string _end = this.EndYearTextBox.Text.PadLeft(4, '0') + this.EndPeriodTextBox.Text.PadLeft(2, '0');

                    ReportDataSource _reportDataSource1 = this._report.PurchaseOrderSummaryPerSupp(_start, _end, _suppCode, Convert.ToInt32(this.GroupByDropDownList.SelectedValue), this.FilterDropDownList.SelectedValue, this.SupplierFromTextBox.Text, this.SupplierToTextBox.Text);

                    this.ReportViewer1.LocalReport.DataSources.Clear();
                    this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

                    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + this.ReportNameDDL.SelectedValue;

                    //this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
                    this.ReportViewer1.DataBind();

                    ReportParameter[] _reportParam = new ReportParameter[11];
                    _reportParam[0] = new ReportParameter("Start", _start, true);
                    _reportParam[1] = new ReportParameter("End", _end, true);
                    _reportParam[2] = new ReportParameter("Str1", _suppCode, true);
                    _reportParam[3] = new ReportParameter("Str2", "", false);
                    _reportParam[4] = new ReportParameter("Str3", this.GroupByDropDownList.SelectedValue, true);
                    _reportParam[5] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                    _reportParam[6] = new ReportParameter("Str4", "", true);
                    _reportParam[7] = new ReportParameter("Str5", "", true);
                    _reportParam[8] = new ReportParameter("FgFilter", this.FilterDropDownList.SelectedValue, true);
                    _reportParam[9] = new ReportParameter("FromSupplier", this.SupplierFromTextBox.Text, true);
                    _reportParam[10] = new ReportParameter("ToSupplier", this.SupplierToTextBox.Text, true);

                    this.ReportViewer1.LocalReport.SetParameters(_reportParam);
                    this.ReportViewer1.LocalReport.Refresh();
                }
                if (this.FilterReportRadioButtonList1.SelectedValue == "1")
                {
                    string _start = this.StartYearTextBox.Text.PadLeft(4, '0');
                    string _end = this.EndYearTextBox.Text.PadLeft(4, '0');

                    ReportDataSource _reportDataSource1 = this._report.PurchaseOrderSummaryPerSuppYear(_start, _end, _suppCode, Convert.ToInt32(this.GroupByDropDownList.SelectedValue), this.ReportNameDDL.SelectedIndex, this.FilterDropDownList.SelectedValue, this.SupplierFromTextBox.Text, this.SupplierToTextBox.Text);

                    this.ReportViewer1.LocalReport.DataSources.Clear();
                    this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
                    //this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
                    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + this.ReportNameDDL.SelectedValue;
                    this.ReportViewer1.DataBind();

                    ReportParameter[] _reportParam = new ReportParameter[10];
                    _reportParam[0] = new ReportParameter("Start", _start, true);
                    _reportParam[1] = new ReportParameter("End", _end, true);
                    _reportParam[2] = new ReportParameter("Str1", _suppCode, true);
                    _reportParam[3] = new ReportParameter("Str2", "", false);
                    _reportParam[4] = new ReportParameter("Str3", this.GroupByDropDownList.SelectedValue, true);
                    _reportParam[5] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                    _reportParam[6] = new ReportParameter("FgReport", this.ReportNameDDL.SelectedIndex.ToString(), true);
                    _reportParam[7] = new ReportParameter("FgFilter", this.FilterDropDownList.SelectedValue, true);
                    _reportParam[8] = new ReportParameter("FromSupplier", this.SupplierFromTextBox.Text, true);
                    _reportParam[9] = new ReportParameter("ToSupplier", this.SupplierToTextBox.Text, true);

                    this.ReportViewer1.LocalReport.SetParameters(_reportParam);
                    this.ReportViewer1.LocalReport.Refresh();
                }
            }
            else
            {
                this.WarningLabel.Text = _warning;
            }
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ViewState[this._currPageKey] = "0";

            this.ClearData();

            this.ShowSupplier(0);
            this.SetCheckBox();
        }

        private double RowCountSupplier()
        {
            double _result1 = 0;
            string _tempSuppGroup = (this.SuppGroupDropDownList.SelectedValue == "null") ? "" : this.SuppGroupDropDownList.SelectedValue;
            string _tempSuppType = (this.SuppTypeDropDownList.SelectedValue == "null") ? "" : this.SuppTypeDropDownList.SelectedValue;

            _result1 = this._suppBL.RowsCountSupplierReport(_tempSuppGroup, _tempSuppType);
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
            double q = this.RowCountSupplier();

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

                this.ShowSupplier(Convert.ToInt32(e.CommandArgument));
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

                    if (_reqPage > this.RowCountSupplier())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCountSupplier().ToString();
                        _reqPage = Convert.ToInt32(this.RowCountSupplier()) - 1;
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

            this.ShowSupplier(_reqPage);
            this.SetCheckBox();
        }

        protected void SetCheckBox()
        {
            int i = 0;
            foreach (ListItem _item in this.SuppCodeCheckBoxList.Items)
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

            foreach (ListItem _item in this.SuppCodeCheckBoxList.Items)
            {
                if (_item.Selected == false)
                {
                    this.AllCheckBox.Checked = false;
                }
            }

            if (this.SuppCodeCheckBoxList.Items.Count < 1)
            {
                this.AllCheckBox.Checked = false;
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

        protected void FilterReportRadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.FilterReportRadioButtonList1.SelectedValue == "0")
            {
                this.ShowData("POSumPerSupp");
                this.ReportType.Value = "POSumPerSupp";
                this.StartPeriodTextBox.Visible = true;
                this.EndPeriodTextBox.Visible = true;
            }
            else
            {
                this.ShowData2("POSumPerSuppYear");
                this.ReportType.Value = "POSumPerSuppYear";
                this.StartPeriodTextBox.Visible = false;
                this.EndPeriodTextBox.Visible = false;
            }
        }


    }
}
