using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using System.IO;
using System.Collections.Generic;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseRequest
{
    public partial class PurchaseRequestXMLApprove : PurchaseRequestBase
    {
        private PurchaseRequestBL _purchaseRequestBL = new PurchaseRequestBL();
        private OrganizationStructureBL _organizationStructureBL = new OrganizationStructureBL();
        private PermissionBL _permBL = new PermissionBL();

        private int?[] _navMark0 = { null, null, null, null };
        private bool _flag0 = true;
        private bool _nextFlag0 = false;
        private bool _lastFlag0 = false;

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private string _currPageKey0 = "CurrentPage0";
        private string _currPageKey = "CurrentPage";

        private int _page0;
        private int _maxrow0 = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength0 = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no0 = 0;
        private int _nomor0 = 0;

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _nomor = 0;

        private string _awal0 = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater0_ctl";
        private string _akhir0 = "_ListCheckBox0";
        private string _cbox0 = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox0";
        private string _tempHidden0 = "ctl00$DefaultBodyContentPlaceHolder$TempHidden0";

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDApproveXML, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);
            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this.DataPagerButton0.Attributes.Add("Style", "visibility: hidden;");
            this.DataPagerBottomButton0.Attributes.Add("Style", "visibility: hidden;");

            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");
            this.DataPagerBottomButton.Attributes.Add("Style", "visibility: hidden;");

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral0.Text = this._pageTitleLiteral;
                this.PageTitleLiteral.Text = this._pageTitleLiteralXMLList;

                this.ViewState[this._currPageKey0] = 0;
                this.ViewState[this._currPageKey] = 0;

                this.GoImageButton0.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/go.jpg";
                this.GoImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/go.jpg";
                this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.DeleteButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";

                this.SetButtonPermission();

                this.ClearLabel();
                this.ClearLabel0();
                this.ShowData(0);
                this.ShowData0(0);
            }
        }

        private void SetButtonPermission()
        {
            this._permDelete = this._permBL.PermissionValidation1(this._menuIDApproveXML, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Delete);

            if (this._permDelete == PermissionLevel.NoAccess)
            {
                this.DeleteButton.Visible = false;
                this.DeleteButton2.Visible = false;
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ClearLabel0()
        {
            this.WarningLabel0.Text = "";
        }

        private double RowCount0()
        {
            double _result = 0;

            _result = this._purchaseRequestBL.RowsCountPRCRequestHd(this.CategoryDropDownList0.SelectedValue, this.KeywordTextBox0.Text);
            _result = System.Math.Ceiling(_result / (double)_maxrow);

            return _result;
        }

        private double RowCount()
        {
            double _result = 0;

            _result = this._purchaseRequestBL.RowsCountPRCRequestHd(this.CategoryDropDownList.SelectedValue, this.KeywordTextBox.Text);
            _result = System.Math.Ceiling(_result / (double)_maxrow);

            return _result;
        }

        private Boolean IsCheckedAll()
        {
            Boolean _result = true;
            foreach (RepeaterItem _row in this.ListRepeater.Items)
            {
                CheckBox _chk = (CheckBox)_row.FindControl("ListCheckBox");
                if (_chk.Checked == false)
                {
                    _result = false;
                    break;
                }
            }
            if (this.ListRepeater.Items.Count == 0)
            {
                _result = false;
            }
            return _result;
        }

        private void ShowPage0(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCount0();

            if (_prmCurrentPage - _maxlength0 > 0)
            {
                min = _prmCurrentPage - _maxlength0;
            }
            else
            {
                min = 0;
            }

            if (_prmCurrentPage + _maxlength0 < q)
            {
                max = _prmCurrentPage + _maxlength0 + 1;
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
                    this._navMark0[0] = 0;

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark0[0]);
                    _pageNumberElement++;

                    this._navMark0[1] = Convert.ToInt32(Math.Abs(_prmCurrentPage - 1));

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark0[1]);
                    _pageNumberElement++;
                }

                for (; i < max; i++)
                {
                    _pageNumber[_pageNumberElement] = i;
                    _pageNumberElement++;
                }

                if (_prmCurrentPage < q - 1)
                {
                    this._navMark0[2] = Convert.ToInt32(_prmCurrentPage + 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark0[2]);
                    _pageNumberElement++;

                    if (_pageNumber[_pageNumberElement - 2] > _pageNumber[_pageNumberElement - 1])
                    {
                        this._flag0 = true;
                    }

                    this._navMark0[3] = Convert.ToInt32(q - 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark0[3]);
                    _pageNumberElement++;
                }

                int?[] _navMarkBackup = new int?[4];
                this._navMark0.CopyTo(_navMarkBackup, 0);
                this.DataPagerTopRepeater0.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTopRepeater0.DataBind();

                _flag0 = true;
                _nextFlag0 = false;
                _lastFlag0 = false;
                _navMark0 = _navMarkBackup;

                this.DataPagerBottomRepeater0.DataSource = from _query in _pageNumber
                                                           select _query;
                this.DataPagerBottomRepeater0.DataBind();
            }
            else
            {
                this.DataPagerTopRepeater0.DataSource = null;
                this.DataPagerTopRepeater0.DataBind();

                this.DataPagerBottomRepeater0.DataSource = null;
                this.DataPagerBottomRepeater0.DataBind();
            }
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

                this.DataPagerBottomRepeater.DataSource = from _query in _pageNumber
                                                          select _query;
                this.DataPagerBottomRepeater.DataBind();
            }
            else
            {
                this.DataPagerTopRepeater.DataSource = null;
                this.DataPagerTopRepeater.DataBind();

                this.DataPagerBottomRepeater.DataSource = null;
                this.DataPagerBottomRepeater.DataBind();
            }
        }

        private void ShowData0(Int32 _prmCurrentPage)
        {
            this.TempHidden0.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._page0 = _prmCurrentPage;

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater0.DataSource = null;
            }
            else
            {
                this.ListRepeater0.DataSource = this._purchaseRequestBL.GetListPRCRequestHd(_prmCurrentPage, _maxrow0, this.CategoryDropDownList0.SelectedValue, this.KeywordTextBox0.Text);
            }
            this.ListRepeater0.DataBind();

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel0.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            if (this._permView != PermissionLevel.NoAccess)
            {
                this.ShowPage0(_prmCurrentPage);
            }
        }

        private void ShowData(Int32 _prmCurrentPage)
        {
            this.TempHidden.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._page = _prmCurrentPage;

            this._permView = this._permBL.PermissionValidation1(this._menuIDApproveXML, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater.DataSource = null;
            }
            else
            {
                this.ListRepeater.DataSource = this._purchaseRequestBL.GetListPRCXMLApprove(_prmCurrentPage, _maxrow, this.CategoryDropDownList.SelectedValue, this.KeywordTextBox.Text);
            }
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            this.AllCheckBox.Checked = this.IsCheckedAll();

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");
            this.DeleteButton2.Attributes.Add("OnClick", "return AskYouFirst();");

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            if (this._permView != PermissionLevel.NoAccess)
            {
                this.ShowPage(_prmCurrentPage);
            }
        }

        private Boolean IsChecked(String _prmValue)
        {
            Boolean _result = false;
            String[] _value = this.CheckHidden.Value.Split(',');
            for (int i = 0; i < _value.Length; i++)
            {
                if (_prmValue == _value[i])
                {
                    _result = true;
                    break;
                }
            }
            return _result;
        }

        protected void ListRepeater0_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PRCRequestHd _temp = (PRCRequestHd)e.Item.DataItem;
                string _code = _temp.TransNmbr.ToString();

                if (this.TempHidden0.Value == "")
                {
                    this.TempHidden0.Value = _code;
                }
                else
                {
                    this.TempHidden0.Value += "," + _code;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no0 = _page0 * _maxrow0;
                _no0 += 1;
                _no0 = _nomor0 + _no0;
                _noLiteral.Text = _no0.ToString();
                _nomor0 += 1;

                ImageButton _createXMLButton = (ImageButton)e.Item.FindControl("CreateXMLButton");
                _createXMLButton.Visible = false;
                PurchaseRequestBL _purchaseRequestBL = new PurchaseRequestBL();

                if (!_purchaseRequestBL.IsXMLFileExist(_purchaseRequestBL.GetCustomerCode() + _temp.TransNmbr.Replace('/', '_') + ".xml"))
                {
                    if (_temp.Status == 'A' || _temp.Status == 'P')
                    {
                        _createXMLButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/exportxml.jpg";
                        _createXMLButton.CommandArgument = _temp.TransNmbr;
                        _createXMLButton.CommandName = "createXML";
                        _createXMLButton.Visible = true;
                    }
                }

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate");
                _tableRow.Attributes.Add("OnMouseOver", "this.style.backgroundColor='" + this._rowColorHover + "';");
                if (e.Item.ItemType == ListItemType.Item)
                {
                    _tableRow.Attributes.Add("style", "background-color:" + this._rowColor);
                    _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColor + "';");
                }
                if (e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    _tableRow.Attributes.Add("style", "background-color:" + this._rowColorAlternate);
                    _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColorAlternate + "';");
                }

                Literal _transNoLiteral = (Literal)e.Item.FindControl("TransNoLiteral");
                _transNoLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                Literal _fileNmbrLiteral = (Literal)e.Item.FindControl("FileNmbrLiteral");
                _fileNmbrLiteral.Text = HttpUtility.HtmlEncode(_temp.FileNmbr);

                Literal _transDateLiteral = (Literal)e.Item.FindControl("TransDateLiteral");
                _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate);

                Literal _statusLiteral = (Literal)e.Item.FindControl("StatusLiteral");
                _statusLiteral.Text = HttpUtility.HtmlEncode(PurchaseRequestDataMapper.GetStatusText(_temp.Status));

                Literal _orgUnitDescLiteral = (Literal)e.Item.FindControl("OrgUnitDescLiteral");
                _orgUnitDescLiteral.Text = this._organizationStructureBL.GetDescriptionByCode(_temp.OrgUnit);

                Literal _requestByLiteral = (Literal)e.Item.FindControl("RequestByLiteral");
                _requestByLiteral.Text = HttpUtility.HtmlEncode(_temp.RequestBy);

                Literal _currCodeLiteral = (Literal)e.Item.FindControl("CurrCodeLiteral");
                _currCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.CurrCode);

            }
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PRCPRxmlList _temp = (PRCPRxmlList)e.Item.DataItem;
                string _code = _temp.XMLFileName.ToString();

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _code;
                }
                else
                {
                    this.TempHidden.Value += "," + _code;
                }

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate");
                _tableRow.Attributes.Add("OnMouseOver", "this.style.backgroundColor='" + this._rowColorHover + "';");
                if (e.Item.ItemType == ListItemType.Item)
                {
                    _tableRow.Attributes.Add("style", "background-color:" + this._rowColor);
                    _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColor + "';");
                }
                if (e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    _tableRow.Attributes.Add("style", "background-color:" + this._rowColorAlternate);
                    _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColorAlternate + "';");
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");
                _listCheckbox.Checked = this.IsChecked(_code);

                ImageButton _btnApprove = (ImageButton)e.Item.FindControl("btnApprove");
                _btnApprove.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/approve.jpg";
                _btnApprove.CommandName = "approveXMLFile";
                _btnApprove.CommandArgument = _temp.XMLFileName;

                ImageButton _btnView = (ImageButton)e.Item.FindControl("btnView");
                _btnView.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                _btnView.OnClientClick = "window.open ('" + ApplicationConfig.UploadXMLPurchaseRequestVirDirPath + _temp.XMLFileName + "');";

                Literal _senderCodeLiteral = (Literal)e.Item.FindControl("SenderCodeLiteral");
                _senderCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.SenderCode);

                Literal _xmlFileNameLiteral = (Literal)e.Item.FindControl("XmlFileNameLiteral");
                _xmlFileNameLiteral.Text = HttpUtility.HtmlEncode(_temp.XMLFileName);

            }
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _error = "";
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');
            string _page = "0";

            this.ClearLabel();

            bool _result = this._purchaseRequestBL.DeleteMultiPRCXMLApprove(_tempSplit);

            if (_result == true)
            {
                _error = "Delete Success";
            }
            else
            {
                _error = "Delete Failed";
            }

            this.CheckHidden.Value = "";
            this.AllCheckBox.Checked = false;

            Response.Redirect(this._approvePage + "?" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_page, ApplicationConfig.EncryptionKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error);
        }

        protected void DataPagerTopRepeater0_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey0] = Convert.ToInt32(e.CommandArgument);

                this.ShowData0(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void DataPagerTopRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this.ShowData(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void DataPagerTopRepeater0_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                int _pageNumber = (int)e.Item.DataItem;

                NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

                if (Convert.ToInt32(this.ViewState[this._currPageKey0]) == _pageNumber)
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

                    if (_pageNumber == this._navMark0[0])
                    {
                        _pageNumberLinkButton.Text = "First";
                        this._navMark0[0] = null;
                    }
                    else if (_pageNumber == this._navMark0[1])
                    {
                        _pageNumberLinkButton.Text = "Prev";
                        this._navMark0[1] = null;
                    }
                    else if (_pageNumber == this._navMark0[2] && this._flag0 == false)
                    {
                        _pageNumberLinkButton.Text = "Next";
                        this._navMark0[2] = null;
                        this._nextFlag0 = true;
                        this._flag0 = true;
                    }
                    else if (_pageNumber == this._navMark0[3] && this._flag0 == true && this._nextFlag0 == true)
                    {
                        _pageNumberLinkButton.Text = "Last";
                        this._navMark0[3] = null;
                        this._lastFlag0 = true;
                    }
                    else
                    {
                        if (this._lastFlag0 == false)
                        {
                            _pageNumberLinkButton.Text = (_pageNumber + 1).ToString();
                        }

                        if (_pageNumber == this._navMark0[2] && this._flag0 == true)
                            this._flag0 = false;
                    }
                }
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

        protected void DataPagerBottomButton0_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerBottomRepeater0.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCount0())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCount0().ToString();
                        _reqPage = Convert.ToInt32(this.RowCount0()) - 1;
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

            this.ViewState[this._currPageKey0] = _reqPage;

            this.ShowData0(_reqPage);
        }

        protected void DataPagerBottomButton_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerBottomRepeater.Controls)
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

            this.ShowData(_reqPage);
        }

        protected void DataPagerButton0_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerTopRepeater0.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCount0())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCount0().ToString();
                        _reqPage = Convert.ToInt32(this.RowCount0()) - 1;
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

            this.ViewState[this._currPageKey0] = _reqPage;

            this.ShowData0(_reqPage);
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

            this.ShowData(_reqPage);
        }

        protected void GoImageButton0_Click(object sender, ImageClickEventArgs e)
        {
            this.ViewState[this._currPageKey0] = 0;

            this.ClearLabel0();
            this.ShowData0(0);
        }

        protected void GoImageButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;
            this.ClearLabel();
            this.ShowData(0);
        }

        protected void ListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "approveXMLFile")
            {
                PurchaseRequestBL _prcRequestBL = new PurchaseRequestBL();
                _prcRequestBL.ApproveXML(e.CommandArgument.ToString());

                this.ViewState[this._currPageKey] = 0;
                this.ClearLabel();
                this.ShowData(0);
            }
        }

        protected void ListRepeater0_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "createXML")
            {
                this._purchaseRequestBL = new PurchaseRequestBL();
                PRCRequestHd _prcRequestHd = this._purchaseRequestBL.GetSinglePRCRequestHd(e.CommandArgument.ToString());
                try
                {
                    if (_prcRequestHd.Status == 2) return;
                    TextWriter tw = new StreamWriter(ApplicationConfig.UploadXMLPurchaseRequestPath + _purchaseRequestBL.GetCustomerCode() + _prcRequestHd.TransNmbr.Replace('/', '_') + ".xml");

                    tw.WriteLine("<PurchaseRequest>");
                    tw.WriteLine("<TransNmbr>" + _prcRequestHd.TransNmbr + "</TransNmbr>");
                    tw.WriteLine("<FileNmbr>" + _prcRequestHd.FileNmbr + "</FileNmbr>");
                    tw.WriteLine("<TransDate>" + _prcRequestHd.TransDate + "</TransDate>");
                    tw.WriteLine("<OrgUnit>" + _prcRequestHd.OrgUnit + "</OrgUnit>");
                    tw.WriteLine("<Remark>" + _prcRequestHd.Remark + "</Remark>");
                    PurchaseRequestBL _prcRequestBL = new PurchaseRequestBL();
                    ProductBL _productBL = new ProductBL();
                    PriceGroupBL _priceGroupBL = new PriceGroupBL();
                    String _tempPGCode = "";
                    List<PRCRequestDt> _PRCRequestDetailData = _prcRequestBL.GetPRCRequestDtListOfHeader(_prcRequestHd.TransNmbr);
                    foreach (PRCRequestDt _rs in _PRCRequestDetailData)
                    {
                        tw.WriteLine("<PurchaseRequestDetail>");
                        tw.WriteLine("<ProductCode>" + _rs.ProductCode + "</ProductCode>");
                        tw.WriteLine("<Specification>" + _rs.Specification + "</Specification>");
                        tw.WriteLine("<Qty>" + _rs.Qty + "</Qty>");
                        tw.WriteLine("<Unit>" + _rs.Unit + "</Unit>");
                        _tempPGCode = _productBL.GetPriceGroupCodeByProductCode(_rs.ProductCode);
                        tw.WriteLine("<PG>" + _tempPGCode + "</PG>");
                        tw.WriteLine("<Price>" + _priceGroupBL.GetAmountForexByPGCode(_tempPGCode).ToString() + "</Price>");                        
                        tw.WriteLine("</PurchaseRequestDetail>");
                    }

                    tw.WriteLine("</PurchaseRequest>");

                    tw.Close();
                    _prcRequestBL.InsertPRCPRxmlList(_purchaseRequestBL.GetCustomerCode(), _purchaseRequestBL.GetCustomerCode() + _prcRequestHd.TransNmbr.Replace('/', '_') + ".xml");
                    Response.Redirect(this._approvePage);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}