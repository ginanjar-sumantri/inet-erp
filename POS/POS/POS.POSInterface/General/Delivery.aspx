<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Delivery.aspx.cs" Inherits="General_Delivery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <b>Delivery</b>
    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            Cashier
                        </td>
                        <td>
                            <asp:TextBox ID="CashierTextBox" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Transaction no
                        </td>
                        <td>
                            <asp:TextBox ID="TransactionNoTextBox" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Phone no
                        </td>
                        <td>
                            <asp:TextBox ID="PhoneNoTextBox" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="SearchButton" runat="server" Text="SEARCH" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Salutation
                        </td>
                        <td>
                            <asp:TextBox ID="SalutationTextBox" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Name
                        </td>
                        <td>
                            <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Address
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="AddressTextBox" runat="server" TextMode="MultiLine" Width="300"
                                Height="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            House no
                        </td>
                        <td>
                            <asp:TextBox ID="HouseNoTextBox" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            Blok
                        </td>
                        <td>
                            <asp:TextBox ID="BlokTextBox" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            Floor
                        </td>
                        <td>
                            <asp:TextBox ID="FloorTextBox" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table>
                    <tr>
                        <td>
                            Aktive rider
                        </td>
                    </tr>
                    <asp:Repeater ID="RepeaterList" runat="server">
                        <ItemTemplate>
                            <tr id="RepeaterTemplate" runat="server">
                                <td>
                                    <asp:Literal ID="AktiveRiderLiteral" runat="server"></asp:Literal>
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
            <td width="150">
                <asp:Button ID="OrderButton" runat="server" Text="ORDER" Width="80" />
            </td>
            <td width="150">
                <asp:Button ID="AddNewButton" runat="server" Text="ADD NEW" Width="80" />
            </td>
            <td width="150">
                <asp:Button ID="BackButton" runat="server" Text="BACK" Width="80" />
            </td>
        </tr>
    </table>
</body>
</html>
