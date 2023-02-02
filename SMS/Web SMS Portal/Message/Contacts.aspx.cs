using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SMSLibrary;

namespace SMS.SMSWeb.Message
{
    public partial class Contacts : MessageBase
    {
        private SMSMessageBL _smsMessageBL = new SMSMessageBL();
        private LoginBL _loginBL = new LoginBL();

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

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this.CekSession();

            if (Session["FgWebAdmin"] != null)
                if (Session["FgWebAdmin"].ToString() == "False" && _loginBL.getPackageName(Session["Organization"].ToString(), Session["UserID"].ToString()) == "PERSONAL") Response.Redirect("../Message/Compose.aspx");

            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");
            this.DataPagerBottomButton.Attributes.Add("Style", "visibility: hidden;");

            if (!this.Page.IsPostBack == true)
            {
                this.SubPageTitleLiteral.Text = "CONTACT LIST";

                this.GoImageButton.ImageUrl = "../images/go.jpg";
                this.GoImageButton2.ImageUrl = "../images/go.jpg";
                this.AddButton.ImageUrl = "../images/add.jpg";
                this.AddButton2.ImageUrl = "../images/add.jpg";
                this.DeleteButton.ImageUrl = "../images/delete.jpg";
                this.DeleteButton2.ImageUrl = "../images/delete.jpg";
                this.SendSMSButton.ImageUrl = "../images/SendSMS.jpg";
                this.SendSMSButton2.ImageUrl = "../images/SendSMS.jpg";
                this.UploadButton.ImageUrl = "../images/UploadXLS.jpg";
                this.UploadButton2.ImageUrl = "../images/UploadXLS.jpg";
                this.BasicImageButton.ImageUrl = "../images/basic_search.jpg";
                this.AdvanceButton.ImageUrl = "../images/advanced_search.jpg";

                this.ClearLabel();
                this.ShowData(0);
                this.SetPanel();
            }
        }

        protected void SetPanel()
        {
            this.QuickSearchPanel.Visible = true;
            this.AdvanceSearchPanel.Visible = false;
            this.BasicImageButton.Visible = false;
            this.AdvanceButton.Visible = true;
            this.ViewListPanel.Visible = false;
            this.ViewNameCardPanel.Visible = false;
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private double RowCount()
        {
            double _result = 0;

            _result = this._smsMessageBL.RowsCountMsPhoneBook(this.CategoryDropDownList.SelectedValue, this.KeywordTextBox.Text, Session["Organization"].ToString(), Session["UserID"].ToString(), Session["FgWebAdmin"].ToString());
            _result = System.Math.Ceiling(_result / (double)_maxrow);

            return _result;
        }

        private double RowCountAdvance()
        {
            double _result = 0;

            _result = this._smsMessageBL.RowsCountAdvanceMsPhoneBook(this.KeywordTextBox2.Text, Session["Organization"].ToString(), Session["UserID"].ToString(), Session["FgWebAdmin"].ToString());
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

        private void ShowPageListViewAdvance(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCountAdvance();

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
                this.DataPagerTopRepeater3.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTopRepeater3.DataBind();

                _flag = true;
                _nextFlag = false;
                _lastFlag = false;
                _navMark = _navMarkBackup;

                this.DataPagerBottomRepeater3.DataSource = from _query in _pageNumber
                                                           select _query;
                this.DataPagerBottomRepeater3.DataBind();
            }
            else
            {
                this.DataPagerTopRepeater3.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTopRepeater3.DataBind();

                this.DataPagerBottomRepeater3.DataSource = from _query in _pageNumber
                                                           select _query;
                this.DataPagerBottomRepeater3.DataBind();
            }
        }

        private void ShowPageNameCardAdvance(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCountAdvance();

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
                this.DataPagerTopRepeater2.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTopRepeater2.DataBind();

                _flag = true;
                _nextFlag = false;
                _lastFlag = false;
                _navMark = _navMarkBackup;

                this.DataPagerBottomRepeater2.DataSource = from _query in _pageNumber
                                                           select _query;
                this.DataPagerBottomRepeater2.DataBind();
            }
            else
            {
                this.DataPagerTopRepeater2.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTopRepeater2.DataBind();

                this.DataPagerBottomRepeater2.DataSource = from _query in _pageNumber
                                                           select _query;
                this.DataPagerBottomRepeater2.DataBind();
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

        protected void DataPagerTopRepeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this.ShowDataNameCard(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void DataPagerTopRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        protected void DataPagerTopRepeater3_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this.ShowDataAdvance(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void DataPagerTopRepeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        protected void DataPagerBottomButton3_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerBottomRepeater3.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCountAdvance())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCountAdvance().ToString();
                        _reqPage = Convert.ToInt32(this.RowCountAdvance()) - 1;
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

            this.ShowDataAdvance(_reqPage);
        }

        protected void DataPagerBottomButton2_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerBottomRepeater2.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCountAdvance())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCountAdvance().ToString();
                        _reqPage = Convert.ToInt32(this.RowCountAdvance()) - 1;
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

            this.ShowDataNameCard(_reqPage);
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

        protected void DataPagerButton3_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerTopRepeater3.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCountAdvance())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCountAdvance().ToString();
                        _reqPage = Convert.ToInt32(this.RowCountAdvance()) - 1;
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

