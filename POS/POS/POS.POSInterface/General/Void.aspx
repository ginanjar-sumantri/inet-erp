<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Void.aspx.cs" Inherits="General_Void" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
                <b>VOID</b>
            </td>
        </tr>
        <tr>
            <td>
                Search field
            </td>
            <td>
                <asp:DropDownList ID="SearchFieldDropDownList" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Condition
            </td>
            <td>
                <asp:DropDownList ID="ConditionDropDownList" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="ResultConditionTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    </form>
    <form>
    <table>
        <tr>
            <td>
                <asp:Button ID="SearchButton2" runat="server" Text="SEARCH" />
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="SHOW ALL" />
            </td>
        </tr>
    </table>
    </form>
    <form>
    <table>
        <tr>
            <td style="width: 20px" align="center">
                Transaction no
            </td>
            <td style="width: 20px" align="center">
                Member no
            </td>
            <td style="width: 20px" align="center">
                Name
            </td>
        </tr>
        <asp:Repeater ID="ListRepeater" runat="server" >
            <ItemTemplate>
                <tr id="RepeaterItemTemplate" runat="server">
                    <td>
                        <asp:Literal runat="server" ID="TransactionNoLiteral"></asp:Literal>
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="MemberNoLiteral"></asp:Literal>
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="NameLiteral"></asp:Literal>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    </form>
    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <form>
                            <table>
                                <tr>
                                    <td>
                                        <b>VOID</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Quantity
                                    </td>
                                    <td>
                                        <asp:TextBox ID="QuantityTextBox" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            </form>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button runat="server" ID="Button2" Text="7" Width="40" />
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="Button3" Text="8" Width="40" />
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="Button4" Text="9" Width="40" />
                                    </td>
                                    <td rowspan="2">
                                        <asp:Button runat="server" ID="Button5" Text="OK" Width="40" Height="55" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button runat="server" ID="Button6" Text="4" Width="40" />
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="Button7" Text="5" Width="40" />
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="Button8" Text="6" Width="40" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button runat="server" ID="Button9" Text="1" Width="40" />
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="Button10" Text="2" Width="40" />
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="Button11" Text="3" Width="40" />
                                    </td>
                                    <td rowspan="2">
                                        <asp:Button runat="server" ID="Button12" Text="CLR" Width="40" Height="55" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button runat="server" ID="Button13" Text="." Width="40" />
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="Button14" Text="0" Width="40" />
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="Button15" Text="00" Width="40" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table>
                    <tr>
                        <td>
                            <b>Reason</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            No
                        </td>
                        <td width="100">
                            Reason
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>
