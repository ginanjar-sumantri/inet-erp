using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.Report
{
    public partial class PurchaseOrderPerProduct : ReportBase
    {
        private ReportPurchaseBL _reportBL = new ReportPurchaseBL();
        private SupplierBL _suppBL = new SupplierBL();
        private PermissionBL _permBL = new PermissionBL();
        private ProductBL _productBL = new ProductBL();

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);

        private string _currPageKey = "CurrentPage";
        private string _currPageKey1 = "CurrentPage1";

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_SuppCodeCheckBoxList_";
        private string _akhir = "";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private string _awal1 = "ctl00_DefaultBodyContentPlaceHolder_ProductCodeCheckBoxList_";
        private string _akhir1 = "";
        private string _cbox1 = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox1";
        private string _tempHidden1 = "ctl00$DefaultBodyContentPlaceHolder$TempHidden1";

        private string _reportPath0 = "Report/PurchaseOrderPerProductSummaryPerProduct.rdlc";
        private string _reportPath1 = "Report/PurchaseOrderPerProductSummaryPerProductSupp.rdlc";
        private string _reportPath2 = "Report/PurchaseOrderPerProductDetailPerProductSupp.rdlc";
        private string _reportPath3 = "Report/PurchaseOrderPerProductSummary.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDPurchaseOrderPerProduct, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.ReportViewer.Attributes.Add("style", "margin-bottom: 30px;");

            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");
            this.DataPagerButton1.Attributes.Add("Style", "visibility: hidden;");

            if (!this.Page.IsPostBack == true)
            {
                this.StartDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.StartDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.EndDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.EndDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                this.PageTitleLiteral.Text = "Purchase Order Per Product Report";

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SearchImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/go.jpg";

                this.MenuPanel.Visible = true;
                this.ReportViewer.Visible = false;

                this.ShowSuppGroup();
                this.ShowSuppType();

                this.SetAttribute();
                this.ClearData();

                this.ShowProductGroup();
                this.ShowProductSubGroup();
                this.ShowProductType();
                this.ShowProduct(0);

                this.ShowSupplier(0);
                this.SetCheckBox();
                this.SetCheckBox1();
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
            this.ProductCodeCheckBoxList.DataSource = this._productBL.GetListForDDLProduct(_tempProductGroup, _tempProductSubGroup, _tempProductType, this.CategoryDropDownList.SelectedValue, this.KeywordTextBox.Text, _prmCurrentPage, _maxrow);
            this.ProductCodeCheckBoxList.DataBind();

            this.AllCheckBox1.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox1.ClientID + ", " + this.CheckHidden1.ClientID + ", '" + _awal1 + "', '" + _akhir1 + "', '" + _tempHidden1 + "', 'true');");

            int i = 0;
            foreach (ListItem _item in this.ProductCodeCheckBoxList.Items)
            {
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden1.ClientID + ", " + _awal1 + i.ToString() + ", '" + _item.Value + "', '" + _awal1 + "', '" + _akhir1 + "', '" + _cbox1 + "', 'true');");
                i++;
            }

            List<MsProduct> _listMsProduct = this._productBL.GetListForDDLProduct(_tempProductGroup, _tempProductSubGroup, _tempProductType);
            this.AllHidden.Value = "";

            foreach (MsProduct _item in _listMsProduct)
            {
                if (this.AllHidden1.Value == "")
                {
                    this.AllHidden1.Value = _item.ProductCode;
                }
                else
                {
                    this.AllHidden1.Value += "," + _item.ProductCode;
                }
            }

            this.ShowPage1(_prmCurrentPage);
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

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.StartDateTextBox.Attributes.Add("ReadOnly", "True");
            this.EndDateTextBox.Attributes.Add("ReadOnly", "True");
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

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ClearLabel();
            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";
            this.StartDateTextBox.Text = DateFormMapper.GetValue(now);
            this.EndDateTextBox.Text = DateFormMapper.GetValue(now);
            this.SuppTypeDropDownList.SelectedValue = "null";
            this.SuppGroupDropDownList.SelectedValue = "null";
            //this.FgTypeDropDownList.SelectedValue = "null";
            this.HeaderReportList1.ReportGroup = "POPerProduct";
        }

        protected void DataPagerTopRepeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey1] = Convert.ToInt32(e.CommandArgument);

                this.TempHidden1.Value = "";

                this.ShowProduct(Convert.ToInt32(e.CommandArgument));
                this.SetCheckBox1();
                this.SetCheckBox();
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

        protected void DataPagerButton1_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerTopRepeater1.Controls)
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

            this.ViewState[this._currPageKey1] = _reqPage;

            this.ShowProduct(_reqPage);
            this.SetCheckBox1();
            this.SetCheckBox();
        }

        protected void SetCheckBox1()
        {
            int i = 0;
            foreach (ListItem _item in this.ProductCodeCheckBoxList.Items)
            {
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _awal1 + i.ToString() + ", '" + _item.Value + "', '" + _awal1 + "', '" + _akhir1 + "', '" + _cbox1 + "', 'true');");

                _item.Selected = this.CheckHidden.Value.Contains(_item.Value);

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

        protected void SetCheckAllCheckBox1()
        {
            this.AllCheckBox.Checked = true;

            foreach (ListItem _item in this.ProductCodeCheckBoxList.Items)
            {
                if (_item.Selected == false)
                {
                    this.AllCheckBox1.Checked = false;
                }
            }

            if (this.ProductCodeCheckBoxList.Items.Count < 1)
            {
                this.AllCheckBox1.Checked = false;
            }
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

        private void ShowPage1(Int32 _prmCurrentPage)
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
                this.DataPagerTopRepeater1.DataSource = from _query in _pageNumber
                                                       select _query;
                this.DataPagerTopRepeater1.DataBind();

                _flag = true;
                _nextFlag = false;
                _lastFlag = false;
                _navMark = _navMarkBackup;
            }
            else
            {
                this.DataPagerTopRepeater1.DataSource = from _query in _pageNumber
                                                       select _query;
                this.DataPagerTopRepeater1.DataBind();
            }
        }

        protected void ProductGroupDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey1] = 0;

            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";
            this.ShowProductSubGroup();
            this.ShowProduct(0);
            this.SetCheckBox();
            this.SetCheckBox1();
        }

        protected void ProductSubGroupDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey1] = 0;

            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";
            this.ShowProduct(0);
            this.SetCheckBox();
            this.SetCheckBox1();
        }

        protected void ProductTypeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey1] = 0;

            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";
            this.ShowProduct(0);
            this.SetCheckBox();
            this.SetCheckBox1();
        }

        protected void SearchImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            this.ViewState[this._currPageKey1] = 0;

            this.ClearLabel();

            this.ShowProduct(0);
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = false;
            this.ReportViewer.Visible = true;

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

            if (this.SelectDropDownList.SelectedValue == "0")
            {
                if (this.SuppCodeFromTextBox.Text == "")
                    this.SuppCodeFromTextBox.Text = "000";
                if (this.SuppCodeToTextBox.Text == "")
                    this.SuppCodeToTextBox.Text = "zzz";

                if (this.ProductFromTextBox.Text == "")
                    this.ProductFromTextBox.Text = "000";
                if (this.ProductToTextBox.Text == "")
                    this.ProductToTextBox.Text = "zzz";
            }

            DateTime _startDateTime = new DateTime(DateFormMapper.GetValue(this.StartDateTextBox.Text).Year, DateFormMapper.GetValue(this.StartDateTextBox.Text).Month, DateFormMapper.GetValue(this.StartDateTextBox.Text).Day, 0, 0, 0);
            DateTime _endDateTime = new DateTime(DateFormMapper.GetValue(this.EndDateTextBox.Text).Year, DateFormMapper.GetValue(this.EndDateTextBox.Text).Month, DateFormMapper.GetValue(this.EndDateTextBox.Text).Day, 23, 59, 59);

            ReportDataSource _reportDataSource1 = this._reportBL.PurchaseOrderPerProduct(_startDateTime, _endDateTime, _suppCode, Convert.ToInt32(this.HeaderReportList1.SelectedIndex), Convert.ToInt32(this.GroupByDropDownList.SelectedValue), Convert.ToInt16(this.SelectDropDownList.SelectedValue), this.SuppCodeFromTextBox.Text, this.SuppCodeToTextBox.Text, this.ProductFromTextBox.Text, this.ProductToTextBox.Text);

            this.ReportViewer.LocalReport.DataSources.Clear();
            this.ReportViewer.LocalReport.DataSources.Add(_reportDataSource1);

            this.ReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + this.HeaderReportList1.SelectedValue;

            //if (this.FgTypeDropDownList.SelectedValue == "0")
            //{
            //    this.ReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
            //}
            //else if (this.FgTypeDropDownList.SelectedValue == "1")
            //{
            //    this.ReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
            //}
            //else if (this.FgTypeDropDownList.SelectedValue == "2")
            //{
            //    this.ReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath2;
            //}
            //else if (this.FgTypeDropDownList.SelectedValue == "3")
            //{
            //    this.ReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath3;
            //}

            this.ReportViewer.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[13];
            _reportParam[0] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            _reportParam[1] = new ReportParameter("Start", _startDateTime.ToString(), true);
            _reportParam[2] = new ReportParameter("End", _endDateTime.ToString(), true);
            _reportParam[3] = new ReportParameter("Str1", _suppCode, true);
            _reportParam[4] = new ReportParameter("Str2", "", false);
            _reportParam[5] = new ReportParameter("Str3", "", false);
            _reportParam[6] = new ReportParameter("FgReport", this.HeaderReportList1.SelectedIndex, false);
            _reportParam[7] = new ReportParameter("FgCurr", this.GroupByDropDownList.SelectedValue, false);
            _reportParam[8] = new ReportParameter("FgFilter", this.SelectDropDownList.SelectedValue, false);
            _reportParam[9] = new ReportParameter("FromSupplier", this.SuppCodeFromTextBox.Text, false);
            _reportParam[10] = new ReportParameter("ToSupplier", this.SuppCodeToTextBox.Text, false);
            _reportParam[11] = new ReportParameter("FromProduct", this.ProductFromTextBox.Text, false);
            _reportParam[12] = new ReportParameter("ToProduct", this.ProductToTextBox.Text, false);

            this.ReportViewer.LocalReport.SetParameters(_reportParam);
            this.ReportViewer.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();

            this.ShowSupplier(0);
            this.SetCheckBox();
            this.SetCheckBox1();
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
                this.SetCheckBox1();
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
            this.SetCheckBox1();
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
            //this.AllCheckBox.Checked = true;

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

        protected void SelectDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearLabel();

            if (this.SelectDropDownList.SelectedValue == "0")
            {
                this.RangePanel.Visible = true;
                this.SelectionPanel.Visible = false;
                this.ViewState[this._currPageKey1] = 0;
                this.ViewState[this._currPageKey] = 0;

                this.CheckHidden.Value = "";
                this.TempHidden.Value = "";

                this.CheckHidden1.Value = "";
                this.TempHidden1.Value = "";

                this.ShowSuppGroup();
                this.ShowSupplier(0);
                this.ShowSuppType();

                this.ShowProductGroup();
                this.ShowProduct(0);
                this.ShowProductType();
            }
            else
            {
                this.RangePanel.Visible = false;
                this.SelectionPanel.Visible = true;
                this.ViewState[this._currPageKey1] = 0;
                this.ViewState[this._currPageKey] = 0;

                this.CheckHidden.Value = "";
                this.TempHidden.Value = "";
                this.CheckHidden1.Value = "";
                this.TempHidden1.Value = "";

                this.ShowSuppGroup();
                this.ShowSupplier(0);
                this.ShowSuppType();

                this.ShowProductGroup();
                this.ShowProduct(0);
                this.ShowProductType();
            }
        }
    }
}