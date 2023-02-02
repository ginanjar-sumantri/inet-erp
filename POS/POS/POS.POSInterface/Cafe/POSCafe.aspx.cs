using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessRule.POSInterface;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace POS.POSInterface.Cafe
{
    public partial class POSCafe : POSCafeBase
    {
        protected MemberForBL _memberBL = new MemberForBL();
        protected POSTrCafeBookingBL _cafeBookingBL = new POSTrCafeBookingBL();
        protected InternetFloorBL _internetFloorBL = new InternetFloorBL();
        protected POSCafeBL _posCafeBL = new POSCafeBL();
        protected POSInternetTableBL _internetTableBL = new POSInternetTableBL();
        protected POSTableStatusHistoryBL _histTableBL = new POSTableStatusHistoryBL();

        protected BoxLayout _boxLayout;
        protected String _lebarPanelLayout,
            _tinggiPanelLayout,
            _posisiXPanelLayout,
            _posisiYPanelLayout,
            _gridSpace,
            _tinggiBox,
            _lebarBox,
            _boxCount;

        protected void ReadPosition(String _prmRoomCode)
        {
            this._boxLayout = new BoxLayout(_prmRoomCode);
            this.hiddenBoxPosition.Value = _boxLayout.StringPosition;
            this._boxCount = _boxLayout.BoxCount.ToString();
            this.hiddenBoxCount.Value = _boxCount;
        }

        protected void ComposeLayout(String _prmRoomCode)
        {
            this._boxLayout = new BoxLayout(_prmRoomCode);
            this._lebarPanelLayout = _boxLayout.LebarPanelLayout.ToString();
            this._tinggiPanelLayout = _boxLayout.TinggiPanelLayout.ToString();
            this._posisiXPanelLayout = _boxLayout.PosisiXPanelLayout.ToString();
            this._posisiYPanelLayout = _boxLayout.PosisiYPanelLayout.ToString();
            this._gridSpace = _boxLayout.GridSpace.ToString();
            this._tinggiBox = _boxLayout.TinggiBox.ToString();
            this._lebarBox = _boxLayout.LebarBox.ToString();
            //if (_boxLayout.BackgroundImage != "" )
            String _imgpath = ApplicationConfig.POSImportVirDirPath + _boxLayout.BackgroundImage.ToString();

            this.CSSLiteral.Text = "<style type='text/css'>" +
                ".divPanel {float:left;background:#AAA;width:" + _lebarPanelLayout + "px;" +
                "height:" + _tinggiPanelLayout + "px;border:inset 5px #AAA;padding: 0px 0px 0px 0px;" +
                "margin: 0px 0px 0px 0px;}" +
                ".divBox  {position:absolute; height:" + _tinggiBox + "px;width:" + _lebarBox + "px;" +
                "background:#CFC;text-align:center;font-family:Arial Rounded MT Bold;" +
                "font-size:16px;border:solid 1px #CDE;}" +
                "</style>";

            //this.PanelLayout.Style.Add("background-image", "url('" + _imgpath.Replace(" ", "%20") + "');no-repeat; background-position :center");
            this.PanelLayout.Attributes.Add("style", "background:url(\"" + _imgpath.Replace(" ", "%20") + "\") no-repeat; background-position :center");
        }

        protected void RenderLayout()
        {
            try
            {
                String _strDivBoxes = "", _jsPositionSetter = "<script language='javascript'>";
                String[] _splitedPosition = this.hiddenBoxPosition.Value.Split('|');

                for (int i = 1; i <= Convert.ToInt16(_boxCount); i++)
                {
                    int _tableStatus = this._posCafeBL.GetTableStatus(this.currFloorHiddenField.Value, i);
                    if (_splitedPosition.Length < i || this.hiddenBoxPosition.Value == "")
                    {
                        String _backgroundStyleJunction = "";
                        if (_tableStatus == POSTrInternetDataMapper.GetStatusTable(POSMsInternetTableStatus.Available))
                        {
                            _strDivBoxes += "<div id=\"divBox" + i.ToString() + "\" class=\"divBox available\"" + ((_tableStatus > 0) ? _backgroundStyleJunction : "") + ">" + this._posCafeBL.GetTableNumber(this.currFloorHiddenField.Value, i) + "</div>";
                        }
                        if (_tableStatus == POSTrInternetDataMapper.GetStatusTable(POSMsInternetTableStatus.Booking))
                        {
                            _strDivBoxes += "<div id=\"divBox" + i.ToString() + "\" class=\"divBox reserved\"" + ((_tableStatus > 0) ? _backgroundStyleJunction : "") + ">" + this._posCafeBL.GetTableNumber(this.currFloorHiddenField.Value, i) + "</div>";
                        }
                        if (_tableStatus == POSTrInternetDataMapper.GetStatusTable(POSMsInternetTableStatus.NotAvailable))
                        {
                            if (this._histTableBL.Is15MinsLeftCafe(this.currFloorHiddenField.Value, i))
                            {
                                _strDivBoxes += "<div id=\"divBox" + i.ToString() + "\" class=\"divBox not-available15min\"" + ((_tableStatus > 0) ? _backgroundStyleJunction : "") + ">" + this._posCafeBL.GetTableNumber(this.currFloorHiddenField.Value, i) + "</div>";
                            }
                            else
                            {
                                _strDivBoxes += "<div id=\"divBox" + i.ToString() + "\" class=\"divBox not-available\"" + ((_tableStatus > 0) ? _backgroundStyleJunction : "") + ">" + this._posCafeBL.GetTableNumber(this.currFloorHiddenField.Value, i) + "</div>";
                            }
                        }
                    }
                    else
                    {
                        String _backgroundStyleJunction = "";
                        String[] _splitedXY = _splitedPosition[i - 1].Split(',');
                        int _xAdjust = -10;
                        int _yAdjust = -40;
                        if (_tableStatus == POSTrInternetDataMapper.GetStatusTable(POSMsInternetTableStatus.Available))
                        {
                            _strDivBoxes += "<div id=\"divBox" + i.ToString() + "\" class=\"divBox available\" style=\"top:" + (Convert.ToInt16(_splitedXY[1]) + _yAdjust).ToString() + "px;left:" + (Convert.ToInt16(_splitedXY[0]) + _xAdjust).ToString() + "px;" + ((_tableStatus > 0) ? _backgroundStyleJunction : "") + "\">" + this._posCafeBL.GetTableNumber(this.currFloorHiddenField.Value, i) + "</div>";
                        }
                        if (_tableStatus == POSTrInternetDataMapper.GetStatusTable(POSMsInternetTableStatus.Booking))
                        {
                            _strDivBoxes += "<div id=\"divBox" + i.ToString() + "\" class=\"divBox reserved\" style=\"top:" + (Convert.ToInt16(_splitedXY[1]) + _yAdjust).ToString() + "px;left:" + (Convert.ToInt16(_splitedXY[0]) + _xAdjust).ToString() + "px;" + ((_tableStatus > 0) ? _backgroundStyleJunction : "") + "\">" + this._posCafeBL.GetTableNumber(this.currFloorHiddenField.Value, i) + "</div>";
                        }
                        if (_tableStatus == POSTrInternetDataMapper.GetStatusTable(POSMsInternetTableStatus.NotAvailable))
                        {
                            if (this._histTableBL.Is15MinsLeftCafe(this.currFloorHiddenField.Value, i))
                            {
                                _strDivBoxes += "<div id=\"divBox" + i.ToString() + "\" class=\"divBox not-available15min\" style=\"top:" + (Convert.ToInt16(_splitedXY[1]) + _yAdjust).ToString() + "px;left:" + (Convert.ToInt16(_splitedXY[0]) + _xAdjust).ToString() + "px;" + ((_tableStatus > 0) ? _backgroundStyleJunction : "") + "\">" + this._posCafeBL.GetTableNumber(this.currFloorHiddenField.Value, i) + "</div>";
                            }
                            else
                            {
                                _strDivBoxes += "<div id=\"divBox" + i.ToString() + "\" class=\"divBox not-available\" style=\"top:" + (Convert.ToInt16(_splitedXY[1]) + _yAdjust).ToString() + "px;left:" + (Convert.ToInt16(_splitedXY[0]) + _xAdjust).ToString() + "px;" + ((_tableStatus > 0) ? _backgroundStyleJunction : "") + "\">" + this._posCafeBL.GetTableNumber(this.currFloorHiddenField.Value, i) + "</div>";
                            }
                        }
                        _jsPositionSetter += "posXBox[" + i + "] = " + _splitedXY[0] + ";\n";
                        _jsPositionSetter += "posYBox[" + i + "] = " + _splitedXY[1] + ";\n";
                    }
                }
                _jsPositionSetter += "</script>\n";
                this.literalBoxes.Text = _strDivBoxes + _jsPositionSetter;

                String _jScript = "<script language='JavaScript'>";
                _jScript += "$(document).ready(function() {\n";

                if (this.WarningTypeHiddenField.Value != "")
                {
                    if (this.WarningHiddenField.Value != "")
                    {
                        switch (this.WarningTypeHiddenField.Value)
                        {
                            case "Error":
                                _jScript += " jAlert('error', '" + this.WarningHiddenField.Value + "', 'Error');\n";
                                break;
                            case "Warning":
                                _jScript += " jAlert('warning', '" + this.WarningHiddenField.Value + "', 'Warning');\n";
                                break;
                            case "Success":
                                _jScript += " jAlert('success', '" + this.WarningHiddenField.Value + "', 'Success');\n";
                                break;
                            case "Info":
                                _jScript += " jAlert('info', '" + this.WarningHiddenField.Value + "', 'Info');\n";
                                break;
                            default:
                                _jScript += "";
                                break;
                        }
                    }
                }
                //_jScript += "jAlert('error', 'This is the error dialog box with some extra text.', 'Error Dialog');\n";
                _jScript += "});\n";
                _jScript += "</script>";
                this.JScriptWarning.Text = _jScript;
                //this.WarningTypeHiddenField.Value = "";
                //this.WarningHiddenField.Value = "";
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            //int _tableStatus = this._posCafeBL.GetTableStatus(this.currFloorHiddenField.Value,Convert.ToInt32(this.SelectedTableHiddenField.Value));
            //if (this.SelectedTableHiddenField.Value != "" && _tableStatus == 1 || _tableStatus == 0)


            if (this.SelectedTableHiddenField.Value != "")
            {
                {
                    Response.Redirect(this._chooseTablePage + "?" + this._selectedTableKey + "=" +
                        HttpUtility.UrlEncode(Rijndael.Encrypt(this.SelectedTableHiddenField.Value, ApplicationConfig.EncryptionKey)) +
                        "&" + this._selectedFloorKey + "=" +
                        HttpUtility.UrlEncode(Rijndael.Encrypt(this.currFloorHiddenField.Value, ApplicationConfig.EncryptionKey)) +
                        "&" + this._referenceNo + "=" +
                        HttpUtility.UrlEncode(Rijndael.Encrypt("", ApplicationConfig.EncryptionKey)));

                }
            }

            if (Request.QueryString[this._selectedFloorKey].ToString() != "")
            {
                this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);
                if (this.currFloorHiddenField.Value != "")
                    this.currFloorHiddenField.Value = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._selectedFloorKey), ApplicationConfig.EncryptionKey);
            }

            if (this.currFloorHiddenField.Value != "")
                this.ComposeLayout(this.currFloorHiddenField.Value);
        }

        protected void FillFromToTable()
        {
            try
            {
                List<POSMsInternetTable> _listOccupiedTable = this._posCafeBL.GetListOccupiedTable(this.currFloorHiddenField.Value);
                this.FromTableDropDownList.DataSource = _listOccupiedTable;
                this.FromTableDropDownList.DataValueField = "TableIDPerRoom";
                this.FromTableDropDownList.DataTextField = "TableNmbr";
                this.FromTableDropDownList.DataBind();
                this.TableDropDownList.DataSource = _listOccupiedTable;
                this.TableDropDownList.DataValueField = "TableIDPerRoom";
                this.TableDropDownList.DataTextField = "TableNmbr";
                this.TableDropDownList.DataBind();

                this.ToTableDropDownList.DataSource = this._posCafeBL.GetListAvailableTable(this.currFloorHiddenField.Value);
                this.ToTableDropDownList.DataValueField = "TableIDPerRoom";
                this.ToTableDropDownList.DataTextField = "TableNmbr";
                this.ToTableDropDownList.DataBind();

                if (this.FromTableDropDownList.Items.Count > 0 && this.ToTableDropDownList.Items.Count > 0)
                    this.TableTransferImageButton.Enabled = true;
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);
                    this.currFloorHiddenField.Value = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._selectedFloorKey), ApplicationConfig.EncryptionKey);
                    POSMsInternetFloor _pOSMsInternetFloor = _internetFloorBL.GetSinglePOSMsInternetFloorByRoomCode(Convert.ToString(this.currFloorHiddenField.Value));
                    this.currFloorNameHiddenField.Value = _pOSMsInternetFloor.FloorName;

                    this.CheckStatusImageButton.OnClientClick = "window.open('../General/CheckStatus.aspx?pos=cafe','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1')";
                    this.TableTransferImageButton.Enabled = false;
                    //this._internetTableBL.UpdateTables();

                    this.FloorButtonRepeater.DataSource = this._posCafeBL.GetList(Convert.ToInt16(this.floorPagingPageHiddenField.Value), 2);
                    this.FloorButtonRepeater.DataBind();
                    //if (this.currFloorHiddenField.Value != "")
                    //{
                    //    this.ComposeLayout(this.currFloorHiddenField.Value);
                    //    this.ReadPosition(this.currFloorHiddenField.Value);

                    //    if (!Page.IsPostBack)
                    //    {
                    //        this.FillFromToTable();
                    //    }
                    //}
                    //this.RenderLayout();
                    //this.ShowData();
                    this.RefreshPage2();
                }
                this.RefreshPage();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void FloorButtonRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            POSMsInternetFloor _temp = (POSMsInternetFloor)e.Item.DataItem;

            Button _floorButton = (Button)e.Item.FindControl("FloorButton");
            _floorButton.Text = _temp.FloorName;
            _floorButton.ToolTip = _temp.roomCode;
        }

        protected void FloorButton_OnClick(object sender, EventArgs e)
        {
            Button _temp = (Button)sender;
            this.currFloorHiddenField.Value = _temp.ToolTip;
            //this.SelectedTableHiddenField.Value = _temp.ToolTip;
            this.currFloorNameHiddenField.Value = _temp.Text;
            this.RefreshPage();
        }

        protected void DownFloorPanelButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (Convert.ToInt16(this.floorPagingPageHiddenField.Value) < this._posCafeBL.GetLastPageFloorButton(2))
                {
                    this.floorPagingPageHiddenField.Value = (Convert.ToInt16(this.floorPagingPageHiddenField.Value) + 1).ToString();
                    this.FloorButtonRepeater.DataSource = this._posCafeBL.GetList(Convert.ToInt16(this.floorPagingPageHiddenField.Value), 2);
                    this.FloorButtonRepeater.DataBind();
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void UpFloorPanelButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (Convert.ToInt16(this.floorPagingPageHiddenField.Value) > 0)
                {
                    this.floorPagingPageHiddenField.Value = (Convert.ToInt16(this.floorPagingPageHiddenField.Value) - 1).ToString();
                    this.FloorButtonRepeater.DataSource = this._posCafeBL.GetList(Convert.ToInt16(this.floorPagingPageHiddenField.Value), 2);
                    this.FloorButtonRepeater.DataBind();
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void BackImageButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../Login.aspx");
        }

        protected void ReservationImageButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("POSCafeReservation.aspx" + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt("", ApplicationConfig.EncryptionKey)));
        }

        protected void JoinTableImageButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("JoinTable.aspx");
        }

        protected void TakeAwayImageButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._chooseTablePage + "?" + this._selectedTableKey + "=" +
                        HttpUtility.UrlEncode(Rijndael.Encrypt("", ApplicationConfig.EncryptionKey)) +
                        "&" + this._selectedFloorKey + "=" +
                        HttpUtility.UrlEncode(Rijndael.Encrypt("", ApplicationConfig.EncryptionKey)) +
                        "&" + this._referenceNo + "=" +
                        HttpUtility.UrlEncode(Rijndael.Encrypt("", ApplicationConfig.EncryptionKey))
                        );
        }

        protected void TableTransferImageButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //POSTableStatusHistoryBL _tableHistBL = new POSTableStatusHistoryBL();
                int _newID = _histTableBL.GetNewIDCafe();
                this._posCafeBL.TableTransfer(this.FromTableDropDownList.SelectedValue, this.ToTableDropDownList.SelectedValue, this.currFloorNameHiddenField.Value, _newID);
                this.RefreshPage();
                this.RefreshPage2();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:$.msg({autoUnblock: false,content: 'Transfer Table Successfully'});", true);
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        private void ShowData()
        {
            try
            {
                this.BookingListRepeater.DataSource = this._cafeBookingBL.GetList();
                this.BookingListRepeater.DataBind();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void BookingListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    POSTrCafeBookingDt _temp = (POSTrCafeBookingDt)e.Item.DataItem;
                    string _code = _temp.CafeBookCode.ToString(); //+ "-" + _temp.CustomerID.ToString() + "-" + _temp.CircuitID.ToString();
                    string _code2 = _temp.CafeBookCode.ToString() + "-" + _temp.ItemNo.ToString();

                    //ImageButton _openButton = (ImageButton)e.Item.FindControl("OpenBookedButton");
                    //_openButton.PostBackUrl = this._reservationPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code2, ApplicationConfig.EncryptionKey));

                    ImageButton _openButton = (ImageButton)e.Item.FindControl("OpenBookedButton");
                    _openButton.ToolTip = _temp.HistoryID.ToString();
                    _openButton.Attributes.Add("Onclick", "return OpenBooked();");
                    _openButton.CommandName = "OpenBooked";
                    _openButton.CommandArgument = _temp.HistoryID.ToString() + "-" + _temp.CafeBookCode.ToString() + "-" + _temp.ItemNo.ToString();

                    Literal _noMemberLiteral = (Literal)e.Item.FindControl("NoMemberLiteral");
                    _noMemberLiteral.Text = HttpUtility.HtmlEncode(this._cafeBookingBL.GetMemberCode(_temp.CafeBookCode));

                    Literal _nameLiteral = (Literal)e.Item.FindControl("NameLiteral");
                    _nameLiteral.Text = HttpUtility.HtmlEncode(this._cafeBookingBL.GetCustName(_temp.CafeBookCode));

                    Literal _noTelponLiteral = (Literal)e.Item.FindControl("NoTelponLiteral");
                    _noTelponLiteral.Text = HttpUtility.HtmlEncode(this._cafeBookingBL.GetPhoneNmbr(_temp.CafeBookCode));

                    Literal _tableLiteral = (Literal)e.Item.FindControl("TableLiteral");
                    _tableLiteral.Text = HttpUtility.HtmlEncode(Convert.ToString(this._internetFloorBL.GetMemberNameByCodeCafe(Convert.ToString(_temp.FloorNmbr)) + "/" + _temp.TableIDPerRoom));

                    Literal _dateLiteral = (Literal)e.Item.FindControl("DateLiteral");
                    _dateLiteral.Text = DateFormMapper.GetValue(_temp.BookingDate);

                    Literal _timeLiteral = (Literal)e.Item.FindControl("TimeLiteral");
                    _timeLiteral.Text = HttpUtility.HtmlEncode(_temp.StartTimeHour.ToString("00") + ":" + _temp.StartTimeMinute.ToString("00"));

                    Literal _toLiteral = (Literal)e.Item.FindControl("ToLiteral");
                    _toLiteral.Text = HttpUtility.HtmlEncode(_temp.EndTimeHour.ToString("00") + ":" + _temp.EndTimeMinute.ToString("00"));

                    Literal _operatorNameLiteral = (Literal)e.Item.FindControl("OperatorNameLiteral");
                    _operatorNameLiteral.Text = HttpUtility.HtmlEncode(this._cafeBookingBL.GetOperatorName(_temp.CafeBookCode));
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void BookingListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "OpenBooked")
                {
                    string _code = e.CommandArgument.ToString();

                    int _maxHistoryID = this._histTableBL.GetNewID();

                    POSTrCafeBookingDt _StatusCafeBooking = this._cafeBookingBL.GetSingleForStatus(_code);
                    _StatusCafeBooking.FgActive = false;
                    _StatusCafeBooking.HistoryID = Convert.ToInt32(_maxHistoryID + 1);
                    this._cafeBookingBL.Edit(_StatusCafeBooking);

                    POSTableStatusHistory _statusHistory = this._histTableBL.GetSingleForCafe(_code);
                    _statusHistory.StillActive = false;
                    this._histTableBL.Edit(_statusHistory);

                    POSTableStatusHistory _newHistoryBookingData = new POSTableStatusHistory();

                    _newHistoryBookingData.ID = Convert.ToInt32(_maxHistoryID + 1);
                    _newHistoryBookingData.FloorNmbr = _statusHistory.FloorNmbr;
                    _newHistoryBookingData.FloorType = _statusHistory.FloorType;
                    _newHistoryBookingData.TableID = _statusHistory.TableID;
                    _newHistoryBookingData.StartTime = _statusHistory.StartTime;
                    _newHistoryBookingData.EndTime = _statusHistory.EndTime;
                    _newHistoryBookingData.Status = 0;
                    _newHistoryBookingData.StillActive = true;

                    this._histTableBL.AddTrBookingHistory(_newHistoryBookingData);
                    this.ShowData();
                    this.RefreshPage();
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void StopImageButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                String _result = this._posCafeBL.StopCafe(this.currFloorNameHiddenField.Value, this.TableDropDownList.SelectedValue);
                if (_result == "")
                {
                    //this.WarningTypeHiddenField.Value = "Success";
                    //this.WarningHiddenField.Value = "Stop Time Success";
                    this.RefreshPage();
                    this.RefreshPage2();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:$.msg({autoUnblock: false,content: 'Stop Time Successfully'});", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:$.msg({autoUnblock: false,content: 'Stop Time Failed'});", true);
                    //this.WarningTypeHiddenField.Value = "Error";
                    //this.WarningHiddenField.Value = _result;
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        //protected void ButtonAddTimeInternet_Click(object sender, ImageClickEventArgs e)
        //{
        //    String _result = this._posCafeBL.AddTimeInternet(this.currFloorNameHiddenField.Value, this.TableDropDownList.SelectedValue, Convert.ToInt32(this.TimeDropdownList.SelectedValue));
        //    if (_result == "")
        //    {
        //        this.WarningTypeHiddenField.Value = "Success";
        //        this.WarningHiddenField.Value = "Add Time Success";
        //        this.RefreshPage();
        //    }
        //    else
        //    {
        //        this.WarningTypeHiddenField.Value = "Error";
        //        this.WarningHiddenField.Value = _result;
        //    }
        //}

        private void RefreshPage()
        {
            try
            {
                this._internetTableBL.UpdateTables("Cafe");
                //this.FillFromToTable();
                //if (this.currFloorHiddenField.Value != "")
                //{
                this.ComposeLayout(this.currFloorHiddenField.Value);
                this.ReadPosition(this.currFloorHiddenField.Value);
                //}
                this.RenderLayout();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        private void RefreshPage2()
        {
            this.FillFromToTable();
            this.ShowData();
        }

        protected void TimerRefresher_Tick(object sender, EventArgs e)
        {
            this.RefreshPage2();
        }

        protected void errorhandler(Exception ex)
        {
            //ErrorHandler.Record(ex, EventLogEntryType.Error);
            String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "CAFE");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
        }

        protected void TableInfoImageButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (this.TableDropDownList.SelectedIndex != -1)
                {
                    POSTrCafeHd _pOSTrCafeHd = this._posCafeBL.GetSinglePOSTableStatusHistory(this._posCafeBL.GetFloorNmbrByName(this.currFloorHiddenField.Value), Convert.ToInt16(this.TableDropDownList.SelectedValue));
                    if (_pOSTrCafeHd != null)
                    {
                        this.ReferenceLiteral.Text = _pOSTrCafeHd.ReferenceNo;
                        this.CustNameLiteral.Text = _pOSTrCafeHd.CustName;
                        this.PaxLiteral.Text = _pOSTrCafeHd.PAX.ToString();
                    }
                    Int32 _id = this._histTableBL.GetID(this._posCafeBL.GetFloorNmbrByName(this.currFloorHiddenField.Value), Convert.ToInt32(this.TableDropDownList.SelectedValue), POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe));
                    POSTableStatusHistory _posTableStatusHistory = this._histTableBL.GetSingleForCafe(_id.ToString());
                    if (_posTableStatusHistory != null)
                    {
                        String _time = Convert.ToString(Convert.ToDateTime(_posTableStatusHistory.StartTime).Hour) + ":" + Convert.ToString(Convert.ToDateTime(_posTableStatusHistory.StartTime).Minute) + " - " + Convert.ToString(Convert.ToDateTime(_posTableStatusHistory.EndTime).Hour) + ":" + Convert.ToString(Convert.ToDateTime(_posTableStatusHistory.EndTime).Minute);
                        this.TimeLiteral.Text = _time;
                        String _timeOccupied = (DateTime.Now - Convert.ToDateTime(_posTableStatusHistory.StartTime)).Hours + ":" + (DateTime.Now - Convert.ToDateTime(_posTableStatusHistory.StartTime)).Minutes;
                        this.OccupiedLiteral.Text = _timeOccupied;
                    }
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }
    }
}