using System;
using System.Web;
using System.Web.UI;
using System.Data.OleDb;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;
using SMSLibrary;

namespace SMS.SMSWeb.AutoReply
{
    public partial class AutoReplyAdd : AutoReplyBase
    {
        private SMSMessageBL _smsMessageBL = new SMSMessageBL();
        private AutoReplyBL _autoReply = new AutoReplyBL();
        private LoginBL _loginBL = new LoginBL();

        String _UserID;
        Int32 _organization;
        bool _fgAdmin;

        int _pageSize = Convert.ToInt32 ( ApplicationConfig.ListPageSize ) ;
        int _currPage;
        int _maxPage;

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this.CekSession();

            if (Session["UserID"] != null)
            {
                _UserID = HttpContext.Current.Session["UserID"].ToString();
                _organization = Convert.ToInt32(HttpContext.Current.Session["Organization"].ToString());
                _fgAdmin = Convert.ToBoolean(Session["FgWebAdmin"].ToString());
            }

            this._maxPage = Convert.ToInt32(Math.Ceiling(_smsMessageBL.ListPhoneBookCount(_organization.ToString(), _UserID, _fgAdmin.ToString(), this.SearchTextBox.Text ) / this._pageSize));

            if (!this.Page.IsPostBack == true)
            {
                this.DestinationTextBox.Attributes.Add("OnKeyUp", "GaBolehPlus(this);");
                this.DestinationTextBox.Attributes.Add("OnChange", "GaBolehPlus(this);");

                _currPage = 0;
                this.ContactPageHiddenField.Value = _currPage.ToString();

                this.PrefixPhoneNumber.DataSource = _smsMessageBL.GetListCountryCode();
                this.PrefixPhoneNumber.DataValueField = this.PrefixPhoneNumber.DataTextField = "CountryCode";
                this.PrefixPhoneNumber.DataBind();
                this.PrefixPhoneNumber.Items.Add(new ListItem("--", ""));

                this.SetDefaultValue();
            }
            _currPage = Convert.ToInt16(this.ContactPageHiddenField.Value);
        }

        private void ClearData()
        {
            this.WarningLabel.Text = "";
            this.SelectionDropDownList.SelectedValue = "0";
            this.DestinationTextBox.Text = "";
            this.KeyWordTextBox.Text = "";
            this.ReplyMessageTextBox.Text = "";
        }

