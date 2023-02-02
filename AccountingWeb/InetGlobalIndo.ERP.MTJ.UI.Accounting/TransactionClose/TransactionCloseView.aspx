<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="TransactionCloseView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.TransactionClose.TransactionCloseView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1">
        <table cellpadding="3" cellspacing="0" width="0" border="0">
            <tr>
                <td class="tahoma_14_black">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
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
                                Start Year
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="YearTextBox" runat="server" Width="50" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox ID="PeriodTextBox" runat="server" Width="50" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox ID="YearEndTextBox" runat="server" Width="50" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox ID="PeriodEndTextBox" runat="server" Width="50" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Description
                            </td>
                            <td>
                                :
                            </td>
                            <td colspan="5">
                                <asp:TextBox runat="server" ID="DescTextBox" Width="300" Height="80" TextMode="MultiLine"
                                    ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Status
                            </td>
                            <td>
                                :
                            </td>
                            <td colspan="5">
                                <asp:CheckBox ID="StatusCheckBox" Text="Locked" runat="server" Enabled="false" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                                <%--</td>
                            <td>--%>
                                &nbsp;<asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False"
                                    OnClick="CancelButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
