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
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.Report
{
    public partial class OutstandingPurchaseOrderPerTrans : ReportBase
    {
        private ReportPurchaseBL _reportBL = new ReportPurchaseBL();
        private ProductBL _productBL = new ProductBL();
        private SupplierBL _suppBL = new SupplierBL();
        private PermissionBL _permBL = new PermissionBL();

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private int?[] _navMark2 = { null, null, null, null };
        private bool _flag2 = true;
        private bool _nextFlag2 = false;
        private bool _lastFlag2 = false;

        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);

        private int _maxrow2 = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength2 = Convert.ToInt32(ApplicationConfig.DataPagerRange);

        private string _currPageKey = "CurrentPage";
        private string _currPageKey2 = "CurrentPage";

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ProductCheckBoxList_";
        private string _akhir = "";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private string _awal2 = "ctl00_DefaultBodyContentPlaceHolder_SuppCodeCheckBoxList_";
        private string _akhir2 = "";
        private string _cbox2 = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox2";
        private string _tempHidden2 = "ctl00$DefaultBodyContentPlaceHolder$TempHidden2";

        private string _reportPath0 = "Report/OutstandingPurchaseOrderPerTrans.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDOutstandingPurchaseOrderPerTrans, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer.Attributes.Add("style", "margin-bottom: 30px;");

            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");

            if (!this.Page.IsPostBack == true)
            {
                this.StartDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.StartDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = "Outstanding Purchase Order Per Transaction Report";

                this.ViewState[this._currPageKey] = 0;
                this.ViewState[this._currPageKey2] = 0;

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.MenuPanel.Visible = true;
                this.ReportViewer.Visible = false;

                this.ShowSuppGroup();
                this.ShowSuppType();

                this.ShowProductSubGroup();
                this.ShowProductType();

                this.SetAttribute();
                this.ClearData();

                this.ShowSupplier(0);
                this.SetCheckBox2();
                this.ShowProduct(0);
                this.SetCheckBox();
            }
        }

        private void SetAttribute()
        {
            this.StartDateTextBox.Attributes.Add("ReadOnly", "True");
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
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

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox2.ClientID + ", " + this.CheckHidden2.ClientID + ", '" + _awal2 + "', '" + _akhir2 + "', '" + _tempHidden2 + "', 'true');");

            int i = 0;
            foreach (ListItem _item in this.SuppCodeCheckBoxList.Items)
            {
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden2.ClientID + ", " + _awal + i.ToString() + ", '" + _item.Value + "', '" + _awal2 + "', '" + _akhir2 + "', '" + _cbox2 + "', 'true');");
                i++;
            }

            List<MsSupplier> _listMsSupplier = this._suppBL.GetListDDLSuppForReport(_tempSuppGroup, _tempSuppType);
            this.AllHidden2.Value = "";

            foreach (MsSupplier _item in _listMsSupplier)
            {
                if (this.AllHidden2.Value == "")
                {
                    this.AllHidden2.Value = _item.SuppCode;
                }
                else
                {
                    this.AllHidden2.Value += "," + _item.SuppCode;
                }
            }

            this.ShowPage2(_prmCurrentPage);
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

        private void ShowProduct(Int32 _prmCurrentPage)
        {
            string _tempProductSubGroup = (this.ProductSubGroupDropDownList.SelectedValue == "null") ? "" : this.ProductSubGroupDropDownList.SelectedValue;
            string _tempProductType = (this.ProductTypeDropDownList.SelectedValue == "null") ? "" : this.ProductTypeDropDownList.SelectedValue;

            this.ProductCheckBoxList.ClearSelection();
            this.ProductCheckBoxList.Items.Clear();
            this.ProductCheckBoxList.DataTextField = "ProductName";
            this.ProductCheckBoxList.DataValueField = "ProductCode";
            this.ProductCheckBoxList.DataSource = this._productBL.GetListDDLProductForReport(_tempProductSubGroup, _tempProductType, _prmCurrentPage, _maxrow);
            this.ProductCheckBoxList.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "', 'true');");

            int i = 0;
            foreach (ListItem _item in this.ProductCheckBoxList.Items)
            {
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _awal + i.ToString() + ", '" + _item.Value + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "', 'true');");
                i++;
            }

            List<MsProduct> _listMsProduct = this._productBL.GetListDDLProductForReport(_tempProductSubGroup, _tempProductType);
            this.AllHidden.Value = "";

            foreach (MsProduct _item in _listMsProduct)
            {
                if (this.AllHidden.Value == "")
                {
                    this.AllHidden.Value = _item.ProductCode;
                }
                else
                {
                    this.AllHidden.Value += "," + _item.ProductCode;
                }
            }

            this.ShowPage(_prmCurrentPage);
        }

        protected void ShowProductSubGroup()
        {
            this.ProductSubGroupDropDownList.Items.Clear();
            this.ProductSubGroupDropDownList.DataSource = this._productBL.GetListProductSubGroupForDDL();
            this.ProductSubGroupDropDownList.DataValueField = "ProductSubGrpCode";
            this.ProductSubGroupDropDownList.DataTextField = "ProductSubGrpName";
            this.ProductSubGroupDropDownList.DataBind();
            this.ProductSubGroupDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowProductType()
        {
            this.ProductTypeDropDownList.Items.Clear();
            this.ProductTypeDropDownList.DataSource = this._productBL.GetListProductTypeForDDL();
            this.ProductTypeDropDownList.DataValueField = "ProductTypeCode";
            this.ProductTypeDropDownList.DataTextField = "ProductTypeName";
            this.ProductTypeDropDownList.DataBind();
            this.ProductTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
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

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ViewState[this._currPageKey] = 0;
            this.ViewState[this._currPageKey2] = 0;

            this.ClearLabel();
            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";
            this.CheckHidden2.Value = "";
            this.TempHidden2.Value = "";
            this.StartDateTextBox.Text = DateFormMapper.GetValue(now);
            this.ProductTypeDropDownList.SelectedValue = "null";
            this.ProductSubGroupDropDownList.SelectedValue = "null";
            this.ProductCheckBoxList.ClearSelection();
            this.SuppTypeDropDownList.SelectedValue = "null";
            this.SuppGroupDropDownList.SelectedValue = "null";
            this.SuppCodeCheckBoxList.ClearSelection();
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = false;
            this.ReportViewer.Visible = true;

            string _suppCode = "";
            string _productCode = "";

            if (this.GrabAllCheckBox2.Checked == true)
            {
                _suppCode = this.AllHidden2.Value;
            }
            else
            {
                if (this.AllCheckBox2.Checked == true)
                {
                    _suppCode = this.TempHidden2.Value;
                }
                else
                {
                    _suppCode = this.CheckHidden2.Value;
                }
            }

            if (this.GrabAllCheckBox.Checked == true)
            {
                _productCode = this.AllHidden.Value;
            }
            else
            {
                if (this.AllCheckBox.Checked == true)
                {
                    _productCode = this.TempHidden.Value;
                }
                else
                {
                    _productCode = this.CheckHidden.Value;
                }
            }

            DateTime _startDateTime = new DateTime(DateFormMapper.GetValue(this.StartDateTextBox.Text).Year, DateFormMapper.GetValue(this.StartDateTextBox.Text).Month, DateFormMapper.GetValue(this.StartDateTextBox.Text).Day, 23, 59, 59);

            ReportDataSource _reportDataSource1 = this._reportBL.OutstandingPurchaseOrderPerTrans(_startDateTime, _suppCode, _productCode);

            this.ReportViewer.LocalReport.DataSources.Clear();
            this.ReportViewer.LocalReport.DataSources.Add(_reportDataSource1);

            this.ReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;

            this.ReportViewer.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[5];
            _reportParam[0] = new ReportParameter("Date", _startDateTime.ToString(), true);
            _reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            _reportParam[2] = new ReportParameter("Str1", _suppCode, true);
            _reportParam[3] = new ReportParameter("Str2", _productCode, true);
            _reportParam[4] = new ReportParameter("Str3", "", false);

            this.ReportViewer.LocalReport.SetParameters(_reportParam);
            this.ReportViewer.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();

            this.ShowSupplier(0);
            this.SetCheckBox2();
            this.ShowProduct(0);
            this.SetCheckBox();
        }

        private double RowCountProduct()
        {
            double _result1 = 0;
            string _tempProductSubGroup = (this.ProductSubGroupDropDownList.SelectedValue == "null") ? "" : this.ProductSubGroupDropDownList.SelectedValue;
            string _tempProductType = (this.ProductTypeDropDownList.SelectedValue == "null") ? "" : this.ProductTypeDropDownList.SelectedValue;

            _result1 = this._productBL.RowsCountProductReport(_tempProductSubGroup, _tempProductType);
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
            double q = this.RowCountProduct();

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

                this.ShowProduct(Convert.ToInt32(e.CommandArgument));
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

                    if (_reqPage > this.RowCountProduct())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCountProduct().ToString();
                        _reqPage = Convert.ToInt32(this.RowCountProduct()) - 1;
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

        protected void SetCheckBox()
        {
            int i = 0;
            foreach (ListItem _item in this.ProductCheckBoxList.Items)
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

            foreach (ListItem _item in this.ProductCheckBoxList.Items)
            {
                if (_item.Selected == false)
                {
                    this.AllCheckBox.Checked = false;
                }
            }

            if (this.ProductCheckBoxList.Items.Count < 1)
            {
                this.AllCheckBox.Checked = false;
            }
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

        private void ShowPage2(Int32 _prmCurrentPage)
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
                this._navMark.CopyTo(_navMarkBackup, 0);
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

        protected void DataPagerTopRepeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager2")
            {
                this.ViewState[this._currPageKey2] = Convert.ToInt32(e.CommandArgument);

                this.TempHidden2.Value = "";

                this.ShowSupplier(Convert.ToInt32(e.CommandArgument));
                this.SetCheckBox2();
            }
        }

        protected void DataPagerTopRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                int _pageNumber = (int)e.Item.DataItem;

                if (Convert.ToInt32(this.ViewState[this._currPageKey2]) == _pageNumber)
                {
                    TextBox _pageNumberTextbox = (TextBox)e.Item.FindControl("PageNumberTextBox2");

                    _pageNumberTextbox.Text = (_pageNumber + 1).ToString();
                    _pageNumberTextbox.MaxLength = 9;
                    _pageNumberTextbox.Visible = true;

                    _pageNumberTextbox.Attributes.Add("style", "text-align: center;");
                    _pageNumberTextbox.Attributes.Add("OnKeyDown", "return NumericWithEnter();");
                }
                else
                {
                    LinkButton _pageNumberLinkButton = (LinkButton)e.Item.FindControl("PageNumberLinkButton2");
                    _pageNumberLinkButton.CommandName = "DataPager2";
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

            this.ViewState[this._currPageKey2] = _reqPage;

            this.ShowSupplier(_reqPage);
            this.SetCheckBox2();
        }

        protected void SetCheckBox2()
        {
            int i = 0;
            foreach (ListItem _item in this.SuppCodeCheckBoxList.Items)
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

        protected void SetCheckAllCheckBox2()
        {
            this.AllCheckBox2.Checked = true;

            foreach (ListItem _item in this.SuppCodeCheckBoxList.Items)
            {
                if (_item.Selected == false)
                {
                    this.AllCheckBox2.Checked = false;
                }
            }

            if (this.SuppCodeCheckBoxList.Items.Count < 1)
            {
                this.AllCheckBox2.Checked = false;
            }
        }
    }
}