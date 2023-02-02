using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Collections.Generic;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Report
{
    public partial class SellingPriceList : ReportBase
    {
        private ReportStockControlBL _reportStokControlBL = new ReportStockControlBL();
        private PermissionBL _permBL = new PermissionBL();
        private ProductBL _productBL = new ProductBL();

        private string _reportPath0 = "Report/SellingPriceList.rdlc";
        private string _reportPath1 = "Report/SellingPriceListByProductSubGroup.rdlc";

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private string _currPageKey = "CurrentPage";

        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ProductCodeCheckBoxList_";
        private string _akhir = "";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDNCPSoldListByCategory, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer1.Attributes.Add("style", "margin-bottom: 30px;");

            if (!Page.IsPostBack == true)
            {
                //this.HeaderReportList1.ReportGroup = "STCPriceList";
                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = "Price List Report";

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.SearchImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/go.jpg";

                this.MenuPanel.Visible = true;
                this.ReportViewer1.Visible = false;
                this.SelectionPanel.Visible = false;

                this.ShowProductSubGroup();
                this.ShowProduct(0);
                this.SetCheckBox();

            }
        }

        public void ClearData()
        {
            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";

            this.ProductSubGroupDDL.SelectedValue = "null";
            this.ProductCodeCheckBoxList.ClearSelection();

            this.AllCheckBox.Checked = false;
        }

        private double RowCountProduct()
        {
            double _result1 = 0;
            string _tempProductSubGroup = (this.ProductSubGroupDDL.SelectedValue == "null") ? "" : this.ProductSubGroupDDL.SelectedValue;

            _result1 = this._productBL.RowsCountProductForReportPriceList(_tempProductSubGroup, this.CategoryDropDownList.SelectedValue, this.KeywordTextBox.Text);
            _result1 = System.Math.Ceiling(_result1 / (double)_maxrow);

            return _result1;
        }

        protected void ShowProductSubGroup()
        {
            this.ProductSubGroupDDL.DataSource = this._productBL.GetListProductSubGroupForDDL();
            this.ProductSubGroupDDL.DataValueField = "ProductSubGrpCode";
            this.ProductSubGroupDDL.DataTextField = "ProductSubGrpName";
            this.ProductSubGroupDDL.DataBind();
            this.ProductSubGroupDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowProduct(Int32 _prmCurrentPage)
        {
            string _tempProductSubGroup = (this.ProductSubGroupDDL.SelectedValue == "null") ? "" : this.ProductSubGroupDDL.SelectedValue;

            this.ProductCodeCheckBoxList.ClearSelection();
            this.ProductCodeCheckBoxList.Items.Clear();
            this.ProductCodeCheckBoxList.DataTextField = "ProductName";
            this.ProductCodeCheckBoxList.DataValueField = "ProductCode";
            this.ProductCodeCheckBoxList.DataSource = this._productBL.GetListForDDLProductForReportPriceList(this.ProductSubGroupDDL.SelectedValue, this.CategoryDropDownList.SelectedValue, this.KeywordTextBox.Text, _prmCurrentPage, _maxrow);
            this.ProductCodeCheckBoxList.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "', 'true');");

            int i = 0;
            foreach (ListItem _item in this.ProductCodeCheckBoxList.Items)
            {
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _awal + i.ToString() + ", '" + _item.Value + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "', 'true');");
                i++;
            }

            List<MsProduct> _listMsProduct = this._productBL.GetListForDDLProductForReportPriceList(_tempProductSubGroup);
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

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = false;
            this.ReportViewer1.Visible = true;

            string _productCode = "";
            string _startCode = "";
            string _endCode = "";

            if (this.ByDDL.SelectedValue == "range")
            {
                _startCode = (this.FromTextBox.Text == "") ? "000" : this.FromTextBox.Text;
                _endCode = (this.ToTextBox.Text == "") ? "zzz" : this.ToTextBox.Text;
            }
            
            
            int _fgReport = Convert.ToInt32(this.ReportDDL.SelectedValue);
            string _subGroup = (this.ProductSubGroupDDL.SelectedValue == "null") ? "" : this.ProductSubGroupDDL.SelectedValue;

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

            //string _startPriceGroup = (this.StartPriceGroupTextBox.Text == "") ? "000" : this.StartPriceGroupTextBox.Text;
            //string _endPriceGroup = (this.EndPriceGroupTextBox.Text == "") ? "zzz" : this.EndPriceGroupTextBox.Text;
            //string _startPrice = (this.StartPriceTextBox.Text == "") ? "0" : this.StartPriceTextBox.Text;
            //string _endPrice = (this.EndPriceTextBox.Text == "") ? "9999" : this.EndPriceTextBox.Text;

            ReportDataSource _reportDataSource1 = this._reportStokControlBL.SellingPriceList(_subGroup, _productCode, _startCode, _endCode, _fgReport);

            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

            if (this.TypeDDL.SelectedValue == "1")
            {
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
            }
            else if (this.TypeDDL.SelectedValue == "2")
            {
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
            }

            this.ReportViewer1.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[6];
            _reportParam[0] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            _reportParam[1] = new ReportParameter("FgReport", _fgReport.ToString(), true);
            _reportParam[2] = new ReportParameter("PrmProduct", _productCode, true);
            _reportParam[3] = new ReportParameter("From", _startCode, true);
            _reportParam[4] = new ReportParameter("To", _endCode, true);
            _reportParam[5] = new ReportParameter("PrmProductSubGroup", _subGroup, true);

            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void ByDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ByDDL.SelectedValue == "range")
            {
                this.RangePanel.Visible = true;
                this.SelectionPanel.Visible = false;

                this.ClearData();
            }
            else if (this.ByDDL.SelectedValue == "check")
            {
                this.RangePanel.Visible = false;
                this.SelectionPanel.Visible = true;

                this.ClearData();
            }
        }

        protected void ProductSubGroupDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";
            this.ShowProduct(0);
            this.SetCheckBox();
        }

        protected void SearchImageButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.ClearLabel();

            this.ShowProduct(0);
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
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

    }
}