<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CloseShift.aspx.cs" Inherits="POS.POSInterface.General.CloseShift" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../CSS/orange/style.css" media="all" rel="Stylesheet" type="text/css" />

    <script src="../CSS/orange/jquery-1.4.4.min.js" type="text/javascript"></script>

    <script src="../CSS/orange/jquery.corner.js" type="text/javascript"></script>

    <script src="../CSS/orange/style.js" type="text/javascript"></script>

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
        $('#bodyCloseShift .container .header .right .date #tanggal').text(Hari[dateObject.getDay()] + ', ' + dateObject.getDate() + ' ' + Bulan[dateObject.getMonth()] + ' ' + (dateObject.getYear() + 1900));
        $('#bodyCloseShift .container .header .right .date #jam').text(hour + ':' + minute + ':' + second);
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
            $('#bodyCloseShift .container .header .right .date #tanggal').text(Hari[dateObject.getDay()] + ', ' + dateObject.getDate() + ' ' + Bulan[dateObject.getMonth()] + ' ' + (dateObject.getYear() + 1900));
            $('#bodyCloseShift .container .header .right .date #jam').text(hour + ':' + minute + ':' + second);
        }
        var x = setInterval("updateJam();", 1000);
    </script>

    <script language="javascript" type="text/javascript">
        function OpenBooked() {
            var _result = false;

            if (confirm("Are you sure want to close this transaction ?") == true) {
                _result = true;
            }
            else {
                _result = false;
            }

            return _result
        }
    </script>

    <title>POS - Close Shift</title>
