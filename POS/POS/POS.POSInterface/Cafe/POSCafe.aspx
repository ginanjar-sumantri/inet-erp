<%@ Page Language="C#" AutoEventWireup="true" CodeFile="POSCafe.aspx.cs" Inherits="POS.POSInterface.Cafe.POSCafe"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<asp:page onpreload="Page_PreLoad" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>POS Cafe</title>
    <link href="../CSS/orange/style.css" media="all" rel="Stylesheet" type="text/css" />

    <script src="../CSS/orange/jquery-1.4.4.min.js" type="text/javascript"></script>

    <script src="../CSS/orange/jquery.corner.js" type="text/javascript"></script>

    <script src="../CSS/orange/style.js" type="text/javascript"></script>

    <script src="../JQuery/jquery.event.drag-1.5.min.js" type="text/javascript"></script>

    <%--<script src="../JQuery/jquery.js" type="text/javascript"></script>

    <script src="../JQuery/jquery.ui.draggable.js" type="text/javascript"></script>

    <script src="../JQuery/jquery.alerts.js" type="text/javascript"></script>

    <link href="../JQuery/jquery.alerts.css" rel="stylesheet" type="text/css" media="screen" />--%>

    <script type="text/javascript" language="javascript">
        var posXBox = new Array () ;
        var posYBox = new Array () ;
        
        function clearSelectedBox ( ) {
            for ( var i = 1 ; i <= <%=_boxCount %> ; i ++ )
                $("#divBox" + i).removeClass ("selectedBox");
        }
        $(function() {
            <%for (int i = 1 ; i <= Convert.ToInt16 ( _boxCount ) ; i ++ ) { %>
                //if ( $("#divBox<%=i.ToString()%>").attr("class") == "divBox available" ) {
                    $("#divBox<%=i.ToString()%>").mousedown(function(event) {
                        clearSelectedBox ();
                        $(this).addClass("selectedBox") ;
                        $("#<%=this.SelectedTableHiddenField.ClientID%>").val(<%=i.ToString()%>);
                        document.forms[0].submit();
                    });
                //}
            <%}%>
        });
    </script>

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
        $('#bodyInternet .container .header .right .date #tanggal').text(Hari[dateObject.getDay()] + ', ' + dateObject.getDate() + ' ' + Bulan[dateObject.getMonth()] + ' ' + (dateObject.getYear() + 1900));
        $('#bodyInternet .container .header .right .date #jam').text(hour + ':' + minute + ':' + second);
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
            $('#bodyCafe .container .header .right .date #tanggal').text(Hari[dateObject.getDay()] + ', ' + dateObject.getDate() + ' ' + Bulan[dateObject.getMonth()] + ' ' + (dateObject.getYear() + 1900));
            $('#bodyCafe .container .header .right .date #jam').text(hour + ':' + minute + ':' + second);
        }
        var x = setInterval("updateJam();", 1000);
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

    <asp:Literal runat="server" ID="CSSLiteral"></asp:Literal>
