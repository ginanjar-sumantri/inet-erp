<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchMember.aspx.cs" Inherits="General_SearchMember" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <b>Search Member</b>
    <table>
        <tr>
            <td>
                Search Field
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
    <table>
        <tr>
            <td>
                <asp:Button ID="SearchButton" runat="server" Text="Search" Width="70" />
            </td>
            <td>
                <asp:Button ID="ShowAllButton" runat="server" Text="Show All" Width="70" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td align="center">
                Member no
            </td>
            <td align="center">
                Name
            </td>
            <td align="center">
                Date of birth
            </td>
            <td align="center">
                Hp number
            </td>
        </tr>
        <asp:Repeater ID="ListRepeater" runat="server">
            <ItemTemplate>
                <tr id="RepeaterDataBound" runat="server">
                    <td>
                        <asp:Literal ID="MemberNoLiteral" runat="server"></asp:Literal>
                    </td>
                    <td>
                        <asp:Literal ID="NameLiteral" runat="server"></asp:Literal>
                    </td>
                    <td>
                        <asp:Literal ID="DateOfBirthLiteral" runat="server"></asp:Literal>
                    </td>
                    <td>
                        <asp:Literal ID="HpNumberLiteral" runat="server"></asp:Literal>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</body>
</html>
