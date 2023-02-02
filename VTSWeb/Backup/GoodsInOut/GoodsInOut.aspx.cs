﻿using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web.UI.HtmlControls;
using VTSWeb.Database;
using VTSWeb.SystemConfig;
using VTSWeb.BusinessRule;
using VTSWeb.Common;
using VTSWeb.DataMapping;

namespace VTSWeb.UI
{
    public partial class GoodsInOut : GoodsInOutBase
    {
        private GoodsInOutBL _GoodsInOutBL = new GoodsInOutBL();
        private MsCustomerBL _customerBL = new MsCustomerBL();

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _nomor = 0;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private string _currPageKey = "CurrentPage";


        protected void Page_Load(object sender, EventArgs e)
        {
            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");
            this.DataPagerBottomButton.Attributes.Add("Style", "visibility: hidden;");

            HttpCookie cookie = Request.Cookies[ApplicationConfig.CookiesPreferences];
            if (cookie == null)
            {
                Response.Redirect("..\\Login.aspx");
            }

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;
                this.ViewState[this._currPageKey] = 0;
                this.ClearLabel();
                this.ShowDataHd(0);
                this.AddButton.ImageUrl = "../images/add.jpg";
                this.AddButton2.ImageUrl = "../images/add.jpg";
                this.DeleteButton.ImageUrl = "../images/delete.jpg";
                this.DeleteButton2.ImageUrl = "../images/delete.jpg";
                this.GoImageButton.ImageUrl = "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + ApplicationConfig.StringSeparatorPublish + "images/go.jpg";
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabelHd.Text = "";

        }
        private double RowCount()
        {
            double _result = 0;

            _result = this._GoodsInOutBL.RowsCount(this.GoodsDropDownList.SelectedValue, this.KeywordTextBox.Text);
            _result = System.Math.Ceiling(_result / (double)_maxrow);

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



        private Boolean IsCheckedAllHd()
        {
            Boolean _result = true;

            foreach (RepeaterItem _row in this.ListRepeaterHd.Items)
            {
                CheckBox _chk = (CheckBox)_row.FindControl("ListCheckBox");

                if (_chk.Checked == false)
                {
                    _result = false;
                    break;
                }
            }

            if (this.ListRepeaterHd.Items.Count == 0)
            {
                _result = false;
            }

            return _result;
        }
        //private Boolean IsCheckedAllDt()
        //{
        //    Boolean _result = true;

        //    foreach (RepeaterItem _row in this.ListRepeaterDt.Items)
        //    {
        //        CheckBox _chk = (CheckBox)_row.FindControl("ListCheckBox");

        //        if (_chk.Checked == false)
        //        {
        //            _result = false;
        //            break;
        //        }
        //    }

        //    if (this.ListRepeaterDt.Items.Count == 0)
        //    {
        //        _result = false;
        //    }

        //    return _result;
        //}

        //private void ShowCustomerDDLHd()
        //{
        //    this.CustomerDDLHd.DataTextField = "CustName";
        //    this.CustomerDDLHd.DataValueField = "CustCode";
        //    this.CustomerDDLHd.DataSource = this._customerBL.GetCustomerForDDL();
        //    this.CustomerDDLHd.DataBind();
        //    this.CustomerDDLHd.Items.Insert(0, new ListItem("ALL", ""));
        //}
        private void ShowDataHd(Int32 _prmCurrentPage)
        {
            this.TempHiddenHd.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._page = _prmCurrentPage;

            this.ListRepeaterHd.DataSource = this._GoodsInOutBL.GetListHd(_prmCurrentPage, _maxrow, this.GoodsDropDownList.SelectedValue, this.KeywordTextBox.Text);
            this.ListRepeaterHd.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHiddenHd.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            this.AllCheckBox.Checked = this.IsCheckedAllHd();

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");
            this.DeleteButton2.Attributes.Add("OnClick", "return AskYouFirst();");
            this.ShowPage(_prmCurrentPage);
        }
        private Boolean IsCheckedHd(String _prmValue)
        {
            Boolean _result = false;
            String[] _value = this.CheckHiddenHd.Value.Split(',');

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

        protected void ListRepeater_ItemDataBoundHd(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                GoodsInOutHd _temp = (GoodsInOutHd)e.Item.DataItem;
                string _code = _temp.TransNmbr.ToString();
                //string _areaCode = "";
                //string _purposeCode = "";

                if (this.TempHiddenHd.Value == "")
                {
                    this.TempHiddenHd.Value = _code;
                }
                else
                {
                    this.TempHiddenHd.Value += "," + _code;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHiddenHd.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");
                _listCheckbox.Checked = this.IsCheckedHd(_code);

                //ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                //_viewButton.PostBackUrl = this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                //_viewButton.ImageUrl = "../images/view_detail.jpg";

                ImageButton _viewDetailButton = (ImageButton)e.Item.FindControl("ViewDetailButton");
                _viewDetailButton.PostBackUrl = this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                _viewDetailButton.ImageUrl = "../images/view_detail.jpg";

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate");
                _tableRow.Attributes.Add("OnMouseOver", "this.className='TableRowOver';");
                _tableRow.Attributes.Add("OnMouseOut", "this.className='TableRow';");

                Literal _TransNumbLiteral = (Literal)e.Item.FindControl("TransNumbLiteral");
                _TransNumbLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                Literal _FileNumbDateLiteral = (Literal)e.Item.FindControl("FileNumbDateLiteral");
                _FileNumbDateLiteral.Text = HttpUtility.HtmlEncode(_temp.FileNmbr);

                Literal _TransTypeLiteral = (Literal)e.Item.FindControl("TransTypeLiteral");
                _TransTypeLiteral.Text = HttpUtility.HtmlEncode(_temp.TransType);

                Literal _CompanyNameLiteral = (Literal)e.Item.FindControl("CompanyNameLiteral");
                _CompanyNameLiteral.Text = HttpUtility.HtmlEncode(_customerBL.GetCustomerNameByCode(_temp.CustCode));

                Literal _TransactionDateLiteral = (Literal)e.Item.FindControl("TransactionDateLiteral");
                _TransactionDateLiteral.Text = HttpUtility.HtmlEncode(DateFormMapping.GetValue(_temp.TransDate));

                Literal _RemarkLiteral = (Literal)e.Item.FindControl("RemarkLiteral");
                _RemarkLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);

                //(ClearanceCompleteDataMapping.GetStatus(_temp.Status));
                Literal _CompleteStatusLiteral = (Literal)e.Item.FindControl("CompleteStatusLiteral");
                _CompleteStatusLiteral.Text = HttpUtility.HtmlEncode(ClearanceCompleteDataMapping.GetStatus(_temp.Status));

            }
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addPage);
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHiddenHd.Value;
            string[] _tempSplit = _temp.Split(',');
            //string _page = "0";

            this.ClearLabel();

            bool _result = this._GoodsInOutBL.DeleteMultiHd(_tempSplit);

            if (_result == true)
            {
                this.WarningLabelHd.Text = "Delete Success";
            }
            else
            {
                this.WarningLabelHd.Text = "Delete Failed";
            }

            this.CheckHiddenHd.Value = "";
            this.AllCheckBox.Checked = false;

            this.ViewState[this._currPageKey] = 0;
            this.ShowDataHd(0);

            //Response.Redirect(this._homePage + "?" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_page, ApplicationConfig.EncryptionKey)));
        }
        protected void DataPagerTopRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this.ShowDataHd(Convert.ToInt32(e.CommandArgument));
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

            this.ShowDataHd(_reqPage);
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

            this.ShowDataHd(_reqPage);
        }


        protected void GoImageButton_Click(object sender, ImageClickEventArgs e)
        {

            this.ViewState[this._currPageKey] = 0;

            this.ClearLabel();
            this.ShowDataHd(0);
        }

    }
}
