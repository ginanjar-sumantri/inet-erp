<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.Report.CashflowProjection, App_Web_o0azb2_0" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>
    <script type="text/javascript" src="../calendar/JScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="ViewButton" runat="server">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td class="tahoma_14_black">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel runat="server" ID="MenuPanel">
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <td>
                                                Date
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <table border="0" cellpadding="3" cellspacing="0" width="0">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="DateTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                                            <asp:Literal ID="DateLiteral" runat="server"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td>
                                                Range Type
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="RangeTypeDDL">
                                                <asp:ListItem Text="Day" Value="Day"></asp:ListItem>
                                                <asp:ListItem Text="Month" Value="Month" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Year" Value="Year"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td>
                                                Range
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="RangeTextBox" runat="server" Width="50"> </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <table cellpadding="3" cellspacing="0" width="0" border="0">
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
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <rsweb:ReportViewer ID="ReportViewer" runat="server" Width="100%" ShowPrintButton="true"
                        ShowZoomControl="true" ZoomMode="Percent" ZoomPercent="100" Height="1500">
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
