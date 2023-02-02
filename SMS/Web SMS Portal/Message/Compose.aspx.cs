using System;
using System.Web;
using System.Web.UI;
using System.Data.OleDb;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;
using SMSLibrary;
using System.Net;
using System.Collections.Specialized;
using System.Text;

namespace SMS.SMSWeb.Message
{
    public partial class Compose : MessageBase
    {
        private SMSMessageBL _smsMessageBL = new SMSMessageBL();
        private LoginBL _loginBL = new LoginBL();

        String _UserID;
        Int32 _organization;
        bool _fgAdmin;

        int _pageSize = Convert.ToInt32(ApplicationConfig.ListPageSize);
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

            this.PanelMasking.Visible = Convert.ToBoolean(_fgAdmin);
            if (_loginBL.getPackageName(_organization.ToString(), _UserID) == "CORPORATE") this.PanelMasking.Visible = true;

            this._maxPage = Convert.ToInt32(Math.Ceiling(_smsMessageBL.ListPhoneBookCount(_organization.ToString(), _UserID, _fgAdmin.ToString(), this.SearchTextBox.Text) / this._pageSize));

            if (!this.Page.IsPostBack == true)
            {
                if (this._nvcExtractor.GetValue(this._codeKey) != "")
                    this.CodeHiddenField.Value = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
                else
                    this.CodeHiddenField.Value = "";

                if (this._nvcExtractor.GetValue("contact") != "")
                    this.ContactHiddenField.Value = Rijndael.Decrypt(this._nvcExtractor.GetValue("contact"), ApplicationConfig.EncryptionKey);

                String _textCounter = "150";
                if (_fgAdmin) _textCounter = "160";

                CounterTextBox.Style.Add("background-color", "#CCC");
                CounterTextBox.Attributes.Add("ReadOnly", "true");
                CounterTextBox.Attributes.Add("value", _textCounter);
                //CounterTextBox.Attributes.Add("value", "150");

                CountSMSTextBox.Style.Add("background-color", "#CCC");
                CountSMSTextBox.Attributes.Add("ReadOnly", "true");
                CountSMSTextBox.Attributes.Add("value", "1");

                MessageTextBox.Attributes.Add("OnKeyDown", "textCounter(" + this.CounterTextBox.ClientID + ",this," + _textCounter + "," + this.CountSMSTextBox.ClientID + ");");
                MessageTextBox.Attributes.Add("OnChange", "textCounter(" + this.CounterTextBox.ClientID + ",this," + _textCounter + "," + this.CountSMSTextBox.ClientID + ");");

                this.DestinationTextBox.Attributes.Add("OnKeyUp", "GaBolehPlus(this);");
                this.DestinationTextBox.Attributes.Add("OnChange", "GaBolehPlus(this);");

                _smsMessageBL.ResetLimitSMS(_organization, _UserID);
                _currPage = 0;
                this.ContactPageHiddenField.Value = _currPage.ToString();

                this.SendButton.ImageUrl = "../images/send.jpg";
                this.CancelButton.ImageUrl = "../images/cancel.jpg";

                if (_fgAdmin)
                    this.UploadButton.ImageUrl = "../images/UploadXLS.jpg";
                else
                    this.UploadButton.Visible = false;

                if (_loginBL.getPackageName(_organization.ToString(), _UserID) == "PERSONAL" || _loginBL.getPackageName(_organization.ToString(), _UserID) == "PROFESSIONAL")
                {
                    this.PrefixPhoneNumber.Items.Add(new ListItem("+62", "+62"));
                }
                else
                {
                    this.PrefixPhoneNumber.DataSource = _smsMessageBL.GetListCountryCode();
                    this.PrefixPhoneNumber.DataValueField = this.PrefixPhoneNumber.DataTextField = "CountryCode";
                    this.PrefixPhoneNumber.DataBind();
                }
                this.PrefixPhoneNumber.Items.Add(new ListItem("--", ""));

                this.SetDefaultValue();
            }
            _currPage = Convert.ToInt16(this.ContactPageHiddenField.Value);
        }

        private void ClearData()
        {
            this.WarningLabel.Text = "";
            this.CategoryTextBox.Text = "";
            this.SelectionDropDownList.SelectedValue = "0";
            this.DestinationTextBox.Text = "";
            this.MessageTextBox.Text = "";
        }

