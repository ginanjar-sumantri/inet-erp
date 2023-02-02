using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessRule.POSInterface;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.Threading;

namespace POS.POSInterface.Internet
{
    public partial class POSInternetReservation : POSInternetBase
    {
        protected POSInternetBL _posInternetBL = new POSInternetBL();
        protected POSTrInternetBookingBL _posInetBookingBL = new POSTrInternetBookingBL();
        protected POSInternetTableBL _posInternetTableBL = new POSInternetTableBL();
        protected MemberBL _memberBL = new MemberBL();
        protected InternetFloorBL _internetFloorBL = new InternetFloorBL();
        protected POSTableStatusHistoryBL _statusHistoryBL = new POSTableStatusHistoryBL();
        protected POSConfigurationBL _pOSConfigurationBL = new POSConfigurationBL();
        DateTime _now = DateTime.Now;

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);
            if (!Page.IsPostBack)
            {
                for (int i = 0; i < 24; i++)
                {
                    this.TimeStartHourDropDownList.Items.Add(new ListItem(i.ToString()));
                    this.TimeEndHourDropDownList.Items.Add(new ListItem(i.ToString()));
                }
                this.TimeStartHourDropDownList.SelectedValue = Convert.ToString(_now.Hour + 1);
                this.TimeEndHourDropDownList.SelectedValue = Convert.ToString(_now.Hour + 2);
                for (int i = 0; i < 60; i++)
                {
                    this.TimeStartMinuteDropDownList.Items.Add(new ListItem(i.ToString()));
                    this.TimeEndMinuteDropDownList.Items.Add(new ListItem(i.ToString()));
                }
                this.BookingDateTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.BookingDateTextBox.Attributes.Add("ReadOnly", "true");
                this.MemberNoTextBox.Attributes.Add("ReadOnly", "true");
                this.CancelAllImageButton.OnClientClick = "return confirm('Are you sure want to cancel all booked table on this time range?');";

                this.SearchMemberButton.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findMember&configCode=member','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1')";
                this.ReferensiNoTextBox.Attributes["onclick"] = "ReferenceKeyBoard(this.id)";
                this.MemberNoTextBox.Attributes["onclick"] = "MemberNoKeyBoard(this.id)";
                this.MemberNameTextBox.Attributes["onclick"] = "MemberNameKeyBoard(this.id)";
                this.AddressTextBox.Attributes["onclick"] = "AddressKeyBoard(this.id)";
                this.PhoneNumber1TextBox.Attributes["onclick"] = "PhoneNumber1KeyBoard(this.id)";
                this.PhoneNumber2TextBox.Attributes["onclick"] = "PhoneNumber2KeyBoard(this.id)";
                this.PasswordTextBox.Attributes["onclick"] = "PasswordKeyBoard(this.id)";

                String spawnJS = "<script language='JavaScript'>\n";

                ////////////////////DECLARE FUNCTION FOR CATCHING SUPPLIER SEARCH
                spawnJS += "function findMember(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.MemberNoTextBox.ClientID + "').value = dataArray[0];\n";
                spawnJS += "document.getElementById('" + this.MemberNameTextBox.ClientID + "').value = dataArray[1];\n";
                spawnJS += "document.getElementById('" + this.AddressTextBox.ClientID + "').value = dataArray[2];\n";
                spawnJS += "document.getElementById('" + this.PhoneNumber1TextBox.ClientID + "').value = dataArray[3];\n";
                spawnJS += "document.getElementById('" + this.PhoneNumber2TextBox.ClientID + "').value = dataArray[4];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR Calling KeyBoard Reference
                spawnJS += "function ReferenceKeyBoard(x) {\n";
                spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findReference&titleinput=Reference&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR CATCHING ON Reference
                spawnJS += "function findReference(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.ReferensiNoTextBox.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR Calling KeyBoard MemberNo
                spawnJS += "function MemberNoKeyBoard(x) {\n";
                spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findMemberNo&titleinput=Member No&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR CATCHING ON MemberNo
                spawnJS += "function findMemberNo(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.MemberNoTextBox.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR Calling KeyBoard MemberName
                spawnJS += "function MemberNameKeyBoard(x) {\n";
                spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findMemberName&titleinput=Name&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR CATCHING ON MemberName
                spawnJS += "function findMemberName(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.MemberNameTextBox.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR Calling KeyBoard Address
                spawnJS += "function AddressKeyBoard(x) {\n";
                spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findAddress&titleinput=Address&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR CATCHING ON Address
                spawnJS += "function findAddress(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.AddressTextBox.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR Calling KeyBoard PhoneNumber1
                spawnJS += "function PhoneNumber1KeyBoard(x) {\n";
                spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findPhoneNumber1&titleinput=Phone&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR CATCHING ON PhoneNumber1
                spawnJS += "function findPhoneNumber1(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.PhoneNumber1TextBox.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR Calling KeyBoard PhoneNumber2
                spawnJS += "function PhoneNumber2KeyBoard(x) {\n";
                spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findPhoneNumber2&titleinput=Phone&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR CATCHING ON PhoneNumber2
                spawnJS += "function findPhoneNumber2(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.PhoneNumber2TextBox.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR Calling KeyBoard Password
                spawnJS += "function PasswordKeyBoard(x) {\n";
                spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findPassword&titleinput=Password&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR CATCHING ON Password
                spawnJS += "function findPassword(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.PasswordTextBox.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";
                spawnJS += "</script>\n";
                this.javascriptReceiver.Text = spawnJS;

                this.ShowFloor();
                this.ShowReservationNo();
                //this.ShowDataBookingList();
                this.SetAttribute();
                this.SearchImageButton_Click(null, null);

                var _code = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
                if (_code != "")
                {
                    this.ShowData();
                }
                this.ChangeVisiblePanel(0);
            }
            ClearLabel();
        }

        protected void SetAttribute()
        {
            this.PhoneNumber1TextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.PhoneNumber1TextBox.ClientID + "," + this.PhoneNumber1TextBox.ClientID + ",500" + ");");
            this.PhoneNumber2TextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.PhoneNumber2TextBox.ClientID + "," + this.PhoneNumber2TextBox.ClientID + ",500" + ")");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowFloor()
        {
            try
            {
                this.FloorDropDownList.DataSource = this._posInetBookingBL.GetListInternetFloor();
                this.FloorDropDownList.DataTextField = "FloorName";
                this.FloorDropDownList.DataValueField = "FloorNmbr";
                this.FloorDropDownList.DataBind();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        private void ShowReservationNo()
        {
            if (this.ReservationNoLabel.Text != "")
            {
                this.ReservationNoPanel.Visible = true;
            }
            else
            {
                this.ReservationNoPanel.Visible = false;
            }
        }

        public void ShowData()
        {
            try
            {
                POSTrInternetBookingHd _msInternetBookingHd = this._posInetBookingBL.GetSingleBookingHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
                POSTrInternetBookingDt _msInternetBookingDt = this._posInetBookingBL.GetSingleBookingDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                this.ReservationNoLabel.Text = _msInternetBookingHd.InternetBookCode;
                if (_msInternetBookingHd.BookingType == "Reservation")
                {
                    this.ReservationRadioButton.Checked = true;
                }
                else
                {
                    this.BookEventRadioButton.Checked = true;
                }

                this.ReferensiNoTextBox.Text = _msInternetBookingHd.ReferenceNo;
                this.MemberNoTextBox.Text = _msInternetBookingHd.MemberCode;
                if (this.MemberNoTextBox.Text != "")
                {
                    this.MemberNameTextBox.Text = _memberBL.GetSingle(_msInternetBookingHd.MemberCode).MemberName;
                }
                else
                {
                    this.MemberNameTextBox.Text = _msInternetBookingHd.CustName;
                }
                this.AddressTextBox.Text = _msInternetBookingHd.CustAddress;
                this.PhoneNumber1TextBox.Text = _msInternetBookingHd.PhoneNumber1;
                this.PhoneNumber2TextBox.Text = _msInternetBookingHd.PhoneNumber2;
                this.BookingDateTextBox.Text = ((Convert.ToDateTime(_msInternetBookingDt.BookingDate).Year) + "-" + (Convert.ToDateTime(_msInternetBookingDt.BookingDate).Month) + "-" + (Convert.ToDateTime(_msInternetBookingDt.BookingDate).Day)).ToString();
                this.TimeStartHourDropDownList.SelectedValue = Convert.ToString(_msInternetBookingDt.StartTimeHour);
                this.TimeStartMinuteDropDownList.SelectedValue = Convert.ToString(_msInternetBookingDt.StartTimeMinute);
                this.TimeEndHourDropDownList.SelectedValue = Convert.ToString(_msInternetBookingDt.EndTimeHour);
                this.TimeEndMinuteDropDownList.SelectedValue = Convert.ToString(_msInternetBookingDt.EndTimeMinute);
                this.FloorDropDownList.SelectedValue = Convert.ToString(_msInternetBookingDt.FloorNmbr);
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        public void ShowDataBookingList()
        {
            try
            {
                this.BookedTableRepeater.DataSource = this._posInetBookingBL.GetListBooking(this.MemberNoTextBox.Text, this.MemberNameTextBox.Text, this.PhoneNumber1TextBox.Text, true);
                this.BookedTableRepeater.DataBind();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        public void ShowAvailableTable()
        {
            try
            {
                int startTimeVal = Convert.ToInt32(this.TimeStartHourDropDownList.SelectedValue) * 60 + Convert.ToInt32(this.TimeStartMinuteDropDownList.SelectedValue);
                int endTimeVal = Convert.ToInt32(this.TimeEndHourDropDownList.SelectedValue) * 60 + Convert.ToInt32(this.TimeEndMinuteDropDownList.SelectedValue);
                if (startTimeVal < endTimeVal)
                {
                    this.AvailableTableRepeater.DataSource = this._posInetBookingBL.GetListUnReservedTable(Convert.ToDateTime(this.BookingDateTextBox.Text),
                        Convert.ToInt16(this.FloorDropDownList.SelectedValue),
                        Convert.ToInt16(this.TimeStartHourDropDownList.SelectedValue),
                        Convert.ToInt16(this.TimeStartMinuteDropDownList.SelectedValue),
                        Convert.ToInt16(this.TimeEndHourDropDownList.SelectedValue),
                        Convert.ToInt16(this.TimeEndMinuteDropDownList.SelectedValue));
                    this.AvailableTableRepeater.DataBind();
                    //this.BookedTableRepeater.DataSource = this._posInetBookingBL.GetListReservedTable(Convert.ToDateTime(this.BookingDateTextBox.Text), true,
                    //    Convert.ToInt16(this.FloorDropDownList.SelectedValue),
                    //    Convert.ToInt16(this.TimeStartHourDropDownList.SelectedValue),
                    //    Convert.ToInt16(this.TimeStartMinuteDropDownList.SelectedValue),
                    //    Convert.ToInt16(this.TimeEndHourDropDownList.SelectedValue),
                    //    Convert.ToInt16(this.TimeEndMinuteDropDownList.SelectedValue));
                    //this.BookedTableRepeater.DataBind();
                }
                else this.WarningLabel.Text = "End booking time must pass the start booking time.";
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void AvailableTableRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            POSMsInternetTable _temp = (POSMsInternetTable)e.Item.DataItem;
            CheckBox _AvailableTableCheckBox = (CheckBox)e.Item.FindControl("AvailableTableCheckBox");
            _AvailableTableCheckBox.ToolTip = _temp.TableIDPerRoom.ToString();
            _AvailableTableCheckBox.Text = _temp.TableNmbr;
        }

        protected void BookedTableRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                POSTrInternetBookingDt _temp = (POSTrInternetBookingDt)e.Item.DataItem;

                Literal _TglLiteral = (Literal)e.Item.FindControl("TglLiteral");
                _TglLiteral.Text = DateFormMapper.GetValue(_temp.BookingDate);
                Literal _BookedTableLiteral = (Literal)e.Item.FindControl("BookedTableLiteral");
                _BookedTableLiteral.Text = _internetFloorBL.GetMemberNameByCodeInternet(Convert.ToString(_temp.FloorNmbr)) + " / " + this._posInetBookingBL.GetTableNumberLabel(_temp.FloorNmbr, _temp.TableIDPerRoom);
                Literal _StartTimeLiteral = (Literal)e.Item.FindControl("StartTimeLiteral");
                _StartTimeLiteral.Text = _temp.StartTimeHour.ToString("00") + " : " + _temp.StartTimeMinute.ToString("00");
                Literal _EndTimeLiteral = (Literal)e.Item.FindControl("EndTimeLiteral");
                _EndTimeLiteral.Text = _temp.EndTimeHour.ToString("00") + " : " + _temp.EndTimeMinute.ToString("00");
                ImageButton _CancelBookedButton = (ImageButton)e.Item.FindControl("CancelBookedButton");
                _CancelBookedButton.ToolTip = _temp.HistoryID.ToString();
                _CancelBookedButton.Attributes.Add("OnClick", "return CancelBooked();");
                _CancelBookedButton.CommandName = "CancelBooked";
                _CancelBookedButton.CommandArgument = _temp.HistoryID.ToString() + "-" + _temp.InternetBookCode.ToString() + "-" + _temp.ItemNo.ToString();

                ImageButton _OpenBookedButton = (ImageButton)e.Item.FindControl("OpenBookedButton");
                _OpenBookedButton.ToolTip = _temp.HistoryID.ToString();
                _OpenBookedButton.Attributes.Add("Onclick", "return OpenBooked();");
                _OpenBookedButton.CommandName = "OpenBooked";
                _OpenBookedButton.CommandArgument = _temp.HistoryID.ToString() + "-" + _temp.InternetBookCode.ToString() + "-" + _temp.ItemNo.ToString();
                //if (this.ReservationNoLabel.Text != "")
                //{
                //    _OpenBookedButton.Visible = true;
                //}
                //else
                //{
                //    _OpenBookedButton.Visible = false;
                //}
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void BookedTableRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "CancelBooked")
                {
                    this.ChangeVisiblePanel(1);
                    this.CancelledTableHiddenField.Value = e.CommandArgument.ToString();

                    //string _code = e.CommandArgument.ToString();

                    //double _maxIDHistory = this._statusHistoryBL.GetNewID();

                    //POSTrInternetBookingDt _StatusInternetBooking = this._posInetBookingBL.GetSingleForStatus(_code);
                    //_StatusInternetBooking.FgActive = false;
                    //_StatusInternetBooking.HistoryID = Convert.ToInt32(_maxIDHistory) + 1;
                    //this._posInetBookingBL.Edit(_StatusInternetBooking);

                    //POSTableStatusHistory _statusHistory = this._statusHistoryBL.GetSingleForInternet(_code);
                    //_statusHistory.StillActive = false;
                    //_statusHistory.Status = 3;
                    //this._statusHistoryBL.Edit(_statusHistory);

                    //POSTableStatusHistory _newHistoryBookingData = new POSTableStatusHistory();

                    //_newHistoryBookingData.ID = Convert.ToInt32(_maxIDHistory) + 1;
                    //_newHistoryBookingData.FloorNmbr = _statusHistory.FloorNmbr;
                    //_newHistoryBookingData.FloorType = _statusHistory.FloorType;
                    //_newHistoryBookingData.TableID = _statusHistory.TableID;
                    //_newHistoryBookingData.StartTime = _statusHistory.StartTime;
                    //_newHistoryBookingData.EndTime = _statusHistory.EndTime;
                    //_newHistoryBookingData.Status = 0;
                    //_newHistoryBookingData.StillActive = true;

                    //this._statusHistoryBL.AddTrBookingHistory(_newHistoryBookingData);

                    //int startTimeVal = Convert.ToInt32(this.TimeStartHourDropDownList.SelectedValue) * 60 + Convert.ToInt32(this.TimeStartMinuteDropDownList.SelectedValue);
                    //int endTimeVal = Convert.ToInt32(this.TimeEndHourDropDownList.SelectedValue) * 60 + Convert.ToInt32(this.TimeEndMinuteDropDownList.SelectedValue);
                    //if (startTimeVal < endTimeVal)
                    //{
                    //    this.AvailableTableRepeater.DataSource = this._posInetBookingBL.GetListUnReservedTable((Convert.ToDateTime(this.BookingDateTextBox.Text)),
                    //        Convert.ToInt16(this.FloorDropDownList.SelectedValue),
                    //        Convert.ToInt16(this.TimeStartHourDropDownList.SelectedValue),
                    //        Convert.ToInt16(this.TimeStartMinuteDropDownList.SelectedValue),
                    //        Convert.ToInt16(this.TimeEndHourDropDownList.SelectedValue),
                    //        Convert.ToInt16(this.TimeEndMinuteDropDownList.SelectedValue));
                    //    this.AvailableTableRepeater.DataBind();
                    //    //this.BookedTableRepeater.DataSource = this._posInetBookingBL.GetListReservedTable(Convert.ToDateTime(this.BookingDateTextBox.Text),true,
                    //    //    Convert.ToInt16(this.FloorDropDownList.SelectedValue),
                    //    //    Convert.ToInt16(this.TimeStartHourDropDownList.SelectedValue),
                    //    //    Convert.ToInt16(this.TimeStartMinuteDropDownList.SelectedValue),
                    //    //    Convert.ToInt16(this.TimeEndHourDropDownList.SelectedValue),
                    //    //    Convert.ToInt16(this.TimeEndMinuteDropDownList.SelectedValue));
                    //    //this.BookedTableRepeater.DataBind();
                    //}
                    //else this.WarningLabel.Text = "End booking time must pass the start booking time.";

                    ////this._posInetBookingBL.Cancel(_temp.ToolTip);
                    //if (this.ReservationNoLabel.Text == "")
                    //{
                    //    this.SearchImageButton_Click(null, null);
                    //}
                    ////this.ShowDataBookingList();
                }
                else if (e.CommandName == "OpenBooked")
                {
                    string _code = e.CommandArgument.ToString();

                    int _maxHistoryID = this._statusHistoryBL.GetNewID();

                    POSTrInternetBookingDt _StatusInternetBooking = this._posInetBookingBL.GetSingleForStatus(_code);
                    _StatusInternetBooking.FgActive = false;
                    _StatusInternetBooking.HistoryID = Convert.ToInt32(_maxHistoryID + 1);
                    this._posInetBookingBL.Edit(_StatusInternetBooking);

                    POSTableStatusHistory _statusHistory = this._statusHistoryBL.GetSingleForInternet(_code);
                    _statusHistory.StillActive = false;
                    this._statusHistoryBL.Edit(_statusHistory);

                    POSTableStatusHistory _newHistoryBookingData = new POSTableStatusHistory();

                    _newHistoryBookingData.ID = Convert.ToInt32(_maxHistoryID + 1);
                    _newHistoryBookingData.FloorNmbr = _statusHistory.FloorNmbr;
                    _newHistoryBookingData.FloorType = _statusHistory.FloorType;
                    _newHistoryBookingData.TableID = _statusHistory.TableID;
                    _newHistoryBookingData.StartTime = _statusHistory.StartTime;
                    _newHistoryBookingData.EndTime = _statusHistory.EndTime;
                    _newHistoryBookingData.Status = 0;
                    _newHistoryBookingData.StillActive = true;

                    this._statusHistoryBL.AddTrBookingHistory(_newHistoryBookingData);

                    int startTimeVal = Convert.ToInt32(this.TimeStartHourDropDownList.SelectedValue) * 60 + Convert.ToInt32(this.TimeStartMinuteDropDownList.SelectedValue);
                    int endTimeVal = Convert.ToInt32(this.TimeEndHourDropDownList.SelectedValue) * 60 + Convert.ToInt32(this.TimeEndMinuteDropDownList.SelectedValue);
                    if (startTimeVal < endTimeVal)
                    {
                        this.AvailableTableRepeater.DataSource = this._posInetBookingBL.GetListUnReservedTable((Convert.ToDateTime(this.BookingDateTextBox.Text)),
                            Convert.ToInt16(this.FloorDropDownList.SelectedValue),
                            Convert.ToInt16(this.TimeStartHourDropDownList.SelectedValue),
                            Convert.ToInt16(this.TimeStartMinuteDropDownList.SelectedValue),
                            Convert.ToInt16(this.TimeEndHourDropDownList.SelectedValue),
                            Convert.ToInt16(this.TimeEndMinuteDropDownList.SelectedValue));
                        this.AvailableTableRepeater.DataBind();
                        //this.BookedTableRepeater.DataSource = this._posInetBookingBL.GetListReservedTable(Convert.ToDateTime(this.BookingDateTextBox.Text),true,
                        //    Convert.ToInt16(this.FloorDropDownList.SelectedValue),
                        //    Convert.ToInt16(this.TimeStartHourDropDownList.SelectedValue),
                        //    Convert.ToInt16(this.TimeStartMinuteDropDownList.SelectedValue),
                        //    Convert.ToInt16(this.TimeEndHourDropDownList.SelectedValue),
                        //    Convert.ToInt16(this.TimeEndMinuteDropDownList.SelectedValue));
                        //this.BookedTableRepeater.DataBind();
                    }
                    else this.WarningLabel.Text = "End booking time must pass the start booking time.";

                    //this._posInetBookingBL.Cancel(_temp.ToolTip);
                    if (this.ReservationNoLabel.Text == "")
                    {
                        this.SearchImageButton_Click(null, null);
                    }
                    //this.ShowDataBookingList();
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void SearchImageButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (this.ReservationNoLabel.Text == "")
                {
                    DateTime _search = Convert.ToDateTime(this.BookingDateTextBox.Text);
                    DateTime _searchStartDate = new DateTime(_search.Year, _search.Month, _search.Day, Convert.ToInt32(this.TimeStartHourDropDownList.SelectedValue), Convert.ToInt32(this.TimeStartMinuteDropDownList.SelectedValue), 0);
                    DateTime _searchEndDate = new DateTime(_search.Year, _search.Month, _search.Day, Convert.ToInt32(this.TimeEndHourDropDownList.SelectedValue), Convert.ToInt32(this.TimeEndMinuteDropDownList.SelectedValue), 0);
                    this.SelectedTableHiddenField.Value = "";
                    if (this.MemberNoTextBox.Text != "")
                    {
                        this.ShowDataBookingList();
                    }
                    else
                    {
                        this.BookedTableRepeater.DataSource = this._posInetBookingBL.GetListReservedTable
                            (
                            //_searchStartDate,
                            _search,
                            true,
                            Convert.ToInt16(this.FloorDropDownList.SelectedValue),
                            Convert.ToInt16(this.TimeStartHourDropDownList.SelectedValue),
                            Convert.ToInt16(this.TimeStartMinuteDropDownList.SelectedValue),
                            Convert.ToInt16(this.TimeEndHourDropDownList.SelectedValue),
                            Convert.ToInt16(this.TimeEndMinuteDropDownList.SelectedValue)
                            );
                        this.BookedTableRepeater.DataBind();
                    }

                    this.CancelBookingAutomatic();

                    this.AvailableTableRepeater.DataSource = null;
                    this.AvailableTableRepeater.DataBind();
                    if (_searchStartDate >= _now)
                    {
                        if (_searchStartDate < _searchEndDate)
                        {
                            this.AvailableTableRepeater.DataSource = this._posInetBookingBL.GetListUnReservedTable
                                (
                                //_searchEndDate,
                                _search,
                                Convert.ToInt16(this.FloorDropDownList.SelectedValue),
                                Convert.ToInt16(this.TimeStartHourDropDownList.SelectedValue),
                                Convert.ToInt16(this.TimeStartMinuteDropDownList.SelectedValue),
                                Convert.ToInt16(this.TimeEndHourDropDownList.SelectedValue),
                                Convert.ToInt16(this.TimeEndMinuteDropDownList.SelectedValue)
                                );
                            this.AvailableTableRepeater.DataBind();
                        }
                        else this.WarningLabel.Text = "Available Table : Start and End booking time must more than current time.";
                    }
                    else this.WarningLabel.Text = "Available Table : Date to be filled with accordingly.";
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void BackImageButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //Response.Redirect(this._homePage + "?selectedFloor=");
                //String _floor = this._posInternetBL.GetRoomCode(Convert.ToInt32(this.FloorDropDownList.Text));
                //Response.Redirect("POSInternet.aspx" + "?" + this._selectedFloorKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_floor, ApplicationConfig.EncryptionKey)));
                String _floor = _posInternetBL.GetSinglePOSMsInternetFloor("Internet");
                Response.Redirect("POSInternet.aspx" + "?" + this._selectedFloorKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_floor, ApplicationConfig.EncryptionKey)));
            }
            catch (ThreadAbortException) { throw; }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void AvailableTableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox _temp = (CheckBox)sender;
            if (_temp.Checked)
            {
                if (this.SelectedTableHiddenField.Value != "") this.SelectedTableHiddenField.Value += ",";
                this.SelectedTableHiddenField.Value += _temp.ToolTip;
            }
            else
            {
                String[] _splitSelectedTable = this.SelectedTableHiddenField.Value.Split(',');
                this.SelectedTableHiddenField.Value = "";
                foreach (String _oneSelected in _splitSelectedTable)
                    if (_oneSelected != _temp.ToolTip) this.SelectedTableHiddenField.Value += "," + _oneSelected;
                if (this.SelectedTableHiddenField.Value != "")
                    this.SelectedTableHiddenField.Value = this.SelectedTableHiddenField.Value.Substring(1);
            }
        }

        protected void SaveImageButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (this.ReservationNoLabel.Text == "")
                {
                    DateTime _nowDate = new DateTime(_now.Year, _now.Month, _now.Day, Convert.ToInt32(_now.Hour), Convert.ToInt32(_now.Minute), 0);

                    DateTime _reserve = Convert.ToDateTime(this.BookingDateTextBox.Text);
                    DateTime _reserveStartDate = new DateTime(_reserve.Year, _reserve.Month, _reserve.Day, Convert.ToInt32(this.TimeStartHourDropDownList.SelectedValue), Convert.ToInt32(this.TimeStartMinuteDropDownList.SelectedValue), 0);
                    DateTime _reserveEndDate = new DateTime(_reserve.Year, _reserve.Month, _reserve.Day, Convert.ToInt32(this.TimeEndHourDropDownList.SelectedValue), Convert.ToInt32(this.TimeEndMinuteDropDownList.SelectedValue), 0);

                    //if (Convert.ToDateTime(this.BookingDateTextBox.Text) >= _now.Date)
                    if (_reserveStartDate >= _nowDate)
                    {
                        //if (Convert.ToInt32(this.TimeStartHourDropDownList.SelectedValue) <= _now.Hour)
                        //{
                        //int startTimeVal = Convert.ToInt32(this.TimeStartHourDropDownList.SelectedValue) * 60 + Convert.ToInt32(this.TimeStartMinuteDropDownList.SelectedValue);
                        //int endTimeVal = Convert.ToInt32(this.TimeEndHourDropDownList.SelectedValue) * 60 + Convert.ToInt32(this.TimeEndMinuteDropDownList.SelectedValue);
                        //if (startTimeVal < endTimeVal)
                        if (_reserveStartDate < _reserveEndDate)
                        {
                            if (this.SelectedTableHiddenField.Value != "")
                            {
                                Boolean _successEdit = true;
                                String[] _splitSelectedTable = this.SelectedTableHiddenField.Value.Split(',');
                                this.SelectedTableHiddenField.Value = "";

                                POSTrInternetBookingHd _newBookingDataForHd = new POSTrInternetBookingHd();
                                _newBookingDataForHd.BookingType = this.ReservationRadioButton.Checked ? "Reservation" : "Book Event";
                                _newBookingDataForHd.CustAddress = this.AddressTextBox.Text;
                                _newBookingDataForHd.CustName = this.MemberNameTextBox.Text;
                                _newBookingDataForHd.MemberCode = this.MemberNoTextBox.Text;
                                _newBookingDataForHd.PhoneNumber1 = this.PhoneNumber1TextBox.Text;
                                _newBookingDataForHd.PhoneNumber2 = this.PhoneNumber2TextBox.Text;
                                _newBookingDataForHd.ReferenceNo = this.ReferensiNoTextBox.Text;
                                _newBookingDataForHd.OperatorName = HttpContext.Current.User.Identity.Name;

                                List<POSTrInternetBookingDt> _posTrInternetBookingDtList = new List<POSTrInternetBookingDt>();
                                List<POSTableStatusHistory> _posTableStatusHistoryList = new List<POSTableStatusHistory>();
                                int _getMaxItemNo = 0;
                                int _maxHistoryID = this._statusHistoryBL.GetNewID();
                                int _selectSplit = 0;
                                foreach (String _selectedTable in _splitSelectedTable)
                                {
                                    if (_selectedTable == _splitSelectedTable[_selectSplit])
                                    {
                                        POSTrInternetBookingDt _newBookingDataForDt = new POSTrInternetBookingDt();

                                        _newBookingDataForDt.ItemNo = _getMaxItemNo;
                                        _getMaxItemNo++;
                                        _newBookingDataForDt.BookingDate = Convert.ToDateTime(this.BookingDateTextBox.Text);
                                        _newBookingDataForDt.EndTimeHour = Convert.ToInt16(this.TimeEndHourDropDownList.SelectedValue);
                                        _newBookingDataForDt.EndTimeMinute = Convert.ToInt16(this.TimeEndMinuteDropDownList.SelectedValue);
                                        _newBookingDataForDt.StartTimeHour = Convert.ToInt16(this.TimeStartHourDropDownList.SelectedValue);
                                        _newBookingDataForDt.StartTimeMinute = Convert.ToInt16(this.TimeStartMinuteDropDownList.SelectedValue);
                                        _newBookingDataForDt.FloorNmbr = Convert.ToInt32(this.FloorDropDownList.SelectedValue);
                                        _newBookingDataForDt.TableIDPerRoom = Convert.ToInt32(_selectedTable);
                                        _maxHistoryID++;
                                        _newBookingDataForDt.HistoryID = _maxHistoryID;
                                        //_getHistory = _newBookingDataForDt.HistoryID;
                                        _newBookingDataForDt.FgActive = true;

                                        _posTrInternetBookingDtList.Add(_newBookingDataForDt);

                                        POSTableStatusHistory _newHistoryBookingData = new POSTableStatusHistory();
                                        DateTime _date = Convert.ToDateTime(this.BookingDateTextBox.Text);

                                        _newHistoryBookingData.ID = Convert.ToInt32(_maxHistoryID);
                                        _newHistoryBookingData.FloorType = POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet);
                                        _newHistoryBookingData.FloorNmbr = Convert.ToInt32(this.FloorDropDownList.SelectedValue);
                                        _newHistoryBookingData.TableID = Convert.ToInt32(_selectedTable);
                                        _newHistoryBookingData.StartTime = new DateTime(_date.Year, _date.Month, _date.Day, Convert.ToInt32(this.TimeStartHourDropDownList.SelectedValue), Convert.ToInt32(this.TimeStartMinuteDropDownList.SelectedValue), 0);
                                        _newHistoryBookingData.EndTime = new DateTime(_date.Year, _date.Month, _date.Day, Convert.ToInt32(this.TimeEndHourDropDownList.SelectedValue), Convert.ToInt16(this.TimeEndMinuteDropDownList.SelectedValue), 0);
                                        _newHistoryBookingData.Status = 1;
                                        _newHistoryBookingData.StillActive = true;
                                        _posTableStatusHistoryList.Add(_newHistoryBookingData);
                                        _selectSplit++;
                                    }
                                }

                                bool _successInsert2 = this._posInetBookingBL.AddTrBooking(_newBookingDataForHd, _posTrInternetBookingDtList, _posTableStatusHistoryList);

                                foreach (var _item in _splitSelectedTable)
                                {
                                    if (this.FloorDropDownList.SelectedValue != "" && _item != "")
                                    {
                                        if (_successInsert2)
                                        {
                                            POSMsInternetTable _msInternetTable = _posInternetTableBL.GetSingleUpdateInternet(this.FloorDropDownList.SelectedValue, _item, POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet));
                                            _msInternetTable.Status = 2;
                                            _successEdit = _posInternetTableBL.Edit(_msInternetTable);
                                        }
                                    }
                                    if (!_successInsert2)
                                        break;
                                }

                                if (_successEdit)
                                {
                                    //Response.Redirect(this._homePage + "?" + this._selectedFloorKey + "=");
                                    //Response.Redirect("POSInternetReservation.aspx" + "?" + this._codeItemKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt("", ApplicationConfig.EncryptionKey)));
                                    this.ShowAvailableTable();
                                    this.SearchImageButton_Click(null, null);
                                    //this.ShowDataBookingList();
                                }
                                else
                                {
                                    this.WarningLabel.Text = "Booking process failed.";
                                }
                            }
                            else this.WarningLabel.Text = "Booked table must be chosen.";
                        }
                        else this.WarningLabel.Text = "End booking time must pass the start booking time.";
                    }
                    else this.WarningLabel.Text = "Date to be filled with accordingly.";
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void CancelAllImageButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this._posInetBookingBL.CancelAll(Convert.ToDateTime(this.BookingDateTextBox.Text),
                    Convert.ToInt16(this.FloorDropDownList.SelectedValue),
                    Convert.ToInt16(this.TimeStartHourDropDownList.SelectedValue),
                    Convert.ToInt16(this.TimeStartMinuteDropDownList.SelectedValue),
                    Convert.ToInt16(this.TimeEndHourDropDownList.SelectedValue),
                    Convert.ToInt16(this.TimeEndMinuteDropDownList.SelectedValue));
                this.SearchImageButton_Click(null, null);
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void FloorDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SearchImageButton_Click(null, null);
        }

        //protected void CancelBookedButton_Click(object sender, ImageClickEventArgs e)
        //{
        //    ImageButton _temp = (ImageButton)sender;
        //    string[] _splitTemp = (_temp.ToolTip).Split('-');

        //    double _maxIDHistory = this._statusHistoryBL.GetMaxIDStatusHistory();

        //    POSTrInternetBookingDt _StatusInternetBooking = this._posInetBookingBL.GetSingleForStatus(_temp.ToolTip);
        //    _StatusInternetBooking.FgActive = false;
        //    _StatusInternetBooking.HistoryID = Convert.ToInt32(_maxIDHistory) + 1;
        //    this._posInetBookingBL.Edit(_StatusInternetBooking);

        //    POSTableStatusHistory _statusHistory = this._statusHistoryBL.GetSingleForInternet(_temp.ToolTip);
        //    _statusHistory.StillActive = false;
        //    _statusHistory.Status = 3;
        //    this._statusHistoryBL.Edit(_statusHistory);

        //    POSTableStatusHistory _newHistoryBookingData = new POSTableStatusHistory();

        //    _newHistoryBookingData.ID = Convert.ToInt32(_maxIDHistory) + 1;
        //    _newHistoryBookingData.FloorNmbr = _statusHistory.FloorNmbr;
        //    _newHistoryBookingData.FloorType = _statusHistory.FloorType;
        //    _newHistoryBookingData.TableID = _statusHistory.TableID;
        //    _newHistoryBookingData.StartTime = _statusHistory.StartTime;
        //    _newHistoryBookingData.EndTime = _statusHistory.EndTime;
        //    _newHistoryBookingData.Status = 0;
        //    _newHistoryBookingData.StillActive = true;

        //    this._statusHistoryBL.AddTrBookingHistory(_newHistoryBookingData);

        //    int startTimeVal = Convert.ToInt32(this.TimeStartHourDropDownList.SelectedValue) * 60 + Convert.ToInt32(this.TimeStartMinuteDropDownList.SelectedValue);
        //    int endTimeVal = Convert.ToInt32(this.TimeEndHourDropDownList.SelectedValue) * 60 + Convert.ToInt32(this.TimeEndMinuteDropDownList.SelectedValue);
        //    if (startTimeVal < endTimeVal)
        //    {
        //        this.AvailableTableRepeater.DataSource = this._posInetBookingBL.GetListUnReservedTable((Convert.ToDateTime(this.BookingDateTextBox.Text)),
        //            Convert.ToInt16(this.FloorDropDownList.SelectedValue),
        //            Convert.ToInt16(this.TimeStartHourDropDownList.SelectedValue),
        //            Convert.ToInt16(this.TimeStartMinuteDropDownList.SelectedValue),
        //            Convert.ToInt16(this.TimeEndHourDropDownList.SelectedValue),
        //            Convert.ToInt16(this.TimeEndMinuteDropDownList.SelectedValue));
        //        this.AvailableTableRepeater.DataBind();
        //        //this.BookedTableRepeater.DataSource = this._posInetBookingBL.GetListReservedTable(Convert.ToDateTime(this.BookingDateTextBox.Text),true,
        //        //    Convert.ToInt16(this.FloorDropDownList.SelectedValue),
        //        //    Convert.ToInt16(this.TimeStartHourDropDownList.SelectedValue),
        //        //    Convert.ToInt16(this.TimeStartMinuteDropDownList.SelectedValue),
        //        //    Convert.ToInt16(this.TimeEndHourDropDownList.SelectedValue),
        //        //    Convert.ToInt16(this.TimeEndMinuteDropDownList.SelectedValue));
        //        //this.BookedTableRepeater.DataBind();
        //    }
        //    else this.WarningLabel.Text = "End booking time must pass the start booking time.";

        //    //this._posInetBookingBL.Cancel(_temp.ToolTip);
        //    if (this.ReservationNoLabel.Text == "")
        //    {
        //        this.SearchImageButton_Click(null, null);
        //    }
        //    this.ShowDataBookingList();
        //}

        //protected void OpenBookedButton_Click(object sender, ImageClickEventArgs e)
        //{
        //    ImageButton _temp = (ImageButton)sender;

        //    int _maxHistoryID = this._statusHistoryBL.GetMaxIDStatusHistory();

        //    POSTrInternetBookingDt _StatusInternetBooking = this._posInetBookingBL.GetSingleForStatus(_temp.ToolTip);
        //    _StatusInternetBooking.FgActive = false;
        //    _StatusInternetBooking.HistoryID = Convert.ToInt32(_maxHistoryID + 1);
        //    this._posInetBookingBL.Edit(_StatusInternetBooking);

        //    POSTableStatusHistory _statusHistory = this._statusHistoryBL.GetSingleForInternet(_temp.ToolTip);
        //    _statusHistory.StillActive = false;
        //    this._statusHistoryBL.Edit(_statusHistory);

        //    POSTableStatusHistory _newHistoryBookingData = new POSTableStatusHistory();

        //    _newHistoryBookingData.ID = Convert.ToInt32(_maxHistoryID + 1);
        //    _newHistoryBookingData.FloorNmbr = _statusHistory.FloorNmbr;
        //    _newHistoryBookingData.TableID = _statusHistory.TableID;
        //    _newHistoryBookingData.StartTime = _statusHistory.StartTime;
        //    _newHistoryBookingData.EndTime = _statusHistory.EndTime;
        //    _newHistoryBookingData.Status = 0;
        //    _newHistoryBookingData.StillActive = true;

        //    this._statusHistoryBL.AddTrBookingHistory(_newHistoryBookingData);

        //    //this._posInetBookingBL.Cancel(_temp.ToolTip);
        //    if (this.ReservationNoLabel.Text == "")
        //    {
        //        this.SearchImageButton_Click(null, null);
        //    }
        //    this.ShowDataBookingList();
        //}

        protected void MemberNoTextBox_TextChanged(object sender, EventArgs e)
        {
            this.SearchImageButton_Click(null, null);
        }

        protected void BookingDateTextBox_TextChanged(object sender, EventArgs e)
        {
            //DateTime _now = DateTime.Now;
            //this.WarningLabel.Text = "";
            //if (Convert.ToDateTime(this.BookingDateTextBox.Text) <= _now.Date)
            //{
            //    this.WarningLabel.Text = "Date to be filled with accordingly.";
            //}
            this.SearchImageButton_Click(null, null);
        }

        protected void TimeStartHourDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DateTime _now = DateTime.Now;
            //DateTime _start = new DateTime(_now.Year, _now.Month, _now.Day, Convert.ToInt32(_now.Hour), Convert.ToInt32(_now.Minute), 0);
            //DateTime _ex = new DateTime(_now.Year, _now.Month, _now.Day, Convert.ToInt32(this.TimeStartHourDropDownList.SelectedValue), Convert.ToInt32(this.TimeStartMinuteDropDownList.SelectedValue), 0);

            //if (_ex < _start)
            //{
            //    this.WarningLabel.Text = "Time to be filled with accordingly";
            //}
            this.SearchImageButton_Click(null, null);
        }

        protected void TimeStartMinuteDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DateTime _now = DateTime.Now;
            //DateTime _start = new DateTime(_now.Year, _now.Month, _now.Day, Convert.ToInt32(_now.Hour), Convert.ToInt32(_now.Minute), 0);
            //DateTime _ex = new DateTime(_now.Year, _now.Month, _now.Day, Convert.ToInt32(this.TimeStartHourDropDownList.SelectedValue), Convert.ToInt32(this.TimeStartMinuteDropDownList.SelectedValue), 0);

            //if (_ex < _start)
            //{
            //    this.WarningLabel.Text = "Time to be filled with accordingly";
            //}
            this.SearchImageButton_Click(null, null);
        }

        protected void TimeEndHourDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SearchImageButton_Click(null, null);
        }

        protected void TimeEndMinuteDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SearchImageButton_Click(null, null);
        }

        protected void CancelBookingAutomatic()
        {
            try
            {
                //string _dateNow = DateFormMapper.GetValue(DateTime.Now);
                //foreach (RepeaterItem _bookTable in this.BookedTableRepeater.Items)
                //{
                //    Literal _startTime = (Literal)_bookTable.FindControl("TglLiteral");
                //    if (_startTime.Text >= _dateNow)
                //    {
                //    }
                //}

                Int16 _startAfter = 0;
                //CompanyConfiguration _companyConfiguration = _pOSConfigurationBL.GetSingle("POSBookingTimeLimitAfter");
                //_startAfter = Convert.ToInt16(_companyConfiguration.SetValue);
                _startAfter = Convert.ToInt16(new CompanyConfig().GetSingle(CompanyConfigure.POSBookingTimeLimitAfter).SetValue);

                List<POSMsInternetFloor> _pOSMsInternetFloor = this._posInetBookingBL.GetListInternetFloor();
                foreach (var _item in _pOSMsInternetFloor)
                {
                    List<POSTrInternetBookingDt> _pOSTrInternetBookingDt = this._posInetBookingBL.GetListReservedTable
                        (
                        _now.Date,
                        true,
                        _item.FloorNmbr,
                        0,
                        0,
                        _now.Hour,
                        _now.Minute
                        );
                    foreach (var _item2 in _pOSTrInternetBookingDt)
                    {
                        int _timeMinuteBooking = 0;
                        int _timeHourBooking = _item2.StartTimeHour;
                        if (_item2.StartTimeMinute + _startAfter < 60)
                        {
                            _timeMinuteBooking = _item2.StartTimeMinute + _startAfter;
                        }
                        else
                        {
                            int xHour = 0;
                            xHour = (_item2.StartTimeMinute + _startAfter) / 60;
                            _timeMinuteBooking = (_item2.StartTimeMinute + _startAfter) - (60 * xHour);
                            _timeHourBooking = _item2.StartTimeHour + xHour;
                        }
                        if (_now > new DateTime(_item2.BookingDate.Year, _item2.BookingDate.Month, _item2.BookingDate.Day, _timeHourBooking, _timeMinuteBooking, 0))
                        {
                            this._posInetBookingBL.CancelAll(
                            _item2.BookingDate,
                            _item2.FloorNmbr,
                            _item2.StartTimeHour,
                            _item2.StartTimeMinute,
                            _item2.EndTimeHour,
                            _item2.EndTimeMinute);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void errorhandler(Exception ex)
        {
            //ErrorHandler.Record(ex, EventLogEntryType.Error);
            String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "INTERNET");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
        }

        protected void ChangeVisiblePanel(Byte _prmValue)
        {
            if (_prmValue == 0)
            {
                this.PasswordPanel.Visible = false;
            }
            else if (_prmValue == 1)
            {
                this.PasswordPanel.Visible = true;
            }
        }

        protected void OKButton_Click(object sender, EventArgs e)
        {
            try
            {
                String _password = this._pOSConfigurationBL.GetSingle("POSCancelPassword").SetValue;
                if (_password.ToLower() == this.PasswordTextBox.Text.ToLower())
                {
                    double _maxIDHistory = this._statusHistoryBL.GetNewID();

                    POSTrInternetBookingDt _StatusInternetBooking = this._posInetBookingBL.GetSingleForStatus(this.CancelledTableHiddenField.Value);
                    _StatusInternetBooking.FgActive = false;
                    _StatusInternetBooking.HistoryID = Convert.ToInt32(_maxIDHistory) + 1;
                    this._posInetBookingBL.Edit(_StatusInternetBooking);

                    POSTableStatusHistory _statusHistory = this._statusHistoryBL.GetSingleForInternet(this.CancelledTableHiddenField.Value);
                    _statusHistory.StillActive = false;
                    _statusHistory.Status = 3;
                    this._statusHistoryBL.Edit(_statusHistory);

                    POSTableStatusHistory _newHistoryBookingData = new POSTableStatusHistory();

                    _newHistoryBookingData.ID = Convert.ToInt32(_maxIDHistory) + 1;
                    _newHistoryBookingData.FloorNmbr = _statusHistory.FloorNmbr;
                    _newHistoryBookingData.FloorType = _statusHistory.FloorType;
                    _newHistoryBookingData.TableID = _statusHistory.TableID;
                    _newHistoryBookingData.StartTime = _statusHistory.StartTime;
                    _newHistoryBookingData.EndTime = _statusHistory.EndTime;
                    _newHistoryBookingData.Status = 0;
                    _newHistoryBookingData.StillActive = true;

                    this._statusHistoryBL.AddTrBookingHistory(_newHistoryBookingData);

                    int startTimeVal = Convert.ToInt32(this.TimeStartHourDropDownList.SelectedValue) * 60 + Convert.ToInt32(this.TimeStartMinuteDropDownList.SelectedValue);
                    int endTimeVal = Convert.ToInt32(this.TimeEndHourDropDownList.SelectedValue) * 60 + Convert.ToInt32(this.TimeEndMinuteDropDownList.SelectedValue);
                    if (startTimeVal < endTimeVal)
                    {
                        this.AvailableTableRepeater.DataSource = this._posInetBookingBL.GetListUnReservedTable((Convert.ToDateTime(this.BookingDateTextBox.Text)),
                            Convert.ToInt16(this.FloorDropDownList.SelectedValue),
                            Convert.ToInt16(this.TimeStartHourDropDownList.SelectedValue),
                            Convert.ToInt16(this.TimeStartMinuteDropDownList.SelectedValue),
                            Convert.ToInt16(this.TimeEndHourDropDownList.SelectedValue),
                            Convert.ToInt16(this.TimeEndMinuteDropDownList.SelectedValue));
                        this.AvailableTableRepeater.DataBind();
                    }

                    this.SearchImageButton_Click(null, null);
                    this.ChangeVisiblePanel(0);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Transaction Has Been Cancelled.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Incorrect Password.');", true);
                }
                this.PasswordTextBox.Text = "";
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }
    }
}
