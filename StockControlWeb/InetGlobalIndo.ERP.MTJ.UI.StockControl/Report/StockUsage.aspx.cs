﻿using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Report
{
    public partial class StockUsage : ReportBase
    {
        private ReportStockControlBL _report = new ReportStockControlBL();
        private PermissionBL _permBL = new PermissionBL();
        private ProductBL _productBL = new ProductBL();
        private WarehouseBL _wrhsBL = new WarehouseBL();

        private string _reportPath0 = "Report/StockUsagePerProduct.rdlc";
        private string _reportPath1 = "Report/StockUsagePerProductGroup.rdlc";
        private string _reportPath2 = "Report/StockUsagePerProductSubGroup.rdlc";
        private string _reportPath3 = "Report/StockUsagePerProductType.rdlc";

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);

        private string _currPageKey = "CurrentPage";

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_WarehouseCheckBoxList_";
        private string _akhir = "";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDStockUsage, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

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
                this.StartDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.StartDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = "Stock Usage Report";

                this.ViewState[this._currPageKey] = 0;

                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.MenuPanel.Visible = true;
                this.ReportViewer1.Visible = false;

                this.ShowProductSubGrp();

                this.ShowWarehouseGroup();
                this.ShowWarehouseArea();

                this.SetAttribute();
                this.ClearData();

                this.ShowWarehouse(0);
                this.SetCheckBox();
            }
        }

        public void SetAttribute()
        {
            this.StartDateTextBox.Attributes.Add("ReadOnly", "True");
        }

        public void ClearData()
        {
            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";

            this.StartDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.FgReportDropDownList.SelectedValue = "null";
            this.WrhsCheckBox.Checked = false;
            this.WarehouseCheckBoxList.ClearSelection();
            this.WrhsAreaDropDownList.SelectedValue = "null";
            this.WrhsGroupDropDownList.SelectedValue = "null";
            this.AllCheckBox.Checked = false;
        }

        private void ShowProductSubGrp()
        {
            this.ProductSubGrpCheckBoxList.ClearSelection();
            this.ProductSubGrpCheckBoxList.Items.Clear();
            this.ProductSubGrpCheckBoxList.DataTextField = "ProductSubGrpName";
            this.ProductSubGrpCheckBoxList.DataValueField = "ProductSubGrpCode";
            this.ProductSubGrpCheckBoxList.DataSource = this._productBL.GetListProductSubGroupForDDL();
            this.ProductSubGrpCheckBoxList.DataBind();
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

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "', 'true');");

            int i = 0;
            foreach (ListItem _item in this.WarehouseCheckBoxList.Items)
            {
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _awal + i.ToString() + ", '" + _item.Value + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "', 'true');");
                i++;
            }

            List<MsWarehouse> _listMsWarehouse = this._wrhsBL.GetListDDLWrhsForReport(_tempWrhsGroup, _tempWrhsArea);
            this.AllHidden.Value = "";

            foreach (MsWarehouse _item in _listMsWarehouse)
            {
                if (this.AllHidden.Value == "")
                {
                    this.AllHidden.Value = _item.WrhsCode;
                }
                else
                {
                    this.AllHidden.Value += "," + _item.WrhsCode;
                }
            }

            this.ShowPage(_prmCurrentPage);
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
            this.ViewState[this._currPageKey] = 0;

            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";
            this.ShowWarehouse(0);
            this.SetCheckBox();
        }

        protected void WrhsAreaDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";
            this.ShowWarehouse(0);
            this.SetCheckBox();
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = false;
            this.ReportViewer1.Visible = true;

            DateTime _startDateTime = new DateTime(DateFormMapper.GetValue(this.StartDateTextBox.Text).Year, DateFormMapper.GetValue(this.StartDateTextBox.Text).Month, DateFormMapper.GetValue(this.StartDateTextBox.Text).Day, 23, 59, 59);

            string _wrhsCode = "";
            string _productSubGrpCode = "";

            if (this.GrabAllCheckBox.Checked == true)
            {
                _wrhsCode = this.AllHidden.Value;
            }
            else
            {
                if (this.AllCheckBox.Checked == true)
                {
                    _wrhsCode = this.TempHidden.Value;
                }
                else
                {
                    _wrhsCode = this.CheckHidden.Value;
                }
            }

            var _hasilProductSubGrp = this._productBL.GetListProductSubGroupForDDL();

            for (var i = 0; i < _hasilProductSubGrp.Count(); i++)
            {
                if (this.ProductSubGrpCheckBoxList.Items[i].Selected == true)
                {
                    if (_productSubGrpCode == "")
                    {
                        _productSubGrpCode += this.ProductSubGrpCheckBoxList.Items[i].Value;
                    }
                    else
                    {
                        _productSubGrpCode += "," + this.ProductSubGrpCheckBoxList.Items[i].Value;
                    }
                }
            }
            ReportDataSource _reportDataSource1 = this._report.StockUsage(_startDateTime, _productSubGrpCode, _wrhsCode, Convert.ToInt32(this.FgReportDropDownList.SelectedValue), Convert.ToInt32(!this.WrhsCheckBox.Checked));

            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

            if (this.FgReportDropDownList.SelectedValue == "0")
            {
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath0;
            }
            else if (this.FgReportDropDownList.SelectedValue == "1")
            {
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
            }
            else if (this.FgReportDropDownList.SelectedValue == "2")
            {
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath2;
            }
            else if (this.FgReportDropDownList.SelectedValue == "3")
            {
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath3;
            }

            this.ReportViewer1.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[9];
            _reportParam[0] = new ReportParameter("Date", _startDateTime.ToString(), true);
            _reportParam[1] = new ReportParameter("Str1", _productSubGrpCode, true);
            _reportParam[2] = new ReportParameter("Str2", _wrhsCode, false);
            _reportParam[3] = new ReportParameter("Str3", "", true);
            _reportParam[4] = new ReportParameter("StrQty", "Qty", false);
            _reportParam[5] = new ReportParameter("FgReport", this.FgReportDropDownList.SelectedValue, true);
            _reportParam[6] = new ReportParameter("FgDetail", Convert.ToInt32(!this.WrhsCheckBox.Checked).ToString(), true);
            _reportParam[7] = new ReportParameter("FgDivide", "1", true);
            _reportParam[8] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);

            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ViewState[this._currPageKey] = "0";

            this.ClearData();

            this.ShowWarehouse(0);
            this.SetCheckBox();
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

        private void ShowPage(Int32 _prmCurrentPage)
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

                this.ShowWarehouse(Convert.ToInt32(e.CommandArgument));
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

            this.ViewState[this._currPageKey] = _reqPage;

            this.ShowWarehouse(_reqPage);
            this.SetCheckBox();
        }

        protected void SetCheckBox()
        {
            int i = 0;
            foreach (ListItem _item in this.WarehouseCheckBoxList.Items)
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

            foreach (ListItem _item in this.WarehouseCheckBoxList.Items)
            {
                if (_item.Selected == false)
                {
                    this.AllCheckBox.Checked = false;
                }
            }

            if (this.WarehouseCheckBoxList.Items.Count < 1)
            {
                this.AllCheckBox.Checked = false;
            }
        }
    }
}