            this.ShowDataAdvance(_reqPage);
        }

        protected void DataPagerButton2_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerTopRepeater2.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCountAdvance())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCountAdvance().ToString();
                        _reqPage = Convert.ToInt32(this.RowCountAdvance()) - 1;
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

            this.ShowDataNameCard(_reqPage);
        }

        private void ShowData(Int32 _prmCurrentPage)
        {
            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";
            this.AllCheckBox.Checked = false;

            this._page = _prmCurrentPage;

            this.ListRepeater.DataSource = this._smsMessageBL.GetListMsPhoneBook(_prmCurrentPage, _maxrow, this.CategoryDropDownList.SelectedValue, this.KeywordTextBox.Text, Session["Organization"].ToString(), Session["UserID"].ToString(), Convert.ToBoolean(Session["FgWebAdmin"].ToString()));
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "return CheckAllClick(" + TempHidden.ClientID + " , " + CheckHidden.ClientID + " , " + this.AllCheckBox.ClientID + ");");

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");
            this.DeleteButton2.Attributes.Add("OnClick", "return AskYouFirst();");

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            this.ShowPage(_prmCurrentPage);
        }

        private void ShowDataAdvance(Int32 _prmCurrentPage)
        {
            this._page = _prmCurrentPage;

            this.ListRepeaterAdvance.DataSource = this._smsMessageBL.GetListMsPhoneBookAdvanceSearch(_prmCurrentPage, _maxrow, this.KeywordTextBox2.Text, Session["Organization"].ToString(), Session["UserID"].ToString(), Convert.ToBoolean(Session["FgWebAdmin"].ToString()));
            this.ListRepeaterAdvance.DataBind();

            this.ShowPageListViewAdvance(_prmCurrentPage);
        }

        private void ShowDataNameCard(Int32 _prmCurrentPage)
        {
            this._page = _prmCurrentPage;

            this.ListCardImageRepeater.DataSource = this._smsMessageBL.GetListMsPhoneBookNameCard(_prmCurrentPage, _maxrow, this.KeywordTextBox2.Text, Session["Organization"].ToString(), Session["UserID"].ToString(), Convert.ToBoolean(Session["FgWebAdmin"].ToString()));
            this.ListCardImageRepeater.DataBind();

            this.ShowPageNameCardAdvance(_prmCurrentPage);
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                MsPhoneBook _temp = (MsPhoneBook)e.Item.DataItem;
                string _code = _temp.id.ToString();

                if (this.CheckHidden.Value == "")
                    this.CheckHidden.Value = _code;
                else
                    this.CheckHidden.Value += "," + _code;


                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.TempHidden.ClientID + ", " + _listCheckbox.ClientID + ", " + _code + ", " + this.AllCheckBox.ClientID + " , " + this.CheckHidden.ClientID + ");");

                //ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                //_viewButton.PostBackUrl = this._contactsViewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                //_viewButton.ImageUrl = "../images/view.jpg";

                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                _editButton.PostBackUrl = this._contactsEditPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                _editButton.ImageUrl = "../images/view.jpg";

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate");
                _tableRow.Attributes.Add("OnMouseOver", "this.className='TableRowOver';");
                _tableRow.Attributes.Add("OnMouseOut", "this.className='TableRow';");

                Literal _nameLiteral = (Literal)e.Item.FindControl("NameLiteral");
                _nameLiteral.Text = HttpUtility.HtmlEncode(_temp.Name);

                Literal _phoneNumberLiteral = (Literal)e.Item.FindControl("PhoneNumberLiteral");
                _phoneNumberLiteral.Text = HttpUtility.HtmlEncode(_temp.PhoneNumber);

                Literal _phoneBookGroupLiteral = (Literal)e.Item.FindControl("PhoneBookGroupLiteral");
                _phoneBookGroupLiteral.Text = HttpUtility.HtmlEncode(_temp.PhoneBookGroup);
            }
        }

        protected void ListRepeaterAdvance_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                MsPhoneBook _temp = (MsPhoneBook)e.Item.DataItem;
                string _code = _temp.id.ToString();

                if (this.CheckHidden.Value == "")
                    this.CheckHidden.Value = _code;
                else
                    this.CheckHidden.Value += "," + _code;


                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                //CheckBox _listCheckbox;
                //_listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                //_listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.TempHidden.ClientID + ", " + _listCheckbox.ClientID + ", " + _code + ", " + this.AllCheckBox.ClientID + " , " + this.CheckHidden.ClientID + ");");

                //ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                //_viewButton.PostBackUrl = this._contactsViewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                //_viewButton.ImageUrl = "../images/view.jpg";

                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                _editButton.PostBackUrl = this._contactsEditPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                _editButton.ImageUrl = "../images/view.jpg";

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate");
                _tableRow.Attributes.Add("OnMouseOver", "this.className='TableRowOver';");
                _tableRow.Attributes.Add("OnMouseOut", "this.className='TableRow';");

                Literal _nameLiteral = (Literal)e.Item.FindControl("NameLiteral");
                _nameLiteral.Text = HttpUtility.HtmlEncode(_temp.Name);

                Literal _phoneNumberLiteral = (Literal)e.Item.FindControl("PhoneNumberLiteral");
                _phoneNumberLiteral.Text = HttpUtility.HtmlEncode(_temp.PhoneNumber);

                Literal _phoneBookGroupLiteral = (Literal)e.Item.FindControl("PhoneBookGroupLiteral");
                _phoneBookGroupLiteral.Text = HttpUtility.HtmlEncode(_temp.PhoneBookGroup);
            }
        }

        protected void ListCardImageRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                MsPhoneBook _temp = (MsPhoneBook)e.Item.DataItem;

                string _code = _temp.id.ToString();

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate");
                _tableRow.Attributes.Add("OnMouseOver", "this.className='TableRowOver';");
                _tableRow.Attributes.Add("OnMouseOut", "this.className='TableRow';");

                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                _editButton.PostBackUrl = this._contactsEditPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                _editButton.ImageUrl = "../images/view.jpg";

                Image _cardImage = (Image)e.Item.FindControl("CardImage");
                _cardImage.Attributes.Add("style", "width: 150px;");
                _cardImage.Attributes.Add("style", "height: 80px;");
                _cardImage.ImageUrl = "../NameCard/" + _temp.NameCardPicture;

                Literal _remarkLiteral = (Literal)e.Item.FindControl("RemarkLiteral");
                _remarkLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);
            }
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._contactsAddPage);
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _error = "";
            string _temp = this.TempHidden.Value;
            string[] _tempsplit = _temp.Split(',');
            string _page = "0";

            this.ClearLabel();

            bool _result = this._smsMessageBL.DeleteMultiMsPhoneBook(_tempsplit);

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

            Response.Redirect(this._contactsPage + "?" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_page, ApplicationConfig.EncryptionKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error);
        }

        protected void GoImageButton_Click(object sender, ImageClickEventArgs e)
        {
            //this.PanelList.Visible = true;
            //this.ImagePanel.Visible = false;

            this.ViewState[this._currPageKey] = 0;

            this.ClearLabel();
            this.ShowData(0);
        }

        protected void StatusDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.ClearLabel();
            this.ShowData(0);
        }

        protected void CategoryDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.ClearLabel();
            this.ShowData(0);
        }
        protected void SendSMSButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../Message/Compose.aspx?contact=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TempHidden.Value, ApplicationConfig.EncryptionKey)));
        }
        protected void UploadButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._contactsUploadPage);
        }

        protected void AdvanceButton_Click(object sender, ImageClickEventArgs e)
        {
            this.QuickSearchPanel.Visible = false;
            this.AdvanceSearchPanel.Visible = true;
            this.ViewListPanel.Visible = true;
            this.ViewNameCardPanel.Visible = false;
            this.BasicImageButton.Visible = true;
            this.AdvanceButton.Visible = false;

            this.ViewState[this._currPageKey] = 0;

            this.ClearLabel();
            this.ShowDataAdvance(0);
        }

        protected void GoImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.ClearLabel();
            if(this.ViewListRBL.SelectedValue == "0")
            {
                this.ShowDataAdvance(0);
            }
            else if (this.ViewListRBL.SelectedValue == "1")
            {
                this.ShowDataNameCard(0);
            }
        }

        protected void BasicImageButton_Click(object sender, ImageClickEventArgs e)
        {
            this.QuickSearchPanel.Visible = true;
            this.AdvanceSearchPanel.Visible = false;
            this.ViewListPanel.Visible = false;
            this.ViewNameCardPanel.Visible = false;
            this.BasicImageButton.Visible = false;
            this.AdvanceButton.Visible = true;

            this.ViewState[this._currPageKey] = 0;

            this.ClearLabel();
            this.ShowData(0);
        }

        //protected void ViewListImageButton_Click(object sender, ImageClickEventArgs e)
        //{
        //    this.ImagePanel.Visible = false;
        //    this.PanelList.Visible = true;

        //    this.CheckHidden.Value = "";
        //    this.TempHidden.Value = "";
        //    this.AllCheckBox.Checked = false;
        //    int _prmCurrentPage = 0;
        //    this._page = _prmCurrentPage;

        //    this.ListRepeater.DataSource = this._smsMessageBL.GetListMsPhoneBookAdvanceSearch(_prmCurrentPage, _maxrow, this.KeywordTextBox2.Text, Session["Organization"].ToString(), Session["UserID"].ToString(), Convert.ToBoolean(Session["FgWebAdmin"].ToString()));
        //    this.ListRepeater.DataBind();

        //    this.AllCheckBox.Attributes.Add("OnClick", "return CheckAllClick(" + TempHidden.ClientID + " , " + CheckHidden.ClientID + " , " + this.AllCheckBox.ClientID + ");");

        //    this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");
        //    this.DeleteButton2.Attributes.Add("OnClick", "return AskYouFirst();");

        //    if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
        //    {
        //        this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
        //    }

        //    this.ShowPage(_prmCurrentPage);
        //}

        //protected void ViewImageCard_Click(object sender, ImageClickEventArgs e)
        //{
        //    this.ImagePanel.Visible = true;
        //    this.PanelList.Visible = false;

        //    int _prmCurrentPage = 0;
        //    this._page = _prmCurrentPage;

        //    this.ListCardImageRepeater.DataSource = this._smsMessageBL.GetListMsPhoneBookNameCard(_prmCurrentPage, _maxrow, this.KeywordTextBox2.Text, Session["Organization"].ToString(), Session["UserID"].ToString(), Convert.ToBoolean(Session["FgWebAdmin"].ToString()));
        //    this.ListCardImageRepeater.DataBind();

        //    this.ShowPage(_prmCurrentPage);
        //}

        protected void ViewListRBL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.ViewListRBL.SelectedValue == "0")
            {
                this.ViewListPanel.Visible = true;
                this.ViewNameCardPanel.Visible = false;
                this.QuickSearchPanel.Visible = false;

                this.ViewState[this._currPageKey] = 0;

                this.ClearLabel();
                this.ShowDataAdvance(0);

            }
            else if (this.ViewListRBL.SelectedValue == "1")
            {
                this.ViewListPanel.Visible = false;
                this.ViewNameCardPanel.Visible = true;
                this.QuickSearchPanel.Visible = false;

                this.ViewState[this._currPageKey] = 0;

                this.ClearLabel();
                this.ShowDataNameCard(0);
            }
        }
}
}