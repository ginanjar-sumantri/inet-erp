<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="StockTransferExternalSJDetailEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.StockTransferExternalSJ.StockTransferExternalSJDetailEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="SaveButton" runat="server">
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
                                Product
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ProductTextBox" runat="server" Width="420" BackColor="#CCCCCC" ReadOnly="true">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Location
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="LocationTextBox" runat="server" Width="350" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
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
                                <asp:TextBox ID="QtyTextBox" runat="server" MaxLength="18" Width="150"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="QtyRequiredFieldValidator" runat="server" Text="*"
                                    ErrorMessage="Qty Must Be Filled" ControlToValidate="QtyTextBox"></asp:RequiredFieldValidator>
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
                                <asp:TextBox runat="server" ID="UnitTextBox" BackColor="#CCCCCC" Width="210" ReadOnly="true"></asp:TextBox>
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
                                <asp:TextBox ID="RemarkTextBox" runat="server" Width="300" Height="80" TextMode="MultiLine"></asp:TextBox>
                                <asp:TextBox ID="CounterTextBox" runat="server" Width="50" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                characters left
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
                                <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ResetButton" runat="server" CausesValidation="False" OnClick="ResetButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
