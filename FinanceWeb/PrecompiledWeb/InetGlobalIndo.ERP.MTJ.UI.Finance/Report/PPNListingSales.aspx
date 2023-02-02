<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.Report.PPNListingSales, App_Web_vzyiodfh" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

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
                <td class="warning">
                    <asp:Literal ID="Literal1" runat="server" Text="* This Report is Best Printed on Legal Size Paper" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel runat="server" ID="MenuPanel">
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td colspan="3">
                                    <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <td>
                                                Type
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="FgTypeDropDownList">
                                                    <asp:ListItem Value="null" Text="[Choose One]"></asp:ListItem>
                                                    <asp:ListItem Value="0">Customer</asp:ListItem>
                                                    <asp:ListItem Value="1">PPN No.</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Type Must Be Choosed"
                                                    Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="FgTypeDropDownList"></asp:CustomValidator>
                                            </td>
                                        </tr>
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
                                                            <asp:TextBox ID="StartDateTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                                            <%--<input id="date_start" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_StartDateTextBox,'yyyy-mm-dd',this)"
                                                                value="..." />--%>
                                                            <asp:Literal ID="StartDateLiteral" runat="server"></asp:Literal>
                                                        </td>
                                                        <td>
                                                            to
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="EndDateTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                                            <%--<input id="Button1" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_EndDateTextBox,'yyyy-mm-dd',this)"
                                                                value="..." />--%>
                                                            <asp:Literal ID="EndDateLiteral" runat="server"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                </table>
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
                        ShowZoomControl="true" ZoomMode="Percent" Height="1800px" ZoomPercent="100">
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
