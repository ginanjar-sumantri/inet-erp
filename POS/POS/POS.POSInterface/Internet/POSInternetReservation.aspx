<%@ Page Language="C#" AutoEventWireup="true" CodeFile="POSInternetReservation.aspx.cs"
    Inherits="POS.POSInterface.Internet.POSInternetReservation" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>POS Internet</title>

    <script src="../JQuery/jquery-ui.min.js" type="text/javascript"></script>

    <script src="../JQuery/jquery-1.4.3.min.js" type="text/javascript"></script>

    <script src="../JQuery/jquery-1.2.6.min.js" type="text/javascript"></script>

    <script src="../JQuery/jquery.event.drag-1.5.min.js" type="text/javascript"></script>

    <script src="../CSS/orange/jquery.corner.js" type="text/javascript"></script>

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script src="../CSS/orange/style.js" type="text/javascript"></script>

    <script src="../CSS/orange/JScript.js" type="text/javascript"></script>

    <link href="../JQuery/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />
    <link href="../CSS/orange/style4.css" media="all" rel="Stylesheet" type="text/css" />
    <asp:Literal ID="JScriptLiteral" runat="server"></asp:Literal>

    <script type="text/javascript" language="javascript">
        var dateObject = new Date();
        var Hari = new Array("SUNDAY", "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY");
        var Bulan = new Array("JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER");
        var hour = dateObject.getHours();
        var minute = dateObject.getMinutes();
        var second = dateObject.getSeconds();
        if (hour < 10) {
            hour = "0" + hour;
        }
        if (minute < 10) {
            minute = "0" + minute;
        }
        if (second < 10) {
            second = "0" + second;
        }
        $('.container .header .right .date #tanggal').text(Hari[dateObject.getDay()] + ', ' + dateObject.getDate() + ' ' + Bulan[dateObject.getMonth()] + ' ' + (dateObject.getYear() + 1900));
        $('.container .header .right .date #jam').text(hour + ':' + minute + ':' + second);
        function updateJam() {
            var dateObject = new Date();
            var Hari = new Array("SUNDAY", "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY");
            var Bulan = new Array("JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER");
            var hour = dateObject.getHours();
            var minute = dateObject.getMinutes();
            var second = dateObject.getSeconds();
            if (hour < 10) {
                hour = "0" + hour;
            }
            if (minute < 10) {
                minute = "0" + minute;
            }
            if (second < 10) {
                second = "0" + second;
            }
            $('.container .header .right .date #tanggal').text(Hari[dateObject.getDay()] + ', ' + dateObject.getDate() + ' ' + Bulan[dateObject.getMonth()] + ' ' + (dateObject.getYear() + 1900));
            $('.container .header .right .date #jam').text(hour + ':' + minute + ':' + second);
        }
        var x = setInterval("updateJam();", 1000);
    </script>

    <script language="javascript" type="text/javascript">
        function CancelBooked() {
            var _result = false;

            if (confirm("Are you sure want to cancel this booking ?") == true) {
                _result = true;
            }
            else {
                _result = false;
            }

            return _result
        }
    </script>

    <script language="javascript" type="text/javascript">
        function OpenBooked() {
            var _result = false;

            if (confirm("Are you sure want to open this booking ?") == true) {
                _result = true;
            }
            else {
                _result = false;
            }

            return _result
        }
    </script>

    <style type="text/css">
        .textValid
        {
            color: Red;
            font-size: 17px;
            font-family: caption;
        }
        .Highlight
        {
            background-color: Red;
        }
    </style>
