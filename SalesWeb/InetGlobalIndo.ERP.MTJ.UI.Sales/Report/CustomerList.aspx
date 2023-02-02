<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="CustomerList.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Sales.Report.CustomerList" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
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
                                <td valign="top">
                                    Customer Group Type
                                </td>
                                <td valign="top">
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="CustGroupTypeDropDownList" runat="server" RepeatColumns="3">
                                        <asp:ListItem Value="null" Text="[Choose One]"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="LOKAL"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="EXPORT"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="ALL"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Customer Group
                                </td>
                                <td valign="top">
                                    :
                                </td>
                                <td>
                                    <asp:CheckBoxList ID="CustGroupCheckBoxList" runat="server" RepeatColumns="3">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Customer Type
                                </td>
                                <td valign="top">
                                    :
                                </td>
                                <td>
                                    <asp:CheckBoxList ID="CustTypeCheckBoxList" runat="server" RepeatColumns="3">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    City
                                </td>
                                <td valign="top">
                                    :
                                </td>
                                <td>
                                    <asp:CheckBoxList ID="CityCheckBoxList" runat="server" RepeatColumns="3">
                                    </asp:CheckBoxList>
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
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <rsweb:ReportViewer ID="ReportViewer" runat="server" Width="100%" ShowPrintButton="true"
                    ShowZoomControl="true" ZoomMode="Percent" ZoomPercent="100">
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
