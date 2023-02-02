<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Settlement.aspx.cs" Inherits="POS.POSInterface.General.Settlement" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>POS Settlement</title>
    <link href="../CSS/orange/style.css" media="all" rel="Stylesheet" type="text/css" />

    <script src="../CSS/orange/jquery-1.4.4.min.js" type="text/javascript"></script>

    <script src="../CSS/orange/style.js" type="text/javascript"></script>

    <script src="../CSS/orange/jquery.corner.js" type="text/javascript"></script>

    <script src="../CSS/orange/JScript.js" type="text/javascript"></script>

    <link href="../JQuery/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script src="../JQuery/jquery-ui.min.js" type="text/javascript"></script>

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
        $('#bodySettlement .container .header .right .date #tanggal').text(Hari[dateObject.getDay()] + ', ' + dateObject.getDate() + ' ' + Bulan[dateObject.getMonth()] + ' ' + (dateObject.getYear() + 1900));
        $('#bodySettlement .container .header .right .date #jam').text(hour + ':' + minute + ':' + second);
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
            $('#bodySettlement .container .header .right .date #tanggal').text(Hari[dateObject.getDay()] + ', ' + dateObject.getDate() + ' ' + Bulan[dateObject.getMonth()] + ' ' + (dateObject.getYear() + 1900));
            $('#bodySettlement .container .header .right .date #jam').text(hour + ':' + minute + ':' + second);
        }
        var x = setInterval("updateJam();", 1000);



        function LimitText(fieldObj, maxChars) {

            var result = true;
            var text = document.getElementById(fieldObj);
            if (text.value.length >= maxChars) {
                result = false;
            }

            if (window.event) {
                window.event.returnValue = result;
                return result;
            }
        }
    </script>

