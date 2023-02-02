<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="ReportSummaryCustomerLogVisitByCustomer.aspx.cs" Inherits="VTSWeb.UI.ReportSummaryCustomerLogVisitByCustomer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <table>
        <tr>
            <td class="tahoma_14_black">
                <b>
                    <asp:Literal ID="PageTitleLiteral" runat="server" /></b>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="PanelSearch">
                    <table border="0" cellpadding="3" cellspacing="0" width="0">
                        <tr>
                            <td>
                                Start Date Between</td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="StartDateTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                &nbsp;
                                <input id="button1" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_StartDateTextBox,'yyyy-mm-dd',this)"
                                    value="..." />
                            </td>
                            <td>
                                s/d
                            </td>
                            <td>
                                <asp:TextBox ID="EndDateTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                &nbsp;
                                <input id="button2" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_EndDateTextBox,'yyyy-mm-dd',this)"
                                    value="..." />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Company Between
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="StartNameTextBox" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                s/d
                            </td>
                            <td>
                                <asp:TextBox ID="EndNameTextBox" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <table border="0" cellpadding="3" cellspacing="0" width="0">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="ViewButton" runat="server" OnClick="ViewButton_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr align="center">
            <td align="center" class="style1">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="200%" ShowPrintButton="true"
                    ShowZoomControl="true" ZoomMode="Percent" ZoomPercent="100">
                </rsweb:ReportViewer>
            </td>
        </tr>
    </table>
</asp:Content>
