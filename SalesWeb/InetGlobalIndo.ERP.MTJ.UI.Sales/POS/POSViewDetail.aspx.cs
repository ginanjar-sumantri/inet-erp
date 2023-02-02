using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using Microsoft.Reporting.WebForms;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.POS
{
    public partial class POSViewDetail : POSBase
    {
        private POSBL _POSBL = new POSBL();
        private PermissionBL _permBL = new PermissionBL();
        private UserBL _userBL = new UserBL();

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private string _currPageKey = "CurrentPage";

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _nomor = 0;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private string _imgGetApproval = "get_approval.jpg";
        private string _imgApprove = "approve.jpg";
        private string _imgPosting = "posting.jpg";
        private string _imgUnposting = "unposting.jpg";
        private string _imgPreview = "preview.jpg";

        private byte _decimalPlace = 0;

        private string _confirmTitle = "Description Required";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.ViewState[this._currPageKey] = 0;

                SAL_TrRetail _posHd = this._POSBL.GetSinglePOSHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
                //if (_posHd.Status == SalesOrderDataMapper.GetStatus(TransStatus.Posted))
                //{
                //    this.AddButton.Visible = false;
                //    this.DeleteButton.Visible = false;
                //    this.EditButton.Visible = false;
                //    this.ImportButton.Visible = false;
                //    this.GetFormatExcelLiteral.Text = "";
                //}
                //else
                //{
                    //this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";                   
                    //this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                    this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                    //this.ImportButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/import_excel.jpg";
                //}
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";

                this.SetButtonPermission();

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData(0);
            }
        }

        private void SetButtonPermission()
        {
            //this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            //if (this._permEdit == PermissionLevel.NoAccess)
            //{
            //    this.EditButton.Visible = false;
            //}

            //this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            //if (this._permAdd == PermissionLevel.NoAccess)
            //{
            //    this.AddButton.Visible = false;
            //}

            this._permDelete = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Delete);

            if (this._permDelete == PermissionLevel.NoAccess)
            {
                this.DeleteButton.Visible = false;
            }
        }

        private void SetAttribute()
        {
        }

        private double RowCount()
        {
            double _result = 0;

            _result = this._POSBL.RowsCountPOSDt(this.TransNoTextBox.Text);
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

        public void ClearLabel()
        {
            this.Label.Text = "";
            this.WarningLabel.Text = "";
        }

        public void ShowData(Int32 _prmCurrentPage)
        {
            SAL_TrRetail _POSHd = this._POSBL.GetSinglePOSHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNoTextBox.Text = _POSHd.TransNmbr;
            this.FileNmbrTextBox.Text = _POSHd.FileNmbr;
            this.DateTextBox.Text = _POSHd.TransDate.ToShortDateString();
            this.EmployeeNoTextBox.Text = _POSHd.EmpNumb + " - " + _POSHd.EmpName;
            this.CustNameTextBox.Text = _POSHd.CustName;
            this.PaymentTypeTextBox.Text = _POSHd.PaymentType;
            this.BankNameTextBox.Text = _POSHd.BankName;
            this.PaymentCodeTextBox.Text = _POSHd.PaymentCode;
            this.CurrCodeTextBox.Text = _POSHd.CurrCode;
            this.ForexRateTextBox.Text = _POSHd.ForexRate.ToString();
            this.BaseForexTextBox.Text = _POSHd.BaseForex.ToString();
            this.TotalAmountTextBox.Text = _POSHd.TotalAmount.ToString();
            this.DiscPercentTextBox.Text = _POSHd.DiscPercent.ToString();
            this.DiscAmountTextBox.Text = _POSHd.DiscAmount.ToString();
            this.PPNPercentTextBox.Text = _POSHd.PPNPercent.ToString();
            this.PPNAmountTextBox.Text = _POSHd.PPNAmount.ToString();
            this.AdditionalFeeTextBox.Text = _POSHd.AdditionalFee.ToString();
            this.RemarkTextBox.Text = _POSHd.Remark;


            this.DeleteButton.Visible = true;

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            this.TempHidden.Value = "";

            this._page = _prmCurrentPage;

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater.DataSource = null;
            }
            else
            {
                this.ListRepeater.DataSource = this._POSBL.GetListPOSDt(_prmCurrentPage, _maxrow, Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            }
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            this.AllCheckBox.Checked = this.IsCheckedAll();

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");

            if (this._permView != PermissionLevel.NoAccess)
            {
                this.ShowPage(_prmCurrentPage);
            }

            this.Panel1.Visible = true;
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

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');

            bool _result = this._POSBL.DeleteMultiPOSDt(_tempSplit);

            this.ClearLabel();

            if (_result == true)
            {
                this.WarningLabel.Text = "Delete Success";
            }
            else
            {
                this.WarningLabel.Text = "Delete Failed";
            }

            this.CheckHidden.Value = "";
            this.AllCheckBox.Checked = false;

            this.ViewState[this._currPageKey] = 0;

            this.ShowData(0);
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SAL_TrRetailItem _temp = (SAL_TrRetailItem)e.Item.DataItem;

                string _code = _temp.POSItemCode.ToString();

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _code;
                }
                else
                {
                    this.TempHidden.Value += "," + _code;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no += 1;
                _noLiteral.Text = _no.ToString();

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");
                _listCheckbox.Checked = this.IsChecked(_code);

                //ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                //_viewButton.PostBackUrl = this._viewDetailPage + "?" + this._productKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._codeRevisiKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt("0", ApplicationConfig.EncryptionKey));
                //_viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

                //this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

                //if (this._permView == PermissionLevel.NoAccess)
                //{
                //    _viewButton.Visible = false;
                //}

                //ImageButton _editButton2 = (ImageButton)e.Item.FindControl("EditButton2");
                //this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

                //if (this._permEdit == PermissionLevel.NoAccess)
                //{
                //    _editButton2.Visible = false;
                //}

                //if (this.StatusHiddenField.Value.Trim().ToLower() == SalesOrderDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
                //{
                //    _editButton2.Visible = false;
                //}
                //else
                //{
                //    _editButton2.PostBackUrl = this._editDetailPage + "?" + this._productKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._codeRevisiKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt("0", ApplicationConfig.EncryptionKey));
                //    _editButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
                //}

                //ImageButton _closeButton = (ImageButton)e.Item.FindControl("CloseButton");
                //this._permClose = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Close);

                //if (this._permClose == PermissionLevel.NoAccess)
                //{
                //    _closeButton.Visible = false;
                //}

                //if ((this.StatusHiddenField.Value.Trim().ToLower() == SalesOrderDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower()) && (_temp.DoneClosing.ToString().Trim().ToLower() == SalesOrderDataMapper.GetStatusDetail(SalesOrderStatusDt.Open).ToString().Trim().ToLower()) && _temp.Qty > (((_temp.QtyDO == null) ? 0 : _temp.QtyDO) + ((_temp.QtyClose == null) ? 0 : _temp.QtyClose)))
                //{
                //    _closeButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/close.jpg";
                //    _closeButton.CommandArgument = _code;
                //    _closeButton.CommandName = "CloseButton";
                //    _closeButton.Attributes.Add("OnClick", "return ConfirmFillDescription('" + this.DescriptionHiddenField.ClientID + "', '" + _confirmTitle + "');");
                //}
                //else
                //{
                //    _closeButton.Visible = false;
                //}

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

                Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductCodeLiteral");
                _productCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductCode);

                Literal _QtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                _QtyLiteral.Text = HttpUtility.HtmlEncode(_temp.Qty.ToString());

                Literal _PriceLiteral = (Literal)e.Item.FindControl("PriceLiteral");
                _PriceLiteral.Text = HttpUtility.HtmlEncode((_temp.Price == 0) ? "0" : _temp.Price.ToString("#,##0.##"));

                Literal _DiscountCodeLiteral = (Literal)e.Item.FindControl("DiscountCodeLiteral");
                _DiscountCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.DiscountCode.ToString() );

                Literal _DiscountAmountLiteral = (Literal)e.Item.FindControl("DiscountAmountLiteral");
                _DiscountAmountLiteral.Text = HttpUtility.HtmlEncode((_temp.DiscountAmount == 0) ? "0" : _temp.DiscountAmount.ToString("#,##0.##"));

                Literal _TotalLiteral = (Literal)e.Item.FindControl("TotalLiteral");
                _TotalLiteral.Text = HttpUtility.HtmlEncode((_temp.Total == 0) ? "0" : _temp.Total.ToString("#,###.##"));

                Literal _RemarkLiteral = (Literal)e.Item.FindControl("RemarkLiteral");
                _RemarkLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);
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
            this.ShowData( _reqPage);
        }
    }
}