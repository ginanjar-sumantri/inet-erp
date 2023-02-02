<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cashier.aspx.cs" Inherits="POS.POSInterface.General.Cashier" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<asp:literal id="javascriptReceiver" runat="server"></asp:literal>
<head runat="server">
    <link href="../CSS/orange/style2.css" media="all" rel="Stylesheet" type="text/css" />

    <script src="../CSS/orange/jquery-1.4.4.min.js" type="text/javascript"></script>

    <script src="../CSS/orange/jquery.corner.js" type="text/javascript"></script>

    <script src="../CSS/orange/style.js" type="text/javascript"></script>

    <style type="text/css">
        .fixedTable
        {
            font-size: small;
        }
    </style>

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
        $('#bodyCashier .container .header .right .date #tanggal').text(Hari[dateObject.getDay()] + ', ' + dateObject.getDate() + ' ' + Bulan[dateObject.getMonth()] + ' ' + (dateObject.getYear() + 1900));
        $('#bodyCashier .container .header .right .date #jam').text(hour + ':' + minute + ':' + second);
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
            $('#bodyCashier .container .header .right .date #tanggal').text(Hari[dateObject.getDay()] + ', ' + dateObject.getDate() + ' ' + Bulan[dateObject.getMonth()] + ' ' + (dateObject.getYear() + 1900));
            $('#bodyCashier .container .header .right .date #jam').text(hour + ':' + minute + ':' + second);
        }
        var x = setInterval("updateJam();", 1000);
    </script>

    <title>POS - Cashier</title>