</head>
<body id="bodyPosReservation">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />
    <asp:UpdatePanel ID="contentupdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Literal runat="server" ID="CSSLiteral"></asp:Literal>
            <asp:Literal runat="server" ID="javascriptReceiver"></asp:Literal>
            <div class="container">
                <div class="header">
                    <div class="left">
                        <div class="title">
                            <div class="text">
                                POS Internet</div>
                            <div class="sep">
                            </div>
                        </div>
                    </div>
                    <div class="right">
                        <div class="date">
                            <div id="tanggal">
                            </div>
                            <div id="jam">
                                <asp:HiddenField runat="server" ID="OperatorHiddenField" />
                            </div>
                        </div>
                    </div>
                </div>
                <%--<div style="height: 13px; background-color: #999;">
        </div>--%>
                <!-- //bagian atas -->
                <%--<div id="FormDiv" style="padding: 5px;">--%>
                <asp:Panel ID="PasswordPanel" runat="server">
                    <div>
                        <table cellpadding="3" cellspacing="1" width="0" border="0">
                            <tr class="bgproductListBox">
                                <td style="width: 190px; font-size: 16px; text-align: left; color: White;">
                                    <b>Password Required : </b>
                                </td>
                                <td>
                                    <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="OKButton" runat="server" Text="OK" OnClick="OKButton_Click" CausesValidation="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <div class="containerBox">
                    <div class="reservation">
                        <table>
                            <tr>
                                <td>
                                    RESERVATION
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="reservationInput">
                        <asp:Panel ID="ReservationNoPanel" runat="server">
                            <div class="reservationBox" id="ReservationNoDiv">
                                <table class="width">
                                    <tr>
                                        <td style="width: 140px;">
                                            Reservation No.
                                        </td>
                                        <td style="font-size: 13px; width: 205px;">
                                            <%-- <asp:TextBox runat="server" ID="ReservationNoTextBox" Width="150" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>--%>
                                            <asp:Label runat="server" ID="ReservationNoLabel"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <div class="reservationBox">
                            <table class="width">
                                <tr>
                                    <td style="width: 150px;">
                                        Reservation Type
                                    </td>
                                </tr>
                                <tr>
                                    <td class="checkBox">
                                        <asp:RadioButton runat="server" ID="ReservationRadioButton" Text="Reservation" GroupName="ReservationType"
                                            Checked="true" />
                                    </td>
                                    <td class="checkBox" style="width: 150px; height: 24px;">
                                        <asp:RadioButton runat="server" ID="BookEventRadioButton" Text="Book Event" GroupName="ReservationType" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="reservationBox">
                            <table class="width">
                                <tr>
                                    <td style="width: 132px;">
                                        Reference No.
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="ReferensiNoTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="reservationBox">
                            <table class="width">
                                <tr>
                                    <td style="width: 170px;">
                                        Member No.
                                    </td>
                                    <td style="width: 190px">
                                        <asp:TextBox runat="server" ID="MemberNoTextBox" Width="120px" OnTextChanged="MemberNoTextBox_TextChanged"
                                            AutoPostBack="true"></asp:TextBox>
                                        <asp:Button runat="server" ID="SearchMemberButton" CausesValidation="false" Text="..." />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="reservationBox">
                            <table class="width">
                                <tr>
                                    <td style="width: 130px;">
                                        Name
                                    </td>
                                    <td class="textValid">
                                        <asp:TextBox runat="server" ID="MemberNameTextBox" Width="116px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="MemberNameRequiredFieldValidator" runat="server"
                                            ErrorMessage="Name Must Be Filled" Text="*" ControlToValidate="MemberNameTextBox"
                                            Display="None"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender2"
                                            TargetControlID="MemberNameRequiredFieldValidator" HighlightCssClass="Highlight">
                                            <Animations>
                                 <OnShow>                                    
                                 <Sequence>   
        <HideAction Visible="true" /> 
        <FadeIn Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
        </Sequence>
        </OnShow>
        <OnHide>
        <Sequence>    
        <FadeOut Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
        <HideAction Visible="false" />
        </Sequence>
        </OnHide></Animations>
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="reservationBox">
                            <table class="width">
                                <tr>
                                    <td style="width: 132px;">
                                        Address
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="AddressTextBox" TextMode="MultiLine" Width="168px"
                                            Height="50px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="reservationBox">
                            <table class="width">
                                <tr>
                                    <td>
                                        Phone No.
                                    </td>
                                    <td class="textValid">
                                        <asp:TextBox runat="server" ID="PhoneNumber1TextBox" Width="116px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PhoneNumber1RequiredFieldValidator" ErrorMessage="Phone No Must Be Filled"
                                            ControlToValidate="PhoneNumber1TextBox" runat="server" Display="None" Text="*"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1"
                                            TargetControlID="PhoneNumber1RequiredFieldValidator" HighlightCssClass="Highlight">
                                            <Animations>
                                 <OnShow>                                    
                                 <Sequence>   
        <HideAction Visible="true" /> 
        <FadeIn Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
        </Sequence>
        </OnShow>
        <OnHide>
        <Sequence>    
        <FadeOut Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
        <HideAction Visible="false" />
        </Sequence>
        </OnHide></Animations>
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="reservationBox">
                            <table class="width">
                                <tr>
                                    <td>
                                        Phone No. 2
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="PhoneNumber2TextBox" Width="116px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="reservationBox">
                            <table class="width">
                                <tr>
                                    <td style="width: 176px;">
                                        Booking Date
                                    </td>
                                    <td style="width: 210px">
                                        <asp:TextBox runat="server" ID="BookingDateTextBox" Width="105" MaxLength="30" BackColor="#CCCCCC"
                                            OnTextChanged="BookingDateTextBox_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <input id="DateButton" type="button" onclick="displayCalendar(BookingDateTextBox,'yyyy-mm-dd',this)"
                                            value="..." />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="reservationBox">
                            <table class="width">
                                <tr>
                                    <td>
                                        Time Start
                                    </td>
                                    <td style="width: 160px">
                                        <asp:DropDownList runat="server" ID="TimeStartHourDropDownList" OnSelectedIndexChanged="TimeStartHourDropDownList_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                        :
                                        <asp:DropDownList runat="server" ID="TimeStartMinuteDropDownList" OnSelectedIndexChanged="TimeStartMinuteDropDownList_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="reservationBox">
                            <table class="width">
                                <tr>
                                    <td>
                                        Time End
                                    </td>
                                    <td style="width: 160px">
                                        <asp:DropDownList runat="server" ID="TimeEndHourDropDownList" OnSelectedIndexChanged="TimeEndHourDropDownList_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                        :
                                        <asp:DropDownList runat="server" ID="TimeEndMinuteDropDownList" OnSelectedIndexChanged="TimeEndMinuteDropDownList_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="reservationBox">
                            <table class="width">
                                <tr>
                                    <td>
                                        Floor
                                    </td>
                                    <td style="width: 157px">
                                        <asp:DropDownList runat="server" ID="FloorDropDownList" OnSelectedIndexChanged="FloorDropDownList_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <asp:Label runat="server" ID="WarningLabel" ForeColor="Pink"></asp:Label></div>
                        <div style="font-size: 14px;">
                            <%--<asp:ValidationSummary runat="server" ID="ValidationSummmary" />
