<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JoinTable.aspx.cs" Inherits="POS.POSInterface.Cafe.JoinTable"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>POS Join Table</title>
    <link href="../CSS/orange/style2.css" media="all" rel="Stylesheet" type="text/css" />

    <script src="../CSS/orange/jquery-1.5.2.min.js" type="text/javascript"></script>

    <script src="../CSS/orange/jquery.corner.js" type="text/javascript"></script>

    <script src="../CSS/orange/style.js" type="text/javascript"></script>

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

    <%--<style type="text/css"> </style>
    <script type="text/javascript" src="../CSS/orange/scrollsync.js"></script>

    <script type="text/javascript" src="../CSS/orange/dragscrollable.js"></script>

    <script src="../JQuery/jquery-ui.min.js" type="text/javascript"></script>

    
    <link href="../JQuery/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            $("#dialogHold").draggable();
            setTimeout(function() {
                $("#dialogHold").fadeOut("fast")
            }, 1000);
        });
    </script>
    <asp:Literal ID="javaScriptDeclaration" runat="server"></asp:Literal>
    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>--%>
</head>
<body id="bodyJoinTable">
    <form id="form1" runat="server">
    <div class="container">
        <div class="header">
            <div class="left">
                <div class="title">
                    <div class="text">
                        Pos Join Table
                    </div>
                    <div class="sep">
                    </div>
                </div>
            </div>
            <div style="float: left; color: Red; margin-left: 30px;">
                <asp:Label ID="WarningLabel" runat="server"></asp:Label>
            </div>
            <div class="right">
                <div class="date">
                    <div id="tanggal">
                    </div>
                    <div id="jam">
                    </div>
                </div>
            </div>
        </div>
        <div class="containerBox" style="clear: both">
            <div class="totalItemBox">
                JOIN TABLE
            </div>
            <div class="transactionRefBox">
                <table>
                    <tr>
                        <td style="padding-left: 400px">
                            <asp:ImageButton ID="SaveImageButton" runat="server" OnClick="SaveImageButton_Click" />
                        </td>
                        <td style="padding-left: 5px">
                            <asp:ImageButton ID="BackImageButton" runat="server" OnClick="BackImageButton_Click" />
                        </td>
                        <td style="padding-left: 5px">
                            <asp:ImageButton ID="CancelImageButton" runat="server" OnClick="CancelImageButton_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="content">
            <div class="top" style="border-radius: 10px;">
                <asp:HiddenField runat="server" ID="currFloorNameHiddenField" />
                <asp:HiddenField runat="server" ID="currFloorHiddenField" />
                <asp:HiddenField runat="server" ID="TemporaryHiddenField" />
                <asp:HiddenField runat="server" ID="floorPagingPageHiddenField" Value="0" />
                <div class="lantai">
                    <table>
                        <div>
                            <asp:Repeater runat="server" ID="FloorButtonRepeater" OnItemDataBound="FloorButtonRepeater_ItemDataBound">
                                <ItemTemplate>
                                    <div class="button-lantai">
                                        <asp:Button runat="server" ID="FloorButton" OnClick="Floorbutton_onclick" />
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </table>
                </div>
                <table class="tableRepater">
                    <tr valign="top">
                        <td>
                            <table>
                                <tr>
                                    <td valign="top">
                                        <table>
                                            <tr>
                                                <td>
                                                    <table cellpadding="3" cellspacing="0" class="fixedTable">
                                                        <tr class="toptable">
                                                            <td align="center" style="width: 50px;">
                                                                No
                                                            </td>
                                                            <td align="center" style="width: 50px;">
                                                                Floor
                                                            </td>
                                                            <td align="center" style="width: 150px">
                                                                Table
                                                            </td>
                                                            <td align="center" style="width: 85px">
                                                                Pick
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td class="ContentTable">
                                                    <div class="table-container-scroll">
                                                        <table>
                                                            <asp:Repeater ID="ListRepeater" runat="server" OnItemDataBound="ListRepeater_ItemDataBound"
                                                                OnItemCommand="ListRepeater_ItemCommand">
                                                                <ItemTemplate>
                                                                    <tr id="RepeaterItemTemplate" runat="server" class="repeater" align="left">
                                                                        <td align="center" style="width: 50px">
                                                                            <asp:Literal ID="NoLiteral" runat="server"></asp:Literal>
                                                                        </td>
                                                                        <td align="center" style="width: 50px">
                                                                            <asp:Literal ID="FloorLiteral" runat="server"></asp:Literal>
                                                                        </td>
                                                                        <td align="center" style="width: 150px">
                                                                            <asp:Literal ID="TableLiteral" runat="server"></asp:Literal>
                                                                        </td>
                                                                        <td align="center" style="width: 85px" class="pickbutton">
                                                                            <asp:ImageButton ID="PickImageButton" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </table>
                                                    </div>
                                                    <asp:HiddenField ID="List1HiddenField" runat="server" />
                                                </td>
                                                <td>
                                                    <div class="table-navigation-scroll">
                                                        <div class="up">
                                                        </div>
                                                        <div class="down">
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top" style="padding-left:30px;">
                                        <table>
                                            <tr>
                                                <td>
                                                    <table cellpadding="3" cellspacing="0" class="fixedTable">
                                                        <tr class="toptable">
                                                            <td align="center" style="width: 85px">
                                                                Back
                                                            </td>
                                                            <td align="center" style="width: 50px">
                                                                Floor
                                                            </td>
                                                            <td align="center" style="width: 150px">
                                                                Table
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentTable2">
                                                    <div class="table-container-scroll2">
                                                        <table>
                                                            <asp:Repeater ID="ListRepeater2" runat="server" OnItemDataBound="ListRepeater2_ItemDataBound"
                                                                OnItemCommand="ListRepeater2_ItemCommand">
                                                                <ItemTemplate>
                                                                    <tr id="RepeaterItemTemplate2" runat="server" class="repeater">
                                                                        <td align="center" class="resetpickbutton">
                                                                            <asp:ImageButton ID="ResetPickImageButton" runat="server" />
                                                                        </td>
                                                                        <td style="width: 50px" align="center">
                                                                            <asp:Literal ID="FloorLiteral" runat="server"></asp:Literal>
                                                                        </td>
                                                                        <td style="width: 150px" align="center">
                                                                            <asp:Literal ID="TableLiteral" runat="server"></asp:Literal>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </table>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="table-navigation-scroll2">
                                                        <div class="up">
                                                        </div>
                                                        <div class="down">
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:HiddenField ID="List2HiddenField" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
