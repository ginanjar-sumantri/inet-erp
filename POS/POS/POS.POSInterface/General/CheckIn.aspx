<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckIn.aspx.cs" Inherits="General_CheckIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <b>CHECK IN</b>
    <table>
        <tr>
            <td>
                Member id
            </td>
            <td>
                <asp:TextBox ID="MemberIDTextBox" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" Text=".." />
            </td>
        </tr>
        <tr>
            <td>
                Customer Name
            </td>
            <td>
                <asp:TextBox ID="CustomerNameTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Hp Number
            </td>
            <td>
                <asp:TextBox ID="HpNumberTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Button ID="Button2" runat="server" Text="OK" Width="80" />
            </td>
            <td>
                <asp:Button ID="Button3" runat="server" Text="Cancel" Width="80" />
            </td>
            <td>
                <asp:Button ID="Button4" runat="server" Text="Reset" Width="80" />
            </td>
            <td>
                <asp:Button ID="Button5" runat="server" Text="Regristration" Width="90" />
            </td>
        </tr>
    </table>
</body>
</html>
