<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="CashflowActualCash.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.Report.CashflowActualCash" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
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
                                <asp:Label ID="WarningLabel" runat="server" CssClass="warning"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellpadding="3" cellspacing="0" width="0">
                                    <tr>
                                        <td>
                                            Start Year
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="YearTextBox" runat="server" Width="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Start Year Must Be Filled"
                                                Text="*" ControlToValidate="YearTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Start Period
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="PeriodTextBox" runat="server" Width="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Start Period Must Be Filled"
                                                Text="*" ControlToValidate="PeriodTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            End Year
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="YearEndTextBox" runat="server" Width="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="End Year Must Be Filled"
                                                Text="*" ControlToValidate="YearEndTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            End Period
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="PeriodEndTextBox" runat="server" Width="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="FACodeRequiredFieldValidator" runat="server" ErrorMessage="End Period Must Be Filled"
                                                Text="*" ControlToValidate="PeriodEndTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
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
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="1500px"
                    ShowPrintButton="true" ShowZoomControl="true" ZoomMode="Percent" ZoomPercent="100">
                </rsweb:ReportViewer>
            </td>
        </tr>
    </table>
</asp:Content>