</head>
<body id="bodyCashier">
    <form id="form1" runat="server">
    <div class="container">
        <asp:Panel runat="server" ID="FormPanel" Visible="false">
            <div class="header">
                <div class="left">
                    <div class="title">
                        <div class="text">
                            Pos Cashier
                        </div>
                        <div class="sep">
                        </div>
                    </div>
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
            <div class="content">
                <div class="top" style="border-radius: 10px;">
                    <div class="table">
                        <div class="table-inner-top">
                            <div class="input-form" id="referencenumber">
                                <div class="label" style="float: left; padding-left: 3px">
                                    <asp:Literal ID="Category" runat="server"></asp:Literal>
                                </div>
                                <div style="float: left; padding-left: 3px; padding-right: 2px">
                                    :
                                </div>
                                <div class="input_textbox">
                                    <%--<div style="float: left; padding-left: 5px; padding-top: 5px; margin-left:3px">--%>
                                    <asp:TextBox ID="ReferencesNumberTextBox" runat="server"></asp:TextBox>
                                    <%--</div>--%>
                                </div>
                                <div style="float: left; padding-left: 3px" id="resetbtn">
                                    <asp:Button ID="ResetButton" runat="server" Text="Reset" OnClick="ResetButton_Click" />
                                </div>
                                <div style="float: left; padding-left: 3px" id="searchbtn">
                                    <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_Click" />
                                </div>
                                <div style="float: left; padding-left: 3px; margin-top:-6px;" class="backbutton">
                                    <asp:Button ID="Back2Button" runat="server" Text="Back" OnClick="Back2Button_Click" />
                                </div>
                                <div class="warning">
                                    <asp:Label ID="WarningLabel" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <table class="tableRepater">
                        <tr valign="top">
                            <td>
                                <asp:Panel ID="NotYetPayListPanel" runat="server">
                                    <table>
                                        <tr>
                                            <td valign="top">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="3" cellspacing="0" class="fixedTable">
                                                                <tr class="toptable">
                                                                    <td align="center" style="width: 144px;">
                                                                        TransNmbr
                                                                    </td>
                                                                    <td align="center" style="width: 70px">
                                                                        Divisi
                                                                    </td>
                                                                    <td align="center" style="width: 93px">
                                                                        Reference No
                                                                    </td>
                                                                    <td align="center" style="width: 85px">
                                                                        View
                                                                    </td>
                                                                    <td align="center" style="width: 28px">
                                                                        Pick
                                                                    </td>
                                                                    <td align="center" style="width: 31px">
                                                                        DP
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
                                                                                <td style="width: 145px">
                                                                                    <asp:Literal ID="TransNmbrLiteral" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td style="width: 65px">
                                                                                    <asp:Literal ID="DivisiLiteral" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td style="width: 95px">
                                                                                    <asp:Literal ID="ReferenceNoLiteral" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td align="center" class="viewbutton">
                                                                                    <asp:ImageButton ID="ViewDetailImageButton" runat="server" />
                                                                                </td>
                                                                                <td align="center" class="pickbutton">
                                                                                    <asp:ImageButton ID="PickImageButton" runat="server" />
                                                                                </td>
                                                                                <td align="center" class="dpbutton">
                                                                                    <asp:ImageButton ID="DpImageButton" runat="server" />
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </table>
                                                            </div>
                                                            <asp:HiddenField ID="List1HiddenField" runat="server" />
                                                            <asp:HiddenField ID="SettleTypeHiddenField" runat="server" />
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
                                            <td valign="top">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="3" cellspacing="0" class="fixedTable">
                                                                <tr class="toptable">
                                                                    <td align="center" style="width: 28px">
                                                                        Back
                                                                    </td>
                                                                    <td align="center" style="width: 146px">
                                                                        TransNmbr
                                                                    </td>
                                                                    <td align="center" style="width: 65px">
                                                                        Divisi
                                                                    </td>
                                                                    <td align="center" style="width: 95px">
                                                                        Reference No
                                                                    </td>
                                                                    <td align="center" style="width: 80px">
                                                                        View
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
                                                                                <td style="width: 145px">
                                                                                    <asp:Literal ID="TransNmbrLiteral" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td style="width: 65px">
                                                                                    <asp:Literal ID="DivisiLiteral" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td style="width: 95px">
                                                                                    <asp:Literal ID="ReferenceNoLiteral" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td align="center" class="viewbutton">
                                                                                    <asp:ImageButton ID="ViewDetailImageButton" runat="server" />
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
                                    <table>
                                        <tr>
                                            <td style="width: 500px">
                                                <table>
                                                    <tr>
                                                        <td style="float: left" class="backbutton">
                                                            <asp:Button ID="BackButton" runat="server" Text="Back" OnClick="BackButton_Click" />
                                                        </td>
                                                        <%--<td class="resetallbutton">
                                                    <asp:Button ID="ResetSelectionButton" runat="server" Text="Reset All" 
                                                        Width="63" onclick="ResetSelectionButton_Click" />
                                                </td>--%>
                                                        <td class="closeshiftbutton">
                                                            <asp:Button ID="CloseShiftButton" runat="server" PostBackUrl="CloseShift.aspx" Text="Close Shift" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td class="settlementbutton">
                                                            <asp:Button ID="SettlementButton" runat="server" Text="Settlement" OnClick="SettlementButton_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="ReprintStrookButton" CssClass="ReprintStrookButton" runat="server"
                                                                Text="RePrint Strook" OnClick="ReprintStrookButton_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td colspan="3">
                                                <asp:HiddenField ID="TransNmbrHiddenField" runat="server" />
                                                <div class="detailcashier">
                                                    <div class="toplabel">
                                                        Job Order No
                                                        <asp:Literal runat="server" ID="JobOrderNoLiteral">
                                                        </asp:Literal>
                                                    </div>
                                                </div>
                                                <table cellpadding="3" cellspacing="0">
                                                    <tr class="toptable">
                                                        <td align="center" style="width: 120px">
                                                            Product Code
                                                        </td>
                                                        <td align="center" style="width: 288px">
                                                            Description
                                                        </td>
                                                        <td align="center" style="width: 50px">
                                                            Qty
                                                        </td>
                                                        <td align="center" style="width: 150px">
                                                            Disc
                                                        </td>
                                                        <td align="center" style="width: 150px">
                                                            Price
                                                        </td>
                                                        <td align="center" style="width: 175px">
                                                            SubTotal
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentTable1">
                                                <asp:HiddenField ID="DetailTypeHiddenField" runat="server" />
                                                <div class="table-container-scroll3">
                                                    <table>
                                                        <asp:Repeater ID="ListRepeaterDetail" runat="server" OnItemDataBound="ListRepeaterDetail_ItemDataBound">
                                                            <ItemTemplate>
                                                                <tr id="DetailRepeaterItemTemplate" runat="server" class="repeater">
                                                                    <td style="width: 120px">
                                                                        <asp:Literal ID="ProductCodeLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td style="width: 288px">
                                                                        <asp:Literal ID="DescriptionLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td style="width: 50px">
                                                                        <asp:Literal ID="QtyLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td style="width: 150px">
                                                                        <asp:Literal ID="DiscLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td style="width: 150px">
                                                                        <asp:Literal ID="PriceLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td style="width: 175px">
                                                                        <asp:Literal ID="LineTotalLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="table-navigation-scroll3">
                                                    <div class="up">
                                                    </div>
                                                    <div class="down">
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="PayListPanel" runat="server">
                                    <table>
                                        <tr>
                                            <td valign="top">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="3" cellspacing="0" class="fixedTable">
                                                                <tr class="toptable">
                                                                    <td align="center" style="width: 146px;">
                                                                        TransNmbr
                                                                    </td>
                                                                    <td align="center" style="width: 146px">
                                                                        FileNmbr
                                                                    </td>
                                                                    <td align="center" style="width: 129px">
                                                                        TransDate
                                                                    </td>
                                                                    <td align="center" style="width: 71px">
                                                                        Cashier
                                                                    </td>
                                                                    <td align="center" style="width: 75px">
                                                                        Settle Type
                                                                    </td>
                                                                    <td align="center" style="width: 93px">
                                                                        Total
                                                                    </td>
                                                                    <td align="center" style="width: 85px">
                                                                        View
                                                                    </td>
                                                                    <td align="center" style="width: 178px">
                                                                        Print
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table>
                                                    <tr>
                                                        <td class="ContentTable">
                                                            <div class="table-container-scroll4">
                                                                <table>
                                                                    <asp:Repeater ID="PayListRepeater" runat="server" OnItemDataBound="PayListRepeater_ItemDataBound"
                                                                        OnItemCommand="PayListRepeater_ItemCommand">
                                                                        <ItemTemplate>
                                                                            <tr id="RepeaterItemTemplate" runat="server" class="repeater" align="left">
                                                                                <td style="width: 145px">
                                                                                    <asp:Literal ID="TransNmbrLiteral" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td style="width: 145px">
                                                                                    <asp:Literal ID="FileNmbrLiteral" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td style="width: 130px">
                                                                                    <asp:Literal ID="TransDateLiteral" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td style="width: 70px">
                                                                                    <asp:Literal ID="CashierLiteral" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td style="width: 76px">
                                                                                    <asp:Literal ID="SettleTypeLiteral" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td style="width: 95px">
                                                                                    <asp:Literal ID="TotalLiteral" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td align="center" class="viewbutton">
                                                                                    <asp:ImageButton ID="ViewDetailImageButton" runat="server" />
                                                                                </td>
                                                                                <td align="center" style="width: 178px">
                                                                                    <div style="float: left; margin-left: 4px;">
                                                                                        <asp:ImageButton ID="PrintCustomerImageButton" CssClass="PrintCustomerImageButton"
                                                                                            runat="server" />
                                                                                    </div>
                                                                                    <div style="float: left; margin-left: 5px;">
                                                                                        <asp:ImageButton ID="PrintKitchenImageButton" CssClass="PrintKitchenImageButton"
                                                                                            runat="server" />
                                                                                    </div>
                                                                                    <%-- <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
