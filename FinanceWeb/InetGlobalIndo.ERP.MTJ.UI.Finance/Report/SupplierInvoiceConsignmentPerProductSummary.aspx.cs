using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.Report
{
    public partial class SupplierInvoiceConsignmentPerProductSummary : ReportBase
    {
        private ReportFinanceBL _reportBL = new ReportFinanceBL();
        private SupplierBL _supplierBL = new SupplierBL();
        private CurrencyBL _currBL = new CurrencyBL();
        private PermissionBL _permBL = new PermissionBL();

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

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDSupplierInvoiceConsignmentPerProductSummary, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                //Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer.Attributes.Add("style", "margin-bottom: 30px;");

            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");

            if (!this.Page.IsPostBack == true)
            {
                this.StartDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.StartDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.EndDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.EndDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = "Supplier Invoice Per Product Report";

                this.ViewState[this._currPageKey] = 0;

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.MenuPanel.Visible = true;
                this.ReportViewer.Visible = false;

                this.ShowCurrency();
                this.ShowSuppGroup();
                this.ShowSuppType();

                this.SetAttribute();
                this.ClearData();

                this.ShowSupplier(0);
                this.SetCheckBox();
            }
        }

        private void SetAttribute()
        {
            this.StartDateTextBox.Attributes.Add("ReadOnly", "True");
            this.EndDateTextBox.Attributes.Add("ReadOnly", "True");
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

        private void ShowSupplier(Int32 _prmCurrentPage)
        {
            string _tempSuppGroup = (this.SuppGroupDDL.SelectedValue == "null") ? "" : this.SuppGroupDDL.SelectedValue;
            string _tempSuppType = (this.SuppTypeDDL.SelectedValue == "null") ? "" : this.SuppTypeDDL.SelectedValue;

            this.SuppCodeCheckBoxList.ClearSelection();
            this.SuppCodeCheckBoxList.Items.Clear();
            this.SuppCodeCheckBoxList.DataTextField = "SuppName";
            this.SuppCodeCheckBoxList.DataValueField = "SuppCode";
            this.SuppCodeCheckBoxList.DataSource = this._supplierBL.GetListDDLSuppForReport(_tempSuppGroup, _tempSuppType, _prmCurrentPage, _maxrow);
            this.SuppCodeCheckBoxList.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "', 'true');");

            int i = 0;
            foreach (ListItem _item in this.SuppCodeCheckBoxList.Items)
            {
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _awal + i.ToString() + ", '" + _item.Value + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "', 'true');");
                i++;
            }

            List<MsSupplier> _listMsSuppler = this._supplierBL.GetListDDLSuppForReport(_tempSuppGroup, _tempSuppType);
            this.AllHidden.Value = "";

            foreach (MsSupplier _item in _listMsSuppler)
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
            this.SuppGroupDDL.Items.Clear();
            this.SuppGroupDDL.DataSource = this._supplierBL.GetListSuppGroupForDDL();
            this.SuppGroupDDL.DataValueField = "SuppGroupCode";
            this.SuppGroupDDL.DataTextField = "SuppGroupName";
            this.SuppGroupDDL.DataBind();
            this.SuppGroupDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowSuppType()
        {
            this.SuppTypeDDL.Items.Clear();
            this.SuppTypeDDL.DataSource = this._supplierBL.GetListSuppTypeForDDL();
            this.SuppTypeDDL.DataValueField = "SuppTypeCode";
            this.SuppTypeDDL.DataTextField = "SuppTypeName";
            this.SuppTypeDDL.DataBind();
            this.SuppTypeDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SuppGroupDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";
            this.ShowSupplier(0);
            this.SetCheckBox();
        }

        protected void SuppTypeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";
            this.ShowSupplier(0);
            this.SetCheckBox();
        }

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ViewState[this._currPageKey] = 0;

            this.ClearLabel();
            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";
            this.StartDateTextBox.Text = DateFormMapper.GetValue(now);
            this.EndDateTextBox.Text = DateFormMapper.GetValue(now);
            //this.FgReportDropDownList.SelectedValue = "null";
            this.SuppTypeDDL.SelectedValue = "null";
            this.SuppGroupDDL.SelectedValue = "null";
            this.CurrCodeCheckBoxList.ClearSelection();
            this.SuppCodeCheckBoxList.ClearSelection();
            this.HeaderReportList1.ReportGroup = "SupplierInvoiceConsignmentPerProductSummary";

        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = false;
            this.ReportViewer.Visible = true;

            string _currCode = "";
            string _suppCode = "";

            var hasilCurr = this._currBL.GetListAll();
            int _fgReport = 0;

            for (var i = 0; i < hasilCurr.Count(); i++)
            {
                if (this.CurrCodeCheckBoxList.Items[i].Selected == true)
                {
                    if (_currCode == "")
                    {
                        _currCode += this.CurrCodeCheckBoxList.Items[i].Value.Trim();
                    }
                    else
                    {
                        _currCode += "," + this.CurrCodeCheckBoxList.Items[i].Value.Trim();
                    }
                }
            }

            if (this.GrabAllCheckBox.Checked == true)
            {
                _suppCode = this.AllHidden.Value.Trim();
            }
            else
            {
                if (this.AllCheckBox.Checked == true)
                {
                    _suppCode = this.TempHidden.Value.Trim();
                }
                else
                {
                    _suppCode = this.CheckHidden.Value.Trim();
                }
            }

            DateTime _startDateTime = new DateTime(DateFormMapper.GetValue(this.StartDateTextBox.Text).Year, DateFormMapper.GetValue(this.StartDateTextBox.Text).Month, DateFormMapper.GetValue(this.StartDateTextBox.Text).Day, 0, 0, 0);
            DateTime _endDateTime = new DateTime(DateFormMapper.GetValue(this.EndDateTextBox.Text).Year, DateFormMapper.GetValue(this.EndDateTextBox.Text).Month, DateFormMapper.GetValue(this.EndDateTextBox.Text).Day, 23, 59, 59);

            if (this.HeaderReportList1.SelectedIndex == "0" && this.GroupByDropDownList.SelectedValue == "0")
            {
                _fgReport = 0;
            }
            else if (this.HeaderReportList1.SelectedIndex == "0" && this.GroupByDropDownList.SelectedValue == "1")
            {
                _fgReport = 1;
            }
            else if (this.HeaderReportList1.SelectedIndex == "1" && this.GroupByDropDownList.SelectedValue == "0")
            {
                _fgReport = 2;
            }
            else if (this.HeaderReportList1.SelectedIndex == "1" && this.GroupByDropDownList.SelectedValue == "1")
            {
                _fgReport = 3;
            }

            //if (this.SuppFromTextBox.Text == "")
            //    this.SuppFromTextBox.Text = "000";
            //if (this.SuppToTextBox.Text == "")
            //    this.SuppToTextBox.Text = "zzz";
            Int32 _fgCurr = Convert.ToInt32(this.GroupByDropDownList.SelectedValue);
            ReportDataSource _reportDataSource1 = this._reportBL.SupplierInvoiceConsignmentPerProductSummary(_startDateTime, _endDateTime, _suppCode, _currCode, this.SuppFromTextBox.Text.Trim(), this.SuppToTextBox.Text.Trim(), _fgReport, _fgCurr);

            this.ReportViewer.LocalReport.DataSources.Clear();
            this.ReportViewer.LocalReport.DataSources.Add(_reportDataSource1);

            this.ReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + this.HeaderReportList1.SelectedValue;

            //if (_fgReport == 0 || _fgReport == 1)
            //{
            //    this.ReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
            //}
            //else if (_fgReport == 2 || _fgReport == 3)
            //{
            //    this.ReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
            //}

            this.ReportViewer.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[9];
            _reportParam[0] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            _reportParam[1] = new ReportParameter("Start", _startDateTime.ToString(), true);
            _reportParam[2] = new ReportParameter("End", _endDateTime.ToString(), true);
            _reportParam[3] = new ReportParameter("Str1", _suppCode, true);
            _reportParam[4] = new ReportParameter("Str2", _currCode, true);
            _reportParam[5] = new ReportParameter("From", this.SuppFromTextBox.Text.Trim(), true);
            _reportParam[6] = new ReportParameter("To", this.SuppToTextBox.Text.Trim(), true);
            _reportParam[7] = new ReportParameter("FgReport", _fgReport.ToString(), true);
            _reportParam[8] = new ReportParameter("FgCurr", _fgCurr.ToString(), true);
            
            this.ReportViewer.LocalReport.SetParameters(_reportParam);
            this.ReportViewer.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();

            this.ShowSupplier(0);
            this.SetCheckBox();
        }

        private double RowCountSupplier()
        {
            double _result1 = 0;
            string _tempSuppGroup = (this.SuppGroupDDL.SelectedValue == "null") ? "" : this.SuppGroupDDL.SelectedValue;
            string _tempSuppType = (this.SuppTypeDDL.SelectedValue == "null") ? "" : this.SuppTypeDDL.SelectedValue;

            _result1 = this._supplierBL.RowsCountSupplierReport(_tempSuppGroup, _tempSuppType);
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
    }
}