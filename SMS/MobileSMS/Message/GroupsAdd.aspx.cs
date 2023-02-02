using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using SMSLibrary;

namespace SMS.SMSWeb.Message
{
    public partial class ContactsAdd : MessageBase
    {
        private SMSMessageBL _smsMessageBL = new SMSMessageBL();
        private LoginBL _loginBL = new LoginBL();

        protected String _orgID, _userID, _fgAdmin;

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);

        private string _currPageKey = "CurrentPage";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["userID"] != null && HttpContext.Current.Session["Organization"] != null && HttpContext.Current.Session["FgWebAdmin"] != null)
            {
                _orgID = Session["Organization"].ToString();
                _userID = Session["userID"].ToString() ; 
                _fgAdmin = Session["FgAdmin"].ToString();
                if ((_orgID == "") || (_userID == "") || ( _fgAdmin == ""))
                    Response.Redirect("../Login/Login.aspx");
            }
            else Response.Redirect("../Login/Login.aspx");

            if (Session["FgAdmin"] != null)
                if (Session["FgAdmin"].ToString() == "False" && _loginBL.getPackageName(Session["Organization"].ToString(), Session["UserID"].ToString()) == "PERSONAL") Response.Redirect("../Message/Compose.aspx");

            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");
            
            if (!this.Page.IsPostBack == true)
            {
                this.SubPageTitleLiteral.Text = "Group Add";

                this.GoImageButton.ImageUrl = "../images/go.jpg";
                this.SaveButton.ImageUrl = "../images/save.jpg";
                this.ResetButton.ImageUrl = "../images/reset.jpg";
                this.CancelButton.ImageUrl = "../images/cancel.jpg";

                this.ClearData();

                this.ShowContactList(0);
                this.SetCheckBox();
            }
        }

        private void ShowContactList(Int32 _prmCurrentPage)
        {
            List<MsPhoneBook> _listMsPhoneBook = this._smsMessageBL.GetListAllContactListNotYetAssigned(this.ContactFilterDropDownList.SelectedValue, this.KeywordTextBox.Text,_orgID,_userID,_fgAdmin);
            this.AllHidden.Value = "";
            foreach (MsPhoneBook _item in _listMsPhoneBook)
            {
                if (this.AllHidden.Value == "")
                    this.AllHidden.Value = _item.id.ToString();
                else
                    this.AllHidden.Value += "," + _item.id.ToString();
            }

            this.ContactCheckBoxList.ClearSelection();
            this.ContactCheckBoxList.Items.Clear();
            this.ContactCheckBoxList.DataTextField = "Name";
            this.ContactCheckBoxList.DataValueField = "id";
            List<MsPhoneBook> _listTempMsPhoneBook = this._smsMessageBL.GetListContactListNotYetAssigned(this.ContactFilterDropDownList.SelectedValue, this.KeywordTextBox.Text, _prmCurrentPage, _maxrow,Session["Organization"].ToString(),Session["UserID"].ToString(),Session["FgAdmin"].ToString());
            foreach (MsPhoneBook _item in _listTempMsPhoneBook)
            {
                if (this.CheckHidden.Value == "")
                    this.CheckHidden.Value = _item.id.ToString();
                else
                    this.CheckHidden.Value += "," + _item.id.ToString();
            }
            this.ContactCheckBoxList.DataSource = _listTempMsPhoneBook;
            this.ContactCheckBoxList.DataBind();

            this.ShowPage(_prmCurrentPage);
        }

        protected void SetCheckBox()
        {
            this.AllCheckBox.Checked = true;
            foreach (ListItem _item in this.ContactCheckBoxList.Items)
            {
                _item.Selected = this.TempHidden.Value.Contains(_item.Value);
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.TempHidden.ClientID + ", this, " + _item.Value + ", " + this.AllCheckBox.ClientID + " , " + this.CheckHidden.ClientID + ")");
                if (!this.TempHidden.Value.Contains(_item.Value)) this.AllCheckBox.Checked = false;
            }
        }

        private double RowCountContactList()
        {
            double _result = 0;
            _result = this._smsMessageBL.RowsCountContactListNotYetAssigned(this.ContactFilterDropDownList.SelectedValue, this.KeywordTextBox.Text,_orgID,_userID,_fgAdmin );
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
            double q = this.RowCountContactList();

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

                this.ShowContactList(Convert.ToInt32(e.CommandArgument));
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

            this.ViewState[this._currPageKey] = _reqPage;

            this.ShowContactList(_reqPage);
            this.SetCheckBox();
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            this.ClearLabel();

            this.GroupNameTextBox.Text = "";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            String[] _contactList;

            if (this.GrabAllCheckBox.Checked == true)
            {
                _contactList = this.AllHidden.Value.Split(',');
            }
            else
            {
                _contactList = this.TempHidden.Value.Split(',');
            }

            String _result = this._smsMessageBL.AddGroup(_contactList, this.GroupNameTextBox.Text);

            if (_result == "")
            {
                Response.Redirect(this._phoneGroupPage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Add Data, " + _result;
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_phoneGroupPage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        protected void GoImageButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.ShowContactList(0);
            this.SetCheckBox();
        }

    protected void  AllCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox _temp = (CheckBox)sender;        
        if (_temp.Checked)
        {
            foreach (ListItem _item in this.ContactCheckBoxList.Items)
            {
                _item.Selected = true;
                if (!this.TempHidden.Value.Contains(_item.Value)) { 
                    if ( this.TempHidden.Value == "")
                        this.TempHidden.Value = _item.Value;
                    else 
                        this.TempHidden.Value += "," + _item.Value ;
                }
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.TempHidden.ClientID + ", this, " + _item.Value + ", " + this.AllCheckBox.ClientID + " , " + this.CheckHidden.ClientID + ")");
            }
        }
        else {            
            foreach (ListItem _item in this.ContactCheckBoxList.Items) {
                _item.Selected = false;
                _item.Attributes.Add("OnClick", "CheckBox_Click(" + this.TempHidden.ClientID + ", this, " + _item.Value + ", " + this.AllCheckBox.ClientID + " , " + this.CheckHidden.ClientID + ")");
            } 
            String _newSelection = "";
            String[] _splitedTempHidden = this.TempHidden.Value.Split(',');
            foreach ( String _singleTempHidden in _splitedTempHidden )
                if (!this.CheckHidden.Value.Contains(_singleTempHidden))
                {
                    if (_newSelection == "")
                        _newSelection = _singleTempHidden;
                    else
                        _newSelection += "," + _singleTempHidden;
                }
            this.TempHidden.Value = _newSelection;
        }
    }
}
}