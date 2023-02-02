<%@ Page Language="C#" AutoEventWireup="true" CodeFile="POSRetail.aspx.cs" Inherits="POS.POSInterface.Retail.POSRetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>POS - Stationary</title>

    <script src="../CSS/orange/jquery-1.5.2.min.js" type="text/javascript"></script>

    <script src="../JQuery/jquery-ui.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="../CSS/orange/scrollsync.js"></script>

    <script type="text/javascript" src="../CSS/orange/dragscrollable.js"></script>

    <script src="../CSS/orange/jquery.corner.js" type="text/javascript"></script>

    <link href="../JQuery/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/orange/style.css" media="all" rel="Stylesheet" type="text/css" />

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
            $('.table-container-scroll').dragscrollable({ dragSelector: '.dragger td', acceptPropagatedEvent: false });
            $("#dialogHold").draggable();
            setTimeout(function() {
                $("#dialogHold").fadeOut("fast")
            }, 1000);
        });
    </script>

    <script language="javascript" type="text/javascript">
        function editQty(_nourut, _code, _nourutHidden, _qtyTextBox, _barcodeTextBox) {
            nourutHidden = document.getElementById(_nourutHidden);
            qtyTextBox = document.getElementById(_qtyTextBox);
            barcodeTextBox = document.getElementById(_barcodeTextBox);

            nourutHidden.value = _nourut;

            barcodeTextBox.value = _code;
            barcodeTextBox.readOnly = true;
            barcodeTextBox.style.color = '#808080';

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
                    _newBoughtItems += _boughtItem[1] + "|" + _boughtItem[2] + "|" + _boughtItem[3] + "|" + _boughtItem[4] + "|" + _boughtItem[5] + "|" + _boughtItem[6] + "|" + _boughtItem[7] + "|" + _boughtItem[8] + "|" + _boughtItem[9] + "|" + _boughtItem[10];
                    _ctrItems++;
                }
            }
            document.getElementById(x).value = _newBoughtItems;
            document.getElementById(z).value = document.getElementById(z).value - 1;
            document.forms[0].submit();
        }
    </script>
    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>