--%>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </table>
                                                            </div>
                                                            <%--<asp:HiddenField ID="HiddenField1" runat="server" />
                                                        <asp:HiddenField ID="HiddenField2" runat="server" />--%>
                                                        </td>
                                                        <td>
                                                            <div class="table-navigation-scroll4">
                                                                <div class="up">
                                                                </div>
                                                                <div class="down">
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td colspan="3">
                                                <asp:HiddenField ID="TransNmbr2HiddenField" runat="server" />
                                                <%--<div class="detailcashier" style="height: 100px;">--%>
                                                <div class="detailcashier" style="height: 35px;">
                                                    <div style="margin-left: 10px; padding-top: 8px;">
                                                        DETAIL TOTAL PAYMENT
                                                    </div>
                                                    <%--class="toplabel"--%>
                                                </div>
                                                <div>
                                                    <table>
                                                        <tr class="toptable">
                                                            <td style="width: 175px;">
                                                                Job Order No :
                                                            </td>
                                                            <td style="width: 150px;">
                                                                Cash
                                                            </td>
                                                            <td style="width: 150px;">
                                                                Debit
                                                            </td>
                                                            <td style="width: 150px;">
                                                                Credit
                                                            </td>
                                                            <td style="width: 150px;">
                                                                Voucher
                                                            </td>
                                                            <td style="width: 150px;">
                                                                Total Payment
                                                            </td>
                                                        </tr>
                                                        <tr class="repeater" style="background-color: #1A1A1A;">
                                                            <td style="font-size: 15px;">
                                                                <asp:Literal runat="server" ID="JobOrder2NoLiteral">
                                                                </asp:Literal>
                                                            </td>
                                                            <td style="font-size: 18px;">
                                                                <asp:Literal runat="server" ID="AmountCashLiteral">
                                                                </asp:Literal>
                                                            </td>
                                                            <td style="font-size: 18px;">
                                                                <asp:Literal runat="server" ID="PaymentDebitLiteral">
                                                                </asp:Literal>
                                                                <asp:Literal runat="server" ID="AmountDebitLiteral">
                                                                </asp:Literal>
                                                            </td>
                                                            <td style="font-size: 18px;">
                                                                <asp:Literal runat="server" ID="PaymentCreditLiteral">
                                                                </asp:Literal>
                                                                <asp:Literal runat="server" ID="AmountCreditLiteral">
                                                                </asp:Literal>
                                                            </td>
                                                            <td style="font-size: 18px;">
                                                                <asp:Literal runat="server" ID="PaymentVoucherLiteral">
                                                                </asp:Literal>
                                                                <asp:Literal runat="server" ID="AmountVoucherLiteral">
                                                                </asp:Literal>
                                                            </td>
                                                            <td style="font-size: 18px;">
                                                                <asp:Literal runat="server" ID="TotalPaymentLiteral">
                                                                </asp:Literal>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <%--</div>--%>
                                                <div class="detailcashier" style="height: 35px; margin-top: 10px; margin-bottom: 0px;">
                                                    <div style="margin-left: 10px; padding-top: 8px;">
                                                        DETAIL PRODUCT
                                                    </div>
                                                </div>
                                                <table cellpadding="3" cellspacing="0">
                                                    <tr class="toptable">
                                                        <td align="center" style="width: 120px">
                                                            Product Code
                                                        </td>
                                                        <td align="center" style="width: 288px">
                                                            Description
                                                        </td>
                                                        <td align="center" style="width: 50px">
                                                            Qty
                                                        </td>
                                                        <td align="center" style="width: 150px">
                                                            Unit
                                                        </td>
                                                        <td align="center" style="width: 150px">
                                                            Price
                                                        </td>
                                                        <td align="center" style="width: 175px">
                                                            SubTotal
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentTable1">
                                                <asp:HiddenField ID="DetailType2HiddenField" runat="server" />
                                                <div class="table-container-scroll3">
                                                    <table>
                                                        <asp:Repeater ID="ListRepeater2Detail" runat="server" OnItemDataBound="ListRepeater2Detail_ItemDataBound">
                                                            <ItemTemplate>
                                                                <tr id="DetailRepeaterItemTemplate" runat="server" class="repeater">
                                                                    <td style="width: 120px">
                                                                        <asp:Literal ID="ProductCodeLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td style="width: 288px">
                                                                        <asp:Literal ID="DescriptionLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td style="width: 50px">
                                                                        <asp:Literal ID="QtyLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td style="width: 150px">
                                                                        <asp:Literal ID="UnitLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td style="width: 150px">
                                                                        <asp:Literal ID="PriceLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td style="width: 175px">
                                                                        <asp:Literal ID="TotalForexLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="table-navigation-scroll3">
                                                    <div class="up">
                                                    </div>
                                                    <div class="down">
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
    </div>
    <div>
        <asp:Panel runat="server" ID="ReportPanel" Visible="false">
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
