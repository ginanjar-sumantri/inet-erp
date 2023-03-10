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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Report
{
    public partial class BeginningStock : ReportBase
    {
        private ReportStockControlBL _report = new ReportStockControlBL();
        private ProductBL _productBL = new ProductBL();
        private WarehouseBL _wrhsBL = new WarehouseBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _reportPath0 = "Report/BeginningStockSummaryPerWarehouse.rdlc";
        private string _reportPath1 = "Report/BeginningStockSummaryPerProduct.rdlc";
        private string _reportPath2 = "Report/BeginningStockDetailPerProduct.rdlc";

        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private int?[] _navMark2 = { null, null, null, null };
        private bool _flag2 = true;
        private bool _nextFlag2 = false;
        private bool _lastFlag2 = false;

        private string _currPageKey = "CurrentPage";
        private string _currPageKey2 = "CurrentPage2";

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ProductCodeCheckBoxList_";
        private string _akhir = "";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private string _awal2 = "ctl00_DefaultBodyContentPlaceHolder_WarehouseCheckBoxList_";
        private string _akhir2 = "";
        private string _cbox2 = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox2";
        private string _tempHidden2 = "ctl00$DefaultBodyContentPlaceHolder$TempHidden2";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDBeginningStock, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer1.Attributes.Add("style", "margin-bottom: 30px;");

            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");
            this.DataPagerButton2.Attributes.Add("Style", "visibility: hidden;");

            if (!Page.IsPostBack == true)
            {
                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = "Beginning Stock Report";

                this.ViewState[this._currPageKey] = 0;
                this.ViewState[this._currPageKey2] = 0;

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.MenuPanel.Visible = true;
                this.ReportViewer1.Visible = false;

                this.ShowProductGroup();
                this.ShowProductSubGroup();
                this.ShowProductType();
                this.ShowWarehouseGroup();
                this.ShowWarehouseArea();
                
                this.ClearData();

                this.ShowProduct(0);
                this.SetCheckBox();
                this.ShowWarehouse(0);
                this.SetCheckBox2();
            }
        }

        private void ShowProduct(Int32 _prmCurrentPage)
        {
            string _tempProductGroup = (this.ProductGroupDDL.SelectedValue == "null") ? "" : this.ProductGroupDDL.SelectedValue;
            string _tempProductSubGroup = (this.ProductSubGroupDDL.SelectedValue == "null") ? "" : this.ProductSubGroupDDL.SelectedValue;
            string _tempProductType = (this.ProductTypeDDL.SelectedValue == "null") ? "" : this.ProductTypeDDL.SelectedValue;

            this.ProductCodeCheckBoxList.ClearSelection();
            this.ProductCodeCheckBoxList.Items.Clear();
            this.ProductCodeCheckBoxList.DataTextField = "ProductName";
            this.ProductCodeCheckBoxList.DataValueField = "ProductCode";
            this.ProductCodeCheckBoxList.DataSource = this._productBL.GetListForDDLProduct(_tempProductGroup, _tempProductSubGroup, _tempProductType, _prmCurrentPage, _maxrow);
            this.ProductCodeCheckBoxList.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "', 'true');");

            int i = 0;
            foreach (ListItem _item in this.ProductCodeCheckBoxList.Items)
            {
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _awal + i.ToString() + ", '" + _item.Value + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "', 'true');");
                i++;
            }

            List<MsProduct> _listMsProduct = this._productBL.GetListForDDLProduct(_tempProductGroup, _tempProductSubGroup, _tempProductType);
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

        protected void ShowProductGroup()
        {
            this.ProductGroupDDL.Items.Clear();
            this.ProductGroupDDL.DataSource = this._productBL.GetListForDDL();
            this.ProductGroupDDL.DataValueField = "ProductGrpCode";
            this.ProductGroupDDL.DataTextField = "ProductGrpName";
            this.ProductGroupDDL.DataBind();
            this.ProductGroupDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowProductSubGroup()
        {
            this.ProductSubGroupDDL.Items.Clear();
            if (this.ProductGroupDDL.SelectedValue != "null")
            {
                this.ProductSubGroupDDL.DataSource = this._productBL.GetListProductSubGroupForDDL(this.ProductGroupDDL.SelectedValue);
            }
            else
            {
                this.ProductSubGroupDDL.DataSource = this._productBL.GetListProductSubGroupForDDL();
            }
            this.ProductSubGroupDDL.DataValueField = "ProductSubGrpCode";
            this.ProductSubGroupDDL.DataTextField = "ProductSubGrpName";
            this.ProductSubGroupDDL.DataBind();
            this.ProductSubGroupDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowProductType()
        {
            this.ProductTypeDDL.Items.Clear();
            this.ProductTypeDDL.DataSource = this._productBL.GetListProductTypeForDDL();
            this.ProductTypeDDL.DataValueField = "ProductTypeCode";
            this.ProductTypeDDL.DataTextField = "ProductTypeName";
            this.ProductTypeDDL.DataBind();
            this.ProductTypeDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowWarehouse(Int32 _prmCurrentPage)
        {
            string _tempWrhsGroup = (this.WrhsGroupDropDownList.SelectedValue == "null") ? "" : this.WrhsGroupDropDownList.SelectedValue;
            string _tempWrhsArea = (this.WrhsAreaDropDownList.SelectedValue == "null") ? "" : this.WrhsAreaDropDownList.SelectedValue;

            var hasil = this._wrhsBL.GetListDDLWrhsForReport(_tempWrhsGroup, _tempWrhsArea, _prmCurrentPage, _maxrow);

            this.WarehouseCheckBoxList.ClearSelection();
            this.WarehouseCheckBoxList.Items.Clear();
            this.WarehouseCheckBoxList.DataSource = hasil;
            this.WarehouseCheckBoxList.DataValueField = "WrhsCode";
            this.WarehouseCheckBoxList.DataTextField = "WrhsName";
            this.WarehouseCheckBoxList.DataBind();

            this.AllCheckBox2.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox2.ClientID + ", " + this.CheckHidden2.ClientID + ", '" + _awal2 + "', '" + _akhir2 + "', '" + _tempHidden2 + "', 'true');");

            int i = 0;
            foreach (ListItem _item in this.WarehouseCheckBoxList.Items)
            {
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden2.ClientID + ", " + _awal2 + i.ToString() + ", '" + _item.Value + "', '" + _awal2 + "', '" + _akhir2 + "', '" + _cbox2 + "', 'true');");
                i++;
            }

            List<MsWarehouse> _listMsWarehouse = this._wrhsBL.GetListDDLWrhsForReport(_tempWrhsGroup, _tempWrhsArea);
            this.AllHidden2.Value = "";

            foreach (MsWarehouse _item in _listMsWarehouse)
            {
                if (this.AllHidden2.Value == "")
                {
                    this.AllHidden2.Value = _item.WrhsCode;
                }
                else
                {
                    this.AllHidden2.Value += "," + _item.WrhsCode;
                }
            }

            this.ShowPage2(_prmCurrentPage);
        }

        protected void ShowWarehouseGroup()
        {
            this.WrhsGroupDropDownList.Items.Clear();
            this.WrhsGroupDropDownList.DataSource = this._wrhsBL.GetListWrhsGroupForDDL();
            this.WrhsGroupDropDownList.DataValueField = "WrhsGroupCode";
            this.WrhsGroupDropDownList.DataTextField = "WrhsGroupName";
            this.WrhsGroupDropDownList.DataBind();
            this.WrhsGroupDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowWarehouseArea()
        {
            this.WrhsAreaDropDownList.Items.Clear();
            this.WrhsAreaDropDownList.DataSource = this._wrhsBL.GetListWrhsAreaForDDL();
            this.WrhsAreaDropDownList.DataValueField = "WrhsAreaCode";
            this.WrhsAreaDropDownList.DataTextField = "WrhsAreaName";
            this.WrhsAreaDropDownList.DataBind();
            this.WrhsAreaDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void WrhsGroupDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey2] = 0;

            this.CheckHidden2.Value = "";
            this.TempHidden2.Value = "";
            this.ShowWarehouse(0);
            this.SetCheckBox();
            this.SetCheckBox2();
        }

        protected void WrhsAreaDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey2] = 0;

            this.CheckHidden2.Value = "";
            this.TempHidden2.Value = "";
            this.ShowWarehouse(0);
            this.SetCheckBox();
            this.SetCheckBox2();
        }

        public void ClearData()
        {
            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";
            this.CheckHidden2.Value = "";
            this.TempHidden2.Value = "";

            this.ProductGroupDDL.SelectedValue = "null";
            this.ProductSubGroupDDL.SelectedValue = "null";
            this.ProductTypeDDL.SelectedValue = "null";
            this.ProductCodeCheckBoxList.ClearSelection();
            this.WrhsGroupDropDownList.SelectedValue = "null";
            this.WrhsAreaDropDownList.SelectedValue = "null";
            this.WarehouseCheckBoxList.ClearSelection();

            this.AllCheckBox.Checked = false;
            this.AllCheckBox2.Checked = false;
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = false;
            this.ReportViewer1.Visible = true;

            string _productCode = "";
            string _wrhsCode = "";
            string _fgReport = "";

            if (this.GrabAllCheckBox2.Checked == true)
            {
                _wrhsCode = this.AllHidden2.Value;
            }
            else
            {
                if (this.AllCheckBox2.Checked == true)
                {
                    _wrhsCode = this.TempHidden2.Value;
                }
                else
                {
                    _wrhsCode = this.CheckHidden2.Value;
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

            ReportDataSource _reportDataSource1 = this._report.BeginningStock(_wrhsCode, _productCode, this.FgReportDDL.SelectedValue);

            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

            if (this.FgReportDDL.SelectedValue == "0")
            {
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
            }
            else if (this.FgReportDDL.SelectedValue == "1")
            {
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
            }
            else if (this.FgReportDDL.SelectedValue == "2")
            {
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath2;
            }

            this.ReportViewer1.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[4];
            _reportParam[0] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            _reportParam[1] = new ReportParameter("Str1", _wrhsCode, true);
            _reportParam[2] = new ReportParameter("Str2", _productCode, true);
            _reportParam[3] = new ReportParameter("FgReport", this.FgReportDDL.SelectedValue, true);

            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ViewState[this._currPageKey] = "0";
            this.ViewState[this._currPageKey2] = "0";

            this.ShowProductGroup();
            this.ShowProductSubGroup();
            this.ShowProductType();
            this.ShowWarehouseGroup();
            this.ShowWarehouseArea();

            this.ClearData();

            this.ShowProduct(0);
            this.SetCheckBox();
            this.ShowWarehouse(0);
            this.SetCheckBox2();
        }

        private double RowCountProduct()
        {
            double _result1 = 0;
            string _tempProductGroup = (this.ProductGroupDDL.SelectedValue == "null") ? "" : this.ProductGroupDDL.SelectedValue;
            string _tempProductSubGroup = (this.ProductSubGroupDDL.SelectedValue == "null") ? "" : this.ProductSubGroupDDL.SelectedValue;
            string _tempProductType = (this.ProductTypeDDL.SelectedValue == "null") ? "" : this.ProductTypeDDL.SelectedValue;

            _result1 = this._productBL.RowsCountProductForReport(_tempProductGroup, _tempProductSubGroup, _tempProductType);
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
                this.SetCheckBox2();
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
            this.SetCheckBox2();
        }

        protected void SetCheckBox()
        {
            int i = 0;
            foreach (ListItem _item in this.ProductCodeCheckBoxList.Items)
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

            foreach (ListItem _item in this.ProductCodeCheckBoxList.Items)
            {
                if (_item.Selected == false)
                {
                    this.AllCheckBox.Checked = false;
                }
            }

            if (this.ProductCodeCheckBoxList.Items.Count < 1)
            {
                this.AllCheckBox.Checked = false;
            }
        }

        private double RowCountWarehouse()
        {
            double _result1 = 0;
            string _tempWrhsGroup = (this.WrhsGroupDropDownList.SelectedValue == "null") ? "" : this.WrhsGroupDropDownList.SelectedValue;
            string _tempWrhsArea = (this.WrhsAreaDropDownList.SelectedValue == "null") ? "" : this.WrhsAreaDropDownList.SelectedValue;

            _result1 = this._wrhsBL.RowsCountWarehouseReport(_tempWrhsGroup, _tempWrhsArea);
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
            double q = this.RowCountWarehouse();

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
                        this._flag = true;
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

        protected void DataPagerTopRepeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager2")
            {
                this.ViewState[this._currPageKey2] = Convert.ToInt32(e.CommandArgument);

                this.TempHidden2.Value = "";

                this.ShowWarehouse(Convert.ToInt32(e.CommandArgument));
                this.SetCheckBox();
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

        protected void DataPagerButton2_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerTopRepeater2.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCountWarehouse())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCountWarehouse().ToString();
                        _reqPage = Convert.ToInt32(this.RowCountWarehouse()) - 1;
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

            this.ShowWarehouse(_reqPage);
            this.SetCheckBox();
            this.SetCheckBox2();
        }

        protected void SetCheckBox2()
        {
            int i = 0;
            foreach (ListItem _item in this.WarehouseCheckBoxList.Items)
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

            foreach (ListItem _item in this.WarehouseCheckBoxList.Items)
            {
                if (_item.Selected == false)
                {
                    this.AllCheckBox2.Checked = false;
                }
            }

            if (this.WarehouseCheckBoxList.Items.Count < 1)
            {
                this.AllCheckBox2.Checked = false;
            }
        }

        protected void ProductGroupDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";
            this.ShowProductSubGroup();
            this.ShowProduct(0);
            this.SetCheckBox();
            this.SetCheckBox2();
        }

        protected void ProductSubGroupDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";
            this.ShowProduct(0);
            this.SetCheckBox();
            this.SetCheckBox2();
        }

        protected void ProductTypeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";
            this.ShowProduct(0);
            this.SetCheckBox();
            this.SetCheckBox2();
        }
    }
}