</head>
<body id="bodyPosRetail">
    <form id="form1" runat="server">
    <%--<asp:Panel runat="server" ID="RetailPanel" DefaultButton="btnOKPanelNumber">--%>
    <div class="container">
        <div class="header">
            <div class="left">
                <div class="title">
                    <div class="text">
                        POS Stationary</div>
                    <div class="sep">
                    </div>
                </div>
            </div>
            <div class="warning">
                <asp:Label ID="WarningLabel" runat="server" ForeColor="Red"></asp:Label>
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
        <asp:Panel ID="PasswordPanel" runat="server">
            <div>
                <table cellpadding="3" cellspacing="1" width="0" border="0">
                    <tr>
                        <td style="width: 190px; font-size: 16px; text-align: left; color: White;">
                            <b>Password Required : </b>
                        </td>
                        <td>
                            <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="OKButton" runat="server" Text="OK" OnClick="OKButton_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="FormPanel">
            <div class="content">
                <div class="top">
                    <div class="top-left">
                        <div class="trans-ref">
                            <div class="input">
                                <asp:TextBox ID="TransNmbrTextBox" runat="server"></asp:TextBox>
                                <asp:HiddenField ID="IsEditedHiddenField" runat="server" />
                                <asp:HiddenField ID="ReferenceNoHiddenField" runat="server" />
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                        <div class="date">
                            <div class="label">
                                Date</div>
                            <div class="sep">
                                :</div>
                            <div class="input">
                                <asp:Label ID="DateLabel" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                        <div class="currency">
                            <div class="label">
                                Currency</div>
                            <div class="sep">
                                :</div>
                            <div class="input">
                                <asp:Label ID="CurrencyLabel" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                        <div class="operator">
                            <div class="label">
                                Operator</div>
                            <div class="sep">
                                :</div>
                            <div class="input">
                                <asp:Label ID="CashierLabel" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="top-center">
                        <div class="top-center-left">
                            <div class="ref-no">
                                <div class="label">
                                    REF. Number</div>
                                <div class="sep">
                                    :</div>
                                <div class="input">
                                    <asp:TextBox ID="ReferenceNoTextBox" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="clear">
                            </div>
                            <div class="member-code">
                                <div class="label">
                                    Member Barcode</div>
                                <div class="sep">
                                    :</div>
                                <div class="input">
                                    <asp:TextBox ID="MemberBarcodeTextBox" runat="server" onKeyPress="return event.keyCode!=13"
                                        OnTextChanged="MemberBarcodeTextBox_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="clear">
                            </div>
                        </div>
                        <div class="top-center-center">
                            <div class="cust-name">
                                <div class="label">
                                    Cust. Name</div>
                                <div class="sep">
                                    :</div>
                                <div class="input">
                                    <asp:TextBox ID="CustNameTextBox" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="clear">
                            </div>
                            <div class="cust-phone">
                                <div class="label">
                                    Cust. Phone</div>
                                <div class="sep">
                                    :</div>
                                <div class="input">
                                    <asp:TextBox ID="CustPhoneTextBox" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="clear">
                            </div>
                        </div>
                        <div class="top-center-right">
                            <div class="button">
                                <asp:Button ID="NewMemberButton" ToolTip="NewMember" runat="server" 
                                    onclick="NewMemberButton_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="top-right">
                        <div class="label">
                            TOTAL (Rp) :</div>
                        <div class="input">
                            <asp:Label ID="GrandTotalLabel" runat="server" class="fontNumberDisplay"></asp:Label>
                        </div>
                        <div class="label">
                            Total Item :
                            <asp:Label ID="TotalItemLabel" runat="server" class="fontNumberDisplay"></asp:Label>
                        </div>
                    </div>
                    <div class="top-clear">
                    </div>
                </div>
                <div class="middle">
                    <div class="middle-left">
                        <div class="productListBox">
                            <div class="table-header">
                                <table class="posTHeaderScroll" cellspacing="0" cellpadding="3">
                                    <tr>
                                        <td class="no">
                                            NO
                                        </td>
                                        <td class="product-code">
                                            Product Code
                                        </td>
                                        <td class="desc">
                                            Description
                                        </td>
                                        <td class="qty" colspan="2">
                                            Qty
                                        </td>
                                        <td class="disc">
                                            Disc
                                        </td>
                                        <td class="price">
                                            Price
                                        </td>
                                        <td class="line-total">
                                            Line Total
                                        </td>
                                        <td class="cancel">
                                            Cancel
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="table-body table-container-scroll">
                                <table class="posTBodyScroll dragger" cellspacing="0" cellpadding="3">
                                    <asp:Literal ID="perulanganDataDibeli" runat="server"></asp:Literal>
                                    <asp:Panel ID="panelAddRow" runat="server">
                                        <tr valign="top">
                                            <td class="no">
                                            </td>
                                            <td class="product-code">
                                                <asp:Label ID="ProductCodeLabel" runat="server"></asp:Label>
                                            </td>
                                            <td class="desc">
                                                <asp:Label ID="DescriptionLabel" runat="server"></asp:Label>
                                            </td>
                                            <td class="qty2">
                                                <asp:Label ID="QtyLabel" runat="server"></asp:Label>
                                            </td>
                                            <td class="qty3">
                                                <asp:ImageButton ID="EditButton" class="EditButton" runat="server" Text="" ImageUrl="" />
                                            </td>
                                            <td class="disc">
                                                <asp:HiddenField ID="discHiddenField" Value="0" runat="server"></asp:HiddenField>
                                                <asp:Label ID="DiscLabel" runat="server"></asp:Label>
                                            </td>
                                            <td class="price">
                                                <asp:HiddenField ID="priceHiddenField" Value="0" runat="server"></asp:HiddenField>
                                                <asp:Label ID="PriceLabel" runat="server"></asp:Label>
                                            </td>
                                            <td class="line-total">
                                                <asp:HiddenField ID="lineTotalHiddenField" Value="0" runat="server"></asp:HiddenField>
                                                <asp:Label ID="LineTotalLabel" runat="server"></asp:Label>
                                            </td>
                                            <td class="cancel">
                                                <asp:ImageButton ID="CancelButton" class="CancelButton" runat="server" Text="" ImageUrl="" />
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <tr runat="server" visible="false" id="bottomSpanNota">
                                        <td colspan="8">
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="boughtItems" runat="server" />
                                <asp:HiddenField ID="itemCount" Value="0" runat="server" />
                            </div>
                        </div>
                        <div class="productListBox2 table-navigation-scroll">
                            <div class="up">
                            </div>
                            <div class="down">
                            </div>
                        </div>
                    </div>
                    <div class="middle-right">
                        <div class="productImageBox">
                            <div class="top">
                                <div class="left">
                                    <div class="label">
                                        Barcode</div>
                                    <div class="input">
                                        <asp:TextBox ID="BarcodeTextBox" runat="server" OnTextChanged="BarcodeTextBox_TextChanged"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="right">
                                    <div class="label">
                                        Qty</div>
                                    <div class="input">
                                        <asp:TextBox ID="QtyTextBox" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <asp:HiddenField ID="NoUrutHiddenField" runat="server" />
                            </div>
                            <div class="mid">
                                <asp:TextBox ID="ProductCodeTextBox" runat="server" OnTextChanged="ProductCodeTextBox_TextChanged"></asp:TextBox>
                            </div>
                            <div class="bottom">
                                <asp:Image ID="ProductImage" Width="292" Height="213" runat="server" />
                            </div>
                            <div class="bottom2">
                                <asp:Label ID="ProductNameLabel" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="middle-clear">
                    </div>
                </div>
                <div class="bottom">
                    <div class="bottom-left">
                        <div class="discount">
                            <div class="label">
                                Discount Product</div>
                            <div class="value">
                                <asp:Label ID="DiscProductLabel" runat="server"></asp:Label>
                                <asp:HiddenField ID="DiscountTotalHiddenField" runat="server" />
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                        <div class="subtotal">
                            <div class="label">
                                Sub Total</div>
                            <div class="value">
                                <asp:Label ID="SubTotalLabel" runat="server"></asp:Label>
                                <asp:HiddenField ID="SubTotalHiddenField" runat="server" />
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                        <div class="service-charge">
                            <div class="label">
                                Service Charge</div>
                            <div class="value">
                                <asp:Label ID="ServiceChargeLabel" runat="server"></asp:Label>
                                <asp:HiddenField ID="ServiceChargeHiddenField" runat="server" />
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                        <div class="tax">
                            <div class="label">
                                Tax</div>
                            <div class="value">
                                <asp:Label ID="TaxLabel" runat="server"></asp:Label>
                                <asp:HiddenField ID="TaxHiddenField" runat="server" />
                                <asp:HiddenField ID="pb1HiddenField" runat="server" />
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                        <div class="total">
                            <div class="label">
                                Total</div>
                            <div class="value">
                                <asp:Label ID="TotalLabel" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="bottom-center">
                        <div class="Button1">
                            <asp:Button ID="BackButton" runat="server" OnClick="BackButton_Click" ToolTip="Back" />
                            <asp:Button runat="server" ID="SearchButton" class="buttonRetail" ToolTip="Search" />
                            <asp:Button runat="server" ID="HoldButton" OnClick="HoldButton_Click" ToolTip="Hold" />
                            <asp:Button runat="server" ID="OpenHoldButton" ToolTip="Open Hold Transaction" />
                            <asp:Button runat="server" ID="CheckStatusButton" ToolTip="Check Transaction Status" />
                            <%--<asp:ImageButton runat="server" ID="BackButton" class="buttonRetail" OnClick="BackButton_Click"
                            ImageUrl="~/images/btnback.gif" ToolTip="Back" /><asp:ImageButton runat="server"
                                ID="SearchButton" class="buttonRetail" ImageUrl="~/images/btnsearch.gif" ToolTip="Search" /><asp:ImageButton
                                    runat="server" ID="HoldButton" class="buttonRetail" OnClick="HoldButton_Click"
                                    ImageUrl="~/images/btnhold.gif" ToolTip="Hold" /><asp:ImageButton runat="server"
                                        ID="OpenHoldButton" class="buttonRetail" OnClick="OpenHoldButton_Click" ImageUrl="~/images/btnopenhold1.gif"
                                        ToolTip="Open Hold" /><asp:ImageButton runat="server" ID="CheckStatusButton" class="buttonRetail"
                                            ToolTip="Check Status" ImageUrl="~/images/btncheckstatus.gif" />--%>
                            <asp:Button ID="JoinJobOrderButton" runat="server" ToolTip="Join Job Order" />
                            <asp:Button ID="GotoCashierButton" runat="server" ToolTip="Go to Cashier" PostBackUrl="../General/Cashier.aspx" />
                            <asp:ImageButton runat="server" class="button-submit" ID="CashierAbuButton" />
                            <asp:Button runat="server" ID="CancelAllButton" class="buttonRetail" OnClick="ClearAllButton_Click"
                                ToolTip="Cancel All" />
                            <%--<asp:ImageButton runat="server" ID="CheckInButton" class="buttonRetail" ToolTip="Check In"
                            ImageUrl="~/images/btncheckin.gif" />--%><%--<asp:ImageButton runat="server" ID="DiscButton"
                                class="buttonRetail" OnClick="DiscButton_Click" ToolTip="Discount" ImageUrl="~/images/btndiscount.gif" /><asp:ImageButton
                                    runat="server" ID="CancelAllButton" class="buttonRetail" OnClick="ClearAllButton_Click"
                                    ImageUrl="~/images/btncancelall.gif" ToolTip="Cancel All" /><asp:ImageButton ID="JoinJobOrderButton"
                                        runat="server" class="buttonRetail2" OnClick="JoinJobOrderButton_Click" ToolTip="Join Job Order"
                                        ImageUrl="~/images/btnjoinjoborder.gif" />--%>
                        </div>
                        <div class="clear">
                        </div>
                        <div class="Button2">
                            <asp:Button ID="SendToCashierButton" runat="server" OnClick="SendToCashierButton_Click"
                                ToolTip="Send To Cashier" />
                        </div>
                    </div>
                    <div class="bottom-right">
                        <!-- PANEL NUMERIC INPUT -->
                        <%--<input type="text" id="lalala" onfocus="$('#currActiveInput').val(this.id);" />--%>
                        <div id="panelInputNumber">
                            <input type="hidden" id="currActiveInput" value="" />
                            <div class="left-container">
                                <div class="number" id="nmbr1">
                                    <input type="button" value="1" class="btnInputNumber">
                                </div>
                                <div class="number" id="nmbr2">
                                    <input type="button" value="2" class="btnInputNumber">
                                </div>
                                <div class="number" id="nmbr3">
                                    <input type="button" value="3" class="btnInputNumber">
                                </div>
                                <div class="number" id="nmbr4">
                                    <input type="button" value="4" class="btnInputNumber">
                                </div>
                                <div class="number" id="nmbr5">
                                    <input type="button" value="5" class="btnInputNumber">
                                </div>
                                <div class="number" id="nmbr6">
                                    <input type="button" value="6" class="btnInputNumber">
                                </div>
                                <div class="number" id="nmbr7">
                                    <input type="button" value="7" class="btnInputNumber">
                                </div>
                                <div class="number" id="nmbr8">
                                    <input type="button" value="8" class="btnInputNumber">
                                </div>
                                <div class="number" id="nmbr9">
                                    <input type="button" value="9" class="btnInputNumber">
                                </div>
                                <div class="number" id="nmbrdot">
                                    <input type="button" value="." class="btnInputNumber">
                                </div>
                                <div class="number" id="nmbr0">
                                    <input type="button" value="0" class="btnInputNumber">
                                </div>
                                <div class="number" id="nmbr00">
                                    <input type="button" value="00" class="btnInputNumber">
                                </div>
                            </div>
                            <div class="right-container">
                                <input type="button" value="CLR" class="btnConfirmNumber" id="btnClearNumber" />
                                <asp:Button runat="server" ID="btnOKPanelNumber" class="btnConfirmNumber" Text="OK"
                                    OnClick="btnOKPanelNumber_Click" />
                            </div>
                        </div>

                        <script language="javascript">
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
                        });
                        $("#btnClearNumber").click(function() {
                            if ($("#currActiveInput").val() != "") {
                                if ($("#ProductCodeTextBox").val() != "") {
                                    $("#" + $("#currActiveInput").val()).val("");
                                }
                                else {
                                    $("#" + $("#currActiveInput").val()).val("0");
                                }
                            }
                        });
                        $(".buttonAmount").click(function() {
                            if ($("#currActiveInput").val() != "") {
                                if ($("#" + $("#currActiveInput").val()).val() == "0") {
                                    $("#" + $("#currActiveInput").val()).val(parseInt(this.value) * 1);
                                }
                                else {
                                    $("#" + $("#currActiveInput").val()).val(parseInt($("#" + $("#currActiveInput").val()).val()) + parseInt(this.value));
                                }
                            }
                            else {
                                $("#" + $("#currActiveInput").val()).val(0);
                                $("#" + $("#currActiveInput").val()).val(parseInt($("#" + $("#currActiveInput").val()).val()) + parseInt(this.value));
                            }
                        });
                        </script>

                        <!-- //PANEL NUMERIC INPUT -->
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="ReasonListPanel">
            <div class="reasonList">
                <table cellpadding="3" cellspacing="1" width="0" border="0">
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
            <div class="table-container-scroll2">
                <table class="dragger2">
                    <%--<asp:HiddenField ID="TransNmbrHiddenField" runat="server" />--%>
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
            <div class="table-navigation-scroll2">
                <div class="up">
                </div>
                <div class="down">
                </div>
            </div>
            <div class="reasonback">
                <table cellpadding="3" cellspacing="0" width="0" border="0">
                    <tr>
                        <td>
                            <asp:ImageButton runat="server" ID="Back2ImageButton" OnClick="Back2ImageButton_Click" />
                            <%--<input type="button" value="Go Back" onclick="history.back()" style="height: 40px;">--%>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </div>
    <%--</asp:Panel>--%>
    </form>
</body>
</html>
