<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Voucher.aspx.cs" Inherits="POS.POSInterface.Voucher.Voucher" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<b>Voucher</b>
    <table>
        <tr>
            <td>
                Voucher No
            </td>
            <td>
                :
            </td>
            <td>
                <asp:TextBox ID="VoucherNoTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Nominal
            </td>
            <td>
                :
            </td>
            <td>
                <asp:TextBox ID="NominalTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text="1" Width="30" />
            </td>
            <td>
                <asp:Button ID="Button2" runat="server" Text="2" Width="30" />
            </td>
            <td>
                <asp:Button ID="Button3" runat="server" Text="3" Width="30" />
            </td>
            <td rowspan="2">
                <asp:Button ID="Button4" runat="server" Text="OK" Width="30" Height="55" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button5" runat="server" Text="4" Width="30" />
            </td>
            <td>
                <asp:Button ID="Button6" runat="server" Text="5" Width="30" />
            </td>
            <td>
                <asp:Button ID="Button7" runat="server" Text="6" Width="30" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button8" runat="server" Text="7" Width="30" />
            </td>
            <td>
                <asp:Button ID="Button9" runat="server" Text="8" Width="30" />
            </td>
            <td>
                <asp:Button ID="Button10" runat="server" Text="9" Width="30" />
            </td>
            <td rowspan="2">
                <asp:Button ID="Button11" runat="server" Text="CLR" Width="30" Height="55" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button12" runat="server" Text="-" Width="30" />
            </td>
            <td>
                <asp:Button ID="Button13" runat="server" Text="0" Width="30" />
            </td>
            <td>
                <asp:Button ID="Button14" runat="server" Text="." Width="30" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="BackButton" runat="server" Text="BACK" Width="150" />
            </td>
        </tr>
    </table>
</body>
</html>
