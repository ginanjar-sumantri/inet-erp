<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SplitPayment.aspx.cs" Inherits="General_SplitPayment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <b>Split Payment</b>
    <table>
        <tr>
            <td valign="top">
                <table>
                    <tr>
                        <td width="70" align="center">
                            Check
                        </td>
                        <td width="70" align="center">
                            Product
                        </td>
                        <td width="70" align="center">
                            Price
                        </td>
                        <td width="70" align="center">
                            Qty
                        </td>
                        <td width="70" align="center">
                            Amount
                        </td>
                    </tr>
                    <asp:Repeater ID="ListRepeater" runat="server">
                        <ItemTemplate>
                            <tr id="RepeaterTemplate" runat="server">
                                <td align="center">
                                    <asp:CheckBox ID="NoCheckBox" runat="server" />
                                </td>
                                <td>
                                    <asp:Literal ID="ProductLiteral" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="PriceLiteral" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="QtyLiteral" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="AmountLiteral" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="Button3" runat="server" Text="-->" Width="70" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="Button4" runat="server" Text="<--" Width="70" />
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table>
                    <tr>
                        <td width="70" align="center">
                            Product
                        </td>
                        <td width="70" align="center">
                            Price
                        </td>
                        <td width="70" align="center">
                            Qty
                        </td>
                        <td width="70" align="center">
                            Amount
                        </td>
                        <td width="70" align="center">
                            Cancel
                        </td>
                    </tr>
                    <asp:Repeater ID="ListRepeater2" runat="server">
                        <ItemTemplate>
                            <tr id="RepeaterTemplate2" runat="server">
                                <td>
                                    <asp:Literal ID="ProductLiteral2" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="PriceLiteral" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="QtyLiteral" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="AmountLiteral" runat="server"></asp:Literal>
                                </td>
                                <td align="center">
                                    <asp:Button ID="Button5" runat="server" Text="x" Width="30" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Total Price
            </td>
            <td>
                <asp:TextBox ID="TotalPriceTextBox" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="Button5" runat="server" Text="<" />
            </td>
            <td>
                <asp:TextBox ID="TextBox" runat="server" Width="30"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" Text=">" />
            </td>
            <td>
                Total Payment
            </td>
            <td>
                <asp:TextBox ID="TotalPaymentTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Button ID="BackButton" runat="server" Text="Back" Width="55" />
            </td>
            <td>
                <asp:Button ID="SettlementButton" runat="server" Text="Settlement" Width="70" />
            </td>
            <td>
                <asp:Button ID="CancelAllButton" runat="server" Text="Cancel All" Width="70" />
            </td>
        </tr>
    </table>
</body>
</html>