        private List<String> generatePage() {
            List<String> _result = new List<String>();
            try
            {
                if (this._maxPage > 1)
                {
                    _result.Add("Prev");
                    for (int i = 1; i <= this._maxPage; i++)
                    {
                        _result.Add(i.ToString());
                    }
                    _result.Add("Next");
                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

        private void showPhoneBookList() {
            List<String> _listPhoneBook = _smsMessageBL.GetListPhoneBook (_organization.ToString(), _UserID, _fgAdmin.ToString(), this._currPage, this._pageSize, this.SearchTextBox.Text );
            this.checkAllPerPageValue.Value = "";
            this.repeaterPhoneBook.DataSource = _listPhoneBook;
            this.repeaterPhoneBook.DataBind();
            String[] _listAllValuePerPage = this.checkAllPerPageValue.Value.Split(',');
            this.CheckAllPhoneBookCheckBox.Checked = true;
            foreach (String _listAllValue in _listAllValuePerPage)
            {
                if (this.SelectedPhone.Value.IndexOf(_listAllValue) == -1)
                {
                    this.CheckAllPhoneBookCheckBox.Checked = false;
                    break;
                }
            }
        }

        private void showPhoneGroupList() {
            this.PhoneGroupRepeater.DataSource = _smsMessageBL.GetListPhoneGroupForCheckBoxList(_organization.ToString(), _UserID, _fgAdmin.ToString());
            this.PhoneGroupRepeater.DataBind();
        }

        private void SetDefaultValue()
        {
            this.repeaterPaging.DataSource = this.generatePage();
            this.repeaterPaging.DataBind();
            this.showPhoneBookList();
            this.showPhoneGroupList();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.KeyWordTextBox.Text != "")
            {
                if (this.ReplyMessageTextBox.Text != "")
                {
                    if (this.SelectionDropDownList.SelectedValue == "0")
                    {
                        if (this.DestinationTextBox.Text != "")
                        {                        
                            TrAutoReply _newData = new TrAutoReply();
                            _newData.OrganizationID = Convert.ToInt32(Session["Organization"]);
                            _newData.PhoneNumber = this.PrefixPhoneNumber.SelectedValue + this.DestinationTextBox.Text;
                            _newData.KeyWord = this.KeyWordTextBox.Text;
                            _newData.ReplyMessage = this.ReplyMessageTextBox.Text;
                            if (_autoReply.AddAutoReply(_newData))
                                Response.Redirect(this._homePage + "?result=Success Insert Data.");
                            else
                                this.WarningLabel.Text = "Failed to insert data.";                            
                        }
                        else this.WarningLabel.Text = "Sender Phone Number must be filled.";
                    }
                    else
                    {
                        if (this.SelectedPhone.Value != "")
                        {
                            String[] _selectedPhoneDestination = this.SelectedPhone.Value.Split(',');
                            foreach (String _item in _selectedPhoneDestination)
                            {
                                TrAutoReply _newData = new TrAutoReply();
                                _newData.OrganizationID = Convert.ToInt32(Session["Organization"]);
                                _newData.PhoneNumber = _item;
                                _newData.KeyWord = this.KeyWordTextBox.Text;
                                _newData.ReplyMessage = this.ReplyMessageTextBox.Text;
                                _autoReply.AddAutoReply(_newData);
                            }
                            Response.Redirect(this._homePage + "?result=Success Insert Data.");
                        }
                        else this.WarningLabel.Text = "At Least one phone number must be chosen.";
                    }
                }
                else this.WarningLabel.Text = "Reply message must be filled.";
            }
            else this.WarningLabel.Text = "Key Word must be filled.";
                
        }

        protected void SelectionDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList _temp = (DropDownList)sender;
            if (_temp.SelectedValue == "0")
            {
                this.PanelOneNo.Visible = true;
                this.PanelPhoneBook.Visible = this.PanelPhoneGroup.Visible = false;
                this.DestinationTextBox.Text = "";
            }
            else if (_temp.SelectedValue == "1")
            {
                this.PanelOneNo.Visible = this.PanelPhoneGroup.Visible = false;
                this.PanelPhoneBook.Visible = true;
                this.SelectedPhone.Value = "";
            }
            else 
            {
                this.PanelOneNo.Visible = this.PanelPhoneBook.Visible = false;
                this.PanelPhoneGroup.Visible = true;
                this.SelectedPhone.Value = "";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void repeaterPhoneBook_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            String _temp = (String)e.Item.DataItem;
            String[] _itemData = _temp.Split(',');
            
            if (this.checkAllPerPageValue.Value.IndexOf(_itemData[0])== -1)
            {
                if (this.checkAllPerPageValue.Value != "")
                    this.checkAllPerPageValue.Value += ",";
                this.checkAllPerPageValue.Value += _itemData[0];
            }

            CheckBox _PhoneBookCheckBox = (CheckBox)e.Item.FindControl("PhoneBookCheckBox");
            _PhoneBookCheckBox.Text = _itemData[1];
            _PhoneBookCheckBox.ToolTip = _itemData[0];
            String[] _listSelectedPhone = this.SelectedPhone.Value.Split(',');
            foreach (String _selectedPhone in _listSelectedPhone)
                if (_selectedPhone == _itemData[0]) _PhoneBookCheckBox.Checked = true;

        }
        protected void CheckAllPhoneBookCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox _sender = (CheckBox)sender ;            
            if (_sender.Checked) {
                String[] _listAllValuePerPage = this.checkAllPerPageValue.Value.Split(',');
                foreach (String _listAllValue in _listAllValuePerPage)
                {
                    if (this.SelectedPhone.Value.IndexOf(_listAllValue) == -1)
                    {
                        if (this.SelectedPhone.Value != "") this.SelectedPhone.Value += ",";
                        this.SelectedPhone.Value += _listAllValue;
                    }
                }
            } else {
                String[] _listSelectedPhone = this.SelectedPhone.Value.Split(',');
                this.SelectedPhone.Value  = "";
                foreach (String _listSelected in _listSelectedPhone)
                {
                    if (this.checkAllPerPageValue.Value.IndexOf(_listSelected) == -1) {
                        this.SelectedPhone.Value += "," + _listSelected;   
                    }
                    if (this.SelectedPhone.Value != "")
                        this.SelectedPhone.Value = this.SelectedPhone.Value.Substring(1);
                }
            }
            this.showPhoneBookList();
        }
        protected void PhoneGroupRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            String _temp = (String)e.Item.DataItem;
            String[] _dataItem = _temp.Split('|');
            CheckBox _PhoneGroupCheckBox = (CheckBox)e.Item.FindControl("PhoneGroupCheckBox");
            _PhoneGroupCheckBox.ToolTip = _dataItem[0];
            _PhoneGroupCheckBox.Text = _dataItem[1];
            _PhoneGroupCheckBox.Checked = (this.SelectedPhone.Value.IndexOf(_dataItem[0]) != -1);
        }

