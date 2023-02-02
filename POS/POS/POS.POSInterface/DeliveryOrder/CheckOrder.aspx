<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOrder.aspx.cs" Inherits="DeliveryOrder_CheckOrder" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
    <link href="../CSS/orange/style3.css" media="all" rel="Stylesheet" type="text/css" />

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
<body id="bodyCheckOrder">
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
                CHECK ORDER
            </div>
            <div class="transactionRefBox">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr style="width: 200px;">
                                    <td>
                                        REFERENCE NO.
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td class="CustIDTextBox">
                                        <asp:TextBox ID="ReferenceNoTextBox" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="width: 200px;">
                                    <td>
                                        SEARCH STATUS
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td class="CustIDTextBox">
                                        <asp:DropDownList ID="StatusDDL" runat="server">
                                        </asp:DropDownList>
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
                                        <asp:ImageButton runat="server" ID="AssignRider" OnClick="AssignRider_Click" />
                                    </td>
                                    <td style="padding-left: 0px">
                                        <asp:ImageButton runat="server" ID="NewOrder" OnClick="ClosingRider_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="float: left; padding-left: 10px; width: 67%;">
            <div class="LabelDivisi">
                DELIVERY STATUS
            </div>
            <div class="productListBoxLeft">
                <div class="productListBox">
                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                        <tr class="bgproductListBox">
                            <td class="no" align="center">
                                <b>NO</b>
                            </td>
                            <td class="reference" align="center">
                                <b>REFERENCE</b>
                            </td>
                            <td class="date" align="center">
                                <b>DATE/TIME</b>
                            </td>
                            <td class="User" align="center">
                                <b>NAME</b>
                            </td>
                            <td class="divisi" align="center">
                                <b>STATUS</b>
                            </td>
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
                                    <td class="no" align="center">
                                        <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                    </td>
                                    <td class="reference">
                                        <asp:Literal runat="server" ID="ReferenceNoLiteral"></asp:Literal>
                                    </td>
                                    <td class="date" align="center">
                                        <asp:Literal runat="server" ID="DatetimeLiteral"></asp:Literal>
                                    </td>
                                    <td class="User">
                                        <asp:Literal runat="server" ID="NameLiteral"></asp:Literal>
                                    </td>
                                    <td class="divisi" align="center">
                                        <asp:Literal runat="server" ID="StatusLiteral"></asp:Literal>
                                    </td>
                                    <td class="action" align="center">
                                        <asp:ImageButton runat="server" ID="ViewButton" class="ViewButton" />
                                        <asp:ImageButton runat="server" ID="UpdateButton" class="UpdateOpenButton" />
                                        <asp:ImageButton runat="server" ID="ChangeButton" class="ActionButton" />
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
            <div class="productListBoxClear">
            </div>
        </div>
        <asp:Panel ID="DetailCustomerInfoPanel" runat="server">
            <div style="float: left; padding-left: 0px; width: 26%;">
                <div class="LabelDivisi">
                    DETAIL CUSTOMER INFO
                </div>
                <div class="detailCustomerInfo">
                    <asp:HiddenField ID="ReferenceNoHiddenField" runat="server" />
                    <asp:HiddenField ID="CustomerCodeHiddenField" runat="server" />
                    <asp:HiddenField ID="TransNumberHiddenField" runat="server" />
                    <asp:HiddenField ID="TransTypeHiddenField" runat="server" />
                    <table class="detailCustInfo">
                        <tr>
                            <td valign="top" style="width: 80px;">
                                ADDRESS 1
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td>
                                <asp:Label ID="Address1Label" runat="server" Height="100px" Width="200px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                ADDRESS 2
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td>
                                <asp:Label ID="Address2Label" runat="server" Height="100px" Width="200px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                CITY
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:Label ID="CityLabel" runat="server" Width="200px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                PHONE
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:Label ID="PhoneLabel" runat="server" Width="200px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="ChangePanel" runat="server">
            <div style="float: left; padding-left: 0px; width: 26%;">
                <div class="LabelDivisi">
                    <asp:Literal ID="TitleChangeLiteral" runat="server"> CHANGE PAYMENT </asp:Literal>
                </div>
                <div class="buttonPay">
                    <table>
                        <tr>
                            <td valign="top" style="width: 80px;">
                                <asp:ImageButton ID="CashImageButton" runat="server" OnClick="CashImageButton_Click"
                                    CssClass="CashImageButton" />
                            </td>
                            <td valign="top" style="width: 80px;">
                                <asp:ImageButton ID="VoucherImageButton" runat="server" OnClick="VoucherImageButton_Click"
                                    CssClass="VoucherImageButton" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="width: 80px;">
                                <asp:ImageButton ID="DebitImageButton" runat="server" OnClick="DebitImageButton_Click"
                                    CssClass="DebitImageButton" />
                            </td>
                            <td valign="top" style="width: 80px;">
                                <asp:ImageButton ID="CreditImageButton" runat="server" OnClick="CreditImageButton_Click"
                                    CssClass="CreditImageButton" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="LabelDivisi">
                    REPRINT
                </div>
                <div style="background-color: #F6891F; border-radius: 10px; width: 314px;">
                    <table>
                        <tr>
                            <td valign="top" style="width: 80px;">
                                <asp:ImageButton ID="PrintCustomerImageButton" runat="server" OnClick="PrintCustomerImageButton_Click"
                                    CssClass="PrintToCustomer" />
                            </td>
                            <%--</tr>
                        <tr>--%>
                            <td valign="top" style="width: 80px;">
                                <asp:ImageButton ID="PrintKitchenImageButton" runat="server" OnClick="PrintKitchenImageButton_Click"
                                    CssClass="PrintToKitchen" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
        <div style="float: left; padding-left: 10px; width: 67%;">
            <div class="LabelDivisi">
                DETAIL ITEM
            </div>
            <asp:Panel ID="DetailListPanel" runat="server">
                <div class="productDetailListBox">
                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                        <tr class="bgproductListBox">
                            <td class="no" align="center">
                                <b>NO</b>
                            </td>
                            <td class="transtype" align="center">
                                <b>DIVISI</b>
                            </td>
                            <td class="transnmbr" align="center">
                                <b>TRANS</b>
                            </td>
                            <td class="productCode" align="center">
                                <b>PROCODE</b>
                            </td>
                            <td class="productName" align="center">
                                <b>PRODUCT NAME</b>
                            </td>
                            <td class="qty" align="center">
                                <b>QTY</b>
                            </td>
                            <td class="action" align="center">
                                <b>ACTION</b>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="table-container-scroll2">
                    <table class="dragger" cellpadding="3" cellspacing="1" width="0" border="0">
                        <asp:Repeater ID="DetailItemRepeater" runat="server" OnItemDataBound="DetailItemRepeater_ItemDataBound"
                            OnItemCommand="DetailItemRepeater_ItemCommand">
                            <ItemTemplate>
                                <tr id="RepeaterTemplate" runat="server">
                                    <td class="no" align="center">
                                        <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                    </td>
                                    <td class="transtype" align="center">
                                        <asp:Literal runat="server" ID="TransTypeLiteral"></asp:Literal>
                                    </td>
                                    <td class="transnmbr" align="center">
                                        <asp:Literal runat="server" ID="TransNmbrLiteral"></asp:Literal>
                                    </td>
                                    <td class="productCode">
                                        <asp:Literal runat="server" ID="ProductCodeLiteral"></asp:Literal>
                                    </td>
                                    <td class="productName" align="center">
                                        <asp:Literal runat="server" ID="ProductNameLiteral"></asp:Literal>
                                    </td>
                                    <td class="qty">
                                        <asp:Literal runat="server" ID="QtyLiteral"></asp:Literal>
                                    </td>
                                    <td class="action" align="right">
                                        <asp:ImageButton runat="server" ID="EditButton" class="EditButton" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <div class="table-navigation-scroll2">
                    <div class="up">
                    </div>
                    <div class="down">
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div style="float: left; padding-left: 0px; width: 31.5%;">
            <div class="LabelDivisi">
                DETAIL DELIVERY LOG
            </div>
            <asp:Panel ID="DeliveryLogPanel" runat="server">
                <div class="productListBoxLeft3">
                    <div class="productListBox3">
                        <table cellpadding="3" cellspacing="1" width="0" border="0">
                            <tr class="bgproductListBox">
                                <td class="no" align="center">
                                    <b>NO</b>
                                </td>
                                <td class="status" align="center">
                                    <b>STATUS</b>
                                </td>
                                <td class="time" align="center">
                                    <b>TIME</b>
                                </td>
                                <td class="user" align="center">
                                    <b>USER</b>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="productListBoxRepeater3">
                        <table class="dragger" cellpadding="3" cellspacing="1" width="0" border="0">
                            <asp:Repeater ID="DeliveryLogRepeater" runat="server" OnItemDataBound="DeliveryLogRepeater_ItemDataBound">
                                <ItemTemplate>
                                    <tr id="RepeaterTemplate" runat="server">
                                        <td class="no" align="center">
                                            <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                        </td>
                                        <td class="status">
                                            <asp:Literal runat="server" ID="StatusLiteral"></asp:Literal>
                                        </td>
                                        <td class="time" align="center">
                                            <asp:Literal runat="server" ID="TimeNameLiteral"></asp:Literal>
                                        </td>
                                        <td class="user">
                                            <asp:Literal runat="server" ID="UserLiteral"></asp:Literal>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="PasswordPanel" runat="server">
                <div class="productDetailListBox">
                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                        <tr class="bgproductListBox">
                            <td style="width: 190px; font-size: 16px; text-align: left" class="tahoma_11_white">
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
    <%-- <div id="panelKeyboard">
        <div style="background-color: #cccccc;">
            <div id="KeyboardToggle" style="padding: 3px; cursor: pointer;">
                KEYBOARD</div>
        </div>
        <div id="KeyboardSlider" style="display: none">
            <div id="KeyBoardDivID0" class="KeyboardDiv">
                <div class="KeyboardDiv1">
                    <div class="number" id="nmbr1">
                        <input type="button" value="1" /></div>
                    <div class="number" id="nmbr2">
                        <input type="button" value="2" /></div>
                    <div class="number" id="nmbr3">
                        <input type="button" value="3" /></div>
                    <div class="number" id="nmbr4">
                        <input type="button" value="4" /></div>
                    <div class="number" id="nmbr5">
                        <input type="button" value="5" /></div>
                    <div class="number" id="nmbr6">
                        <input type="button" value="6" /></div>
                    <div class="number" id="nmbr7">
                        <input type="button" value="7" /></div>
                    <div class="number" id="nmbr8">
                        <input type="button" value="8" /></div>
                    <div class="number" id="nmbr9">
                        <input type="button" value="9" /></div>
                    <div class="number" id="nmbr0">
                        <input type="button" value="0" /></div>
                    <div class="number" id="nmbrMin">
                        <input type="button" value="-" /></div>
                    <div class="number" id="nmbrEqual">
                        <input type="button" value="=" /></div>
                    <div class="number" id="nmbrBack">
                        <input type="button" value="BACKSPACE" style="margin-right: 43px; width: 120px;" /></div>
                </div>
                <div class="KeyboardDiv2">
                    <div class="number" id="nmbrQ">
                        <input type="button" value="q" style="margin-left: 0px" /></div>
                    <div class="number" id="nmbrW">
                        <input type="button" value="w" /></div>
                    <div class="number" id="nmbrE">
                        <input type="button" value="e" /></div>
                    <div class="number" id="nmbrR">
                        <input type="button" value="r" /></div>
                    <div class="number" id="nmbrT">
                        <input type="button" value="t" /></div>
                    <div class="number" id="nmbrY">
                        <input type="button" value="y" /></div>
                    <div class="number" id="nmbrU">
                        <input type="button" value="u" /></div>
                    <div class="number" id="nmbrI">
                        <input type="button" value="i" /></div>
                    <div class="number" id="nmbrO">
                        <input type="button" value="o" /></div>
                    <div class="number" id="nmbrP">
                        <input type="button" value="p" /></div>
                    <div class="number" id="nmbrOpenKurva">
                        <input type="button" value="[" /></div>
                    <div class="number" id="nmbrCloseKurva">
                        <input type="button" value="]" style="margin-right: 83px;" /></div>
                </div>
                <div class="KeyboardDiv2">
                    <div class="number" id="nmbrA">
                        <input type="button" value="a" style="margin-left: 34px;" /></div>
                    <div class="number" id="nmbrS">
                        <input type="button" value="s" /></div>
                    <div class="number" id="nmbrD">
                        <input type="button" value="d" /></div>
                    <div class="number" id="nmbrF">
                        <input type="button" value="f" /></div>
                    <div class="number" id="nmbrG">
                        <input type="button" value="g" /></div>
                    <div class="number" id="nmbrH">
                        <input type="button" value="h" /></div>
                    <div class="number" id="nmbrJ">
                        <input type="button" value="j" /></div>
                    <div class="number" id="nmbrK">
                        <input type="button" value="k" /></div>
                    <div class="number" id="nmbrL">
                        <input type="button" value="l" /></div>
                    <div class="number" id="nmbrTitikKoma">
                        <input type="button" value=";" /></div>
                    <div class="number" id="nmbrPetik">
                        <input type="button" value="'" style="margin-right: 73px;" /></div>
                </div>
                <div class="KeyboardDiv2">
                    <div class="number" id="nmbrZ">
                        <input type="button" value="z" style="margin-left: 60px;" /></div>
                    <div class="number" id="nmbrX">
                        <input type="button" value="x" /></div>
                    <div class="number" id="nmbrC">
                        <input type="button" value="c" /></div>
                    <div class="number" id="nmbrV">
                        <input type="button" value="v" /></div>
                    <div class="number" id="nmbrB">
                        <input type="button" value="b" /></div>
                    <div class="number" id="nmbrN">
                        <input type="button" value="n" /></div>
                    <div class="number" id="nmbrM">
                        <input type="button" value="m" /></div>
                    <div class="number" id="nmbrKoma">
                        <input type="button" value="," /></div>
                    <div class="number" id="nmbrTitik">
                        <input type="button" value="." /></div>
                    <div class="number" id="nmbrSlash">
                        <input type="button" value="/" style="margin-right: 79.5px;" /></div>
                </div>
                <div class="KeyboardDiv1">
                    <div class="number" id="nmbrSift">
                        <input type="button" value="^^^^^" style="width: 125px;" /></div>
                    <div class="number" id="nmbrSpace">
                        <input type="button" value="SPACE" style="width: 600px;" /></div>
                    <div class="number" id="nmbrEnter">
                        <input type="button" value="ENTER" style="width: 125px;" /></div>
                </div>
            </div>
            <%--KEYBOARD SIFT--%>
    <%--<div id="KeyBoardDivID1" class="KeyboardDiv" style="display: none">
                <div class="KeyboardDiv1">
                    <div class="number" id="nmbrSeru">
                        <input type="button" value="!" /></div>
                    <div class="number" id="nmbrAdd">
                        <input type="button" value="@" /></div>
                    <div class="number" id="nmbrSharp">
                        <input type="button" value="#" /></div>
                    <div class="number" id="nmbrDolar">
                        <input type="button" value="$" /></div>
                    <div class="number" id="nmbrPersen">
                        <input type="button" value="%" /></div>
                    <div class="number" id="nmbrPangkat">
                        <input type="button" value="^" /></div>
                    <div class="number" id="nmbrAnd">
                        <input type="button" value="&" /></div>
                    <div class="number" id="nmbrBintang">
                        <input type="button" value="*" /></div>
                    <div class="number" id="nmbrBukaKurung">
                        <input type="button" value="(" /></div>
                    <div class="number" id="nmbrTutupKurung">
                        <input type="button" value=")" /></div>
                    <div class="number" id="nmbrUnderCross">
                        <input type="button" value="_" /></div>
                    <div class="number" id="nmbrPlus">
                        <input type="button" value="+" /></div>
                    <div class="number" id="nmbrBack2">
                        <input type="button" value="BACKSPACE" style="width: 120px; margin-right: 43px;" /></div>
                </div>
                <div class="KeyboardDiv2">
                    <div class="number" id="nmbrQ2">
                        <input type="button" value="Q" /></div>
                    <div class="number" id="nmbrW2">
                        <input type="button" value="W" /></div>
                    <div class="number" id="nmbrE2">
                        <input type="button" value="E" /></div>
                    <div class="number" id="nmbrR2">
                        <input type="button" value="R" /></div>
                    <div class="number" id="nmbrT2">
                        <input type="button" value="T" /></div>
                    <div class="number" id="nmbrY2">
                        <input type="button" value="Y" /></div>
                    <div class="number" id="nmbrU2">
                        <input type="button" value="U" /></div>
                    <div class="number" id="nmbrI2">
                        <input type="button" value="I" /></div>
                    <div class="number" id="nmbrO2">
                        <input type="button" value="O" /></div>
                    <div class="number" id="nmbrP2">
                        <input type="button" value="P" /></div>
                    <div class="number" id="nmbrKurvaOpen2">
                        <input type="button" value="{" /></div>
                    <div class="number" id="nmbrKurvaClose2">
                        <input type="button" value="}" style="margin-right: 83px;" /></div>
                </div>
                <div class="KeyboardDiv2">
                    <div class="number" id="nmbrA2">
                        <input type="button" value="A" style="margin-left: 34px;" /></div>
                    <div class="number" id="nmbrS2">
                        <input type="button" value="S" /></div>
                    <div class="number" id="nmbrD2">
                        <input type="button" value="D" /></div>
                    <div class="number" id="nmbrF2">
                        <input type="button" value="F" /></div>
                    <div class="number" id="nmbrG2">
                        <input type="button" value="G" /></div>
                    <div class="number" id="nmbrH2">
                        <input type="button" value="H" /></div>
                    <div class="number" id="nmbrJ2">
                        <input type="button" value="J" /></div>
                    <div class="number" id="nmbrK2">
                        <input type="button" value="K" /></div>
                    <div class="number" id="nmbrL2">
                        <input type="button" value="L" /></div>
                    <div class="number" id="nmbrTitikDua">
                        <input type="button" value=":" /></div>
                    <div class="number" id="nmbrBackSlash">
                        <input type="button" value="\" style="margin-right: 73px;" /></div>
                </div>
                <div class="KeyboardDiv2">
                    <div class="number" id="nmbrZ2">
                        <input type="button" value="Z" style="margin-left: 60px;" /></div>
                    <div class="number" id="nmbrX2">
                        <input type="button" value="X" /></div>
                    <div class="number" id="nmbrC2">
                        <input type="button" value="C" /></div>
                    <div class="number" id="nmbrV2">
                        <input type="button" value="V" /></div>
                    <div class="number" id="nmbrB2">
                        <input type="button" value="B" /></div>
                    <div class="number" id="nmbrN2">
                        <input type="button" value="N" /></div>
                    <div class="number" id="nmbrM2">
                        <input type="button" value="M" /></div>
                    <div class="number" id="nmbrLebihKecil">
                        <input type="button" value="<" /></div>
                    <div class="number" id="nmbrLebihBesar">
                        <input type="button" value=">" /></div>
                    <div class="number" id="nmbrTanya">
                        <input type="button" value="?" style="margin-right: 79.5px;" /></div>
                </div>
                <div class="KeyboardDiv1">
                    <div class="number" id="nmbrSift2">
                        <input type="button" value="^^^^^" style="width: 125px;" /></div>
                    <div class="number" id="nmbrSpace2">
                        <input type="button" value="SPACE" style="width: 600px;" /></div>
                    <div class="number" id="nmbrEnter2">
                        <input type="button" value="ENTER" style="width: 125px;" /></div>
                </div>
            </div>
    </div> </div>--%>
    <div>
        <asp:Panel runat="server" ID="Panel2" Visible="false">
            <table cellpadding="3" cellspacing="0" width="100%" border="0">
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server">
                        </rsweb:ReportViewer>
                        <rsweb:ReportViewer ID="ReportViewer2" runat="server">
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