</head>
<body id="bodyCafe">
    <form id="form1" runat="server">
    <div class="container">
        <div class="header">
            <div class="left">
                <div class="title">
                    <div class="text">
                        POS Cafe</div>
                    <div class="sep">
                    </div>
                </div>
            </div>
            <div class="right">
                <%--<div style="float:left"><img src="../images/logoBizxpress.gif" /></div>--%>
                <div class="date">
                    <div id="tanggal">
                    </div>
                    <div id="jam">
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true">
            </asp:ScriptManager>
            <asp:Timer ID="TimerRefresher" runat="server" Interval="15000" OnTick="TimerRefresher_Tick">
            </asp:Timer>

            <div class="left">
                <asp:HiddenField runat="server" ID="hiddenBoxCount" />
                <asp:HiddenField runat="server" ID="hiddenBoxPosition" />
                <asp:HiddenField runat="server" ID="WarningTypeHiddenField" />
                <asp:HiddenField runat="server" ID="WarningHiddenField" />
                <div class="left-inner-top">
                    <div runat="server" id="PanelLayout" class="divPanel">
                        <div class="flag-available">
                            <div class="flag">
                            </div>
                            Available</div>
                        <div class="flag-reserved">
                            <div class="flag">
                            </div>
                            Reserved</div>
                        <div class="flag-not-available15min">
                            <div class="flag">
                            </div>
                            15 Mins</div>
                        <div class="flag-not-available">
                            <div class="flag">
                            </div>
                            Not Available</div>
                        <asp:Literal runat="server" ID="literalBoxes"></asp:Literal>
                    </div>
                    <asp:HiddenField runat="server" ID="SelectedTableHiddenField" />
                </div>
                <div class="left-inner-bottom">
                    <div class="left-inner-bottom-left">
                        <div class="title">
                            BOOKING LIST</div>
                        <div class="table-header">
                            <table>
                                <tr>
                                    <td class="noteHeader" id="no-member">
                                        Member
                                    </td>
                                    <td class="noteHeader" id="name">
                                        Name
                                    </td>
                                    <td class="noteHeader" id="no-telpon">
                                        Phone
                                    </td>
                                    <td class="noteHeader" id="table">
                                        Floor/Table
                                    </td>
                                    <td class="noteHeader" id="date">
                                        Date
                                    </td>
                                    <td class="noteHeader" id="time">
                                        Start
                                    </td>
                                    <td class="noteHeader" id="to">
                                        To
                                    </td>
                                    <td class="noteHeader" id="operator-name" align="left">
                                        Operator
                                    </td>
                                    <td>
                                        Open
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="table-body table-container-scroll">
                            <table>
                                <asp:Repeater runat="server" ID="BookingListRepeater" OnItemDataBound="BookingListRepeater_ItemDataBound"
                                    OnItemCommand="BookingListRepeater_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="noteContent" id="no-member" align="left">
                                                <asp:Literal runat="server" ID="NoMemberLiteral"></asp:Literal>
                                            </td>
                                            <td class="noteContent" id="name" align="left">
                                                <asp:Literal runat="server" ID="NameLiteral"></asp:Literal>
                                            </td>
                                            <td class="noteContent" id="no-telpon" align="left">
                                                <asp:Literal runat="server" ID="NoTelponLiteral"></asp:Literal>
                                            </td>
                                            <td class="noteContent" id="table" align="Right">
                                                <asp:Literal runat="server" ID="TableLiteral"></asp:Literal>
                                            </td>
                                            <td class="noteContent" id="date" align="right">
                                                <asp:Literal runat="server" ID="DateLiteral"></asp:Literal>
                                            </td>
                                            <td class="noteContent" id="time" align="right">
                                                <asp:Literal runat="server" ID="TimeLiteral"></asp:Literal>
                                            </td>
                                            <td class="noteContent" id="to" align="right">
                                                <asp:Literal runat="server" ID="ToLiteral"></asp:Literal>
                                            </td>
                                            <td class="noteContent" id="operator-name">
                                                <asp:Literal runat="server" ID="OperatorNameLiteral"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="OpenBookedButton" Class="OpenBookedButton" runat="server" Text="Open"
                                                    CausesValidation="false" /></div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </div>
                    <div class="left-inner-bottom-right table-navigation-scroll">
                        <div class="up">
                        </div>
                        <div class="down">
                        </div>
                    </div>
                </div>
            </div>
            <div class="right">
                <div class="right-inner">
                    <div class="title">
                        FLOOR</div>
                    <asp:HiddenField runat="server" ID="currFloorNameHiddenField" />
                    <asp:HiddenField runat="server" ID="currFloorHiddenField" />
                    <asp:HiddenField runat="server" ID="floorPagingPageHiddenField" Value="0" />
                    <div class="lantai">
                        <table style="width: 120px; height: 100px;">
                            <div>
                                <%--<tr>
                                    <td>--%>
                                <asp:Repeater runat="server" ID="FloorButtonRepeater" OnItemDataBound="FloorButtonRepeater_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="button-lantai">
                                            <asp:Button runat="server" ID="FloorButton" OnClick="FloorButton_OnClick" />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <%--</td>
                                </tr>--%>
                            </div>
                        </table>
                    </div>
                    <div class="control">
                        <div class="control1">
                            <asp:ImageButton runat="server" ID="UpFloorPanelButton" OnClick="UpFloorPanelButton_Click" />
                            <asp:ImageButton runat="server" ID="DownFloorPanelButton" OnClick="DownFloorPanelButton_Click" />
                        </div>
                        <div class="control2">
                            <asp:ImageButton runat="server" ID="ReservationImageButton" OnClick="ReservationImageButton_Click" />
                            <%--<asp:ImageButton runat="server" ID="CloseShiftImageButton" ImageUrl="../images/btncloseshift.gif" />--%>
                            <asp:ImageButton runat="server" ID="BackImageButton" OnClick="BackImageButton_Click" />
                            <asp:ImageButton runat="server" class="button-submit" ID="CheckStatusImageButton" />
                            <asp:ImageButton runat="server" ID="JoinTableImageButton" OnClick="JoinTableImageButton_Click" />
                            <asp:ImageButton runat="server" ID="TakeAwayImageButton" OnClick="TakeAwayImageButton_Click" />
                        </div>
                    </div>
                    <div class="table-transfer">
                        <div class="title">
                            Table INFO</div>
                        <div class="left-inner">
                            <div class="from-table">
                                <div class="label">
                                    From Table</div>
                                <div class="input">
                                    <asp:DropDownList runat="server" ID="FromTableDropDownList">
                                    </asp:DropDownList>
                                    <%--<asp:HiddenField ID="FromTableHiddenField" runat="server" />--%>
                                </div>
                            </div>
                            <div class="to-table">
                                <div class="label">
                                    To Table</div>
                                <div class="input2">
                                    <asp:DropDownList runat="server" ID="ToTableDropDownList">
                                    </asp:DropDownList>
                                    <%--<asp:HiddenField ID="ToTableHiddenField" runat="server" />--%>
                                </div>
                            </div>
                        </div>
                        <div class="right-inner">
                            <asp:ImageButton runat="server" ID="TableTransferImageButton" OnClick="TableTransferImageButton_Click" />
                        </div>
                        <div class="left-inner">
                            <div class="from-table">
                                <div class="label">
                                    Table
                                </div>
                                <div class="input">
                                    <asp:DropDownList runat="server" ID="TableDropDownList">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="right-inner2">
                            <asp:ImageButton runat="server" ID="StopImageButton" OnClick="StopImageButton_Click" />
                        </div>
                        <div class="left-inner">
                            <div style="color: #FFFFFF; float: left; font-size: 14px; font-family: Myriad Pro;
                                font-weight: bold; background-color: #C1272D; border-radius: 10px; padding: 3px;">
                                <table>
                                    <tr>
                                        <td>
                                            Reference
                                        </td>
                                        <td style="width: 100px;">
                                            :
                                            <asp:Literal ID="ReferenceLiteral" runat="server"></asp:Literal>
                                        </td>
                                        <%--<td>
                                            <asp:ImageButton runat="server" ID="TableInfoImageButton" OnClick="TableInfoImageButton_Click" />
                                        </td>
