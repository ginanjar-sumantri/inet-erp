<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CloseRider.aspx.cs" Inherits="DeliveryOrder_CloseRaider" %>

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
        function CancelPaid() {
            var _result = false;

            if (confirm("Are you sure want to cancel this order ?") == true) {
                _result = true;
            }
            else {
                _result = false;
            }

            return _result
        }

        function editQty(_nourut, _code, _nourutHidden, _qtyTextBox, _barcodeTextBox) {
            nourutHidden = document.getElementById(_nourutHidden);
            qtyTextBox = document.getElementById(_qtyTextBox);
            barcodeTextBox = document.getElementById(_barcodeTextBox);

            nourutHidden.value = _nourut;

            barcodeTextBox.value = _code;
            barcodeTextBox.readOnly = true;
            barcodeTextBox.style.backgroundColor = '#CCCCCC';

            qtyTextBox.focus();
        }

        function deleteItem(x, y, z) {
            _boughtItems = document.getElementById(x).value.split("^");
            _newBoughtItems = "";
            _ctrItems = 1;
            for (i = 0; i < _boughtItems.length; i++) {
                _boughtItem = _boughtItems[i].split('|');
                if (_boughtItem[0] != y) {
                    if (_newBoughtItems == "") {
                        _newBoughtItems = _ctrItems + "|";
                    } else {
                        _newBoughtItems += "^" + _ctrItems + "|";
                    }
                    _newBoughtItems += _boughtItem[1] + "|" + _boughtItem[2] + "|" + _boughtItem[3] + "|" + _boughtItem[4] + "|" + _boughtItem[5] + "|" + _boughtItem[6];
                    _ctrItems++;
                }
            }
            document.getElementById(x).value = _newBoughtItems;
            document.getElementById(z).value = document.getElementById(z).value - 1;
            document.forms[0].submit();
        }
    </script>

    <asp:Literal ID="javaScriptDeclaration" runat="server"></asp:Literal>
    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>
</head>
<body id="bodyCloseRider">
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
        <div class="containerBox" style="clear: both">
            <div class="totalItemBox">
                CLOSE RIDER
            </div>
            <div class="transactionRefBox">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr style="width: 200px;">
                                    <td>
                                        REFERENCE NO
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td class="CustIDTextBox">
                                        <asp:TextBox ID="ReferenceNoTextBox" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td style="padding-left: 5px">
                                        <asp:ImageButton ID="SearchButton" OnClick="btnSearch_Click" runat="server" Text="search"
                                            CausesValidation="false" />
                                    </td>
                                    <td style="padding-left: 0px">
                                        <asp:ImageButton runat="server" ID="BackButton" OnClick="BackButton_Click" />
                                    </td>
                                    <td style="padding-left: 0px">
                                        <asp:ImageButton runat="server" ID="ResetButton" OnClick="btnReset_Click" />
                                    </td>
                                    <td style="padding-left: 0px">
                                        <asp:ImageButton runat="server" ID="AssignButton" OnClick="AssignRider_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="float: left; padding-left: 10px; width: 68%;">
            <div class="LabelDivisi">
                PROCESS DELIVERY
                <asp:HiddenField ID="ReferenceNoHiddenField" runat="server" />
                <%--<asp:HiddenField ID="CustomerCodeHiddenField" runat="server" />
                <asp:HiddenField ID="TransNumberHiddenField" runat="server" />
                <asp:HiddenField ID="TransTypeHiddenField" runat="server" />--%>
            </div>
            <div class="productListBoxLeft">
                <div class="productListBox">
                    <table cellpadding="1" cellspacing="1" width="0" border="0">
                        <tr class="bgproductListBox">
                            <td class="reference" align="center">
                                <b>REFERENCE</b>
                            </td>
                            <td class="driver" align="center">
                                <b>DRIVER</b>
                            </td>
                            <td class="date" align="center">
                                <b>DATE/TIME</b>
                            </td>
                            <%--<td class="timeout" align="center">
                                <b>TIMEOUT</b>
                            </td>--%>
                            <td class="action" align="center">
                                <b>ACTION</b>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="productListBoxRepeater table-container-scroll">
                    <table class="dragger" cellpadding="3" cellspacing="1" width="0" border="0">
                        <asp:Repeater ID="DeliveryListRepeater" runat="server" OnItemDataBound="DeliveryListRepeater_ItemDataBound"
                            OnItemCommand="DeliveryListRepeater_ItemCommand">
                            <ItemTemplate>
                                <tr id="RepeaterTemplate" runat="server">
                                    <td class="reference">
                                        <asp:Literal runat="server" ID="ReferenceNoLiteral"></asp:Literal>
                                    </td>
                                    <td class="driver">
                                        <asp:Literal runat="server" ID="DriverLiteral"></asp:Literal>
                                    </td>
                                    <td class="date" align="center">
                                        <asp:Literal runat="server" ID="DatetimeLiteral"></asp:Literal>
                                    </td>
                                    <%--<td class="timeout" align="center">
                                        <asp:Literal runat="server" ID="TimeOutLiteral"></asp:Literal>
                                    </td>--%>
                                    <td class="action" align="center">
                                        <%--<asp:ImageButton runat="server" ID="DoneButton" class="doneButton" />--%>
                                        <asp:ImageButton runat="server" ID="ReceiptButton" class="receiptTimeButton" />
                                        <asp:ImageButton runat="server" ID="PaidButton" class="paidButton" />
                                        <asp:ImageButton runat="server" ID="CancelButton" class="cancelButton" />
                                        <div class="clearClass">
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
            <div class="productListBoxRight table-navigation-scroll">
                <div class="up">
                </div>
                <div class="down">
                </div>
            </div>
            <%--<div class="productListBoxClear">
            </div>--%>
        </div>
        <div style="float: left; padding-left: 0px; width: 29%;">
            <div class="LabelDivisi">
                REASON
            </div>
            <asp:Panel ID="ReasonListPanelCR" runat="server">
                <div class="reasonListCR">
                    <table cellpadding="1" cellspacing="1" width="0" border="0">
                        <tr class="bgproductListBox">
                            <td style="width: 40px" class="tahoma_11_white" align="center">
                                <b>Pick</b>
                            </td>
                            <td style="width: 345px" class="tahoma_11_white" align="center">
                                <b>Reason</b>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="table-container-scroll3">
                    <table class="dragger3">
                        <asp:Repeater ID="ReasonListRepeater" runat="server" OnItemCommand="ReasonListRepeater_ItemCommand"
                            OnItemDataBound="ReasonListRepeater_ItemDataBound">
                            <ItemTemplate>
                                <tr style="width: 40px" class="tahoma_11_white" id="RepeaterTemplate" runat="server">
                                    <td align="center" class="pickReasonButton">
                                        <asp:ImageButton ID="PickReasonImageButton" runat="server" />
                                    </td>
                                    <td style="width: 345px" class="tahoma_11_white">
                                        <asp:Literal runat="server" ID="ReasonLiteral"></asp:Literal>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </asp:Panel>
            <asp:Panel ID="PasswordPanel" runat="server">
                <div class="reasonListCR">
                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                        <tr class="bgproductListBox">
                            <td style="width: 190px; font-size: 15px; text-align: left">
                                <b>Password Required : </b>
                            </td>
                            <td>
                                <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:ImageButton ID="OKButton" runat="server" Text="OK" OnClick="OKButton_Click" />
                                <asp:ImageButton ID="CancellButton" runat="server" Text="OK" OnClick="CancellButton_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </div>
    </div>
    </form>
</body>
</html>