--%>
                        </div>
                        <div style="clear: both; padding-left: 6px;">
                            <table>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="BackButton" runat="server" Text="Back" CausesValidation="false"
                                            OnClick="BackImageButton_Click" />
                                    </td>
                                    <td style="padding-left: 10px;">
                                        <asp:ImageButton runat="server" ID="SaveButton" OnClick="SaveImageButton_Click" />
                                    </td>
                                    <td style="padding-left: 10px;">
                                        <asp:ImageButton ID="SearchButton" runat="server" Text="search" CausesValidation="false"
                                            OnClick="SearchImageButton_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <%-- </div>--%>
                <div class="reservationList">
                    <asp:UpdatePanel runat="server" ID="UpdatePanelBookingTable">
                        <ContentTemplate>
                            <div class="avialableTable">
                                <div class="avialableTableList">
                                    <table>
                                        <tr class="bgproductListBox">
                                            <td align="center">
                                                Available<br />
                                                Table
                                            </td>
                                            <tr>
                                                <td>
                                                    <asp:HiddenField runat="server" ID="SelectedTableHiddenField" />
                                                </td>
                                            </tr>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td>
                                                <div class="table-container-scroll">
                                                    <table>
                                                        <asp:Repeater runat="server" ID="AvailableTableRepeater" OnItemDataBound="AvailableTableRepeater_ItemDataBound">
                                                            <ItemTemplate>
                                                                <tr style="height: 12px;" class="repeaterAvailableTable2">
                                                                    <td style="width: 74px;">
                                                                        <asp:CheckBox runat="server" ID="AvailableTableCheckBox" AutoPostBack="true" OnCheckedChanged="AvailableTableCheckBox_CheckedChanged" />
                                                                    </td>
                                                                    <%-- <td style="width: 75px;">
                                                                <asp:Literal runat="server" ID="AvailableTableLiteral"></asp:Literal>
                                                            </td>--%>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="float: left; padding-left: 0px; width: 0px; clear: both;">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="CancelAllImageButton" runat="server" Text="Back" CausesValidation="false"
                                                    OnClick="CancelAllImageButton_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="table-navigation-scroll">
                    <div class="up">
                    </div>
                    <div class="down">
                    </div>
                </div>
                <div class="reservationList2">
                    <div class="reservationListBook" style="width: 540px;">
                        <asp:HiddenField runat="server" ID="CancelledTableHiddenField" />
                        <table>
                            <tr class="bgproductListBox">
                                <td style="width: 430px;" align="center">
                                    Tanggal
                                </td>
                                <td style="width: 250px" align="center">
                                    <b>Floor / No.Table</b>
                                </td>
                                <td style="width: 260px" align="center">
                                    <b>Start Time</b>
                                </td>
                                <td style="width: 240px" align="center">
                                    <b>End Time</b>
                                </td>
                                <td style="width: 1000px" align="center">
                                    <b>Action</b>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <div class="table-container-scroll2">
                                        <table>
                                            <asp:Repeater runat="server" ID="BookedTableRepeater" OnItemDataBound="BookedTableRepeater_ItemDataBound"
                                                OnItemCommand="BookedTableRepeater_ItemCommand">
                                                <ItemTemplate>
                                                    <tr style="height: 12px;" class="repeaterAvailableTable2">
                                                        <td style="width: 300px;" align="center">
                                                            <asp:Literal runat="server" ID="TglLiteral"></asp:Literal>
                                                        </td>
                                                        <td style="width: 300px;" align="center">
                                                            <asp:Literal runat="server" ID="BookedTableLiteral"></asp:Literal>
                                                        </td>
                                                        <td style="width: 240px;" align="center">
                                                            <asp:Literal runat="server" ID="StartTimeLiteral"></asp:Literal>
                                                        </td>
                                                        <td style="width: 240px;" align="center">
                                                            <asp:Literal runat="server" ID="EndTimeLiteral"></asp:Literal>
                                                        </td>
                                                        <td align="center" style="width: 260px" align="center">
                                                            <div style="float: left">
                                                                <asp:ImageButton ID="CancelBookedButton" Class="CancelBookedButton" runat="server"
                                                                    Text="Back" CausesValidation="false" />
                                                            </div>
                                                            <div style="padding-left: 80px;">
                                                                <asp:ImageButton ID="OpenBookedButton" Class="OpenBookedButton" runat="server" Text="Open"
                                                                    CausesValidation="false" /></div>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="table-navigation-scroll2">
                    <div class="up">
                    </div>
                    <div class="down">
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
