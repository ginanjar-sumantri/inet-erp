<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="CustomerNoteDetailView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.CustomerNote.CustomerNoteDetailView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel DefaultButton="EditButton" ID="Panel1" runat="server">
        <table cellpadding="3" cellspacing="0" width="0" border="0">
            <tr>
                <td class="tahoma_14_black">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
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
                    SJ No
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="SJNoTextBox" BackColor="#CCCCCC"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Product
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="ProductTextBox" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    SO No
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="SONoTextBox" BackColor="#CCCCCC"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Qty
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="QtyTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Unit
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="UnitTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Price
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="PriceTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Amount
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="AmountTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                </td>
            </tr>
            <tr valign="top">
                <td>
                    Remark
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" MaxLength="500" Height="80"
                        TextMode="MultiLine" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="EditButton" runat="server" CausesValidation="False" OnClick="EditButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