</head>
<body id="bodyCloseShift">
    <form id="form1" runat="server">
    <div class="container" id="Menu" runat="server">
        <div class="header">
            <div class="left">
                <div class="title">
                    <div class="text">
                        Pos Close Shift
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
        <asp:Panel runat="server" ID="FormPanel">
            <div class="content">
                <div class="content-top">
                    <div class="title" style="padding-top: 8px; padding-bottom: 17px; padding-left: 5px;">
                        <a style="font-size: 18px; vertical-align: top;">Close Shift</a>
                        <br />
                        <table style="vertical-align: top; text-align: left;">
                            <tr>
                                <td>
                                    <a style="width: 10px; height: 8px; font-size: 12px;">Cashier ID</a>
                                </td>
                                <td>
                                    <a style="width: 10px; height: 8px; font-size: 12px">:</a>
                                </td>
                                <td>
                                    <asp:Label ID="CashierIDLabel" runat="server" Style="width: 12px; height: 8px; font-size: 12px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <a style="width: 10px; height: 8px; font-size: 12px;">Cashier Employee</a>
                                </td>
                                <td>
                                    <a style="width: 10px; height: 8px; font-size: 12px">:</a>
                                </td>
                                <td>
                                    <asp:Label ID="CashierEmployeeLabel" runat="server" Style="width: 11px; height: 8px;
                                        font-size: 12px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <a style="width: 10px; height: 8px; font-size: 12px;">Cashier Account</a>
                                </td>
                                <td>
                                    <a style="width: 10px; height: 8px; font-size: 12px">:</a>
                                </td>
                                <td>
                                    <asp:Label ID="CashierAccountLabel" runat="server" Style="width: 11px; height: 8px;
                                        font-size: 12px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="cashier" style="padding-top: 18px; padding-bottom: 18px">
                        <div class="petty-cash">
                            <div class="label" style="width: 75px">
                                Cash</div>
                            <div class="sep">
                                :</div>
                            <div class="input">
                                <asp:Literal ID="CashLiteral" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                        <div class="petty-cash">
                            <div class="label" style="width: 75px">
                                Debit</div>
                            <div class="sep">
                                :</div>
                            <div class="input">
                                <asp:Literal ID="DebitLiteral" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                        <div class="petty-cash">
                            <div class="label" style="width: 75px">
                                Kredit</div>
                            <div class="sep">
                                :</div>
                            <div class="input">
                                <asp:Literal ID="KreditLiteral" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                        <div class="petty-cash">
                            <div class="label" style="width: 75px">
                                Voucher</div>
                            <div class="sep">
                                :</div>
                            <div class="input">
                                <asp:Literal ID="VoucherLiteral" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="clear">
                            <asp:HiddenField ID="CashHiddenField" runat="server" />
                            <asp:HiddenField ID="DebitHiddenField" runat="server" />
                            <asp:HiddenField ID="KreditHiddenField" runat="server" />
                            <asp:HiddenField ID="VoucherHiddenField" runat="server" />
                        </div>
                    </div>
                    <div class="ending-balance" style="padding-left: 11px; padding-right: 11px; padding-bottom: 10px;">
                        <%--<div class="left">
                        <div class="label">
                            Ending Balance (Rp)</div>
                        <div class="input">
                            <asp:Literal ID="EndingBalanceLiteral" runat="server" Text="0"></asp:Literal>
                        </div>
                    </div>--%>
                        <div class="button">
                            <div id="ApproveForm" runat="server">
                                <asp:Button ID="CloseButton" runat="server" Text="Close" PostBackUrl="FormCloseShift.aspx" /> <%--OnClick="CloseButton_Click"--%>
                                <asp:Button ID="PrintButton" runat="server" Text="Print" OnClick="PrintButton_Click" />
                                <%--<asp:Button ID="ApproveButton" runat="server" Text="Approve" />--%>
                                <asp:Button ID="BackButton" runat="server" Text="Back" PostBackUrl="Cashier.aspx" />
                            </div>
                        </div>
                    </div>
                </div>
                <div style="float: left; padding-left: 4px; width: 100%;">
                    <div class="content-mid">
                        <div class="table-header">
                            <table>
                                <tr>
                                    <td class="Number" style="width: 5px">
                                        No.
                                    </td>
                                    <td class="TransactionNo" style="width: 100px">
                                        Transaction No.
                                    </td>
                                    <td class="TransactionDate" style="width: 80px">
                                        Transaction Date
                                    </td>
                                    <%--<td class="ReferenceNo" style="width: 80px">
                                    Reference No.
                                </td>--%>
                                    <td class="PaymentStatus" style="width: 60px">
                                        Payment Type
                                    </td>
                                    <%--<td class="DeliveryStatus" style="width: 60px">
                                    Divisi
                                </td>--%>
                                    <td class="AmountTrans" style="width: 100px">
                                        Amount Transaction
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="table-container-scroll">
                            <table>
                                <asp:Repeater ID="ListRepeater" runat="server" OnItemDataBound="ListRepeater_ItemDataBound">
                                    <ItemTemplate>
                                        <tr id="RepeaterTemplate" runat="server">
                                            <td class="Number" style="width: 15px">
                                                <asp:Literal ID="Number" runat="server" />
                                            </td>
                                            <td class="TransactionNo" style="width: 107px">
                                                <asp:Literal ID="TransactionNoLiteral" runat="server"></asp:Literal>
                                            </td>
                                            <td class="TransactionDate" style="width: 80px">
                                                <asp:Literal ID="TransactionDateLiteral" runat="server"></asp:Literal>
                                            </td>
                                            <%--<td class="ReferenceNo" style="width: 80px">
                                            <asp:Literal ID="ReferenceNoLiteral" runat="server"></asp:Literal>
                                        </td>--%>
                                            <td class="PaymentStatus" style="width: 60px">
                                                <asp:Literal ID="PaymentTypeLiteral" runat="server"></asp:Literal>
                                            </td>
                                            <%--<td class="DeliveryStatus" style="width: 60px">
                                            <asp:Literal ID="DivisiLiteral" runat="server"></asp:Literal>
                                        </td>--%>
                                            <td class="AmountTrans" style="width: 100px">
                                                <asp:Literal ID="AmountTransactionLiteral" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </div>
                    <div class="table-navigation-scroll">
                        <div class="up">
                        </div>
                        <div class="down">
                        </div>
                    </div>
                </div>
                <div class="content-bottom">
                    <div class="total">
                        <div class="label">
                            Total :</div>
                        <div class="input">
                            <asp:Literal ID="TotalLiteral" runat="server" Text="0"></asp:Literal></div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="ReportPanel">
            <div style="background-color: white; background-image: none;">
                <table cellpadding="3" cellspacing="0" width="100%" border="0">
                    <tr>
                        <td style="text-align: right;">
                            <input type="button" value="Go Back" onclick="history.back()" style="height: 40px;">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" ShowPrintButton="true"
                                Height="768px" ShowZoomControl="true" ZoomMode="Percent" ZoomPercent="100">
                            </rsweb:ReportViewer>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
