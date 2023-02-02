using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FAProcess
{
    public partial class FAProcessDetailAdd : FAProcessBase
    {
        private FixedAssetsBL _faProcessBL = new FixedAssetsBL();
        private PermissionBL _permBL = new PermissionBL();

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

        private int _year = 0;
        private int _period = 0;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");
            
            if (!this.Page.IsPostBack == true)
            {
                this.ViewState[this._currPageKey] = 0;

                this.PageTitleLiteral.Text = _pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";
                this.GoImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/go.jpg";

                this.ShowData(0);
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private double RowCount()
        {
            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            double _result = 0;

            _result = this._faProcessBL.RowsCountFAProcessForList(Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codePeriod), ApplicationConfig.EncryptionKey)), this.CategoryDropDownList.SelectedValue, this.KeywordTextBox.Text);
            _result = System.Math.Ceiling(_result / (double)_maxrow);

            return _result;
        }

        private void ShowPage(int _prmYear, int _prmPeriod, int _prmCurrentPage)
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
            }
            else
            {
                this.DataPagerTopRepeater.DataSource = from _query in _pageNumber
                                                       select _query;
                this.DataPagerTopRepeater.DataBind();
            }
        }

        public void ShowData(Int32 _prmReqPage)
        {
            this.TempHidden.Value = "";

            this._page = _prmReqPage;

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            GLFAProcessHd _glFAProcessHd = this._faProcessBL.GetSingleFAProcessHd(Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codePeriod), ApplicationConfig.EncryptionKey)));

            this.ListRepeater.DataSource = this._faProcessBL.FAProcessForList(_prmReqPage, _maxrow, Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codePeriod), ApplicationConfig.EncryptionKey)), this.CategoryDropDownList.SelectedValue, this.KeywordTextBox.Text);
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            List<MsFixedAsset> _msFixedAsset = new List<MsFixedAsset>();
            _msFixedAsset = this._faProcessBL.FAProcessForListA(Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codePeriod), ApplicationConfig.EncryptionKey)), this.CategoryDropDownList.SelectedValue, this.KeywordTextBox.Text);
            this.AllHidden.Value = "";
            foreach (MsFixedAsset _item in _msFixedAsset)
            {
                if (this.AllHidden.Value == "")
                {
                    this.AllHidden.Value = _item.FACode;
                }
                else
                {
                    this.AllHidden.Value += "," + _item.FACode;
                }
            }

            this.ShowPage(_glFAProcessHd.Year, _glFAProcessHd.Period, _prmReqPage);
        }

        private Boolean IsSelected(string _prmCode)
        {
            Boolean _result = false;

            string[] _code = this.CheckHidden.Value.Split(',');

            for (int i = 0; i < _code.Length; i++)
            {
                if (_code[i] == _prmCode)
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
                MsFixedAsset _temp = (MsFixedAsset)e.Item.DataItem;

                string _faCode = _temp.FACode.ToString();

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _faCode;
                }
                else
                {
                    this.TempHidden.Value += "," + _faCode;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = Convert.ToInt32(this.ViewState[this._currPageKey]) * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                CheckBox _cb = (CheckBox)e.Item.FindControl("ListCheckBox");
                _cb.Checked = this.IsSelected(_faCode);
                //_cb.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + "," + _cb.ClientID + ",'" + _faCode + "', '" + _awal + "' , '" + _akhir + "', '" + _cbox + "','" + _tempHidden + "' )");
                _cb.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + "," + _cb.ClientID + ",'" + _faCode + "', '" + _awal + "' , '" + _akhir + "', '" + _cbox + "' );");

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

                Literal _faCodeLiteral = (Literal)e.Item.FindControl("FACodeLiteral");
                _faCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.FACode);

                Literal _faName = (Literal)e.Item.FindControl("FANameLiteral");
                _faName.Text = HttpUtility.HtmlEncode(_temp.FAName);

                Literal _balanceLife = (Literal)e.Item.FindControl("BalanceLifeLiteral");
                _balanceLife.Text = HttpUtility.HtmlEncode(_temp.LifeAdd.ToString());

                Literal _balanceAmount = (Literal)e.Item.FindControl("BalanceAmountLiteral");
                //decimal _balance = Convert.ToDecimal((_temp.AmountProcess == null) ? 0 : _temp.AmountProcess);
                _balanceAmount.Text = (_temp.AmountProcess == 0 ? "0" : HttpUtility.HtmlEncode(_temp.AmountProcess.ToString("#,###.##")));

                Literal _amountDepr = (Literal)e.Item.FindControl("AmountDeprLiteral");
                decimal _amount = Convert.ToDecimal((_temp.AmountAdd == null) ? 0 : _temp.AmountAdd);
                _amountDepr.Text = (_temp.AmountAdd == 0 ? "0" : HttpUtility.HtmlEncode(_amount.ToString("#,###.##")));

                Literal _adjustDepr = (Literal)e.Item.FindControl("AdjustDeprLiteral");
                _adjustDepr.Text = "0";

                Literal _totalDepr = (Literal)e.Item.FindControl("TotalDeprLiteral");
                decimal _total = Convert.ToDecimal(_amountDepr.Text) + Convert.ToDecimal(_adjustDepr.Text);
                _totalDepr.Text = (_total == 0 ? "0" : HttpUtility.HtmlEncode(_total.ToString("#,###.##")));
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.GrapAllCheckBox.Checked == false)
            {
                if (this._faProcessBL.AddList(this.CheckHidden.Value.Split(','), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codePeriod), ApplicationConfig.EncryptionKey))) != null)
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codePeriod + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codePeriod)));
                }
            }
            else
            {
                if (this._faProcessBL.AddList(this.AllHidden.Value.Split(','), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codePeriod), ApplicationConfig.EncryptionKey))) != null)
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codePeriod + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codePeriod)));
                }
            }
        }

        protected void DataPagerRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this.ShowData(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void DataPagerRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

            this.ShowData(_reqPage);
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codePeriod + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codePeriod)));
        }

        protected void GoImageButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.ClearLabel();
            this.ShowData(0);
        }
    }
}