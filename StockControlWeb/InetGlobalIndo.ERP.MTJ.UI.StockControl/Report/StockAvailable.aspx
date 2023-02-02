<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="StockAvailable.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.Report.StockAvailable" %>

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
                                <table border="0" cellpadding="3" cellspacing="0" width="0">
                                    <tr>
                                        <td>
                                            Type
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="FgReportDropDownList" runat="server">
                                                <asp:ListItem Value="null" Text="[Choose One]"></asp:ListItem>
                                                <asp:ListItem Text="Product" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Product Group" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Product Sub Group" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Product Type" Value="3"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Type Must Be Choosed"
                                                Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="FgReportDropDownList"></asp:CustomValidator>
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
                                            <asp:TextBox ID="StartDateTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                            <%--<input id="date_start" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_StartDateTextBox,'yyyy-mm-dd',this)"
                                                value="..." />--%>
                                                <asp:Literal ID="StartDateLiteral" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            Summary Report Type
                                        </td>
                                        <td valign="top">
                                            :
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="MinRadioButtonList" runat="server">
                                                <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Qty Available <= Qty Minimum" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Qty Available > Qty Minimum" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            Summary Report Type
                                        </td>
                                        <td valign="top">
                                            :
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="MaxRadioButtonList" runat="server">
                                                <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Qty Available <= Qty Maximum" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Qty Available > Qty Maximum" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            Product Type
                                        </td>
                                        <td valign="top">
                                            :
                                        </td>
                                        <td>
                                            <asp:CheckBoxList runat="server" ID="ProductTypeCheckBoxList" RepeatColumns="3">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            Product Sub Group
                                        </td>
                                        <td valign="top">
                                            :
                                        </td>
                                        <td>
                                            <asp:CheckBoxList runat="server" ID="ProductSubGrpCheckBoxList" RepeatColumns="3">
                                            </asp:CheckBoxList>
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
