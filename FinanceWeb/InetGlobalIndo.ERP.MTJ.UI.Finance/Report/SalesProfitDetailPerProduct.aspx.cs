using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.Report
{
    public partial class SalesProfitDetailPerProduct : ReportBase
    {
        private ReportFinanceBL _reportBL = new ReportFinanceBL();
        private ProductBL _productBL = new ProductBL();
        private PermissionBL _permBL = new PermissionBL();

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);

        private string _currPageKey = "CurrentPage";

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ProductCheckBoxList_";
        private string _akhir = "";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private string _reportPath0 = "Report/SalesProfitDetailPerProduct.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDSalesProfitDetailPerProduct, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

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
                this.EndDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.EndDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = "Sales Profit Detail Per Product";

                this.ViewState[this._currPageKey] = 0;

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.MenuPanel.Visible = true;
                this.ReportViewer.Visible = false;

                this.ShowProductSubGroupCBL();

                this.ShowProductSubGroup();
                this.ShowProductType();

                this.SetAttribute();
                this.ClearData();

                this.ShowProduct(0);
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

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ViewState[this._currPageKey] = 0;

            this.ClearLabel();
            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";
            this.StartDateTextBox.Text = DateFormMapper.GetValue(now);
            this.EndDateTextBox.Text = DateFormMapper.GetValue(now);
            this.ProductTypeDropDownList.SelectedValue = "null";
            this.ProductSubGroupDropDownList.SelectedValue = "null";
            this.ProductSubGroupCheckBoxList.ClearSelection();
            this.ProductCheckBoxList.ClearSelection();
        }

        protected void ShowProductSubGroupCBL()
        {
            this.ProductSubGroupCheckBoxList.ClearSelection();
            this.ProductSubGroupCheckBoxList.Items.Clear();
            this.ProductSubGroupCheckBoxList.DataSource = this._productBL.GetListProductSubGroupForDDL();
            this.ProductSubGroupCheckBoxList.DataValueField = "ProductSubGrpCode";
            this.ProductSubGroupCheckBoxList.DataTextField = "ProductSubGrpName";
            this.ProductSubGroupCheckBoxList.DataBind();
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

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            int _fgReport = 0;
            this.MenuPanel.Visible = false;
            this.ReportViewer.Visible = true;

            string _productSubGroupCode = "";
            string _productCode = "";

            var _hasilProductSubGroup = this._productBL.GetListProductSubGroupForDDL();

            for (var i = 0; i < _hasilProductSubGroup.Count(); i++)
            {
                if (this.ProductSubGroupCheckBoxList.Items[i].Selected == true)
                {
                    if (_productSubGroupCode == "")
                    {
                        _productSubGroupCode += this.ProductSubGroupCheckBoxList.Items[i].Value;
                    }
                    else
                    {
                        _productSubGroupCode += "," + this.ProductSubGroupCheckBoxList.Items[i].Value;
                    }
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

            DateTime _startDateTime = new DateTime(DateFormMapper.GetValue(this.StartDateTextBox.Text).Year, DateFormMapper.GetValue(this.StartDateTextBox.Text).Month, DateFormMapper.GetValue(this.StartDateTextBox.Text).Day, 0, 0, 0);
            DateTime _endDateTime = new DateTime(DateFormMapper.GetValue(this.EndDateTextBox.Text).Year, DateFormMapper.GetValue(this.EndDateTextBox.Text).Month, DateFormMapper.GetValue(this.EndDateTextBox.Text).Day, 0, 0, 0);

            ReportDataSource _reportDataSource1 = this._reportBL.SalesProfitDetailPerProduct(_startDateTime, _endDateTime, "", _productSubGroupCode, _productCode, _fgReport);

            this.ReportViewer.LocalReport.DataSources.Clear();
            this.ReportViewer.LocalReport.DataSources.Add(_reportDataSource1);
            this.ReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
            this.ReportViewer.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[7];
            _reportParam[0] = new ReportParameter("Start", _startDateTime.ToString(), true);
            _reportParam[1] = new ReportParameter("End", _endDateTime.ToString(), true);
            _reportParam[2] = new ReportParameter("Str1", "", false);
            _reportParam[3] = new ReportParameter("Str2", _productSubGroupCode, true);
            _reportParam[4] = new ReportParameter("Str3", _productCode, true);
            _reportParam[5] = new ReportParameter("FgReport", _fgReport.ToString(), true);
            _reportParam[6] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);

            this.ReportViewer.LocalReport.SetParameters(_reportParam);
            this.ReportViewer.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();

            this.ShowProduct(0);
            this.SetCheckBox();
        }
    }
}
