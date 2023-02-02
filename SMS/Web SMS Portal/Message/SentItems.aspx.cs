using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SMSLibrary;

namespace SMS.SMSWeb.Message
{
    public partial class SentItems : MessageBase
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

        String _UserID;
        Int32 _organization;
        bool _fgAdmin;

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this.CekSession();

            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");
            this.DataPagerBottomButton.Attributes.Add("Style", "visibility: hidden;");

            if (Session["UserID"] != null)
            {
                _UserID = HttpContext.Current.Session["UserID"].ToString();
                _organization = Convert.ToInt32(HttpContext.Current.Session["Organization"].ToString());
                _fgAdmin = Convert.ToBoolean(HttpContext.Current.Session["FgWebAdmin"].ToString());
            }

            if (!this.Page.IsPostBack == true)
            {
                this.SubPageTitleLiteral.Text = "Sent";

                this.GoImageButton.ImageUrl = "../images/go.jpg";
                this.DeleteButton.ImageUrl = "../images/delete.jpg";
                this.DeleteButton2.ImageUrl = "../images/delete.jpg";

                if (!Convert.ToBoolean(_fgAdmin))
                    this.DeleteButton.Visible = this.DeleteButton2.Visible = false;

                if (_loginBL.getPackageName(_organization.ToString(), _UserID) == "CORPORATE")
                    this.SMSLeft.Visible = false;
                else
                    this.ShowLeftSMS();

                this.ShowCategory();
                this.ClearLabel();
                this.ShowData(0);
            }
        }

        private void ShowLeftSMS()
        {
            this.SMSLeftLabel.Text = _smsMessageBL.GetSMSLimit(_UserID, _organization).ToString();
        }

        protected void ShowCategory()
        {
            this.CategoryDDL.Items.Clear();
            this.CategoryDDL.DataTextField = "CategoryText";
            this.CategoryDDL.DataValueField = "Category";
            this.CategoryDDL.DataSource = this._smsMessageBL.GetListSMSGatewaySendForDDL(_UserID, _organization, _fgAdmin);
            this.CategoryDDL.DataBind();
            this.CategoryDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private double RowCount()
        {
            double _result = 0;

            _result = this._smsMessageBL.RowsCountSMSGatewaySendSentItems(this.CategoryDropDownList.SelectedValue, this.KeywordTextBox.Text, this.CategoryDDL.SelectedValue, _UserID, _organization, _fgAdmin);
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
            this.CheckHidden.Value = "";
            this.TempHidden.Value = "";
            this.AllCheckBox.Checked = false;

            this._page = _prmCurrentPage;

            this.ListRepeater.DataSource = this._smsMessageBL.GetListSMSGatewaySendSentItems(_prmCurrentPage, _maxrow, this.CategoryDropDownList.SelectedValue, this.KeywordTextBox.Text, this.CategoryDDL.SelectedValue, _UserID, _organization, _fgAdmin);
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

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                SMSGatewaySend _smsReceive = (SMSGatewaySend)e.Item.DataItem;
                string _code = _smsReceive.id.ToString();

                if (this.CheckHidden.Value == "")
                {
                    this.CheckHidden.Value = _code;
                }
                else
                {
                    this.CheckHidden.Value += "," + _code;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.TempHidden.ClientID + ", " + _listCheckbox.ClientID + ", " + _code + ", " + this.AllCheckBox.ClientID + " , " + this.CheckHidden.ClientID + ");");

                ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                _viewButton.PostBackUrl = this._sentItemsViewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                _viewButton.ImageUrl = "../images/view.jpg";

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate");
                _tableRow.Attributes.Add("OnMouseOver", "this.className='TableRowOver';");
                _tableRow.Attributes.Add("OnMouseOut", "this.className='TableRow';");

                Literal _catLiteral = (Literal)e.Item.FindControl("CategoryLiteral");
                _catLiteral.Text = HttpUtility.HtmlEncode(_smsReceive.Category);

                Literal _destinationLiteral = (Literal)e.Item.FindControl("DestinationLiteral");
                _destinationLiteral.Text = HttpUtility.HtmlEncode(_smsMessageBL.GetNameFromPhoneBook(_smsReceive.DestinationPhoneNo.ToString(), Convert.ToInt32(_organization), _UserID, _fgAdmin));

                Literal _statusLiteral = (Literal)e.Item.FindControl("StatusLiteral");
                _statusLiteral.Text = HttpUtility.HtmlEncode((_smsReceive.flagSend == null || _smsReceive.flagSend == 0) ? "Queued" : "Sent");

                Literal _userNameLiteral = (Literal)e.Item.FindControl("UserNameLiteral");
                _userNameLiteral.Text = HttpUtility.HtmlEncode(_smsReceive.userID);

                Literal _dateSentLiteral = (Literal)e.Item.FindControl("DateSentLiteral");
                String _hour = Convert.ToDateTime(_smsReceive.DateSent).Hour.ToString().PadLeft(2, '0');
                String _minute = Convert.ToDateTime(_smsReceive.DateSent).Minute.ToString().PadLeft(2, '0');
                //String _second = Convert.ToDateTime(_smsReceive.DateSent).Second.ToString().PadLeft(2, '0');
                _dateSentLiteral.Text = HttpUtility.HtmlEncode(DateFormMapper.GetValue(_smsReceive.DateSent) + " " + _hour + ":" + _minute);
            }
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _error = "";
            string _temp = this.TempHidden.Value;
            string[] _tempsplit = _temp.Split(',');
            string _page = "0";

            this.ClearLabel();

            bool _result = this._smsMessageBL.DeleteMultiSMSGatewaySend(_tempsplit);

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

            Response.Redirect(this._sentItemsPage + "?" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_page, ApplicationConfig.EncryptionKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error);
        }

        protected void GoImageButton_Click(object sender, ImageClickEventArgs e)
        {
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
    }
}