--%>
                                    </tr>
                                    <tr>
                                        <td>
                                            Time
                                        </td>
                                        <td>
                                            :
                                            <asp:Literal ID="TimeLiteral" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Occupied
                                        </td>
                                        <td>
                                            :
                                            <asp:Literal ID="OccupiedLiteral" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Customer
                                        </td>
                                        <td>
                                            :
                                            <asp:Literal ID="CustNameLiteral" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            PAX
                                        </td>
                                        <td>
                                            :
                                            <asp:Literal ID="PaxLiteral" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="float: left; margin-top: 12px;">
                                <asp:ImageButton runat="server" ID="TableInfoImageButton" OnClick="TableInfoImageButton_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--<asp:UpdatePanel ID="ButtonUP" runat="server">
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="FromTableDropDownList" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ToTableDropDownList" EventName="SelectedIndexChanged" />
                <%--<asp:AsyncPostBackTrigger ControlID="TableTransferImageButton" EventName="Click" />
                <%--<asp:AsyncPostBackTrigger ControlID="ButtonStopInternet" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ButtonAddTimeInternet" EventName="Click" />
                <%--<asp:AsyncPostBackTrigger ControlID="ToTableDropDownList" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>--%>
        <%--<div style="float:left;width:200px;padding:5px;">
                <div id="BoxAttributeMonitor" class="divBoxAttributeMonitor">Box attribute :</div>
            </div>--%>
    </div>
    <asp:Literal runat="server" ID="JScriptWarning"></asp:Literal>
    </form>
</body>
</html>
