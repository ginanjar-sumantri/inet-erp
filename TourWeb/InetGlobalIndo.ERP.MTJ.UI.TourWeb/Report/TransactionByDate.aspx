<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="TransactionByDate.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Tour.Report.TransactionByDate" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <table cellpadding="3" cellspacing="0" width="100%" border="0">
        <tr>
            <td class="tahoma_14_black">
                <b>
                    <asp:Literal ID="PageTitleLiteral" runat="server" />
                </b>
            </td>
        </tr>
        <tr>
            <td class="warning">
                <asp:Literal ID="Literal1" runat="server" Text="* This Report is Best Printed on Legal Size Paper" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="MenuPanel">
                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td colspan="3">
                                <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="WarningLabel" runat="server" CssClass="warning"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset>
                                    <table border="0" cellpadding="3" cellspacing="0" width="0">
                                        <tr>
                                            <td>
                                                Report Title
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="RekapTypeDDL" runat="server">
                                                    <asp:ListItem Text="Report Recapitulation Ticketing" Value="RptRpTicketing"></asp:ListItem>
                                                    <asp:ListItem Text="Report Recapitulation Voucher Hotel" Value="RptRpHotel"></asp:ListItem>
                                                    <asp:ListItem Text="Report Transaction Ticketing AR" Value="RptTrTicketingAR"></asp:ListItem>
                                                    <asp:ListItem Text="Report Transaction Ticketing Cash" Value="RptTrTicketingCash"></asp:ListItem>
                                                    <asp:ListItem Text="Report Transaction Voucher Hotel AR" Value="RptTrHotelAR"></asp:ListItem>
                                                    <asp:ListItem Text="Report Transaction Voucher Hotel Cash" Value="RptTrHotelCash"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Begin Date
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="BeginDateTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                                &nbsp;
                                                <asp:Literal ID="BeginDateLiteral" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                End Date
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="EndDateTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                                &nbsp;
                                                <asp:Literal ID="EndDateLiteral" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <table border="0" cellpadding="3" cellspacing="0" width="0">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ViewButton" runat="server" OnClick="ViewButton_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ResetButton" runat="server" CausesValidation="false" OnClick="ResetButton_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" ShowPrintButton="true"
                    ShowZoomControl="true" ZoomMode="Percent" ZoomPercent="100">
                </rsweb:ReportViewer>
            </td>
        </tr>
    </table>
</asp:Content>