</head>
<body id="bodySettlement">
    <form id="form1" runat="server">
    <div class="container">
        <div class="header">
            <div class="left">
                <div class="title">
                    <div class="text">
                        POS CASHIER</div>
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
            <div class="left">
                <div class="boxLeft1">
                    <asp:HiddenField ID="CashierHiddenField" runat="server" />
                    <asp:HiddenField ID="TransactionHiddenField" runat="server" />
                    <div class="discount">
                        <div class="label">
                            Discount</div>
                        <div class="sep">
                            :</div>
                        <div class="input">
                            <asp:Literal ID="DiscountLiteral" runat="server"></asp:Literal></div>
                        <asp:HiddenField ID="DiscountHiddenField" runat="server" />
                    </div>
                    <div class="clear">
                    </div>
                    <div class="tax">
                        <div class="label">
                            Tax</div>
                        <div class="sep">
                            :</div>
                        <div class="input">
                            <asp:Literal ID="TaxLiteral" runat="server"></asp:Literal>
                            <asp:HiddenField ID="TaxHiddenField" runat="server" />
                            <asp:HiddenField ID="pb1HiddenField" runat="server" />
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="service-charge">
                        <div class="label">
                            Service Charge</div>
                        <div class="sep">
                            :</div>
                        <div class="input">
                            <asp:Literal ID="ServiceChargeLiteral" runat="server"></asp:Literal></div>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="subtotal">
                        <div class="label">
                            Subtotal</div>
                        <div class="sep">
                            :</div>
                        <div class="input">
                            <asp:Literal ID="SubtotalLiteral" runat="server"></asp:Literal></div>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="dp-received">
                        <div class="label">
                            DP Received</div>
                        <div class="sep">
                            :</div>
                        <div class="input">
                            <asp:Literal ID="DPReceivedLiteral" runat="server"></asp:Literal></div>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="other">
                        <div class="label">
                            Other Fee</div>
                        <div class="sep">
                            :</div>
                        <div class="input">
                            <asp:Literal ID="OtherFeeLiteral" runat="server"></asp:Literal></div>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="other">
                        <div class="label">
                            Bank Charge</div>
                        <div class="sep">
                            :</div>
                        <div class="input">
                            <asp:Literal ID="BankChargeLiteral" runat="server"></asp:Literal></div>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="total">
                        <div class="label">
                            Round</div>
                        <div class="sep">
                            :</div>
                        <div class="input">
                            <asp:Literal ID="RoundLiteral" runat="server"></asp:Literal></div>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="total">
                        <div class="label">
                            Total</div>
                        <div class="sep">
                            :</div>
                        <div class="input">
                            <asp:Literal ID="TotalLiteral" runat="server"></asp:Literal></div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="boxLeft2">
                    <div class="cash-payment">
                        <div class="label">
                            Cash Payment</div>
                        <div class="sep">
                            :</div>
                        <div class="input">
                            <asp:Literal ID="CashPaymentLiteral" runat="server"></asp:Literal></div>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="credit-card-payment">
                        <div class="label">
                            Credit Card Payment</div>
                        <div class="sep">
                            :</div>
                        <div class="input">
                            <asp:Literal ID="CreditCardLiteral" runat="server"></asp:Literal></div>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="debit-card-payment">
                        <div class="label">
                            Debit Card Payment</div>
                        <div class="sep">
                            :</div>
                        <div class="input">
                            <asp:Literal ID="DebitCardLiteral" runat="server"></asp:Literal></div>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="gift-voucher">
                        <div class="label">
                            Gift Voucher</div>
                        <div class="sep">
                            :</div>
                        <div class="input">
                            <asp:Literal ID="VoucherLiteral" runat="server"></asp:Literal></div>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="change">
                        <div class="label">
                            Change</div>
                        <div class="sep">
                            :</div>
                        <div class="input">
                            <asp:Literal ID="ChangeLiteral" runat="server"></asp:Literal></div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="boxLeft3">
                    <asp:ImageButton ID="PayImageButton" runat="server" OnClick="PayImageButton_Click" />
                </div>
                <div class="boxLeft4">
                    <div class="change">
                        <div class="label">
                            Change Due</div>
                        <div class="input">
                            Rp.
                            <asp:Literal ID="ChangeDueLiteral" runat="server"></asp:Literal></div>
                    </div>
                </div>
            </div>
            <div class="right">
                <div class="top">
                    <div class="logo">
                        logo</div>
                    <div class="top-right">
                        <asp:Label ID="SettlementType" runat="server"></asp:Label></div>
                </div>
                <div class="clear">
                </div>
                <div class="middle">
                    <div class="boxRightTopTopLineFloat">
                        <asp:ImageButton ID="ImageButton1" runat="server" class="buttonSettlement" Text="CASH"
                            ImageUrl="" ToolTip="Cash" OnClick="CashButton_Click" />
                    </div>
                    <div class="boxRightTopTopLineFloat">
                        <asp:ImageButton ID="ImageButton2" runat="server" class="buttonSettlement" Text="CreditCard"
                            ImageUrl="" ToolTip="Credit Card" OnClick="CreditCardButton_Click" />
                    </div>
                    <div class="boxRightTopTopLineFloat">
                        <asp:ImageButton ID="ImageButton3" runat="server" class="buttonSettlement" Text="DebitCard"
                            ImageUrl="" ToolTip="Debit Card" OnClick="DebitCardButton_Click" />
                    </div>
                    <div class="boxRightTopTopLineFloat">
                        <asp:ImageButton ID="ImageButton4" runat="server" class="buttonSettlement" Text="DebitCard"
                            ImageUrl="" ToolTip="Voucher" OnClick="VoucherButton_Click" />
                    </div>
                    <div class="boxRightTopTopLineFloat">
                        <asp:ImageButton ID="ImageButton5" runat="server" class="buttonSettlement" Text="Back"
                            ImageUrl="" ToolTip="Back" OnClick="BackButton_Click" />
                    </div>
                </div>
                <asp:Panel ID="NumberInputPanel" runat="server">
                    <div class="bottom">
                        <div style="color: Red; font-size: 17px; margin-bottom: 5px;">
                            <asp:Label ID="WarningLabelPay" runat="server"></asp:Label></div>
                        <div runat="server" id="CashPanel" class="boxRightCash">
                            <div>
                                <div id="AmountDiv">
                                    <div class="title">
                                        CASH AMOUNT</div>
                                    <div class="inner">
                                        <input type="button" value="5000" class="buttonAmount" />
                                        <input type="button" value="100000" class="buttonAmount" />
                                        <br />
                                        <br />
                                        <input type="button" value="10000" class="buttonAmount" />
                                        <input type="button" value="200000" class="buttonAmount" />
                                        <br />
                                        <br />
                                        <input type="button" value="20000" class="buttonAmount" />
                                        <input type="button" value="500000" class="buttonAmount" />
                                        <br />
                                        <br />
                                        <input type="button" value="50000" class="buttonAmount" />
                                        <input type="button" value="1000000" class="buttonAmount" />
                                    </div>
                                </div>
                                <div id="NumpadDiv" style="float: left">
                                    <asp:TextBox runat="server" ID="AmountPaymentTextBox" Text="0"></asp:TextBox>
                                    <!-- PANEL NUMERIC INPUT -->
                                    <%--<input type="text" id="lalala" onfocus="$('#currActiveInput').val(this.id);" />--%>
                                    <div id="panelInputNumber">
                                        <input type="hidden" id="currActiveInput" value="0" />
                                        <div class="left-container">
                                            <div class="number" id="nmbr1">
                                                <input type="button" value="1" class="btnInputNumber"></div>
                                            <div class="number" id="nmbr2">
                                                <input type="button" value="2" class="btnInputNumber"></div>
                                            <div class="number" id="nmbr3">
                                                <input type="button" value="3" class="btnInputNumber"></div>
                                            <div class="number" id="nmbr4">
                                                <input type="button" value="4" class="btnInputNumber"></div>
                                            <div class="number" id="nmbr5">
                                                <input type="button" value="5" class="btnInputNumber"></div>
                                            <div class="number" id="nmbr6">
                                                <input type="button" value="6" class="btnInputNumber"></div>
                                            <div class="number" id="nmbr7">
                                                <input type="button" value="7" class="btnInputNumber"></div>
                                            <div class="number" id="nmbr8">
                                                <input type="button" value="8" class="btnInputNumber"></div>
                                            <div class="number" id="nmbr9">
                                                <input type="button" value="9" class="btnInputNumber"></div>
                                            <div class="number" id="nmbrdot">
                                                <input type="button" value="." class="btnInputNumber"></div>
                                            <div class="number" id="nmbr0">
                                                <input type="button" value="0" class="btnInputNumber"></div>
                                            <div class="number" id="nmbr00">
                                                <input type="button" value="00" class="btnInputNumber"></div>
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
                                                $("#" + $("#currActiveInput").val()).val("0");
                                            }
                                        });
                                        $(".buttonAmount").click(function() {
                                            if ($("#currActiveInput").val() != "") {
                                                if ($("#" + $("#currActiveInput").val()).val() == "0") {
                                                    $("#" + $("#currActiveInput").val()).val(parseInt(this.value || "0") * 1);
                                                }
                                                else {
                                                    $("#" + $("#currActiveInput").val()).val(parseInt($("#" + $("#currActiveInput").val()).val() || "0") + parseInt(this.value || "0"));
                                                }
                                            }
                                            else {
                                                $("#" + $("#currActiveInput").val()).val(0);
                                                $("#" + $("#currActiveInput").val()).val(parseInt($("#" + $("#currActiveInput").val()).val() || "0") + parseInt(this.value || "0"));
                                            }
                                        });
                                    </script>

                                    <!-- //PANEL NUMERIC INPUT -->
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="CreditCardPanel" class="boxRightCreditCard">
                            <div>
                                <div id="NumpadDivCredit">
                                    <div class="reference">
                                        <div class="label">
                                            References :</div>
                                        <div class="input">
                                            <asp:TextBox runat="server" ID="CreditCardReferenceTextBox" Text="0"></asp:TextBox></div>
                                        <div class="label">
                                            Nominal :</div>
                                        <div class="input">
                                            <asp:TextBox runat="server" ID="CreditCardNominalTextBox" Text="0"></asp:TextBox></div>
                                    </div>
                                    <!-- PANEL NUMERIC INPUT -->
                                    <%--<input type="text" id="lalala" onfocus="$('#currActiveInput').val(this.id);" />--%>
                                    <div id="panelInputNumberCredit">
                                        <input type="hidden" id="currActiveInputCredit" value="0" />
                                        <div class="left-container">
                                            <div class="number" id="nmbr1_2">
                                                <input type="button" value="1" class="btnInputNumberCredit"></div>
                                            <div class="number" id="nmbr2_2">
                                                <input type="button" value="2" class="btnInputNumberCredit"></div>
                                            <div class="number" id="nmbr3_2">
                                                <input type="button" value="3" class="btnInputNumberCredit"></div>
                                            <div class="number" id="nmbr4_2">
                                                <input type="button" value="4" class="btnInputNumberCredit"></div>
                                            <div class="number" id="nmbr5_2">
                                                <input type="button" value="5" class="btnInputNumberCredit"></div>
                                            <div class="number" id="nmbr6_2">
                                                <input type="button" value="6" class="btnInputNumberCredit"></div>
                                            <div class="number" id="nmbr7_2">
                                                <input type="button" value="7" class="btnInputNumberCredit"></div>
                                            <div class="number" id="nmbr8_2">
                                                <input type="button" value="8" class="btnInputNumberCredit"></div>
                                            <div class="number" id="nmbr9_2">
                                                <input type="button" value="9" class="btnInputNumberCredit"></div>
                                            <div class="number" id="nmbrdot_2">
                                                <input type="button" value="." class="btnInputNumberCredit"></div>
                                            <div class="number" id="nmbr0_2">
                                                <input type="button" value="0" class="btnInputNumberCredit"></div>
                                            <div class="number" id="nmbr00_2">
                                                <input type="button" value="00" class="btnInputNumberCredit"></div>
                                        </div>
                                        <div class="right-container">
                                            <input type="button" value="CLR" class="btnConfirmNumberCredit" id="btnClearNumberCredit" />
                                            <asp:Button runat="server" ID="btnOKPanelNumberCredit" class="btnConfirmNumberCredit"
                                                Text="OK" OnClick="btnOKPanelNumberCredit_Click" />
                                        </div>
                                    </div>

                                    <script type="text/javascript" language="javascript">
                                        $(document).ready(function() {
                                            $(".btnInputNumberCredit").click(function() {
                                                if ($("#currActiveInputCredit").val() != "") {
                                                    if (this.value == '.') {
                                                        if (($("#" + $("#currActiveInputCredit").val()).val()).indexOf('.') < 0) {
                                                            if ($("#currActiveInputCredit").val() == "CreditCardReferenceTextBox") {
                                                                if ($("#CreditCardReferenceTextBox").val().length < 16) {
                                                                    $("#" + $("#currActiveInputCredit").val()).val($("#" + $("#currActiveInputCredit").val()).val() + this.value);
                                                                }
                                                            }
                                                            else {
                                                                $("#" + $("#currActiveInputCredit").val()).val($("#" + $("#currActiveInputCredit").val()).val() + this.value);
                                                            }
                                                        }
                                                    }
                                                    else {
                                                        if ($("#" + $("#currActiveInputCredit").val()).val() == "0") {
                                                            $("#" + $("#currActiveInputCredit").val()).val(this.value * 1);
                                                        }
                                                        else {
                                                            if ($("#currActiveInputCredit").val() == "CreditCardReferenceTextBox") {
                                                                if ($("#CreditCardReferenceTextBox").val().length < 16) {
                                                                    $("#" + $("#currActiveInputCredit").val()).val($("#" + $("#currActiveInputCredit").val()).val() + this.value);
                                                                }
                                                            }
                                                            else {
                                                                $("#" + $("#currActiveInputCredit").val()).val($("#" + $("#currActiveInputCredit").val()).val() + this.value);
                                                            }
                                                        }
                                                    }
                                                }
                                            });
                                            $("#btnClearNumberCredit").click(function() {
                                                if ($("#currActiveInputCredit").val() != "") {
                                                    $("#" + $("#currActiveInputCredit").val()).val("0");
                                                }
                                            });
                                        });
                                    </script>

                                    <%--<script type="text/javascript" language="javascript">
                                    $(document).ready(function() {
                                        $(".btnInputNumberCredit").click(function() {
                                            //alert($("#CreditCardReferenceTextBox").val().length);
//                                            if ($("#CreditCardReferenceTextBox").val().length < 16) {
                                                if ($("#currActiveInputCredit").val() != "") {
                                                    if (this.value == '.') {
                                                        if (($("#" + $("#currActiveInputCredit").val()).val()).indexOf('.') < 0)
                                                            $("#" + $("#currActiveInputCredit").val()).val($("#" + $("#currActiveInputCredit").val()).val() + this.value);
                                                    }
                                                    else {
                                                        if ($("#" + $("#currActiveInputCredit").val()).val() == "0") {
                                                            $("#" + $("#currActiveInputCredit").val()).val(this.value * 1);
                                                        }
                                                        else
                                                            $("#" + $("#currActiveInputCredit").val()).val($("#" + $("#currActiveInputCredit").val()).val() + this.value);
                                                    
                                                }
                                            }
                                        });
                                        $("#btnClearNumberCredit").click(function() {
                                            if ($("#currActiveInputCredit").val() != "") {
                                                $("#" + $("#currActiveInputCredit").val()).val("0");
                                            }
                                        });
                                    });
                                </script>--%>
                                    <!-- //PANEL NUMERIC INPUT -->
                                </div>
                                <div id="ListCardTypePanel">
                                    <div id="ListCreditTypeBox">
                                        <asp:Repeater runat="server" ID="CardTypeRepeater" OnItemCommand="CardTypeRepeater_ItemCommand"
                                            OnItemDataBound="CardTypeRepeater_ItemDataBound">
                                            <ItemTemplate>
                                                <asp:Button runat="server" ID="CreditTypeButton" />
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:HiddenField ID="CardTypeHiddenField" runat="server" />
                                    </div>
                                </div>
                                <div id="ListCreditCardPanel">
                                    <div id="ListCreditCardBox">
                                        <asp:Repeater runat="server" ID="CreditCardButtonRepeater" OnItemCommand="CreditCardButtonRepeater_ItemCommand"
                                            OnItemDataBound="CreditCardButtonRepeater_ItemDataBound">
                                            <ItemTemplate>
                                                <asp:Button runat="server" ID="CreditCardButton" />
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:HiddenField ID="CreditCardHiddenField" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="DebitPanel" class="boxRightDebit">
                            <div>
                                <div id="NumpadDivDebit">
                                    <div class="reference">
                                        <div class="label">
                                            References :</div>
                                        <div class="input">
                                            <asp:TextBox runat="server" ID="DebitCardReferenceTextBox" Text="0"></asp:TextBox></div>
                                        <div class="label">
                                            Nominal :</div>
                                        <div class="input">
                                            <asp:TextBox runat="server" ID="DebitCardNominalTextBox" Text="0"></asp:TextBox></div>
                                    </div>
                                    <!-- PANEL NUMERIC INPUT -->
                                    <%--<input type="text" id="lalala" onfocus="$('#currActiveInput').val(this.id);" />--%>
                                    <div id="panelInputNumberDebit">
                                        <input type="hidden" id="currActiveInputDebit" value="0" />
                                        <div class="left-container">
                                            <div class="number" id="nmbr1_3">
                                                <input type="button" value="1" class="btnInputNumberDebit"></div>
                                            <div class="number" id="nmbr2_3">
                                                <input type="button" value="2" class="btnInputNumberDebit"></div>
                                            <div class="number" id="nmbr3_3">
                                                <input type="button" value="3" class="btnInputNumberDebit"></div>
                                            <div class="number" id="nmbr4_3">
                                                <input type="button" value="4" class="btnInputNumberDebit"></div>
                                            <div class="number" id="nmbr5_3">
                                                <input type="button" value="5" class="btnInputNumberDebit"></div>
                                            <div class="number" id="nmbr6_3">
                                                <input type="button" value="6" class="btnInputNumberDebit"></div>
                                            <div class="number" id="nmbr7_3">
                                                <input type="button" value="7" class="btnInputNumberDebit"></div>
                                            <div class="number" id="nmbr8_3">
                                                <input type="button" value="8" class="btnInputNumberDebit"></div>
                                            <div class="number" id="nmbr9_3">
                                                <input type="button" value="9" class="btnInputNumberDebit"></div>
                                            <div class="number" id="nmbrdot_3">
                                                <input type="button" value="." class="btnInputNumberDebit"></div>
                                            <div class="number" id="nmbr0_3">
                                                <input type="button" value="0" class="btnInputNumberDebit"></div>
                                            <div class="number" id="nmbr00_3">
                                                <input type="button" value="00" class="btnInputNumberDebit"></div>
                                        </div>
                                        <div class="right-container">
                                            <input type="button" value="CLR" class="btnConfirmNumberDebit" id="btnClearNumberDebit" />
                                            <asp:Button runat="server" ID="btnOKPanelNumberDebit" class="btnConfirmNumberDebit"
                                                Text="OK" OnClick="btnOKPanelNumberDebit_Click" />
                                        </div>
                                    </div>

                                    <script type="text/javascript" language="javascript">
                                        $(document).ready(function() {
                                            $(".btnInputNumberDebit").click(function() {
                                                if ($("#currActiveInputDebit").val() != "") {
                                                    if (this.value == '.') {
                                                        if (($("#" + $("#currActiveInputDebit").val()).val()).indexOf('.') < 0) {
                                                            if ($("#currActiveInputDebit").val() == "DebitCardReferenceTextBox") {
                                                                if ($("#DebitCardReferenceTextBox").val().length < 16) {
                                                                    $("#" + $("#currActiveInputDebit").val()).val($("#" + $("#currActiveInputDebit").val()).val() + this.value);
                                                                }
                                                            }
                                                            else {
                                                                $("#" + $("#currActiveInputDebit").val()).val($("#" + $("#currActiveInputDebit").val()).val() + this.value);
                                                            }
                                                        }
                                                    }
                                                    else {
                                                        if ($("#" + $("#currActiveInputDebit").val()).val() == "0") {
                                                            $("#" + $("#currActiveInputDebit").val()).val(this.value * 1);
                                                        }
                                                        else {
                                                            if ($("#currActiveInputDebit").val() == "DebitCardReferenceTextBox") {
                                                                if ($("#DebitCardReferenceTextBox").val().length < 16) {
                                                                    $("#" + $("#currActiveInputDebit").val()).val($("#" + $("#currActiveInputDebit").val()).val() + this.value);
                                                                }
                                                            }
                                                            else {
                                                                $("#" + $("#currActiveInputDebit").val()).val($("#" + $("#currActiveInputDebit").val()).val() + this.value);
                                                            }
                                                        }
                                                    }
                                                }
                                            });
                                            $("#btnClearNumberDebit").click(function() {
                                                if ($("#currActiveInputDebit").val() != "") {
                                                    $("#" + $("#currActiveInputDebit").val()).val("0");
                                                }
                                            });
                                        });
                                    </script>

                                    <!-- //PANEL NUMERIC INPUT -->
                                </div>
                                <div id="ListBankDebitPanel">
                                    <div id="ListBankDebitBox">
                                        <asp:Repeater runat="server" ID="DebitCardRepeater" OnItemCommand="DebitCardRepeater_ItemCommand"
                                            OnItemDataBound="DebitCardRepeater_ItemDataBound">
                                            <ItemTemplate>
                                                <asp:Button runat="server" ID="DebitCardButton" />
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:HiddenField ID="DebitHiddenField" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="VoucherPanel" class="boxRightVoucher">
                            <div>
                                <div class="left-inner">
                                    <div>
                                        <div class="title">
                                            Voucher No</div>
                                        <asp:TextBox runat="server" ID="VoucherNoTextBox"></asp:TextBox>
                                        <asp:HiddenField ID="VoucherNoHiddenField" runat="server" />
                                    </div>
                                    <div>
                                        <div class="title">
                                            Nominal</div>
                                        <asp:TextBox runat="server" ID="VoucherNominalTextBox"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="right-inner">
                                    <!-- PANEL NUMERIC INPUT -->
                                    <div id="panelInputNumberVoucher">
                                        <input type="hidden" id="currActiveInputVoucher" value="0" />
                                        <div class="left-container">
                                            <div class="number" id="nmbr1_4">
                                                <input type="button" value="1" class="btnInputNumberVoucher"></div>
                                            <div class="number" id="nmbr2_4">
                                                <input type="button" value="2" class="btnInputNumberVoucher"></div>
                                            <div class="number" id="nmbr3_4">
                                                <input type="button" value="3" class="btnInputNumberVoucher"></div>
                                            <div class="number" id="nmbr4_4">
                                                <input type="button" value="4" class="btnInputNumberVoucher"></div>
                                            <div class="number" id="nmbr5_4">
                                                <input type="button" value="5" class="btnInputNumberVoucher"></div>
                                            <div class="number" id="nmbr6_4">
                                                <input type="button" value="6" class="btnInputNumberVoucher"></div>
                                            <div class="number" id="nmbr7_4">
                                                <input type="button" value="7" class="btnInputNumberVoucher"></div>
                                            <div class="number" id="nmbr8_4">
                                                <input type="button" value="8" class="btnInputNumberVoucher"></div>
                                            <div class="number" id="nmbr9_4">
                                                <input type="button" value="9" class="btnInputNumberVoucher"></div>
                                            <div class="number" id="nmbrdot_4">
                                                <input type="button" value="." class="btnInputNumberVoucher"></div>
                                            <div class="number" id="nmbr0_4">
                                                <input type="button" value="0" class="btnInputNumberVoucher"></div>
                                            <div class="number" id="nmbr00_4">
                                                <input type="button" value="00" class="btnInputNumberVoucher"></div>
                                        </div>
                                        <div class="right-container">
                                            <input type="button" value="CLR" class="btnConfirmNumberVoucher" id="btnClearNumberVoucher" />
                                            <asp:Button runat="server" ID="btnOKPanelNumberVoucher" class="btnConfirmNumberVoucher"
                                                Text="OK" OnClick="btnOKPanelNumberVoucher_Click" />
                                        </div>
                                    </div>

                                    <script type="text/javascript" language="javascript">
                                        $(document).ready(function() {
                                            $(".btnInputNumberVoucher").click(function() {
                                                if ($("#currActiveInputVoucher").val() != "") {
                                                    if (this.value == '.') {
                                                        if (($("#" + $("#currActiveInputVoucher").val()).val()).indexOf('.') < 0)
                                                            $("#" + $("#currActiveInputVoucher").val()).val($("#" + $("#currActiveInputVoucher").val()).val() + this.value);
                                                    }
                                                    else {
                                                        if ($("#" + $("#currActiveInputVoucher").val()).val() == "0") {
                                                            $("#" + $("#currActiveInputVoucher").val()).val(this.value * 1);
                                                        }
                                                        else
                                                            $("#" + $("#currActiveInputVoucher").val()).val($("#" + $("#currActiveInputVoucher").val()).val() + this.value);
                                                    }
                                                }
                                            });
                                            $("#btnClearNumberVoucher").click(function() {
                                                if ($("#currActiveInputVoucher").val() != "") {
                                                    $("#" + $("#currActiveInputVoucher").val()).val("0");
                                                }
                                            });
                                        });
                                    </script>

                                    <!-- //PANEL NUMERIC INPUT -->
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="SplitCashPanel" class="boxRightSplitCash">
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <asp:Panel ID="ProductPromoPanel" runat="server">
                <div class="rightBottom">
                    <div style="margin: 3px 0px 3px 3px; color: #FFFF00; font-size: 16px; font-weight: bold;
                        margin-bottom: 10px;">
                        PRODUCT PROMO
                    </div>
                    <div>
                        <asp:Label ID="WarningLabel" runat="server" class="warning" Text="" Enabled="false"></asp:Label>
                        <asp:Repeater runat="server" ID="ListRepeaterPromo" OnItemDataBound="ListRepeaterPromo_ItemDataBound">
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <%--<td>
                                            <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                        </td>--%>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Literal runat="server" ID="ProductLiteral"></asp:Literal>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </asp:Panel>
        </div>
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
    </div>
    </form>
</body>
</html>