        protected void PhoneBookCheckBOx_CheckChanged(object sender, EventArgs e)
        {
            CheckBox _temp = (CheckBox)sender;
            if (_temp.Checked)
            {
                if (this.SelectedPhone.Value == "")
                    this.SelectedPhone.Value = _temp.ToolTip;
                else
                    this.SelectedPhone.Value += "," + _temp.ToolTip;
            }
            else
            {
                String[] _splitedSelectedPhone = this.SelectedPhone.Value.Split(',');
                this.SelectedPhone.Value = "";
                if (_splitedSelectedPhone.Length > 1)
                {
                    foreach (String _eachPhone in _splitedSelectedPhone)
                    {
                        if (_eachPhone != _temp.ToolTip)
                            this.SelectedPhone.Value += "," + _eachPhone;
                    }
                    this.SelectedPhone.Value = this.SelectedPhone.Value.Substring(1);
                }
            }
        }
        protected void PhoneGroupCheckBox_CheckChanged(object sender, EventArgs e)
        {
            CheckBox _temp = (CheckBox)sender;
            if (_temp.Checked)
            {
                if (this.SelectedPhone.Value != "") this.SelectedPhone.Value += ",";
                this.SelectedPhone.Value += _temp.ToolTip;
            }
            else 
            {
                String _tempSelected = this.SelectedPhone.Value;
                this.SelectedPhone.Value = "" ;
                if ( _tempSelected.Length > _temp.ToolTip.Length )
                {
                    if ( _tempSelected.IndexOf ( _temp.ToolTip ) > 0 )
                        this.SelectedPhone.Value = _tempSelected.Substring ( 0 , _tempSelected.IndexOf ( _temp.ToolTip ) - 1 ) ;
                    if ( _tempSelected.Length > (_temp.ToolTip.Length + this.SelectedPhone.Value.Length + 1))
                        this.SelectedPhone.Value += _tempSelected.Substring(_temp.ToolTip.Length + this.SelectedPhone.Value.Length + 1); 
                }
            }
        }

        protected void repeaterPaging_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            String _temp = (String)e.Item.DataItem;
            LinkButton _pageLink = (LinkButton)e.Item.FindControl("pageLink");
            _pageLink.Text = _temp;
            _pageLink.CommandArgument = _temp;
        }
        protected void repeaterPaging_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "Prev") {
                if (this._currPage > 0)
                    this._currPage -= 1;
            }
            else if (e.CommandArgument.ToString() == "Next")
            {
                if (this._currPage < this._maxPage - 1)
                    this._currPage += 1;
            }
            else {
                this._currPage = Convert.ToInt32(e.CommandArgument) - 1;
            }
            this.ContactPageHiddenField.Value = this._currPage.ToString();
            showPhoneBookList();
        }
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            this._currPage = 0;
            this.repeaterPaging.DataSource = this.generatePage();
            this.repeaterPaging.DataBind();
            this.showPhoneBookList();
        }

}
}