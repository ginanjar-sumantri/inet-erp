using System;
using System.Web;
using System.Web.UI;
using System.Linq;
using SMSLibrary;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace SMS.SMSWeb.Message
{
    public partial class ContactsEdit : MessageBase
    {
        private SMSMessageBL _smsMessageBL = new SMSMessageBL();
        private LoginBL _loginBL = new LoginBL();

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;
        private int?[] _navMark2 = { null, null, null, null };
        private bool _flag2 = true;
        private bool _nextFlag2 = false;
        private bool _lastFlag2 = false;

        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _maxrow2 = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength2 = Convert.ToInt32(ApplicationConfig.DataPagerRange);

        private string _currPageKey = "CurrentPage";
        private string _currPageKey2 = "CurrentPage";

        String _userID;
        Int32 _organization;
        bool _fgAdmin;

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (HttpContext.Current.Session["userID"] != null && HttpContext.Current.Session["Organization"] != null && HttpContext.Current.Session["FgWebAdmin"] != null)
            {
                if ((HttpContext.Current.Session["userID"].ToString() == "") || (HttpContext.Current.Session["Organization"].ToString() == "") || (HttpContext.Current.Session["FgWebAdmin"].ToString() == ""))
                    Response.Redirect("../Login/Login.aspx");
            }
            else Response.Redirect("../Login/Login.aspx");

            if (Session["FgAdmin"] != null)
                if (Session["FgAdmin"].ToString() == "False" && _loginBL.getPackageName(Session["Organization"].ToString(), Session["UserID"].ToString()) == "PERSONAL") Response.Redirect("../Message/Compose.aspx");

            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");
            this.DataPagerButton2.Attributes.Add("Style", "visibility: hidden;");

            _userID = HttpContext.Current.Session["userID"].ToString();
            _organization = Convert.ToInt32(HttpContext.Current.Session["Organization"].ToString());
            _fgAdmin = Convert.ToBoolean(HttpContext.Current.Session["FgWebAdmin"].ToString());

            if (!this.Page.IsPostBack == true)
            {
               
                this.SubPageTitleLiteral.Text = "Group Edit";

                this.SaveButton.ImageUrl = "../images/save.jpg";
                this.DeleteButton.ImageUrl = "../images/delete.jpg";
                this.GoImageButton.ImageUrl = "../images/go.jpg";
                this.ResetButton.ImageUrl = "../images/reset.jpg";
                this.CancelButton.ImageUrl = "../images/cancel.jpg";

                this.ClearLabel();
                this.ShowData(0);

                this.ShowContactList(0);
                this.SetCheckBox();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData(Int32 _prmCurrentPage)
        {
            String _groupName = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            this.GroupNameTextBox.Text = _groupName;
            this.GroupNameHiddenField.Value = _groupName;

            this.ListRepeater.DataSource = this._smsMessageBL.GetListPhoneGroupByGroupName(_prmCurrentPage, _maxrow, _groupName, _organization.ToString(),_userID,_fgAdmin.ToString());
            this.ListRepeater.DataBind();

            this.ShowPage(_prmCurrentPage);
        }

        private double RowCount()
        {
            double _result = 0;

            _result = this._smsMessageBL.RowCountPhoneGroupByGroupName(this.GroupNameTextBox.Text,_organization.ToString(),_userID,_fgAdmin.ToString());
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
            }
            else
            {
                this.DataPagerTopRepeater.DataSource = null;
                this.DataPagerTopRepeater.DataBind();

            }
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                MsPhoneBook _temp = (MsPhoneBook)e.Item.DataItem;
                String _code = _temp.id.ToString();

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no += 1;
                _noLiteral.Text = _no.ToString();

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.TempHidden.ClientID + ", this, " + _code + ", " + this.AllCheckBox.ClientID + " , " + this.CheckHidden.ClientID + ")");


                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate");
                _tableRow.Attributes.Add("OnMouseOver", "this.className='TableRowOver';");
                _tableRow.Attributes.Add("OnMouseOut", "this.className='TableRow';");

                Literal _nameLiteral = (Literal)e.Item.FindControl("ContactNameLiteral");
                _nameLiteral.Text = HttpUtility.HtmlEncode(_temp.Name);

                Literal _phoneNoLiteral = (Literal)e.Item.FindControl("PhoneNoLiteral");
                _phoneNoLiteral.Text = HttpUtility.HtmlEncode(_temp.PhoneNumber);
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

            this.ShowData(_reqPage);
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _error = "";
            string _temp = this.TempHidden.Value;
            string[] _tempSplit = _temp.Split(',');
            string _page = "0";

            this.ClearLabel();

            bool _result = this._smsMessageBL.DeleteMultiContactListPhoneGroup(_tempSplit);

            if (_result == true)
            {
                _error = "Delete Success";
            }
            else
            {
                _error = "Delete Failed";
            }

            this.CheckHidden2.Value = "";
            this.AllCheckBox2.Checked = false;

            Response.Redirect(this._phoneGroupEditPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_page, ApplicationConfig.EncryptionKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error);
        }

        private void ShowContactList(Int32 _prmCurrentPage)
        {
            List<MsPhoneBook> _listMsPhoneBook = this._smsMessageBL.GetListAllContactListNotYetAssigned(this.ContactFilterDropDownList.SelectedValue, this.KeywordTextBox.Text,_organization.ToString(),_userID,_fgAdmin.ToString());
            this.AllHidden2.Value = "";
            foreach (MsPhoneBook _item in _listMsPhoneBook)
            {
                if (this.AllHidden2.Value == "")
                    this.AllHidden2.Value = _item.id.ToString();
                else
                    this.AllHidden2.Value += "," + _item.id.ToString();
            }

            this.ContactCheckBoxList.ClearSelection();
            this.ContactCheckBoxList.Items.Clear();
            this.ContactCheckBoxList.DataTextField = "Name";
            this.ContactCheckBoxList.DataValueField = "id";
            //List<MsPhoneBook> _listTempMsPhoneBook = this._smsMessageBL.GetListContactListNotYetAssigned(this.ContactFilterDropDownList.SelectedValue, this.KeywordTextBox.Text, _prmCurrentPage, _maxrow, Session["Organization"].ToString(), Session["UserID"].ToString(),Session["FgAdmin"].ToString());
            List<MsPhoneBook> _listTempMsPhoneBook = this._smsMessageBL.GetListContactListNotYetAssigned(this.ContactFilterDropDownList.SelectedValue, this.KeywordTextBox.Text, Session["Organization"].ToString(), Session["UserID"].ToString(), Session["FgAdmin"].ToString());
            foreach (MsPhoneBook _item in _listTempMsPhoneBook)
            {
                if (this.CheckHidden2.Value == "")
                    this.CheckHidden2.Value = _item.id.ToString();
                else
                    this.CheckHidden2.Value += "," + _item.id.ToString();
            }
            this.ContactCheckBoxList.DataSource = _listTempMsPhoneBook;
            this.ContactCheckBoxList.DataBind();

            this.ShowPage(_prmCurrentPage);
        }

        private double RowCountContactList()
        {
            double _result = 0;
            _result = this._smsMessageBL.RowCountPhoneGroupByGroupName(this.GroupNameTextBox.Text,_organization.ToString(),_userID,_fgAdmin.ToString());
            _result = System.Math.Ceiling(_result / (double)_maxrow2);

            return _result;
        }

        private void ShowPage2(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCountContactList();

            if (_prmCurrentPage - _maxlength2 > 0)
            {
                min = _prmCurrentPage - _maxlength2;
            }
            else
            {
                min = 0;
            }

            if (_prmCurrentPage + _maxlength2 < q)
            {
                max = _prmCurrentPage + _maxlength2 + 1;
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
                    this._navMark2[0] = 0;

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark2[0]);
                    _pageNumberElement++;

                    this._navMark2[1] = Convert.ToInt32(Math.Abs(_prmCurrentPage - 1));

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark2[1]);
                    _pageNumberElement++;
                }

                for (; i < max; i++)
                {
                    _pageNumber[_pageNumberElement] = i;
                    _pageNumberElement++;
                }

                if (_prmCurrentPage < q - 1)
                {
                    this._navMark2[2] = Convert.ToInt32(_prmCurrentPage + 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark2[2]);
                    _pageNumberElement++;

                    if (_pageNumber[_pageNumberElement - 2] > _pageNumber[_pageNumberElement - 1])
                    {
                        this._flag2 = true;
                    }

                    this._navMark2[3] = Convert.ToInt32(q - 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark2[3]);
                    _pageNumberElement++;
                }

                int?[] _navMarkBackup = new int?[4];
                this._navMark2.CopyTo(_navMarkBackup, 0);
                this.DataPagerTopRepeater2.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTopRepeater2.DataBind();

                _flag2 = true;
                _nextFlag2 = false;
                _lastFlag2 = false;
                _navMark2 = _navMarkBackup;
            }
            else
            {
                this.DataPagerTopRepeater2.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTopRepeater2.DataBind();
            }
        }
        protected void DataPagerTopRepeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager2")
            {
                this.ViewState[this._currPageKey2] = Convert.ToInt32(e.CommandArgument);

                this.TempHidden2.Value = "";

                this.ShowContactList(Convert.ToInt32(e.CommandArgument));
                this.SetCheckBox();
            }
        }
        protected void DataPagerTopRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                int _pageNumber = (int)e.Item.DataItem;

                if (Convert.ToInt32(this.ViewState[this._currPageKey2]) == _pageNumber)
                {
                    TextBox _pageNumberTextbox = (TextBox)e.Item.FindControl("PageNumberTextBox2");

                    _pageNumberTextbox.Text = (_pageNumber + 1).ToString();
                    _pageNumberTextbox.MaxLength = 9;
                    _pageNumberTextbox.Visible = true;

                    _pageNumberTextbox.Attributes.Add("style", "text-align: center;");
                    _pageNumberTextbox.Attributes.Add("OnKeyDown", "return NumericWithEnter();");
                }
                else
                {
                    LinkButton _pageNumberLinkButton = (LinkButton)e.Item.FindControl("PageNumberLinkButton2");
                    _pageNumberLinkButton.CommandName = "DataPager2";
                    _pageNumberLinkButton.CommandArgument = _pageNumber.ToString();

                    if (_pageNumber == this._navMark2[0])
                    {
                        _pageNumberLinkButton.Text = "First";
                        this._navMark2[0] = null;
                    }
                    else if (_pageNumber == this._navMark2[1])
                    {
                        _pageNumberLinkButton.Text = "Prev";
                        this._navMark2[1] = null;
                    }
                    else if (_pageNumber == this._navMark2[2] && this._flag2 == false)
                    {
                        _pageNumberLinkButton.Text = "Next";
                        this._navMark2[2] = null;
                        this._nextFlag2 = true;
                        this._flag2 = true;
                    }
                    else if (_pageNumber == this._navMark2[3] && this._flag2 == true && this._nextFlag2 == true)
                    {
                        _pageNumberLinkButton.Text = "Last";
                        this._navMark2[3] = null;
                        this._lastFlag2 = true;
                    }
                    else
                    {
                        if (this._lastFlag2 == false)
                        {
                            _pageNumberLinkButton.Text = (_pageNumber + 1).ToString();
                        }

                        if (_pageNumber == this._navMark2[2] && this._flag2 == true)
                            this._flag2 = false;
                    }
                }
            }
        }
        protected void DataPagerButton2_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerTopRepeater2.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCountContactList())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCountContactList().ToString();
                        _reqPage = Convert.ToInt32(this.RowCountContactList()) - 1;
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

            this.ViewState[this._currPageKey2] = _reqPage;

            this.ShowContactList(_reqPage);
            this.SetCheckBox();
        }

        protected void SetCheckBox()
        {
            this.AllCheckBox2.Checked = true;
            foreach (ListItem _item in this.ContactCheckBoxList.Items)
            {
                _item.Selected = this.TempHidden2.Value.Contains(_item.Value);
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.TempHidden2.ClientID + ", this, " + _item.Value + ", " + this.AllCheckBox2.ClientID + " , " + this.CheckHidden2.ClientID + ")");
                if (!this.TempHidden2.Value.Contains(_item.Value)) this.AllCheckBox2.Checked = false;
            }
        }

        protected void GoImageButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ViewState[this._currPageKey2] = 0;

            this.ShowContactList(0);
            this.SetCheckBox();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _error = "";
            string _temp = this.TempHidden2.Value;
            string[] _tempsplit = _temp.Split(',');
            string _page = "0";

            String _result = this._smsMessageBL.EditGroup(_tempsplit, this.GroupNameHiddenField.Value);//this.GroupNameTextBox.Text);

            if (_result == "")
            {
                this.ViewState[this._currPageKey] = 0;
                this.ViewState[this._currPageKey2] = 0;

                this.ClearLabel();
                this.ShowData(0);
                this.ShowContactList(0);
                this.SetCheckBox();
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._phoneGroupPage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;
            this.ViewState[this._currPageKey2] = 0;

            this.ClearLabel();
            this.ShowData(0);
            this.ShowContactList(0);
            this.SetCheckBox();
        }

        protected void EditGroupNameButton_Click(object sender, EventArgs e)
        {
            if (this.EditGroupNameButton.Text == "Edit Group Name")
            {
                this.GroupNameTextBox.ReadOnly = false;//Attributes.Add("ReadOnly", "false");
                this.GroupNameTextBox.Attributes.Add("Style", "Background-Color:#FFFFFF");
                this.EditGroupNameButton.Text = "Save Group Name";
            }
            else if (this.EditGroupNameButton.Text == "Save Group Name")
            {
                if (this.GroupNameTextBox.Text != "")
                {
                    bool _result = _smsMessageBL.EditListPhoneGroupName(_userID, _organization,  this.GroupNameHiddenField.Value, this.GroupNameTextBox.Text);

                    if (_result)
                    {
                        this.GroupNameTextBox.ReadOnly = true;
                        this.GroupNameTextBox.Attributes.Add("Style", "Background-Color:#CCCCCC");
                        this.EditGroupNameButton.Text = "Edit Group Name";
                        this.GroupNameHiddenField.Value = this.GroupNameTextBox.Text;
                        Response.Redirect(this._phoneGroupEditPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.GroupNameTextBox.Text, ApplicationConfig.EncryptionKey)));
                    }
                }
                else 
                {
                    this.WarningLabel.Text = "Please, insert Group Name";
                }
            }
        }


        protected void AllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox _temp = (CheckBox)sender;
            if (_temp.Checked)
            {
                foreach (ListItem _item in this.ContactCheckBoxList.Items)
                {
                    _item.Selected = true;
                    if (!this.TempHidden2.Value.Contains(_item.Value))
                    {
                        if (this.TempHidden2.Value == "")
                            this.TempHidden2.Value = _item.Value;
                        else
                            this.TempHidden2.Value += "," + _item.Value;
                    }
                    _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.TempHidden2.ClientID + ", this, " + _item.Value + ", " + this.AllCheckBox2.ClientID + " , " + this.CheckHidden2.ClientID + ")");
                }
            }
            else
            {
                foreach (ListItem _item in this.ContactCheckBoxList.Items)
                {
                    _item.Selected = false;
                    _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.TempHidden2.ClientID + ", this, " + _item.Value + ", " + this.AllCheckBox2.ClientID + " , " + this.CheckHidden2.ClientID + ")");
                }
                String _newSelection = "";
                String[] _splitedTempHidden = this.TempHidden.Value.Split(',');
                foreach (String _singleTempHidden in _splitedTempHidden)
                    if (!this.CheckHidden2.Value.Contains(_singleTempHidden))
                    {
                        if (_newSelection == "")
                            _newSelection = _singleTempHidden;
                        else
                            _newSelection += "," + _singleTempHidden;
                    }
                this.TempHidden2.Value = _newSelection;
            }
        }
}
}