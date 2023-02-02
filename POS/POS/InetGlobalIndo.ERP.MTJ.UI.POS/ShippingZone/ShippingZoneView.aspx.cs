using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.UI.POS.Member;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.ShippingZone
{
    public partial class ShippingZoneView : ShippingZoneBase
    {
        private ShippingBL _shippingBL = new ShippingBL();
        private PermissionBL _permBL = new PermissionBL();
        private CityBL _cityBL = new CityBL();
        private CountryBL _countryBL = new CountryBL();
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
        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private string _awal2 = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater2_ctl";
        private string _akhir2 = "_ListCheckBox2";
        private string _cbox2 = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox2";
        private string _tempHidden2 = "ctl00$DefaultBodyContentPlaceHolder$TempHidden2";

        private string _awal3 = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater3_ctl";
        private string _akhir3 = "_ListCheckBox3";
        private string _cbox3 = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox3";
        private string _tempHidden3 = "ctl00$DefaultBodyContentPlaceHolder$TempHidden3";

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

            if (!this.Page.IsPostBack == true)
            {
                this.SetAttribute();
                this.SetButtonPermission();
                this.ShowData();
            }
        }

        protected void SetAttribute()
        {
            this.PageTitleLiteral.Text = this._pageTitleLiteral;
            this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
            this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
            this.AddCountryButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
            this.AddPriceButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
            this.AddMultipleButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
            this.DeleteCountryButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/Delete.jpg";
            this.DeletePriceButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/Delete.jpg";
            this.DeleteMultipleButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/Delete.jpg";
            this.CodeTextBox.Attributes.Add("ReadOnly", "True");
            this.NameTextBox.Attributes.Add("ReadOnly", "True");
            this.FgActiveTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("ReadOnly", "True");
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                this.AddCountryButton.Visible = false;
                this.AddPriceButton.Visible = false;
                this.AddMultipleButton.Visible = false;
            }

            this._permDelete = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Delete);

            if (this._permDelete == PermissionLevel.NoAccess)
            {
                this.DeleteCountryButton.Visible = false;
                this.DeletePriceButton.Visible = false;
                this.DeleteMultipleButton.Visible = false;
            }
        }

        public void ShowData()
        {
            Int32 _reqPage = 0;
            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|'); ;
            POSMsZone _POSMsZone = this._shippingBL.GetSinglePOSMsZone(_tempSplit[0]);
            this.CodeTextBox.Text = _POSMsZone.ZoneCode;
            this.NameTextBox.Text = _POSMsZone.ZoneName;
            this.FgActiveTextBox.Text = _POSMsZone.FgActive.ToString();
            this.RemarkTextBox.Text = _POSMsZone.Remark;

            this.ShowDataDetailCountry(_reqPage);
            _nomor = 0;
            this.ShowDataDetailPrice(_reqPage);
            _nomor = 0;
            this.ShowDataDetailMultiple(_reqPage);
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        #region Detail Country

        private double RowCountCountry()
        {
            double _result = 0;

            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|'); ;

            _result = this._shippingBL.RowsCountPOSMsZoneCountry("Code", _tempSplit[0]);
            _result = System.Math.Ceiling(_result / (double)_maxrow);

            return _result;
        }

        private Boolean IsCheckedCountry(String _prmValue)
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

        private Boolean IsCheckedAllCountry()
        {
            Boolean _result = true;

            foreach (RepeaterItem _row in this.CountryRepeater.Items)
            {
                CheckBox _chk = (CheckBox)_row.FindControl("ListCheckBox");

                if (_chk.Checked == false)
                {
                    _result = false;
                    break;
                }
            }

            if (this.CountryRepeater.Items.Count == 0)
            {
                _result = false;
            }

            return _result;
        }

        private void ShowPageCountry(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCountCountry();

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

        public void ShowDataDetailCountry(Int32 _prmCurrentPage)
        {
            this.TempHidden.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._page = _prmCurrentPage;

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|'); ;
            if (this._permView == PermissionLevel.NoAccess)
            {
                this.CountryRepeater.DataSource = null;
            }
            else
            {
                this.CountryRepeater.DataSource = this._shippingBL.GetListPOSMsZoneCountry(_prmCurrentPage, _maxrow, "Code", _tempSplit[0]);
            }
            this.CountryRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            this.AllCheckBox.Checked = this.IsCheckedAllCountry();

            this.DeleteCountryButton.Attributes.Add("OnClick", "return AskYouFirst();");

            if (this._permView != PermissionLevel.NoAccess)
            {
                this.ShowPageCountry(_prmCurrentPage);
            }
        }

        protected void CountryRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            POSMsZoneCountry _temp = (POSMsZoneCountry)e.Item.DataItem;
            string _code = _temp.ZoneCode.ToString() + "|" + _temp.CountryCode.ToString() + "|" + _temp.CityCode.ToString();

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

            CheckBox _listCheckbox;
            _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
            _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");
            _listCheckbox.Checked = this.IsCheckedCountry(_code);

            ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
            _viewButton.PostBackUrl = this._viewCountryDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
            _viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                _viewButton.Visible = false;
            }

            ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
            _editButton.PostBackUrl = this._editCountryDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
            _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                _editButton.Visible = false;
            }

            Literal _countryName = (Literal)e.Item.FindControl("CountryLiteral");
            _countryName.Text = this._countryBL.GetCountryNameByCode(_temp.CountryCode);

            Literal _cityName = (Literal)e.Item.FindControl("CityLiteral");
            _cityName.Text = this._cityBL.GetSingle(HttpUtility.HtmlEncode(_temp.CityCode)).CityName;

            Literal _estimate = (Literal)e.Item.FindControl("EstimateLiteral");
            _estimate.Text = HttpUtility.HtmlEncode(_temp.EstimationTime);

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
        }

        protected void AddCountryButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addCountryDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeleteCountryButton_Click(object sender, ImageClickEventArgs e)
        {
            string _error = "";
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');

            this.ClearLabel();

            bool _result = this._shippingBL.DeleteMultiPOSMsZoneCountry(_tempSplit);

            if (_result == true)
            {
                _error = "Delete Success";
            }
            else
            {
                _error = "Delete Failed";
            }
            this.WarningLabel.Text = _error;
            this.CheckHidden.Value = "";
            this.AllCheckBox.Checked = false;
            this.ShowDataDetailCountry(0);
        }

        protected void DataPagerTopRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this.ShowDataDetailCountry(Convert.ToInt32(e.CommandArgument));
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

                    if (_reqPage > this.RowCountCountry())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCountCountry().ToString();
                        _reqPage = Convert.ToInt32(this.RowCountCountry()) - 1;
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

            this.ShowDataDetailCountry(_reqPage);
        }

        #endregion

        #region Detail Price

        private double RowCountPrice()
        {
            double _result = 0;

            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|'); ;
            _result = this._shippingBL.RowsCountPOSMsZonePrice(_tempSplit[0]);
            _result = System.Math.Ceiling(_result / (double)_maxrow);

            return _result;
        }

        private Boolean IsCheckedPrice(String _prmValue)
        {
            Boolean _result = false;
            String[] _value = this.CheckHidden2.Value.Split(',');

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

        private Boolean IsCheckedAllPrice()
        {
            Boolean _result = true;

            foreach (RepeaterItem _row in this.PriceRepeater.Items)
            {
                CheckBox _chk = (CheckBox)_row.FindControl("ListCheckBox");

                if (_chk.Checked == false)
                {
                    _result = false;
                    break;
                }
            }

            if (this.PriceRepeater.Items.Count == 0)
            {
                _result = false;
            }

            return _result;
        }

        private void ShowPagePrice(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCountPrice();

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
                this.DataPagerTop2Repeater.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTop2Repeater.DataBind();

                _flag = true;
                _nextFlag = false;
                _lastFlag = false;
                _navMark = _navMarkBackup;
            }
            else
            {
                this.DataPagerTop2Repeater.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTop2Repeater.DataBind();
            }
        }

        public void ShowDataDetailPrice(Int32 _prmCurrentPage)
        {
            this.TempHidden2.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._page = _prmCurrentPage;

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|'); ;
            if (this._permView == PermissionLevel.NoAccess)
            {
                this.PriceRepeater.DataSource = null;
            }
            else
            {
                this.PriceRepeater.DataSource = this._shippingBL.GetListPOSMsZonePrice(_prmCurrentPage, _maxrow, "Code", _tempSplit[0]);
            }
            this.PriceRepeater.DataBind();

            this.AllCheckBox2.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox2.ClientID + ", " + this.CheckHidden2.ClientID + ", '" + _awal2 + "', '" + _akhir2 + "', '" + _tempHidden2 + "' );");

            this.AllCheckBox2.Checked = this.IsCheckedAllPrice();

            this.DeletePriceButton.Attributes.Add("OnClick", "return AskYouFirst();");

            if (this._permView != PermissionLevel.NoAccess)
            {
                this.ShowPagePrice(_prmCurrentPage);
            }
        }

        protected void PriceRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            POSMsZonePrice _temp = (POSMsZonePrice)e.Item.DataItem;
            string _code = _temp.ZoneCode.ToString() + "|" + _temp.ProductShape.ToString() + "|" + _temp.Weight.ToString();

            if (this.TempHidden2.Value == "")
            {
                this.TempHidden2.Value = _code;
            }
            else
            {
                this.TempHidden2.Value += "," + _code;
            }

            Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
            _no = _page * _maxrow;
            _no += 1;
            _no = _nomor + _no;
            _noLiteral.Text = _no.ToString();
            _nomor += 1;

            CheckBox _listCheckbox2;
            _listCheckbox2 = (CheckBox)e.Item.FindControl("ListCheckBox");
            _listCheckbox2.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden2.ClientID + ", " + _listCheckbox2.ClientID + ", '" + _code + "', '" + _awal2 + "', '" + _akhir2 + "', '" + _cbox2 + "')");
            _listCheckbox2.Checked = this.IsCheckedPrice(_code);

            ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
            _viewButton.PostBackUrl = this._viewPriceDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
            _viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                _viewButton.Visible = false;
            }

            ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
            _editButton.PostBackUrl = this._editPriceDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
            _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                _editButton.Visible = false;
            }

            String _productShapeType = "";
            if (_temp.ProductShape == "0")
                _productShapeType = "Document";
            else if (_temp.ProductShape == "1")
                _productShapeType = "Non Document";
            else if (_temp.ProductShape == "2")
                _productShapeType = "International Priority";
            
            Literal _productShape = (Literal)e.Item.FindControl("ProductShapeLiteral");
            _productShape.Text = _productShapeType;

            Literal _currency = (Literal)e.Item.FindControl("CurrencyLiteral");
            _currency.Text = this._currencyBL.GetCurrencyName(_temp.CurrCode);
            
            Literal _weight = (Literal)e.Item.FindControl("WeightLiteral");
            _weight.Text = Convert.ToDecimal(_temp.Weight).ToString("#,#");

            Literal _unit = (Literal)e.Item.FindControl("UnitLiteral");
            _unit.Text = HttpUtility.HtmlEncode(_temp.Unit);

            Literal _price = (Literal)e.Item.FindControl("PriceLiteral");
            _price.Text = Convert.ToDecimal(_temp.Price).ToString("#,#");

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
        }

        protected void AddPriceButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addPriceDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeletePriceButton_Click(object sender, ImageClickEventArgs e)
        {
            string _error = "";
            string _temp = this.CheckHidden2.Value;
            string[] _tempSplit = _temp.Split(',');

            this.ClearLabel();

            bool _result = this._shippingBL.DeleteMultiPOSMsZonePrice(_tempSplit);

            if (_result == true)
            {
                _error = "Delete Success";
            }
            else
            {
                _error = "Delete Failed";
            }
            this.WarningLabel2.Text = _error;
            this.CheckHidden2.Value = "";
            this.AllCheckBox2.Checked = false;
            this.ShowDataDetailPrice(0);
        }

        protected void DataPagerTop2Repeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this.ShowDataDetailPrice(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void DataPagerTop2Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        protected void DataPager2Button_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerTop2Repeater.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCountPrice())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCountPrice().ToString();
                        _reqPage = Convert.ToInt32(this.RowCountPrice()) - 1;
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

            this.ShowDataDetailPrice(_reqPage);
        }

        #endregion

        #region Detail Multiple

        private double RowCountMultiple()
        {
            double _result = 0;

            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|'); ;
            _result = this._shippingBL.RowsCountPOSMsZoneMultiplePrice("Code", _tempSplit[0]);
            _result = System.Math.Ceiling(_result / (double)_maxrow);

            return _result;
        }

        private Boolean IsCheckedMultiple(String _prmValue)
        {
            Boolean _result = false;
            String[] _value = this.CheckHidden3.Value.Split(',');

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

        private Boolean IsCheckedAllMultiple()
        {
            Boolean _result = true;

            foreach (RepeaterItem _row in this.MultipleRepeater.Items)
            {
                CheckBox _chk = (CheckBox)_row.FindControl("ListCheckBox");

                if (_chk.Checked == false)
                {
                    _result = false;
                    break;
                }
            }

            if (this.MultipleRepeater.Items.Count == 0)
            {
                _result = false;
            }

            return _result;
        }

        private void ShowPageMultiple(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCountMultiple();

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
                this.DataPagerTop3Repeater.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTop3Repeater.DataBind();

                _flag = true;
                _nextFlag = false;
                _lastFlag = false;
                _navMark = _navMarkBackup;
            }
            else
            {
                this.DataPagerTop3Repeater.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTop3Repeater.DataBind();
            }
        }

        public void ShowDataDetailMultiple(Int32 _prmCurrentPage)
        {
            this.TempHidden3.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._page = _prmCurrentPage;

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|'); ;
            if (this._permView == PermissionLevel.NoAccess)
            {
                this.MultipleRepeater.DataSource = null;
            }
            else
            {
                this.MultipleRepeater.DataSource = this._shippingBL.GetListPOSMsZoneMultiplePrice(_prmCurrentPage, _maxrow, "Code", _tempSplit[0]);
            }
            this.MultipleRepeater.DataBind();

            this.AllCheckBox3.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox3.ClientID + ", " + this.CheckHidden3.ClientID + ", '" + _awal3 + "', '" + _akhir3 + "', '" + _tempHidden3 + "' );");

            this.AllCheckBox3.Checked = this.IsCheckedAllMultiple();

            this.DeleteMultipleButton.Attributes.Add("OnClick", "return AskYouFirst();");

            if (this._permView != PermissionLevel.NoAccess)
            {
                this.ShowPageMultiple(_prmCurrentPage);
            }
        }

        protected void MultipleRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            POSMsZoneMultiplePrice _temp = (POSMsZoneMultiplePrice)e.Item.DataItem;
            string _code = _temp.ZoneCode.ToString() + "|" + _temp.ProductShape.ToString() + "|" + _temp.Weight1.ToString() + "|" + _temp.Weight2.ToString();

            if (this.TempHidden3.Value == "")
            {
                this.TempHidden3.Value = _code;
            }
            else
            {
                this.TempHidden3.Value += "," + _code;
            }

            Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
            _no = _page * _maxrow;
            _no += 1;
            _no = _nomor + _no;
            _noLiteral.Text = _no.ToString();
            _nomor += 1;

            CheckBox _listCheckbox3;
            _listCheckbox3 = (CheckBox)e.Item.FindControl("ListCheckBox");
            _listCheckbox3.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden3.ClientID + ", " + _listCheckbox3.ClientID + ", '" + _code + "', '" + _awal3 + "', '" + _akhir3 + "', '" + _cbox3 + "')");
            _listCheckbox3.Checked = this.IsCheckedMultiple(_code);

            ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
            _viewButton.PostBackUrl = this._viewMultipleDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
            _viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                _viewButton.Visible = false;
            }

            ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
            _editButton.PostBackUrl = this._editMultipleDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
            _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                _editButton.Visible = false;
            }

            String _productShapeType = "";
            if (_temp.ProductShape == "0")
                _productShapeType = "Document";
            else if (_temp.ProductShape == "1")
                _productShapeType = "Non Document";
            else if (_temp.ProductShape == "2")
                _productShapeType = "International Priority";

            Literal _productShape = (Literal)e.Item.FindControl("ProductShapeLiteral");
            _productShape.Text = _productShapeType;

            Literal _weight1 = (Literal)e.Item.FindControl("Weight1Literal");
            _weight1.Text = Convert.ToDecimal(_temp.Weight1).ToString("#,#");

            Literal _unit1 = (Literal)e.Item.FindControl("Unit1Literal");
            _unit1.Text = HttpUtility.HtmlEncode(_temp.Unit1);

            Literal _weight2 = (Literal)e.Item.FindControl("Weight2Literal");
            _weight2.Text = Convert.ToDecimal(_temp.Weight2).ToString("#,#");

            Literal _unit2 = (Literal)e.Item.FindControl("Unit2Literal");
            _unit2.Text = HttpUtility.HtmlEncode(_temp.Unit2);

            Literal _price = (Literal)e.Item.FindControl("PriceLiteral");
            _price.Text = Convert.ToDecimal(_temp.Price).ToString("#,#");

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
        }

        protected void AddMultipleButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addMultipleDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeleteMultipleButton_Click(object sender, ImageClickEventArgs e)
        {
            string _error = "";
            string _temp = this.CheckHidden3.Value;
            string[] _tempSplit = _temp.Split(',');

            this.ClearLabel();

            bool _result = this._shippingBL.DeleteMultiPOSMsZoneMultiplePrice(_tempSplit);

            if (_result == true)
            {
                _error = "Delete Success";
            }
            else
            {
                _error = "Delete Failed";
            }
            this.WarningLabel3.Text = _error;
            this.CheckHidden3.Value = "";
            this.AllCheckBox3.Checked = false;
            this.ShowDataDetailMultiple(0);
        }

        protected void DataPagerTop3Repeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this.ShowDataDetailMultiple(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void DataPagerTop3Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        protected void DataPager3Button_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerTop3Repeater.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCountMultiple())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCountMultiple().ToString();
                        _reqPage = Convert.ToInt32(this.RowCountMultiple()) - 1;
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

            this.ShowDataDetailMultiple(_reqPage);
        }

        #endregion

    }
}