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
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Report
{
    public partial class SuratJalanSummaryPerCustomer : ReportBase
    {
        private ReportStockControlBL _report = new ReportStockControlBL();
        private ProductBL _productBL = new ProductBL();
        private WarehouseBL _wrhsBL = new WarehouseBL();
        private PermissionBL _permBL = new PermissionBL();
        private CustomerBL _custBL = new CustomerBL();

        private string _reportPath0 = "Report/SuratJalanSumPerCustomerByCust.rdlc";
        private string _reportPath1 = "Report/SuratJalanSumPerCustomerByCustProduct.rdlc";
        private string _reportPath2 = "Report/SuratJalanSumPerCustomerByProductCust.rdlc";

        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);

        private int _maxrow2 = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength2 = Convert.ToInt32(ApplicationConfig.DataPagerRange);

        private int?[] _navMark2 = { null, null, null, null };
        private bool _flag2 = true;
        private bool _nextFlag2 = false;
        private bool _lastFlag2 = false;

        private string _currPageKey2 = "CurrentPage2";
        private string _currPageKey1 = "CurrentPage1";

        private string _awal2 = "ctl00_DefaultBodyContentPlaceHolder_CustCodeCheckBoxList_";
        private string _akhir2 = "";
        private string _cbox2 = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox2";
        private string _tempHidden2 = "ctl00$DefaultBodyContentPlaceHolder$TempHidden2";

        private string _awal1 = "ctl00_DefaultBodyContentPlaceHolder_ProductSubGroupCheckBoxList_";
        private string _akhir1 = "";
        private string _cbox1 = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox1";
        private string _tempHidden1 = "ctl00$DefaultBodyContentPlaceHolder$TempHidden1";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDSuratJalanSumCust, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer1.Attributes.Add("style", "margin-bottom: 30px;");

            this.DataPagerButton2.Attributes.Add("Style", "visibility: hidden;");
            this.DataPagerButton1.Attributes.Add("Style", "visibility: hidden;");

            if (!Page.IsPostBack == true)
            {
                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = "Bill Of Lading Summary Per Customer Report";

                this.ViewState[this._currPageKey2] = 0;
                this.ViewState[this._currPageKey1] = 0;

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.GoImageButton1.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/go.jpg";
                this.GoImageButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/go.jpg";

                this.MenuPanel.Visible = true;
                this.ReportViewer1.Visible = false;

                this.ShowCustGroup();
                this.ShowCustType();

                this.SetAttribute();
                this.ClearData();

                this.ShowProductSubGroup(0);
                this.SetCheckBox1();

                this.ShowCustomer(0);
                this.SetCheckBox2();
            }
        }

        public void SetAttribute()
        {
            this.StartPeriodTextBox.Attributes.Add("OnBlur", " ValidatePeriod(" + this.StartPeriodTextBox.ClientID + ");");
            this.StartYearTextBox.Attributes.Add("OnBlur", " ValidateYear(" + this.StartYearTextBox.ClientID + ");");

            this.EndPeriodTextBox.Attributes.Add("OnBlur", " ValidatePeriod(" + this.EndPeriodTextBox.ClientID + ");");
            this.EndYearTextBox.Attributes.Add("OnBlur", " ValidateYear(" + this.EndYearTextBox.ClientID + ");");
        }

        private void ShowProductSubGroup(Int32 _prmCurrentPage2)
        {
            this.ProductSubGroupCheckBoxList.Items.Clear();
            this.ProductSubGroupCheckBoxList.DataValueField = "ProductSubGrpCode";
            this.ProductSubGroupCheckBoxList.DataTextField = "ProductSubGrpName";
            this.ProductSubGroupCheckBoxList.DataSource = this._productBL.GetListProductSubGroupForDDL(this.CategoryDropDownList1.SelectedValue, this.KeywordTextBox1.Text, _prmCurrentPage2, _maxrow2);
            this.ProductSubGroupCheckBoxList.DataBind();

            this.AllCheckBox1.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox1.ClientID + ", " + this.CheckHidden1.ClientID + ", '" + _awal1 + "', '" + _akhir1 + "', '" + _tempHidden1 + "', 'true');");

            int i = 0;
            foreach (ListItem _item in this.ProductSubGroupCheckBoxList.Items)
            {
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden1.ClientID + ", " + _awal1 + i.ToString() + ", '" + _item.Value + "', '" + _awal1 + "', '" + _akhir1 + "', '" + _cbox1 + "', 'true');");
                i++;
            }

            this.ShowPage1(_prmCurrentPage2);
        }

        private void ShowCustomer(Int32 _prmCurrentPage)
        {
            string _tempCustGroup = (this.CustGroupDropDownList.SelectedValue == "null") ? "" : this.CustGroupDropDownList.SelectedValue;
            string _tempCustType = (this.CustTypeDropDownList.SelectedValue == "null") ? "" : this.CustTypeDropDownList.SelectedValue;

            this.CustCodeCheckBoxList.ClearSelection();
            this.CustCodeCheckBoxList.Items.Clear();
            this.CustCodeCheckBoxList.DataTextField = "CustName";
            this.CustCodeCheckBoxList.DataValueField = "CustCode";
            this.CustCodeCheckBoxList.DataSource = this._custBL.GetListDDLCustomer(_tempCustGroup, _tempCustType, this.CategoryDropDownList2.SelectedValue, this.KeywordTextBox2.Text, _prmCurrentPage, _maxrow);
            this.CustCodeCheckBoxList.DataBind();

            this.AllCheckBox2.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox2.ClientID + ", " + this.CheckHidden2.ClientID + ", '" + _awal2 + "', '" + _akhir2 + "', '" + this._tempHidden2 + "', 'true');");

            int i = 0;
            foreach (ListItem _item in this.CustCodeCheckBoxList.Items)
            {
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden2.ClientID + ", " + _awal2 + i.ToString() + ", '" + _item.Value + "', '" + _awal2 + "', '" + _akhir2 + "', '" + _cbox2 + "', 'true');");
                i++;
            }

            List<MsCustomer> _listMsCustomer = this._custBL.GetListDDLCustomer(_tempCustGroup, _tempCustType);
            this.AllHidden2.Value = "";

            foreach (MsCustomer _item in _listMsCustomer)
            {
                if (this.AllHidden2.Value == "")
                {
                    this.AllHidden2.Value = _item.CustCode;
                }
                else
                {
                    this.AllHidden2.Value += "," + _item.CustCode;
                }
            }

            this.ShowPage2(_prmCurrentPage);
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
            this.ViewState[this._currPageKey2] = 0;

            this.CheckHidden2.Value = "";
            this.TempHidden2.Value = "";
            this.ShowCustomer(0);
            this.SetCheckBox2();
        }

        public void ClearData()
        {
            this.HeaderReportList1.ReportGroup = "SJSumPerCust";
            this.CheckHidden2.Value = "";
            this.TempHidden2.Value = "";
            this.CustGroupDropDownList.SelectedValue = "null";
            this.CustTypeDropDownList.SelectedValue = "null";
            this.CustCodeCheckBoxList.ClearSelection();
            this.AllCheckBox2.Checked = false;
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
            ReportDataSource _reportDataSource1;
            if (this.HeaderReportList1.ReportGroup == "SOSumPerCust")
            {
                _validate = ValidateDiffDate();
                _warning = "Difference Between Start and End Period Must Not Greater Than 12 Months";

            }
            if (this.HeaderReportList1.ReportGroup == "SOSumPerCustByYear")
            {
                _validate = ValidateDiffYear();
                _warning = "Difference Between Start and End Period Must Not Greater Than 12 Years";

            }

            if (_validate == true)
            {

                this.MenuPanel.Visible = false;
                this.ReportViewer1.Visible = true;

                //string _productCode = "";
                string _custCode = "";

                string _start = this.StartYearTextBox.Text.PadLeft(4, '0') + this.StartPeriodTextBox.Text.PadLeft(2, '0');
                string _end = this.EndYearTextBox.Text.PadLeft(4, '0') + this.EndPeriodTextBox.Text.PadLeft(2, '0');

                if (this.GrabAllCheckBox2.Checked == true)
                {
                    _custCode = this.AllHidden2.Value;
                }
                else
                {
                    if (this.AllCheckBox2.Checked == true)
                    {
                        _custCode = this.TempHidden2.Value;
                    }
                    else
                    {
                        _custCode = this.CheckHidden2.Value;
                    }
                }

                string _productSubGroup = "";

                if (this.GrabAllCheckBox1.Checked == true)
                {
                    _productSubGroup = this.AllHidden1.Value;
                }
                else
                {
                    if (this.AllCheckBox1.Checked == true)
                    {
                        _productSubGroup = this.TempHidden1.Value;
                    }
                    else
                    {
                        _productSubGroup = this.CheckHidden1.Value;
                    }
                }

                if (this.CustCodeFromTextBox.Text == "")
                    this.CustCodeFromTextBox.Text = "000";

                if (this.CustCodeToTextBox.Text == "")
                    this.CustCodeToTextBox.Text = "zzz";

                if (this.ProductSubGroupFromTextBox.Text == "")
                    this.ProductSubGroupFromTextBox.Text = "000";

                if (this.ProductSubGroupToTextBox.Text == "")
                    this.ProductSubGroupToTextBox.Text = "zzz";
                

                //var _hasilCurr = this._productBL.GetListProductSubGroupForDDL().Count;

                //for (var i = 0; i < _hasilCurr; i++)
                //{
                //    if (this.ProductSubGroupCheckBoxList.Items[i].Selected == true)
                //    {
                //        if (_productSubGroup == "")
                //        {
                //            _productSubGroup += this.ProductSubGroupCheckBoxList.Items[i].Value;
                //        }
                //        else
                //        {
                //            _productSubGroup += "," + this.ProductSubGroupCheckBoxList.Items[i].Value;
                //        }
                //    }
                //}
                int _fgAmount = 0;
                if (this.FilterReportRadioButtonList1.SelectedValue == "0")
                {

                    if (this.HeaderReportList1.SelectedText == "Summary By Customer") _fgAmount = 1;

                    _reportDataSource1 = this._report.SuratJalanSummaryPerCustomer(_start, _end, _custCode, _productSubGroup, this.HeaderReportList1.SelectedIndex, _fgAmount, this.SelectionDropDownList.SelectedValue, this.ProductSubGroupFromTextBox.Text, this.ProductSubGroupToTextBox.Text, this.CustCodeFromTextBox.Text, this.CustCodeToTextBox.Text);

                    this.ReportViewer1.LocalReport.DataSources.Clear();
                    this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

                    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + this.HeaderReportList1.SelectedValue;

                    //if (this.ReportTypeDropDownList.SelectedValue == "0")
                    //{
                    //    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
                    //}
                    //else if (this.ReportTypeDropDownList.SelectedValue == "1")
                    //{
                    //    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
                    //}
                    //else if (this.ReportTypeDropDownList.SelectedValue == "2")
                    //{
                    //    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath2;
                    //}

                    this.ReportViewer1.DataBind();

                    ReportParameter[] _reportParam = new ReportParameter[14];
                    _reportParam[0] = new ReportParameter("Start", _start, true);
                    _reportParam[1] = new ReportParameter("End", _end, true);
                    _reportParam[2] = new ReportParameter("Str1", _custCode, true);
                    _reportParam[3] = new ReportParameter("Str2", _productSubGroup, true);
                    _reportParam[4] = new ReportParameter("Str3", "", true);
                    _reportParam[5] = new ReportParameter("FgReport", this.HeaderReportList1.SelectedIndex, true);
                    _reportParam[6] = new ReportParameter("FgAmount", "0", true);
                    _reportParam[7] = new ReportParameter("FgPriceType", "0", true);
                    _reportParam[8] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                    _reportParam[9] = new ReportParameter("FgFilter", this.SelectionDropDownList.SelectedValue, true);
                    _reportParam[10] = new ReportParameter("FromProduct", this.ProductSubGroupFromTextBox.Text, true);
                    _reportParam[11] = new ReportParameter("ToProduct", this.ProductSubGroupToTextBox.Text, true);
                    _reportParam[12] = new ReportParameter("FromCustomer", this.CustCodeFromTextBox.Text, true);
                    _reportParam[13] = new ReportParameter("ToCustomer", this.CustCodeToTextBox.Text, true);

                    this.ReportViewer1.LocalReport.SetParameters(_reportParam);
                    this.ReportViewer1.LocalReport.Refresh();
                }
                else if (this.FilterReportRadioButtonList1.SelectedValue == "1")
                {
                    _start = this.StartYearTextBox.Text.PadLeft(4, '0');
                    _end = this.EndYearTextBox.Text.PadLeft(4, '0');

                    _reportDataSource1 = this._report.SuratJalanSummaryPerCustomerByYear(_start, _end, _custCode, _productSubGroup, this.HeaderReportList1.SelectedIndex, this.SelectionDropDownList.SelectedValue, this.ProductSubGroupFromTextBox.Text, this.ProductSubGroupToTextBox.Text, this.CustCodeFromTextBox.Text, this.CustCodeToTextBox.Text);

                    this.ReportViewer1.LocalReport.DataSources.Clear();
                    this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

                    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + this.HeaderReportList1.SelectedValue;

                    this.ReportViewer1.DataBind();

                    ReportParameter[] _reportParam = new ReportParameter[13];
                    _reportParam[0] = new ReportParameter("Start", _start, true);
                    _reportParam[1] = new ReportParameter("End", _end, true);
                    _reportParam[2] = new ReportParameter("Str1", _custCode, true);
                    _reportParam[3] = new ReportParameter("Str2", _productSubGroup, true);
                    _reportParam[4] = new ReportParameter("Str3", "", true);
                    _reportParam[5] = new ReportParameter("FgReport", this.HeaderReportList1.SelectedIndex, true);
                    //_reportParam[6] = new ReportParameter("FgAmount", "0", true);
                    _reportParam[6] = new ReportParameter("FgExport", "0", true);
                    _reportParam[7] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                    _reportParam[8] = new ReportParameter("FgFilter", this.SelectionDropDownList.SelectedValue, true);
                    _reportParam[9] = new ReportParameter("FromProdSubGrp", this.ProductSubGroupFromTextBox.Text, true);
                    _reportParam[10] = new ReportParameter("ToProdSubGrp", this.ProductSubGroupToTextBox.Text, true);
                    _reportParam[11] = new ReportParameter("From", this.CustCodeFromTextBox.Text, true);
                    _reportParam[12] = new ReportParameter("To", this.CustCodeToTextBox.Text, true);

                    this.ReportViewer1.LocalReport.SetParameters(_reportParam);
                    this.ReportViewer1.LocalReport.Refresh();
                }
            }
            else
            {
                this.WarningLabel.Text = "Diffrence Between Start and End Period Must Not Greater Than 12 Months";  
            }
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ViewState[this._currPageKey2] = "0";
            this.ViewState[this._currPageKey1] = "0";

            this.ShowCustGroup();
            this.ShowCustType();
           
            this.ClearData();

            this.ShowCustomer(0);
            this.SetCheckBox2();
        }

        private double RowCountCustomer()
        {
            double _result1 = 0;
            string _tempCustGroup = (this.CustGroupDropDownList.SelectedValue == "null") ? "" : this.CustGroupDropDownList.SelectedValue;
            string _tempCustType = (this.CustTypeDropDownList.SelectedValue == "null") ? "" : this.CustTypeDropDownList.SelectedValue;

            _result1 = this._custBL.RowsCountCustReport(_tempCustGroup, _tempCustType, this.CategoryDropDownList2.SelectedValue, this.KeywordTextBox2.Text);
            _result1 = System.Math.Ceiling(_result1 / (double)_maxrow);

            return _result1;
        }

        private double RowCountProductSubGroup()
        {
            double _result1 = 0;

            _result1 = this._productBL.RowsCountProductSubGroup(this.CategoryDropDownList1.SelectedValue, this.KeywordTextBox1.Text);
            _result1 = System.Math.Ceiling(_result1 / (double)_maxrow2);

            return _result1;
        }

        private void ShowPage2(Int32 _prmCurrentPage)
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
                    this._navMark2[0] = 0;

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark2[0]);
                    _pageNumberElement++;

                    this._navMark2[1] = Convert.ToInt32(Math.Abs(_prmCurrentPage - 1));

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark2[1]);
                    _pageNumberElement++;
                }

                for (; i < max; i++)
                {
                    _pageNumber[_pageNumberElement] = i;
                    _pageNumberElement++;
                }

                if (_prmCurrentPage < q - 1)
                {
                    this._navMark2[2] = Convert.ToInt32(_prmCurrentPage + 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark2[2]);
                    _pageNumberElement++;

                    if (_pageNumber[_pageNumberElement - 2] > _pageNumber[_pageNumberElement - 1])
                    {
                        this._flag2 = true;
                    }

                    this._navMark2[3] = Convert.ToInt32(q - 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark2[3]);
                    _pageNumberElement++;
                }

                int?[] _navMarkBackup = new int?[4];
                this._navMark2.CopyTo(_navMarkBackup, 0);
                this.DataPagerTopRepeater2.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTopRepeater2.DataBind();

                _flag2 = true;
                _nextFlag2 = false;
                _lastFlag2 = false;
                _navMark2 = _navMarkBackup;
            }
            else
            {
                this.DataPagerTopRepeater2.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTopRepeater2.DataBind();
            }
        }

        private void ShowPage1(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCountProductSubGroup();

            if (_prmCurrentPage - _maxlength2 > 0)
            {
                min = _prmCurrentPage - _maxlength2;
            }
            else
            {
                min = 0;
            }

            if (_prmCurrentPage + _maxlength2 < q)
            {
                max = _prmCurrentPage + _maxlength2 + 1;
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
                    this._navMark2[0] = 0;

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark2[0]);
                    _pageNumberElement++;

                    this._navMark2[1] = Convert.ToInt32(Math.Abs(_prmCurrentPage - 1));

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark2[1]);
                    _pageNumberElement++;
                }

                for (; i < max; i++)
                {
                    _pageNumber[_pageNumberElement] = i;
                    _pageNumberElement++;
                }

                if (_prmCurrentPage < q - 1)
                {
                    this._navMark2[2] = Convert.ToInt32(_prmCurrentPage + 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark2[2]);
                    _pageNumberElement++;

                    if (_pageNumber[_pageNumberElement - 2] > _pageNumber[_pageNumberElement - 1])
                    {
                        this._flag2 = true;
                    }

                    this._navMark2[3] = Convert.ToInt32(q - 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark2[3]);
                    _pageNumberElement++;
                }

                int?[] _navMarkBackup = new int?[4];
                this._navMark2.CopyTo(_navMarkBackup, 0);
                this.DataPagerTopRepeater1.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTopRepeater1.DataBind();

                _flag2 = true;
                _nextFlag2 = false;
                _lastFlag2 = false;
                _navMark2 = _navMarkBackup;
            }
            else
            {
                this.DataPagerTopRepeater1.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTopRepeater1.DataBind();
            }
        }

        protected void DataPagerTopRepeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager2")
            {
                this.ViewState[this._currPageKey2] = Convert.ToInt32(e.CommandArgument);

                this.TempHidden2.Value = "";

                this.ShowCustomer(Convert.ToInt32(e.CommandArgument));
                this.SetCheckBox2();
            }
        }

        protected void DataPagerTopRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                int _pageNumber2 = (int)e.Item.DataItem;

                if (Convert.ToInt32(this.ViewState[this._currPageKey2]) == _pageNumber2)
                {
                    TextBox _pageNumberTextbox2 = (TextBox)e.Item.FindControl("PageNumberTextBox2");

                    _pageNumberTextbox2.Text = (_pageNumber2 + 1).ToString();
                    _pageNumberTextbox2.MaxLength = 9;
                    _pageNumberTextbox2.Visible = true;

                    _pageNumberTextbox2.Attributes.Add("style", "text-align: center;");
                    _pageNumberTextbox2.Attributes.Add("OnKeyDown", "return NumericWithEnter();");
                }
                else
                {
                    LinkButton _pageNumberLinkButton2 = (LinkButton)e.Item.FindControl("PageNumberLinkButton2");
                    _pageNumberLinkButton2.CommandName = "DataPager2";
                    _pageNumberLinkButton2.CommandArgument = _pageNumber2.ToString();

                    if (_pageNumber2 == this._navMark2[0])
                    {
                        _pageNumberLinkButton2.Text = "First";
                        this._navMark2[0] = null;
                    }
                    else if (_pageNumber2 == this._navMark2[1])
                    {
                        _pageNumberLinkButton2.Text = "Prev";
                        this._navMark2[1] = null;
                    }
                    else if (_pageNumber2 == this._navMark2[2] && this._flag2 == false)
                    {
                        _pageNumberLinkButton2.Text = "Next";
                        this._navMark2[2] = null;
                        this._nextFlag2 = true;
                        this._flag2 = true;
                    }
                    else if (_pageNumber2 == this._navMark2[3] && this._flag2 == true && this._nextFlag2 == true)
                    {
                        _pageNumberLinkButton2.Text = "Last";
                        this._navMark2[3] = null;
                        this._lastFlag2 = true;
                    }
                    else
                    {
                        if (this._lastFlag2 == false)
                        {
                            _pageNumberLinkButton2.Text = (_pageNumber2 + 1).ToString();
                        }

                        if (_pageNumber2 == this._navMark2[2] && this._flag2 == true)
                            this._flag2 = false;
                    }
                }
            }
        }

        protected void DataPagerTopRepeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey1] = Convert.ToInt32(e.CommandArgument);

                this.TempHidden1.Value = "";

                this.ShowProductSubGroup(Convert.ToInt32(e.CommandArgument));
                this.SetCheckBox1();
            }
        }

        protected void DataPagerTopRepeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                int _pageNumber = (int)e.Item.DataItem;

                if (Convert.ToInt32(this.ViewState[this._currPageKey1]) == _pageNumber)
                {
                    TextBox _pageNumberTextbox = (TextBox)e.Item.FindControl("PageNumberTextBox1");

                    _pageNumberTextbox.Text = (_pageNumber + 1).ToString();
                    _pageNumberTextbox.MaxLength = 9;
                    _pageNumberTextbox.Visible = true;

                    _pageNumberTextbox.Attributes.Add("style", "text-align: center;");
                    _pageNumberTextbox.Attributes.Add("OnKeyDown", "return NumericWithEnter();");
                }
                else
                {
                    LinkButton _pageNumberLinkButton = (LinkButton)e.Item.FindControl("PageNumberLinkButton1");
                    _pageNumberLinkButton.CommandName = "DataPager";
                    _pageNumberLinkButton.CommandArgument = _pageNumber.ToString();

                    if (_pageNumber == this._navMark2[0])
                    {
                        _pageNumberLinkButton.Text = "First";
                        this._navMark2[0] = null;
                    }
                    else if (_pageNumber == this._navMark2[1])
                    {
                        _pageNumberLinkButton.Text = "Prev";
                        this._navMark2[1] = null;
                    }
                    else if (_pageNumber == this._navMark2[2] && this._flag2 == false)
                    {
                        _pageNumberLinkButton.Text = "Next";
                        this._navMark2[2] = null;
                        this._nextFlag2 = true;
                        this._flag2 = true;
                    }
                    else if (_pageNumber == this._navMark2[3] && this._flag2 == true && this._nextFlag2 == true)
                    {
                        _pageNumberLinkButton.Text = "Last";
                        this._navMark2[3] = null;
                        this._lastFlag2 = true;
                    }
                    else
                    {
                        if (this._lastFlag2 == false)
                        {
                            _pageNumberLinkButton.Text = (_pageNumber + 1).ToString();
                        }

                        if (_pageNumber == this._navMark2[2] && this._flag2 == true)
                            this._flag2 = false;
                    }
                }
            }
        }

        protected void DataPagerButton2_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerTopRepeater2.Controls)
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

            this.ViewState[this._currPageKey2] = _reqPage;

            this.ShowCustomer(_reqPage);
            this.SetCheckBox2();
        }

        protected void DataPagerButton1_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerTopRepeater1.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCountProductSubGroup())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCountProductSubGroup().ToString();
                        _reqPage = Convert.ToInt32(this.RowCountProductSubGroup()) - 1;
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

            this.ViewState[this._currPageKey1] = _reqPage;

            this.ShowProductSubGroup(_reqPage);
            this.SetCheckBox1();
        }

        protected void SetCheckBox2()
        {
            int i = 0;
            foreach (ListItem _item in this.CustCodeCheckBoxList.Items)
            {
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden2.ClientID + ", " + _awal2 + i.ToString() + ", '" + _item.Value + "', '" + _awal2 + "', '" + _akhir2 + "', '" + _cbox2 + "', 'true');");

                _item.Selected = this.CheckHidden2.Value.Contains(_item.Value);

                i++;

                //bound all data (on this page) to temphidden
                if (this.TempHidden2.Value == "")
                {
                    this.TempHidden2.Value = _item.Value;
                }
                else
                {
                    this.TempHidden2.Value += "," + _item.Value;
                }
            }

            this.SetCheckAllCheckBox2();
        }

        protected void SetCheckBox1()
        {
            int i = 0;
            foreach (ListItem _item in this.ProductSubGroupCheckBoxList.Items)
            {
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden1.ClientID + ", " + _awal1 + i.ToString() + ", '" + _item.Value + "', '" + _awal1 + "', '" + _akhir1 + "', '" + _cbox1 + "', 'true');");

                _item.Selected = this.CheckHidden1.Value.Contains(_item.Value);

                i++;

                //bound all data (on this page) to temphidden
                if (this.TempHidden1.Value == "")
                {
                    this.TempHidden1.Value = _item.Value;
                }
                else
                {
                    this.TempHidden1.Value += "," + _item.Value;
                }
            }

            this.SetCheckAllCheckBox1();
        }

        protected void SetCheckAllCheckBox2()
        {
            this.AllCheckBox2.Checked = true;

            foreach (ListItem _item in this.CustCodeCheckBoxList.Items)
            {
                if (_item.Selected == false)
                {
                    this.AllCheckBox2.Checked = false;
                }
            }

            if (this.CustCodeCheckBoxList.Items.Count < 1)
            {
                this.AllCheckBox2.Checked = false;
            }
        }

        protected void SetCheckAllCheckBox1()
        {
            this.AllCheckBox1.Checked = true;

            foreach (ListItem _item in this.ProductSubGroupCheckBoxList.Items)
            {
                if (_item.Selected == false)
                {
                    this.AllCheckBox1.Checked = false;
                }
            }

            if (this.ProductSubGroupCheckBoxList.Items.Count < 1)
            {
                this.AllCheckBox1.Checked = false;
            }
        }

        protected void GoImageButton1_Click(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey1] = 0;

            this.ClearLabel();

            this.ShowProductSubGroup(0);
        }

        protected void GoImageButton2_Click(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey2] = 0;

            this.ClearLabel();

            this.ShowCustomer(0);
        }

        protected void SelectionDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearLabel();

            if (this.SelectionDropDownList.SelectedValue == "0")
            {
                this.RangePanel.Visible = true;
                this.SelectionPanel.Visible = false;
                this.ViewState[this._currPageKey2] = 0;
                this.ViewState[this._currPageKey1] = 0;

                this.CheckHidden2.Value = "";
                this.TempHidden2.Value = "";
                this.CheckHidden1.Value = "";
                this.TempHidden1.Value = "";
                this.ShowCustGroup();
                this.ShowCustomer(0);
                this.SetCheckBox2();
                this.ShowProductSubGroup(0);
                this.SetCheckBox1();
            }
            else
            {
                this.RangePanel.Visible = false;
                this.SelectionPanel.Visible = true;
                this.ViewState[this._currPageKey2] = 0;
                this.ViewState[this._currPageKey1] = 0;

                this.CheckHidden1.Value = "";
                this.TempHidden1.Value = "";
                this.CheckHidden2.Value = "";
                this.TempHidden2.Value = "";
                this.ShowCustGroup();
                this.ShowCustomer(0);
                this.SetCheckBox2();
                this.ShowProductSubGroup(0);
                this.SetCheckBox1();
            }
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }
        
        protected void CustTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey2] = 0;

            this.CheckHidden2.Value = "";
            this.TempHidden2.Value = "";
            this.ShowCustomer(0);
            this.SetCheckBox2();
        }

        protected void FilterReportRadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.FilterReportRadioButtonList1.SelectedValue == "0")
            {
                this.HeaderReportList1.ReportGroup = "SJSumPerCust";
                this.HeaderReportList1.Render();
                this.StartPeriodTextBox.Visible = true;
                this.EndPeriodTextBox.Visible = true;
            }
            else
            {
                this.HeaderReportList1.ReportGroup = "SJSumPerCustByYear";
                this.HeaderReportList1.Render();
                this.StartPeriodTextBox.Visible = false;
                this.EndPeriodTextBox.Visible = false;
            }
        }
    }
}