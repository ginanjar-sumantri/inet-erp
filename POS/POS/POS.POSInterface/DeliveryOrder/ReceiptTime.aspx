<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReceiptTime.aspx.cs" Inherits="DeliveryOrder_ReceiptTime" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>POS Delivery Order</title>
    <style type="text/css">
        </style>

    <script src="../CSS/orange/jquery-1.5.2.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="../CSS/orange/scrollsync.js"></script>

    <script type="text/javascript" src="../CSS/orange/dragscrollable.js"></script>

    <script src="../JQuery/jquery-ui.min.js" type="text/javascript"></script>

    <script src="../CSS/orange/jquery.corner.js" type="text/javascript"></script>

    <link href="../JQuery/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/orange/style4.css" media="all" rel="Stylesheet" type="text/css" />

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

    <script language="javascript" type="text/javascript">

        $(document).ready(function() {
            $("#dialogHold").draggable();
            setTimeout(function() {
                $("#dialogHold").fadeOut("fast")
            }, 1000);
        });
    </script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            $(".btnInputNumber").click(function() {
                if ($("#currActiveInput").val() != "") {
                    if (this.value == '.') {
                        if (($("#" + $("#currActiveInput").val()).val()).indexOf('.') < 0)
                            $("#" + $("#currActiveInput").val()).val($("#" + $("#currActiveInput").val()).val() + this.value);
                    }
                    else {
                        if ($("#" + $("#currActiveInput").val()).val() == "0") {
                            $("#" + $("#currActiveInput").val()).val(this.value * 1);
                        }
                        else
                            $("#" + $("#currActiveInput").val()).val($("#" + $("#currActiveInput").val()).val() + this.value);
                    }
                }
//                if ($("#currActiveInput2").val() != "") {
//                    if (this.value == '.') {
//                        if (($("#" + $("#currActiveInput2").val()).val()).indexOf('.') < 0)
//                            $("#" + $("#currActiveInput2").val()).val($("#" + $("#currActiveInput2").val()).val() + this.value);
//                    }
//                    else {
//                        if ($("#" + $("#currActiveInput2").val()).val() == "0") {
//                            $("#" + $("#currActiveInput2").val()).val(this.value * 1);
//                        }
//                        else
//                            $("#" + $("#currActiveInput2").val()).val($("#" + $("#currActiveInput2").val()).val() + this.value);
//                    }
//                }
            });
        });
    </script>

    <asp:Literal ID="javaScriptDeclaration" runat="server"></asp:Literal>
    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>
</head>
<body id="bodyReceiptTime">
    <form id="form1" runat="server">
    <div class="container">
        <div class="header">
            <div class="left">
                <div class="title">
                    <div class="text">
                        POS DELIVERY ORDER</div>
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
        <div class="containerBox">
            <div class="totalItemBox">
                RECEIPT TIME
                <div class="transactionRefBox">
                    <table>
                        <tr>
                            <td style="width: 250px; text-align: right;">
                                Customer Receipt Time
                            </td>
                            <td class="ReferenceNoTextBox">
                                <input type="hidden" id="currActiveInput" value="0" />
                                <asp:TextBox ID="HourTextBox" runat="server" Height="28px"></asp:TextBox>
                            </td>
                            <td style="width: 10px; text-align: middle;">
                                :
                            </td>
                            <td class="ReferenceNoTextBox">
                                <asp:TextBox ID="MinuteTextBox" runat="server" Height="28px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="ReferenceNoHiddenField" runat="server" />
                </div>
                <div class="buttonTransaction">
                    <table>
                        <tr>
                            <td>
                                <asp:ImageButton ID="SaveImageButton" runat="server" OnClick="SaveImageButton_Click" style="margin-bottom:20px;"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="CancelImageButton" runat="server" OnClick="CancelImageButton_Click" style="margin-bottom:20px;" />
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <asp:ImageButton ID="BackImageButton" runat="server" OnClick="BackImageButton_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="buttonNumber">
                    <div class="number" id="number1">
                        <input type="button" value="1" class="btnInputNumber" /></div>
                    <div class="number" id="number2">
                        <input type="button" value="2" class="btnInputNumber" /></div>
                    <div class="number" id="number3">
                        <input type="button" value="3" class="btnInputNumber" /></div>
                    <div class="number" id="number4">
                        <input type="button" value="4" class="btnInputNumber" /></div>
                    <div class="number" id="number5">
                        <input type="button" value="5" class="btnInputNumber" /></div>
                    <div class="number" id="number6">
                        <input type="button" value="6" class="btnInputNumber" /></div>
                    <div class="number" id="number7">
                        <input type="button" value="7" class="btnInputNumber" /></div>
                    <div class="number" id="number8">
                        <input type="button" value="8" class="btnInputNumber" /></div>
                    <div class="number" id="number9">
                        <input type="button" value="9" class="btnInputNumber" /></div>
                    <div class="number0" id="number0">
                        <input type="button" value="0" class="btnInputNumber" /></div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