        private List<String> generatePage()
        {
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

        private void showPhoneBookList()
        {
            List<String> _listPhoneBook = _smsMessageBL.GetListPhoneBook(_organization.ToString(), _UserID, _fgAdmin.ToString(), this._currPage, this._pageSize, this.SearchTextBox.Text);
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

        private void showPhoneGroupList()
        {
            this.PhoneGroupRepeater.DataSource = _smsMessageBL.GetListPhoneGroupForCheckBoxList(_organization.ToString(), _UserID, _fgAdmin.ToString());
            this.PhoneGroupRepeater.DataBind();
        }

        private void SetDefaultValue()
        {
            if (this.ContactHiddenField.Value != "")
            {
                this.SelectionDropDownList.SelectedValue = "1";
                this.SelectedPhone.Value = _smsMessageBL.ConvertPhoneBookContactIDArrayToPhoneNumberArray(this.ContactHiddenField.Value);
                this.PanelOneNo.Visible = false;
                this.PanelPhoneBook.Visible = true;
            }

            this.repeaterPaging.DataSource = this.generatePage();
            this.repeaterPaging.DataBind();
            this.showPhoneBookList();
            this.showPhoneGroupList();

            if (this.CodeHiddenField.Value != "")
            {
                this.panelReply.Visible = true;
                SMSGatewayReceive _smsRcv = this._smsMessageBL.GetSingleSMSGatewayReceive(this.CodeHiddenField.Value);
                this.CategoryTextBox.Text = _smsRcv.Category;

                String _preFix = _smsRcv.SenderPhoneNo.Substring(0, 2);
                if (_preFix == "+1" || _preFix == "+7")
                {
                    this.PrefixPhoneNumber.SelectedValue = _smsRcv.SenderPhoneNo.Substring(0, 2);
                    this.DestinationTextBox.Text = _smsRcv.SenderPhoneNo.Substring(2);
                }
                else
                {
                    this.PrefixPhoneNumber.SelectedValue = _smsRcv.SenderPhoneNo.Substring(0, 3);
                    this.DestinationTextBox.Text = _smsRcv.SenderPhoneNo.Substring(3);
                }
                this.OriginalMessageLabel.Text = _smsRcv.Message;
            }
            else
                this.panelReply.Visible = false;

        }

        protected void SendButton_Click(object sender, ImageClickEventArgs e)
        {
            Boolean _result = false;
            Int32 _smsLimit = _smsMessageBL.GetSMSLimit(_UserID, _organization);
            DateTime _lastLimitReset = _smsMessageBL.GetLastLimitReset(_UserID, _organization);

            String _messageSMSWhole = this.MessageTextBox.Text;
            String _footerMessage = _smsMessageBL.GetFooterAdditionalMessage(_organization);
            List<String> _messageSMSPieces = new List<String>();
            int _maxMessageSize = 158 - _footerMessage.Length;

            while (_messageSMSWhole.Length > _maxMessageSize)
            {
                _messageSMSPieces.Add(_messageSMSWhole.Substring(0, _maxMessageSize) + _footerMessage);
                _messageSMSWhole = _messageSMSWhole.Substring(_maxMessageSize);
            }
            _messageSMSPieces.Add(_messageSMSWhole + " " + _footerMessage);

            if (this.SelectionDropDownList.SelectedValue == "0")
            {
                if (_smsLimit >= _messageSMSPieces.Count)
                {
                    if (this.MaskingCheckBox.Checked)
                        if (!_smsMessageBL.decreaseMaskingBalance(_organization, _messageSMSPieces.Count))
                        {
                            this.WarningLabel.Text = "Your Masking Balance is not enough.";
                            return;
                        }
                    foreach (String _messageSMS in _messageSMSPieces)
                    {
                        SMSGatewaySend _smsGatewaySend = new SMSGatewaySend();
                        _smsGatewaySend.id = this._smsMessageBL.GetMaxNoSend();
                        _smsGatewaySend.Category = this.CategoryTextBox.Text;
                        _smsGatewaySend.DestinationPhoneNo = this.PrefixPhoneNumber.SelectedValue + this.DestinationTextBox.Text;
                        if (!(this.PrefixPhoneNumber.SelectedValue == "+62" || this.PrefixPhoneNumber.SelectedValue == "") && (_loginBL.getPackageName(_organization.ToString(), _UserID) == "PERSONAL" || _loginBL.getPackageName(_organization.ToString(), _UserID) == "PROFESSIONAL"))
                        {
                            this.WarningLabel.Text = "For Personal and Professional package, Destination Number Only to Indonesia Area Code (+62)";
                            return;
                        }
                        _smsGatewaySend.Message = _messageSMS;
                        if (this.MaskingCheckBox.Checked)
                        {
                            _smsGatewaySend.flagSend = 1;
                        }
                        else
                        {
                            _smsGatewaySend.flagSend = 0;
                        }
                        _smsGatewaySend.userID = _UserID;
                        _smsGatewaySend.DateSent = DateTime.Now;
                        _smsGatewaySend.OrganizationID = _organization;
                        //_smsGatewaySend.fgMasking = false;
                        _smsGatewaySend.fgMasking = this.MaskingCheckBox.Checked;

                        _result = this._smsMessageBL.AddSMSGatewaySend(_smsGatewaySend, this.CodeHiddenField.Value);
                        this._smsMessageBL.DecreaseSMSLimit(_organization.ToString(), _UserID, _messageSMSPieces.Count);

                        if (this.MaskingCheckBox.Checked)
                        {
                            this.SendMaskMessege(_smsGatewaySend);
                        }
                    }
                }
                else
                {
                    this.WarningLabel.Text = "Sorry, You have spent your SMS Limit for today.";
                }
            }
            else
            {
                List<SMSGatewaySend> _listSMSGatewaySend = new List<SMSGatewaySend>();
                String[] _selectedPhoneDestination = this.SelectedPhone.Value.Split(',');

                if (this.MaskingCheckBox.Checked)
                    if (!_smsMessageBL.decreaseMaskingBalance(_organization, _selectedPhoneDestination.Length * _messageSMSPieces.Count))
                    {
                        this.WarningLabel.Text = "Your Masking Balance is not enough";
                        return;
                    }

                if (_smsLimit >= _selectedPhoneDestination.Length * _messageSMSPieces.Count)
                {
                    foreach (String _item in _selectedPhoneDestination)
                    {
                        foreach (String _messageSMS in _messageSMSPieces)
                        {
                            SMSGatewaySend _smsGatewaySend = new SMSGatewaySend();
                            _smsGatewaySend.Category = this.CategoryTextBox.Text;
                            _smsGatewaySend.DestinationPhoneNo = _item;
                            if (_item.Substring(0, 3) != "+62" && (_loginBL.getPackageName(_organization.ToString(), _UserID) == "PERSONAL" || _loginBL.getPackageName(_organization.ToString(), _UserID) == "PROFESSIONAL"))
                            {
                                this.WarningLabel.Text = "For Personal and Professional package, Destination Number Only to Indonesia Area Code (+62)";
                                return;
                            }
                            _smsGatewaySend.Message = _messageSMS;
                            if (this.MaskingCheckBox.Checked)
                            {
                                _smsGatewaySend.flagSend = 1;
                            }
                            else
                            {
                                _smsGatewaySend.flagSend = 0;
                            }
                            _smsGatewaySend.userID = _UserID;
                            _smsGatewaySend.DateSent = DateTime.Now;
                            _smsGatewaySend.OrganizationID = _organization;
                            //_smsGatewaySend.fgMasking = false;
                            //if (Convert.ToBoolean(_fgAdmin))
                            _smsGatewaySend.fgMasking = this.MaskingCheckBox.Checked;

                            _listSMSGatewaySend.Add(_smsGatewaySend);
                        }
                    }

                    _result = this._smsMessageBL.AddSMSGatewaySend(_listSMSGatewaySend);
                    this._smsMessageBL.DecreaseSMSLimit(_organization.ToString(), _UserID, _selectedPhoneDestination.Length * _messageSMSPieces.Count);

                    if (this.MaskingCheckBox.Checked)
                    {
                        foreach (var _smsGatewaySend in _listSMSGatewaySend)
                        {
                            this.SendMaskMessege(_smsGatewaySend);    
                        }
                    }
                }
                else
                {
                    this.WarningLabel.Text = "Sorry, You have spent your SMS Limit for today.";
                }
            }

            if (_result)
            {
                Response.Redirect(this._outboxPage);
            }
            else
                this.WarningLabel.Text = "Sending Failed";
        }

        private void SendMaskMessege(SMSGatewaySend _smsGatewaySend)
        {
            dbConnector _dbConn = new dbConnector();
            //_dbConn.OpenConnection(ApplicationConfig.SMSPortalConnectionString);
            GlobalVar.ConnString = ApplicationConfig.SMSPortalConnectionString;

            //String MessageToSend = _dbConn.snatchQuery("id,DestinationPhoneNo,Message,OrganizationID,fgMasking", "SMSGatewaySend", "flagSend = 0 AND OrganizationID=" + GlobalVar.OrgID);
            //String[] MessageFields = MessageToSend.Split('♀');//♀ = alt +12
            
            WebClient myWebClient = new WebClient();
            NameValueCollection _maskingNVC = new NameValueCollection();
            String _maskingCID = _dbConn.snatchQuery("value", "MsBackEndSetting", "Description='MaskingCID'");
            String _maskingPWD = _dbConn.snatchQuery("value", "MsBackEndSetting", "Description='MaskingPWD'");
            String _maskingSD = _dbConn.snatchQuery("MaskingSD", "MsOrganization", "OrganizationID=" + _organization);

            if (_smsGatewaySend.DestinationPhoneNo.Contains("+")) _smsGatewaySend.DestinationPhoneNo = _smsGatewaySend.DestinationPhoneNo.Remove(_smsGatewaySend.DestinationPhoneNo.IndexOf('+'), 1);
            _maskingNVC.Add("cid", _maskingCID);
            _maskingNVC.Add("pwd", _maskingPWD);
            _maskingNVC.Add("sd", _maskingSD);
            _maskingNVC.Add("dn", _smsGatewaySend.DestinationPhoneNo);
            _maskingNVC.Add("msg", _smsGatewaySend.Message);

            String _maskingURL = _dbConn.snatchQuery("value", "MsBackEndSetting", "Description='MaskingURL'");

            byte[] responseArray = myWebClient.UploadValues(_maskingURL, "POST", _maskingNVC);
            String _MaskingResponse = Encoding.ASCII.GetString(responseArray);

            if (_MaskingResponse.Contains("Success"))
                _dbConn.executeNonQuery("UPDATE SMSGatewaySend SET flagSend = 1, MaskingStatus='" + _MaskingResponse + "' WHERE id=" + _smsGatewaySend.id.ToString());
            else
                _dbConn.executeNonQuery("UPDATE SMSGatewaySend SET flagSend = 2, MaskingStatus='" + _MaskingResponse + "' WHERE id=" + _smsGatewaySend.id.ToString());
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
            Response.Redirect(this._inboxPage);
        }

        protected void repeaterPhoneBook_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            String _temp = (String)e.Item.DataItem;
            String[] _itemData = _temp.Split(',');

            if (this.checkAllPerPageValue.Value.IndexOf(_itemData[0]) == -1)
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
            CheckBox _sender = (CheckBox)sender;
            if (_sender.Checked)
            {
                String[] _listAllValuePerPage = this.checkAllPerPageValue.Value.Split(',');
                foreach (String _listAllValue in _listAllValuePerPage)
                {
                    if (this.SelectedPhone.Value.IndexOf(_listAllValue) == -1)
                    {
                        if (this.SelectedPhone.Value != "") this.SelectedPhone.Value += ",";
                        this.SelectedPhone.Value += _listAllValue;
                    }
                }
            }
            else
            {
                String[] _listSelectedPhone = this.SelectedPhone.Value.Split(',');
                this.SelectedPhone.Value = "";
                foreach (String _listSelected in _listSelectedPhone)
                {
                    if (this.checkAllPerPageValue.Value.IndexOf(_listSelected) == -1)
                    {
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
                this.SelectedPhone.Value = "";
                if (_tempSelected.Length > _temp.ToolTip.Length)
                {
                    if (_tempSelected.IndexOf(_temp.ToolTip) > 0)
                        this.SelectedPhone.Value = _tempSelected.Substring(0, _tempSelected.IndexOf(_temp.ToolTip) - 1);
                    if (_tempSelected.Length > (_temp.ToolTip.Length + this.SelectedPhone.Value.Length + 1))
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
            if (e.CommandArgument.ToString() == "Prev")
            {
                if (this._currPage > 0)
                    this._currPage -= 1;
            }
            else if (e.CommandArgument.ToString() == "Next")
            {
                if (this._currPage < this._maxPage - 1)
                    this._currPage += 1;
            }
            else
            {
                this._currPage = Convert.ToInt32(e.CommandArgument) - 1;
            }
            this.ContactPageHiddenField.Value = this._currPage.ToString();
            showPhoneBookList();
        }
        protected void UploadButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._composeUploadPage);
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