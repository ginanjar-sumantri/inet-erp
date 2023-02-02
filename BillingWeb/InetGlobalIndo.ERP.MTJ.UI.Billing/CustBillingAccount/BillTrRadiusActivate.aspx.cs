using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.CustBillingAccountBase
{
    public partial class BillTrRadiusActivate : CustBillingAccountBase
    {
        private CustBillAccountBL _custBillAccountBL = new CustBillAccountBL();
        private CustomerBL _customerBL = new CustomerBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();

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
        //private Boolean _isCheckedAll = true;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00_DefaultBodyContentPlaceHolder_TempHidden";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");
            this.DataPagerBottomButton.Attributes.Add("Style", "visibility: hidden;");

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleRadiusLiteral;

                this.GoImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/go.jpg";

                //this.SetButtonPermission();
                this.ClearLabel();
                this.ShowData(0);
            }
        }

        //private void SetButtonPermission()
        //{
        //    this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

        //    if (this._permAdd == PermissionLevel.NoAccess)
        //    {
        //        this.AddButton.Visible = false;
        //        this.AddButton2.Visible = false;
        //    }

        //    _permDelete = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Delete);

        //    if (this._permDelete == PermissionLevel.NoAccess)
        //    {
        //        this.DeleteButton.Visible = false;
        //        this.DeleteButton2.Visible = false;
        //    }
        //}

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private double RowCount()
        {
            double _result = 0;

            _result = this._custBillAccountBL.RowsCountBILTrRadiusActivate(this.CategoryDropDownList.SelectedValue, this.KeywordTextBox.Text);
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
                this.DataPagerTopRepeater.DataSource = from _query in _pageNumber
                                                       select _query;
                this.DataPagerTopRepeater.DataBind();

                this.DataPagerBottomRepeater.DataSource = from _query in _pageNumber
                                                          select _query;
                this.DataPagerBottomRepeater.DataBind();
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

        private void ShowData(Int32 _prmCurrentPage)
        {
            this.TempHidden.Value = "";

            this._page = _prmCurrentPage;

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater.DataSource = null;
            }
            else
            {
                this.ListRepeater.DataSource = this._custBillAccountBL.GetListBILTrRadiusActivate(_prmCurrentPage, _maxrow, this.CategoryDropDownList.SelectedValue, this.KeywordTextBox.Text);
            }
            this.ListRepeater.DataBind();

            //this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            //this.AllCheckBox.Checked = this.IsCheckedAll();

            //this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");
            //this.DeleteButton2.Attributes.Add("OnClick", "return AskYouFirst();");

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

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                BILTrRadiusActivate _bILTrRadiusActivate = (BILTrRadiusActivate)e.Item.DataItem;
                string _code = _bILTrRadiusActivate.CustBillCode.ToString();

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _code;
                }
                else
                {
                    this.TempHidden.Value += "," + _code;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                //CheckBox _listCheckbox;
                //_listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                //_listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");
                //_listCheckbox.Checked = this.IsChecked(_code);

                ImageButton _updateButton = (ImageButton)e.Item.FindControl("UpdateButton");
                _updateButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/update.jpg";
                _updateButton.CommandName = "Update";
                _updateButton.CommandArgument = _bILTrRadiusActivate.Period + "-" + _bILTrRadiusActivate.Year + "-" + _bILTrRadiusActivate.CustBillCode;

                //PERMISSION UPDATE DISAMAKAN DENGAN POSTING 
                this._prmUpdate = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                if (this._prmUpdate == PermissionLevel.NoAccess)
                {
                    _updateButton.Visible = false;
                }
                if (_bILTrRadiusActivate.fgUpdate == true)
                {
                    _updateButton.Visible = false;
                }

                this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

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

                Literal _custBillNametLiteral = (Literal)e.Item.FindControl("CustBillNametLiteral");
                string _custCode = this._custBillAccountBL.GetCustCodeByCustbillcode(_bILTrRadiusActivate.CustBillCode);
                _custBillNametLiteral.Text = HttpUtility.HtmlEncode(this._custBillAccountBL.GetCustNameByCustCode(_custCode));

                Literal _custBillAccountLiteral = (Literal)e.Item.FindControl("CustBillCodetLiteral");
                _custBillAccountLiteral.Text = HttpUtility.HtmlEncode(_bILTrRadiusActivate.CustBillCode);

                Literal _custLiteral = (Literal)e.Item.FindControl("ReferenceLiteral");
                _custLiteral.Text = HttpUtility.HtmlEncode(_bILTrRadiusActivate.PaidRefTransNmbr);

                Literal _productLiteral = (Literal)e.Item.FindControl("TransdateLiteral");
                _productLiteral.Text = HttpUtility.HtmlEncode(DateFormMapper.GetValue(_bILTrRadiusActivate.PaidTransDate));

                Literal _currLiteral = (Literal)e.Item.FindControl("BillingAmountLiteral");
                _currLiteral.Text = ((Decimal)_bILTrRadiusActivate.AmountForex).ToString("#,##0.0");

                Literal _descLiteral = (Literal)e.Item.FindControl("PaidAmountLiteral");
                _descLiteral.Text = ((Decimal)_bILTrRadiusActivate.PaidAmount).ToString("#,##0.0");

                Literal _yearLiteral = (Literal)e.Item.FindControl("YearLiteral");
                _yearLiteral.Text = HttpUtility.HtmlEncode(_bILTrRadiusActivate.Year.ToString());

                Literal _periodeLiteral = (Literal)e.Item.FindControl("PeriodeLiteral");
                _periodeLiteral.Text = HttpUtility.HtmlEncode(_bILTrRadiusActivate.Period.ToString());
            }
        }

        protected void ListRepeater_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string _date = "";
            string _updateRadiusExpired = "";
            int _year = 0;
            int _mount = 0;

            if (e.CommandName == "Update")
            {
                String[] _split = e.CommandArgument.ToString().Split('-');

                //UPDATE fgUpdate IN BILTrRadiusActivate
                BILTrRadiusActivate _bilTrRadiusActivate = this._custBillAccountBL.GetSingleCustBillAccount(_split[0], _split[1], _split[2]);
                _bilTrRadiusActivate.fgUpdate = true;

                //UPDATE RadiusExpiredDate IN Master_CustBillAccount
                Master_CustBillAccount _custBillAccount = this._custBillAccountBL.GetRadiusExpiredDateByUserName(_bilTrRadiusActivate.UserName, _bilTrRadiusActivate.PIN);
                MsCustomer _msCustomer = this._customerBL.GetSingleCust(_custBillAccount.CustCode);
                String _expiredDate =DateFormMapper.GetValue(_custBillAccount.RadiusExpiredDate);
                String[] _newExpiredDate = _expiredDate.Split('-');
                _date = (_newExpiredDate[2] + "-" + (Convert.ToInt32(_newExpiredDate[1]) + 1) + "-" + _msCustomer.DueDateCycle.ToString());
                
                string[] _date2 = _date.Split('-');
                if (Convert.ToInt32(_date2[1]) > 12)
                {
                    _year = Convert.ToInt32(_date2[0]) + 1;
                    _mount = Convert.ToInt32(_date2[1]) - 12;
                }
                if (_year == 0)
                {
                    _updateRadiusExpired = _date;
                }
                else
                {
                    _updateRadiusExpired = _year + "-" + _mount + "-" + _msCustomer.DueDateCycle.ToString();
                }

                _custBillAccount.RadiusExpiredDate = Convert.ToDateTime(_updateRadiusExpired);

                bool _result = this._custBillAccountBL.EditBILTrRadiusActivate(_bilTrRadiusActivate, _custBillAccount);

                if (_result == true)
                {
                    this.WarningLabel.Text = "Your Success Update";
                    this.ShowData(0);
                }
                else
                {
                    this.WarningLabel.Text = "Your Failed Update";
                }
            }
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addPage);
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _error = "";
            string _temp = this.CheckHidden.Value;
            string[] _tempsplit = _temp.Split(',');
            string _page = "0";

            this.ClearLabel();

            bool _result = this._custBillAccountBL.DeleteMultiCustBillAccount(_tempsplit);

            if (_result == true)
            {
                _error = "Delete Success";
            }
            else
            {
                _error = "Delete Failed";
            }

            this.CheckHidden.Value = "";
            //this.AllCheckBox.Checked = false;

            Response.Redirect(this._homePage + "?" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_page, ApplicationConfig.EncryptionKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error);
        }

        protected void GoImageButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.ClearLabel();
            this.ShowData(0);
        }
    }
}
