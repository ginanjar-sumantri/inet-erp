using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.Report
{
    public partial class POSPerProduct : ReportBase
    {
        private POSReportBL _reportBL = new POSReportBL();
        private ProductBL _productBL = new ProductBL();
        private PermissionBL _permBL = new PermissionBL();

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private string _reportPath0 = "Report/PosPerProduct.rdlc";

        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);

        private string _currPageKey = "CurrentPage";

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ProductCheckBoxList_";
        private string _awalCashier = "ctl00_DefaultBodyContentPlaceHolder_CashierCheckBoxList_";
        private string _akhir = "";
        private string _akhirCashier = "";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _cboxCashier = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBoxCashier";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";
        private string _tempHiddenCashier = "ctl00$DefaultBodyContentPlaceHolder$TempHiddenCashier";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDPOSPerProduct, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer1.Attributes.Add("style", "margin-bottom: 30px;");

            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");
            this.DataPagerButtonCashier.Attributes.Add("Style", "visibility: hidden;");

            if (!Page.IsPostBack == true)
            {
                this.StartDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.StartDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.EndDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.EndDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = "POS Report List Per Product";

                this.ViewState[this._currPageKey] = 0;

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.GoImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/go.jpg";
                this.GoImageButtonCashier.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/go.jpg";

                this.MenuPanel.Visible = true;
                this.ReportViewer1.Visible = false;

                this.SetAttribut();
                this.ShowProductSubGroup();
                this.ShowProductType();
                this.ClearData();
                this.ShowCashier(0);
                this.ShowProduct(0);
                this.SetCheckBox();
                this.SetCheckBoxCashier();
            }
        }

        private double RowCount()
        {
            double _result = 0;

            _result = this._reportBL.RowsCountDDLProductForReport(this.ProductSubGroupDropDownList.SelectedValue, this.ProductTypeDropDownList.SelectedValue, this.CategoryDropDownList.SelectedValue, this.KeywordTextBox.Text);
            _result = System.Math.Ceiling(_result / (double)_maxrow);

            return _result;
        }

        private double RowCountCashier()
        {
            double _result = 0;

            _result = this._reportBL.RowsCountTransactionReport(this.CategoryDropDownListCahsier.SelectedValue, this.KeywordTextBoxCashier.Text);
            _result = System.Math.Ceiling(_result / (double)_maxrow);

            return _result;
        }

        private void ShowPage(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCount();

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

        private void ShowPageCashier(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCountCashier();

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
                this.DataPagerTopRepeaterCashier.DataSource = from _query in _pageNumber
                                                              select _query;
                this.DataPagerTopRepeaterCashier.DataBind();

                _flag = true;
                _nextFlag = false;
                _lastFlag = false;
                _navMark = _navMarkBackup;
            }
            else
            {
                this.DataPagerTopRepeaterCashier.DataSource = from _query in _pageNumber
                                                              select _query;
                this.DataPagerTopRepeaterCashier.DataBind();
            }
        }

        public void ShowCashier(Int32 _prmCurrentPage)
        {
            var hasil = this._reportBL.GetListCashierForReport(_prmCurrentPage, _maxrow, this.CategoryDropDownListCahsier.SelectedValue, this.KeywordTextBoxCashier.Text);

            this.CashierCheckBoxList.DataSource = hasil;
            this.CashierCheckBoxList.DataValueField = "CashierID";
            this.CashierCheckBoxList.DataTextField = "CashierID";
            this.CashierCheckBoxList.DataBind();

            this.AllCheckBoxCashier.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBoxCashier.ClientID + ", " + this.CheckHiddenCashier.ClientID + ", '" + _awalCashier + "', '" + _akhirCashier + "', '" + _tempHiddenCashier + "', 'true');");

            int i = 0;
            foreach (ListItem _item in this.CashierCheckBoxList.Items)
            {
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHiddenCashier.ClientID + ", " + "ctl00_DefaultBodyContentPlaceHolder_CashierCheckBoxList_" + i.ToString() + ", '" + _item.Value + "', '" + _awalCashier + "', '" + _akhirCashier + "', '" + _cboxCashier + "', 'true');");
                i++;
            }

            this.ShowPageCashier(_prmCurrentPage);
        }

        protected void ShowProductSubGroup()
        {
            this.ProductSubGroupDropDownList.Items.Clear();
            this.ProductSubGroupDropDownList.DataSource = this._productBL.GetListProductSubGroupForDDL();
            this.ProductSubGroupDropDownList.DataValueField = "ProductSubGrpCode";
            this.ProductSubGroupDropDownList.DataTextField = "ProductSubGrpName";
            this.ProductSubGroupDropDownList.DataBind();
            this.ProductSubGroupDropDownList.Items.Insert(0, new ListItem("[Choose One]", ""));
        }

        protected void ShowProductType()
        {
            this.ProductTypeDropDownList.Items.Clear();
            this.ProductTypeDropDownList.DataSource = this._productBL.GetListProductTypeForDDL();
            this.ProductTypeDropDownList.DataValueField = "ProductTypeCode";
            this.ProductTypeDropDownList.DataTextField = "ProductTypeName";
            this.ProductTypeDropDownList.DataBind();
            this.ProductTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", ""));
        }

        public void ShowProduct(Int32 _prmCurrentPage)
        {
            var hasil = this._reportBL.GetListDDLProductForReport(_prmCurrentPage, _maxrow, this.ProductSubGroupDropDownList.SelectedValue, this.ProductTypeDropDownList.SelectedValue, this.CategoryDropDownList.SelectedValue, this.KeywordTextBox.Text);
            this.ProductCheckBoxList.DataSource = hasil;
            this.ProductCheckBoxList.DataValueField = "ProductCode";
            this.ProductCheckBoxList.DataTextField = "ProductName";
            this.ProductCheckBoxList.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "', 'true');");

            int i = 0;
            foreach (ListItem _item in this.ProductCheckBoxList.Items)
            {
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + "ctl00_DefaultBodyContentPlaceHolder_ProductCheckBoxList_" + i.ToString() + ", '" + _item.Value + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "', 'true');");
                i++;
            }

            this.ShowPage(_prmCurrentPage);
        }

        public void ClearData()
        {
            this.StartDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.EndDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            //this.HeaderReportList1.ReportGroup = "POSPerProduct";
            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";
            this.AllCheckBox.Checked = false;
            this.CheckHiddenCashier.Value = "";
            this.TempHiddenCashier.Value = "";
            this.AllCheckBoxCashier.Checked = false;
            this.ProductSubGroupDropDownList.SelectedValue = "";
            this.ProductTypeDropDownList.SelectedValue = "";
            this.ProductCheckBoxList.ClearSelection();
            this.CashierCheckBoxList.ClearSelection();
            this.SelectionPanel.Visible = false;
            this.RangePanel.Visible = true;
            this.FromTextBox.Text = "";
            this.ToTextBox.Text = "";
            this.CategoryDropDownList.SelectedIndex = 0;
            this.CategoryDropDownListCahsier.SelectedIndex = 0;
            this.KeywordTextBox.Text = "";
            this.KeywordTextBoxCashier.Text = "";
        }

        public void SetAttribut()
        {
            this.StartDateTextBox.Attributes.Add("ReadOnly", "True");
            this.EndDateTextBox.Attributes.Add("ReadOnly", "True");
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            String _from = this.FromTextBox.Text;
            String _to = this.ToTextBox.Text;

            if (_from.Length <= _to.Length)
            {
                this.MenuPanel.Visible = false;
                this.ReportViewer1.Visible = true;
                string _product = "";
                string _cashier = "";

                if (this.AllCheckBox.Checked == true)
                {
                    _product = this.TempHidden.Value;
                }
                else
                {
                    _product = this.CheckHidden.Value;
                }

                if (this.AllCheckBoxCashier.Checked == true)
                {
                    _cashier = this.TempHiddenCashier.Value;
                }
                else
                {
                    _cashier = this.CheckHiddenCashier.Value;
                }


                DateTime _startDateTime = new DateTime(DateFormMapper.GetValue(this.StartDateTextBox.Text).Year, DateFormMapper.GetValue(this.StartDateTextBox.Text).Month, DateFormMapper.GetValue(this.StartDateTextBox.Text).Day, 0, 0, 0);
                DateTime _endDateTime = new DateTime(DateFormMapper.GetValue(this.EndDateTextBox.Text).Year, DateFormMapper.GetValue(this.EndDateTextBox.Text).Month, DateFormMapper.GetValue(this.EndDateTextBox.Text).Day, 23, 59, 59);

                //int _int = 0;
                //if (this.HeaderReportList1.SelectedValue == "Report/PosPerProduct.rdlc")
                //    _int = 1;

                ReportDataSource _reportDataSource1 = this._reportBL.POSReportListPerProduct(_startDateTime, _endDateTime, Convert.ToInt32(this.ReportTypeRadioButtonList.SelectedValue), _product, _from, _to, _cashier, this.ProductSubGroupDropDownList.SelectedValue);

                this.ReportViewer1.LocalReport.EnableExternalImages = true;
                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
                this.ReportViewer1.DataBind();

                ReportParameter[] _reportParam = new ReportParameter[9];
                _reportParam[0] = new ReportParameter("StartDate", _startDateTime.ToString(), true);
                _reportParam[1] = new ReportParameter("EndDate", _endDateTime.ToString(), true);
                _reportParam[2] = new ReportParameter("PrmCashierId", _cashier, true);
                _reportParam[3] = new ReportParameter("PrmProduct", _product, true);
                _reportParam[4] = new ReportParameter("From", this.FromTextBox.Text, true);
                _reportParam[5] = new ReportParameter("To", this.ToTextBox.Text, true);
                _reportParam[6] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                _reportParam[7] = new ReportParameter("FgReport", this.ReportTypeRadioButtonList.SelectedValue.ToString(), true);
                _reportParam[8] = new ReportParameter("PrmProductSubGroup", this.ProductSubGroupDropDownList.SelectedValue, true);
                
                this.ReportViewer1.LocalReport.SetParameters(_reportParam);
                this.ReportViewer1.LocalReport.Refresh();
            }
            else
            {
                this.WarningLabel.Text = "Product From must be less than Product To";
            }
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ViewState[this._currPageKey] = "0";

            this.ClearData();

            //this.ShowProductSubGroup();
            //this.ShowProductType();
            this.ShowProduct(0);
            this.ShowCashier(0);
            this.SetCheckBox();
            this.SetCheckBoxCashier();
        }

        protected void DataPagerTopRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this.TempHidden.Value = "";

                this.ShowProduct(Convert.ToInt32(e.CommandArgument));
                this.SetCheckBox();
            }
        }

        protected void DataPagerTopRepeater_ItemCommandCashier(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this.TempHiddenCashier.Value = "";

                this.ShowCashier(Convert.ToInt32(e.CommandArgument));
                this.SetCheckBoxCashier();
            }
        }

        protected void DataPagerTopRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                int _pageNumber = (int)e.Item.DataItem;

                NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

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

        protected void DataPagerTopRepeater_ItemDataBoundCashier(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                int _pageNumber = (int)e.Item.DataItem;

                NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

                if (Convert.ToInt32(this.ViewState[this._currPageKey]) == _pageNumber)
                {
                    TextBox _pageNumberTextbox = (TextBox)e.Item.FindControl("PageNumberTextBoxCashier");

                    _pageNumberTextbox.Text = (_pageNumber + 1).ToString();
                    _pageNumberTextbox.MaxLength = 9;
                    _pageNumberTextbox.Visible = true;

                    _pageNumberTextbox.Attributes.Add("style", "text-align: center;");
                    _pageNumberTextbox.Attributes.Add("OnKeyDown", "return NumericWithEnter();");
                }
                else
                {
                    LinkButton _pageNumberLinkButton = (LinkButton)e.Item.FindControl("PageNumberLinkButtonCashier");
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

                    if (_reqPage > this.RowCount())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCount().ToString();
                        _reqPage = Convert.ToInt32(this.RowCount()) - 1;
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

            this.ShowProduct(_reqPage);
            this.SetCheckBox();
        }

        protected void DataPagerButton_ClickCashier(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerButtonCashier.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCountCashier())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCountCashier().ToString();
                        _reqPage = Convert.ToInt32(this.RowCountCashier()) - 1;
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

            this.ShowCashier(_reqPage);
            this.SetCheckBoxCashier();
        }

        protected void SetCheckBox()
        {
            int i = 0;
            foreach (ListItem _item in this.ProductCheckBoxList.Items)
            {
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + "ctl00_DefaultBodyContentPlaceHolder_ProductCheckBoxList_" + i.ToString() + ", '" + _item.Value + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "', 'true');");

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

            foreach (ListItem _item in this.ProductCheckBoxList.Items)
            {
                if (_item.Selected == false)
                {
                    this.AllCheckBox.Checked = false;
                }
            }
        }

        protected void SetCheckBoxCashier()
        {
            int i = 0;
            foreach (ListItem _item in this.CashierCheckBoxList.Items)
            {
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHiddenCashier.ClientID + ", " + "ctl00_DefaultBodyContentPlaceHolder_CashierCheckBoxList_" + i.ToString() + ", '" + _item.Value + "', '" + _awalCashier + "', '" + _akhirCashier + "', '" + _cboxCashier + "', 'true');");

                _item.Selected = this.CheckHiddenCashier.Value.Contains(_item.Value);

                i++;

                //bound all data (on this page) to temphidden
                if (this.TempHiddenCashier.Value == "")
                {
                    this.TempHiddenCashier.Value = _item.Value;
                }
                else
                {
                    this.TempHiddenCashier.Value += "," + _item.Value;
                }
            }

            this.SetCheckAllCheckBoxCashier();
        }

        protected void SetCheckAllCheckBoxCashier()
        {
            this.AllCheckBoxCashier.Checked = true;

            foreach (ListItem _item in this.CashierCheckBoxList.Items)
            {
                if (_item.Selected == false)
                {
                    this.AllCheckBoxCashier.Checked = false;
                }
            }
        }

        protected void FilterDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.ClearData();

            if (this.FilterDropDownList.SelectedValue == "0")
            {
                this.RangePanel.Visible = true;
                this.SelectionPanel.Visible = false;
                this.CheckHidden.Value = "";
                this.TempHidden.Value = "";
            }
            else
            {
                this.FromTextBox.Text = "";
                this.ToTextBox.Text = "";
                this.SetCheckBox();
                this.RangePanel.Visible = false;
                this.SelectionPanel.Visible = true;
            }
        }

        protected void GoImageButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.RangePanel.Visible = false;
            this.SelectionPanel.Visible = true;
            this.ShowProduct(0);
            this.SetCheckBox();
        }

        protected void GoImageButton_ClickCashier(object sender, ImageClickEventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            //this.RangePanel.Visible = false;
            this.SelectionPanel.Visible = true;
            this.ShowCashier(0);
            this.SetCheckBox();
        }

        protected void ProductSubGroupDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";
            this.ShowProduct(0);
            this.SetCheckBox();

        }

        protected void ProductTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";
            this.ShowProduct(0);
            this.SetCheckBox();
        }
    }
}